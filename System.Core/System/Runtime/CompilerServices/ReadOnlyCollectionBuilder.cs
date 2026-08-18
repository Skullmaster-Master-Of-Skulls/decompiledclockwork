using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000145 RID: 325
	[Serializable]
	public sealed class ReadOnlyCollectionBuilder<T> : IList<T>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x00025CDD File Offset: 0x00023EDD
		public ReadOnlyCollectionBuilder()
		{
			this._items = ReadOnlyCollectionBuilder<T>._emptyArray;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00025CF0 File Offset: 0x00023EF0
		public ReadOnlyCollectionBuilder(int capacity)
		{
			ContractUtils.Requires(capacity >= 0, "capacity");
			this._items = new T[capacity];
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00025D18 File Offset: 0x00023F18
		public ReadOnlyCollectionBuilder(IEnumerable<T> collection)
		{
			ContractUtils.Requires(collection != null, "collection");
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				this._items = new T[count];
				collection2.CopyTo(this._items, 0);
				this._size = count;
				return;
			}
			this._size = 0;
			this._items = new T[4];
			foreach (T item in collection)
			{
				this.Add(item);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00025DB8 File Offset: 0x00023FB8
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x00025DC4 File Offset: 0x00023FC4
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				ContractUtils.Requires(value >= this._size, "value");
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = ReadOnlyCollectionBuilder<T>._emptyArray;
				}
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00025E2D File Offset: 0x0002402D
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00025E35 File Offset: 0x00024035
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00025E4C File Offset: 0x0002404C
		public void Insert(int index, T item)
		{
			ContractUtils.Requires(index <= this._size, "index");
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00025EDC File Offset: 0x000240DC
		public void RemoveAt(int index)
		{
			ContractUtils.Requires(index >= 0 && index < this._size, "index");
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		// Token: 0x17000216 RID: 534
		public T this[int index]
		{
			get
			{
				ContractUtils.Requires(index < this._size, "index");
				return this._items[index];
			}
			set
			{
				ContractUtils.Requires(index < this._size, "index");
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00025FB0 File Offset: 0x000241B0
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00026006 File Offset: 0x00024206
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00026038 File Offset: 0x00024238
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int j = 0; j < this._size; j++)
			{
				if (@default.Equals(this._items[j], item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000260A4 File Offset: 0x000242A4
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000260BA File Offset: 0x000242BA
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000260C0 File Offset: 0x000242C0
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000260E3 File Offset: 0x000242E3
		public IEnumerator<T> GetEnumerator()
		{
			return new ReadOnlyCollectionBuilder<T>.Enumerator(this);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000260EB File Offset: 0x000242EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000260F3 File Offset: 0x000242F3
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000260F8 File Offset: 0x000242F8
		int IList.Add(object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				ReadOnlyCollectionBuilder<T>.ThrowInvalidTypeException(value, "value");
			}
			return this.Count - 1;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00026144 File Offset: 0x00024344
		bool IList.Contains(object value)
		{
			return ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002615C File Offset: 0x0002435C
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00026174 File Offset: 0x00024374
		void IList.Insert(int index, object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				ReadOnlyCollectionBuilder<T>.ThrowInvalidTypeException(value, "value");
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x000261BC File Offset: 0x000243BC
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000261BF File Offset: 0x000243BF
		void IList.Remove(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x1700021A RID: 538
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ReadOnlyCollectionBuilder<T>.ThrowInvalidTypeException(value, "value");
				}
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002622C File Offset: 0x0002442C
		void ICollection.CopyTo(Array array, int index)
		{
			ContractUtils.RequiresNotNull(array, "array");
			ContractUtils.Requires(array.Rank == 1, "array");
			Array.Copy(this._items, 0, array, index, this._size);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00026260 File Offset: 0x00024460
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00026263 File Offset: 0x00024463
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00026285 File Offset: 0x00024485
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00026294 File Offset: 0x00024494
		public void Reverse(int index, int count)
		{
			ContractUtils.Requires(index >= 0, "index");
			ContractUtils.Requires(count >= 0, "count");
			Array.Reverse(this._items, index, count);
			this._version++;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000262D4 File Offset: 0x000244D4
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00026304 File Offset: 0x00024504
		public ReadOnlyCollection<T> ToReadOnlyCollection()
		{
			T[] list;
			if (this._size == this._items.Length)
			{
				list = this._items;
			}
			else
			{
				list = this.ToArray();
			}
			this._items = ReadOnlyCollectionBuilder<T>._emptyArray;
			this._size = 0;
			this._version++;
			return new TrueReadOnlyCollection<T>(list);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00026358 File Offset: 0x00024558
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = 4;
				if (this._items.Length != 0)
				{
					num = this._items.Length * 2;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00026394 File Offset: 0x00024594
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000263C4 File Offset: 0x000245C4
		private static void ValidateNullValue(object value, string argument)
		{
			if (value == null && default(T) != null)
			{
				throw new ArgumentException(Strings.InvalidNullValue(typeof(T)), argument);
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000263FA File Offset: 0x000245FA
		private static void ThrowInvalidTypeException(object value, string argument)
		{
			throw new ArgumentException(Strings.InvalidObjectType((value != null) ? value.GetType() : "null", typeof(T)), argument);
		}

		// Token: 0x04000774 RID: 1908
		private const int DefaultCapacity = 4;

		// Token: 0x04000775 RID: 1909
		private T[] _items;

		// Token: 0x04000776 RID: 1910
		private int _size;

		// Token: 0x04000777 RID: 1911
		private int _version;

		// Token: 0x04000778 RID: 1912
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04000779 RID: 1913
		private static readonly T[] _emptyArray = new T[0];

		// Token: 0x0200036F RID: 879
		[Serializable]
		private class Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06001BA2 RID: 7074 RVA: 0x00063939 File Offset: 0x00061B39
			internal Enumerator(ReadOnlyCollectionBuilder<T> builder)
			{
				this._builder = builder;
				this._version = builder._version;
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x00063967 File Offset: 0x00061B67
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06001BA4 RID: 7076 RVA: 0x0006396F File Offset: 0x00061B6F
			public void Dispose()
			{
				GC.SuppressFinalize(this);
			}

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00063977 File Offset: 0x00061B77
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index > this._builder._size)
					{
						throw Error.EnumerationIsDone();
					}
					return this._current;
				}
			}

			// Token: 0x06001BA6 RID: 7078 RVA: 0x000639A8 File Offset: 0x00061BA8
			public bool MoveNext()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				if (this._index < this._builder._size)
				{
					T[] items = this._builder._items;
					int index = this._index;
					this._index = index + 1;
					this._current = items[index];
					return true;
				}
				this._index = this._builder._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x06001BA7 RID: 7079 RVA: 0x00063A2A File Offset: 0x00061C2A
			void IEnumerator.Reset()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04000F99 RID: 3993
			private readonly ReadOnlyCollectionBuilder<T> _builder;

			// Token: 0x04000F9A RID: 3994
			private readonly int _version;

			// Token: 0x04000F9B RID: 3995
			private int _index;

			// Token: 0x04000F9C RID: 3996
			private T _current;
		}
	}
}
