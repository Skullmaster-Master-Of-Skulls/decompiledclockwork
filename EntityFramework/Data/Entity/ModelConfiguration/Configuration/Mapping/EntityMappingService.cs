using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007BB RID: 1979
	internal class EntityMappingService
	{
		// Token: 0x0600597E RID: 22910 RVA: 0x0018129D File Offset: 0x0017F49D
		public EntityMappingService(DbDatabaseMapping databaseMapping)
		{
			this._databaseMapping = databaseMapping;
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x001812AC File Offset: 0x0017F4AC
		public void Configure()
		{
			this.Analyze();
			this.Transform();
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x001812C4 File Offset: 0x0017F4C4
		private void Analyze()
		{
			this._tableMappings = new Dictionary<EntityType, TableMapping>();
			this._entityTypes = new SortedEntityTypeIndex();
			foreach (EntitySetMapping entitySetMapping in this._databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings))
			{
				foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
				{
					this._entityTypes.Add(entitySetMapping.EntitySet, entityTypeMapping.EntityType);
					foreach (MappingFragment mappingFragment in entityTypeMapping.MappingFragments)
					{
						TableMapping tableMapping = this.FindOrCreateTableMapping(mappingFragment.Table);
						tableMapping.AddEntityTypeMappingFragment(entitySetMapping.EntitySet, entityTypeMapping.EntityType, mappingFragment);
					}
				}
			}
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x0018149C File Offset: 0x0017F69C
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void Transform()
		{
			using (IEnumerator<EntitySet> enumerator = this._entityTypes.GetEntitySets().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntitySet entitySet = enumerator.Current;
					Dictionary<TableMapping, Dictionary<EntityType, EntityTypeMapping>> dictionary = new Dictionary<TableMapping, Dictionary<EntityType, EntityTypeMapping>>();
					using (IEnumerator<EntityType> enumerator2 = this._entityTypes.GetEntityTypes(entitySet).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EntityType entityType = enumerator2.Current;
							foreach (TableMapping tableMapping in from tm in this._tableMappings.Values
							where tm.EntityTypes.Contains(entitySet, entityType)
							select tm)
							{
								Dictionary<EntityType, EntityTypeMapping> dictionary2;
								if (!dictionary.TryGetValue(tableMapping, out dictionary2))
								{
									dictionary2 = new Dictionary<EntityType, EntityTypeMapping>();
									dictionary.Add(tableMapping, dictionary2);
								}
								EntityMappingService.RemoveRedundantDefaultDiscriminators(tableMapping);
								bool flag = this.DetermineRequiresIsTypeOf(tableMapping, entitySet, entityType);
								EntityTypeMapping propertiesTypeMapping;
								MappingFragment propertiesTypeMappingFragment;
								if (this.FindPropertyEntityTypeMapping(tableMapping, entitySet, entityType, flag, out propertiesTypeMapping, out propertiesTypeMappingFragment))
								{
									bool flag2 = EntityMappingService.DetermineRequiresSplitEntityTypeMapping(tableMapping, entityType, flag);
									EntityTypeMapping entityTypeMapping = this.FindConditionTypeMapping(entityType, flag2, propertiesTypeMapping);
									MappingFragment mappingFragment = EntityMappingService.FindConditionTypeMappingFragment(this._databaseMapping.Database.GetEntitySet(tableMapping.Table), propertiesTypeMappingFragment, entityTypeMapping);
									if (flag)
									{
										if (!propertiesTypeMapping.IsHierarchyMapping)
										{
											EntityTypeMapping entityTypeMapping2 = this._databaseMapping.GetEntityTypeMappings(entityType).SingleOrDefault((EntityTypeMapping etm) => etm.IsHierarchyMapping);
											if (entityTypeMapping2 == null)
											{
												if (propertiesTypeMapping.MappingFragments.Count > 1)
												{
													EntityTypeMapping entityTypeMapping3 = propertiesTypeMapping.Clone();
													EntitySetMapping entitySetMapping = this._databaseMapping.GetEntitySetMappings().Single((EntitySetMapping esm) => esm.EntityTypeMappings.Contains(propertiesTypeMapping));
													entitySetMapping.AddTypeMapping(entityTypeMapping3);
													foreach (MappingFragment fragment in propertiesTypeMapping.MappingFragments.Where((MappingFragment tmf) => tmf != propertiesTypeMappingFragment).ToArray<MappingFragment>())
													{
														propertiesTypeMapping.RemoveFragment(fragment);
														entityTypeMapping3.AddFragment(fragment);
													}
												}
												propertiesTypeMapping.AddIsOfType(propertiesTypeMapping.EntityType);
											}
											else
											{
												propertiesTypeMapping.RemoveFragment(propertiesTypeMappingFragment);
												if (propertiesTypeMapping.MappingFragments.Count == 0)
												{
													this._databaseMapping.GetEntitySetMapping(entitySet).RemoveTypeMapping(propertiesTypeMapping);
												}
												propertiesTypeMapping = entityTypeMapping2;
												propertiesTypeMapping.AddFragment(propertiesTypeMappingFragment);
											}
										}
										dictionary2.Add(entityType, propertiesTypeMapping);
									}
									EntityMappingService.ConfigureTypeMappings(tableMapping, dictionary2, entityType, propertiesTypeMappingFragment, mappingFragment);
									if (propertiesTypeMappingFragment.IsUnmappedPropertiesFragment())
									{
										if (propertiesTypeMappingFragment.ColumnMappings.All((ColumnMappingBuilder pm) => entityType.GetKeyProperties().Contains(pm.PropertyPath.First<EdmProperty>())))
										{
											this.RemoveFragment(entitySet, propertiesTypeMapping, propertiesTypeMappingFragment);
											if (flag2)
											{
												if (mappingFragment.ColumnMappings.All((ColumnMappingBuilder pm) => entityType.GetKeyProperties().Contains(pm.PropertyPath.First<EdmProperty>())))
												{
													this.RemoveFragment(entitySet, entityTypeMapping, mappingFragment);
												}
											}
										}
									}
									EntityMappingConfiguration.CleanupUnmappedArtifacts(this._databaseMapping, tableMapping.Table);
									foreach (ForeignKeyBuilder foreignKeyBuilder in tableMapping.Table.ForeignKeyBuilders)
									{
										AssociationType associationType = foreignKeyBuilder.GetAssociationType();
										if (associationType != null && associationType.IsRequiredToNonRequired())
										{
											AssociationEndMember associationEndMember;
											AssociationEndMember associationEndMember2;
											foreignKeyBuilder.GetAssociationType().TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2);
											if (associationEndMember2.GetEntityType() == entityType)
											{
												this.MarkColumnsAsNonNullableIfNoTableSharing(entitySet, tableMapping.Table, entityType, foreignKeyBuilder.DependentColumns);
											}
										}
									}
								}
							}
						}
					}
					this.ConfigureAssociationSetMappingForeignKeys(entitySet);
				}
			}
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x00181A0C File Offset: 0x0017FC0C
		private void ConfigureAssociationSetMappingForeignKeys(EntitySet entitySet)
		{
			foreach (AssociationSetMapping associationSetMapping in from asm in this._databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.AssociationSetMappings)
			where (asm.AssociationSet.SourceSet == entitySet || asm.AssociationSet.TargetSet == entitySet) && asm.AssociationSet.ElementType.IsRequiredToNonRequired()
			select asm)
			{
				AssociationEndMember associationEndMember;
				AssociationEndMember associationEndMember2;
				associationSetMapping.AssociationSet.ElementType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2);
				if ((associationEndMember2 == associationSetMapping.AssociationSet.ElementType.SourceEnd && associationSetMapping.AssociationSet.SourceSet == entitySet) || (associationEndMember2 == associationSetMapping.AssociationSet.ElementType.TargetEnd && associationSetMapping.AssociationSet.TargetSet == entitySet))
				{
					EndPropertyMapping endPropertyMapping = (associationSetMapping.SourceEndMapping.AssociationEnd == associationEndMember2) ? associationSetMapping.TargetEndMapping : associationSetMapping.SourceEndMapping;
					this.MarkColumnsAsNonNullableIfNoTableSharing(entitySet, associationSetMapping.Table, associationEndMember2.GetEntityType(), endPropertyMapping.PropertyMappings.Select((ScalarPropertyMapping pm) => pm.Column));
				}
			}
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x00181BBC File Offset: 0x0017FDBC
		private void MarkColumnsAsNonNullableIfNoTableSharing(EntitySet entitySet, EntityType table, EntityType dependentEndEntityType, IEnumerable<EdmProperty> columns)
		{
			IEnumerable<EntityType> source = from et in this._tableMappings[table].EntityTypes.GetEntityTypes(entitySet)
			where et != dependentEndEntityType && (et.IsAncestorOf(dependentEndEntityType) || !dependentEndEntityType.IsAncestorOf(et))
			select et;
			if (source.Count<EntityType>() != 0)
			{
				if (!source.All((EntityType et) => et.Abstract))
				{
					return;
				}
			}
			columns.Each((EdmProperty c) => c.Nullable = false);
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x00181EFC File Offset: 0x001800FC
		private static void ConfigureTypeMappings(TableMapping tableMapping, Dictionary<EntityType, EntityTypeMapping> rootMappings, EntityType entityType, MappingFragment propertiesTypeMappingFragment, MappingFragment conditionTypeMappingFragment)
		{
			List<ColumnMappingBuilder> list = new List<ColumnMappingBuilder>(from pm in propertiesTypeMappingFragment.ColumnMappings
			where !pm.ColumnProperty.IsPrimaryKeyColumn
			select pm);
			List<ConditionPropertyMapping> list2 = new List<ConditionPropertyMapping>(propertiesTypeMappingFragment.ColumnConditions);
			using (var enumerator = (from cm in tableMapping.ColumnMappings
			from pm in cm.PropertyMappings
			where pm.EntityType == entityType
			select new
			{
				Column = cm.Column,
				Property = pm
			}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					<>f__AnonymousType3b<EdmProperty, PropertyMappingSpecification> columnMapping = enumerator.Current;
					if (columnMapping.Property.PropertyPath != null && !EntityMappingService.IsRootTypeMapping(rootMappings, columnMapping.Property.EntityType, columnMapping.Property.PropertyPath))
					{
						ColumnMappingBuilder columnMappingBuilder = propertiesTypeMappingFragment.ColumnMappings.SingleOrDefault((ColumnMappingBuilder x) => x.PropertyPath == columnMapping.Property.PropertyPath);
						if (columnMappingBuilder != null)
						{
							list.Remove(columnMappingBuilder);
						}
						else
						{
							columnMappingBuilder = new ColumnMappingBuilder(columnMapping.Column, columnMapping.Property.PropertyPath);
							propertiesTypeMappingFragment.AddColumnMapping(columnMappingBuilder);
						}
					}
					if (columnMapping.Property.Conditions != null)
					{
						foreach (ConditionPropertyMapping conditionPropertyMapping in columnMapping.Property.Conditions)
						{
							if (conditionTypeMappingFragment.ColumnConditions.Contains(conditionPropertyMapping))
							{
								list2.Remove(conditionPropertyMapping);
							}
							else if (!entityType.Abstract)
							{
								conditionTypeMappingFragment.AddConditionProperty(conditionPropertyMapping);
							}
						}
					}
				}
			}
			foreach (ColumnMappingBuilder columnMappingBuilder2 in list)
			{
				propertiesTypeMappingFragment.RemoveColumnMapping(columnMappingBuilder2);
			}
			foreach (ConditionPropertyMapping condition in list2)
			{
				conditionTypeMappingFragment.RemoveConditionProperty(condition);
			}
			if (entityType.Abstract)
			{
				propertiesTypeMappingFragment.ClearConditions();
			}
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x00182220 File Offset: 0x00180420
		private static MappingFragment FindConditionTypeMappingFragment(EntitySet tableSet, MappingFragment propertiesTypeMappingFragment, EntityTypeMapping conditionTypeMapping)
		{
			EntityType table = tableSet.ElementType;
			MappingFragment mappingFragment = conditionTypeMapping.MappingFragments.SingleOrDefault((MappingFragment x) => x.Table == table);
			if (mappingFragment == null)
			{
				mappingFragment = EntityMappingOperations.CreateTypeMappingFragment(conditionTypeMapping, propertiesTypeMappingFragment, tableSet);
				mappingFragment.SetIsConditionOnlyFragment(true);
				if (propertiesTypeMappingFragment.GetDefaultDiscriminator() != null)
				{
					mappingFragment.SetDefaultDiscriminator(propertiesTypeMappingFragment.GetDefaultDiscriminator());
					propertiesTypeMappingFragment.RemoveDefaultDiscriminatorAnnotation();
				}
			}
			return mappingFragment;
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x001822A8 File Offset: 0x001804A8
		private EntityTypeMapping FindConditionTypeMapping(EntityType entityType, bool requiresSplit, EntityTypeMapping propertiesTypeMapping)
		{
			EntityTypeMapping entityTypeMapping = propertiesTypeMapping;
			if (requiresSplit)
			{
				if (!entityType.Abstract)
				{
					entityTypeMapping = propertiesTypeMapping.Clone();
					entityTypeMapping.RemoveIsOfType(entityTypeMapping.EntityType);
					EntitySetMapping entitySetMapping = this._databaseMapping.GetEntitySetMappings().Single((EntitySetMapping esm) => esm.EntityTypeMappings.Contains(propertiesTypeMapping));
					entitySetMapping.AddTypeMapping(entityTypeMapping);
				}
				propertiesTypeMapping.MappingFragments.Each(delegate(MappingFragment tmf)
				{
					tmf.ClearConditions();
				});
			}
			return entityTypeMapping;
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x001823AC File Offset: 0x001805AC
		private bool DetermineRequiresIsTypeOf(TableMapping tableMapping, EntitySet entitySet, EntityType entityType)
		{
			return entityType.IsRootOfSet(tableMapping.EntityTypes.GetEntityTypes(entitySet)) && ((tableMapping.EntityTypes.GetEntityTypes(entitySet).Count<EntityType>() > 1 && tableMapping.EntityTypes.GetEntityTypes(entitySet).Any((EntityType et) => et != entityType && !et.Abstract)) || this._tableMappings.Values.Any((TableMapping tm) => tm != tableMapping && tm.Table.ForeignKeyBuilders.Any((ForeignKeyBuilder fk) => fk.GetIsTypeConstraint() && fk.PrincipalTable == tableMapping.Table)));
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x00182448 File Offset: 0x00180648
		private static bool DetermineRequiresSplitEntityTypeMapping(TableMapping tableMapping, EntityType entityType, bool requiresIsTypeOf)
		{
			return requiresIsTypeOf && EntityMappingService.HasConditions(tableMapping, entityType);
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x001826CC File Offset: 0x001808CC
		private bool FindPropertyEntityTypeMapping(TableMapping tableMapping, EntitySet entitySet, EntityType entityType, bool requiresIsTypeOf, out EntityTypeMapping entityTypeMapping, out MappingFragment fragment)
		{
			entityTypeMapping = null;
			fragment = null;
			var <>f__AnonymousType3d = (from etm in this._databaseMapping.GetEntityTypeMappings(entityType)
			from tmf in etm.MappingFragments
			where tmf.Table == tableMapping.Table
			select new
			{
				TypeMapping = etm,
				Fragment = tmf
			}).SingleOrDefault();
			if (<>f__AnonymousType3d == null)
			{
				return false;
			}
			entityTypeMapping = <>f__AnonymousType3d.TypeMapping;
			fragment = <>f__AnonymousType3d.Fragment;
			if (!requiresIsTypeOf && entityType.Abstract)
			{
				this.RemoveFragment(entitySet, <>f__AnonymousType3d.TypeMapping, <>f__AnonymousType3d.Fragment);
				return false;
			}
			return true;
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x00182808 File Offset: 0x00180A08
		private void RemoveFragment(EntitySet entitySet, EntityTypeMapping entityTypeMapping, MappingFragment fragment)
		{
			EdmProperty defaultDiscriminator = fragment.GetDefaultDiscriminator();
			if (defaultDiscriminator != null && entityTypeMapping.EntityType.BaseType != null && !entityTypeMapping.EntityType.Abstract)
			{
				ColumnMapping columnMapping = this._tableMappings[fragment.Table].ColumnMappings.SingleOrDefault((ColumnMapping cm) => cm.Column == defaultDiscriminator);
				if (columnMapping != null)
				{
					PropertyMappingSpecification propertyMappingSpecification = columnMapping.PropertyMappings.SingleOrDefault((PropertyMappingSpecification pm) => pm.EntityType == entityTypeMapping.EntityType);
					if (propertyMappingSpecification != null)
					{
						columnMapping.PropertyMappings.Remove(propertyMappingSpecification);
					}
				}
				defaultDiscriminator.Nullable = true;
			}
			if (entityTypeMapping.EntityType.Abstract)
			{
				foreach (ColumnMapping columnMapping2 in from cm in this._tableMappings[fragment.Table].ColumnMappings
				where cm.PropertyMappings.All((PropertyMappingSpecification pm) => pm.EntityType == entityTypeMapping.EntityType)
				select cm)
				{
					fragment.Table.RemoveMember(columnMapping2.Column);
				}
			}
			entityTypeMapping.RemoveFragment(fragment);
			if (!entityTypeMapping.MappingFragments.Any<MappingFragment>())
			{
				this._databaseMapping.GetEntitySetMapping(entitySet).RemoveTypeMapping(entityTypeMapping);
			}
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x00182BA8 File Offset: 0x00180DA8
		private static void RemoveRedundantDefaultDiscriminators(TableMapping tableMapping)
		{
			using (IEnumerator<EntitySet> enumerator = tableMapping.EntityTypes.GetEntitySets().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntitySet entitySet = enumerator.Current;
					(from cm in tableMapping.ColumnMappings
					from pm in cm.PropertyMappings
					where (from pm1 in cm.PropertyMappings
					where tableMapping.EntityTypes.GetEntityTypes(entitySet).Contains(pm1.EntityType)
					select pm1).Count((PropertyMappingSpecification pms) => pms.IsDefaultDiscriminatorCondition) == 1
					select new
					{
						ColumnMapping = cm,
						PropertyMapping = pm
					}).ToArray().Each(delegate(x)
					{
						x.PropertyMapping.Conditions.Clear();
						if (x.PropertyMapping.PropertyPath == null)
						{
							x.ColumnMapping.PropertyMappings.Remove(x.PropertyMapping);
						}
					});
				}
			}
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x00182CF8 File Offset: 0x00180EF8
		private static bool HasConditions(TableMapping tableMapping, EntityType entityType)
		{
			return tableMapping.ColumnMappings.SelectMany((ColumnMapping cm) => cm.PropertyMappings).Any((PropertyMappingSpecification pm) => pm.EntityType == entityType && pm.Conditions.Count > 0);
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x00182D70 File Offset: 0x00180F70
		private static bool IsRootTypeMapping(Dictionary<EntityType, EntityTypeMapping> rootMappings, EntityType entityType, IList<EdmProperty> propertyPath)
		{
			for (EntityType entityType2 = (EntityType)entityType.BaseType; entityType2 != null; entityType2 = (EntityType)entityType2.BaseType)
			{
				EntityTypeMapping entityTypeMapping;
				if (rootMappings.TryGetValue(entityType2, out entityTypeMapping))
				{
					return entityTypeMapping.MappingFragments.SelectMany((MappingFragment etmf) => etmf.ColumnMappings).Any((ColumnMappingBuilder pm) => pm.PropertyPath.SequenceEqual(propertyPath));
				}
			}
			return false;
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x00182DF4 File Offset: 0x00180FF4
		private TableMapping FindOrCreateTableMapping(EntityType table)
		{
			TableMapping tableMapping;
			if (!this._tableMappings.TryGetValue(table, out tableMapping))
			{
				tableMapping = new TableMapping(table);
				this._tableMappings.Add(table, tableMapping);
			}
			return tableMapping;
		}

		// Token: 0x040023BC RID: 9148
		private readonly DbDatabaseMapping _databaseMapping;

		// Token: 0x040023BD RID: 9149
		private Dictionary<EntityType, TableMapping> _tableMappings;

		// Token: 0x040023BE RID: 9150
		private SortedEntityTypeIndex _entityTypes;
	}
}
