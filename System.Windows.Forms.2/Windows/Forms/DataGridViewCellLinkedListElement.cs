using System;

namespace System.Windows.Forms
{
	// Token: 0x020001AE RID: 430
	internal class DataGridViewCellLinkedListElement
	{
		// Token: 0x06001E52 RID: 7762 RVA: 0x0008F45F File Offset: 0x0008D65F
		public DataGridViewCellLinkedListElement(DataGridViewCell dataGridViewCell)
		{
			this.dataGridViewCell = dataGridViewCell;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0008F46E File Offset: 0x0008D66E
		public DataGridViewCell DataGridViewCell
		{
			get
			{
				return this.dataGridViewCell;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0008F476 File Offset: 0x0008D676
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x0008F47E File Offset: 0x0008D67E
		public DataGridViewCellLinkedListElement Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x04000CD3 RID: 3283
		private DataGridViewCell dataGridViewCell;

		// Token: 0x04000CD4 RID: 3284
		private DataGridViewCellLinkedListElement next;
	}
}
