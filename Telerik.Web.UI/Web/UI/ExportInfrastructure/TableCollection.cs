using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A59 RID: 2649
	public class TableCollection : IEnumerable<Table>, IEnumerable
	{
		// Token: 0x060066D1 RID: 26321 RVA: 0x00180D01 File Offset: 0x0017EF01
		public IEnumerator<Table> GetEnumerator()
		{
			return this._tableCollection.GetEnumerator();
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x00180D13 File Offset: 0x0017EF13
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._tableCollection.GetEnumerator();
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x00180D25 File Offset: 0x0017EF25
		public int Count
		{
			get
			{
				return this._tableCollection.Count;
			}
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x00180D32 File Offset: 0x0017EF32
		public void Add(Table table)
		{
			table.Index = this._tableCollection.Count;
			this._tableCollection.Add(table);
		}

		// Token: 0x170021DA RID: 8666
		public Table this[int idx]
		{
			get
			{
				if (this._tableCollection.Count < idx)
				{
					throw new IndexOutOfRangeException();
				}
				return this._tableCollection[idx];
			}
			set
			{
				if (this._tableCollection.Count < idx)
				{
					throw new IndexOutOfRangeException();
				}
				this._tableCollection[idx] = value;
			}
		}

		// Token: 0x170021DB RID: 8667
		public Table this[string tableName]
		{
			get
			{
				return (from item in this._tableCollection
				where item.Title == tableName
				select item).FirstOrDefault<Table>();
			}
			set
			{
				if ((from item in this._tableCollection
				where item.Title == tableName
				select item).FirstOrDefault<Table>() == null)
				{
					throw new KeyNotFoundException();
				}
			}
		}

		// Token: 0x04001904 RID: 6404
		private List<Table> _tableCollection = new List<Table>();
	}
}
