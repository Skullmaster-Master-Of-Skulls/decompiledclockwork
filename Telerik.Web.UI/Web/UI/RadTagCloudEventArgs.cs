using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000FA2 RID: 4002
	public class RadTagCloudEventArgs : EventArgs
	{
		// Token: 0x060099AC RID: 39340 RVA: 0x00224D93 File Offset: 0x00222F93
		public RadTagCloudEventArgs(RadTagCloudItem item)
		{
			this.Item = item;
		}

		// Token: 0x170030A3 RID: 12451
		// (get) Token: 0x060099AD RID: 39341 RVA: 0x00224DA2 File Offset: 0x00222FA2
		// (set) Token: 0x060099AE RID: 39342 RVA: 0x00224DAA File Offset: 0x00222FAA
		public RadTagCloudItem Item
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

		// Token: 0x04002BA8 RID: 11176
		private RadTagCloudItem _item;
	}
}
