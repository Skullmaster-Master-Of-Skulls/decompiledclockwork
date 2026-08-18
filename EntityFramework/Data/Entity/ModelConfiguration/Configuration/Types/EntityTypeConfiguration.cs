using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Types
{
	// Token: 0x020007E0 RID: 2016
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class EntityTypeConfiguration : StructuralTypeConfiguration
	{
		// Token: 0x06005BAE RID: 23470 RVA: 0x0018A494 File Offset: 0x00188694
		internal EntityTypeConfiguration(Type structuralType) : base(structuralType)
		{
			this.IsReplaceable = false;
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x0018A564 File Offset: 0x00188764
		private EntityTypeConfiguration(EntityTypeConfiguration source) : base(source)
		{
			this._keyProperties.AddRange(source._keyProperties);
			source._navigationPropertyConfigurations.Each(delegate(KeyValuePair<PropertyInfo, NavigationPropertyConfiguration> c)
			{
				this._navigationPropertyConfigurations.Add(c.Key, c.Value.Clone());
			});
			source._entitySubTypesMappingConfigurations.Each(delegate(KeyValuePair<Type, EntityMappingConfiguration> c)
			{
				this._entitySubTypesMappingConfigurations.Add(c.Key, c.Value.Clone());
			});
			this._entityMappingConfigurations.AddRange(from e in source._entityMappingConfigurations.Except(source._nonCloneableMappings)
			select e.Clone());
			this._isKeyConfigured = source._isKeyConfigured;
			this._entitySetName = source._entitySetName;
			if (source._modificationStoredProceduresConfiguration != null)
			{
				this._modificationStoredProceduresConfiguration = source._modificationStoredProceduresConfiguration.Clone();
			}
			this.IsReplaceable = source.IsReplaceable;
			this.IsTableNameConfigured = source.IsTableNameConfigured;
			this.IsExplicitEntity = source.IsExplicitEntity;
			foreach (KeyValuePair<string, object> item in source._annotations)
			{
				this._annotations.Add(item);
			}
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x0018A704 File Offset: 0x00188904
		internal virtual EntityTypeConfiguration Clone()
		{
			return new EntityTypeConfiguration(this);
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x0018A73C File Offset: 0x0018893C
		internal IEnumerable<Type> ConfiguredComplexTypes
		{
			get
			{
				return from pi in (from c in base.PrimitivePropertyConfigurations
				where c.Key.Count > 1
				select c.Key.Reverse<PropertyInfo>().Skip(1)).SelectMany((IEnumerable<PropertyInfo> p) => p)
				select pi.PropertyType;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x0018A7D8 File Offset: 0x001889D8
		internal bool IsStructuralConfigurationOnly
		{
			get
			{
				return !this._keyProperties.Any<PropertyInfo>() && !this._navigationPropertyConfigurations.Any<KeyValuePair<PropertyInfo, NavigationPropertyConfiguration>>() && !this._entityMappingConfigurations.Any<EntityMappingConfiguration>() && !this._entitySubTypesMappingConfigurations.Any<KeyValuePair<Type, EntityMappingConfiguration>>() && this._entitySetName == null;
			}
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x0018A824 File Offset: 0x00188A24
		internal override void RemoveProperty(PropertyPath propertyPath)
		{
			base.RemoveProperty(propertyPath);
			this._navigationPropertyConfigurations.Remove(propertyPath.Single<PropertyInfo>());
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005BB4 RID: 23476 RVA: 0x0018A83F File Offset: 0x00188A3F
		internal bool IsKeyConfigured
		{
			get
			{
				return this._isKeyConfigured;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005BB5 RID: 23477 RVA: 0x0018A847 File Offset: 0x00188A47
		internal IEnumerable<PropertyInfo> KeyProperties
		{
			get
			{
				return this._keyProperties;
			}
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x0018A850 File Offset: 0x00188A50
		internal virtual void Key(IEnumerable<PropertyInfo> keyProperties)
		{
			this.ClearKey();
			foreach (PropertyInfo propertyInfo in keyProperties)
			{
				this.Key(propertyInfo, new OverridableConfigurationParts?(OverridableConfigurationParts.None));
			}
			this._isKeyConfigured = true;
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x0018A8AC File Offset: 0x00188AAC
		public void Key(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			this.Key(propertyInfo, null);
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0018A8D8 File Offset: 0x00188AD8
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		internal virtual void Key(PropertyInfo propertyInfo, OverridableConfigurationParts? overridableConfigurationParts)
		{
			if (!propertyInfo.IsValidEdmScalarProperty())
			{
				throw Error.ModelBuilder_KeyPropertiesMustBePrimitive(propertyInfo.Name, base.ClrType);
			}
			if (!this._isKeyConfigured && !this._keyProperties.ContainsSame(propertyInfo))
			{
				this._keyProperties.Add(propertyInfo);
				base.Property(new PropertyPath(propertyInfo), overridableConfigurationParts);
			}
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x0018A92F File Offset: 0x00188B2F
		internal void ClearKey()
		{
			this._keyProperties.Clear();
			this._isKeyConfigured = false;
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06005BBA RID: 23482 RVA: 0x0018A943 File Offset: 0x00188B43
		// (set) Token: 0x06005BBB RID: 23483 RVA: 0x0018A94B File Offset: 0x00188B4B
		public bool IsTableNameConfigured { get; private set; }

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06005BBC RID: 23484 RVA: 0x0018A954 File Offset: 0x00188B54
		// (set) Token: 0x06005BBD RID: 23485 RVA: 0x0018A95C File Offset: 0x00188B5C
		internal bool IsReplaceable { get; set; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06005BBE RID: 23486 RVA: 0x0018A965 File Offset: 0x00188B65
		// (set) Token: 0x06005BBF RID: 23487 RVA: 0x0018A96D File Offset: 0x00188B6D
		internal bool IsExplicitEntity { get; set; }

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x0018A976 File Offset: 0x00188B76
		internal ModificationStoredProceduresConfiguration ModificationStoredProceduresConfiguration
		{
			get
			{
				return this._modificationStoredProceduresConfiguration;
			}
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0018A97E File Offset: 0x00188B7E
		internal virtual void MapToStoredProcedures()
		{
			if (this._modificationStoredProceduresConfiguration == null)
			{
				this._modificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration();
			}
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x0018A993 File Offset: 0x00188B93
		internal virtual void MapToStoredProcedures(ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration, bool allowOverride)
		{
			if (this._modificationStoredProceduresConfiguration == null)
			{
				this._modificationStoredProceduresConfiguration = modificationStoredProceduresConfiguration;
				return;
			}
			this._modificationStoredProceduresConfiguration.Merge(modificationStoredProceduresConfiguration, allowOverride);
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0018A9B2 File Offset: 0x00188BB2
		internal void ReplaceFrom(EntityTypeConfiguration existing)
		{
			if (this.EntitySetName == null)
			{
				this.EntitySetName = existing.EntitySetName;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005BC4 RID: 23492 RVA: 0x0018A9C8 File Offset: 0x00188BC8
		// (set) Token: 0x06005BC5 RID: 23493 RVA: 0x0018A9D0 File Offset: 0x00188BD0
		public virtual string EntitySetName
		{
			get
			{
				return this._entitySetName;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._entitySetName = value;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005BC6 RID: 23494 RVA: 0x0018A9E5 File Offset: 0x00188BE5
		internal override IEnumerable<PropertyInfo> ConfiguredProperties
		{
			get
			{
				return base.ConfiguredProperties.Union(this._navigationPropertyConfigurations.Keys);
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005BC7 RID: 23495 RVA: 0x0018A9FD File Offset: 0x00188BFD
		public string TableName
		{
			get
			{
				if (!this.IsTableNameConfigured)
				{
					return null;
				}
				return this.GetTableName().Name;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005BC8 RID: 23496 RVA: 0x0018AA14 File Offset: 0x00188C14
		public string SchemaName
		{
			get
			{
				if (!this.IsTableNameConfigured)
				{
					return null;
				}
				return this.GetTableName().Schema;
			}
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x0018AA2B File Offset: 0x00188C2B
		internal DatabaseName GetTableName()
		{
			if (!this.IsTableNameConfigured)
			{
				return null;
			}
			return this._entityMappingConfigurations.First<EntityMappingConfiguration>().TableName;
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x0018AA47 File Offset: 0x00188C47
		public void ToTable(string tableName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.ToTable(tableName, null);
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x0018AA60 File Offset: 0x00188C60
		public void ToTable(string tableName, string schemaName)
		{
			Check.NotEmpty(tableName, "tableName");
			this.IsTableNameConfigured = true;
			if (!this._entityMappingConfigurations.Any<EntityMappingConfiguration>())
			{
				this._entityMappingConfigurations.Add(new EntityMappingConfiguration());
			}
			this._entityMappingConfigurations.First<EntityMappingConfiguration>().TableName = (string.IsNullOrWhiteSpace(schemaName) ? new DatabaseName(tableName) : new DatabaseName(tableName, schemaName));
			this.UpdateTableNameForSubTypes();
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06005BCC RID: 23500 RVA: 0x0018AACA File Offset: 0x00188CCA
		public IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x0018AAD2 File Offset: 0x00188CD2
		public virtual void SetAnnotation(string name, object value)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x0018AB2C File Offset: 0x00188D2C
		private void UpdateTableNameForSubTypes()
		{
			(from stmc in this._entitySubTypesMappingConfigurations
			where stmc.Value.TableName == null
			select stmc into tphs
			select tphs.Value).Each((EntityMappingConfiguration tphmc) => tphmc.TableName = this.GetTableName());
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x0018ABBC File Offset: 0x00188DBC
		internal void AddMappingConfiguration(EntityMappingConfiguration mappingConfiguration, bool cloneable = true)
		{
			if (this._entityMappingConfigurations.Contains(mappingConfiguration))
			{
				return;
			}
			DatabaseName tableName = mappingConfiguration.TableName;
			if (tableName != null)
			{
				EntityMappingConfiguration entityMappingConfiguration = this._entityMappingConfigurations.SingleOrDefault((EntityMappingConfiguration mf) => tableName.Equals(mf.TableName));
				if (entityMappingConfiguration != null)
				{
					throw Error.InvalidTableMapping(base.ClrType.Name, tableName);
				}
			}
			this._entityMappingConfigurations.Add(mappingConfiguration);
			if (this._entityMappingConfigurations.Count > 1)
			{
				if (this._entityMappingConfigurations.Any((EntityMappingConfiguration mc) => mc.TableName == null))
				{
					throw Error.InvalidTableMapping_NoTableName(base.ClrType.Name);
				}
			}
			this.IsTableNameConfigured |= (tableName != null);
			if (!cloneable)
			{
				this._nonCloneableMappings.Add(mappingConfiguration);
			}
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x0018ACA8 File Offset: 0x00188EA8
		internal void AddSubTypeMappingConfiguration(Type subType, EntityMappingConfiguration mappingConfiguration)
		{
			EntityMappingConfiguration entityMappingConfiguration;
			if (this._entitySubTypesMappingConfigurations.TryGetValue(subType, out entityMappingConfiguration))
			{
				throw Error.InvalidChainedMappingSyntax(subType.Name);
			}
			this._entitySubTypesMappingConfigurations.Add(subType, mappingConfiguration);
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005BD1 RID: 23505 RVA: 0x0018ACDE File Offset: 0x00188EDE
		internal Dictionary<Type, EntityMappingConfiguration> SubTypeMappingConfigurations
		{
			get
			{
				return this._entitySubTypesMappingConfigurations;
			}
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x0018ACE8 File Offset: 0x00188EE8
		internal NavigationPropertyConfiguration Navigation(PropertyInfo propertyInfo)
		{
			NavigationPropertyConfiguration result;
			if (!this._navigationPropertyConfigurations.TryGetValue(propertyInfo, out result))
			{
				this._navigationPropertyConfigurations.Add(propertyInfo, result = new NavigationPropertyConfiguration(propertyInfo));
			}
			return result;
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x0018AD1A File Offset: 0x00188F1A
		internal virtual void Configure(EntityType entityType, EdmModel model)
		{
			this.ConfigureKey(entityType);
			base.Configure(entityType.Name, entityType.Properties, entityType.GetMetadataProperties());
			this.ConfigureAssociations(entityType, model);
			this.ConfigureEntitySetName(entityType, model);
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x0018AD4C File Offset: 0x00188F4C
		private void ConfigureEntitySetName(EntityType entityType, EdmModel model)
		{
			if (this.EntitySetName == null || entityType.BaseType != null)
			{
				return;
			}
			EntitySet entitySet = model.GetEntitySet(entityType);
			entitySet.Name = model.GetEntitySets().Except(new EntitySet[]
			{
				entitySet
			}).UniquifyName(this.EntitySetName);
			entitySet.SetConfiguration(this);
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0018AE04 File Offset: 0x00189004
		private void ConfigureKey(EntityType entityType)
		{
			if (!this._keyProperties.Any<PropertyInfo>())
			{
				return;
			}
			if (entityType.BaseType != null)
			{
				throw Error.KeyRegisteredOnDerivedType(base.ClrType, entityType.GetRootType().GetClrType());
			}
			IEnumerable<PropertyInfo> enumerable = this._keyProperties.AsEnumerable<PropertyInfo>();
			if (!this._isKeyConfigured)
			{
				var source = from p in this._keyProperties
				select new
				{
					PropertyInfo = p,
					ColumnOrder = base.Property(new PropertyPath(p), null).ColumnOrder
				};
				if (this._keyProperties.Count > 1)
				{
					if (source.Any(p => p.ColumnOrder == null))
					{
						throw Error.ModelGeneration_UnableToDetermineKeyOrder(base.ClrType);
					}
				}
				enumerable = from p in source
				orderby p.ColumnOrder
				select p.PropertyInfo;
			}
			foreach (PropertyInfo propertyInfo in enumerable)
			{
				EdmProperty declaredPrimitiveProperty = entityType.GetDeclaredPrimitiveProperty(propertyInfo);
				if (declaredPrimitiveProperty == null)
				{
					throw Error.KeyPropertyNotFound(propertyInfo.Name, entityType.Name);
				}
				declaredPrimitiveProperty.Nullable = false;
				entityType.AddKeyMember(declaredPrimitiveProperty);
			}
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x0018AF94 File Offset: 0x00189194
		private void ConfigureAssociations(EntityType entityType, EdmModel model)
		{
			foreach (KeyValuePair<PropertyInfo, NavigationPropertyConfiguration> keyValuePair in this._navigationPropertyConfigurations)
			{
				PropertyInfo propertyInfo = keyValuePair.Key;
				NavigationPropertyConfiguration value = keyValuePair.Value;
				NavigationProperty navigationProperty = entityType.GetNavigationProperty(propertyInfo);
				if (navigationProperty == null)
				{
					EdmProperty edmProperty = entityType.Properties.SingleOrDefault((EdmProperty p) => p.GetClrPropertyInfo() == propertyInfo);
					if (edmProperty != null && edmProperty.ComplexType != null)
					{
						throw new InvalidOperationException(Strings.InvalidNavigationPropertyComplexType(propertyInfo.Name, entityType.Name, edmProperty.ComplexType.Name));
					}
					throw Error.NavigationPropertyNotFound(propertyInfo.Name, entityType.Name);
				}
				else if (entityType.DeclaredNavigationProperties.Any((NavigationProperty np) => np.GetClrPropertyInfo().IsSameAs(propertyInfo)))
				{
					value.Configure(navigationProperty, model, this);
				}
			}
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x0018B0A8 File Offset: 0x001892A8
		internal void ConfigureTablesAndConditions(EntityTypeMapping entityTypeMapping, DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest)
		{
			EntityType entityType = (entityTypeMapping != null) ? entityTypeMapping.EntityType : databaseMapping.Model.GetEntityType(base.ClrType);
			if (this._entityMappingConfigurations.Any<EntityMappingConfiguration>())
			{
				for (int i = 0; i < this._entityMappingConfigurations.Count; i++)
				{
					this._entityMappingConfigurations[i].Configure(databaseMapping, entitySets, providerManifest, entityType, ref entityTypeMapping, this.IsMappingAnyInheritedProperty(entityType), i, this._entityMappingConfigurations.Count, this._annotations);
				}
				return;
			}
			EntityTypeConfiguration.ConfigureUnconfiguredType(databaseMapping, entitySets, providerManifest, entityType, this._annotations);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x0018B150 File Offset: 0x00189350
		internal bool IsMappingAnyInheritedProperty(EntityType entityType)
		{
			return this._entityMappingConfigurations.Any((EntityMappingConfiguration emc) => emc.MapsAnyInheritedProperties(entityType));
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x0018B181 File Offset: 0x00189381
		internal bool IsNavigationPropertyConfigured(PropertyInfo propertyInfo)
		{
			return this._navigationPropertyConfigurations.ContainsKey(propertyInfo);
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x0018B190 File Offset: 0x00189390
		internal static void ConfigureUnconfiguredType(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest, EntityType entityType, IDictionary<string, object> commonAnnotations)
		{
			EntityMappingConfiguration entityMappingConfiguration = new EntityMappingConfiguration();
			EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType.GetClrType());
			entityMappingConfiguration.Configure(databaseMapping, entitySets, providerManifest, entityType, ref entityTypeMapping, false, 0, 1, commonAnnotations);
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x0018B1C4 File Offset: 0x001893C4
		internal void Configure(EntityType entityType, DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType.GetClrType());
			if (entityTypeMapping != null)
			{
				EntityTypeConfiguration.VerifyAllCSpacePropertiesAreMapped(databaseMapping.GetEntityTypeMappings(entityType).ToList<EntityTypeMapping>(), entityTypeMapping.EntityType.DeclaredProperties, new List<EdmProperty>());
			}
			this.ConfigurePropertyMappings(databaseMapping, entityType, providerManifest, false);
			this.ConfigureAssociationMappings(databaseMapping, entityType, providerManifest);
			EntityTypeConfiguration.ConfigureDependentKeys(databaseMapping, providerManifest);
			this.ConfigureModificationStoredProcedures(databaseMapping, entityType, providerManifest);
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x0018B38C File Offset: 0x0018958C
		internal void ConfigureFunctionParameters(DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			List<ModificationFunctionParameterBinding> parameterBindings = (from esm in databaseMapping.GetEntitySetMappings()
			from mfm in esm.ModificationFunctionMappings
			where mfm.EntityType == entityType
			from pb in mfm.PrimaryParameterBindings
			select pb).ToList<ModificationFunctionParameterBinding>();
			base.ConfigureFunctionParameters(parameterBindings);
			foreach (EntityType entityType2 in from et in databaseMapping.Model.EntityTypes
			where et.BaseType == entityType
			select et)
			{
				this.ConfigureFunctionParameters(databaseMapping, entityType2);
			}
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x0018B4C0 File Offset: 0x001896C0
		private void ConfigureModificationStoredProcedures(DbDatabaseMapping databaseMapping, EntityType entityType, DbProviderManifest providerManifest)
		{
			if (this._modificationStoredProceduresConfiguration != null)
			{
				new ModificationFunctionMappingGenerator(providerManifest).Generate(entityType, databaseMapping);
				EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping = databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping esm) => esm.ModificationFunctionMappings).SingleOrDefault((EntityTypeModificationFunctionMapping mfm) => mfm.EntityType == entityType);
				if (entityTypeModificationFunctionMapping != null)
				{
					this._modificationStoredProceduresConfiguration.Configure(entityTypeModificationFunctionMapping, providerManifest);
				}
			}
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x0018B8D8 File Offset: 0x00189AD8
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void ConfigurePropertyMappings(DbDatabaseMapping databaseMapping, EntityType entityType, DbProviderManifest providerManifest, bool allowOverride = false)
		{
			IList<EntityTypeMapping> entityTypeMappings = databaseMapping.GetEntityTypeMappings(entityType);
			List<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings = (from etm in entityTypeMappings
			from etmf in etm.MappingFragments
			from pm in etmf.ColumnMappings
			select Tuple.Create<ColumnMappingBuilder, EntityType>(pm, etmf.Table)).ToList<Tuple<ColumnMappingBuilder, EntityType>>();
			base.ConfigurePropertyMappings(propertyMappings, providerManifest, allowOverride);
			this._entityMappingConfigurations.Each(delegate(EntityMappingConfiguration c)
			{
				c.ConfigurePropertyMappings(propertyMappings, providerManifest, allowOverride);
			});
			List<Tuple<ColumnMappingBuilder, EntityType>> inheritedPropertyMappings = (from esm in databaseMapping.GetEntitySetMappings()
			from etm in esm.EntityTypeMappings
			where etm.IsHierarchyMapping && etm.EntityType.IsAncestorOf(entityType)
			from etmf in etm.MappingFragments
			from pm1 in etmf.ColumnMappings
			where !propertyMappings.Any((Tuple<ColumnMappingBuilder, EntityType> pm2) => pm2.Item1.PropertyPath.SequenceEqual(pm1.PropertyPath))
			select Tuple.Create<ColumnMappingBuilder, EntityType>(pm1, etmf.Table)).ToList<Tuple<ColumnMappingBuilder, EntityType>>();
			base.ConfigurePropertyMappings(inheritedPropertyMappings, providerManifest, false);
			this._entityMappingConfigurations.Each(delegate(EntityMappingConfiguration c)
			{
				c.ConfigurePropertyMappings(inheritedPropertyMappings, providerManifest, false);
			});
			foreach (EntityType entityType2 in from et in databaseMapping.Model.EntityTypes
			where et.BaseType == entityType
			select et)
			{
				this.ConfigurePropertyMappings(databaseMapping, entityType2, providerManifest, true);
			}
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x0018BB88 File Offset: 0x00189D88
		private void ConfigureAssociationMappings(DbDatabaseMapping databaseMapping, EntityType entityType, DbProviderManifest providerManifest)
		{
			foreach (KeyValuePair<PropertyInfo, NavigationPropertyConfiguration> keyValuePair in this._navigationPropertyConfigurations)
			{
				PropertyInfo key = keyValuePair.Key;
				NavigationPropertyConfiguration value = keyValuePair.Value;
				NavigationProperty navigationProperty = entityType.GetNavigationProperty(key);
				if (navigationProperty == null)
				{
					throw Error.NavigationPropertyNotFound(key.Name, entityType.Name);
				}
				AssociationSetMapping associationSetMapping = databaseMapping.GetAssociationSetMappings().SingleOrDefault((AssociationSetMapping asm) => asm.AssociationSet.ElementType == navigationProperty.Association);
				if (associationSetMapping != null)
				{
					value.Configure(associationSetMapping, databaseMapping, providerManifest);
				}
			}
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x0018BC3C File Offset: 0x00189E3C
		private static void ConfigureDependentKeys(DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			IList<EntityType> list = (databaseMapping.Database.EntityTypes as IList<EntityType>) ?? databaseMapping.Database.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				EntityType entityType = list[i];
				IList<ForeignKeyBuilder> list2 = (entityType.ForeignKeyBuilders as IList<ForeignKeyBuilder>) ?? entityType.ForeignKeyBuilders.ToList<ForeignKeyBuilder>();
				for (int j = 0; j < list2.Count; j++)
				{
					ForeignKeyBuilder foreignKeyBuilder = list2[j];
					IEnumerable<EdmProperty> dependentColumns = foreignKeyBuilder.DependentColumns;
					IList<EdmProperty> list3 = (dependentColumns as IList<EdmProperty>) ?? dependentColumns.ToList<EdmProperty>();
					for (int k = 0; k < list3.Count; k++)
					{
						EdmProperty edmProperty = list3[k];
						PrimitivePropertyConfiguration primitivePropertyConfiguration = edmProperty.GetConfiguration() as PrimitivePropertyConfiguration;
						if (primitivePropertyConfiguration == null || primitivePropertyConfiguration.ColumnType == null)
						{
							EdmProperty edmProperty2 = foreignKeyBuilder.PrincipalTable.KeyProperties.ElementAt(k);
							edmProperty.PrimitiveType = providerManifest.GetStoreTypeFromName(edmProperty2.TypeName);
							edmProperty.CopyFrom(edmProperty2);
						}
					}
				}
			}
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x0018BD84 File Offset: 0x00189F84
		private static void VerifyAllCSpacePropertiesAreMapped(ICollection<EntityTypeMapping> entityTypeMappings, IEnumerable<EdmProperty> properties, IList<EdmProperty> propertyPath)
		{
			EntityType entityType = entityTypeMappings.First<EntityTypeMapping>().EntityType;
			foreach (EdmProperty edmProperty in properties)
			{
				propertyPath.Add(edmProperty);
				if (edmProperty.IsComplexType)
				{
					EntityTypeConfiguration.VerifyAllCSpacePropertiesAreMapped(entityTypeMappings, edmProperty.ComplexType.Properties, propertyPath);
				}
				else if (!entityTypeMappings.SelectMany((EntityTypeMapping etm) => etm.MappingFragments).SelectMany((MappingFragment mf) => mf.ColumnMappings).Any((ColumnMappingBuilder pm) => pm.PropertyPath.SequenceEqual(propertyPath)) && !entityType.Abstract)
				{
					throw Error.InvalidEntitySplittingProperties(entityType.Name);
				}
				propertyPath.Remove(edmProperty);
			}
		}

		// Token: 0x04002456 RID: 9302
		private readonly List<PropertyInfo> _keyProperties = new List<PropertyInfo>();

		// Token: 0x04002457 RID: 9303
		private readonly Dictionary<PropertyInfo, NavigationPropertyConfiguration> _navigationPropertyConfigurations = new Dictionary<PropertyInfo, NavigationPropertyConfiguration>(new DynamicEqualityComparer<PropertyInfo>((PropertyInfo p1, PropertyInfo p2) => p1.IsSameAs(p2)));

		// Token: 0x04002458 RID: 9304
		private readonly List<EntityMappingConfiguration> _entityMappingConfigurations = new List<EntityMappingConfiguration>();

		// Token: 0x04002459 RID: 9305
		private readonly Dictionary<Type, EntityMappingConfiguration> _entitySubTypesMappingConfigurations = new Dictionary<Type, EntityMappingConfiguration>();

		// Token: 0x0400245A RID: 9306
		private readonly List<EntityMappingConfiguration> _nonCloneableMappings = new List<EntityMappingConfiguration>();

		// Token: 0x0400245B RID: 9307
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();

		// Token: 0x0400245C RID: 9308
		private bool _isKeyConfigured;

		// Token: 0x0400245D RID: 9309
		private string _entitySetName;

		// Token: 0x0400245E RID: 9310
		private ModificationStoredProceduresConfiguration _modificationStoredProceduresConfiguration;
	}
}
