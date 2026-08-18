using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000156 RID: 342
	internal class Set<TElement>
	{
		// Token: 0x06000C0E RID: 3086 RVA: 0x0002CA5F File Offset: 0x0002AC5F
		public Set() : this(null)
		{
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002CA68 File Offset: 0x0002AC68
		public Set(IEqualityComparer<TElement> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TElement>.Default;
			}
			this.comparer = comparer;
			this.buckets = new int[7];
			this.slots = new Set<TElement>.Slot[7];
			this.freeList = -1;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002CAA0 File Offset: 0x0002ACA0
		public bool Add(TElement value)
		{
			return !this.Find(value, true);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002CAAD File Offset: 0x0002ACAD
		public bool Contains(TElement value)
		{
			return this.Find(value, false);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
		public bool Remove(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this.buckets.Length;
			int num3 = -1;
			for (int i = this.buckets[num2] - 1; i >= 0; i = this.slots[i].next)
			{
				if (this.slots[i].hashCode == num && this.comparer.Equals(this.slots[i].value, value))
				{
					if (num3 < 0)
					{
						this.buckets[num2] = this.slots[i].next + 1;
					}
					else
					{
						this.slots[num3].next = this.slots[i].next;
					}
					this.slots[i].hashCode = -1;
					this.slots[i].value = default(TElement);
					this.slots[i].next = this.freeList;
					this.freeList = i;
					return true;
				}
				num3 = i;
			}
			return false;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002CBCC File Offset: 0x0002ADCC
		private bool Find(TElement value, bool add)
		{
			int num = this.InternalGetHashCode(value);
			for (int i = this.buckets[num % this.buckets.Length] - 1; i >= 0; i = this.slots[i].next)
			{
				if (this.slots[i].hashCode == num && this.comparer.Equals(this.slots[i].value, value))
				{
					return true;
				}
			}
			if (add)
			{
				int num2;
				if (this.freeList >= 0)
				{
					num2 = this.freeList;
					this.freeList = this.slots[num2].next;
				}
				else
				{
					if (this.count == this.slots.Length)
					{
						this.Resize();
					}
					num2 = this.count;
					this.count++;
				}
				int num3 = num % this.buckets.Length;
				this.slots[num2].hashCode = num;
				this.slots[num2].value = value;
				this.slots[num2].next = this.buckets[num3] - 1;
				this.buckets[num3] = num2 + 1;
			}
			return false;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002CCF4 File Offset: 0x0002AEF4
		private void Resize()
		{
			int num = checked(this.count * 2 + 1);
			int[] array = new int[num];
			Set<TElement>.Slot[] array2 = new Set<TElement>.Slot[num];
			Array.Copy(this.slots, 0, array2, 0, this.count);
			for (int i = 0; i < this.count; i++)
			{
				int num2 = array2[i].hashCode % num;
				array2[i].next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this.buckets = array;
			this.slots = array2;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002CD76 File Offset: 0x0002AF76
		internal int InternalGetHashCode(TElement value)
		{
			if (value != null)
			{
				return this.comparer.GetHashCode(value) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x04000780 RID: 1920
		private int[] buckets;

		// Token: 0x04000781 RID: 1921
		private Set<TElement>.Slot[] slots;

		// Token: 0x04000782 RID: 1922
		private int count;

		// Token: 0x04000783 RID: 1923
		private int freeList;

		// Token: 0x04000784 RID: 1924
		private IEqualityComparer<TElement> comparer;

		// Token: 0x0200039B RID: 923
		internal struct Slot
		{
			// Token: 0x040010D7 RID: 4311
			internal int hashCode;

			// Token: 0x040010D8 RID: 4312
			internal TElement value;

			// Token: 0x040010D9 RID: 4313
			internal int next;
		}
	}
}
