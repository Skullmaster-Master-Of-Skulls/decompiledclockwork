using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000B1E RID: 2846
	public class DropDownListItemEventArgs : EventArgs
	{
		// Token: 0x06006A64 RID: 27236 RVA: 0x0018EB91 File Offset: 0x0018CD91
		public DropDownListItemEventArgs(DropDownListItem item)
		{
			this.Item = item;
		}

		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x06006A65 RID: 27237 RVA: 0x0018EBA0 File Offset: 0x0018CDA0
		// (set) Token: 0x06006A66 RID: 27238 RVA: 0x0018EBA8 File Offset: 0x0018CDA8
		public DropDownListItem Item { get; set; }
	}
}
