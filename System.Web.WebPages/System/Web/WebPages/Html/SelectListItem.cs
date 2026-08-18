using System;

namespace System.Web.WebPages.Html
{
	// Token: 0x02000072 RID: 114
	public class SelectListItem
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
		public SelectListItem()
		{
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000BFAA File Offset: 0x0000A1AA
		public SelectListItem(SelectListItem item)
		{
			this.Text = item.Text;
			this.Value = item.Value;
			this.Selected = item.Selected;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000BFD6 File Offset: 0x0000A1D6
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000BFDE File Offset: 0x0000A1DE
		public string Text { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000BFE7 File Offset: 0x0000A1E7
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000BFEF File Offset: 0x0000A1EF
		public string Value { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000C000 File Offset: 0x0000A200
		public bool Selected { get; set; }
	}
}
