using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x0200081F RID: 2079
	internal class TableMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x06005D6D RID: 23917 RVA: 0x00193AA6 File Offset: 0x00191CA6
		public TableMappingGenerator(DbProviderManifest providerManifest) : base(providerManifest)
		{
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x00193AD0 File Offset: 0x00191CD0
		public void Generate(EntityType entityType, DbDatabaseMapping databaseMapping)
		{
			EntitySet entitySet = databaseMapping.Model.GetEntitySet(entityType);
			EntitySetMapping entitySetMapping = databaseMapping.GetEntitySetMapping(entitySet) ?? databaseMapping.AddEntitySetMapping(entitySet);
			EntityTypeMapping entityTypeMapping = entitySetMapping.EntityTypeMappings.FirstOrDefault((EntityTypeMapping m) => m.EntityTypes.Contains(entitySet.ElementType)) ?? entitySetMapping.EntityTypeMappings.FirstOrDefault<EntityTypeMapping>();
			EntityType entityType2 = (entityTypeMapping != null) ? entityTypeMapping.MappingFragments.First<MappingFragment>().Table : databaseMapping.Database.AddTable(entityType.GetRootType().Name);
			entityTypeMapping = new EntityTypeMapping(null);
			MappingFragment mappingFragment = new MappingFragment(databaseMapping.Database.GetEntitySet(entityType2), entityTypeMapping, false);
			entityTypeMapping.AddType(entityType);
			entityTypeMapping.AddFragment(mappingFragment);
			entityTypeMapping.SetClrType(entityType.GetClrType());
			entitySetMapping.AddTypeMapping(entityTypeMapping);
			new PropertyMappingGenerator(this._providerManifest).Generate(entityType, entityType.Properties, entitySetMapping, mappingFragment, new List<EdmProperty>(), false);
		}
	}
}
