using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000188 RID: 392
	internal class PreviousPageType
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00054500 File Offset: 0x00052700
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x00054508 File Offset: 0x00052708
		[Filterable(false)]
		public string TypeName { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00054511 File Offset: 0x00052711
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x00054519 File Offset: 0x00052719
		[Filterable(false)]
		[UrlProperty("*.aspx")]
		public string VirtualPath { get; set; }
	}
}
