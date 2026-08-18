using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000177 RID: 375
	public class ReorderListItemReorderEventArgs : ReorderListItemEventArgs
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001B64C File Offset: 0x0001984C
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0001B654 File Offset: 0x00019854
		public int OldIndex
		{
			get
			{
				return this._oldIndex;
			}
			set
			{
				this._oldIndex = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001B65D File Offset: 0x0001985D
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0001B665 File Offset: 0x00019865
		public int NewIndex
		{
			get
			{
				return this._newIndex;
			}
			set
			{
				this._newIndex = value;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001B66E File Offset: 0x0001986E
		internal ReorderListItemReorderEventArgs(ReorderListItem item, int oldIndex, int newIndex) : base(item)
		{
			this._oldIndex = oldIndex;
			this._newIndex = newIndex;
		}

		// Token: 0x040003F7 RID: 1015
		private int _oldIndex;

		// Token: 0x040003F8 RID: 1016
		private int _newIndex;
	}
}
