using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B7 RID: 1207
	public class RepeaterItemEventArgs : EventArgs
	{
		// Token: 0x06003C5D RID: 15453 RVA: 0x000C39C5 File Offset: 0x000C1BC5
		public RepeaterItemEventArgs(RepeaterItem item)
		{
			this.item = item;
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x000C39D4 File Offset: 0x000C1BD4
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04002379 RID: 9081
		private RepeaterItem item;
	}
}
