using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x0200022B RID: 555
	internal class ObjectAssociationEndMapping : ObjectMemberMapping
	{
		// Token: 0x060023CB RID: 9163 RVA: 0x000815F6 File Offset: 0x0007F7F6
		internal ObjectAssociationEndMapping(AssociationEndMember edmAssociationEnd, AssociationEndMember clrAssociationEnd) : base(edmAssociationEnd, clrAssociationEnd)
		{
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x00033532 File Offset: 0x00031732
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.AssociationEndMapping;
			}
		}
	}
}
