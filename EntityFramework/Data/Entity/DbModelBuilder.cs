using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Conventions.Sets;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Mappers;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity
{
	// Token: 0x02000734 RID: 1844
	public class DbModelBuilder
	{
		// Token: 0x06005372 RID: 21362 RVA: 0x0016F84F File Offset: 0x0016DA4F
		public DbModelBuilder() : this(new ModelConfiguration(), DbModelBuilderVersion.Latest)
		{
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0016F85D File Offset: 0x0016DA5D
		public DbModelBuilder(DbModelBuilderVersion modelBuilderVersion) : this(new ModelConfiguration(), modelBuilderVersion)
		{
			if (!Enum.IsDefined(typeof(DbModelBuilderVersion), modelBuilderVersion))
			{
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0016F88D File Offset: 0x0016DA8D
		internal DbModelBuilder(ModelConfiguration modelConfiguration, DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest) : this(modelConfiguration, new ConventionsConfiguration(DbModelBuilder.SelectConventionSet(modelBuilderVersion)), modelBuilderVersion)
		{
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0016F8A4 File Offset: 0x0016DAA4
		private static ConventionSet SelectConventionSet(DbModelBuilderVersion modelBuilderVersion)
		{
			switch (modelBuilderVersion)
			{
			case DbModelBuilderVersion.Latest:
			case DbModelBuilderVersion.V5_0_Net4:
			case DbModelBuilderVersion.V5_0:
			case DbModelBuilderVersion.V6_0:
				return V2ConventionSet.Conventions;
			case DbModelBuilderVersion.V4_1:
				return V1ConventionSet.Conventions;
			default:
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0016F8E8 File Offset: 0x0016DAE8
		private DbModelBuilder(ModelConfiguration modelConfiguration, ConventionsConfiguration conventionsConfiguration, DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest)
		{
			this._lock = new object();
			base..ctor();
			if (!Enum.IsDefined(typeof(DbModelBuilderVersion), modelBuilderVersion))
			{
				throw new ArgumentOutOfRangeException("modelBuilderVersion");
			}
			this._modelConfiguration = modelConfiguration;
			this._conventionsConfiguration = conventionsConfiguration;
			this._modelBuilderVersion = modelBuilderVersion;
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x0016F940 File Offset: 0x0016DB40
		private DbModelBuilder(DbModelBuilder source)
		{
			this._lock = new object();
			base..ctor();
			this._modelConfiguration = source._modelConfiguration.Clone();
			this._conventionsConfiguration = source._conventionsConfiguration.Clone();
			this._modelBuilderVersion = source._modelBuilderVersion;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0016F98C File Offset: 0x0016DB8C
		internal virtual DbModelBuilder Clone()
		{
			DbModelBuilder result;
			lock (this._lock)
			{
				result = new DbModelBuilder(this);
			}
			return result;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0016F9F0 File Offset: 0x0016DBF0
		internal DbModel BuildDynamicUpdateModel(DbProviderInfo providerInfo)
		{
			DbModel dbModel = this.Build(providerInfo);
			EntityContainerMapping entityContainerMapping = dbModel.DatabaseMapping.EntityContainerMappings.Single<EntityContainerMapping>();
			entityContainerMapping.EntitySetMappings.Each(delegate(EntitySetMapping esm)
			{
				esm.ClearModificationFunctionMappings();
			});
			entityContainerMapping.AssociationSetMappings.Each((AssociationSetMapping asm) => asm.ModificationFunctionMapping = null);
			return dbModel;
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x0016FA67 File Offset: 0x0016DC67
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public virtual DbModelBuilder Ignore<T>() where T : class
		{
			this._modelConfiguration.Ignore(typeof(T));
			return this;
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0016FA7F File Offset: 0x0016DC7F
		public virtual DbModelBuilder HasDefaultSchema(string schema)
		{
			this._modelConfiguration.DefaultSchema = schema;
			return this;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0016FA90 File Offset: 0x0016DC90
		public virtual DbModelBuilder Ignore(IEnumerable<Type> types)
		{
			Check.NotNull<IEnumerable<Type>>(types, "types");
			foreach (Type type in types)
			{
				this._modelConfiguration.Ignore(type);
			}
			return this;
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0016FAEC File Offset: 0x0016DCEC
		public virtual EntityTypeConfiguration<TEntityType> Entity<TEntityType>() where TEntityType : class
		{
			return new EntityTypeConfiguration<TEntityType>(this._modelConfiguration.Entity(typeof(TEntityType), true));
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0016FB09 File Offset: 0x0016DD09
		public virtual void RegisterEntityType(Type entityType)
		{
			Check.NotNull<Type>(entityType, "entityType");
			this.Entity(entityType);
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0016FB20 File Offset: 0x0016DD20
		internal virtual EntityTypeConfiguration Entity(Type entityType)
		{
			EntityTypeConfiguration entityTypeConfiguration = this._modelConfiguration.Entity(entityType);
			entityTypeConfiguration.IsReplaceable = true;
			return entityTypeConfiguration;
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x0016FB42 File Offset: 0x0016DD42
		public virtual ComplexTypeConfiguration<TComplexType> ComplexType<TComplexType>() where TComplexType : class
		{
			return new ComplexTypeConfiguration<TComplexType>(this._modelConfiguration.ComplexType(typeof(TComplexType)));
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0016FB5E File Offset: 0x0016DD5E
		public TypeConventionConfiguration Types()
		{
			return new TypeConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x0016FB6B File Offset: 0x0016DD6B
		public TypeConventionConfiguration<T> Types<T>() where T : class
		{
			return new TypeConventionConfiguration<T>(this._conventionsConfiguration);
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x0016FB78 File Offset: 0x0016DD78
		public PropertyConventionConfiguration Properties()
		{
			return new PropertyConventionConfiguration(this._conventionsConfiguration);
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x0016FBB4 File Offset: 0x0016DDB4
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public PropertyConventionConfiguration Properties<T>()
		{
			if (!typeof(T).IsValidEdmScalarType())
			{
				throw Error.ModelBuilder_PropertyFilterTypeMustBePrimitive(typeof(T));
			}
			PropertyConventionConfiguration propertyConventionConfiguration = new PropertyConventionConfiguration(this._conventionsConfiguration);
			return propertyConventionConfiguration.Where(delegate(PropertyInfo p)
			{
				Type left;
				p.PropertyType.TryUnwrapNullableType(out left);
				return left == typeof(T);
			});
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06005385 RID: 21381 RVA: 0x0016FC00 File Offset: 0x0016DE00
		public virtual ConventionsConfiguration Conventions
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06005386 RID: 21382 RVA: 0x0016FC08 File Offset: 0x0016DE08
		public virtual ConfigurationRegistrar Configurations
		{
			get
			{
				return new ConfigurationRegistrar(this._modelConfiguration);
			}
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0016FC18 File Offset: 0x0016DE18
		public virtual DbModel Build(DbConnection providerConnection)
		{
			Check.NotNull<DbConnection>(providerConnection, "providerConnection");
			DbProviderManifest providerManifest;
			DbProviderInfo providerInfo = providerConnection.GetProviderInfo(out providerManifest);
			return this.Build(providerManifest, providerInfo);
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0016FC44 File Offset: 0x0016DE44
		public virtual DbModel Build(DbProviderInfo providerInfo)
		{
			Check.NotNull<DbProviderInfo>(providerInfo, "providerInfo");
			DbProviderManifest providerManifest = DbModelBuilder.GetProviderManifest(providerInfo);
			return this.Build(providerManifest, providerInfo);
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06005389 RID: 21385 RVA: 0x0016FC6C File Offset: 0x0016DE6C
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		internal DbModelBuilderVersion Version
		{
			get
			{
				return this._modelBuilderVersion;
			}
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x0016FC74 File Offset: 0x0016DE74
		private DbModel Build(DbProviderManifest providerManifest, DbProviderInfo providerInfo)
		{
			double edmVersion = this._modelBuilderVersion.GetEdmVersion();
			DbModelBuilder modelBuilder = this.Clone();
			DbModel dbModel = new DbModel(new DbDatabaseMapping
			{
				Model = EdmModel.CreateConceptualModel(edmVersion),
				Database = EdmModel.CreateStoreModel(providerInfo, providerManifest, edmVersion)
			}, modelBuilder);
			dbModel.ConceptualModel.Container.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:UseClrTypes", "true");
			this._conventionsConfiguration.ApplyModelConfiguration(this._modelConfiguration);
			this._modelConfiguration.NormalizeConfigurations();
			this.MapTypes(dbModel.ConceptualModel);
			this._modelConfiguration.Configure(dbModel.ConceptualModel);
			this._conventionsConfiguration.ApplyConceptualModel(dbModel);
			dbModel.ConceptualModel.Validate();
			dbModel = new DbModel(dbModel.ConceptualModel.GenerateDatabaseMapping(providerInfo, providerManifest), modelBuilder);
			this._conventionsConfiguration.ApplyPluralizingTableNameConvention(dbModel);
			this._modelConfiguration.Configure(dbModel.DatabaseMapping, providerManifest);
			this._conventionsConfiguration.ApplyStoreModel(dbModel);
			this._conventionsConfiguration.ApplyMapping(dbModel.DatabaseMapping);
			dbModel.StoreModel.Validate();
			return dbModel;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x0016FD84 File Offset: 0x0016DF84
		private static DbProviderManifest GetProviderManifest(DbProviderInfo providerInfo)
		{
			DbProviderFactory service = DbConfiguration.DependencyResolver.GetService(providerInfo.ProviderInvariantName);
			DbProviderServices providerServices = service.GetProviderServices();
			return providerServices.GetProviderManifest(providerInfo.ProviderManifestToken);
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0016FDB8 File Offset: 0x0016DFB8
		private void MapTypes(EdmModel model)
		{
			TypeMapper typeMapper = new TypeMapper(new MappingContext(this._modelConfiguration, this._conventionsConfiguration, model, this._modelBuilderVersion, DbConfiguration.DependencyResolver.GetService<AttributeProvider>()));
			IList<Type> list = (this._modelConfiguration.Entities as IList<Type>) ?? this._modelConfiguration.Entities.ToList<Type>();
			for (int i = 0; i < list.Count; i++)
			{
				Type type = list[i];
				if (typeMapper.MapEntityType(type) == null)
				{
					throw Error.InvalidEntityType(type);
				}
			}
			IList<Type> list2 = (this._modelConfiguration.ComplexTypes as IList<Type>) ?? this._modelConfiguration.ComplexTypes.ToList<Type>();
			for (int j = 0; j < list2.Count; j++)
			{
				Type type2 = list2[j];
				if (typeMapper.MapComplexType(type2, false) == null)
				{
					throw Error.CodeFirstInvalidComplexType(type2);
				}
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x0016FE95 File Offset: 0x0016E095
		internal ModelConfiguration ModelConfiguration
		{
			get
			{
				return this._modelConfiguration;
			}
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0016FE9D File Offset: 0x0016E09D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x0016FEA5 File Offset: 0x0016E0A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0016FEAE File Offset: 0x0016E0AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0016FEB6 File Offset: 0x0016E0B6
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002262 RID: 8802
		private readonly ModelConfiguration _modelConfiguration;

		// Token: 0x04002263 RID: 8803
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x04002264 RID: 8804
		private readonly DbModelBuilderVersion _modelBuilderVersion;

		// Token: 0x04002265 RID: 8805
		private readonly object _lock;
	}
}
