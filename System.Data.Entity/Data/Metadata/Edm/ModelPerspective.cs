using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000203 RID: 515
	internal class ModelPerspective : Perspective
	{
		// Token: 0x06002207 RID: 8711 RVA: 0x0006CE41 File Offset: 0x0006B041
		internal ModelPerspective(MetadataWorkspace metadataWorkspace) : base(metadataWorkspace, DataSpace.CSpace)
		{
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00077EFC File Offset: 0x000760FC
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage)
		{
			EntityUtil.CheckStringArgument(fullName, "fullName");
			typeUsage = null;
			EdmType edmType = null;
			if (base.MetadataWorkspace.TryGetItem<EdmType>(fullName, ignoreCase, base.TargetDataspace, out edmType))
			{
				if (Helper.IsPrimitiveType(edmType))
				{
					typeUsage = base.MetadataWorkspace.GetCanonicalModelTypeUsage(((PrimitiveType)edmType).PrimitiveTypeKind);
				}
				else
				{
					typeUsage = TypeUsage.Create(edmType);
				}
			}
			return typeUsage != null;
		}
	}
}
