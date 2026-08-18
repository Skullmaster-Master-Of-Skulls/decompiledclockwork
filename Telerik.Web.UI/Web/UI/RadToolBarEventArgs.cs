using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AE2 RID: 6882
	public class RadToolBarEventArgs : EventArgs
	{
		// Token: 0x17005126 RID: 20774
		// (get) Token: 0x06010AD4 RID: 68308 RVA: 0x003B75FF File Offset: 0x003B57FF
		public RadToolBarItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x06010AD5 RID: 68309 RVA: 0x003B7607 File Offset: 0x003B5807
		public RadToolBarEventArgs(RadToolBarItem item)
		{
			this._item = item;
		}

		// Token: 0x04004A66 RID: 19046
		private RadToolBarItem _item;
	}
}
