using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007B4 RID: 1972
	internal static class EntityMappingOperations
	{
		// Token: 0x06005949 RID: 22857 RVA: 0x001806C0 File Offset: 0x0017E8C0
		public static MappingFragment CreateTypeMappingFragment(EntityTypeMapping entityTypeMapping, MappingFragment templateFragment, EntitySet tableSet)
		{
			MappingFragment mappingFragment = new MappingFragment(tableSet, entityTypeMapping, false);
			entityTypeMapping.AddFragment(mappingFragment);
			foreach (ColumnMappingBuilder columnMappingBuilder in from pm in templateFragment.ColumnMappings
			where pm.ColumnProperty.IsPrimaryKeyColumn
			select pm)
			{
				EntityMappingOperations.CopyPropertyMappingToFragment(columnMappingBuilder, mappingFragment, TablePrimitiveOperations.GetNameMatcher(columnMappingBuilder.ColumnProperty.Name), true);
			}
			return mappingFragment;
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x00180754 File Offset: 0x0017E954
		private static void UpdatePropertyMapping(DbDatabaseMapping databaseMapping, IEnumerable<EntitySet> entitySets, Dictionary<EdmProperty, IList<ColumnMappingBuilder>> columnMappingIndex, ColumnMappingBuilder propertyMappingBuilder, EntityType fromTable, EntityType toTable, bool useExisting)
		{
			propertyMappingBuilder.ColumnProperty = TableOperations.CopyColumnAndAnyConstraints(databaseMapping.Database, fromTable, toTable, propertyMappingBuilder.ColumnProperty, EntityMappingOperations.GetPropertyPathMatcher(columnMappingIndex, propertyMappingBuilder), useExisting);
			propertyMappingBuilder.SyncNullabilityCSSpace(databaseMapping, entitySets, toTable);
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x001807EC File Offset: 0x0017E9EC
		private static Func<EdmProperty, bool> GetPropertyPathMatcher(Dictionary<EdmProperty, IList<ColumnMappingBuilder>> columnMappingIndex, ColumnMappingBuilder propertyMappingBuilder)
		{
			return delegate(EdmProperty c)
			{
				if (!columnMappingIndex.ContainsKey(c))
				{
					return false;
				}
				IList<ColumnMappingBuilder> list = columnMappingIndex[c];
				for (int i = 0; i < list.Count; i++)
				{
					ColumnMappingBuilder columnMappingBuilder = list[i];
					if (columnMappingBuilder.PropertyPath.PathEqual(propertyMappingBuilder.PropertyPath))
					{
						return true;
					}
				}
				return false;
			};
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x0018081C File Offset: 0x0017EA1C
		private static bool PathEqual(this IList<EdmProperty> listA, IList<EdmProperty> listB)
		{
			if (listA == null || listB == null)
			{
				return false;
			}
			if (listA.Count != listB.Count)
			{
				return false;
			}
			for (int i = 0; i < listA.Count; i++)
			{
				if (listA[i] != listB[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x00180868 File Offset: 0x0017EA68
		private static Dictionary<EdmProperty, IList<ColumnMappingBuilder>> GetColumnMappingIndex(DbDatabaseMapping databaseMapping)
		{
			Dictionary<EdmProperty, IList<ColumnMappingBuilder>> dictionary = new Dictionary<EdmProperty, IList<ColumnMappingBuilder>>();
			IEnumerable<EntitySetMapping> entitySetMappings = databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings;
			if (entitySetMappings == null)
			{
				return dictionary;
			}
			List<EntitySetMapping> list = entitySetMappings.ToList<EntitySetMapping>();
			for (int i = 0; i < list.Count; i++)
			{
				IList<EntityTypeMapping> entityTypeMappings = list[i].EntityTypeMappings;
				if (entityTypeMappings != null)
				{
					for (int j = 0; j < entityTypeMappings.Count; j++)
					{
						IList<MappingFragment> mappingFragments = entityTypeMappings[j].MappingFragments;
						if (mappingFragments != null)
						{
							for (int k = 0; k < mappingFragments.Count; k++)
							{
								IList<ColumnMappingBuilder> list2 = mappingFragments[k].ColumnMappings as IList<ColumnMappingBuilder>;
								if (list2 != null)
								{
									for (int l = 0; l < list2.Count; l++)
									{
										ColumnMappingBuilder columnMappingBuilder = list2[l];
										IList<ColumnMappingBuilder> list3;
										if (dictionary.ContainsKey(columnMappingBuilder.ColumnProperty))
										{
											list3 = dictionary[columnMappingBuilder.ColumnProperty];
										}
										else
										{
											dictionary.Add(columnMappingBuilder.ColumnProperty, list3 = new List<ColumnMappingBuilder>());
										}
										list3.Add(columnMappingBuilder);
									}
								}
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x00180990 File Offset: 0x0017EB90
		public static void UpdatePropertyMappings(DbDatabaseMapping databaseMapping, IEnumerable<EntitySet> entitySets, EntityType fromTable, MappingFragment fragment, bool useExisting)
		{
			if (fromTable != fragment.Table)
			{
				Dictionary<EdmProperty, IList<ColumnMappingBuilder>> columnMappingIndex = EntityMappingOperations.GetColumnMappingIndex(databaseMapping);
				List<ColumnMappingBuilder> list = fragment.ColumnMappings.ToList<ColumnMappingBuilder>();
				for (int i = 0; i < list.Count; i++)
				{
					EntityMappingOperations.UpdatePropertyMapping(databaseMapping, entitySets, columnMappingIndex, list[i], fromTable, fragment.Table, useExisting);
				}
			}
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x001809E4 File Offset: 0x0017EBE4
		public static void MovePropertyMapping(DbDatabaseMapping databaseMapping, IEnumerable<EntitySet> entitySets, MappingFragment fromFragment, MappingFragment toFragment, ColumnMappingBuilder propertyMappingBuilder, bool requiresUpdate, bool useExisting)
		{
			if (requiresUpdate && fromFragment.Table != toFragment.Table)
			{
				EntityMappingOperations.UpdatePropertyMapping(databaseMapping, entitySets, EntityMappingOperations.GetColumnMappingIndex(databaseMapping), propertyMappingBuilder, fromFragment.Table, toFragment.Table, useExisting);
			}
			fromFragment.RemoveColumnMapping(propertyMappingBuilder);
			toFragment.AddColumnMapping(propertyMappingBuilder);
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x00180A30 File Offset: 0x0017EC30
		public static void CopyPropertyMappingToFragment(ColumnMappingBuilder propertyMappingBuilder, MappingFragment fragment, Func<EdmProperty, bool> isCompatible, bool useExisting)
		{
			EdmProperty columnProperty = TablePrimitiveOperations.IncludeColumn(fragment.Table, propertyMappingBuilder.ColumnProperty, isCompatible, useExisting);
			fragment.AddColumnMapping(new ColumnMappingBuilder(columnProperty, propertyMappingBuilder.PropertyPath));
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x00180AA8 File Offset: 0x0017ECA8
		public static void UpdateConditions(EdmModel database, EntityType fromTable, MappingFragment fragment)
		{
			if (fromTable != fragment.Table)
			{
				fragment.ColumnConditions.Each(delegate(ConditionPropertyMapping cc)
				{
					cc.Column = TableOperations.CopyColumnAndAnyConstraints(database, fromTable, fragment.Table, cc.Column, TablePrimitiveOperations.GetNameMatcher(cc.Column.Name), true);
				});
			}
		}
	}
}
