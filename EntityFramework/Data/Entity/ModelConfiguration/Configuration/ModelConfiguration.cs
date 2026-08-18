using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Mapping;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007C1 RID: 1985
	[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	internal class ModelConfiguration : ConfigurationBase
	{
		// Token: 0x060059E0 RID: 23008 RVA: 0x00183956 File Offset: 0x00181B56
		internal ModelConfiguration()
		{
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x001839C0 File Offset: 0x00181BC0
		private ModelConfiguration(ModelConfiguration source)
		{
			source._entityConfigurations.Each(delegate(KeyValuePair<Type, EntityTypeConfiguration> c)
			{
				this._entityConfigurations.Add(c.Key, c.Value.Clone());
			});
			source._complexTypeConfigurations.Each(delegate(KeyValuePair<Type, ComplexTypeConfiguration> c)
			{
				this._complexTypeConfigurations.Add(c.Key, c.Value.Clone());
			});
			this._ignoredTypes.AddRange(source._ignoredTypes);
			this.DefaultSchema = source.DefaultSchema;
			this.ModelNamespace = source.ModelNamespace;
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x00183A59 File Offset: 0x00181C59
		internal virtual ModelConfiguration Clone()
		{
			return new ModelConfiguration(this);
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060059E3 RID: 23011 RVA: 0x00183A61 File Offset: 0x00181C61
		public virtual IEnumerable<Type> ConfiguredTypes
		{
			get
			{
				return this._entityConfigurations.Keys.Union(this._complexTypeConfigurations.Keys).Union(this._ignoredTypes);
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060059E4 RID: 23012 RVA: 0x00183A89 File Offset: 0x00181C89
		internal virtual IEnumerable<Type> Entities
		{
			get
			{
				return this._entityConfigurations.Keys.Except(this._ignoredTypes).ToList<Type>();
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060059E5 RID: 23013 RVA: 0x00183AA6 File Offset: 0x00181CA6
		internal virtual IEnumerable<Type> ComplexTypes
		{
			get
			{
				return this._complexTypeConfigurations.Keys.Except(this._ignoredTypes).ToList<Type>();
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060059E6 RID: 23014 RVA: 0x00183AC3 File Offset: 0x00181CC3
		internal virtual IEnumerable<Type> StructuralTypes
		{
			get
			{
				return this._entityConfigurations.Keys.Union(this._complexTypeConfigurations.Keys).Except(this._ignoredTypes).ToList<Type>();
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060059E7 RID: 23015 RVA: 0x00183AF0 File Offset: 0x00181CF0
		// (set) Token: 0x060059E8 RID: 23016 RVA: 0x00183AF8 File Offset: 0x00181CF8
		public string DefaultSchema { get; set; }

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x00183B01 File Offset: 0x00181D01
		// (set) Token: 0x060059EA RID: 23018 RVA: 0x00183B09 File Offset: 0x00181D09
		public string ModelNamespace { get; set; }

		// Token: 0x060059EB RID: 23019 RVA: 0x00183B14 File Offset: 0x00181D14
		internal virtual void Add(EntityTypeConfiguration entityTypeConfiguration)
		{
			EntityTypeConfiguration entityTypeConfiguration2;
			if ((this._entityConfigurations.TryGetValue(entityTypeConfiguration.ClrType, out entityTypeConfiguration2) && !entityTypeConfiguration2.IsReplaceable) || this._complexTypeConfigurations.ContainsKey(entityTypeConfiguration.ClrType))
			{
				throw Error.DuplicateStructuralTypeConfiguration(entityTypeConfiguration.ClrType);
			}
			if (entityTypeConfiguration2 != null && entityTypeConfiguration2.IsReplaceable)
			{
				this._entityConfigurations.Remove(entityTypeConfiguration2.ClrType);
				entityTypeConfiguration.ReplaceFrom(entityTypeConfiguration2);
			}
			else
			{
				entityTypeConfiguration.IsReplaceable = false;
			}
			this._entityConfigurations.Add(entityTypeConfiguration.ClrType, entityTypeConfiguration);
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x00183B9C File Offset: 0x00181D9C
		internal virtual void Add(ComplexTypeConfiguration complexTypeConfiguration)
		{
			if (this._entityConfigurations.ContainsKey(complexTypeConfiguration.ClrType) || this._complexTypeConfigurations.ContainsKey(complexTypeConfiguration.ClrType))
			{
				throw Error.DuplicateStructuralTypeConfiguration(complexTypeConfiguration.ClrType);
			}
			this._complexTypeConfigurations.Add(complexTypeConfiguration.ClrType, complexTypeConfiguration);
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x00183BED File Offset: 0x00181DED
		public virtual EntityTypeConfiguration Entity(Type entityType)
		{
			Check.NotNull<Type>(entityType, "entityType");
			return this.Entity(entityType, false);
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x00183C04 File Offset: 0x00181E04
		internal virtual EntityTypeConfiguration Entity(Type entityType, bool explicitEntity)
		{
			if (this._complexTypeConfigurations.ContainsKey(entityType))
			{
				throw Error.EntityTypeConfigurationMismatch(entityType.Name);
			}
			EntityTypeConfiguration result;
			if (!this._entityConfigurations.TryGetValue(entityType, out result))
			{
				this._entityConfigurations.Add(entityType, result = new EntityTypeConfiguration(entityType)
				{
					IsExplicitEntity = explicitEntity
				});
			}
			return result;
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x00183C5C File Offset: 0x00181E5C
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#")]
		public virtual ComplexTypeConfiguration ComplexType(Type complexType)
		{
			Check.NotNull<Type>(complexType, "complexType");
			if (this._entityConfigurations.ContainsKey(complexType))
			{
				throw Error.ComplexTypeConfigurationMismatch(complexType.Name);
			}
			ComplexTypeConfiguration result;
			if (!this._complexTypeConfigurations.TryGetValue(complexType, out result))
			{
				this._complexTypeConfigurations.Add(complexType, result = new ComplexTypeConfiguration(complexType));
			}
			return result;
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x00183CB4 File Offset: 0x00181EB4
		public virtual void Ignore(Type type)
		{
			Check.NotNull<Type>(type, "type");
			this._ignoredTypes.Add(type);
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x00183CD0 File Offset: 0x00181ED0
		internal virtual StructuralTypeConfiguration GetStructuralTypeConfiguration(Type type)
		{
			EntityTypeConfiguration result;
			if (this._entityConfigurations.TryGetValue(type, out result))
			{
				return result;
			}
			ComplexTypeConfiguration result2;
			if (this._complexTypeConfigurations.TryGetValue(type, out result2))
			{
				return result2;
			}
			return null;
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x00183D02 File Offset: 0x00181F02
		public virtual bool IsComplexType(Type type)
		{
			Check.NotNull<Type>(type, "type");
			return this._complexTypeConfigurations.ContainsKey(type);
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x00183D1C File Offset: 0x00181F1C
		public virtual bool IsIgnoredType(Type type)
		{
			Check.NotNull<Type>(type, "type");
			return this._ignoredTypes.Contains(type);
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x00183D38 File Offset: 0x00181F38
		public virtual IEnumerable<PropertyInfo> GetConfiguredProperties(Type type)
		{
			Check.NotNull<Type>(type, "type");
			StructuralTypeConfiguration structuralTypeConfiguration = this.GetStructuralTypeConfiguration(type);
			if (structuralTypeConfiguration == null)
			{
				return Enumerable.Empty<PropertyInfo>();
			}
			return structuralTypeConfiguration.ConfiguredProperties;
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00183D80 File Offset: 0x00181F80
		public virtual bool IsIgnoredProperty(Type type, PropertyInfo propertyInfo)
		{
			Check.NotNull<Type>(type, "type");
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			while (type != null)
			{
				StructuralTypeConfiguration structuralTypeConfiguration = this.GetStructuralTypeConfiguration(type);
				if (structuralTypeConfiguration != null)
				{
					if (structuralTypeConfiguration.IgnoredProperties.Any((PropertyInfo p) => p.IsSameAs(propertyInfo)))
					{
						return true;
					}
				}
				if (propertyInfo.DeclaringType == type)
				{
					break;
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x00183E0B File Offset: 0x0018200B
		internal void Configure(EdmModel model)
		{
			this.ConfigureEntities(model);
			this.ConfigureComplexTypes(model);
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x00183E1C File Offset: 0x0018201C
		private void ConfigureEntities(EdmModel model)
		{
			foreach (EntityTypeConfiguration entityTypeConfiguration in this.ActiveEntityConfigurations)
			{
				this.ConfigureFunctionMappings(model, entityTypeConfiguration, model.GetEntityType(entityTypeConfiguration.ClrType));
			}
			foreach (EntityTypeConfiguration entityTypeConfiguration2 in this.ActiveEntityConfigurations)
			{
				entityTypeConfiguration2.Configure(model.GetEntityType(entityTypeConfiguration2.ClrType), model);
			}
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x00183EE8 File Offset: 0x001820E8
		private void ConfigureFunctionMappings(EdmModel model, EntityTypeConfiguration entityTypeConfiguration, EntityType entityType)
		{
			if (entityTypeConfiguration.ModificationStoredProceduresConfiguration == null)
			{
				return;
			}
			while (entityType.BaseType != null)
			{
				Type clrType = ((EntityType)entityType.BaseType).GetClrType();
				EntityTypeConfiguration entityTypeConfiguration2;
				if (!entityType.BaseType.Abstract && (!this._entityConfigurations.TryGetValue(clrType, out entityTypeConfiguration2) || entityTypeConfiguration2.ModificationStoredProceduresConfiguration == null))
				{
					throw Error.BaseTypeNotMappedToFunctions(clrType.Name, entityTypeConfiguration.ClrType.Name);
				}
				entityType = (EntityType)entityType.BaseType;
			}
			model.GetSelfAndAllDerivedTypes(entityType).Each(delegate(EntityType e)
			{
				EntityTypeConfiguration entityTypeConfiguration3 = this.Entity(e.GetClrType());
				if (entityTypeConfiguration3.ModificationStoredProceduresConfiguration == null)
				{
					entityTypeConfiguration3.MapToStoredProcedures();
				}
			});
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x00183F78 File Offset: 0x00182178
		private void ConfigureComplexTypes(EdmModel model)
		{
			foreach (ComplexTypeConfiguration complexTypeConfiguration in this.ActiveComplexTypeConfigurations)
			{
				ComplexType complexType = model.GetComplexType(complexTypeConfiguration.ClrType);
				complexTypeConfiguration.Configure(complexType);
			}
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x00183FE8 File Offset: 0x001821E8
		internal void Configure(DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in from StructuralTypeConfiguration c in 
				from ct in databaseMapping.Model.ComplexTypes
				select ct.GetConfiguration()
			where c != null
			select c)
			{
				structuralTypeConfiguration.ConfigurePropertyMappings(databaseMapping.GetComplexPropertyMappings(structuralTypeConfiguration.ClrType).ToList<Tuple<ColumnMappingBuilder, EntityType>>(), providerManifest, false);
			}
			this.ConfigureEntityTypes(databaseMapping, databaseMapping.Model.Container.EntitySets, providerManifest);
			ModelConfiguration.RemoveRedundantColumnConditions(databaseMapping);
			ModelConfiguration.RemoveRedundantTables(databaseMapping);
			ModelConfiguration.ConfigureTables(databaseMapping.Database);
			this.ConfigureDefaultSchema(databaseMapping);
			ModelConfiguration.UniquifyFunctionNames(databaseMapping);
			ModelConfiguration.ConfigureFunctionParameters(databaseMapping);
			ModelConfiguration.RemoveDuplicateTphColumns(databaseMapping);
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x00184100 File Offset: 0x00182300
		private static void ConfigureFunctionParameters(DbDatabaseMapping databaseMapping)
		{
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in from StructuralTypeConfiguration c in 
				from ct in databaseMapping.Model.ComplexTypes
				select ct.GetConfiguration()
			where c != null
			select c)
			{
				structuralTypeConfiguration.ConfigureFunctionParameters(databaseMapping.GetComplexParameterBindings(structuralTypeConfiguration.ClrType).ToList<ModificationFunctionParameterBinding>());
			}
			foreach (EntityType entityType in from e in databaseMapping.Model.EntityTypes
			where e.GetConfiguration() != null
			select e)
			{
				EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)entityType.GetConfiguration();
				entityTypeConfiguration.ConfigureFunctionParameters(databaseMapping, entityType);
			}
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x0018423C File Offset: 0x0018243C
		private static void UniquifyFunctionNames(DbDatabaseMapping databaseMapping)
		{
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping esm) => esm.ModificationFunctionMappings))
			{
				EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)entityTypeModificationFunctionMapping.EntityType.GetConfiguration();
				if (entityTypeConfiguration.ModificationStoredProceduresConfiguration != null)
				{
					ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration = entityTypeConfiguration.ModificationStoredProceduresConfiguration;
					ModelConfiguration.UniquifyFunctionName(databaseMapping, modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration, entityTypeModificationFunctionMapping.InsertFunctionMapping);
					ModelConfiguration.UniquifyFunctionName(databaseMapping, modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration, entityTypeModificationFunctionMapping.UpdateFunctionMapping);
					ModelConfiguration.UniquifyFunctionName(databaseMapping, modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration, entityTypeModificationFunctionMapping.DeleteFunctionMapping);
				}
			}
			foreach (AssociationSetModificationFunctionMapping associationSetModificationFunctionMapping in from asm in databaseMapping.GetAssociationSetMappings()
			select asm.ModificationFunctionMapping into asm
			where asm != null
			select asm)
			{
				NavigationPropertyConfiguration navigationPropertyConfiguration = (NavigationPropertyConfiguration)associationSetModificationFunctionMapping.AssociationSet.ElementType.GetConfiguration();
				if (navigationPropertyConfiguration.ModificationStoredProceduresConfiguration != null)
				{
					ModelConfiguration.UniquifyFunctionName(databaseMapping, navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration, associationSetModificationFunctionMapping.InsertFunctionMapping);
					ModelConfiguration.UniquifyFunctionName(databaseMapping, navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration, associationSetModificationFunctionMapping.DeleteFunctionMapping);
				}
			}
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x001843D8 File Offset: 0x001825D8
		private static void UniquifyFunctionName(DbDatabaseMapping databaseMapping, ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration, ModificationFunctionMapping functionMapping)
		{
			if (modificationStoredProcedureConfiguration == null || string.IsNullOrWhiteSpace(modificationStoredProcedureConfiguration.Name))
			{
				functionMapping.Function.StoreFunctionNameAttribute = (from f in databaseMapping.Database.Functions.Except(new EdmFunction[]
				{
					functionMapping.Function
				})
				select f.FunctionName).Uniquify(functionMapping.Function.FunctionName);
			}
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x001844BC File Offset: 0x001826BC
		private void ConfigureDefaultSchema(DbDatabaseMapping databaseMapping)
		{
			(from es in databaseMapping.Database.GetEntitySets()
			where string.IsNullOrWhiteSpace(es.Schema)
			select es).Each((EntitySet es) => es.Schema = (this.DefaultSchema ?? "dbo"));
			(from f in databaseMapping.Database.Functions
			where string.IsNullOrWhiteSpace(f.Schema)
			select f).Each((EdmFunction f) => f.Schema = (this.DefaultSchema ?? "dbo"));
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x00184554 File Offset: 0x00182754
		private void ConfigureEntityTypes(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest)
		{
			IList<EntityTypeConfiguration> list = this.SortEntityConfigurationsByInheritance(databaseMapping);
			foreach (EntityTypeConfiguration entityTypeConfiguration in list)
			{
				EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityTypeConfiguration.ClrType);
				entityTypeConfiguration.ConfigureTablesAndConditions(entityTypeMapping, databaseMapping, entitySets, providerManifest);
				ModelConfiguration.ConfigureUnconfiguredDerivedTypes(databaseMapping, entitySets, providerManifest, databaseMapping.Model.GetEntityType(entityTypeConfiguration.ClrType), list);
			}
			new EntityMappingService(databaseMapping).Configure();
			foreach (EntityType entityType in from e in databaseMapping.Model.EntityTypes
			where e.GetConfiguration() != null
			select e)
			{
				EntityTypeConfiguration entityTypeConfiguration2 = (EntityTypeConfiguration)entityType.GetConfiguration();
				entityTypeConfiguration2.Configure(entityType, databaseMapping, providerManifest);
			}
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x00184678 File Offset: 0x00182878
		private static void ConfigureUnconfiguredDerivedTypes(DbDatabaseMapping databaseMapping, ICollection<EntitySet> entitySets, DbProviderManifest providerManifest, EntityType entityType, IList<EntityTypeConfiguration> sortedEntityConfigurations)
		{
			List<EntityType> list = databaseMapping.Model.GetDerivedTypes(entityType).ToList<EntityType>();
			while (list.Count > 0)
			{
				EntityType currentType = list[0];
				list.RemoveAt(0);
				if (!currentType.Abstract && sortedEntityConfigurations.All((EntityTypeConfiguration etc) => etc.ClrType != currentType.GetClrType()))
				{
					EntityTypeConfiguration.ConfigureUnconfiguredType(databaseMapping, entitySets, providerManifest, currentType, new Dictionary<string, object>());
					list.AddRange(databaseMapping.Model.GetDerivedTypes(currentType));
				}
			}
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x00184708 File Offset: 0x00182908
		private static void ConfigureTables(EdmModel database)
		{
			foreach (EntityType table in database.EntityTypes.ToList<EntityType>())
			{
				ModelConfiguration.ConfigureTable(database, table);
			}
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x00184760 File Offset: 0x00182960
		private static void ConfigureTable(EdmModel database, EntityType table)
		{
			DatabaseName tableName = table.GetTableName();
			if (tableName == null)
			{
				return;
			}
			EntitySet entitySet = database.GetEntitySet(table);
			if (!string.IsNullOrWhiteSpace(tableName.Schema))
			{
				entitySet.Schema = tableName.Schema;
			}
			entitySet.Table = tableName.Name;
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x001847C8 File Offset: 0x001829C8
		private IList<EntityTypeConfiguration> SortEntityConfigurationsByInheritance(DbDatabaseMapping databaseMapping)
		{
			List<EntityTypeConfiguration> list = new List<EntityTypeConfiguration>();
			foreach (EntityTypeConfiguration entityTypeConfiguration in this.ActiveEntityConfigurations)
			{
				EntityType entityType = databaseMapping.Model.GetEntityType(entityTypeConfiguration.ClrType);
				if (entityType != null)
				{
					if (entityType.BaseType == null)
					{
						if (!list.Contains(entityTypeConfiguration))
						{
							list.Add(entityTypeConfiguration);
						}
					}
					else
					{
						Stack<EntityType> stack = new Stack<EntityType>();
						while (entityType != null)
						{
							stack.Push(entityType);
							entityType = (EntityType)entityType.BaseType;
						}
						while (stack.Count > 0)
						{
							entityType = stack.Pop();
							EntityTypeConfiguration entityTypeConfiguration2 = this.ActiveEntityConfigurations.SingleOrDefault((EntityTypeConfiguration ec) => ec.ClrType == entityType.GetClrType());
							if (entityTypeConfiguration2 != null && !list.Contains(entityTypeConfiguration2))
							{
								list.Add(entityTypeConfiguration2);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x001848F8 File Offset: 0x00182AF8
		internal void NormalizeConfigurations()
		{
			this.DiscoverIndirectlyConfiguredComplexTypes();
			this.ReassignSubtypeMappings();
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x00184917 File Offset: 0x00182B17
		private void DiscoverIndirectlyConfiguredComplexTypes()
		{
			this.ActiveEntityConfigurations.SelectMany((EntityTypeConfiguration ec) => ec.ConfiguredComplexTypes).Each((Type t) => this.ComplexType(t));
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x00184970 File Offset: 0x00182B70
		private void ReassignSubtypeMappings()
		{
			foreach (EntityTypeConfiguration entityTypeConfiguration in this.ActiveEntityConfigurations)
			{
				foreach (KeyValuePair<Type, EntityMappingConfiguration> keyValuePair in entityTypeConfiguration.SubTypeMappingConfigurations)
				{
					Type subTypeClrType = keyValuePair.Key;
					EntityTypeConfiguration entityTypeConfiguration2 = this.ActiveEntityConfigurations.SingleOrDefault((EntityTypeConfiguration ec) => ec.ClrType == subTypeClrType);
					if (entityTypeConfiguration2 == null)
					{
						entityTypeConfiguration2 = new EntityTypeConfiguration(subTypeClrType);
						this._entityConfigurations.Add(subTypeClrType, entityTypeConfiguration2);
					}
					entityTypeConfiguration2.AddMappingConfiguration(keyValuePair.Value, false);
				}
			}
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x00184A88 File Offset: 0x00182C88
		private static void RemoveDuplicateTphColumns(DbDatabaseMapping databaseMapping)
		{
			foreach (EntityType currentTable2 in databaseMapping.Database.EntityTypes)
			{
				EntityType currentTable = currentTable2;
				new TphColumnFixer((from f in databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping e) => e.EntityTypeMappings).SelectMany((EntityTypeMapping e) => e.MappingFragments)
				where f.Table == currentTable
				select f).SelectMany((MappingFragment f) => f.ColumnMappings), currentTable, databaseMapping.Database).RemoveDuplicateTphColumns();
			}
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x00184F70 File Offset: 0x00183170
		private static void RemoveRedundantColumnConditions(DbDatabaseMapping databaseMapping)
		{
			(from esm in databaseMapping.GetEntitySetMappings()
			select new
			{
				Set = esm,
				Fragments = from etm in esm.EntityTypeMappings
				from etmf in etm.MappingFragments
				group etmf by etmf.Table into g
				where g.Count((MappingFragment x) => x.GetDefaultDiscriminator() != null) == 1
				select g.Single((MappingFragment x) => x.GetDefaultDiscriminator() != null)
			}).Each(delegate(x)
			{
				x.Fragments.Each(delegate(MappingFragment f)
				{
					f.RemoveDefaultDiscriminator(x.Set);
				});
			});
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x00185184 File Offset: 0x00183384
		private static void RemoveRedundantTables(DbDatabaseMapping databaseMapping)
		{
			List<EntityType> ts = (from t in databaseMapping.Database.EntityTypes
			where databaseMapping.GetEntitySetMappings().SelectMany((EntitySetMapping esm) => esm.EntityTypeMappings).SelectMany((EntityTypeMapping etm) => etm.MappingFragments).All((MappingFragment etmf) => etmf.Table != t) && databaseMapping.GetAssociationSetMappings().All((AssociationSetMapping asm) => asm.Table != t)
			select t).ToList<EntityType>();
			ts.Each(delegate(EntityType t)
			{
				DatabaseName tableName = t.GetTableName();
				if (tableName != null)
				{
					throw Error.OrphanedConfiguredTableDetected(tableName);
				}
				databaseMapping.Database.RemoveEntityType(t);
				List<AssociationType> ts2 = (from at in databaseMapping.Database.AssociationTypes
				where at.SourceEnd.GetEntityType() == t || at.TargetEnd.GetEntityType() == t
				select at).ToList<AssociationType>();
				ts2.Each(delegate(AssociationType at)
				{
					databaseMapping.Database.RemoveAssociationType(at);
				});
			});
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005A0A RID: 23050 RVA: 0x001851F7 File Offset: 0x001833F7
		private IEnumerable<EntityTypeConfiguration> ActiveEntityConfigurations
		{
			get
			{
				return (from keyValuePair in this._entityConfigurations
				where !this._ignoredTypes.Contains(keyValuePair.Key)
				select keyValuePair.Value).ToList<EntityTypeConfiguration>();
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005A0B RID: 23051 RVA: 0x00185257 File Offset: 0x00183457
		private IEnumerable<ComplexTypeConfiguration> ActiveComplexTypeConfigurations
		{
			get
			{
				return (from keyValuePair in this._complexTypeConfigurations
				where !this._ignoredTypes.Contains(keyValuePair.Key)
				select keyValuePair.Value).ToList<ComplexTypeConfiguration>();
			}
		}

		// Token: 0x040023E6 RID: 9190
		private readonly Dictionary<Type, EntityTypeConfiguration> _entityConfigurations = new Dictionary<Type, EntityTypeConfiguration>();

		// Token: 0x040023E7 RID: 9191
		private readonly Dictionary<Type, ComplexTypeConfiguration> _complexTypeConfigurations = new Dictionary<Type, ComplexTypeConfiguration>();

		// Token: 0x040023E8 RID: 9192
		private readonly HashSet<Type> _ignoredTypes = new HashSet<Type>();
	}
}
