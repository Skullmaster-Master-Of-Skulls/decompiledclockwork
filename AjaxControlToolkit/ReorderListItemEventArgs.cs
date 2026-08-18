using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000176 RID: 374
	public class ReorderListItemEventArgs : EventArgs
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0001B62C File Offset: 0x0001982C
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0001B634 File Offset: 0x00019834
		public ReorderListItem Item
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

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001B63D File Offset: 0x0001983D
		public ReorderListItemEventArgs(ReorderListItem item)
		{
			this._item = item;
		}

		// Token: 0x040003F6 RID: 1014
		private ReorderListItem _item;
	}
}
