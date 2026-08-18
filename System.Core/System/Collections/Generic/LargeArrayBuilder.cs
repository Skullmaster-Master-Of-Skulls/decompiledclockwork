using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000099 RID: 153
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000BB40 File Offset: 0x00009D40
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000BB50 File Offset: 0x00009D50
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = new T[0]);
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000BB80 File Offset: 0x00009D80
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000BB88 File Offset: 0x00009D88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			if (this._index == this._current.Length)
			{
				this.AllocateBuffer();
			}
			T[] current = this._current;
			int index = this._index;
			this._index = index + 1;
			current[index] = item;
			this._count++;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		public void AddRange(IEnumerable<T> items)
		{
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				T[] current = this._current;
				int index = this._index;
				while (enumerator.MoveNext())
				{
					if (index == current.Length)
					{
						this._count += index - this._index;
						this._index = index;
						this.AllocateBuffer();
						current = this._current;
						index = this._index;
					}
					current[index++] = enumerator.Current;
				}
				this._count += index - this._index;
				this._index = index;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000BC84 File Offset: 0x00009E84
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			while (count > 0)
			{
				T[] buffer = this.GetBuffer(num);
				int num2 = Math.Min(count, buffer.Length);
				Array.Copy(buffer, 0, array, arrayIndex, num2);
				count -= num2;
				arrayIndex += num2;
				num++;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		public CopyPosition CopyTo(CopyPosition position, T[] array, int arrayIndex, int count)
		{
			int num = position.Row;
			int num2 = position.Column;
			while (count > 0)
			{
				T[] buffer = this.GetBuffer(num);
				int num3 = Math.Min(buffer.Length, count);
				if (num3 > 0)
				{
					Array.Copy(buffer, num2, array, arrayIndex, num3);
					arrayIndex += num3;
					count -= num3;
					num2 += num3;
				}
				num++;
				num2 = 0;
			}
			return new CopyPosition(num, num2);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000BD25 File Offset: 0x00009F25
		public T[] GetBuffer(int index)
		{
			if (index == 0)
			{
				return this._first;
			}
			if (index > this._buffers.Count)
			{
				return this._current;
			}
			return this._buffers[index - 1];
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000BD54 File Offset: 0x00009F54
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			this.Add(item);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000BD60 File Offset: 0x00009F60
		public T[] ToArray()
		{
			T[] array;
			if (this.TryMove(out array))
			{
				return array;
			}
			array = new T[this._count];
			this.CopyTo(array, 0, this._count);
			return array;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000BD94 File Offset: 0x00009F94
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		private void AllocateBuffer()
		{
			if (this._count < 8)
			{
				int num = Math.Min((this._count == 0) ? 4 : (this._count * 2), this._maxCapacity);
				this._current = new T[num];
				Array.Copy(this._first, 0, this._current, 0, this._count);
				this._first = this._current;
				return;
			}
			int num2;
			if (this._count == 8)
			{
				num2 = 8;
			}
			else
			{
				this._buffers.Add(this._current);
				num2 = Math.Min(this._count, this._maxCapacity - this._count);
			}
			this._current = new T[num2];
			this._index = 0;
		}

		// Token: 0x040004DF RID: 1247
		private const int StartingCapacity = 4;

		// Token: 0x040004E0 RID: 1248
		private const int ResizeLimit = 8;

		// Token: 0x040004E1 RID: 1249
		private readonly int _maxCapacity;

		// Token: 0x040004E2 RID: 1250
		private T[] _first;

		// Token: 0x040004E3 RID: 1251
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x040004E4 RID: 1252
		private T[] _current;

		// Token: 0x040004E5 RID: 1253
		private int _index;

		// Token: 0x040004E6 RID: 1254
		private int _count;
	}
}
