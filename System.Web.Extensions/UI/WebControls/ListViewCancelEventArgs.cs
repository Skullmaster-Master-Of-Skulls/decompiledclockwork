using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A9 RID: 169
	public class ListViewCancelEventArgs : CancelEventArgs
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x00022175 File Offset: 0x00020375
		public ListViewCancelEventArgs(int itemIndex, ListViewCancelMode cancelMode) : base(false)
		{
			this._itemIndex = itemIndex;
			this._cancelMode = cancelMode;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0002218C File Offset: 0x0002038C
		public int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00022194 File Offset: 0x00020394
		public ListViewCancelMode CancelMode
		{
			get
			{
				return this._cancelMode;
			}
		}

		// Token: 0x040002D0 RID: 720
		private int _itemIndex;

		// Token: 0x040002D1 RID: 721
		private ListViewCancelMode _cancelMode;
	}
}
