using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B47 RID: 6983
	public class RadPanelBarEventArgs : EventArgs
	{
		// Token: 0x06010E1D RID: 69149 RVA: 0x003BE3F9 File Offset: 0x003BC5F9
		public RadPanelBarEventArgs(RadPanelItem item)
		{
			this.Item = item;
		}

		// Token: 0x17005258 RID: 21080
		// (get) Token: 0x06010E1E RID: 69150 RVA: 0x003BE408 File Offset: 0x003BC608
		// (set) Token: 0x06010E1F RID: 69151 RVA: 0x003BE410 File Offset: 0x003BC610
		public RadPanelItem Item
		{
			get
			{
				return this._item;
			}
			private set
			{
				this._item = value;
			}
		}

		// Token: 0x04004B93 RID: 19347
		private RadPanelItem _item;
	}
}
