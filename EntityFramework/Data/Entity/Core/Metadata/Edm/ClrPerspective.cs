using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B1 RID: 1201
	internal sealed class ClrPerspective : Perspective
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000D7542 File Offset: 0x000D5742
		internal ClrPerspective(MetadataWorkspace metadataWorkspace) : base(metadataWorkspace, DataSpace.CSpace)
		{
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000D754C File Offset: 0x000D574C
		internal bool TryGetType(Type clrType, out TypeUsage outTypeUsage)
		{
			return this.TryGetTypeByName(clrType.FullNameWithNesting(), false, out outTypeUsage);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000D755C File Offset: 0x000D575C
		internal override bool TryGetMember(StructuralType type, string memberName, bool ignoreCase, out EdmMember outMember)
		{
			outMember = null;
			MappingBase mappingBase = null;
			if (base.MetadataWorkspace.TryGetMap(type, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = mappingBase as ObjectTypeMapping;
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

		// Token: 0x06002C49 RID: 11337 RVA: 0x000D75A4 File Offset: 0x000D57A4
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage)
		{
			typeUsage = null;
			MappingBase mappingBase = null;
			if (base.MetadataWorkspace.TryGetMap(fullName, DataSpace.OSpace, ignoreCase, DataSpace.OCSpace, out mappingBase))
			{
				if (mappingBase.EdmItem.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					PrimitiveType mappedPrimitiveType = base.MetadataWorkspace.GetMappedPrimitiveType(((PrimitiveType)mappingBase.EdmItem).PrimitiveTypeKind, DataSpace.CSpace);
					if (mappedPrimitiveType != null)
					{
						typeUsage = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(mappedPrimitiveType.PrimitiveTypeKind);
					}
				}
				else
				{
					typeUsage = ClrPerspective.GetMappedTypeUsage(mappingBase);
				}
			}
			return null != typeUsage;
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000D761C File Offset: 0x000D581C
		internal override EntityContainer GetDefaultContainer()
		{
			return this._defaultContainer;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000D7624 File Offset: 0x000D5824
		internal void SetDefaultContainer(string defaultContainerName)
		{
			EntityContainer defaultContainer = null;
			if (!string.IsNullOrEmpty(defaultContainerName) && !base.MetadataWorkspace.TryGetEntityContainer(defaultContainerName, DataSpace.CSpace, out defaultContainer))
			{
				throw new ArgumentException(Strings.ObjectContext_InvalidDefaultContainerName(defaultContainerName), "defaultContainerName");
			}
			this._defaultContainer = defaultContainer;
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000D7664 File Offset: 0x000D5864
		private static TypeUsage GetMappedTypeUsage(MappingBase map)
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

		// Token: 0x04001055 RID: 4181
		private EntityContainer _defaultContainer;
	}
}
