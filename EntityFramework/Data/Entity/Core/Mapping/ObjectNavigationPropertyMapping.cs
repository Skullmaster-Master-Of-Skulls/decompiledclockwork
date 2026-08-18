using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003CF RID: 975
	internal class ObjectNavigationPropertyMapping : ObjectMemberMapping
	{
		// Token: 0x06002377 RID: 9079 RVA: 0x000A5260 File Offset: 0x000A3460
		internal ObjectNavigationPropertyMapping(NavigationProperty edmNavigationProperty, NavigationProperty clrNavigationProperty) : base(edmNavigationProperty, clrNavigationProperty)
		{
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x000A526A File Offset: 0x000A346A
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.NavigationPropertyMapping;
			}
		}
	}
}
