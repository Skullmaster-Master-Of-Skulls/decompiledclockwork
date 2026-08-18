using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AC8 RID: 6856
	public class RadSliderItemEventArgs : EventArgs
	{
		// Token: 0x06010986 RID: 67974 RVA: 0x003B36FE File Offset: 0x003B18FE
		public RadSliderItemEventArgs(RadSliderItem item)
		{
			this._item = item;
		}

		// Token: 0x170050AD RID: 20653
		// (get) Token: 0x06010987 RID: 67975 RVA: 0x003B370D File Offset: 0x003B190D
		// (set) Token: 0x06010988 RID: 67976 RVA: 0x003B3715 File Offset: 0x003B1915
		public RadSliderItem Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x04004A25 RID: 18981
		private RadSliderItem _item;
	}
}
