using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x0200041B RID: 1051
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class ViewLoader
	{
		// Token: 0x060026AB RID: 9899 RVA: 0x000B9C14 File Offset: 0x000B7E14
		internal ViewLoader(StorageMappingItemCollection mappingCollection)
		{
			this.m_mappingCollection = mappingCollection;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000B9C75 File Offset: 0x000B7E75
		internal ModificationFunctionMappingTranslator GetFunctionMappingTranslator(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<EntitySetBase, ModificationFunctionMappingTranslator>(extent, workspace, this.m_functionMappingTranslators, extent);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000B9C86 File Offset: 0x000B7E86
		internal Set<EntitySet> GetAffectedTables(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<EntitySetBase, Set<EntitySet>>(extent, workspace, this.m_affectedTables, extent);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000B9C97 File Offset: 0x000B7E97
		internal AssociationSetMetadata GetAssociationSetMetadata(AssociationSet associationSet, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<AssociationSet, AssociationSetMetadata>(associationSet, workspace, this.m_associationSetMetadata, associationSet);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000B9CA8 File Offset: 0x000B7EA8
		internal bool IsServerGen(EntitySetBase entitySetBase, MetadataWorkspace workspace, EdmMember member)
		{
			return this.SyncContains<EdmMember>(entitySetBase, workspace, this.m_serverGenProperties, member);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000B9CB9 File Offset: 0x000B7EB9
		internal bool IsNullConditionMember(EntitySetBase entitySetBase, MetadataWorkspace workspace, EdmMember member)
		{
			return this.SyncContains<EdmMember>(entitySetBase, workspace, this.m_isNullConditionProperties, member);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000B9CE0 File Offset: 0x000B7EE0
		private T_Value SyncGetValue<T_Key, T_Value>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Dictionary<T_Key, T_Value> dictionary, T_Key key)
		{
			return this.SyncInitializeEntitySet<T_Key, T_Value>(entitySetBase, workspace, (T_Key k) => dictionary[k], key);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000B9D10 File Offset: 0x000B7F10
		private bool SyncContains<T_Element>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Set<T_Element> set, T_Element element)
		{
			return this.SyncInitializeEntitySet<T_Element, bool>(entitySetBase, workspace, new Func<T_Element, bool>(set.Contains), element);
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000B9D28 File Offset: 0x000B7F28
		private TResult SyncInitializeEntitySet<TArg, TResult>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Func<TArg, TResult> evaluate, TArg arg)
		{
			this.m_readerWriterLock.EnterReadLock();
			try
			{
				if (this.m_affectedTables.ContainsKey(entitySetBase))
				{
					return evaluate(arg);
				}
			}
			finally
			{
				this.m_readerWriterLock.ExitReadLock();
			}
			this.m_readerWriterLock.EnterWriteLock();
			TResult result;
			try
			{
				if (this.m_affectedTables.ContainsKey(entitySetBase))
				{
					result = evaluate(arg);
				}
				else
				{
					this.InitializeEntitySet(entitySetBase, workspace);
					result = evaluate(arg);
				}
			}
			finally
			{
				this.m_readerWriterLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000B9DC8 File Offset: 0x000B7FC8
		private void InitializeEntitySet(EntitySetBase entitySetBase, MetadataWorkspace workspace)
		{
			EntityContainerMapping entityContainerMapping = (EntityContainerMapping)this.m_mappingCollection.GetMap(entitySetBase.EntityContainer);
			if (entityContainerMapping.HasViews)
			{
				this.m_mappingCollection.GetGeneratedView(entitySetBase, workspace);
			}
			Set<EntitySet> set = new Set<EntitySet>();
			if (entityContainerMapping != null)
			{
				Set<EdmMember> set2 = new Set<EdmMember>();
				EntitySetBaseMapping entitySetBaseMapping;
				if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					entitySetBaseMapping = entityContainerMapping.GetEntitySetMapping(entitySetBase.Name);
					this.m_serverGenProperties.Unite(ViewLoader.GetMembersWithResultBinding((EntitySetMapping)entitySetBaseMapping));
				}
				else
				{
					if (entitySetBase.BuiltInTypeKind != BuiltInTypeKind.AssociationSet)
					{
						throw new NotSupportedException();
					}
					entitySetBaseMapping = entityContainerMapping.GetAssociationSetMapping(entitySetBase.Name);
				}
				foreach (MappingFragment mappingFragment in ViewLoader.GetMappingFragments(entitySetBaseMapping))
				{
					set.Add(mappingFragment.TableSet);
					this.m_serverGenProperties.AddRange(ViewLoader.FindServerGenMembers(mappingFragment));
					set2.AddRange(ViewLoader.FindIsNullConditionColumns(mappingFragment));
				}
				if (0 < set2.Count)
				{
					foreach (MappingFragment mappingFragment2 in ViewLoader.GetMappingFragments(entitySetBaseMapping))
					{
						this.m_isNullConditionProperties.AddRange(ViewLoader.FindPropertiesMappedToColumns(set2, mappingFragment2));
					}
				}
			}
			this.m_affectedTables.Add(entitySetBase, set.MakeReadOnly());
			this.InitializeFunctionMappingTranslators(entitySetBase, entityContainerMapping);
			if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
			{
				AssociationSet associationSet = (AssociationSet)entitySetBase;
				if (!this.m_associationSetMetadata.ContainsKey(associationSet))
				{
					this.m_associationSetMetadata.Add(associationSet, new AssociationSetMetadata(this.m_affectedTables[associationSet], associationSet, workspace));
				}
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000BA2C0 File Offset: 0x000B84C0
		private static IEnumerable<EdmMember> GetMembersWithResultBinding(EntitySetMapping entitySetMapping)
		{
			foreach (EntityTypeModificationFunctionMapping typeFunctionMapping in entitySetMapping.ModificationFunctionMappings)
			{
				if (typeFunctionMapping.InsertFunctionMapping != null && typeFunctionMapping.InsertFunctionMapping.ResultBindings != null)
				{
					foreach (ModificationFunctionResultBinding binding in typeFunctionMapping.InsertFunctionMapping.ResultBindings)
					{
						yield return binding.Property;
					}
				}
				if (typeFunctionMapping.UpdateFunctionMapping != null && typeFunctionMapping.UpdateFunctionMapping.ResultBindings != null)
				{
					foreach (ModificationFunctionResultBinding binding2 in typeFunctionMapping.UpdateFunctionMapping.ResultBindings)
					{
						yield return binding2.Property;
					}
				}
			}
			yield break;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000BA2E0 File Offset: 0x000B84E0
		private void InitializeFunctionMappingTranslators(EntitySetBase entitySetBase, EntityContainerMapping mapping)
		{
			KeyToListMap<AssociationSet, AssociationEndMember> keyToListMap = new KeyToListMap<AssociationSet, AssociationEndMember>(EqualityComparer<AssociationSet>.Default);
			if (!this.m_functionMappingTranslators.ContainsKey(entitySetBase))
			{
				foreach (EntitySetBaseMapping entitySetBaseMapping in mapping.EntitySetMaps)
				{
					EntitySetMapping entitySetMapping = (EntitySetMapping)entitySetBaseMapping;
					if (0 < entitySetMapping.ModificationFunctionMappings.Count)
					{
						this.m_functionMappingTranslators.Add(entitySetMapping.Set, ModificationFunctionMappingTranslator.CreateEntitySetTranslator(entitySetMapping));
						using (IEnumerator<AssociationSetEnd> enumerator2 = entitySetMapping.ImplicitlyMappedAssociationSetEnds.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								AssociationSetEnd associationSetEnd = enumerator2.Current;
								AssociationSet parentAssociationSet = associationSetEnd.ParentAssociationSet;
								if (!this.m_functionMappingTranslators.ContainsKey(parentAssociationSet))
								{
									this.m_functionMappingTranslators.Add(parentAssociationSet, ModificationFunctionMappingTranslator.CreateAssociationSetTranslator(null));
								}
								AssociationSetEnd oppositeEnd = MetadataHelper.GetOppositeEnd(associationSetEnd);
								keyToListMap.Add(parentAssociationSet, oppositeEnd.CorrespondingAssociationEndMember);
							}
							continue;
						}
					}
					this.m_functionMappingTranslators.Add(entitySetMapping.Set, null);
				}
				foreach (EntitySetBaseMapping entitySetBaseMapping2 in mapping.RelationshipSetMaps)
				{
					AssociationSetMapping associationSetMapping = (AssociationSetMapping)entitySetBaseMapping2;
					if (associationSetMapping.ModificationFunctionMapping != null)
					{
						AssociationSet key = (AssociationSet)associationSetMapping.Set;
						this.m_functionMappingTranslators.Add(key, ModificationFunctionMappingTranslator.CreateAssociationSetTranslator(associationSetMapping));
						keyToListMap.AddRange(key, Enumerable.Empty<AssociationEndMember>());
					}
					else if (!this.m_functionMappingTranslators.ContainsKey(associationSetMapping.Set))
					{
						this.m_functionMappingTranslators.Add(associationSetMapping.Set, null);
					}
				}
			}
			foreach (AssociationSet key2 in keyToListMap.Keys)
			{
				this.m_associationSetMetadata.Add(key2, new AssociationSetMetadata(keyToListMap.EnumerateValues(key2)));
			}
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000BA6A8 File Offset: 0x000B88A8
		private static IEnumerable<EdmMember> FindServerGenMembers(MappingFragment mappingFragment)
		{
			foreach (ScalarPropertyMapping scalarPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<ScalarPropertyMapping>())
			{
				if (MetadataHelper.GetStoreGeneratedPattern(scalarPropertyMapping.Column) != StoreGeneratedPattern.None)
				{
					yield return scalarPropertyMapping.Property;
				}
			}
			yield break;
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000BA884 File Offset: 0x000B8A84
		private static IEnumerable<EdmMember> FindIsNullConditionColumns(MappingFragment mappingFragment)
		{
			foreach (ConditionPropertyMapping conditionPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<ConditionPropertyMapping>())
			{
				if (conditionPropertyMapping.Column != null && conditionPropertyMapping.IsNull != null)
				{
					yield return conditionPropertyMapping.Column;
				}
			}
			yield break;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000BAA64 File Offset: 0x000B8C64
		private static IEnumerable<EdmMember> FindPropertiesMappedToColumns(Set<EdmMember> columns, MappingFragment mappingFragment)
		{
			foreach (ScalarPropertyMapping scalarPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<ScalarPropertyMapping>())
			{
				if (columns.Contains(scalarPropertyMapping.Column))
				{
					yield return scalarPropertyMapping.Property;
				}
			}
			yield break;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000BAC9C File Offset: 0x000B8E9C
		private static IEnumerable<MappingFragment> GetMappingFragments(EntitySetBaseMapping setMapping)
		{
			foreach (TypeMapping typeMapping in setMapping.TypeMappings)
			{
				foreach (MappingFragment mappingFragment in typeMapping.MappingFragments)
				{
					yield return mappingFragment;
				}
			}
			yield break;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000BAFC4 File Offset: 0x000B91C4
		private static IEnumerable<PropertyMapping> FlattenPropertyMappings(ReadOnlyCollection<PropertyMapping> propertyMappings)
		{
			foreach (PropertyMapping propertyMapping in propertyMappings)
			{
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				if (complexPropertyMapping != null)
				{
					foreach (ComplexTypeMapping complexTypeMapping in complexPropertyMapping.TypeMappings)
					{
						foreach (PropertyMapping nestedPropertyMapping in ViewLoader.FlattenPropertyMappings(complexTypeMapping.AllProperties))
						{
							yield return nestedPropertyMapping;
						}
					}
				}
				else
				{
					yield return propertyMapping;
				}
			}
			yield break;
		}

		// Token: 0x04000E84 RID: 3716
		private readonly StorageMappingItemCollection m_mappingCollection;

		// Token: 0x04000E85 RID: 3717
		private readonly Dictionary<AssociationSet, AssociationSetMetadata> m_associationSetMetadata = new Dictionary<AssociationSet, AssociationSetMetadata>();

		// Token: 0x04000E86 RID: 3718
		private readonly Dictionary<EntitySetBase, Set<EntitySet>> m_affectedTables = new Dictionary<EntitySetBase, Set<EntitySet>>();

		// Token: 0x04000E87 RID: 3719
		private readonly Set<EdmMember> m_serverGenProperties = new Set<EdmMember>();

		// Token: 0x04000E88 RID: 3720
		private readonly Set<EdmMember> m_isNullConditionProperties = new Set<EdmMember>();

		// Token: 0x04000E89 RID: 3721
		private readonly Dictionary<EntitySetBase, ModificationFunctionMappingTranslator> m_functionMappingTranslators = new Dictionary<EntitySetBase, ModificationFunctionMappingTranslator>(EqualityComparer<EntitySetBase>.Default);

		// Token: 0x04000E8A RID: 3722
		private readonly ReaderWriterLockSlim m_readerWriterLock = new ReaderWriterLockSlim();
	}
}
