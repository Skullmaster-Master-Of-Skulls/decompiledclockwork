using System;
using System.Collections;

namespace System.Data.Odbc
{
	// Token: 0x0200029C RID: 668
	[Serializable]
	public sealed class OdbcErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x060028D6 RID: 10454 RVA: 0x00110A24 File Offset: 0x0010FE24
		internal OdbcErrorCollection()
		{
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x00110A44 File Offset: 0x0010FE44
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x00110A54 File Offset: 0x0010FE54
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x00110A64 File Offset: 0x0010FE64
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x170006AC RID: 1708
		public OdbcError this[int i]
		{
			get
			{
				return (OdbcError)this._items[i];
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00110A9C File Offset: 0x0010FE9C
		internal void Add(OdbcError error)
		{
			this._items.Add(error);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x00110AB8 File Offset: 0x0010FEB8
		public void CopyTo(Array array, int i)
		{
			this._items.CopyTo(array, i);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00110AD4 File Offset: 0x0010FED4
		public void CopyTo(OdbcError[] array, int i)
		{
			this._items.CopyTo(array, i);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x00110AF0 File Offset: 0x0010FEF0
		public IEnumerator GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00110B08 File Offset: 0x0010FF08
		internal void SetSource(string Source)
		{
			foreach (object obj in this._items)
			{
				((OdbcError)obj).SetSource(Source);
			}
		}

		// Token: 0x04001AA7 RID: 6823
		private ArrayList _items = new ArrayList();
	}
}
