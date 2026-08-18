using System;
using System.Data.Mapping;
using System.Data.Metadata.Edm;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D8 RID: 984
	internal static class Util
	{
		// Token: 0x0600351B RID: 13595 RVA: 0x000CEC18 File Offset: 0x000CCE18
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
				throw EntityUtil.MaterializerUnsupportedType();
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
