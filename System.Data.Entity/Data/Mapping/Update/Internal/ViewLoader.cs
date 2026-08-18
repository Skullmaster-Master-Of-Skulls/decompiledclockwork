using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Threading;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002D5 RID: 725
	internal class ViewLoader
	{
		// Token: 0x06002AA0 RID: 10912 RVA: 0x000A6F14 File Offset: 0x000A5114
		internal ViewLoader(StorageMappingItemCollection mappingCollection)
		{
			this.m_mappingCollection = mappingCollection;
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000A6F75 File Offset: 0x000A5175
		internal ModificationFunctionMappingTranslator GetFunctionMappingTranslator(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<EntitySetBase, ModificationFunctionMappingTranslator>(extent, workspace, this.m_functionMappingTranslators, extent);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000A6F86 File Offset: 0x000A5186
		internal Set<EntitySet> GetAffectedTables(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<EntitySetBase, Set<EntitySet>>(extent, workspace, this.m_affectedTables, extent);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000A6F97 File Offset: 0x000A5197
		internal AssociationSetMetadata GetAssociationSetMetadata(AssociationSet associationSet, MetadataWorkspace workspace)
		{
			return this.SyncGetValue<AssociationSet, AssociationSetMetadata>(associationSet, workspace, this.m_associationSetMetadata, associationSet);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000A6FA8 File Offset: 0x000A51A8
		internal bool IsServerGen(EntitySetBase entitySetBase, MetadataWorkspace workspace, EdmMember member)
		{
			return this.SyncContains<EdmMember>(entitySetBase, workspace, this.m_serverGenProperties, member);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000A6FB9 File Offset: 0x000A51B9
		internal bool IsNullConditionMember(EntitySetBase entitySetBase, MetadataWorkspace workspace, EdmMember member)
		{
			return this.SyncContains<EdmMember>(entitySetBase, workspace, this.m_isNullConditionProperties, member);
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000A6FCC File Offset: 0x000A51CC
		private T_Value SyncGetValue<T_Key, T_Value>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Dictionary<T_Key, T_Value> dictionary, T_Key key)
		{
			return this.SyncInitializeEntitySet<T_Key, T_Value>(entitySetBase, workspace, (T_Key k) => dictionary[k], key);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000A6FFC File Offset: 0x000A51FC
		private bool SyncContains<T_Element>(EntitySetBase entitySetBase, MetadataWorkspace workspace, Set<T_Element> set, T_Element element)
		{
			return this.SyncInitializeEntitySet<T_Element, bool>(entitySetBase, workspace, new Func<T_Element, bool>(set.Contains), element);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000A7014 File Offset: 0x000A5214
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

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000A70B4 File Offset: 0x000A52B4
		private void InitializeEntitySet(EntitySetBase entitySetBase, MetadataWorkspace workspace)
		{
			StorageEntityContainerMapping storageEntityContainerMapping = (StorageEntityContainerMapping)this.m_mappingCollection.GetMap(entitySetBase.EntityContainer);
			if (storageEntityContainerMapping.HasViews)
			{
				this.m_mappingCollection.GetGeneratedView(entitySetBase, workspace);
			}
			Set<EntitySet> set = new Set<EntitySet>();
			if (storageEntityContainerMapping != null)
			{
				Set<EdmMember> set2 = new Set<EdmMember>();
				StorageSetMapping storageSetMapping;
				if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					storageSetMapping = storageEntityContainerMapping.GetEntitySetMapping(entitySetBase.Name);
					this.m_serverGenProperties.Unite(this.GetMembersWithResultBinding((StorageEntitySetMapping)storageSetMapping));
				}
				else
				{
					if (entitySetBase.BuiltInTypeKind != BuiltInTypeKind.AssociationSet)
					{
						throw EntityUtil.NotSupported();
					}
					storageSetMapping = storageEntityContainerMapping.GetRelationshipSetMapping(entitySetBase.Name);
				}
				foreach (StorageMappingFragment storageMappingFragment in ViewLoader.GetMappingFragments(storageSetMapping))
				{
					set.Add(storageMappingFragment.TableSet);
					this.m_serverGenProperties.AddRange(ViewLoader.FindServerGenMembers(storageMappingFragment));
					set2.AddRange(ViewLoader.FindIsNullConditionColumns(storageMappingFragment));
				}
				if (0 < set2.Count)
				{
					foreach (StorageMappingFragment mappingFragment in ViewLoader.GetMappingFragments(storageSetMapping))
					{
						this.m_isNullConditionProperties.AddRange(ViewLoader.FindPropertiesMappedToColumns(set2, mappingFragment));
					}
				}
			}
			this.m_affectedTables.Add(entitySetBase, set.MakeReadOnly());
			this.InitializeFunctionMappingTranslators(entitySetBase, storageEntityContainerMapping);
			if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
			{
				AssociationSet associationSet = (AssociationSet)entitySetBase;
				if (!this.m_associationSetMetadata.ContainsKey(associationSet))
				{
					this.m_associationSetMetadata.Add(associationSet, new AssociationSetMetadata(this.m_affectedTables[associationSet], associationSet, workspace));
				}
			}
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000A726C File Offset: 0x000A546C
		private IEnumerable<EdmMember> GetMembersWithResultBinding(StorageEntitySetMapping entitySetMapping)
		{
			foreach (StorageEntityTypeModificationFunctionMapping typeFunctionMapping in entitySetMapping.ModificationFunctionMappings)
			{
				if (typeFunctionMapping.InsertFunctionMapping != null && typeFunctionMapping.InsertFunctionMapping.ResultBindings != null)
				{
					foreach (StorageModificationFunctionResultBinding storageModificationFunctionResultBinding in typeFunctionMapping.InsertFunctionMapping.ResultBindings)
					{
						yield return storageModificationFunctionResultBinding.Property;
					}
					IEnumerator<StorageModificationFunctionResultBinding> enumerator2 = null;
				}
				if (typeFunctionMapping.UpdateFunctionMapping != null && typeFunctionMapping.UpdateFunctionMapping.ResultBindings != null)
				{
					foreach (StorageModificationFunctionResultBinding storageModificationFunctionResultBinding2 in typeFunctionMapping.UpdateFunctionMapping.ResultBindings)
					{
						yield return storageModificationFunctionResultBinding2.Property;
					}
					IEnumerator<StorageModificationFunctionResultBinding> enumerator2 = null;
				}
				typeFunctionMapping = null;
			}
			IEnumerator<StorageEntityTypeModificationFunctionMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000A727C File Offset: 0x000A547C
		private void InitializeFunctionMappingTranslators(EntitySetBase entitySetBase, StorageEntityContainerMapping mapping)
		{
			KeyToListMap<AssociationSet, AssociationEndMember> keyToListMap = new KeyToListMap<AssociationSet, AssociationEndMember>(EqualityComparer<AssociationSet>.Default);
			if (!this.m_functionMappingTranslators.ContainsKey(entitySetBase))
			{
				foreach (StorageSetMapping storageSetMapping in mapping.EntitySetMaps)
				{
					StorageEntitySetMapping storageEntitySetMapping = (StorageEntitySetMapping)storageSetMapping;
					if (0 < storageEntitySetMapping.ModificationFunctionMappings.Count)
					{
						this.m_functionMappingTranslators.Add(storageEntitySetMapping.Set, ModificationFunctionMappingTranslator.CreateEntitySetTranslator(storageEntitySetMapping));
						using (IEnumerator<AssociationSetEnd> enumerator2 = storageEntitySetMapping.ImplicitlyMappedAssociationSetEnds.GetEnumerator())
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
					this.m_functionMappingTranslators.Add(storageEntitySetMapping.Set, null);
				}
				foreach (StorageSetMapping storageSetMapping2 in mapping.RelationshipSetMaps)
				{
					StorageAssociationSetMapping storageAssociationSetMapping = (StorageAssociationSetMapping)storageSetMapping2;
					if (storageAssociationSetMapping.ModificationFunctionMapping != null)
					{
						AssociationSet key = (AssociationSet)storageAssociationSetMapping.Set;
						this.m_functionMappingTranslators.Add(key, ModificationFunctionMappingTranslator.CreateAssociationSetTranslator(storageAssociationSetMapping));
						keyToListMap.AddRange(key, Enumerable.Empty<AssociationEndMember>());
					}
					else if (!this.m_functionMappingTranslators.ContainsKey(storageAssociationSetMapping.Set))
					{
						this.m_functionMappingTranslators.Add(storageAssociationSetMapping.Set, null);
					}
				}
			}
			foreach (AssociationSet key2 in keyToListMap.Keys)
			{
				this.m_associationSetMetadata.Add(key2, new AssociationSetMetadata(keyToListMap.EnumerateValues(key2)));
			}
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000A7494 File Offset: 0x000A5694
		private static IEnumerable<EdmMember> FindServerGenMembers(StorageMappingFragment mappingFragment)
		{
			foreach (StorageScalarPropertyMapping storageScalarPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<StorageScalarPropertyMapping>())
			{
				if (MetadataHelper.GetStoreGeneratedPattern(storageScalarPropertyMapping.ColumnProperty) != StoreGeneratedPattern.None)
				{
					yield return storageScalarPropertyMapping.EdmProperty;
				}
			}
			IEnumerator<StorageScalarPropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000A74A4 File Offset: 0x000A56A4
		private static IEnumerable<EdmMember> FindIsNullConditionColumns(StorageMappingFragment mappingFragment)
		{
			foreach (StorageConditionPropertyMapping storageConditionPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<StorageConditionPropertyMapping>())
			{
				if (storageConditionPropertyMapping.ColumnProperty != null && storageConditionPropertyMapping.IsNull != null)
				{
					yield return storageConditionPropertyMapping.ColumnProperty;
				}
			}
			IEnumerator<StorageConditionPropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000A74B4 File Offset: 0x000A56B4
		private static IEnumerable<EdmMember> FindPropertiesMappedToColumns(Set<EdmMember> columns, StorageMappingFragment mappingFragment)
		{
			foreach (StorageScalarPropertyMapping storageScalarPropertyMapping in ViewLoader.FlattenPropertyMappings(mappingFragment.AllProperties).OfType<StorageScalarPropertyMapping>())
			{
				if (columns.Contains(storageScalarPropertyMapping.ColumnProperty))
				{
					yield return storageScalarPropertyMapping.EdmProperty;
				}
			}
			IEnumerator<StorageScalarPropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000A74CB File Offset: 0x000A56CB
		private static IEnumerable<StorageMappingFragment> GetMappingFragments(StorageSetMapping setMapping)
		{
			foreach (StorageTypeMapping storageTypeMapping in setMapping.TypeMappings)
			{
				foreach (StorageMappingFragment storageMappingFragment in storageTypeMapping.MappingFragments)
				{
					yield return storageMappingFragment;
				}
				IEnumerator<StorageMappingFragment> enumerator2 = null;
			}
			IEnumerator<StorageTypeMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000A74DB File Offset: 0x000A56DB
		private static IEnumerable<StoragePropertyMapping> FlattenPropertyMappings(ReadOnlyCollection<StoragePropertyMapping> propertyMappings)
		{
			foreach (StoragePropertyMapping storagePropertyMapping in propertyMappings)
			{
				StorageComplexPropertyMapping storageComplexPropertyMapping = storagePropertyMapping as StorageComplexPropertyMapping;
				if (storageComplexPropertyMapping != null)
				{
					foreach (StorageComplexTypeMapping storageComplexTypeMapping in storageComplexPropertyMapping.TypeMappings)
					{
						foreach (StoragePropertyMapping storagePropertyMapping2 in ViewLoader.FlattenPropertyMappings(storageComplexTypeMapping.AllProperties))
						{
							yield return storagePropertyMapping2;
						}
						IEnumerator<StoragePropertyMapping> enumerator3 = null;
					}
					IEnumerator<StorageComplexTypeMapping> enumerator2 = null;
				}
				else
				{
					yield return storagePropertyMapping;
				}
			}
			IEnumerator<StoragePropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040012EB RID: 4843
		private readonly StorageMappingItemCollection m_mappingCollection;

		// Token: 0x040012EC RID: 4844
		private readonly Dictionary<AssociationSet, AssociationSetMetadata> m_associationSetMetadata = new Dictionary<AssociationSet, AssociationSetMetadata>();

		// Token: 0x040012ED RID: 4845
		private readonly Dictionary<EntitySetBase, Set<EntitySet>> m_affectedTables = new Dictionary<EntitySetBase, Set<EntitySet>>();

		// Token: 0x040012EE RID: 4846
		private readonly Set<EdmMember> m_serverGenProperties = new Set<EdmMember>();

		// Token: 0x040012EF RID: 4847
		private readonly Set<EdmMember> m_isNullConditionProperties = new Set<EdmMember>();

		// Token: 0x040012F0 RID: 4848
		private readonly Dictionary<EntitySetBase, ModificationFunctionMappingTranslator> m_functionMappingTranslators = new Dictionary<EntitySetBase, ModificationFunctionMappingTranslator>(EqualityComparer<EntitySetBase>.Default);

		// Token: 0x040012F1 RID: 4849
		private readonly ReaderWriterLockSlim m_readerWriterLock = new ReaderWriterLockSlim();
	}
}
