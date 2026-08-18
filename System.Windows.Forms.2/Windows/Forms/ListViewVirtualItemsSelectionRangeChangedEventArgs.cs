using System;

namespace System.Windows.Forms
{
	// Token: 0x020002E3 RID: 739
	public class ListViewVirtualItemsSelectionRangeChangedEventArgs : EventArgs
	{
		// Token: 0x06002EAA RID: 11946 RVA: 0x000D33D9 File Offset: 0x000D15D9
		public ListViewVirtualItemsSelectionRangeChangedEventArgs(int startIndex, int endIndex, bool isSelected)
		{
			if (startIndex > endIndex)
			{
				throw new ArgumentException(SR.GetString("ListViewStartIndexCannotBeLargerThanEndIndex"));
			}
			this.startIndex = startIndex;
			this.endIndex = endIndex;
			this.isSelected = isSelected;
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000D340A File Offset: 0x000D160A
		public int EndIndex
		{
			get
			{
				return this.endIndex;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000D3412 File Offset: 0x000D1612
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000D341A File Offset: 0x000D161A
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x0400134E RID: 4942
		private int startIndex;

		// Token: 0x0400134F RID: 4943
		private int endIndex;

		// Token: 0x04001350 RID: 4944
		private bool isSelected;
	}
}
