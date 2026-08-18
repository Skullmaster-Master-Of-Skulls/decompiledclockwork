using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000504 RID: 1284
	internal static class MappingMetadataHelper
	{
		// Token: 0x06003006 RID: 12294 RVA: 0x000E6678 File Offset: 0x000E4878
		internal static IEnumerable<TypeMapping> GetMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType)
		{
			EntityContainerMapping containerMapping = MappingMetadataHelper.GetEntityContainerMap(mappingCollection, container);
			EntitySetBaseMapping extentMap = containerMapping.GetSetMapping(entitySet.Name);
			if (extentMap != null)
			{
				foreach (TypeMapping typeMap in from map in extentMap.TypeMappings
				where map.Types.Union(map.IsOfTypes).Contains(entityType)
				select map)
				{
					yield return typeMap;
				}
			}
			yield break;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000E6710 File Offset: 0x000E4910
		internal static IEnumerable<TypeMapping> GetMappingsForEntitySetAndSuperTypes(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase childEntityType)
		{
			return MetadataHelper.GetTypeAndParentTypesOf(childEntityType, true).SelectMany(delegate(EdmType edmType)
			{
				EntityTypeBase entityType = edmType as EntityTypeBase;
				if (!edmType.EdmEquals(childEntityType))
				{
					return MappingMetadataHelper.GetIsTypeOfMappingsForEntitySetAndType(mappingCollection, container, entitySet, entityType, childEntityType);
				}
				return MappingMetadataHelper.GetMappingsForEntitySetAndType(mappingCollection, container, entitySet, entityType);
			}).ToList<TypeMapping>();
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000E69D0 File Offset: 0x000E4BD0
		private static IEnumerable<TypeMapping> GetIsTypeOfMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType, EntityTypeBase childEntityType)
		{
			foreach (TypeMapping mapping in MappingMetadataHelper.GetMappingsForEntitySetAndType(mappingCollection, container, entitySet, entityType))
			{
				if (mapping.IsOfTypes.Any((EntityTypeBase parentType) => parentType.IsAssignableFrom(childEntityType)) || mapping.Types.Contains(childEntityType))
				{
					yield return mapping;
				}
			}
			yield break;
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000E6C8C File Offset: 0x000E4E8C
		internal static IEnumerable<EntityTypeModificationFunctionMapping> GetModificationFunctionMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType)
		{
			EntityContainerMapping containerMapping = MappingMetadataHelper.GetEntityContainerMap(mappingCollection, container);
			EntitySetBaseMapping extentMap = containerMapping.GetSetMapping(entitySet.Name);
			EntitySetMapping entitySetMapping = extentMap as EntitySetMapping;
			if (entitySetMapping != null && entitySetMapping != null)
			{
				foreach (EntityTypeModificationFunctionMapping v in from functionMap in entitySetMapping.ModificationFunctionMappings
				where functionMap.EntityType.Equals(entityType)
				select functionMap)
				{
					yield return v;
				}
			}
			yield break;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000E6CC0 File Offset: 0x000E4EC0
		internal static EntityContainerMapping GetEntityContainerMap(StorageMappingItemCollection mappingCollection, EntityContainer entityContainer)
		{
			ReadOnlyCollection<EntityContainerMapping> items = mappingCollection.GetItems<EntityContainerMapping>();
			EntityContainerMapping entityContainerMapping = null;
			foreach (EntityContainerMapping entityContainerMapping2 in items)
			{
				if (entityContainer.Equals(entityContainerMapping2.EdmEntityContainer) || entityContainer.Equals(entityContainerMapping2.StorageEntityContainer))
				{
					entityContainerMapping = entityContainerMapping2;
					break;
				}
			}
			if (entityContainerMapping == null)
			{
				throw new MappingException(Strings.Mapping_NotFound_EntityContainer(entityContainer.Name));
			}
			return entityContainerMapping;
		}
	}
}
