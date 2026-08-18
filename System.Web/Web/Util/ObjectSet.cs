using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Util
{
	// Token: 0x02000770 RID: 1904
	internal class ObjectSet : ICollection, IEnumerable
	{
		// Token: 0x06005C54 RID: 23636 RVA: 0x001724EC File Offset: 0x001714EC
		internal ObjectSet()
		{
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x001724F4 File Offset: 0x001714F4
		protected virtual bool CaseInsensitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x001724F7 File Offset: 0x001714F7
		public void Add(object o)
		{
			if (this._objects == null)
			{
				this._objects = new HybridDictionary(this.CaseInsensitive);
			}
			this._objects[o] = null;
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x00172520 File Offset: 0x00171520
		public void AddCollection(ICollection c)
		{
			foreach (object o in c)
			{
				this.Add(o);
			}
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00172570 File Offset: 0x00171570
		public void Remove(object o)
		{
			if (this._objects == null)
			{
				return;
			}
			this._objects.Remove(o);
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x00172587 File Offset: 0x00171587
		public bool Contains(object o)
		{
			return this._objects != null && this._objects.Contains(o);
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x0017259F File Offset: 0x0017159F
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._objects == null)
			{
				return ObjectSet._emptyEnumerator;
			}
			return this._objects.Keys.GetEnumerator();
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06005C5B RID: 23643 RVA: 0x001725BF File Offset: 0x001715BF
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

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06005C5C RID: 23644 RVA: 0x001725DB File Offset: 0x001715DB
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._objects == null || this._objects.Keys.IsSynchronized;
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x06005C5D RID: 23645 RVA: 0x001725F7 File Offset: 0x001715F7
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

		// Token: 0x06005C5E RID: 23646 RVA: 0x00172613 File Offset: 0x00171613
		public void CopyTo(Array array, int index)
		{
			if (this._objects != null)
			{
				this._objects.Keys.CopyTo(array, index);
			}
		}

		// Token: 0x04003167 RID: 12647
		private static ObjectSet.EmptyEnumerator _emptyEnumerator = new ObjectSet.EmptyEnumerator();

		// Token: 0x04003168 RID: 12648
		private IDictionary _objects;

		// Token: 0x02000771 RID: 1905
		private class EmptyEnumerator : IEnumerator
		{
			// Token: 0x170017C1 RID: 6081
			// (get) Token: 0x06005C60 RID: 23648 RVA: 0x0017263B File Offset: 0x0017163B
			public object Current
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06005C61 RID: 23649 RVA: 0x0017263E File Offset: 0x0017163E
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x06005C62 RID: 23650 RVA: 0x00172641 File Offset: 0x00171641
			public void Reset()
			{
			}
		}
	}
}
