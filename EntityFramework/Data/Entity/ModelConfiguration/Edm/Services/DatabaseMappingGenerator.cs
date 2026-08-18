using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x0200081E RID: 2078
	internal class DatabaseMappingGenerator
	{
		// Token: 0x06005D66 RID: 23910 RVA: 0x001937C2 File Offset: 0x001919C2
		public DatabaseMappingGenerator(DbProviderInfo providerInfo, DbProviderManifest providerManifest)
		{
			this._providerInfo = providerInfo;
			this._providerManifest = providerManifest;
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x001937D8 File Offset: 0x001919D8
		public DbDatabaseMapping Generate(EdmModel conceptualModel)
		{
			DbDatabaseMapping dbDatabaseMapping = this.InitializeDatabaseMapping(conceptualModel);
			DatabaseMappingGenerator.GenerateEntityTypes(dbDatabaseMapping);
			DatabaseMappingGenerator.GenerateDiscriminators(dbDatabaseMapping);
			DatabaseMappingGenerator.GenerateAssociationTypes(dbDatabaseMapping);
			return dbDatabaseMapping;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x00193800 File Offset: 0x00191A00
		private DbDatabaseMapping InitializeDatabaseMapping(EdmModel conceptualModel)
		{
			EdmModel database = EdmModel.CreateStoreModel(this._providerInfo, this._providerManifest, conceptualModel.SchemaVersion);
			return new DbDatabaseMapping().Initialize(conceptualModel, database);
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0019384C File Offset: 0x00191A4C
		private static void GenerateEntityTypes(DbDatabaseMapping databaseMapping)
		{
			using (IEnumerator<EntityType> enumerator = databaseMapping.Model.EntityTypes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntityType entityType = enumerator.Current;
					if (entityType.Abstract)
					{
						if (databaseMapping.Model.EntityTypes.All((EntityType e) => e.BaseType != entityType))
						{
							throw new InvalidOperationException(Strings.UnmappedAbstractType(entityType.GetClrType()));
						}
					}
					new TableMappingGenerator(databaseMapping.ProviderManifest).Generate(entityType, databaseMapping);
				}
			}
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x00193904 File Offset: 0x00191B04
		private static void GenerateDiscriminators(DbDatabaseMapping databaseMapping)
		{
			foreach (EntitySetMapping entitySetMapping in databaseMapping.GetEntitySetMappings())
			{
				if (entitySetMapping.EntityTypeMappings.Count<EntityTypeMapping>() > 1)
				{
					TypeUsage storeType = databaseMapping.ProviderManifest.GetStoreType(DatabaseMappingGenerator.DiscriminatorTypeUsage);
					EdmProperty edmProperty = new EdmProperty("Discriminator", storeType)
					{
						Nullable = false,
						DefaultValue = "(Undefined)"
					};
					entitySetMapping.EntityTypeMappings.First<EntityTypeMapping>().MappingFragments.Single<MappingFragment>().Table.AddColumn(edmProperty);
					foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
					{
						if (!entityTypeMapping.EntityType.Abstract)
						{
							MappingFragment mappingFragment = entityTypeMapping.MappingFragments.Single<MappingFragment>();
							mappingFragment.SetDefaultDiscriminator(edmProperty);
							mappingFragment.AddDiscriminatorCondition(edmProperty, entityTypeMapping.EntityType.Name);
						}
					}
				}
			}
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x00193A2C File Offset: 0x00191C2C
		private static void GenerateAssociationTypes(DbDatabaseMapping databaseMapping)
		{
			foreach (AssociationType associationType in databaseMapping.Model.AssociationTypes)
			{
				new AssociationTypeMappingGenerator(databaseMapping.ProviderManifest).Generate(associationType, databaseMapping);
			}
		}

		// Token: 0x040024EB RID: 9451
		private const string DiscriminatorColumnName = "Discriminator";

		// Token: 0x040024EC RID: 9452
		public const int DiscriminatorMaxLength = 128;

		// Token: 0x040024ED RID: 9453
		public static TypeUsage DiscriminatorTypeUsage = TypeUsage.CreateStringTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String), true, false, 128);

		// Token: 0x040024EE RID: 9454
		private readonly DbProviderInfo _providerInfo;

		// Token: 0x040024EF RID: 9455
		private readonly DbProviderManifest _providerManifest;
	}
}
