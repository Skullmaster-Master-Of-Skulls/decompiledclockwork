using System;
using System.Data.Mapping;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001B7 RID: 439
	internal sealed class ClrPerspective : Perspective
	{
		// Token: 0x06001EE1 RID: 7905 RVA: 0x0006CE41 File Offset: 0x0006B041
		internal ClrPerspective(MetadataWorkspace metadataWorkspace) : base(metadataWorkspace, DataSpace.CSpace)
		{
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0006CE4B File Offset: 0x0006B04B
		internal bool TryGetType(Type clrType, out TypeUsage outTypeUsage)
		{
			return this.TryGetTypeByName(clrType.FullName, false, out outTypeUsage);
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0006CE5C File Offset: 0x0006B05C
		internal override bool TryGetMember(StructuralType type, string memberName, bool ignoreCase, out EdmMember outMember)
		{
			outMember = null;
			Map map = null;
			if (base.MetadataWorkspace.TryGetMap(type, DataSpace.OCSpace, out map))
			{
				ObjectTypeMapping objectTypeMapping = map as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					ObjectMemberMapping memberMapForClrMember = objectTypeMapping.GetMemberMapForClrMember(memberName, ignoreCase);
					if (memberMapForClrMember != null)
					{
						outMember = memberMapForClrMember.EdmMember;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0006CEA4 File Offset: 0x0006B0A4
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage)
		{
			typeUsage = null;
			Map map = null;
			if (base.MetadataWorkspace.TryGetMap(fullName, DataSpace.OSpace, ignoreCase, DataSpace.OCSpace, out map))
			{
				if (map.EdmItem.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					PrimitiveType mappedPrimitiveType = base.MetadataWorkspace.GetMappedPrimitiveType(((PrimitiveType)map.EdmItem).PrimitiveTypeKind, DataSpace.CSpace);
					if (mappedPrimitiveType != null)
					{
						typeUsage = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(mappedPrimitiveType.PrimitiveTypeKind);
					}
				}
				else
				{
					typeUsage = ClrPerspective.GetMappedTypeUsage(map);
				}
			}
			return typeUsage != null;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0006CF19 File Offset: 0x0006B119
		internal override EntityContainer GetDefaultContainer()
		{
			return this._defaultContainer;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0006CF24 File Offset: 0x0006B124
		internal void SetDefaultContainer(string defaultContainerName)
		{
			EntityContainer defaultContainer = null;
			if (!string.IsNullOrEmpty(defaultContainerName) && !base.MetadataWorkspace.TryGetEntityContainer(defaultContainerName, DataSpace.CSpace, out defaultContainer))
			{
				throw EntityUtil.InvalidDefaultContainerName("defaultContainerName", defaultContainerName);
			}
			this._defaultContainer = defaultContainer;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0006CF60 File Offset: 0x0006B160
		private static TypeUsage GetMappedTypeUsage(Map map)
		{
			TypeUsage result = null;
			if (map != null)
			{
				MetadataItem edmItem = map.EdmItem;
				EdmType edmType = edmItem as EdmType;
				if (edmItem != null && edmType != null)
				{
					result = TypeUsage.Create(edmType);
				}
			}
			return result;
		}

		// Token: 0x04000CF8 RID: 3320
		private EntityContainer _defaultContainer;
	}
}
