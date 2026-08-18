using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000507 RID: 1287
	internal struct QueryBuffer<T>
	{
		// Token: 0x060030A4 RID: 12452 RVA: 0x000BA84E File Offset: 0x000B8A4E
		internal QueryBuffer(int capacity)
		{
			if (capacity == 0)
			{
				this.buffer = QueryBuffer<T>.EmptyBuffer;
			}
			else
			{
				this.buffer = new T[capacity];
			}
			this.count = 0;
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000BA873 File Offset: 0x000B8A73
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000B87 RID: 2951
		internal T this[int index]
		{
			get
			{
				return this.buffer[index];
			}
			set
			{
				this.buffer[index] = value;
			}
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000BA898 File Offset: 0x000B8A98
		internal void Add(T t)
		{
			if (this.count == this.buffer.Length)
			{
				Array.Resize<T>(ref this.buffer, (this.count > 0) ? (this.count * 2) : 16);
			}
			T[] array = this.buffer;
			int num = this.count;
			this.count = num + 1;
			array[num] = t;
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000BA8F4 File Offset: 0x000B8AF4
		internal void Add(ref QueryBuffer<T> addBuffer)
		{
			if (1 == addBuffer.count)
			{
				this.Add(addBuffer.buffer[0]);
				return;
			}
			int num = this.count + addBuffer.count;
			if (num >= this.buffer.Length)
			{
				this.Grow(num);
			}
			Array.Copy(addBuffer.buffer, 0, this.buffer, this.count, addBuffer.count);
			this.count = num;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000BA962 File Offset: 0x000B8B62
		internal void Clear()
		{
			this.count = 0;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000BA96C File Offset: 0x000B8B6C
		internal void CopyFrom(ref QueryBuffer<T> addBuffer)
		{
			int num = addBuffer.count;
			if (num == 0)
			{
				this.count = 0;
				return;
			}
			if (num != 1)
			{
				if (num > this.buffer.Length)
				{
					this.buffer = new T[num];
				}
				Array.Copy(addBuffer.buffer, 0, this.buffer, 0, num);
				this.count = num;
				return;
			}
			if (this.buffer.Length == 0)
			{
				this.buffer = new T[1];
			}
			this.buffer[0] = addBuffer.buffer[0];
			this.count = 1;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000BA9F6 File Offset: 0x000B8BF6
		internal void CopyTo(T[] dest)
		{
			Array.Copy(this.buffer, dest, this.count);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000BAA0C File Offset: 0x000B8C0C
		private void Grow(int capacity)
		{
			int num = this.buffer.Length * 2;
			Array.Resize<T>(ref this.buffer, (capacity > num) ? capacity : num);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000BAA38 File Offset: 0x000B8C38
		internal int IndexOf(T t)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (t.Equals(this.buffer[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000BAA7C File Offset: 0x000B8C7C
		internal int IndexOf(T t, int startAt)
		{
			for (int i = startAt; i < this.count; i++)
			{
				if (t.Equals(this.buffer[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000BAABD File Offset: 0x000B8CBD
		internal bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.count;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000BAAD0 File Offset: 0x000B8CD0
		internal void Reserve(int reserveCount)
		{
			int num = this.count + reserveCount;
			if (num >= this.buffer.Length)
			{
				this.Grow(num);
			}
			this.count = num;
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000BAB00 File Offset: 0x000B8D00
		internal void ReserveAt(int index, int reserveCount)
		{
			if (index == this.count)
			{
				this.Reserve(reserveCount);
				return;
			}
			int num;
			if (index > this.count)
			{
				num = index + reserveCount + 1;
				if (num >= this.buffer.Length)
				{
					this.Grow(num);
				}
			}
			else
			{
				num = this.count + reserveCount;
				if (num >= this.buffer.Length)
				{
					this.Grow(num);
				}
				Array.Copy(this.buffer, index, this.buffer, index + reserveCount, this.count - index);
			}
			this.count = num;
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000BAB80 File Offset: 0x000B8D80
		internal void Remove(T t)
		{
			int num = this.IndexOf(t);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000BABA0 File Offset: 0x000B8DA0
		internal void RemoveAt(int index)
		{
			if (index < this.count - 1)
			{
				Array.Copy(this.buffer, index + 1, this.buffer, index, this.count - index - 1);
			}
			this.count--;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000BABDA File Offset: 0x000B8DDA
		internal void Sort(IComparer<T> comparer)
		{
			Array.Sort<T>(this.buffer, 0, this.count, comparer);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000BABF0 File Offset: 0x000B8DF0
		internal void TrimToCount()
		{
			if (this.count < this.buffer.Length)
			{
				if (this.count == 0)
				{
					this.buffer = QueryBuffer<T>.EmptyBuffer;
					return;
				}
				T[] destinationArray = new T[this.count];
				Array.Copy(this.buffer, destinationArray, this.count);
			}
		}

		// Token: 0x04002610 RID: 9744
		internal T[] buffer;

		// Token: 0x04002611 RID: 9745
		internal int count;

		// Token: 0x04002612 RID: 9746
		internal static T[] EmptyBuffer = new T[0];
	}
}
