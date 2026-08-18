using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x0200022E RID: 558
	internal class ObjectNavigationPropertyMapping : ObjectMemberMapping
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x000815F6 File Offset: 0x0007F7F6
		internal ObjectNavigationPropertyMapping(NavigationProperty edmNavigationProperty, NavigationProperty clrNavigationProperty) : base(edmNavigationProperty, clrNavigationProperty)
		{
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x00017938 File Offset: 0x00015B38
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.NavigationPropertyMapping;
			}
		}
	}
}
