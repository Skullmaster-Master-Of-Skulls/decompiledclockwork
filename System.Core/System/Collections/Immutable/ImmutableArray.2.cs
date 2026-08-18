using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000093 RID: 147
	internal struct ImmutableArray<T>
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000A0D8 File Offset: 0x000082D8
		public ImmutableArray(T[] array)
		{
			this._array = array;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000A0E1 File Offset: 0x000082E1
		public bool IsDefault
		{
			get
			{
				return this._array == null;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000A0EC File Offset: 0x000082EC
		public int Length
		{
			get
			{
				return this._array.Length;
			}
		}

		// Token: 0x170000E1 RID: 225
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000A104 File Offset: 0x00008304
		public T[] UnderlyingArray
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000A10C File Offset: 0x0000830C
		public T FirstOrDefault(Func<T, bool> predicate)
		{
			foreach (T t in this._array)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000A14A File Offset: 0x0000834A
		public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			Array.Copy(this._array, sourceIndex, destination, destinationIndex, length);
		}

		// Token: 0x040004C4 RID: 1220
		private readonly T[] _array;

		// Token: 0x040004C5 RID: 1221
		public static ImmutableArray<T> Empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x02000307 RID: 775
		public sealed class Builder
		{
			// Token: 0x06001A72 RID: 6770 RVA: 0x00060EC7 File Offset: 0x0005F0C7
			internal Builder(int capacity)
			{
				this._elements = new T[capacity];
				this._count = 0;
			}

			// Token: 0x06001A73 RID: 6771 RVA: 0x00060EE2 File Offset: 0x0005F0E2
			internal Builder() : this(8)
			{
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x06001A74 RID: 6772 RVA: 0x00060EEB File Offset: 0x0005F0EB
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00060EF3 File Offset: 0x0005F0F3
			public int Capacity
			{
				get
				{
					return this._elements.Length;
				}
			}

			// Token: 0x170004EC RID: 1260
			public T this[int index]
			{
				get
				{
					if (index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					return this._elements[index];
				}
				set
				{
					if (index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					this._elements[index] = value;
				}
			}

			// Token: 0x06001A78 RID: 6776 RVA: 0x00060F38 File Offset: 0x0005F138
			public ImmutableArray<T> MoveToImmutable()
			{
				if (this.Capacity != this.Count)
				{
					throw new InvalidOperationException();
				}
				T[] elements = this._elements;
				this._elements = ImmutableArray<T>.Empty._array;
				this._count = 0;
				return new ImmutableArray<T>(elements);
			}

			// Token: 0x06001A79 RID: 6777 RVA: 0x00060F80 File Offset: 0x0005F180
			public void Add(T item)
			{
				this.EnsureCapacity(this.Count + 1);
				T[] elements = this._elements;
				int count = this._count;
				this._count = count + 1;
				elements[count] = item;
			}

			// Token: 0x06001A7A RID: 6778 RVA: 0x00060FB8 File Offset: 0x0005F1B8
			private void EnsureCapacity(int capacity)
			{
				if (this._elements.Length < capacity)
				{
					int newSize = Math.Max(this._elements.Length * 2, capacity);
					Array.Resize<T>(ref this._elements, newSize);
				}
			}

			// Token: 0x04000E1D RID: 3613
			private T[] _elements;

			// Token: 0x04000E1E RID: 3614
			private int _count;
		}
	}
}
