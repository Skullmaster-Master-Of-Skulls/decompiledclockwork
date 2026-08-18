using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001951 RID: 6481
	public class RadDataPagerFieldCreatedEventArgs : EventArgs
	{
		// Token: 0x17004BC0 RID: 19392
		// (get) Token: 0x0600FAB0 RID: 64176 RVA: 0x00386E0E File Offset: 0x0038500E
		public RadDataPagerFieldItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0600FAB1 RID: 64177 RVA: 0x00386E16 File Offset: 0x00385016
		public RadDataPagerFieldCreatedEventArgs(RadDataPagerFieldItem item)
		{
			this._item = item;
		}

		// Token: 0x04004755 RID: 18261
		private RadDataPagerFieldItem _item;
	}
}
