using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006FA RID: 1786
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class EdmModelDiffer
	{
		// Token: 0x06004773 RID: 18291 RVA: 0x00153810 File Offset: 0x00151A10
		public ICollection<MigrationOperation> Diff(XDocument sourceModel, XDocument targetModel, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator = null, MigrationSqlGenerator migrationSqlGenerator = null, string sourceModelVersion = null, string targetModelVersion = null)
		{
			if (sourceModel == targetModel || XNode.DeepEquals(sourceModel, targetModel))
			{
				return new MigrationOperation[0];
			}
			DbProviderInfo providerInfo;
			StorageMappingItemCollection storageMappingItemCollection = sourceModel.GetStorageMappingItemCollection(out providerInfo);
			EdmModelDiffer.ModelMetadata source = new EdmModelDiffer.ModelMetadata
			{
				EdmItemCollection = storageMappingItemCollection.EdmItemCollection,
				StoreItemCollection = storageMappingItemCollection.StoreItemCollection,
				StoreEntityContainer = storageMappingItemCollection.StoreItemCollection.GetItems<EntityContainer>().Single<EntityContainer>(),
				EntityContainerMapping = storageMappingItemCollection.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(),
				ProviderManifest = EdmModelDiffer.GetProviderManifest(providerInfo),
				ProviderInfo = providerInfo
			};
			storageMappingItemCollection = targetModel.GetStorageMappingItemCollection(out providerInfo);
			EdmModelDiffer.ModelMetadata target = new EdmModelDiffer.ModelMetadata
			{
				EdmItemCollection = storageMappingItemCollection.EdmItemCollection,
				StoreItemCollection = storageMappingItemCollection.StoreItemCollection,
				StoreEntityContainer = storageMappingItemCollection.StoreItemCollection.GetItems<EntityContainer>().Single<EntityContainer>(),
				EntityContainerMapping = storageMappingItemCollection.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(),
				ProviderManifest = EdmModelDiffer.GetProviderManifest(providerInfo),
				ProviderInfo = providerInfo
			};
			return this.Diff(source, target, modificationCommandTreeGenerator, migrationSqlGenerator, sourceModelVersion, targetModelVersion);
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x00153918 File Offset: 0x00151B18
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private ICollection<MigrationOperation> Diff(EdmModelDiffer.ModelMetadata source, EdmModelDiffer.ModelMetadata target, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator, string sourceModelVersion = null, string targetModelVersion = null)
		{
			this._source = source;
			this._target = target;
			List<Tuple<EntityType, EntityType>> entityTypePairs = this.FindEntityTypePairs().ToList<Tuple<EntityType, EntityType>>();
			List<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs = this.FindMappingFragmentPairs(entityTypePairs).ToList<Tuple<MappingFragment, MappingFragment>>();
			List<Tuple<AssociationType, AssociationType>> list = this.FindAssociationTypePairs(entityTypePairs).ToList<Tuple<AssociationType, AssociationType>>();
			List<Tuple<EntitySet, EntitySet>> tablePairs = this.FindTablePairs(mappingFragmentPairs, list).ToList<Tuple<EntitySet, EntitySet>>();
			list.AddRange(this.FindStoreOnlyAssociationTypePairs(list, tablePairs));
			List<RenameTableOperation> renameTableOperations = EdmModelDiffer.FindRenamedTables(tablePairs).ToList<RenameTableOperation>();
			List<RenameColumnOperation> list2 = this.FindRenamedColumns(mappingFragmentPairs, list).ToList<RenameColumnOperation>();
			List<AddColumnOperation> second = this.FindAddedColumns(tablePairs, list2).ToList<AddColumnOperation>();
			List<DropColumnOperation> second2 = this.FindDroppedColumns(tablePairs, list2).ToList<DropColumnOperation>();
			List<AlterColumnOperation> list3 = this.FindAlteredColumns(tablePairs, list2).ToList<AlterColumnOperation>();
			List<DropColumnOperation> second3 = this.FindOrphanedColumns(tablePairs, list2).ToList<DropColumnOperation>();
			List<MoveTableOperation> second4 = this.FindMovedTables(tablePairs).ToList<MoveTableOperation>();
			List<CreateTableOperation> second5 = this.FindAddedTables(tablePairs).ToList<CreateTableOperation>();
			List<DropTableOperation> second6 = this.FindDroppedTables(tablePairs).ToList<DropTableOperation>();
			List<AlterTableOperation> second7 = this.FindAlteredTables(tablePairs).ToList<AlterTableOperation>();
			List<MigrationOperation> source2 = this.FindAlteredPrimaryKeys(tablePairs, list2, list3).ToList<MigrationOperation>();
			List<AddForeignKeyOperation> source3 = this.FindAddedForeignKeys(list, list2).Concat(source2.OfType<AddForeignKeyOperation>()).ToList<AddForeignKeyOperation>();
			List<DropForeignKeyOperation> source4 = this.FindDroppedForeignKeys(list, list2).Concat(source2.OfType<DropForeignKeyOperation>()).ToList<DropForeignKeyOperation>();
			List<CreateProcedureOperation> second8 = this.FindAddedModificationFunctions(modificationCommandTreeGenerator, migrationSqlGenerator).ToList<CreateProcedureOperation>();
			List<AlterProcedureOperation> second9 = this.FindAlteredModificationFunctions(modificationCommandTreeGenerator, migrationSqlGenerator).ToList<AlterProcedureOperation>();
			List<DropProcedureOperation> second10 = this.FindDroppedModificationFunctions().ToList<DropProcedureOperation>();
			List<RenameProcedureOperation> second11 = this.FindRenamedModificationFunctions().ToList<RenameProcedureOperation>();
			List<MoveProcedureOperation> second12 = this.FindMovedModificationFunctions().ToList<MoveProcedureOperation>();
			List<ConsolidatedIndex> sourceIndexes = ((string.IsNullOrWhiteSpace(sourceModelVersion) || string.Compare(sourceModelVersion.Substring(0, 3), "6.1", StringComparison.Ordinal) >= 0) ? this.FindSourceIndexes(tablePairs) : EdmModelDiffer.BuildLegacyIndexes(source)).ToList<ConsolidatedIndex>();
			List<ConsolidatedIndex> targetIndexes = ((string.IsNullOrWhiteSpace(targetModelVersion) || string.Compare(targetModelVersion.Substring(0, 3), "6.1", StringComparison.Ordinal) >= 0) ? this.FindTargetIndexes() : EdmModelDiffer.BuildLegacyIndexes(target)).ToList<ConsolidatedIndex>();
			List<CreateIndexOperation> list4 = EdmModelDiffer.FindAddedIndexes(sourceIndexes, targetIndexes, list3, list2).ToList<CreateIndexOperation>();
			List<DropIndexOperation> list5 = EdmModelDiffer.FindDroppedIndexes(sourceIndexes, targetIndexes, list3, list2).ToList<DropIndexOperation>();
			List<RenameIndexOperation> renameIndexOperations = EdmModelDiffer.FindRenamedIndexes(list4, list5, list3, list2).ToList<RenameIndexOperation>();
			return EdmModelDiffer.HandleTransitiveRenameDependencies(renameTableOperations).Concat(second4).Concat(source4.Distinct(EdmModelDiffer._foreignKeyEqualityComparer)).Concat(list5.Distinct(EdmModelDiffer._indexEqualityComparer)).Concat(second3).Concat(EdmModelDiffer.HandleTransitiveRenameDependencies(list2)).Concat(EdmModelDiffer.HandleTransitiveRenameDependencies(renameIndexOperations)).Concat(source2.OfType<DropPrimaryKeyOperation>()).Concat(second5).Concat(second7).Concat(second).Concat(list3).Concat(source2.OfType<AddPrimaryKeyOperation>()).Concat(list4.Distinct(EdmModelDiffer._indexEqualityComparer)).Concat(source3.Distinct(EdmModelDiffer._foreignKeyEqualityComparer)).Concat(second2).Concat(second6).Concat(second8).Concat(second12).Concat(second11).Concat(second9).Concat(second10).ToList<MigrationOperation>();
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x00153F4C File Offset: 0x0015214C
		private static IEnumerable<ConsolidatedIndex> BuildLegacyIndexes(EdmModelDiffer.ModelMetadata modelMetadata)
		{
			using (IEnumerator<AssociationType> enumerator = modelMetadata.StoreItemCollection.GetItems<AssociationType>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AssociationType associationType = enumerator.Current;
					IEnumerable<string> dependentColumnNames = from p in associationType.Constraint.ToProperties
					select p.Name;
					string indexName = IndexOperation.BuildDefaultName(dependentColumnNames);
					string tableName = EdmModelDiffer.GetSchemaQualifiedName(modelMetadata.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == associationType.Constraint.DependentEnd.GetEntityType()));
					ReadOnlyMetadataCollection<EdmProperty> dependentColumns = associationType.Constraint.ToProperties;
					ConsolidatedIndex consolidatedIndex;
					if (dependentColumns.Count > 0)
					{
						consolidatedIndex = new ConsolidatedIndex(tableName, dependentColumns[0].Name, new IndexAttribute(indexName, 0));
						for (int i = 1; i < dependentColumns.Count; i++)
						{
							consolidatedIndex.Add(dependentColumns[i].Name, new IndexAttribute(indexName, i));
						}
					}
					else
					{
						consolidatedIndex = new ConsolidatedIndex(tableName, new IndexAttribute(indexName));
					}
					yield return consolidatedIndex;
				}
			}
			yield break;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x0015411C File Offset: 0x0015231C
		private IEnumerable<Tuple<EntityType, EntityType>> FindEntityTypePairs()
		{
			List<Tuple<EntityType, EntityType>> list = (from et1 in this._source.EdmItemCollection.GetItems<EntityType>()
			from et2 in this._target.EdmItemCollection.GetItems<EntityType>()
			where et1.Name.EqualsOrdinal(et2.Name)
			select Tuple.Create<EntityType, EntityType>(et1, et2)).ToList<Tuple<EntityType, EntityType>>();
			List<EntityType> second = (from t in list
			select t.Item1).ToList<EntityType>();
			List<EntityType> source = this._source.EdmItemCollection.GetItems<EntityType>().Except(second).ToList<EntityType>();
			List<EntityType> second2 = (from t in list
			select t.Item2).ToList<EntityType>();
			List<EntityType> targetRemainingEntities = this._target.EdmItemCollection.GetItems<EntityType>().Except(second2).ToList<EntityType>();
			return list.Concat(from et1 in source
			from et2 in targetRemainingEntities
			where EdmModelDiffer.FuzzyMatchEntities(et1, et2)
			select Tuple.Create<EntityType, EntityType>(et1, et2));
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x00154420 File Offset: 0x00152620
		private static bool FuzzyMatchEntities(EntityType entityType1, EntityType entityType2)
		{
			if (!entityType1.KeyMembers.SequenceEqual(entityType2.KeyMembers, new DynamicEqualityComparer<EdmMember>((EdmMember m1, EdmMember m2) => m1.EdmEquals(m2))))
			{
				return false;
			}
			if ((entityType1.BaseType != null && entityType2.BaseType == null) || (entityType1.BaseType == null && entityType2.BaseType != null))
			{
				return false;
			}
			int num = (from m1 in entityType1.DeclaredMembers
			from m2 in entityType2.DeclaredMembers
			where m1.EdmEquals(m2)
			select 1).Count<int>();
			return (double)((float)num * 2f / (float)(entityType1.DeclaredMembers.Count + entityType2.DeclaredMembers.Count)) > 0.8;
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x00154A9C File Offset: 0x00152C9C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private IEnumerable<Tuple<MappingFragment, MappingFragment>> FindMappingFragmentPairs(ICollection<Tuple<EntityType, EntityType>> entityTypePairs)
		{
			List<EntityTypeMapping> targetEntityTypeMappings = this._target.EntityContainerMapping.EntitySetMappings.SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).ToList<EntityTypeMapping>();
			using (IEnumerator<EntityTypeMapping> enumerator = this._source.EntityContainerMapping.EntitySetMappings.SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntityTypeMapping etm1 = enumerator.Current;
					using (List<EntityTypeMapping>.Enumerator enumerator2 = targetEntityTypeMappings.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EntityTypeMapping etm2 = enumerator2.Current;
							if (entityTypePairs.Any(delegate(Tuple<EntityType, EntityType> t)
							{
								if (etm1.EntityType != null && etm2.EntityType != null && t.Item1 == etm1.EntityType && t.Item2 == etm2.EntityType)
								{
									return true;
								}
								if (etm1.EntityType == null && etm2.EntityType == null && etm1.IsOfTypes.Contains(t.Item1) && etm2.IsOfTypes.Contains(t.Item2))
								{
									return (from et in etm1.IsOfTypes.Except(new EntityType[]
									{
										t.Item1
									})
									select et.Name).SequenceEqual(from et in etm2.IsOfTypes.Except(new EntityType[]
									{
										t.Item2
									})
									select et.Name);
								}
								return false;
							}))
							{
								foreach (Tuple<MappingFragment, MappingFragment> t2 in etm1.MappingFragments.Zip(etm2.MappingFragments, new Func<MappingFragment, MappingFragment, Tuple<MappingFragment, MappingFragment>>(Tuple.Create<MappingFragment, MappingFragment>)))
								{
									yield return t2;
								}
								break;
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x00154FD0 File Offset: 0x001531D0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private IEnumerable<Tuple<AssociationType, AssociationType>> FindAssociationTypePairs(ICollection<Tuple<EntityType, EntityType>> entityTypePairs)
		{
			List<Tuple<AssociationType, AssociationType>> list = (from ets in entityTypePairs
			from np1 in ets.Item1.NavigationProperties
			from np2 in ets.Item2.NavigationProperties
			where np1.Name.EqualsIgnoreCase(np2.Name)
			from t in this.GetStoreAssociationTypePairs(np1.Association, np2.Association, entityTypePairs)
			select t).Distinct<Tuple<AssociationType, AssociationType>>().ToList<Tuple<AssociationType, AssociationType>>();
			List<AssociationType> source = this._source.StoreItemCollection.GetItems<AssociationType>().Except(from t in list
			select t.Item1).ToList<AssociationType>();
			List<AssociationType> targetRemainingAssociationTypes = this._target.StoreItemCollection.GetItems<AssociationType>().Except(from t in list
			select t.Item2).ToList<AssociationType>();
			return list.Concat(from <>h__TransparentIdentifier50 in (from at1 in source
			from at2 in targetRemainingAssociationTypes
			select new
			{
				at1,
				at2
			}).Where(delegate(<>h__TransparentIdentifier50)
			{
				if (<>h__TransparentIdentifier50.at1.Name.EqualsIgnoreCase(<>h__TransparentIdentifier50.at2.Name))
				{
					return true;
				}
				if (<>h__TransparentIdentifier50.at1.Constraint != null && <>h__TransparentIdentifier50.at2.Constraint != null && <>h__TransparentIdentifier50.at1.Constraint.PrincipalEnd.GetEntityType().EdmEquals(<>h__TransparentIdentifier50.at2.Constraint.PrincipalEnd.GetEntityType()) && <>h__TransparentIdentifier50.at1.Constraint.DependentEnd.GetEntityType().EdmEquals(<>h__TransparentIdentifier50.at2.Constraint.DependentEnd.GetEntityType()))
				{
					return <>h__TransparentIdentifier50.at1.Constraint.ToProperties.SequenceEqual(<>h__TransparentIdentifier50.at2.Constraint.ToProperties, new DynamicEqualityComparer<EdmMember>((EdmMember p1, EdmMember p2) => p1.EdmEquals(p2)));
				}
				return false;
			})
			select Tuple.Create<AssociationType, AssociationType>(<>h__TransparentIdentifier50.at1, <>h__TransparentIdentifier50.at2));
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x00155500 File Offset: 0x00153700
		private IEnumerable<Tuple<AssociationType, AssociationType>> GetStoreAssociationTypePairs(AssociationType conceptualAssociationType1, AssociationType conceptualAssociationType2, ICollection<Tuple<EntityType, EntityType>> entityTypePairs)
		{
			AssociationType associationType;
			AssociationType associationType2;
			if (this._source.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(conceptualAssociationType1.Name), out associationType) && this._target.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(conceptualAssociationType2.Name), out associationType2))
			{
				yield return Tuple.Create<AssociationType, AssociationType>(associationType, associationType2);
			}
			else
			{
				AssociationEndMember sourceEnd1 = conceptualAssociationType1.SourceEnd;
				Tuple<EntityType, EntityType> sourceEndEntityTypePair = entityTypePairs.Single((Tuple<EntityType, EntityType> t) => t.Item1 == sourceEnd1.GetEntityType());
				AssociationEndMember sourceEnd2 = (conceptualAssociationType2.SourceEnd.GetEntityType() == sourceEndEntityTypePair.Item2) ? conceptualAssociationType2.SourceEnd : conceptualAssociationType2.TargetEnd;
				if (this._source.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(sourceEnd1.Name), out associationType) && this._target.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(sourceEnd2.Name), out associationType2))
				{
					yield return Tuple.Create<AssociationType, AssociationType>(associationType, associationType2);
				}
				AssociationEndMember targetEnd = conceptualAssociationType1.GetOtherEnd(sourceEnd1);
				AssociationEndMember targetEnd2 = conceptualAssociationType2.GetOtherEnd(sourceEnd2);
				if (this._source.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(targetEnd.Name), out associationType) && this._target.StoreItemCollection.TryGetItem<AssociationType>(EdmModelDiffer.GetStoreAssociationIdentity(targetEnd2.Name), out associationType2))
				{
					yield return Tuple.Create<AssociationType, AssociationType>(associationType, associationType2);
				}
			}
			yield break;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00155604 File Offset: 0x00153804
		private IEnumerable<Tuple<AssociationType, AssociationType>> FindStoreOnlyAssociationTypePairs(ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs, ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			List<AssociationType> list = this._source.StoreItemCollection.GetItems<AssociationType>().Except(from t in associationTypePairs
			select t.Item1).ToList<AssociationType>();
			List<AssociationType> list2 = this._target.StoreItemCollection.GetItems<AssociationType>().Except(from t in associationTypePairs
			select t.Item2).ToList<AssociationType>();
			List<Tuple<AssociationType, AssociationType>> list3 = new List<Tuple<AssociationType, AssociationType>>();
			while (list.Any<AssociationType>())
			{
				AssociationType associationType1 = list[0];
				for (int i = 0; i < list2.Count; i++)
				{
					AssociationType associationType2 = list2[i];
					if (tablePairs.Any((Tuple<EntitySet, EntitySet> t) => t.Item1.ElementType == associationType1.Constraint.PrincipalEnd.GetEntityType() && t.Item2.ElementType == associationType2.Constraint.PrincipalEnd.GetEntityType()) && tablePairs.Any((Tuple<EntitySet, EntitySet> t) => t.Item1.ElementType == associationType1.Constraint.DependentEnd.GetEntityType() && t.Item2.ElementType == associationType2.Constraint.DependentEnd.GetEntityType()))
					{
						list3.Add(Tuple.Create<AssociationType, AssociationType>(associationType1, associationType2));
						list2.RemoveAt(i);
						break;
					}
				}
				list.RemoveAt(0);
			}
			return list3;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x0015573B File Offset: 0x0015393B
		private static string GetStoreAssociationIdentity(string associationName)
		{
			return "CodeFirstDatabaseSchema." + associationName;
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x00155B94 File Offset: 0x00153D94
		private IEnumerable<Tuple<EntitySet, EntitySet>> FindTablePairs(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs, ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs)
		{
			HashSet<EntitySet> sourceTables = new HashSet<EntitySet>();
			HashSet<EntitySet> targetTables = new HashSet<EntitySet>();
			foreach (Tuple<MappingFragment, MappingFragment> mappingFragmentPair in mappingFragmentPairs)
			{
				EntitySet sourceTable = mappingFragmentPair.Item1.TableSet;
				EntitySet targetTable = mappingFragmentPair.Item2.TableSet;
				if (!sourceTables.Contains(sourceTable) && !targetTables.Contains(targetTable))
				{
					sourceTables.Add(sourceTable);
					targetTables.Add(targetTable);
					yield return Tuple.Create<EntitySet, EntitySet>(sourceTable, targetTable);
				}
			}
			using (IEnumerator<Tuple<AssociationType, AssociationType>> enumerator2 = associationTypePairs.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Tuple<AssociationType, AssociationType> associationTypePair = enumerator2.Current;
					EntitySet sourceTable2 = this._source.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == associationTypePair.Item1.Constraint.DependentEnd.GetEntityType());
					EntitySet targetTable2 = this._target.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == associationTypePair.Item2.Constraint.DependentEnd.GetEntityType());
					if (!sourceTables.Contains(sourceTable2) && !targetTables.Contains(targetTable2))
					{
						sourceTables.Add(sourceTable2);
						targetTables.Add(targetTable2);
						yield return Tuple.Create<EntitySet, EntitySet>(sourceTable2, targetTable2);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x00155C24 File Offset: 0x00153E24
		private static IEnumerable<RenameTableOperation> HandleTransitiveRenameDependencies(IList<RenameTableOperation> renameTableOperations)
		{
			return EdmModelDiffer.HandleTransitiveRenameDependencies<RenameTableOperation>(renameTableOperations, delegate(RenameTableOperation rt1, RenameTableOperation rt2)
			{
				DatabaseName databaseName = DatabaseName.Parse(rt1.Name);
				DatabaseName databaseName2 = DatabaseName.Parse(rt2.Name);
				return databaseName.Name.EqualsIgnoreCase(rt2.NewName) && databaseName.Schema.EqualsIgnoreCase(databaseName2.Schema);
			}, (string t, RenameTableOperation rt) => new RenameTableOperation(t, rt.NewName, null), delegate(RenameTableOperation rt, string t)
			{
				rt.NewName = t;
			});
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x00155CD4 File Offset: 0x00153ED4
		private static IEnumerable<RenameColumnOperation> HandleTransitiveRenameDependencies(IList<RenameColumnOperation> renameColumnOperations)
		{
			return EdmModelDiffer.HandleTransitiveRenameDependencies<RenameColumnOperation>(renameColumnOperations, (RenameColumnOperation rc1, RenameColumnOperation rc2) => rc1.Table.EqualsIgnoreCase(rc2.Table) && rc1.Name.EqualsIgnoreCase(rc2.NewName), (string c, RenameColumnOperation rc) => new RenameColumnOperation(rc.Table, c, rc.NewName, null), delegate(RenameColumnOperation rc, string c)
			{
				rc.NewName = c;
			});
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x00155D84 File Offset: 0x00153F84
		private static IEnumerable<RenameIndexOperation> HandleTransitiveRenameDependencies(IList<RenameIndexOperation> renameIndexOperations)
		{
			return EdmModelDiffer.HandleTransitiveRenameDependencies<RenameIndexOperation>(renameIndexOperations, (RenameIndexOperation ri1, RenameIndexOperation ri2) => ri1.Table.EqualsIgnoreCase(ri2.Table) && ri1.Name.EqualsIgnoreCase(ri2.NewName), (string i, RenameIndexOperation rc) => new RenameIndexOperation(rc.Table, i, rc.NewName, null), delegate(RenameIndexOperation rc, string i)
			{
				rc.NewName = i;
			});
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00156128 File Offset: 0x00154328
		private static IEnumerable<T> HandleTransitiveRenameDependencies<T>(IList<T> renameOperations, Func<T, T, bool> dependencyFinder, Func<string, T, T> renameCreator, Action<T, string> setNewName) where T : class
		{
			int tempCounter = 0;
			List<T> tempRenames = new List<T>();
			for (int i = 0; i < renameOperations.Count; i++)
			{
				T renameOperation = renameOperations[i];
				T dependentRename = renameOperations.Skip(i + 1).SingleOrDefault((T rt) => dependencyFinder(renameOperation, rt));
				if (dependentRename != null)
				{
					object arg = "__mig_tmp__";
					int num;
					tempCounter = (num = tempCounter) + 1;
					string text = arg + num;
					tempRenames.Add(renameCreator(text, renameOperation));
					setNewName(renameOperation, text);
				}
				yield return renameOperation;
			}
			foreach (T renameOperation2 in tempRenames)
			{
				yield return renameOperation2;
			}
			yield break;
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x001566D8 File Offset: 0x001548D8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private IEnumerable<MoveProcedureOperation> FindMovedModificationFunctions()
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
			from mfm1 in esm1.ModificationFunctionMappings
			from esm2 in this._target.EntityContainerMapping.EntitySetMappings
			from mfm2 in esm2.ModificationFunctionMappings
			where mfm1.EntityType.Identity == mfm2.EntityType.Identity
			from o in EdmModelDiffer.DiffModificationFunctionSchemas(mfm1, mfm2)
			select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
			where asm1.ModificationFunctionMapping != null
			from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
			where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
			from o in EdmModelDiffer.DiffModificationFunctionSchemas(asm1.ModificationFunctionMapping, asm2.ModificationFunctionMapping)
			select o);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x00156AF0 File Offset: 0x00154CF0
		private static IEnumerable<MoveProcedureOperation> DiffModificationFunctionSchemas(EntityTypeModificationFunctionMapping sourceModificationFunctionMapping, EntityTypeModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function), targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema, null);
			}
			if (!sourceModificationFunctionMapping.UpdateFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.UpdateFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.UpdateFunctionMapping.Function), targetModificationFunctionMapping.UpdateFunctionMapping.Function.Schema, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function), targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema, null);
			}
			yield break;
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x00156CC0 File Offset: 0x00154EC0
		private static IEnumerable<MoveProcedureOperation> DiffModificationFunctionSchemas(AssociationSetModificationFunctionMapping sourceModificationFunctionMapping, AssociationSetModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function), targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.Schema.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema))
			{
				yield return new MoveProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function), targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema, null);
			}
			yield break;
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x00157008 File Offset: 0x00155208
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "<>h__TransparentIdentifier0")]
		private IEnumerable<CreateProcedureOperation> FindAddedModificationFunctions(Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return (from esm1 in this._target.EntityContainerMapping.EntitySetMappings
			from mfm1 in esm1.ModificationFunctionMappings
			where !(from esm2 in this._source.EntityContainerMapping.EntitySetMappings
			from mfm2 in esm2.ModificationFunctionMappings
			where mfm1.EntityType.Identity == mfm2.EntityType.Identity
			select mfm2).Any<EntityTypeModificationFunctionMapping>()
			from o in this.BuildCreateProcedureOperations(mfm1, modificationCommandTreeGenerator, migrationSqlGenerator)
			select o).Concat(from asm1 in this._target.EntityContainerMapping.AssociationSetMappings
			where asm1.ModificationFunctionMapping != null
			where !(from asm2 in this._source.EntityContainerMapping.AssociationSetMappings
			where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
			select asm2.ModificationFunctionMapping).Any<AssociationSetModificationFunctionMapping>()
			from o in this.BuildCreateProcedureOperations(asm1.ModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator)
			select o);
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x00157484 File Offset: 0x00155684
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private IEnumerable<RenameProcedureOperation> FindRenamedModificationFunctions()
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
			from mfm1 in esm1.ModificationFunctionMappings
			from esm2 in this._target.EntityContainerMapping.EntitySetMappings
			from mfm2 in esm2.ModificationFunctionMappings
			where mfm1.EntityType.Identity == mfm2.EntityType.Identity
			from o in EdmModelDiffer.DiffModificationFunctionNames(mfm1, mfm2)
			select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
			where asm1.ModificationFunctionMapping != null
			from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
			where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
			from o in EdmModelDiffer.DiffModificationFunctionNames(asm1.ModificationFunctionMapping, asm2.ModificationFunctionMapping)
			select o);
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x00157854 File Offset: 0x00155A54
		private static IEnumerable<RenameProcedureOperation> DiffModificationFunctionNames(AssociationSetModificationFunctionMapping sourceModificationFunctionMapping, AssociationSetModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema), targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema), targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, null);
			}
			yield break;
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x00157AF0 File Offset: 0x00155CF0
		private static IEnumerable<RenameProcedureOperation> DiffModificationFunctionNames(EntityTypeModificationFunctionMapping sourceModificationFunctionMapping, EntityTypeModificationFunctionMapping targetModificationFunctionMapping)
		{
			if (!sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.InsertFunctionMapping.Function.Schema), targetModificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, null);
			}
			if (!sourceModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.UpdateFunctionMapping.Function.Schema), targetModificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName, null);
			}
			if (!sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName.EqualsOrdinal(targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName))
			{
				yield return new RenameProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(sourceModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, targetModificationFunctionMapping.DeleteFunctionMapping.Function.Schema), targetModificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, null);
			}
			yield break;
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x00157B14 File Offset: 0x00155D14
		private static string GetSchemaQualifiedName(string table, string schema)
		{
			return new DatabaseName(table, schema).ToString();
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x00157E9C File Offset: 0x0015609C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private IEnumerable<AlterProcedureOperation> FindAlteredModificationFunctions(Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
			from mfm1 in esm1.ModificationFunctionMappings
			from esm2 in this._target.EntityContainerMapping.EntitySetMappings
			from mfm2 in esm2.ModificationFunctionMappings
			where mfm1.EntityType.Identity == mfm2.EntityType.Identity
			from o in this.DiffModificationFunctions(mfm1, mfm2, modificationCommandTreeGenerator, migrationSqlGenerator)
			select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
			where asm1.ModificationFunctionMapping != null
			from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
			where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
			from o in this.DiffModificationFunctions(asm1.ModificationFunctionMapping, asm2.ModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator)
			select o);
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x0015824C File Offset: 0x0015644C
		private IEnumerable<AlterProcedureOperation> DiffModificationFunctions(AssociationSetModificationFunctionMapping sourceModificationFunctionMapping, AssociationSetModificationFunctionMapping targetModificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.InsertFunctionMapping, targetModificationFunctionMapping.InsertFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.DeleteFunctionMapping, targetModificationFunctionMapping.DeleteFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			yield break;
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x001584C4 File Offset: 0x001566C4
		private IEnumerable<AlterProcedureOperation> DiffModificationFunctions(EntityTypeModificationFunctionMapping sourceModificationFunctionMapping, EntityTypeModificationFunctionMapping targetModificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.InsertFunctionMapping, targetModificationFunctionMapping.InsertFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.UpdateFunctionMapping, targetModificationFunctionMapping.UpdateFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.UpdateFunctionMapping.Function, this.GenerateUpdateFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			if (!this.DiffModificationFunction(sourceModificationFunctionMapping.DeleteFunctionMapping, targetModificationFunctionMapping.DeleteFunctionMapping))
			{
				yield return this.BuildAlterProcedureOperation(targetModificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(targetModificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			}
			yield break;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x00158507 File Offset: 0x00156707
		private string GenerateInsertFunctionBody(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbModificationCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateInsert(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.InsertFunctionMapping.Function.FunctionName, null);
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x00158549 File Offset: 0x00156749
		private string GenerateInsertFunctionBody(AssociationSetModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbInsertCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateAssociationInsert(s), modificationCommandTreeGenerator, migrationSqlGenerator, null);
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x0015857C File Offset: 0x0015677C
		private string GenerateUpdateFunctionBody(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbModificationCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateUpdate(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.UpdateFunctionMapping.Function.FunctionName, modificationFunctionMapping.UpdateFunctionMapping.RowsAffectedParameterName);
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x001585D4 File Offset: 0x001567D4
		private string GenerateDeleteFunctionBody(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbModificationCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateDelete(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.DeleteFunctionMapping.Function.FunctionName, modificationFunctionMapping.DeleteFunctionMapping.RowsAffectedParameterName);
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0015862B File Offset: 0x0015682B
		private string GenerateDeleteFunctionBody(AssociationSetModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			return this.GenerateFunctionBody<DbDeleteCommandTree>(modificationFunctionMapping, (ModificationCommandTreeGenerator m, string s) => m.GenerateAssociationDelete(s), modificationCommandTreeGenerator, migrationSqlGenerator, modificationFunctionMapping.DeleteFunctionMapping.RowsAffectedParameterName);
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00158660 File Offset: 0x00156860
		private string GenerateFunctionBody<TCommandTree>(EntityTypeModificationFunctionMapping modificationFunctionMapping, Func<ModificationCommandTreeGenerator, string, IEnumerable<TCommandTree>> treeGenerator, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator, string functionName, string rowsAffectedParameterName) where TCommandTree : DbModificationCommandTree
		{
			TCommandTree[] commandTrees = new TCommandTree[0];
			if (modificationCommandTreeGenerator != null)
			{
				DynamicToFunctionModificationCommandConverter dynamicToFunctionModificationCommandConverter = new DynamicToFunctionModificationCommandConverter(modificationFunctionMapping, this._target.EntityContainerMapping);
				try
				{
					commandTrees = dynamicToFunctionModificationCommandConverter.Convert<TCommandTree>(treeGenerator(modificationCommandTreeGenerator.Value, modificationFunctionMapping.EntityType.Identity)).ToArray<TCommandTree>();
				}
				catch (UpdateException innerException)
				{
					throw new InvalidOperationException(Strings.ErrorGeneratingCommandTree(functionName, modificationFunctionMapping.EntityType.Name), innerException);
				}
			}
			return this.GenerateFunctionBody<TCommandTree>(migrationSqlGenerator, rowsAffectedParameterName, commandTrees);
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x001586E4 File Offset: 0x001568E4
		private string GenerateFunctionBody<TCommandTree>(AssociationSetModificationFunctionMapping modificationFunctionMapping, Func<ModificationCommandTreeGenerator, string, IEnumerable<TCommandTree>> treeGenerator, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator, string rowsAffectedParameterName) where TCommandTree : DbModificationCommandTree
		{
			TCommandTree[] commandTrees = new TCommandTree[0];
			if (modificationCommandTreeGenerator != null)
			{
				DynamicToFunctionModificationCommandConverter dynamicToFunctionModificationCommandConverter = new DynamicToFunctionModificationCommandConverter(modificationFunctionMapping, this._target.EntityContainerMapping);
				commandTrees = dynamicToFunctionModificationCommandConverter.Convert<TCommandTree>(treeGenerator(modificationCommandTreeGenerator.Value, modificationFunctionMapping.AssociationSet.ElementType.Identity)).ToArray<TCommandTree>();
			}
			return this.GenerateFunctionBody<TCommandTree>(migrationSqlGenerator, rowsAffectedParameterName, commandTrees);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x00158740 File Offset: 0x00156940
		private string GenerateFunctionBody<TCommandTree>(MigrationSqlGenerator migrationSqlGenerator, string rowsAffectedParameterName, TCommandTree[] commandTrees) where TCommandTree : DbModificationCommandTree
		{
			if (migrationSqlGenerator == null)
			{
				return null;
			}
			string providerManifestToken = this._target.ProviderInfo.ProviderManifestToken;
			return migrationSqlGenerator.GenerateProcedureBody((ICollection<DbModificationCommandTree>)commandTrees, rowsAffectedParameterName, providerManifestToken);
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x00158774 File Offset: 0x00156974
		private bool DiffModificationFunction(ModificationFunctionMapping functionMapping1, ModificationFunctionMapping functionMapping2)
		{
			if (!functionMapping1.RowsAffectedParameterName.EqualsOrdinal(functionMapping2.RowsAffectedParameterName))
			{
				return false;
			}
			if (!functionMapping1.ParameterBindings.SequenceEqual(functionMapping2.ParameterBindings, new Func<ModificationFunctionParameterBinding, ModificationFunctionParameterBinding, bool>(this.DiffParameterBinding)))
			{
				return false;
			}
			IEnumerable<ModificationFunctionResultBinding> enumerable = Enumerable.Empty<ModificationFunctionResultBinding>();
			return (functionMapping1.ResultBindings ?? enumerable).SequenceEqual(functionMapping2.ResultBindings ?? enumerable, new Func<ModificationFunctionResultBinding, ModificationFunctionResultBinding, bool>(EdmModelDiffer.DiffResultBinding));
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x001587FC File Offset: 0x001569FC
		private bool DiffParameterBinding(ModificationFunctionParameterBinding parameterBinding1, ModificationFunctionParameterBinding parameterBinding2)
		{
			FunctionParameter parameter = parameterBinding1.Parameter;
			FunctionParameter parameter2 = parameterBinding2.Parameter;
			if (!parameter.Name.EqualsOrdinal(parameter2.Name))
			{
				return false;
			}
			if (parameter.Mode != parameter2.Mode)
			{
				return false;
			}
			if (parameterBinding1.IsCurrent != parameterBinding2.IsCurrent)
			{
				return false;
			}
			if (!parameterBinding1.MemberPath.Members.SequenceEqual(parameterBinding2.MemberPath.Members, (EdmMember m1, EdmMember m2) => m1.Identity.EqualsOrdinal(m2.Identity)))
			{
				return false;
			}
			if (this._source.ProviderInfo.Equals(this._target.ProviderInfo))
			{
				return parameter.TypeName.EqualsIgnoreCase(parameter2.TypeName) && parameter.TypeUsage.EdmEquals(parameter2.TypeUsage);
			}
			return parameter.Precision == parameter2.Precision && parameter.Scale == parameter2.Scale;
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00158933 File Offset: 0x00156B33
		private static bool DiffResultBinding(ModificationFunctionResultBinding resultBinding1, ModificationFunctionResultBinding resultBinding2)
		{
			return resultBinding1.ColumnName.EqualsOrdinal(resultBinding2.ColumnName) && resultBinding1.Property.Identity.EqualsOrdinal(resultBinding2.Property.Identity);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00158B30 File Offset: 0x00156D30
		private IEnumerable<CreateProcedureOperation> BuildCreateProcedureOperations(EntityTypeModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.UpdateFunctionMapping.Function, this.GenerateUpdateFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield break;
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00158CD8 File Offset: 0x00156ED8
		private IEnumerable<CreateProcedureOperation> BuildCreateProcedureOperations(AssociationSetModificationFunctionMapping modificationFunctionMapping, Lazy<ModificationCommandTreeGenerator> modificationCommandTreeGenerator, MigrationSqlGenerator migrationSqlGenerator)
		{
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.InsertFunctionMapping.Function, this.GenerateInsertFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield return this.BuildCreateProcedureOperation(modificationFunctionMapping.DeleteFunctionMapping.Function, this.GenerateDeleteFunctionBody(modificationFunctionMapping, modificationCommandTreeGenerator, migrationSqlGenerator));
			yield break;
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00158D38 File Offset: 0x00156F38
		private CreateProcedureOperation BuildCreateProcedureOperation(EdmFunction function, string bodySql)
		{
			CreateProcedureOperation createProcedureOperation = new CreateProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(function), bodySql, null);
			function.Parameters.Each(delegate(FunctionParameter p)
			{
				createProcedureOperation.Parameters.Add(EdmModelDiffer.BuildParameterModel(p, this._target));
			});
			return createProcedureOperation;
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x00158DB0 File Offset: 0x00156FB0
		private AlterProcedureOperation BuildAlterProcedureOperation(EdmFunction function, string bodySql)
		{
			AlterProcedureOperation alterProcedureOperation = new AlterProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(function), bodySql, null);
			function.Parameters.Each(delegate(FunctionParameter p)
			{
				alterProcedureOperation.Parameters.Add(EdmModelDiffer.BuildParameterModel(p, this._target));
			});
			return alterProcedureOperation;
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x00158DFC File Offset: 0x00156FFC
		private static ParameterModel BuildParameterModel(FunctionParameter functionParameter, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			TypeUsage modelTypeUsage = functionParameter.TypeUsage.ModelTypeUsage;
			string name = modelMetadata.ProviderManifest.GetStoreType(modelTypeUsage).EdmType.Name;
			ParameterModel parameterModel = new ParameterModel(((PrimitiveType)modelTypeUsage.EdmType).PrimitiveTypeKind, modelTypeUsage)
			{
				Name = functionParameter.Name,
				IsOutParameter = (functionParameter.Mode == ParameterMode.Out),
				StoreType = ((!functionParameter.TypeName.EqualsIgnoreCase(name)) ? functionParameter.TypeName : null)
			};
			Facet facet;
			if (modelTypeUsage.Facets.TryGetValue("MaxLength", true, out facet) && facet.Value != null)
			{
				parameterModel.MaxLength = (facet.Value as int?);
			}
			if (modelTypeUsage.Facets.TryGetValue("Precision", true, out facet) && facet.Value != null)
			{
				parameterModel.Precision = (byte?)facet.Value;
			}
			if (modelTypeUsage.Facets.TryGetValue("Scale", true, out facet) && facet.Value != null)
			{
				parameterModel.Scale = (byte?)facet.Value;
			}
			if (modelTypeUsage.Facets.TryGetValue("FixedLength", true, out facet) && facet.Value != null && (bool)facet.Value)
			{
				parameterModel.IsFixedLength = new bool?(true);
			}
			if (modelTypeUsage.Facets.TryGetValue("Unicode", true, out facet) && facet.Value != null && !(bool)facet.Value)
			{
				parameterModel.IsUnicode = new bool?(false);
			}
			return parameterModel;
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x00159204 File Offset: 0x00157404
		[SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "<>h__TransparentIdentifier0")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private IEnumerable<DropProcedureOperation> FindDroppedModificationFunctions()
		{
			return (from esm1 in this._source.EntityContainerMapping.EntitySetMappings
			from mfm1 in esm1.ModificationFunctionMappings
			where !(from esm2 in this._target.EntityContainerMapping.EntitySetMappings
			from mfm2 in esm2.ModificationFunctionMappings
			where mfm1.EntityType.Identity == mfm2.EntityType.Identity
			select mfm2).Any<EntityTypeModificationFunctionMapping>()
			from o in new DropProcedureOperation[]
			{
				new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(mfm1.InsertFunctionMapping.Function), null),
				new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(mfm1.UpdateFunctionMapping.Function), null),
				new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(mfm1.DeleteFunctionMapping.Function), null)
			}
			select o).Concat(from asm1 in this._source.EntityContainerMapping.AssociationSetMappings
			where asm1.ModificationFunctionMapping != null
			where !(from asm2 in this._target.EntityContainerMapping.AssociationSetMappings
			where asm2.ModificationFunctionMapping != null && asm1.ModificationFunctionMapping.AssociationSet.Identity == asm2.ModificationFunctionMapping.AssociationSet.Identity
			select asm2.ModificationFunctionMapping).Any<AssociationSetModificationFunctionMapping>()
			from o in new DropProcedureOperation[]
			{
				new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(asm1.ModificationFunctionMapping.InsertFunctionMapping.Function), null),
				new DropProcedureOperation(EdmModelDiffer.GetSchemaQualifiedName(asm1.ModificationFunctionMapping.DeleteFunctionMapping.Function), null)
			}
			select o);
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x00159378 File Offset: 0x00157578
		private static IEnumerable<RenameTableOperation> FindRenamedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from p in tablePairs
			where !p.Item1.Table.EqualsIgnoreCase(p.Item2.Table)
			select new RenameTableOperation(EdmModelDiffer.GetSchemaQualifiedName(p.Item1), p.Item2.Table, null);
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x001593E0 File Offset: 0x001575E0
		private IEnumerable<CreateTableOperation> FindAddedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from es in this._target.StoreEntityContainer.EntitySets.Except(from p in tablePairs
			select p.Item2)
			select EdmModelDiffer.BuildCreateTableOperation(es, this._target);
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x001594AF File Offset: 0x001576AF
		private IEnumerable<MoveTableOperation> FindMovedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from p in tablePairs
			where !p.Item1.Schema.EqualsIgnoreCase(p.Item2.Schema)
			select new MoveTableOperation(new DatabaseName(p.Item2.Table, p.Item1.Schema).ToString(), p.Item2.Schema, null)
			{
				CreateTableOperation = EdmModelDiffer.BuildCreateTableOperation(p.Item2, this._target)
			};
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x001595AC File Offset: 0x001577AC
		private IEnumerable<DropTableOperation> FindDroppedTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from es in this._source.StoreEntityContainer.EntitySets.Except(from p in tablePairs
			select p.Item1)
			select new DropTableOperation(EdmModelDiffer.GetSchemaQualifiedName(es), EdmModelDiffer.GetAnnotations(es.ElementType), (from p in es.ElementType.Properties
			where EdmModelDiffer.GetAnnotations(p).Count > 0
			select p).ToDictionary((EdmProperty p) => p.Name, (EdmProperty p) => EdmModelDiffer.GetAnnotations(p)), EdmModelDiffer.BuildCreateTableOperation(es, this._source), null);
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x00159640 File Offset: 0x00157840
		private IEnumerable<AlterTableOperation> FindAlteredTables(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from p in tablePairs
			where !EdmModelDiffer.GetAnnotations(p.Item1.ElementType).SequenceEqual(EdmModelDiffer.GetAnnotations(p.Item2.ElementType))
			select this.BuildAlterTableAnnotationsOperation(p.Item1, p.Item2);
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x00159710 File Offset: 0x00157910
		private AlterTableOperation BuildAlterTableAnnotationsOperation(EntitySet sourceTable, EntitySet destinationTable)
		{
			AlterTableOperation operation = new AlterTableOperation(EdmModelDiffer.GetSchemaQualifiedName(destinationTable), EdmModelDiffer.BuildAnnotationPairs(EdmModelDiffer.GetAnnotations(sourceTable.ElementType), EdmModelDiffer.GetAnnotations(destinationTable.ElementType)), null);
			destinationTable.ElementType.Properties.Each(delegate(EdmProperty p)
			{
				operation.Columns.Add(EdmModelDiffer.BuildColumnModel(p, this._target, EdmModelDiffer.GetAnnotations(p).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(a.Value, a.Value))));
			});
			return operation;
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x001597C4 File Offset: 0x001579C4
		internal static Dictionary<string, object> GetAnnotations(MetadataItem item)
		{
			return (from a in item.Annotations
			where a.Name.StartsWith("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:", StringComparison.Ordinal) && !a.Name.EndsWith("Index", StringComparison.Ordinal)
			select a).ToDictionary((MetadataProperty a) => a.Name.Substring("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:".Length), (MetadataProperty a) => a.Value);
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00159B84 File Offset: 0x00157D84
		private IEnumerable<MigrationOperation> FindAlteredPrimaryKeys(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns, ICollection<AlterColumnOperation> alteredColumns)
		{
			return from ts in tablePairs
			let t2 = EdmModelDiffer.GetSchemaQualifiedName(ts.Item2)
			where !ts.Item1.ElementType.KeyProperties.SequenceEqual(ts.Item2.ElementType.KeyProperties, (EdmProperty p1, EdmProperty p2) => p1.Name.EqualsIgnoreCase(p2.Name) || renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(t2) && rc.Name.EqualsIgnoreCase(p1.Name) && rc.NewName.EqualsIgnoreCase(p2.Name))) || ts.Item2.ElementType.KeyProperties.Any((EdmProperty p) => alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(t2) && ac.Column.Name.EqualsIgnoreCase(p.Name)))
			from o in this.BuildChangePrimaryKeyOperations(ts)
			select o;
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0015A100 File Offset: 0x00158300
		private IEnumerable<MigrationOperation> BuildChangePrimaryKeyOperations(Tuple<EntitySet, EntitySet> tablePair)
		{
			List<ReferentialConstraint> sourceReferencedForeignKeys = (from at in this._source.StoreItemCollection.GetItems<AssociationType>()
			select at.Constraint into c
			where c.FromProperties.SequenceEqual(tablePair.Item1.ElementType.KeyProperties)
			select c).ToList<ReferentialConstraint>();
			foreach (ReferentialConstraint constraint in sourceReferencedForeignKeys)
			{
				yield return EdmModelDiffer.BuildDropForeignKeyOperation(constraint, this._source);
			}
			DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(null)
			{
				Table = EdmModelDiffer.GetSchemaQualifiedName(tablePair.Item2)
			};
			tablePair.Item1.ElementType.KeyProperties.Each(delegate(EdmProperty pr)
			{
				dropPrimaryKeyOperation.Columns.Add(pr.Name);
			});
			yield return dropPrimaryKeyOperation;
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null)
			{
				Table = EdmModelDiffer.GetSchemaQualifiedName(tablePair.Item2)
			};
			tablePair.Item2.ElementType.KeyProperties.Each(delegate(EdmProperty pr)
			{
				addPrimaryKeyOperation.Columns.Add(pr.Name);
			});
			yield return addPrimaryKeyOperation;
			List<ReferentialConstraint> targetReferencedForeignKeys = (from at in this._target.StoreItemCollection.GetItems<AssociationType>()
			select at.Constraint into c
			where c.FromProperties.SequenceEqual(tablePair.Item2.ElementType.KeyProperties)
			select c).ToList<ReferentialConstraint>();
			foreach (ReferentialConstraint constraint2 in targetReferencedForeignKeys)
			{
				yield return EdmModelDiffer.BuildAddForeignKeyOperation(constraint2, this._target);
			}
			yield break;
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0015A17C File Offset: 0x0015837C
		private IEnumerable<AddForeignKeyOperation> FindAddedForeignKeys(ICollection<Tuple<AssociationType, AssociationType>> assocationTypePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from at in this._target.StoreItemCollection.GetItems<AssociationType>().Except(from p in assocationTypePairs
			select p.Item2).Concat(from at in assocationTypePairs
			where !this.DiffAssociations(at.Item1.Constraint, at.Item2.Constraint, renamedColumns)
			select at.Item2)
			select EdmModelDiffer.BuildAddForeignKeyOperation(at.Constraint, this._target);
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x0015A278 File Offset: 0x00158478
		private IEnumerable<DropForeignKeyOperation> FindDroppedForeignKeys(ICollection<Tuple<AssociationType, AssociationType>> assocationTypePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from at in this._source.StoreItemCollection.GetItems<AssociationType>().Except(from p in assocationTypePairs
			select p.Item1).Concat(from at in assocationTypePairs
			where !this.DiffAssociations(at.Item1.Constraint, at.Item2.Constraint, renamedColumns)
			select at.Item1)
			select EdmModelDiffer.BuildDropForeignKeyOperation(at.Constraint, this._source);
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x0015A400 File Offset: 0x00158600
		private bool DiffAssociations(ReferentialConstraint referentialConstraint1, ReferentialConstraint referentialConstraint2, ICollection<RenameColumnOperation> renamedColumns)
		{
			string targetTable = EdmModelDiffer.GetSchemaQualifiedName(this._target.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == referentialConstraint2.DependentEnd.GetEntityType()));
			return referentialConstraint1.ToProperties.SequenceEqual(referentialConstraint2.ToProperties, (EdmProperty p1, EdmProperty p2) => p1.Name.EqualsIgnoreCase(p2.Name) || renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(targetTable) && rc.Name.EqualsIgnoreCase(p1.Name) && rc.NewName.EqualsIgnoreCase(p2.Name))) && referentialConstraint1.PrincipalEnd.DeleteBehavior == referentialConstraint2.PrincipalEnd.DeleteBehavior;
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0015A4B0 File Offset: 0x001586B0
		private static AddForeignKeyOperation BuildAddForeignKeyOperation(ReferentialConstraint referentialConstraint, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			AddForeignKeyOperation addForeignKeyOperation = new AddForeignKeyOperation(null);
			EdmModelDiffer.BuildForeignKeyOperation(referentialConstraint, addForeignKeyOperation, modelMetadata);
			referentialConstraint.FromProperties.Each(delegate(EdmProperty pr)
			{
				addForeignKeyOperation.PrincipalColumns.Add(pr.Name);
			});
			addForeignKeyOperation.CascadeDelete = (referentialConstraint.PrincipalEnd.DeleteBehavior == OperationAction.Cascade);
			return addForeignKeyOperation;
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x0015A514 File Offset: 0x00158714
		private static DropForeignKeyOperation BuildDropForeignKeyOperation(ReferentialConstraint referentialConstraint, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			DropForeignKeyOperation dropForeignKeyOperation = new DropForeignKeyOperation(EdmModelDiffer.BuildAddForeignKeyOperation(referentialConstraint, modelMetadata), null);
			EdmModelDiffer.BuildForeignKeyOperation(referentialConstraint, dropForeignKeyOperation, modelMetadata);
			return dropForeignKeyOperation;
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x0015A58C File Offset: 0x0015878C
		private static void BuildForeignKeyOperation(ReferentialConstraint referentialConstraint, ForeignKeyOperation foreignKeyOperation, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			foreignKeyOperation.PrincipalTable = EdmModelDiffer.GetSchemaQualifiedName(modelMetadata.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == referentialConstraint.PrincipalEnd.GetEntityType()));
			foreignKeyOperation.DependentTable = EdmModelDiffer.GetSchemaQualifiedName(modelMetadata.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == referentialConstraint.DependentEnd.GetEntityType()));
			referentialConstraint.ToProperties.Each(delegate(EdmProperty pr)
			{
				foreignKeyOperation.DependentColumns.Add(pr.Name);
			});
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x0015A9F8 File Offset: 0x00158BF8
		private IEnumerable<AddColumnOperation> FindAddedColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
			let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
			from c in p.Item2.ElementType.Properties.Except(p.Item1.ElementType.Properties, (EdmProperty c1, EdmProperty c2) => c1.Name.EqualsIgnoreCase(c2.Name))
			where !renamedColumns.Any((RenameColumnOperation cr) => cr.Table.EqualsIgnoreCase(t) && cr.NewName.EqualsIgnoreCase(c.Name))
			select new AddColumnOperation(t, EdmModelDiffer.BuildColumnModel(c, this._target, EdmModelDiffer.GetAnnotations(c).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))), null);
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x0015AD80 File Offset: 0x00158F80
		private IEnumerable<DropColumnOperation> FindDroppedColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
			let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
			from c in p.Item1.ElementType.Properties.Except(p.Item2.ElementType.Properties, (EdmProperty c1, EdmProperty c2) => c1.Name.EqualsIgnoreCase(c2.Name))
			where !renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(t) && rc.Name.EqualsIgnoreCase(c.Name))
			select new DropColumnOperation(t, c.Name, EdmModelDiffer.GetAnnotations(c), new AddColumnOperation(t, EdmModelDiffer.BuildColumnModel(c, this._source, EdmModelDiffer.GetAnnotations(c).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))), null), null);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x0015B260 File Offset: 0x00159460
		private IEnumerable<DropColumnOperation> FindOrphanedColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
			let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
			from rc1 in renamedColumns
			where rc1.Table.EqualsIgnoreCase(t)
			from c in p.Item1.ElementType.Properties
			where c.Name.EqualsIgnoreCase(rc1.NewName) && !renamedColumns.Any((RenameColumnOperation rc2) => rc2 != rc1 && rc2.Table.EqualsIgnoreCase(rc1.Table) && rc2.Name.EqualsIgnoreCase(rc1.NewName))
			select new DropColumnOperation(t, c.Name, EdmModelDiffer.GetAnnotations(c), new AddColumnOperation(t, EdmModelDiffer.BuildColumnModel(c, this._source, EdmModelDiffer.GetAnnotations(c).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))), null), null);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x0015B774 File Offset: 0x00159974
		private IEnumerable<AlterColumnOperation> FindAlteredColumns(ICollection<Tuple<EntitySet, EntitySet>> tablePairs, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from p in tablePairs
			let t = EdmModelDiffer.GetSchemaQualifiedName(p.Item2)
			from p1 in p.Item1.ElementType.Properties
			let p2 = p.Item2.ElementType.Properties.SingleOrDefault((EdmProperty c) => (p1.Name.EqualsIgnoreCase(c.Name) || renamedColumns.Any((RenameColumnOperation rc) => rc.Table.EqualsIgnoreCase(t) && rc.Name.EqualsIgnoreCase(p1.Name) && rc.NewName.EqualsIgnoreCase(c.Name))) && !this.DiffColumns(p1, c))
			where p2 != null
			select this.BuildAlterColumnOperation(t, p2, this._target, p1, this._source);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0015BB60 File Offset: 0x00159D60
		private IEnumerable<ConsolidatedIndex> FindSourceIndexes(ICollection<Tuple<EntitySet, EntitySet>> tablePairs)
		{
			return from es in this._source.StoreEntityContainer.EntitySets
			let p = tablePairs.SingleOrDefault((Tuple<EntitySet, EntitySet> p) => p.Item1 == es)
			let t = EdmModelDiffer.GetSchemaQualifiedName((p != null) ? p.Item2 : es)
			from i in ConsolidatedIndex.BuildIndexes(t, from c in es.ElementType.Properties
			select Tuple.Create<string, EdmProperty>(c.Name, c))
			select i;
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x0015BC48 File Offset: 0x00159E48
		private IEnumerable<ConsolidatedIndex> FindTargetIndexes()
		{
			return from es in this._target.StoreEntityContainer.EntitySets
			from i in ConsolidatedIndex.BuildIndexes(EdmModelDiffer.GetSchemaQualifiedName(es), from p in es.ElementType.Properties
			select Tuple.Create<string, EdmProperty>(p.Name, p))
			select i;
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x0015BD4C File Offset: 0x00159F4C
		private static IEnumerable<CreateIndexOperation> FindAddedIndexes(ICollection<ConsolidatedIndex> sourceIndexes, ICollection<ConsolidatedIndex> targetIndexes, ICollection<AlterColumnOperation> alteredColumns, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from i in targetIndexes.Except(sourceIndexes, (ConsolidatedIndex i1, ConsolidatedIndex i2) => EdmModelDiffer.IndexesEqual(i1, i2, renamedColumns) && !alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(i2.Table) && i2.Columns.Contains(ac.Column.Name, StringComparer.OrdinalIgnoreCase)))
			select i.CreateCreateIndexOperation();
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x0015BE4C File Offset: 0x0015A04C
		private static IEnumerable<DropIndexOperation> FindDroppedIndexes(ICollection<ConsolidatedIndex> sourceIndexes, ICollection<ConsolidatedIndex> targetIndexes, ICollection<AlterColumnOperation> alteredColumns, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from i in sourceIndexes.Except(targetIndexes, (ConsolidatedIndex i2, ConsolidatedIndex i1) => EdmModelDiffer.IndexesEqual(i1, i2, renamedColumns) && !alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(i2.Table) && i2.Columns.Contains(ac.Column.Name, StringComparer.OrdinalIgnoreCase)))
			select i.CreateDropIndexOperation();
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x0015BF58 File Offset: 0x0015A158
		private static bool IndexesEqual(ConsolidatedIndex consolidatedIndex1, ConsolidatedIndex consolidatedIndex2, ICollection<RenameColumnOperation> renamedColumns)
		{
			return consolidatedIndex1.Table.EqualsIgnoreCase(consolidatedIndex2.Table) && consolidatedIndex1.Index.Equals(consolidatedIndex2.Index) && (from c in consolidatedIndex1.Columns
			select (from rc in renamedColumns
			where rc.Table.EqualsIgnoreCase(consolidatedIndex1.Table) && rc.Name.EqualsIgnoreCase(c)
			select rc.NewName).SingleOrDefault<string>() ?? c).SequenceEqual(consolidatedIndex2.Columns, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x0015C508 File Offset: 0x0015A708
		private static IEnumerable<RenameIndexOperation> FindRenamedIndexes(ICollection<CreateIndexOperation> addedIndexes, ICollection<DropIndexOperation> droppedIndexes, ICollection<AlterColumnOperation> alteredColumns, ICollection<RenameColumnOperation> renamedColumns)
		{
			return from ci1 in addedIndexes.ToList<CreateIndexOperation>()
			from di in droppedIndexes.ToList<DropIndexOperation>()
			let ci2 = (CreateIndexOperation)di.Inverse
			where ci1.Table.EqualsIgnoreCase(ci2.Table) && !ci1.Name.EqualsIgnoreCase(ci2.Name) && ci1.Columns.SequenceEqual(from c in ci2.Columns
			select (from rc in renamedColumns
			where rc.Table.EqualsIgnoreCase(ci2.Table) && rc.Name.EqualsIgnoreCase(c)
			select rc.NewName).SingleOrDefault<string>() ?? c, StringComparer.OrdinalIgnoreCase) && ci1.IsClustered == ci2.IsClustered && ci1.IsUnique == ci2.IsUnique && (!alteredColumns.Any((AlterColumnOperation ac) => ac.Table.EqualsIgnoreCase(ci1.Table) && ci1.Columns.Contains(ac.Column.Name, StringComparer.OrdinalIgnoreCase)) && addedIndexes.Remove(ci1)) && droppedIndexes.Remove(di)
			select new RenameIndexOperation(ci1.Table, di.Name, ci1.Name, null);
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x0015C5D8 File Offset: 0x0015A7D8
		private bool DiffColumns(EdmProperty column1, EdmProperty column2)
		{
			if (column1.Nullable != column2.Nullable)
			{
				return false;
			}
			if (column1.PrimitiveType.PrimitiveTypeKind != column2.PrimitiveType.PrimitiveTypeKind)
			{
				return false;
			}
			if (column1.StoreGeneratedPattern != column2.StoreGeneratedPattern)
			{
				return false;
			}
			if (!(from a in EdmModelDiffer.GetAnnotations(column1)
			orderby a.Key
			select a).SequenceEqual(from a in EdmModelDiffer.GetAnnotations(column2)
			orderby a.Key
			select a))
			{
				return false;
			}
			if (this._source.ProviderInfo.Equals(this._target.ProviderInfo))
			{
				return column1.TypeName.EqualsIgnoreCase(column2.TypeName) && column1.TypeUsage.EdmEquals(column2.TypeUsage);
			}
			return column1.Precision == column2.Precision && column1.Scale == column2.Scale && column1.IsUnicode == column2.IsUnicode && column1.IsFixedLength == column2.IsFixedLength;
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x0015C7B8 File Offset: 0x0015A9B8
		private AlterColumnOperation BuildAlterColumnOperation(string table, EdmProperty targetProperty, EdmModelDiffer.ModelMetadata targetModelMetadata, EdmProperty sourceProperty, EdmModelDiffer.ModelMetadata sourceModelMetadata)
		{
			IDictionary<string, AnnotationValues> dictionary = EdmModelDiffer.BuildAnnotationPairs(EdmModelDiffer.GetAnnotations(sourceProperty), EdmModelDiffer.GetAnnotations(targetProperty));
			Dictionary<string, AnnotationValues> annotations = dictionary.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => new AnnotationValues(a.Value.NewValue, a.Value.OldValue));
			ColumnModel columnModel = EdmModelDiffer.BuildColumnModel(targetProperty, targetModelMetadata, dictionary);
			ColumnModel columnModel2 = EdmModelDiffer.BuildColumnModel(sourceProperty, sourceModelMetadata, annotations);
			columnModel2.Name = columnModel.Name;
			return new AlterColumnOperation(table, columnModel, columnModel.IsNarrowerThan(columnModel2, this._target.ProviderManifest), new AlterColumnOperation(table, columnModel2, columnModel2.IsNarrowerThan(columnModel, this._target.ProviderManifest), null), null);
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x0015C870 File Offset: 0x0015AA70
		private static IDictionary<string, AnnotationValues> BuildAnnotationPairs(IDictionary<string, object> rawSourceAnnotations, IDictionary<string, object> rawTargetAnnotations)
		{
			Dictionary<string, AnnotationValues> dictionary = new Dictionary<string, AnnotationValues>();
			IEnumerable<string> enumerable = rawTargetAnnotations.Keys.Concat(rawSourceAnnotations.Keys).Distinct<string>();
			foreach (string key in enumerable)
			{
				if (!rawSourceAnnotations.ContainsKey(key))
				{
					dictionary[key] = new AnnotationValues(null, rawTargetAnnotations[key]);
				}
				else if (!rawTargetAnnotations.ContainsKey(key))
				{
					dictionary[key] = new AnnotationValues(rawSourceAnnotations[key], null);
				}
				else if (!object.Equals(rawSourceAnnotations[key], rawTargetAnnotations[key]))
				{
					dictionary[key] = new AnnotationValues(rawSourceAnnotations[key], rawTargetAnnotations[key]);
				}
			}
			return dictionary;
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x0015C978 File Offset: 0x0015AB78
		private IEnumerable<RenameColumnOperation> FindRenamedColumns(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs, ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs)
		{
			return EdmModelDiffer.FindRenamedMappedColumns(mappingFragmentPairs).Concat(this.FindRenamedForeignKeyColumns(associationTypePairs)).Concat(EdmModelDiffer.FindRenamedDiscriminatorColumns(mappingFragmentPairs)).Distinct(new DynamicEqualityComparer<RenameColumnOperation>((RenameColumnOperation c1, RenameColumnOperation c2) => c1.Table.EqualsIgnoreCase(c2.Table) && c1.Name.EqualsIgnoreCase(c2.Name) && c1.NewName.EqualsIgnoreCase(c2.NewName)));
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x0015CB20 File Offset: 0x0015AD20
		private static IEnumerable<RenameColumnOperation> FindRenamedMappedColumns(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs)
		{
			return from mfs in mappingFragmentPairs
			let t = EdmModelDiffer.GetSchemaQualifiedName(mfs.Item2.StoreEntitySet)
			from cr in EdmModelDiffer.FindRenamedMappedColumns(mfs.Item1, mfs.Item2, t)
			select cr;
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x0015CD78 File Offset: 0x0015AF78
		private static IEnumerable<RenameColumnOperation> FindRenamedMappedColumns(MappingFragment mappingFragment1, MappingFragment mappingFragment2, string table)
		{
			return from cmb1 in mappingFragment1.FlattenedProperties
			from cmb2 in mappingFragment2.FlattenedProperties
			where cmb1.PropertyPath.SequenceEqual(cmb2.PropertyPath, new DynamicEqualityComparer<EdmProperty>((EdmProperty p1, EdmProperty p2) => p1.EdmEquals(p2))) && !cmb1.ColumnProperty.Name.EqualsIgnoreCase(cmb2.ColumnProperty.Name)
			select new RenameColumnOperation(table, cmb1.ColumnProperty.Name, cmb2.ColumnProperty.Name, null);
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x0015D368 File Offset: 0x0015B568
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private IEnumerable<RenameColumnOperation> FindRenamedForeignKeyColumns(ICollection<Tuple<AssociationType, AssociationType>> associationTypePairs)
		{
			return from ats in associationTypePairs
			let rc1 = ats.Item1.Constraint
			let rc2 = ats.Item2.Constraint
			from ps in rc1.ToProperties.Zip(rc2.ToProperties)
			where !ps.Key.Name.EqualsIgnoreCase(ps.Value.Name) && (!rc2.DependentEnd.GetEntityType().Properties.Any((EdmProperty p) => p.Name.EqualsIgnoreCase(ps.Key.Name)) || rc1.DependentEnd.GetEntityType().Properties.Any((EdmProperty p) => p.Name.EqualsIgnoreCase(ps.Value.Name)))
			select new RenameColumnOperation(EdmModelDiffer.GetSchemaQualifiedName(this._target.StoreEntityContainer.EntitySets.Single((EntitySet es) => es.ElementType == rc2.DependentEnd.GetEntityType())), ps.Key.Name, ps.Value.Name, null);
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x0015D46C File Offset: 0x0015B66C
		private static IEnumerable<RenameColumnOperation> FindRenamedDiscriminatorColumns(ICollection<Tuple<MappingFragment, MappingFragment>> mappingFragmentPairs)
		{
			return from mfs in mappingFragmentPairs
			let t = EdmModelDiffer.GetSchemaQualifiedName(mfs.Item2.StoreEntitySet)
			from cr in EdmModelDiffer.FindRenamedDiscriminatorColumns(mfs.Item1, mfs.Item2, t)
			select cr;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x0015D68C File Offset: 0x0015B88C
		private static IEnumerable<RenameColumnOperation> FindRenamedDiscriminatorColumns(MappingFragment mappingFragment1, MappingFragment mappingFragment2, string table)
		{
			return from c1 in mappingFragment1.Conditions
			from c2 in mappingFragment2.Conditions
			where object.Equals(c1.Value, c2.Value)
			where !c1.Column.Name.EqualsIgnoreCase(c2.Column.Name)
			select new RenameColumnOperation(table, c1.Column.Name, c2.Column.Name, null);
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x0015D7E0 File Offset: 0x0015B9E0
		private static CreateTableOperation BuildCreateTableOperation(EntitySet entitySet, EdmModelDiffer.ModelMetadata modelMetadata)
		{
			CreateTableOperation createTableOperation = new CreateTableOperation(EdmModelDiffer.GetSchemaQualifiedName(entitySet), EdmModelDiffer.GetAnnotations(entitySet.ElementType), null);
			entitySet.ElementType.Properties.Each(delegate(EdmProperty p)
			{
				createTableOperation.Columns.Add(EdmModelDiffer.BuildColumnModel(p, modelMetadata, EdmModelDiffer.GetAnnotations(p).ToDictionary((KeyValuePair<string, object> a) => a.Key, (KeyValuePair<string, object> a) => new AnnotationValues(null, a.Value))));
			});
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null);
			entitySet.ElementType.KeyProperties.Each(delegate(EdmProperty p)
			{
				addPrimaryKeyOperation.Columns.Add(p.Name);
			});
			createTableOperation.PrimaryKey = addPrimaryKeyOperation;
			return createTableOperation;
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x0015D874 File Offset: 0x0015BA74
		private static ColumnModel BuildColumnModel(EdmProperty property, EdmModelDiffer.ModelMetadata modelMetadata, IDictionary<string, AnnotationValues> annotations)
		{
			TypeUsage edmType = modelMetadata.ProviderManifest.GetEdmType(property.TypeUsage);
			TypeUsage storeType = modelMetadata.ProviderManifest.GetStoreType(edmType);
			return EdmModelDiffer.BuildColumnModel(property, edmType, storeType, annotations);
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x0015D8AC File Offset: 0x0015BAAC
		public static ColumnModel BuildColumnModel(EdmProperty property, TypeUsage conceptualTypeUsage, TypeUsage defaultStoreTypeUsage, IDictionary<string, AnnotationValues> annotations)
		{
			ColumnModel columnModel = new ColumnModel(property.PrimitiveType.PrimitiveTypeKind, conceptualTypeUsage)
			{
				Name = property.Name,
				IsNullable = ((!property.Nullable) ? new bool?(false) : null),
				StoreType = ((!property.TypeName.EqualsIgnoreCase(defaultStoreTypeUsage.EdmType.Name)) ? property.TypeName : null),
				IsIdentity = (property.IsStoreGeneratedIdentity && EdmModelDiffer._validIdentityTypes.Contains(property.PrimitiveType.PrimitiveTypeKind)),
				IsTimestamp = (property.PrimitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary && property.MaxLength == 8 && property.IsStoreGeneratedComputed),
				IsUnicode = ((property.IsUnicode == false) ? new bool?(false) : null),
				IsFixedLength = ((property.IsFixedLength == true) ? new bool?(true) : null),
				Annotations = annotations
			};
			Facet facet;
			if (property.TypeUsage.Facets.TryGetValue("MaxLength", true, out facet) && !facet.IsUnbounded && !facet.Description.IsConstant)
			{
				columnModel.MaxLength = (int?)facet.Value;
			}
			if (property.TypeUsage.Facets.TryGetValue("Precision", true, out facet) && !facet.IsUnbounded && !facet.Description.IsConstant)
			{
				columnModel.Precision = (byte?)facet.Value;
			}
			if (property.TypeUsage.Facets.TryGetValue("Scale", true, out facet) && !facet.IsUnbounded && !facet.Description.IsConstant)
			{
				columnModel.Scale = (byte?)facet.Value;
			}
			return columnModel;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x0015DAB4 File Offset: 0x0015BCB4
		private static DbProviderManifest GetProviderManifest(DbProviderInfo providerInfo)
		{
			DbProviderFactory service = DbConfiguration.DependencyResolver.GetService(providerInfo.ProviderInvariantName);
			return service.GetProviderServices().GetProviderManifest(providerInfo.ProviderManifestToken);
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x0015DAE3 File Offset: 0x0015BCE3
		private static string GetSchemaQualifiedName(EntitySet entitySet)
		{
			return new DatabaseName(entitySet.Table, entitySet.Schema).ToString();
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0015DAFB File Offset: 0x0015BCFB
		private static string GetSchemaQualifiedName(EdmFunction function)
		{
			return new DatabaseName(function.FunctionName, function.Schema).ToString();
		}

		// Token: 0x04001A3D RID: 6717
		private static readonly PrimitiveTypeKind[] _validIdentityTypes = new PrimitiveTypeKind[]
		{
			PrimitiveTypeKind.Byte,
			PrimitiveTypeKind.Decimal,
			PrimitiveTypeKind.Guid,
			PrimitiveTypeKind.Int16,
			PrimitiveTypeKind.Int32,
			PrimitiveTypeKind.Int64
		};

		// Token: 0x04001A3E RID: 6718
		private static readonly DynamicEqualityComparer<ForeignKeyOperation> _foreignKeyEqualityComparer = new DynamicEqualityComparer<ForeignKeyOperation>((ForeignKeyOperation fk1, ForeignKeyOperation fk2) => fk1.Name.EqualsOrdinal(fk2.Name));

		// Token: 0x04001A3F RID: 6719
		private static readonly DynamicEqualityComparer<IndexOperation> _indexEqualityComparer = new DynamicEqualityComparer<IndexOperation>((IndexOperation i1, IndexOperation i2) => i1.Name.EqualsOrdinal(i2.Name) && i1.Table.EqualsOrdinal(i2.Table));

		// Token: 0x04001A40 RID: 6720
		private EdmModelDiffer.ModelMetadata _source;

		// Token: 0x04001A41 RID: 6721
		private EdmModelDiffer.ModelMetadata _target;

		// Token: 0x020006FB RID: 1787
		private class ModelMetadata
		{
			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x06004893 RID: 18579 RVA: 0x0015DBDB File Offset: 0x0015BDDB
			// (set) Token: 0x06004894 RID: 18580 RVA: 0x0015DBE3 File Offset: 0x0015BDE3
			public EdmItemCollection EdmItemCollection { get; set; }

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x06004895 RID: 18581 RVA: 0x0015DBEC File Offset: 0x0015BDEC
			// (set) Token: 0x06004896 RID: 18582 RVA: 0x0015DBF4 File Offset: 0x0015BDF4
			public StoreItemCollection StoreItemCollection { get; set; }

			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x06004897 RID: 18583 RVA: 0x0015DBFD File Offset: 0x0015BDFD
			// (set) Token: 0x06004898 RID: 18584 RVA: 0x0015DC05 File Offset: 0x0015BE05
			public EntityContainerMapping EntityContainerMapping { get; set; }

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x06004899 RID: 18585 RVA: 0x0015DC0E File Offset: 0x0015BE0E
			// (set) Token: 0x0600489A RID: 18586 RVA: 0x0015DC16 File Offset: 0x0015BE16
			public EntityContainer StoreEntityContainer { get; set; }

			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x0600489B RID: 18587 RVA: 0x0015DC1F File Offset: 0x0015BE1F
			// (set) Token: 0x0600489C RID: 18588 RVA: 0x0015DC27 File Offset: 0x0015BE27
			public DbProviderManifest ProviderManifest { get; set; }

			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x0600489D RID: 18589 RVA: 0x0015DC30 File Offset: 0x0015BE30
			// (set) Token: 0x0600489E RID: 18590 RVA: 0x0015DC38 File Offset: 0x0015BE38
			public DbProviderInfo ProviderInfo { get; set; }
		}
	}
}
