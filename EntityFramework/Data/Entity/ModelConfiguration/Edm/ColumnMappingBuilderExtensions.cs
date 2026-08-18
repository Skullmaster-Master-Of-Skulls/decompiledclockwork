using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200080E RID: 2062
	internal static class ColumnMappingBuilderExtensions
	{
		// Token: 0x06005CCB RID: 23755 RVA: 0x00190C88 File Offset: 0x0018EE88
		public static void SyncNullabilityCSSpace(this ColumnMappingBuilder propertyMappingBuilder, DbDatabaseMapping databaseMapping, IEnumerable<EntitySet> entitySets, EntityType toTable)
		{
			EdmProperty edmProperty = propertyMappingBuilder.PropertyPath.Last<EdmProperty>();
			EntitySetMapping entitySetMapping = null;
			EntityType baseType = (EntityType)edmProperty.DeclaringType.BaseType;
			if (baseType != null)
			{
				entitySetMapping = ColumnMappingBuilderExtensions.GetEntitySetMapping(databaseMapping, baseType, entitySets);
			}
			while (baseType != null)
			{
				if (toTable == entitySetMapping.EntityTypeMappings.First((EntityTypeMapping m) => m.EntityType == baseType).GetPrimaryTable())
				{
					return;
				}
				baseType = (EntityType)baseType.BaseType;
			}
			propertyMappingBuilder.ColumnProperty.Nullable = edmProperty.Nullable;
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x00190D58 File Offset: 0x0018EF58
		private static EntitySetMapping GetEntitySetMapping(DbDatabaseMapping databaseMapping, EntityType cSpaceEntityType, IEnumerable<EntitySet> entitySets)
		{
			while (cSpaceEntityType.BaseType != null)
			{
				cSpaceEntityType = (EntityType)cSpaceEntityType.BaseType;
			}
			EntitySet cSpaceEntitySet = entitySets.First((EntitySet s) => s.ElementType == cSpaceEntityType);
			return databaseMapping.EntityContainerMappings.First<EntityContainerMapping>().EntitySetMappings.First((EntitySetMapping m) => m.EntitySet == cSpaceEntitySet);
		}
	}
}
