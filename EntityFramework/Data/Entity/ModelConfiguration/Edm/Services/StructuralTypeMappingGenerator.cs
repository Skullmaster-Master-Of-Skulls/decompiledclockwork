using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x020002C9 RID: 713
	internal abstract class StructuralTypeMappingGenerator
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x0007CD69 File Offset: 0x0007AF69
		protected StructuralTypeMappingGenerator(DbProviderManifest providerManifest)
		{
			this._providerManifest = providerManifest;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0007CD78 File Offset: 0x0007AF78
		protected EdmProperty MapTableColumn(EdmProperty property, string columnName, bool isInstancePropertyOnDerivedType)
		{
			TypeUsage edmType = TypeUsage.Create(property.UnderlyingPrimitiveType, property.TypeUsage.Facets);
			TypeUsage storeType = this._providerManifest.GetStoreType(edmType);
			EdmProperty edmProperty = new EdmProperty(columnName, storeType)
			{
				Nullable = (isInstancePropertyOnDerivedType || property.Nullable)
			};
			if (edmProperty.IsPrimaryKeyColumn)
			{
				edmProperty.Nullable = false;
			}
			StoreGeneratedPattern? storeGeneratedPattern = property.GetStoreGeneratedPattern();
			if (storeGeneratedPattern != null)
			{
				edmProperty.StoreGeneratedPattern = storeGeneratedPattern.Value;
			}
			StructuralTypeMappingGenerator.MapPrimitivePropertyFacets(property, edmProperty, storeType);
			return edmProperty;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0007CE00 File Offset: 0x0007B000
		internal static void MapPrimitivePropertyFacets(EdmProperty property, EdmProperty column, TypeUsage typeUsage)
		{
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "FixedLength") && property.IsFixedLength != null)
			{
				column.IsFixedLength = property.IsFixedLength;
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "MaxLength"))
			{
				column.IsMaxLength = property.IsMaxLength;
				if (!column.IsMaxLength || property.MaxLength != null)
				{
					column.MaxLength = property.MaxLength;
				}
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "Unicode") && property.IsUnicode != null)
			{
				column.IsUnicode = property.IsUnicode;
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "Precision"))
			{
				byte? precision = property.Precision;
				int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
				if (num != null)
				{
					column.Precision = property.Precision;
				}
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "Scale"))
			{
				byte? scale = property.Scale;
				int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
				if (num2 != null)
				{
					column.Scale = property.Scale;
				}
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0007CF38 File Offset: 0x0007B138
		private static bool IsValidFacet(TypeUsage typeUsage, string name)
		{
			Facet facet;
			return typeUsage.Facets.TryGetValue(name, false, out facet) && !facet.Description.IsConstant;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0007D004 File Offset: 0x0007B204
		protected static EntityTypeMapping GetEntityTypeMappingInHierarchy(DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType);
			if (entityTypeMapping == null)
			{
				EntitySetMapping entitySetMapping = databaseMapping.GetEntitySetMapping(databaseMapping.Model.GetEntitySet(entityType));
				if (entitySetMapping != null)
				{
					entityTypeMapping = entitySetMapping.EntityTypeMappings.First((EntityTypeMapping etm) => entityType.DeclaredProperties.All((EdmProperty dp) => (from pm in etm.MappingFragments.First<MappingFragment>().ColumnMappings
					select pm.PropertyPath.First<EdmProperty>()).Contains(dp)));
				}
			}
			return entityTypeMapping;
		}

		// Token: 0x040008A9 RID: 2217
		protected readonly DbProviderManifest _providerManifest;
	}
}
