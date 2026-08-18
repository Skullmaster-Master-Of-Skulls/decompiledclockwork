using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000602 RID: 1538
	public class RadMultiSelectItemEventArgs : EventArgs
	{
		// Token: 0x0600377C RID: 14204 RVA: 0x000B77BE File Offset: 0x000B59BE
		public RadMultiSelectItemEventArgs(MultiSelectItem item)
		{
			this.Item = item;
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000B77CD File Offset: 0x000B59CD
		// (set) Token: 0x0600377E RID: 14206 RVA: 0x000B77D5 File Offset: 0x000B59D5
		public MultiSelectItem Item { get; set; }
	}
}
