using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000202 RID: 514
	internal class DataGridViewIntLinkedList : IEnumerable
	{
		// Token: 0x0600216F RID: 8559 RVA: 0x0009DDEF File Offset: 0x0009BFEF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DataGridViewIntLinkedListEnumerator(this.headElement);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0009DDFC File Offset: 0x0009BFFC
		public DataGridViewIntLinkedList()
		{
			this.lastAccessedIndex = -1;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public DataGridViewIntLinkedList(DataGridViewIntLinkedList source)
		{
			int num = source.Count;
			for (int i = 0; i < num; i++)
			{
				this.Add(source[i]);
			}
		}

		// Token: 0x17000783 RID: 1923
		public int this[int index]
		{
			get
			{
				if (this.lastAccessedIndex == -1 || index < this.lastAccessedIndex)
				{
					DataGridViewIntLinkedListElement next = this.headElement;
					for (int i = index; i > 0; i--)
					{
						next = next.Next;
					}
					this.lastAccessedElement = next;
					this.lastAccessedIndex = index;
					return next.Int;
				}
				while (this.lastAccessedIndex < index)
				{
					this.lastAccessedElement = this.lastAccessedElement.Next;
					this.lastAccessedIndex++;
				}
				return this.lastAccessedElement.Int;
			}
			set
			{
				if (index != this.lastAccessedIndex)
				{
					int num = this[index];
				}
				this.lastAccessedElement.Int = value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x0009DEEE File Offset: 0x0009C0EE
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x0009DEF6 File Offset: 0x0009C0F6
		public int HeadInt
		{
			get
			{
				return this.headElement.Int;
			}
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0009DF04 File Offset: 0x0009C104
		public void Add(int integer)
		{
			DataGridViewIntLinkedListElement dataGridViewIntLinkedListElement = new DataGridViewIntLinkedListElement(integer);
			if (this.headElement != null)
			{
				dataGridViewIntLinkedListElement.Next = this.headElement;
			}
			this.headElement = dataGridViewIntLinkedListElement;
			this.count++;
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0009DF4F File Offset: 0x0009C14F
		public void Clear()
		{
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
			this.headElement = null;
			this.count = 0;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0009DF70 File Offset: 0x0009C170
		public bool Contains(int integer)
		{
			int num = 0;
			DataGridViewIntLinkedListElement next = this.headElement;
			while (next != null)
			{
				if (next.Int == integer)
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

		// Token: 0x06002179 RID: 8569 RVA: 0x0009DFB0 File Offset: 0x0009C1B0
		public int IndexOf(int integer)
		{
			if (this.Contains(integer))
			{
				return this.lastAccessedIndex;
			}
			return -1;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0009DFC4 File Offset: 0x0009C1C4
		public bool Remove(int integer)
		{
			DataGridViewIntLinkedListElement dataGridViewIntLinkedListElement = null;
			DataGridViewIntLinkedListElement next = this.headElement;
			while (next != null && next.Int != integer)
			{
				dataGridViewIntLinkedListElement = next;
				next = next.Next;
			}
			if (next.Int == integer)
			{
				DataGridViewIntLinkedListElement next2 = next.Next;
				if (dataGridViewIntLinkedListElement == null)
				{
					this.headElement = next2;
				}
				else
				{
					dataGridViewIntLinkedListElement.Next = next2;
				}
				this.count--;
				this.lastAccessedElement = null;
				this.lastAccessedIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0009E034 File Offset: 0x0009C234
		public void RemoveAt(int index)
		{
			DataGridViewIntLinkedListElement dataGridViewIntLinkedListElement = null;
			DataGridViewIntLinkedListElement next = this.headElement;
			while (index > 0)
			{
				dataGridViewIntLinkedListElement = next;
				next = next.Next;
				index--;
			}
			DataGridViewIntLinkedListElement next2 = next.Next;
			if (dataGridViewIntLinkedListElement == null)
			{
				this.headElement = next2;
			}
			else
			{
				dataGridViewIntLinkedListElement.Next = next2;
			}
			this.count--;
			this.lastAccessedElement = null;
			this.lastAccessedIndex = -1;
		}

		// Token: 0x04000DFC RID: 3580
		private DataGridViewIntLinkedListElement lastAccessedElement;

		// Token: 0x04000DFD RID: 3581
		private DataGridViewIntLinkedListElement headElement;

		// Token: 0x04000DFE RID: 3582
		private int count;

		// Token: 0x04000DFF RID: 3583
		private int lastAccessedIndex;
	}
}
