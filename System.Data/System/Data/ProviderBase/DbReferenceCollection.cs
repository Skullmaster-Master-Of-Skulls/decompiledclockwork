using System;
using System.Collections;

namespace System.Data.ProviderBase
{
	// Token: 0x020001FC RID: 508
	internal abstract class DbReferenceCollection
	{
		// Token: 0x06001C54 RID: 7252 RVA: 0x00268C48 File Offset: 0x00268048
		protected DbReferenceCollection()
		{
			this._items = new DbReferenceCollection.CollectionEntry[5];
		}

		// Token: 0x06001C55 RID: 7253
		public abstract void Add(object value, int tag);

		// Token: 0x06001C56 RID: 7254 RVA: 0x00268C68 File Offset: 0x00268068
		protected void AddItem(object value, int tag)
		{
			DbReferenceCollection.CollectionEntry[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (!items[i].HasTarget)
				{
					items[i].Target = value;
					items[i].Tag = tag;
					return;
				}
			}
			int num = (5 == items.Length) ? 15 : (items.Length + 15);
			DbReferenceCollection.CollectionEntry[] array = new DbReferenceCollection.CollectionEntry[num];
			for (int j = 0; j < items.Length; j++)
			{
				array[j] = items[j];
			}
			array[items.Length].Target = value;
			array[items.Length].Tag = tag;
			this._items = array;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00268D18 File Offset: 0x00268118
		internal IEnumerable Filter(int tag)
		{
			return new DbReferenceCollection.DbFilteredReferenceCollection(this._items, tag);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00268D38 File Offset: 0x00268138
		public void Notify(int message)
		{
			DbReferenceCollection.CollectionEntry[] items = this._items;
			int num = 0;
			while (num < items.Length && items[num].InUse)
			{
				object target = items[num].Target;
				if (target != null && !this.NotifyItem(message, items[num].Tag, target))
				{
					items[num].Tag = 0;
					items[num].Target = null;
				}
				num++;
			}
		}

		// Token: 0x06001C59 RID: 7257
		protected abstract bool NotifyItem(int message, int tag, object value);

		// Token: 0x06001C5A RID: 7258 RVA: 0x00268DA8 File Offset: 0x002681A8
		public void Purge()
		{
			DbReferenceCollection.CollectionEntry[] items = this._items;
			if (100 < items.Length)
			{
				this._items = new DbReferenceCollection.CollectionEntry[5];
			}
		}

		// Token: 0x06001C5B RID: 7259
		public abstract void Remove(object value);

		// Token: 0x06001C5C RID: 7260 RVA: 0x00268DD8 File Offset: 0x002681D8
		protected void RemoveItem(object value)
		{
			DbReferenceCollection.CollectionEntry[] items = this._items;
			int num = 0;
			while (num < items.Length && items[num].InUse)
			{
				if (value == items[num].Target)
				{
					items[num].Tag = 0;
					items[num].Target = null;
					return;
				}
				num++;
			}
		}

		// Token: 0x0400106D RID: 4205
		private DbReferenceCollection.CollectionEntry[] _items;

		// Token: 0x020001FD RID: 509
		private struct CollectionEntry
		{
			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00268E38 File Offset: 0x00268238
			public bool HasTarget
			{
				get
				{
					return this._tag != 0 && this._weak != null && this._weak.IsAlive;
				}
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00268E68 File Offset: 0x00268268
			public bool InUse
			{
				get
				{
					return null != this._weak;
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00268E88 File Offset: 0x00268288
			// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00268EA8 File Offset: 0x002682A8
			public int Tag
			{
				get
				{
					return this._tag;
				}
				set
				{
					this._tag = value;
				}
			}

			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00268EC8 File Offset: 0x002682C8
			// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00268EF8 File Offset: 0x002682F8
			public object Target
			{
				get
				{
					if (this._tag != 0)
					{
						return this._weak.Target;
					}
					return null;
				}
				set
				{
					if (this._weak == null)
					{
						this._weak = new WeakReference(value, false);
						return;
					}
					this._weak.Target = value;
				}
			}

			// Token: 0x0400106E RID: 4206
			private int _tag;

			// Token: 0x0400106F RID: 4207
			private WeakReference _weak;
		}

		// Token: 0x020001FE RID: 510
		private struct DbFilteredReferenceCollection : IEnumerable
		{
			// Token: 0x06001C63 RID: 7267 RVA: 0x00268F28 File Offset: 0x00268328
			internal DbFilteredReferenceCollection(DbReferenceCollection.CollectionEntry[] items, int filterTag)
			{
				this._items = items;
				this._filterTag = filterTag;
			}

			// Token: 0x06001C64 RID: 7268 RVA: 0x00268F48 File Offset: 0x00268348
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new DbReferenceCollection.DbFilteredReferenceCollection.DbFilteredReferenceCollectionedEnumerator(this._items, this._filterTag);
			}

			// Token: 0x04001070 RID: 4208
			private readonly DbReferenceCollection.CollectionEntry[] _items;

			// Token: 0x04001071 RID: 4209
			private readonly int _filterTag;

			// Token: 0x020001FF RID: 511
			private struct DbFilteredReferenceCollectionedEnumerator : IEnumerator
			{
				// Token: 0x06001C65 RID: 7269 RVA: 0x00268F78 File Offset: 0x00268378
				internal DbFilteredReferenceCollectionedEnumerator(DbReferenceCollection.CollectionEntry[] items, int filterTag)
				{
					this._items = items;
					this._filterTag = filterTag;
					this._current = -1;
				}

				// Token: 0x170003D7 RID: 983
				// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00268FA8 File Offset: 0x002683A8
				object IEnumerator.Current
				{
					get
					{
						return this._items[this._current].Target;
					}
				}

				// Token: 0x06001C67 RID: 7271 RVA: 0x00268FD8 File Offset: 0x002683D8
				bool IEnumerator.MoveNext()
				{
					while (++this._current < this._items.Length && this._items[this._current].InUse)
					{
						if (this._items[this._current].Tag == this._filterTag)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06001C68 RID: 7272 RVA: 0x00269048 File Offset: 0x00268448
				void IEnumerator.Reset()
				{
					this._current = -1;
				}

				// Token: 0x04001072 RID: 4210
				private readonly DbReferenceCollection.CollectionEntry[] _items;

				// Token: 0x04001073 RID: 4211
				private readonly int _filterTag;

				// Token: 0x04001074 RID: 4212
				private int _current;
			}
		}
	}
}
