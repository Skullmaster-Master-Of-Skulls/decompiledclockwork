using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000187 RID: 391
	[SchemaElementName("Page")]
	internal class Page4_0 : Page
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x000544BC File Offset: 0x000526BC
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x000544C4 File Offset: 0x000526C4
		[Browsable(false)]
		[Filterable(false)]
		public string Description { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000544CD File Offset: 0x000526CD
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x000544D5 File Offset: 0x000526D5
		public string MetaDescription { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000544DE File Offset: 0x000526DE
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x000544E6 File Offset: 0x000526E6
		public string MetaKeywords { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000544EF File Offset: 0x000526EF
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x000544F7 File Offset: 0x000526F7
		public ViewStateMode ViewStateMode { get; set; }
	}
}
