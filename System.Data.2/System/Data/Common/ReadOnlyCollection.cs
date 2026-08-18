using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Common
{
	// Token: 0x020002E9 RID: 745
	[Serializable]
	internal sealed class ReadOnlyCollection<T> : ICollection, IEnumerable, ICollection<T>, IEnumerable<T>
	{
		// Token: 0x06002F2F RID: 12079 RVA: 0x0012A684 File Offset: 0x00129A84
		internal ReadOnlyCollection(T[] items)
		{
			this._items = items;
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x0012A6A0 File Offset: 0x00129AA0
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._items.Length);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x0012A6C4 File Offset: 0x00129AC4
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._items.Length);
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x0012A6E8 File Offset: 0x00129AE8
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new ReadOnlyCollection<T>.Enumerator<T>(this._items);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x0012A708 File Offset: 0x00129B08
		public IEnumerator GetEnumerator()
		{
			return new ReadOnlyCollection<T>.Enumerator<T>(this._items);
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x0012A728 File Offset: 0x00129B28
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x0012A738 File Offset: 0x00129B38
		object ICollection.SyncRoot
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x0012A74C File Offset: 0x00129B4C
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x0012A75C File Offset: 0x00129B5C
		void ICollection<!0>.Add(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x0012A770 File Offset: 0x00129B70
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x0012A784 File Offset: 0x00129B84
		bool ICollection<!0>.Contains(T value)
		{
			return Array.IndexOf<T>(this._items, value) >= 0;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x0012A7A4 File Offset: 0x00129BA4
		bool ICollection<!0>.Remove(T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x0012A7B8 File Offset: 0x00129BB8
		public int Count
		{
			get
			{
				return this._items.Length;
			}
		}

		// Token: 0x04001CE4 RID: 7396
		private T[] _items;

		// Token: 0x02000437 RID: 1079
		[Serializable]
		internal struct Enumerator<K> : IEnumerator<K>, IDisposable, IEnumerator
		{
			// Token: 0x06003639 RID: 13881 RVA: 0x00149630 File Offset: 0x00148A30
			internal Enumerator(K[] items)
			{
				this._items = items;
				this._index = -1;
			}

			// Token: 0x0600363A RID: 13882 RVA: 0x0014964C File Offset: 0x00148A4C
			public void Dispose()
			{
			}

			// Token: 0x0600363B RID: 13883 RVA: 0x0014965C File Offset: 0x00148A5C
			public bool MoveNext()
			{
				int num = this._index + 1;
				this._index = num;
				return num < this._items.Length;
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x0600363C RID: 13884 RVA: 0x00149684 File Offset: 0x00148A84
			public K Current
			{
				get
				{
					return this._items[this._index];
				}
			}

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x0600363D RID: 13885 RVA: 0x001496A4 File Offset: 0x00148AA4
			object IEnumerator.Current
			{
				get
				{
					return this._items[this._index];
				}
			}

			// Token: 0x0600363E RID: 13886 RVA: 0x001496C8 File Offset: 0x00148AC8
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x0400234B RID: 9035
			private K[] _items;

			// Token: 0x0400234C RID: 9036
			private int _index;
		}
	}
}
