using System;

namespace System.Collections.Generic
{
	// Token: 0x0200009A RID: 154
	internal struct ArrayBuilder<T>
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0000BE61 File Offset: 0x0000A061
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000BE7A File Offset: 0x0000A07A
		public int Capacity
		{
			get
			{
				T[] array = this._array;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000BE8A File Offset: 0x0000A08A
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170000EE RID: 238
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
			set
			{
				this._array[index] = value;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000BEAF File Offset: 0x0000A0AF
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000BEE2 File Offset: 0x0000A0E2
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public T[] ToArray()
		{
			if (this._count == 0)
			{
				return new T[0];
			}
			T[] array = this._array;
			if (this._count < array.Length)
			{
				array = new T[this._count];
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			return array;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000BF48 File Offset: 0x0000A148
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000BF74 File Offset: 0x0000A174
		private void EnsureCapacity(int minimum)
		{
			int capacity = this.Capacity;
			int num = (capacity == 0) ? 4 : (2 * capacity);
			if (num > 2146435071)
			{
				num = Math.Max(capacity + 1, 2146435071);
			}
			num = Math.Max(num, minimum);
			T[] array = new T[num];
			if (this._count > 0)
			{
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			this._array = array;
		}

		// Token: 0x040004E7 RID: 1255
		private const int DefaultCapacity = 4;

		// Token: 0x040004E8 RID: 1256
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x040004E9 RID: 1257
		private T[] _array;

		// Token: 0x040004EA RID: 1258
		private int _count;
	}
}
