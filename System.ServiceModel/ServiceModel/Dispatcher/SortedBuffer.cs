using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000508 RID: 1288
	internal struct SortedBuffer<T, C> where C : IComparer<T>
	{
		// Token: 0x060030B8 RID: 12472 RVA: 0x000BAC4C File Offset: 0x000B8E4C
		internal SortedBuffer(C comparerInstance)
		{
			this.size = 0;
			this.buffer = null;
			if (SortedBuffer<T, C>.Comparer == null)
			{
				SortedBuffer<T, C>.Comparer = new SortedBuffer<T, C>.DefaultComparer(comparerInstance);
			}
		}

		// Token: 0x17000B88 RID: 2952
		internal T this[int index]
		{
			get
			{
				return this.GetAt(index);
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (set) Token: 0x060030BA RID: 12474 RVA: 0x000BAC77 File Offset: 0x000B8E77
		internal int Capacity
		{
			set
			{
				if (this.buffer != null)
				{
					if (value != this.buffer.Length)
					{
						if (value > 0)
						{
							Array.Resize<T>(ref this.buffer, value);
							return;
						}
						this.buffer = null;
						return;
					}
				}
				else
				{
					this.buffer = new T[value];
				}
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000BACB1 File Offset: 0x000B8EB1
		internal int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000BACBC File Offset: 0x000B8EBC
		internal int Add(T item)
		{
			int num = this.Search(item);
			if (num < 0)
			{
				num = ~num;
				this.InsertAt(num, item);
			}
			return num;
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000BACE1 File Offset: 0x000B8EE1
		internal void Clear()
		{
			this.size = 0;
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000BACEC File Offset: 0x000B8EEC
		internal void Exchange(T old, T replace)
		{
			if (SortedBuffer<T, C>.Comparer.Compare(old, replace) != 0)
			{
				this.Remove(old);
				this.Insert(replace);
				return;
			}
			int num = this.IndexOf(old);
			if (num >= 0)
			{
				this.buffer[num] = replace;
				return;
			}
			this.Insert(replace);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000BAD3A File Offset: 0x000B8F3A
		internal T GetAt(int index)
		{
			return this.buffer[index];
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000BAD48 File Offset: 0x000B8F48
		internal int IndexOf(T item)
		{
			return this.Search(item);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000BAD51 File Offset: 0x000B8F51
		internal int IndexOfKey<K>(K key, IItemComparer<K, T> itemComp)
		{
			return this.Search<K>(key, itemComp);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000BAD5C File Offset: 0x000B8F5C
		internal int Insert(T item)
		{
			int num = this.Search(item);
			if (num >= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new ArgumentException(SR.GetString("QueryItemAlreadyExists")));
			}
			this.InsertAt(~num, item);
			return ~num;
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000BAD9C File Offset: 0x000B8F9C
		private void InsertAt(int index, T item)
		{
			if (this.buffer == null)
			{
				this.buffer = new T[1];
			}
			else if (this.buffer.Length == this.size)
			{
				T[] destinationArray = new T[this.size + 1];
				if (index == 0)
				{
					Array.Copy(this.buffer, 0, destinationArray, 1, this.size);
				}
				else if (index == this.size)
				{
					Array.Copy(this.buffer, 0, destinationArray, 0, this.size);
				}
				else
				{
					Array.Copy(this.buffer, 0, destinationArray, 0, index);
					Array.Copy(this.buffer, index, destinationArray, index + 1, this.size - index);
				}
				this.buffer = destinationArray;
			}
			else
			{
				Array.Copy(this.buffer, index, this.buffer, index + 1, this.size - index);
			}
			this.buffer[index] = item;
			this.size++;
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000BAE80 File Offset: 0x000B9080
		internal bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000BAEA4 File Offset: 0x000B90A4
		internal void RemoveAt(int index)
		{
			if (index < this.size - 1)
			{
				Array.Copy(this.buffer, index + 1, this.buffer, index, this.size - index - 1);
			}
			T[] array = this.buffer;
			int num = this.size - 1;
			this.size = num;
			array[num] = default(T);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000BAF00 File Offset: 0x000B9100
		private int Search(T item)
		{
			if (this.size == 0)
			{
				return -1;
			}
			return this.Search<T>(item, SortedBuffer<T, C>.Comparer);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000BAF18 File Offset: 0x000B9118
		private int Search<K>(K key, IItemComparer<K, T> comparer)
		{
			if (this.size <= 8)
			{
				return this.LinearSearch<K>(key, comparer, 0, this.size);
			}
			return this.BinarySearch<K>(key, comparer);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000BAF3C File Offset: 0x000B913C
		private int BinarySearch<K>(K key, IItemComparer<K, T> comparer)
		{
			int num = 0;
			int num2 = this.size;
			while (num2 - num > 8)
			{
				int num3 = (num2 + num) / 2;
				int num4 = comparer.Compare(key, this.buffer[num3]);
				if (num4 < 0)
				{
					num2 = num3;
				}
				else
				{
					if (num4 <= 0)
					{
						return num3;
					}
					num = num3 + 1;
				}
			}
			return this.LinearSearch<K>(key, comparer, num, num2);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000BAF94 File Offset: 0x000B9194
		private int LinearSearch<K>(K key, IItemComparer<K, T> comparer, int start, int bound)
		{
			for (int i = start; i < bound; i++)
			{
				int num = comparer.Compare(key, this.buffer[i]);
				if (num == 0)
				{
					return i;
				}
				if (num < 0)
				{
					return ~i;
				}
			}
			return ~bound;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000BAFD1 File Offset: 0x000B91D1
		internal void Trim()
		{
			this.Capacity = this.size;
		}

		// Token: 0x04002613 RID: 9747
		private int size;

		// Token: 0x04002614 RID: 9748
		private T[] buffer;

		// Token: 0x04002615 RID: 9749
		private static SortedBuffer<T, C>.DefaultComparer Comparer;

		// Token: 0x02000C4C RID: 3148
		internal class DefaultComparer : IItemComparer<T, T>
		{
			// Token: 0x0600777A RID: 30586 RVA: 0x001BE0D1 File Offset: 0x001BC2D1
			public DefaultComparer(C comparer)
			{
				SortedBuffer<T, C>.DefaultComparer.Comparer = comparer;
			}

			// Token: 0x0600777B RID: 30587 RVA: 0x001BE0E4 File Offset: 0x001BC2E4
			public int Compare(T item1, T item2)
			{
				return SortedBuffer<T, C>.DefaultComparer.Comparer.Compare(item1, item2);
			}

			// Token: 0x04004458 RID: 17496
			public static IComparer<T> Comparer;
		}
	}
}
