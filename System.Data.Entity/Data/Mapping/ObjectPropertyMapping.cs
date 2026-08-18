using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000237 RID: 567
	internal class ObjectPropertyMapping : ObjectMemberMapping
	{
		// Token: 0x06002407 RID: 9223 RVA: 0x000815F6 File Offset: 0x0007F7F6
		internal ObjectPropertyMapping(EdmProperty edmProperty, EdmProperty clrProperty) : base(edmProperty, clrProperty)
		{
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x00082744 File Offset: 0x00080944
		internal EdmProperty ClrProperty
		{
			get
			{
				return (EdmProperty)base.ClrMember;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.ScalarPropertyMapping;
			}
		}
	}
}
