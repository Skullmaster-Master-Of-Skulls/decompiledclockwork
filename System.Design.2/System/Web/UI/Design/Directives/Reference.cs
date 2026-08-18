using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000189 RID: 393
	internal class Reference
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00054522 File Offset: 0x00052722
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0005452A File Offset: 0x0005272A
		[Filterable(false)]
		[UrlProperty("*.aspx")]
		public string Page { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00054533 File Offset: 0x00052733
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x0005453B File Offset: 0x0005273B
		[Filterable(false)]
		[UrlProperty("*.ascx")]
		public string Control { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00054544 File Offset: 0x00052744
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0005454C File Offset: 0x0005274C
		[Filterable(false)]
		[UrlProperty("*.aspx;*.ascx")]
		public string VirtualPath { get; set; }
	}
}
