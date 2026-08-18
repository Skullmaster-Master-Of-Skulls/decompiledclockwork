using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003CC RID: 972
	internal class ObjectPropertyMapping : ObjectMemberMapping
	{
		// Token: 0x06002372 RID: 9074 RVA: 0x000A5239 File Offset: 0x000A3439
		internal ObjectPropertyMapping(EdmProperty edmProperty, EdmProperty clrProperty) : base(edmProperty, clrProperty)
		{
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000A5243 File Offset: 0x000A3443
		internal EdmProperty ClrProperty
		{
			get
			{
				return (EdmProperty)base.ClrMember;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000A5250 File Offset: 0x000A3450
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.ScalarPropertyMapping;
			}
		}
	}
}
