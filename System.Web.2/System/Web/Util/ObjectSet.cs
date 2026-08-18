using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Util
{
	// Token: 0x0200020C RID: 524
	internal class ObjectSet : ICollection, IEnumerable
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x000030B5 File Offset: 0x000012B5
		internal ObjectSet()
		{
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool CaseInsensitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005013F File Offset: 0x0004E33F
		public void Add(object o)
		{
			if (this._objects == null)
			{
				this._objects = new HybridDictionary(this.CaseInsensitive);
			}
			this._objects[o] = null;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00050168 File Offset: 0x0004E368
		public void AddCollection(ICollection c)
		{
			foreach (object o in c)
			{
				this.Add(o);
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000501B8 File Offset: 0x0004E3B8
		public void Remove(object o)
		{
			if (this._objects == null)
			{
				return;
			}
			this._objects.Remove(o);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000501CF File Offset: 0x0004E3CF
		public bool Contains(object o)
		{
			return this._objects != null && this._objects.Contains(o);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000501E7 File Offset: 0x0004E3E7
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._objects == null)
			{
				return ObjectSet._emptyEnumerator;
			}
			return this._objects.Keys.GetEnumerator();
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x00050207 File Offset: 0x0004E407
		public int Count
		{
			get
			{
				if (this._objects == null)
				{
					return 0;
				}
				return this._objects.Keys.Count;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00050223 File Offset: 0x0004E423
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._objects == null || this._objects.Keys.IsSynchronized;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0005023F File Offset: 0x0004E43F
		object ICollection.SyncRoot
		{
			get
			{
				if (this._objects == null)
				{
					return this;
				}
				return this._objects.Keys.SyncRoot;
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0005025B File Offset: 0x0004E45B
		public void CopyTo(Array array, int index)
		{
			if (this._objects != null)
			{
				this._objects.Keys.CopyTo(array, index);
			}
		}

		// Token: 0x040017E1 RID: 6113
		private static ObjectSet.EmptyEnumerator _emptyEnumerator = new ObjectSet.EmptyEnumerator();

		// Token: 0x040017E2 RID: 6114
		private IDictionary _objects;

		// Token: 0x0200094A RID: 2378
		private class EmptyEnumerator : IEnumerator
		{
			// Token: 0x17001D25 RID: 7461
			// (get) Token: 0x06006993 RID: 27027 RVA: 0x0000298D File Offset: 0x00000B8D
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006994 RID: 27028 RVA: 0x00007722 File Offset: 0x00005922
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x06006995 RID: 27029 RVA: 0x00006164 File Offset: 0x00004364
			public void Reset()
			{
			}
		}
	}
}
