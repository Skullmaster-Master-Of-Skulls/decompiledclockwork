using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001933 RID: 6451
	public class RadListBoxItemEventArgs : EventArgs
	{
		// Token: 0x0600F9A1 RID: 63905 RVA: 0x0038510C File Offset: 0x0038330C
		public RadListBoxItemEventArgs(RadListBoxItem item)
		{
			this.Item = item;
		}

		// Token: 0x17004B62 RID: 19298
		// (get) Token: 0x0600F9A2 RID: 63906 RVA: 0x0038511B File Offset: 0x0038331B
		// (set) Token: 0x0600F9A3 RID: 63907 RVA: 0x00385123 File Offset: 0x00383323
		public RadListBoxItem Item { get; set; }
	}
}
