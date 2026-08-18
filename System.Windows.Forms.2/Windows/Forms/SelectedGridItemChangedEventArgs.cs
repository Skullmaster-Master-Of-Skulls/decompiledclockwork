using System;

namespace System.Windows.Forms
{
	// Token: 0x02000364 RID: 868
	public class SelectedGridItemChangedEventArgs : EventArgs
	{
		// Token: 0x06003885 RID: 14469 RVA: 0x000FAB00 File Offset: 0x000F8D00
		public SelectedGridItemChangedEventArgs(GridItem oldSel, GridItem newSel)
		{
			this.oldSelection = oldSel;
			this.newSelection = newSel;
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000FAB16 File Offset: 0x000F8D16
		public GridItem NewSelection
		{
			get
			{
				return this.newSelection;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000FAB1E File Offset: 0x000F8D1E
		public GridItem OldSelection
		{
			get
			{
				return this.oldSelection;
			}
		}

		// Token: 0x040021D2 RID: 8658
		private GridItem oldSelection;

		// Token: 0x040021D3 RID: 8659
		private GridItem newSelection;
	}
}
