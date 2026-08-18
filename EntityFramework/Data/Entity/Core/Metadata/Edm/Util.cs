using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000502 RID: 1282
	internal static class Util
	{
		// Token: 0x06002FC0 RID: 12224 RVA: 0x000E5597 File Offset: 0x000E3797
		internal static void ThrowIfReadOnly(MetadataItem item)
		{
			if (item.IsReadOnly)
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyItem);
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000E55AC File Offset: 0x000E37AC
		[Conditional("DEBUG")]
		internal static void AssertItemHasIdentity(MetadataItem item, string argumentName)
		{
			Check.NotNull<MetadataItem>(item, argumentName);
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000E55B8 File Offset: 0x000E37B8
		internal static ObjectTypeMapping GetObjectMapping(EdmType type, MetadataWorkspace workspace)
		{
			ItemCollection itemCollection;
			if (workspace.TryGetItemCollection(DataSpace.CSpace, out itemCollection))
			{
				return (ObjectTypeMapping)workspace.GetMap(type, DataSpace.OCSpace);
			}
			EdmType edmType;
			EdmType cdmType;
			if (type.DataSpace == DataSpace.CSpace)
			{
				if (Helper.IsPrimitiveType(type))
				{
					edmType = workspace.GetMappedPrimitiveType(((PrimitiveType)type).PrimitiveTypeKind, DataSpace.OSpace);
				}
				else
				{
					edmType = workspace.GetItem<EdmType>(type.FullName, DataSpace.OSpace);
				}
				cdmType = type;
			}
			else
			{
				edmType = type;
				cdmType = type;
			}
			if (!Helper.IsPrimitiveType(edmType) && !Helper.IsEntityType(edmType) && !Helper.IsComplexType(edmType))
			{
				throw new NotSupportedException(Strings.Materializer_UnsupportedType);
			}
			ObjectTypeMapping result;
			if (Helper.IsPrimitiveType(edmType))
			{
				result = new ObjectTypeMapping(edmType, cdmType);
			}
			else
			{
				result = DefaultObjectMappingItemCollection.LoadObjectMapping(cdmType, edmType, null);
			}
			return result;
		}
	}
}
