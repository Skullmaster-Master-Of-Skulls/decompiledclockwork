using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020001AC RID: 428
	internal class DataGridViewCellLinkedList : IEnumerable
	{
		// Token: 0x06001E44 RID: 7748 RVA: 0x0008F19A File Offset: 0x0008D39A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DataGridViewCellLinkedListEnumerator(this.headElement);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0008F1A7 File Offset: 0x0008D3A7
		public DataGridViewCellLinkedList()
		{
			this.lastAccessedIndex = -1;
		}

		// Token: 0x17000695 RID: 1685
		public DataGridViewCell this[int index]
		{
			get
			{
				if (this.lastAccessedIndex == -1 || index < this.lastAccessedIndex)
				{
					DataGridViewCellLinkedListElement next = this.headElement;
					for (int i = index; i > 0; i--)
					{
						next = next.Next;
					}
					this.lastAccessedElement = next;
					this.lastAccessedIndex = index;
					return next.DataGridViewCell;
				}
				while (this.lastAccessedIndex < index)
				{
					this.lastAccessedElement = this.lastAccessedElement.Next;
					this.lastAccessedIndex++;
				}
				return this.lastAccessedElement.DataGridViewCell;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0008F239 File Offset: 0x0008D439
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0008F241 File Offset: 0x0008D441
		public DataGridViewCell HeadCell
		{
			get
			{
				return this.headElement.DataGridViewCell;
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0008F250 File Offset: 0x0008D450
		public void Add(DataGridViewCell dataGridViewCell)
		{
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement = new DataGridViewCellLinkedListElement(dataGridViewCell);
			if (this.headElement != null)
			{
				dataGridViewCellLinkedListElement.Next = this.headElement;
			}
			this.headElement = dataGridViewCellLinkedListElement;
			this.count++;
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0008F29B File Offset: 0x0008D49B
		public void Clear()
		{
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
			this.headElement = null;
			this.count = 0;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0008F2BC File Offset: 0x0008D4BC
		public bool Contains(DataGridViewCell dataGridViewCell)
		{
			int num = 0;
			DataGridViewCellLinkedListElement next = this.headElement;
			while (next != null)
			{
				if (next.DataGridViewCell == dataGridViewCell)
				{
					this.lastAccessedElement = next;
					this.lastAccessedIndex = num;
					return true;
				}
				next = next.Next;
				num++;
			}
			return false;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0008F2FC File Offset: 0x0008D4FC
		public bool Remove(DataGridViewCell dataGridViewCell)
		{
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement = null;
			DataGridViewCellLinkedListElement next = this.headElement;
			while (next != null && next.DataGridViewCell != dataGridViewCell)
			{
				dataGridViewCellLinkedListElement = next;
				next = next.Next;
			}
			if (next.DataGridViewCell == dataGridViewCell)
			{
				DataGridViewCellLinkedListElement next2 = next.Next;
				if (dataGridViewCellLinkedListElement == null)
				{
					this.headElement = next2;
				}
				else
				{
					dataGridViewCellLinkedListElement.Next = next2;
				}
				this.count--;
				this.lastAccessedElement = null;
				this.lastAccessedIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0008F36C File Offset: 0x0008D56C
		public int RemoveAllCellsAtBand(bool column, int bandIndex)
		{
			int num = 0;
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement = null;
			DataGridViewCellLinkedListElement dataGridViewCellLinkedListElement2 = this.headElement;
			while (dataGridViewCellLinkedListElement2 != null)
			{
				if ((column && dataGridViewCellLinkedListElement2.DataGridViewCell.ColumnIndex == bandIndex) || (!column && dataGridViewCellLinkedListElement2.DataGridViewCell.RowIndex == bandIndex))
				{
					DataGridViewCellLinkedListElement next = dataGridViewCellLinkedListElement2.Next;
					if (dataGridViewCellLinkedListElement == null)
					{
						this.headElement = next;
					}
					else
					{
						dataGridViewCellLinkedListElement.Next = next;
					}
					dataGridViewCellLinkedListElement2 = next;
					this.count--;
					this.lastAccessedElement = null;
					this.lastAccessedIndex = -1;
					num++;
				}
				else
				{
					dataGridViewCellLinkedListElement = dataGridViewCellLinkedListElement2;
					dataGridViewCellLinkedListElement2 = dataGridViewCellLinkedListElement2.Next;
				}
			}
			return num;
		}

		// Token: 0x04000CCC RID: 3276
		private DataGridViewCellLinkedListElement lastAccessedElement;

		// Token: 0x04000CCD RID: 3277
		private DataGridViewCellLinkedListElement headElement;

		// Token: 0x04000CCE RID: 3278
		private int count;

		// Token: 0x04000CCF RID: 3279
		private int lastAccessedIndex;
	}
}
