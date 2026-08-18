using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004CB RID: 1227
	public class SiteMapNodeItemEventArgs : EventArgs
	{
		// Token: 0x06003CF3 RID: 15603 RVA: 0x000C5011 File Offset: 0x000C3211
		public SiteMapNodeItemEventArgs(SiteMapNodeItem item)
		{
			this._item = item;
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000C5020 File Offset: 0x000C3220
		public SiteMapNodeItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x040023A0 RID: 9120
		private SiteMapNodeItem _item;
	}
}
