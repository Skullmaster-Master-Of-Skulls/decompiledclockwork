using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x020006FD RID: 1789
	internal struct OlapExpressionOptions
	{
		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000C965F File Offset: 0x000C785F
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000C9667 File Offset: 0x000C7867
		public OlapHierarchyFieldInfo HierarchyInfo { get; set; }

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000C9670 File Offset: 0x000C7870
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000C9678 File Offset: 0x000C7878
		public OlapFieldInfo MemberInfo { get; set; }

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000C9681 File Offset: 0x000C7881
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x000C9689 File Offset: 0x000C7889
		public OlapExpression DimensionExpression { get; set; }

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000C9692 File Offset: 0x000C7892
		// (set) Token: 0x06003F94 RID: 16276 RVA: 0x000C969A File Offset: 0x000C789A
		public bool UseHierarchyAsAccess { get; set; }

		// Token: 0x040010D9 RID: 4313
		public static OlapExpressionOptions Default = default(OlapExpressionOptions);
	}
}
