using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AB7 RID: 6839
	public class RadSiteMapNodeEventArgs : EventArgs
	{
		// Token: 0x06010891 RID: 67729 RVA: 0x003B0FEC File Offset: 0x003AF1EC
		public RadSiteMapNodeEventArgs(RadSiteMapNode node)
		{
			this.Node = node;
		}

		// Token: 0x17005061 RID: 20577
		// (get) Token: 0x06010892 RID: 67730 RVA: 0x003B0FFB File Offset: 0x003AF1FB
		// (set) Token: 0x06010893 RID: 67731 RVA: 0x003B1003 File Offset: 0x003AF203
		public RadSiteMapNode Node { get; set; }
	}
}
