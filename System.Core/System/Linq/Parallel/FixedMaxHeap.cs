using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001FA RID: 506
	internal class FixedMaxHeap<TElement>
	{
		// Token: 0x06001017 RID: 4119 RVA: 0x00038C5C File Offset: 0x00036E5C
		internal FixedMaxHeap(int maximumSize) : this(maximumSize, Util.GetDefaultComparer<TElement>())
		{
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00038C6A File Offset: 0x00036E6A
		internal FixedMaxHeap(int maximumSize, IComparer<TElement> comparer)
		{
			this.m_elements = new TElement[maximumSize];
			this.m_comparer = comparer;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00038C85 File Offset: 0x00036E85
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00038C8D File Offset: 0x00036E8D
		internal int Size
		{
			get
			{
				return this.m_elements.Length;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00038C97 File Offset: 0x00036E97
		internal TElement MaxValue
		{
			get
			{
				if (this.m_count == 0)
				{
					throw new InvalidOperationException(SR.GetString("NoElements"));
				}
				return this.m_elements[0];
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00038CBD File Offset: 0x00036EBD
		internal void Clear()
		{
			this.m_count = 0;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00038CC8 File Offset: 0x00036EC8
		internal bool Insert(TElement e)
		{
			if (this.m_count < this.m_elements.Length)
			{
				this.m_elements[this.m_count] = e;
				this.m_count++;
				this.HeapifyLastLeaf();
				return true;
			}
			if (this.m_comparer.Compare(e, this.m_elements[0]) < 0)
			{
				this.m_elements[0] = e;
				this.HeapifyRoot();
				return true;
			}
			return false;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00038D3E File Offset: 0x00036F3E
		internal void ReplaceMax(TElement newValue)
		{
			this.m_elements[0] = newValue;
			this.HeapifyRoot();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00038D53 File Offset: 0x00036F53
		internal void RemoveMax()
		{
			this.m_count--;
			if (this.m_count > 0)
			{
				this.m_elements[0] = this.m_elements[this.m_count];
				this.HeapifyRoot();
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00038D90 File Offset: 0x00036F90
		private void Swap(int i, int j)
		{
			TElement telement = this.m_elements[i];
			this.m_elements[i] = this.m_elements[j];
			this.m_elements[j] = telement;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00038DD0 File Offset: 0x00036FD0
		private void HeapifyRoot()
		{
			int i = 0;
			int count = this.m_count;
			while (i < count)
			{
				int num = (i + 1) * 2 - 1;
				int num2 = num + 1;
				if (num < count && this.m_comparer.Compare(this.m_elements[i], this.m_elements[num]) < 0)
				{
					if (num2 < count && this.m_comparer.Compare(this.m_elements[num], this.m_elements[num2]) < 0)
					{
						this.Swap(i, num2);
						i = num2;
					}
					else
					{
						this.Swap(i, num);
						i = num;
					}
				}
				else
				{
					if (num2 >= count || this.m_comparer.Compare(this.m_elements[i], this.m_elements[num2]) >= 0)
					{
						break;
					}
					this.Swap(i, num2);
					i = num2;
				}
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00038EA0 File Offset: 0x000370A0
		private void HeapifyLastLeaf()
		{
			int num;
			for (int i = this.m_count - 1; i > 0; i = num)
			{
				num = (i + 1) / 2 - 1;
				if (this.m_comparer.Compare(this.m_elements[i], this.m_elements[num]) <= 0)
				{
					break;
				}
				this.Swap(i, num);
			}
		}

		// Token: 0x04000925 RID: 2341
		private TElement[] m_elements;

		// Token: 0x04000926 RID: 2342
		private int m_count;

		// Token: 0x04000927 RID: 2343
		private IComparer<TElement> m_comparer;
	}
}
