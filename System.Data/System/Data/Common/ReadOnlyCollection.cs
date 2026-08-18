using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Common
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	internal sealed class ReadOnlyCollection<T> : ICollection, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060013EE RID: 5102 RVA: 0x0023D888 File Offset: 0x0023CC88
		internal ReadOnlyCollection(T[] items)
		{
			this._items = items;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0023D8A8 File Offset: 0x0023CCA8
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._items.Length);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0023D8D8 File Offset: 0x0023CCD8
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._items.Length);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0023D908 File Offset: 0x0023CD08
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new ReadOnlyCollection<T>.Enumerator<T>(this._items);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0023D928 File Offset: 0x0023CD28
		public IEnumerator GetEnumerator()
		{
			return new ReadOnlyCollection<T>.Enumerator<T>(this._items);
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0023D948 File Offset: 0x0023CD48
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0023D958 File Offset: 0x0023CD58
		object ICollection.SyncRoot
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0023D978 File Offset: 0x0023CD78
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0023D988 File Offset: 0x0023CD88
		void ICollection<!0>.Add(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0023D9A8 File Offset: 0x0023CDA8
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0023D9C8 File Offset: 0x0023CDC8
		bool ICollection<!0>.Contains(T value)
		{
			return Array.IndexOf<T>(this._items, value) >= 0;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0023D9E8 File Offset: 0x0023CDE8
		bool ICollection<!0>.Remove(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0023DA08 File Offset: 0x0023CE08
		public int Count
		{
			get
			{
				return this._items.Length;
			}
		}

		// Token: 0x04000C38 RID: 3128
		private T[] _items;

		// Token: 0x02000130 RID: 304
		[Serializable]
		internal struct Enumerator<K> : IEnumerator<K>, IDisposable, IEnumerator
		{
			// Token: 0x060013FB RID: 5115 RVA: 0x0023DA28 File Offset: 0x0023CE28
			internal Enumerator(K[] items)
			{
				this._items = items;
				this._index = -1;
			}

			// Token: 0x060013FC RID: 5116 RVA: 0x0023DA48 File Offset: 0x0023CE48
			public void Dispose()
			{
			}

			// Token: 0x060013FD RID: 5117 RVA: 0x0023DA58 File Offset: 0x0023CE58
			public bool MoveNext()
			{
				return ++this._index < this._items.Length;
			}

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x060013FE RID: 5118 RVA: 0x0023DA88 File Offset: 0x0023CE88
			public K Current
			{
				get
				{
					return this._items[this._index];
				}
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x060013FF RID: 5119 RVA: 0x0023DAA8 File Offset: 0x0023CEA8
			object IEnumerator.Current
			{
				get
				{
					return this._items[this._index];
				}
			}

			// Token: 0x06001400 RID: 5120 RVA: 0x0023DAD8 File Offset: 0x0023CED8
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x04000C39 RID: 3129
			private K[] _items;

			// Token: 0x04000C3A RID: 3130
			private int _index;
		}
	}
}
