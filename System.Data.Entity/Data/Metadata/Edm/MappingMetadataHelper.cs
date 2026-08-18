using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Linq;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200020A RID: 522
	internal static class MappingMetadataHelper
	{
		// Token: 0x0600229F RID: 8863 RVA: 0x0007B128 File Offset: 0x00079328
		internal static IEnumerable<StorageTypeMapping> GetMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType)
		{
			StorageEntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap(mappingCollection, container);
			StorageSetMapping setMapping = entityContainerMap.GetSetMapping(entitySet.Name);
			if (setMapping != null)
			{
				IEnumerable<StorageTypeMapping> typeMappings = setMapping.TypeMappings;
				Func<StorageTypeMapping, bool> <>9__0;
				Func<StorageTypeMapping, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((StorageTypeMapping map) => map.Types.Union(map.IsOfTypes).Contains(entityType)));
				}
				foreach (StorageTypeMapping storageTypeMapping in typeMappings.Where(predicate))
				{
					yield return storageTypeMapping;
				}
				IEnumerator<StorageTypeMapping> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0007B150 File Offset: 0x00079350
		internal static IEnumerable<StorageTypeMapping> GetMappingsForEntitySetAndSuperTypes(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase childEntityType)
		{
			return MetadataHelper.GetTypeAndParentTypesOf(childEntityType, mappingCollection.EdmItemCollection, true).SelectMany(delegate(EdmType edmType)
			{
				if (!edmType.EdmEquals(childEntityType))
				{
					return MappingMetadataHelper.GetIsTypeOfMappingsForEntitySetAndType(mappingCollection, container, entitySet, edmType as EntityTypeBase, childEntityType);
				}
				return MappingMetadataHelper.GetMappingsForEntitySetAndType(mappingCollection, container, entitySet, edmType as EntityTypeBase);
			}).ToList<StorageTypeMapping>();
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0007B1AC File Offset: 0x000793AC
		private static IEnumerable<StorageTypeMapping> GetIsTypeOfMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType, EntityTypeBase childEntityType)
		{
			Func<EdmType, bool> <>9__0;
			foreach (StorageTypeMapping storageTypeMapping in MappingMetadataHelper.GetMappingsForEntitySetAndType(mappingCollection, container, entitySet, entityType))
			{
				IEnumerable<EdmType> isOfTypes = storageTypeMapping.IsOfTypes;
				Func<EdmType, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((EdmType parentType) => parentType.IsAssignableFrom(childEntityType)));
				}
				if (isOfTypes.Any(predicate) || storageTypeMapping.Types.Contains(childEntityType))
				{
					yield return storageTypeMapping;
				}
			}
			IEnumerator<StorageTypeMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0007B1D9 File Offset: 0x000793D9
		internal static IEnumerable<StorageEntityTypeModificationFunctionMapping> GetModificationFunctionMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType)
		{
			StorageEntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap(mappingCollection, container);
			StorageSetMapping setMapping = entityContainerMap.GetSetMapping(entitySet.Name);
			StorageEntitySetMapping storageEntitySetMapping = setMapping as StorageEntitySetMapping;
			if (storageEntitySetMapping != null && storageEntitySetMapping != null)
			{
				IEnumerable<StorageEntityTypeModificationFunctionMapping> modificationFunctionMappings = storageEntitySetMapping.ModificationFunctionMappings;
				Func<StorageEntityTypeModificationFunctionMapping, bool> <>9__0;
				Func<StorageEntityTypeModificationFunctionMapping, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((StorageEntityTypeModificationFunctionMapping functionMap) => functionMap.EntityType.Equals(entityType)));
				}
				foreach (StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping in modificationFunctionMappings.Where(predicate))
				{
					yield return storageEntityTypeModificationFunctionMapping;
				}
				IEnumerator<StorageEntityTypeModificationFunctionMapping> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0007B200 File Offset: 0x00079400
		internal static StorageEntityContainerMapping GetEntityContainerMap(StorageMappingItemCollection mappingCollection, EntityContainer entityContainer)
		{
			ReadOnlyCollection<StorageEntityContainerMapping> items = mappingCollection.GetItems<StorageEntityContainerMapping>();
			StorageEntityContainerMapping storageEntityContainerMapping = null;
			foreach (StorageEntityContainerMapping storageEntityContainerMapping2 in items)
			{
				if (entityContainer.Equals(storageEntityContainerMapping2.EdmEntityContainer) || entityContainer.Equals(storageEntityContainerMapping2.StorageEntityContainer))
				{
					storageEntityContainerMapping = storageEntityContainerMapping2;
					break;
				}
			}
			if (storageEntityContainerMapping == null)
			{
				throw new MappingException(Strings.Mapping_NotFound_EntityContainer(entityContainer.Name));
			}
			return storageEntityContainerMapping;
		}
	}
}
