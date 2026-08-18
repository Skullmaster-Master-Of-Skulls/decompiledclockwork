using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003CB RID: 971
	internal class ObjectAssociationEndMapping : ObjectMemberMapping
	{
		// Token: 0x06002370 RID: 9072 RVA: 0x000A522C File Offset: 0x000A342C
		internal ObjectAssociationEndMapping(AssociationEndMember edmAssociationEnd, AssociationEndMember clrAssociationEnd) : base(edmAssociationEnd, clrAssociationEnd)
		{
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000A5236 File Offset: 0x000A3436
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.AssociationEndMapping;
			}
		}
	}
}
