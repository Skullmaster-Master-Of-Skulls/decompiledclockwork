using System;
using System.Collections;

namespace System.Data.Odbc
{
	// Token: 0x020001EC RID: 492
	[Serializable]
	public sealed class OdbcErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x06001B76 RID: 7030 RVA: 0x002637A8 File Offset: 0x00262BA8
		internal OdbcErrorCollection()
		{
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x002637C8 File Offset: 0x00262BC8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x002637D8 File Offset: 0x00262BD8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x002637E8 File Offset: 0x00262BE8
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x170003B0 RID: 944
		public OdbcError this[int i]
		{
			get
			{
				return (OdbcError)this._items[i];
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00263828 File Offset: 0x00262C28
		internal void Add(OdbcError error)
		{
			this._items.Add(error);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00263848 File Offset: 0x00262C48
		public void CopyTo(Array array, int i)
		{
			this._items.CopyTo(array, i);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00263868 File Offset: 0x00262C68
		public void CopyTo(OdbcError[] array, int i)
		{
			this._items.CopyTo(array, i);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00263888 File Offset: 0x00262C88
		public IEnumerator GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x002638A8 File Offset: 0x00262CA8
		internal void SetSource(string Source)
		{
			foreach (object obj in this._items)
			{
				((OdbcError)obj).SetSource(Source);
			}
		}

		// Token: 0x0400101B RID: 4123
		private ArrayList _items = new ArrayList();
	}
}
