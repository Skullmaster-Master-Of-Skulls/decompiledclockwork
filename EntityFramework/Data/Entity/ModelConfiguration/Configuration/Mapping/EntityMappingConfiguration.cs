using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x020007AF RID: 1967
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class EntityMappingConfiguration
	{
		// Token: 0x060058DA RID: 22746 RVA: 0x0017D560 File Offset: 0x0017B760
		internal EntityMappingConfiguration()
		{
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x0017D5C8 File Offset: 0x0017B7C8
		private EntityMappingConfiguration(EntityMappingConfiguration source)
		{
			this._tableName = source._tableName;
			this.MapInheritedProperties = source.MapInheritedProperties;
			if (source._properties != null)
			{
				this._properties = new List<PropertyPath>(source._properties);
			}
			this._valueConditions.AddRange(from c in source._valueConditions
			select c.Clone(this));
			this._notNullConditions.AddRange(from c in source._notNullConditions
			select c.Clone(this));
			source._primitivePropertyConfigurations.Each(delegate(KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> c)
			{
				this._primitivePropertyConfigurations.Add(c.Key, c.Value.Clone());
			});
			foreach (KeyValuePair<string, object> item in source._annotations)
			{
				this._annotations.Add(item);
			}
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x0017D6F4 File Offset: 0x0017B8F4
		internal virtual EntityMappingConfiguration Clone()
		{
			return new EntityMappingConfiguration(this);
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060058DD RID: 22749 RVA: 0x0017D6FC File Offset: 0x0017B8FC
		// (set) Token: 0x060058DE RID: 22750 RVA: 0x0017D704 File Offset: 0x0017B904
		public bool MapInheritedProperties { get; set; }

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x060058DF RID: 22751 RVA: 0x0017D70D File Offset: 0x0017B90D
		// (set) Token: 0x060058E0 RID: 22752 RVA: 0x0017D715 File Offset: 0x0017B915
		public DatabaseName TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060058E1 RID: 22753 RVA: 0x0017D71E File Offset: 0x0017B91E
		public IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x0017D726 File Offset: 0x0017B926
		public virtual void SetAnnotation(string name, object value)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060058E3 RID: 22755 RVA: 0x0017D749 File Offset: 0x0017B949
		// (set) Token: 0x060058E4 RID: 22756 RVA: 0x0017D751 File Offset: 0x0017B951
		internal List<PropertyPath> Properties
		{
			get
			{
				return this._properties;
			}
			set
			{
				if (this._properties == null)
				{
					this._properties = new List<PropertyPath>();
				}
				value.Each(new Action<PropertyPath>(this.Property));
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x060058E5 RID: 22757 RVA: 0x0017D778 File Offset: 0x0017B978
		internal IDictionary<PropertyPath, PrimitivePropertyConfiguration> PrimitivePropertyConfigurations
		{
			get
			{
				return this._primitivePropertyConfigurations;
			}
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0017D780 File Offset: 0x0017B980
		internal TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(PropertyPath propertyPath, Func<TPrimitivePropertyConfiguration> primitivePropertyConfigurationCreator) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration
		{
			if (this._properties == null)
			{
				this._properties = new List<PropertyPath>();
			}
			this.Property(propertyPath);
			PrimitivePropertyConfiguration primitivePropertyConfiguration;
			if (!this._primitivePropertyConfigurations.TryGetValue(propertyPath, out primitivePropertyConfiguration))
			{
				this._primitivePropertyConfigurations.Add(propertyPath, primitivePropertyConfiguration = primitivePropertyConfigurationCreator());
			}
			return (TPrimitivePropertyConfiguration)((object)primitivePropertyConfiguration);
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x0017D7EC File Offset: 0x0017B9EC
		private void Property(PropertyPath property)
		{
			if (!(from pp in this._properties
			where pp.SequenceEqual(property)
			select pp).Any<PropertyPath>())
			{
				this._properties.Add(property);
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x0017D835 File Offset: 0x0017BA35
		public List<ValueConditionConfiguration> ValueConditions
		{
			get
			{
				return this._valueConditions;
			}
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0017D860 File Offset: 0x0017BA60
		public void AddValueCondition(ValueConditionConfiguration valueCondition)
		{
			ValueConditionConfiguration valueConditionConfiguration = this.ValueConditions.SingleOrDefault((ValueConditionConfiguration vc) => vc.Discriminator.Equals(valueCondition.Discriminator, StringComparison.Ordinal));
			if (valueConditionConfiguration == null)
			{
				this.ValueConditions.Add(valueCondition);
				return;
			}
			valueConditionConfiguration.Value = valueCondition.Value;
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x0017D8B8 File Offset: 0x0017BAB8
		// (set) Token: 0x060058EB RID: 22763 RVA: 0x0017D8C0 File Offset: 0x0017BAC0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public List<NotNullConditionConfiguration> NullabilityConditions
		{
			get
			{
				return this._notNullConditions;
			}
			set
			{
				value.Each(new Action<NotNullConditionConfiguration>(this.AddNullabilityCondition));
			}
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x0017D8D4 File Offset: 0x0017BAD4
		public void AddNullabilityCondition(NotNullConditionConfiguration notNullConditionConfiguration)
		{
			if (!this.NullabilityConditions.Contains(notNullConditionConfiguration))
			{
				this.NullabilityConditions.Add(notNullConditionConfiguration);
			}
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0017D948 File Offset: 0x0017BB48
		public bool MapsAnyInheritedProperties(EntityType entityType)
		{
			HashSet<EdmPropertyPath> properties = new HashSet<EdmPropertyPath>();
			if (this.Properties != null)
			{
				this.Properties.Each(delegate(PropertyPath p)
				{
					properties.AddRange(EntityMappingConfiguration.PropertyPathToEdmPropertyPath(p, entityType));
				});
			}
			return this.MapInheritedProperties || properties.Any((EdmPropertyPath x) => !entityType.KeyProperties().Contains(x.First<EdmProperty>()) && !entityType.DeclaredProperties.Contains(x.First<EdmProperty>()));
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x0017DA78 File Offset: 0x0017BC78
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		public void Configure(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest, EntityType entityType, ref EntityTypeMapping entityTypeMapping, bool isMappingAnyInheritedProperty, int configurationIndex, int configurationCount, IDictionary<string, object> commonAnnotations)
		{
			EntityType baseType = (EntityType)entityType.BaseType;
			bool flag = baseType == null && configurationIndex == 0;
			MappingFragment mappingFragment = this.FindOrCreateTypeMappingFragment(databaseMapping, ref entityTypeMapping, configurationIndex, entityType, providerManifest);
			EntityType table = mappingFragment.Table;
			bool flag2;
			EntityType entityType2 = this.FindOrCreateTargetTable(databaseMapping, mappingFragment, entityType, table, out flag2);
			bool isSharingTableWithBase = this.DiscoverIsSharingWithBase(databaseMapping, entityType, entityType2);
			HashSet<EdmPropertyPath> hashSet = this.DiscoverAllMappingsToContain(databaseMapping, entityType, entityType2, isSharingTableWithBase);
			List<ColumnMappingBuilder> list = mappingFragment.ColumnMappings.ToList<ColumnMappingBuilder>();
			using (HashSet<EdmPropertyPath>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmPropertyPath propertyPath = enumerator.Current;
					ColumnMappingBuilder columnMappingBuilder = mappingFragment.ColumnMappings.SingleOrDefault((ColumnMappingBuilder pm) => pm.PropertyPath.SequenceEqual(propertyPath));
					if (columnMappingBuilder == null)
					{
						throw Error.EntityMappingConfiguration_DuplicateMappedProperty(entityType.Name, propertyPath.ToString());
					}
					list.Remove(columnMappingBuilder);
				}
			}
			if (!flag)
			{
				bool isSplitting;
				EntityType entityType3 = EntityMappingConfiguration.FindParentTable(databaseMapping, table, entityTypeMapping, entityType2, isMappingAnyInheritedProperty, configurationIndex, configurationCount, out isSplitting);
				if (entityType3 != null)
				{
					DatabaseOperations.AddTypeConstraint(databaseMapping.Database, entityType, entityType3, entityType2, isSplitting);
				}
			}
			if (table != entityType2)
			{
				if (this.Properties == null)
				{
					AssociationMappingOperations.MoveAllDeclaredAssociationSetMappings(databaseMapping, entityType, table, entityType2, !flag2);
					ForeignKeyPrimitiveOperations.MoveAllDeclaredForeignKeyConstraintsForPrimaryKeyColumns(entityType, table, entityType2);
				}
				if (isMappingAnyInheritedProperty)
				{
					IEnumerable<EntityType> baseTables = from mf in databaseMapping.GetEntityTypeMappings(baseType).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
					select mf.Table;
					AssociationSetMapping associationSetMapping = databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping asm) => asm.AssociationSetMappings).FirstOrDefault((AssociationSetMapping a) => baseTables.Contains(a.Table) && (baseType == a.AssociationSet.ElementType.SourceEnd.GetEntityType() || baseType == a.AssociationSet.ElementType.TargetEnd.GetEntityType()));
					if (associationSetMapping != null)
					{
						AssociationType elementType = associationSetMapping.AssociationSet.ElementType;
						throw Error.EntityMappingConfiguration_TPCWithIAsOnNonLeafType(elementType.Name, elementType.SourceEnd.GetEntityType().Name, elementType.TargetEnd.GetEntityType().Name);
					}
					ForeignKeyPrimitiveOperations.CopyAllForeignKeyConstraintsForPrimaryKeyColumns(databaseMapping.Database, table, entityType2);
				}
			}
			if (list.Any<ColumnMappingBuilder>())
			{
				EntityType extraTable = null;
				if (configurationIndex < configurationCount - 1)
				{
					ColumnMappingBuilder pm2 = list.First<ColumnMappingBuilder>();
					extraTable = EntityMappingConfiguration.FindTableForTemporaryExtraPropertyMapping(databaseMapping, entityType, table, entityType2, pm2);
					MappingFragment toFragment = EntityMappingOperations.CreateTypeMappingFragment(entityTypeMapping, mappingFragment, databaseMapping.Database.GetEntitySet(extraTable));
					bool requiresUpdate = extraTable != table;
					using (List<ColumnMappingBuilder>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ColumnMappingBuilder propertyMappingBuilder = enumerator2.Current;
							EntityMappingOperations.MovePropertyMapping(databaseMapping, entitySets, mappingFragment, toFragment, propertyMappingBuilder, requiresUpdate, true);
						}
						goto IL_3DE;
					}
				}
				EntityType entityType4 = null;
				foreach (ColumnMappingBuilder columnMappingBuilder2 in list)
				{
					extraTable = EntityMappingConfiguration.FindTableForExtraPropertyMapping(databaseMapping, entityType, table, entityType2, ref entityType4, columnMappingBuilder2);
					MappingFragment mappingFragment2 = entityTypeMapping.MappingFragments.SingleOrDefault((MappingFragment tmf) => tmf.Table == extraTable);
					if (mappingFragment2 == null)
					{
						mappingFragment2 = EntityMappingOperations.CreateTypeMappingFragment(entityTypeMapping, mappingFragment, databaseMapping.Database.GetEntitySet(extraTable));
						mappingFragment2.SetIsUnmappedPropertiesFragment(true);
					}
					if (extraTable == table)
					{
						EntityMappingConfiguration.CopyDefaultDiscriminator(mappingFragment, mappingFragment2);
					}
					bool requiresUpdate2 = extraTable != table;
					EntityMappingOperations.MovePropertyMapping(databaseMapping, entitySets, mappingFragment, mappingFragment2, columnMappingBuilder2, requiresUpdate2, true);
				}
			}
			IL_3DE:
			EntityMappingOperations.UpdatePropertyMappings(databaseMapping, entitySets, table, mappingFragment, !flag2);
			this.ConfigureDefaultDiscriminator(entityType, mappingFragment);
			this.ConfigureConditions(databaseMapping, entityType, mappingFragment, providerManifest);
			EntityMappingOperations.UpdateConditions(databaseMapping.Database, table, mappingFragment);
			ForeignKeyPrimitiveOperations.UpdatePrincipalTables(databaseMapping, entityType, table, entityType2, isMappingAnyInheritedProperty);
			EntityMappingConfiguration.CleanupUnmappedArtifacts(databaseMapping, table);
			EntityMappingConfiguration.CleanupUnmappedArtifacts(databaseMapping, entityType2);
			EntityMappingConfiguration.ConfigureAnnotations(entityType2, commonAnnotations);
			EntityMappingConfiguration.ConfigureAnnotations(entityType2, this._annotations);
			entityType2.SetConfiguration(this);
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0017DF48 File Offset: 0x0017C148
		private static void ConfigureAnnotations(EdmType toTable, IDictionary<string, object> annotations)
		{
			using (IEnumerator<KeyValuePair<string, object>> enumerator = annotations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntityMappingConfiguration.<>c__DisplayClass25 CS$<>8__locals1 = new EntityMappingConfiguration.<>c__DisplayClass25();
					CS$<>8__locals1.annotation = enumerator.Current;
					EntityMappingConfiguration.<>c__DisplayClass27 CS$<>8__locals2 = new EntityMappingConfiguration.<>c__DisplayClass27();
					CS$<>8__locals2.CS$<>8__locals26 = CS$<>8__locals1;
					EntityMappingConfiguration.<>c__DisplayClass27 CS$<>8__locals3 = CS$<>8__locals2;
					string str = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:";
					KeyValuePair<string, object> annotation = CS$<>8__locals1.annotation;
					CS$<>8__locals3.name = str + annotation.Key;
					MetadataProperty metadataProperty = toTable.Annotations.FirstOrDefault(delegate(MetadataProperty a)
					{
						if (a.Name == CS$<>8__locals2.name)
						{
							object value = a.Value;
							KeyValuePair<string, object> annotation5 = CS$<>8__locals2.CS$<>8__locals26.annotation;
							return !object.Equals(value, annotation5.Value);
						}
						return false;
					});
					if (metadataProperty != null)
					{
						KeyValuePair<string, object> annotation2 = CS$<>8__locals1.annotation;
						object key = annotation2.Key;
						KeyValuePair<string, object> annotation3 = CS$<>8__locals1.annotation;
						throw new InvalidOperationException(Strings.ConflictingTypeAnnotation(key, annotation3.Value, metadataProperty.Value, toTable.Name));
					}
					string name = CS$<>8__locals2.name;
					KeyValuePair<string, object> annotation4 = CS$<>8__locals1.annotation;
					toTable.AddAnnotation(name, annotation4.Value);
				}
			}
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0017E0D0 File Offset: 0x0017C2D0
		internal void ConfigurePropertyMappings(IList<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings, DbProviderManifest providerManifest, bool allowOverride = false)
		{
			foreach (KeyValuePair<PropertyPath, PrimitivePropertyConfiguration> keyValuePair in this._primitivePropertyConfigurations)
			{
				PropertyPath propertyPath = keyValuePair.Key;
				PrimitivePropertyConfiguration value = keyValuePair.Value;
				value.Configure(from pm in propertyMappings
				where propertyPath.Equals(new PropertyPath(from p in pm.Item1.PropertyPath.Skip(pm.Item1.PropertyPath.Count - propertyPath.Count)
				select p.GetClrPropertyInfo())) && object.Equals(this.TableName, pm.Item2.GetTableName())
				select pm, providerManifest, allowOverride, true);
			}
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x0017E15C File Offset: 0x0017C35C
		private void ConfigureDefaultDiscriminator(EntityType entityType, MappingFragment fragment)
		{
			if (this.ValueConditions.Any<ValueConditionConfiguration>() || this.NullabilityConditions.Any<NotNullConditionConfiguration>())
			{
				EdmProperty edmProperty = fragment.RemoveDefaultDiscriminatorCondition();
				if (edmProperty != null && entityType.BaseType != null)
				{
					edmProperty.Nullable = true;
				}
			}
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x0017E1B4 File Offset: 0x0017C3B4
		private static void CopyDefaultDiscriminator(MappingFragment fromFragment, MappingFragment toFragment)
		{
			EdmProperty discriminatorColumn = fromFragment.GetDefaultDiscriminator();
			if (discriminatorColumn != null)
			{
				ConditionPropertyMapping conditionPropertyMapping = fromFragment.ColumnConditions.SingleOrDefault((ConditionPropertyMapping cc) => cc.Column == discriminatorColumn);
				if (conditionPropertyMapping != null)
				{
					toFragment.AddDiscriminatorCondition(conditionPropertyMapping.Column, conditionPropertyMapping.Value);
					toFragment.SetDefaultDiscriminator(conditionPropertyMapping.Column);
				}
			}
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0017E21C File Offset: 0x0017C41C
		private static EntityType FindTableForTemporaryExtraPropertyMapping(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, ColumnMappingBuilder pm)
		{
			EntityType entityType2;
			if (fromTable == toTable)
			{
				entityType2 = databaseMapping.Database.AddTable(entityType.Name, fromTable);
			}
			else if (entityType.BaseType == null)
			{
				entityType2 = fromTable;
			}
			else
			{
				entityType2 = EntityMappingConfiguration.FindBaseTableForExtraPropertyMapping(databaseMapping, entityType, pm);
				if (entityType2 == null)
				{
					entityType2 = fromTable;
				}
			}
			return entityType2;
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x0017E260 File Offset: 0x0017C460
		private static EntityType FindTableForExtraPropertyMapping(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, ref EntityType unmappedTable, ColumnMappingBuilder pm)
		{
			EntityType entityType2 = EntityMappingConfiguration.FindBaseTableForExtraPropertyMapping(databaseMapping, entityType, pm);
			if (entityType2 == null)
			{
				if (fromTable != toTable && entityType.BaseType == null)
				{
					return fromTable;
				}
				if (unmappedTable == null)
				{
					unmappedTable = databaseMapping.Database.AddTable(fromTable.Name, fromTable);
				}
				entityType2 = unmappedTable;
			}
			return entityType2;
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x0017E2E0 File Offset: 0x0017C4E0
		private static EntityType FindBaseTableForExtraPropertyMapping(DbDatabaseMapping databaseMapping, EntityType entityType, ColumnMappingBuilder pm)
		{
			EntityType entityType2 = (EntityType)entityType.BaseType;
			MappingFragment mappingFragment = null;
			while (entityType2 != null && mappingFragment == null)
			{
				EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType2);
				if (entityTypeMapping != null)
				{
					mappingFragment = entityTypeMapping.MappingFragments.SingleOrDefault((MappingFragment f) => f.ColumnMappings.Any((ColumnMappingBuilder bpm) => bpm.PropertyPath.SequenceEqual(pm.PropertyPath)));
					if (mappingFragment != null)
					{
						return mappingFragment.Table;
					}
				}
				entityType2 = (EntityType)entityType2.BaseType;
			}
			return null;
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x0017E374 File Offset: 0x0017C574
		private bool DiscoverIsSharingWithBase(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType toTable)
		{
			bool flag = false;
			if (entityType.BaseType != null)
			{
				EdmType baseType = entityType.BaseType;
				bool flag2 = false;
				while (baseType != null && !flag)
				{
					IList<EntityTypeMapping> entityTypeMappings = databaseMapping.GetEntityTypeMappings((EntityType)baseType);
					if (entityTypeMappings.Any<EntityTypeMapping>())
					{
						flag = entityTypeMappings.SelectMany((EntityTypeMapping m) => m.MappingFragments).Any((MappingFragment tmf) => tmf.Table == toTable);
						flag2 = true;
					}
					baseType = baseType.BaseType;
				}
				if (!flag2)
				{
					flag = (this.TableName == null || string.IsNullOrWhiteSpace(this.TableName.Name));
				}
			}
			return flag;
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x0017E430 File Offset: 0x0017C630
		private static EntityType FindParentTable(DbDatabaseMapping databaseMapping, EntityType fromTable, EntityTypeMapping entityTypeMapping, EntityType toTable, bool isMappingInheritedProperties, int configurationIndex, int configurationCount, out bool isSplitting)
		{
			EntityType entityType = null;
			isSplitting = false;
			if ((entityTypeMapping.UsesOtherTables(toTable) || configurationCount > 1) && configurationIndex != 0)
			{
				entityType = entityTypeMapping.GetPrimaryTable();
				isSplitting = true;
			}
			if (entityType == null && fromTable != toTable && !isMappingInheritedProperties)
			{
				EdmType baseType = entityTypeMapping.EntityType.BaseType;
				while (baseType != null && entityType == null)
				{
					EntityTypeMapping entityTypeMapping2 = databaseMapping.GetEntityTypeMappings((EntityType)baseType).FirstOrDefault<EntityTypeMapping>();
					if (entityTypeMapping2 != null)
					{
						entityType = entityTypeMapping2.GetPrimaryTable();
					}
					baseType = baseType.BaseType;
				}
			}
			return entityType;
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0017E500 File Offset: 0x0017C700
		private MappingFragment FindOrCreateTypeMappingFragment(DbDatabaseMapping databaseMapping, ref EntityTypeMapping entityTypeMapping, int configurationIndex, EntityType entityType, DbProviderManifest providerManifest)
		{
			if (entityTypeMapping == null)
			{
				new TableMappingGenerator(providerManifest).Generate(entityType, databaseMapping);
				entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType);
				configurationIndex = 0;
			}
			MappingFragment result;
			if (configurationIndex < entityTypeMapping.MappingFragments.Count)
			{
				result = entityTypeMapping.MappingFragments[configurationIndex];
			}
			else
			{
				if (this.MapInheritedProperties)
				{
					throw Error.EntityMappingConfiguration_DuplicateMapInheritedProperties(entityType.Name);
				}
				if (this.Properties == null)
				{
					throw Error.EntityMappingConfiguration_DuplicateMappedProperties(entityType.Name);
				}
				this.Properties.Each(delegate(PropertyPath p)
				{
					if (EntityMappingConfiguration.PropertyPathToEdmPropertyPath(p, entityType).Any((EdmPropertyPath pp) => !entityType.KeyProperties().Contains(pp.First<EdmProperty>())))
					{
						throw Error.EntityMappingConfiguration_DuplicateMappedProperty(entityType.Name, p.ToString());
					}
				});
				EntityType table = entityTypeMapping.MappingFragments[0].Table;
				EntityType entityType2 = databaseMapping.Database.AddTable(table.Name, table);
				result = EntityMappingOperations.CreateTypeMappingFragment(entityTypeMapping, entityTypeMapping.MappingFragments[0], databaseMapping.Database.GetEntitySet(entityType2));
			}
			return result;
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x0017E63C File Offset: 0x0017C83C
		private EntityType FindOrCreateTargetTable(DbDatabaseMapping databaseMapping, MappingFragment fragment, EntityType entityType, EntityType fromTable, out bool isTableSharing)
		{
			isTableSharing = false;
			EntityType entityType2;
			if (this.TableName == null)
			{
				entityType2 = fragment.Table;
			}
			else
			{
				entityType2 = databaseMapping.Database.FindTableByName(this.TableName);
				if (entityType2 == null)
				{
					if (entityType.BaseType == null)
					{
						entityType2 = fragment.Table;
					}
					else
					{
						entityType2 = databaseMapping.Database.AddTable(this.TableName.Name, fromTable);
					}
				}
				isTableSharing = EntityMappingConfiguration.UpdateColumnNamesForTableSharing(databaseMapping, entityType, entityType2, fragment);
				fragment.TableSet = databaseMapping.Database.GetEntitySet(entityType2);
				using (IEnumerator<ColumnMappingBuilder> enumerator = (from cm in fragment.ColumnMappings
				where cm.ColumnProperty.IsPrimaryKeyColumn
				select cm).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ColumnMappingBuilder columnMapping = enumerator.Current;
						EdmProperty edmProperty = entityType2.Properties.SingleOrDefault((EdmProperty c) => string.Equals(c.Name, columnMapping.ColumnProperty.Name, StringComparison.Ordinal));
						columnMapping.ColumnProperty = (edmProperty ?? columnMapping.ColumnProperty);
					}
				}
				entityType2.SetTableName(this.TableName);
			}
			return entityType2;
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x0017E7F8 File Offset: 0x0017C9F8
		private HashSet<EdmPropertyPath> DiscoverAllMappingsToContain(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType toTable, bool isSharingTableWithBase)
		{
			HashSet<EdmPropertyPath> mappingsToContain = new HashSet<EdmPropertyPath>();
			entityType.KeyProperties().Each(delegate(EdmProperty p)
			{
				mappingsToContain.AddRange(p.ToPropertyPathList());
			});
			if (this.MapInheritedProperties)
			{
				entityType.Properties.Except(entityType.DeclaredProperties).Each(delegate(EdmProperty p)
				{
					mappingsToContain.AddRange(p.ToPropertyPathList());
				});
			}
			if (isSharingTableWithBase)
			{
				HashSet<EdmPropertyPath> baseMappingsToContain = new HashSet<EdmPropertyPath>();
				EntityType entityType2 = (EntityType)entityType.BaseType;
				EntityTypeMapping entityTypeMapping = null;
				MappingFragment mappingFragment = null;
				while (entityType2 != null && entityTypeMapping == null)
				{
					entityTypeMapping = databaseMapping.GetEntityTypeMapping((EntityType)entityType.BaseType);
					if (entityTypeMapping != null)
					{
						mappingFragment = entityTypeMapping.MappingFragments.SingleOrDefault((MappingFragment tmf) => tmf.Table == toTable);
					}
					if (mappingFragment == null)
					{
						entityType2.DeclaredProperties.Each(delegate(EdmProperty p)
						{
							baseMappingsToContain.AddRange(p.ToPropertyPathList());
						});
					}
					entityType2 = (EntityType)entityType2.BaseType;
				}
				if (mappingFragment != null)
				{
					foreach (ColumnMappingBuilder columnMappingBuilder in mappingFragment.ColumnMappings)
					{
						mappingsToContain.Add(new EdmPropertyPath(columnMappingBuilder.PropertyPath));
					}
				}
				mappingsToContain.AddRange(baseMappingsToContain);
			}
			if (this.Properties == null)
			{
				entityType.DeclaredProperties.Each(delegate(EdmProperty p)
				{
					mappingsToContain.AddRange(p.ToPropertyPathList());
				});
			}
			else
			{
				this.Properties.Each(delegate(PropertyPath p)
				{
					mappingsToContain.AddRange(EntityMappingConfiguration.PropertyPathToEdmPropertyPath(p, entityType));
				});
			}
			return mappingsToContain;
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x0017EA04 File Offset: 0x0017CC04
		private void ConfigureConditions(DbDatabaseMapping databaseMapping, EntityType entityType, MappingFragment fragment, DbProviderManifest providerManifest)
		{
			if (this.ValueConditions.Any<ValueConditionConfiguration>() || this.NullabilityConditions.Any<NotNullConditionConfiguration>())
			{
				fragment.ClearConditions();
				foreach (ValueConditionConfiguration valueConditionConfiguration in this.ValueConditions)
				{
					valueConditionConfiguration.Configure(databaseMapping, fragment, entityType, providerManifest);
				}
				foreach (NotNullConditionConfiguration notNullConditionConfiguration in this.NullabilityConditions)
				{
					notNullConditionConfiguration.Configure(databaseMapping, fragment, entityType);
				}
			}
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x0017EBEC File Offset: 0x0017CDEC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		internal static void CleanupUnmappedArtifacts(DbDatabaseMapping databaseMapping, EntityType table)
		{
			AssociationSetMapping[] source = (from asm in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.AssociationSetMappings)
			where asm.Table == table
			select asm).ToArray<AssociationSetMapping>();
			MappingFragment[] source2 = (from f in databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings).SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).SelectMany((EntityTypeMapping etm) => etm.MappingFragments)
			where f.Table == table
			select f).ToArray<MappingFragment>();
			if (!source.Any<AssociationSetMapping>() && !source2.Any<MappingFragment>())
			{
				databaseMapping.Database.RemoveEntityType(table);
				(from t in databaseMapping.Database.AssociationTypes
				where t.SourceEnd.GetEntityType() == table || t.TargetEnd.GetEntityType() == table
				select t).ToArray<AssociationType>().Each(delegate(AssociationType t)
				{
					databaseMapping.Database.RemoveAssociationType(t);
				});
				return;
			}
			EdmProperty[] array = table.Properties.ToArray<EdmProperty>();
			for (int i = 0; i < array.Length; i++)
			{
				EdmProperty column = array[i];
				if (source2.SelectMany((MappingFragment f) => f.ColumnMappings).All((ColumnMappingBuilder pm) => pm.ColumnProperty != column))
				{
					if (source2.SelectMany((MappingFragment f) => f.ColumnConditions).All((ConditionPropertyMapping cc) => cc.Column != column))
					{
						if (source.SelectMany((AssociationSetMapping am) => am.SourceEndMapping.PropertyMappings).All((ScalarPropertyMapping pm) => pm.Column != column))
						{
							if (source.SelectMany((AssociationSetMapping am) => am.SourceEndMapping.PropertyMappings).All((ScalarPropertyMapping pm) => pm.Column != column))
							{
								ForeignKeyPrimitiveOperations.RemoveAllForeignKeyConstraintsForColumn(table, column, databaseMapping);
								TablePrimitiveOperations.RemoveColumn(table, column);
							}
						}
					}
				}
			}
			(from fk in table.ForeignKeyBuilders
			where fk.PrincipalTable == table && fk.DependentColumns.SequenceEqual(table.KeyProperties)
			select fk).ToArray<ForeignKeyBuilder>().Each(new Action<ForeignKeyBuilder>(table.RemoveForeignKey));
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x0017EF50 File Offset: 0x0017D150
		internal static IEnumerable<EdmPropertyPath> PropertyPathToEdmPropertyPath(PropertyPath path, EntityType entityType)
		{
			EntityMappingConfiguration.<>c__DisplayClass7c CS$<>8__locals1 = new EntityMappingConfiguration.<>c__DisplayClass7c();
			CS$<>8__locals1.path = path;
			List<EdmProperty> list = new List<EdmProperty>();
			StructuralType structuralType = entityType;
			int i;
			for (i = 0; i < CS$<>8__locals1.path.Count; i++)
			{
				EdmProperty edmProperty = structuralType.Members.OfType<EdmProperty>().SingleOrDefault((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(CS$<>8__locals1.path[i]));
				if (edmProperty == null)
				{
					throw Error.EntityMappingConfiguration_CannotMapIgnoredProperty(entityType.Name, CS$<>8__locals1.path.ToString());
				}
				list.Add(edmProperty);
				if (edmProperty.IsComplexType)
				{
					structuralType = edmProperty.ComplexType;
				}
			}
			EdmProperty edmProperty2 = list.Last<EdmProperty>();
			if (edmProperty2.IsUnderlyingPrimitiveType)
			{
				return new EdmPropertyPath[]
				{
					new EdmPropertyPath(list)
				};
			}
			if (edmProperty2.IsComplexType)
			{
				list.Remove(edmProperty2);
				return edmProperty2.ToPropertyPathList(list);
			}
			return new EdmPropertyPath[]
			{
				EdmPropertyPath.Empty
			};
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x0017F05C File Offset: 0x0017D25C
		private static List<EntityTypeMapping> FindAllTypeMappingsUsingTable(DbDatabaseMapping databaseMapping, EntityType toTable)
		{
			List<EntityTypeMapping> list = new List<EntityTypeMapping>();
			IList<EntityContainerMapping> entityContainerMappings = databaseMapping.EntityContainerMappings;
			for (int i = 0; i < entityContainerMappings.Count; i++)
			{
				List<EntitySetMapping> list2 = entityContainerMappings[i].EntitySetMappings.ToList<EntitySetMapping>();
				for (int j = 0; j < list2.Count; j++)
				{
					ReadOnlyCollection<EntityTypeMapping> entityTypeMappings = list2[j].EntityTypeMappings;
					for (int k = 0; k < entityTypeMappings.Count; k++)
					{
						EntityTypeMapping entityTypeMapping = entityTypeMappings[k];
						EntityTypeConfiguration entityTypeConfiguration = entityTypeMapping.EntityType.GetConfiguration() as EntityTypeConfiguration;
						for (int l = 0; l < entityTypeMapping.MappingFragments.Count; l++)
						{
							bool flag = entityTypeConfiguration != null && entityTypeConfiguration.IsTableNameConfigured;
							if ((!flag && entityTypeMapping.MappingFragments[l].Table == toTable) || (flag && EntityMappingConfiguration.IsTableNameEqual(toTable, entityTypeConfiguration.GetTableName())))
							{
								list.Add(entityTypeMapping);
								break;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x0017F168 File Offset: 0x0017D368
		private static bool IsTableNameEqual(EntityType table, DatabaseName otherTableName)
		{
			DatabaseName tableName = table.GetTableName();
			if (tableName != null)
			{
				return otherTableName.Equals(tableName);
			}
			return otherTableName.Name.Equals(table.Name, StringComparison.Ordinal) && otherTableName.Schema == null;
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x0017F1A8 File Offset: 0x0017D3A8
		private static IEnumerable<AssociationType> FindAllOneToOneFKAssociationTypes(EdmModel model, EntityType entityType, EntityType candidateType)
		{
			List<AssociationType> list = new List<AssociationType>();
			foreach (EntityContainer entityContainer in model.Containers)
			{
				ReadOnlyMetadataCollection<AssociationSet> associationSets = entityContainer.AssociationSets;
				for (int i = 0; i < associationSets.Count; i++)
				{
					AssociationSet associationSet = associationSets[i];
					AssociationEndMember sourceEnd = associationSet.ElementType.SourceEnd;
					AssociationEndMember targetEnd = associationSet.ElementType.TargetEnd;
					EntityType entityType2 = sourceEnd.GetEntityType();
					EntityType entityType3 = targetEnd.GetEntityType();
					if (associationSet.ElementType.Constraint != null && sourceEnd.RelationshipMultiplicity == RelationshipMultiplicity.One && targetEnd.RelationshipMultiplicity == RelationshipMultiplicity.One && ((entityType2 == entityType && entityType3 == candidateType) || (entityType3 == entityType && entityType2 == candidateType)))
					{
						list.Add(associationSet.ElementType);
					}
				}
			}
			return list;
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0017F2E8 File Offset: 0x0017D4E8
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private static bool UpdateColumnNamesForTableSharing(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType toTable, MappingFragment fragment)
		{
			List<EntityTypeMapping> list = EntityMappingConfiguration.FindAllTypeMappingsUsingTable(databaseMapping, toTable);
			Dictionary<EntityType, List<AssociationType>> dictionary = new Dictionary<EntityType, List<AssociationType>>();
			foreach (EntityTypeMapping entityTypeMapping in list)
			{
				EntityType entityType2 = entityTypeMapping.EntityType;
				if (entityType != entityType2)
				{
					IEnumerable<AssociationType> enumerable = EntityMappingConfiguration.FindAllOneToOneFKAssociationTypes(databaseMapping.Model, entityType, entityType2);
					EntityType rootType = entityType2.GetRootType();
					if (!dictionary.ContainsKey(rootType))
					{
						dictionary.Add(rootType, enumerable.ToList<AssociationType>());
					}
					else
					{
						dictionary[rootType].AddRange(enumerable);
					}
				}
			}
			List<EntityType> list2 = new List<EntityType>();
			foreach (KeyValuePair<EntityType, List<AssociationType>> keyValuePair in dictionary)
			{
				if (keyValuePair.Key != entityType.GetRootType() && keyValuePair.Value.Count == 0)
				{
					list2.Add(keyValuePair.Key);
				}
			}
			if (list2.Count > 0 && list2.Count == dictionary.Count)
			{
				DatabaseName tableName = toTable.GetTableName();
				throw Error.EntityMappingConfiguration_InvalidTableSharing(entityType.Name, list2.First<EntityType>().Name, (tableName != null) ? tableName.Name : databaseMapping.Database.GetEntitySet(toTable).Table);
			}
			IEnumerable<AssociationType> source = dictionary.Values.SelectMany((List<AssociationType> l) => l);
			if (source.Any<AssociationType>())
			{
				AssociationType associationType = source.First<AssociationType>();
				EntityType entityType3 = associationType.Constraint.FromRole.GetEntityType();
				EntityType dependentEntityType = (entityType == entityType3) ? associationType.Constraint.ToRole.GetEntityType() : entityType;
				MappingFragment mappingFragment;
				if (entityType != entityType3)
				{
					mappingFragment = fragment;
				}
				else
				{
					mappingFragment = list.Single((EntityTypeMapping etm) => etm.EntityType == dependentEntityType).Fragments.SingleOrDefault((MappingFragment mf) => mf.Table == toTable);
				}
				MappingFragment mappingFragment2 = mappingFragment;
				if (mappingFragment2 != null)
				{
					List<EdmProperty> list3 = entityType3.KeyProperties().ToList<EdmProperty>();
					List<EdmProperty> list4 = dependentEntityType.KeyProperties().ToList<EdmProperty>();
					for (int i = 0; i < list3.Count; i++)
					{
						EdmProperty dependentKey = list4[i];
						dependentKey.SetStoreGeneratedPattern(StoreGeneratedPattern.None);
						EdmProperty columnProperty = mappingFragment2.ColumnMappings.Single((ColumnMappingBuilder pm) => pm.PropertyPath.First<EdmProperty>() == dependentKey).ColumnProperty;
						columnProperty.Name = list3[i].Name;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04002396 RID: 9110
		private DatabaseName _tableName;

		// Token: 0x04002397 RID: 9111
		private List<PropertyPath> _properties;

		// Token: 0x04002398 RID: 9112
		private readonly List<ValueConditionConfiguration> _valueConditions = new List<ValueConditionConfiguration>();

		// Token: 0x04002399 RID: 9113
		private readonly List<NotNullConditionConfiguration> _notNullConditions = new List<NotNullConditionConfiguration>();

		// Token: 0x0400239A RID: 9114
		private readonly Dictionary<PropertyPath, PrimitivePropertyConfiguration> _primitivePropertyConfigurations = new Dictionary<PropertyPath, PrimitivePropertyConfiguration>();

		// Token: 0x0400239B RID: 9115
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();
	}
}
