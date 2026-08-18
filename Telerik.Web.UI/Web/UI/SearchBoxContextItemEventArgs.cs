using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000869 RID: 2153
	public class SearchBoxContextItemEventArgs : EventArgs
	{
		// Token: 0x06004F1B RID: 20251 RVA: 0x000F7FFF File Offset: 0x000F61FF
		public SearchBoxContextItemEventArgs(SearchContextItem item)
		{
			this._item = item;
		}

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x000F800E File Offset: 0x000F620E
		// (set) Token: 0x06004F1D RID: 20253 RVA: 0x000F8016 File Offset: 0x000F6216
		public SearchContextItem Item
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

		// Token: 0x040013BE RID: 5054
		private SearchContextItem _item;
	}
}
