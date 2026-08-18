using System;
using Telerik.Web.UI.AutoCompleteBox;

namespace Telerik.Web.UI
{
	// Token: 0x020009BF RID: 2495
	public class AutoCompleteDropDownItemEventArgs : EventArgs
	{
		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x00122763 File Offset: 0x00120963
		// (set) Token: 0x06005F59 RID: 24409 RVA: 0x0012276B File Offset: 0x0012096B
		public DropDownItem Item
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

		// Token: 0x06005F5A RID: 24410 RVA: 0x00122774 File Offset: 0x00120974
		public AutoCompleteDropDownItemEventArgs(DropDownItem item)
		{
			this._item = item;
		}

		// Token: 0x040016F8 RID: 5880
		private DropDownItem _item;
	}
}
