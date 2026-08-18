using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050F RID: 1295
	internal class ModelPerspective : Perspective
	{
		// Token: 0x060030D1 RID: 12497 RVA: 0x000E9EDD File Offset: 0x000E80DD
		internal ModelPerspective(MetadataWorkspace metadataWorkspace) : base(metadataWorkspace, DataSpace.CSpace)
		{
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000E9EE8 File Offset: 0x000E80E8
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage)
		{
			Check.NotEmpty(fullName, "fullName");
			typeUsage = null;
			EdmType edmType = null;
			if (base.MetadataWorkspace.TryGetItem<EdmType>(fullName, ignoreCase, base.TargetDataspace, out edmType))
			{
				if (Helper.IsPrimitiveType(edmType))
				{
					typeUsage = MetadataWorkspace.GetCanonicalModelTypeUsage(((PrimitiveType)edmType).PrimitiveTypeKind);
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
