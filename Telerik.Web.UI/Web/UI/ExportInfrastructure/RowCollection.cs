using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A54 RID: 2644
	public class RowCollection : IEnumerable<Row>, IEnumerable
	{
		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06006673 RID: 26227 RVA: 0x0017FCB0 File Offset: 0x0017DEB0
		// (set) Token: 0x06006674 RID: 26228 RVA: 0x0017FCB8 File Offset: 0x0017DEB8
		public Table Table
		{
			get
			{
				return this._table;
			}
			internal set
			{
				this._table = value;
			}
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x0017FCC1 File Offset: 0x0017DEC1
		internal RowCollection(Table tbl)
		{
			this.Table = tbl;
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x0017FCDB File Offset: 0x0017DEDB
		public IEnumerator<Row> GetEnumerator()
		{
			return this._rowCollection.Values.GetEnumerator();
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x0017FCF2 File Offset: 0x0017DEF2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._rowCollection.Values.GetEnumerator();
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x06006678 RID: 26232 RVA: 0x0017FD09 File Offset: 0x0017DF09
		public int Count
		{
			get
			{
				return this._rowCollection.Values.Count;
			}
		}

		// Token: 0x170021C3 RID: 8643
		public Row this[int index]
		{
			get
			{
				if (!this._rowCollection.ContainsKey(index))
				{
					this._rowCollection.Add(index, new Row(this.Table)
					{
						Index = index
					});
				}
				return this._rowCollection[index];
			}
			set
			{
				if (!this._rowCollection.ContainsKey(index))
				{
					this._rowCollection.Add(index, new Row(this.Table)
					{
						Index = index
					});
				}
				this._rowCollection[index] = value;
			}
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x0017FDAC File Offset: 0x0017DFAC
		internal Row AddRow(int index)
		{
			if (!this._rowCollection.ContainsKey(index))
			{
				this._rowCollection.Add(index, new Row(this.Table)
				{
					Index = index
				});
			}
			return this._rowCollection[index];
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x0017FDF3 File Offset: 0x0017DFF3
		internal Row GetRow(int index)
		{
			if (this._rowCollection.ContainsKey(index))
			{
				return this._rowCollection[index];
			}
			return null;
		}

		// Token: 0x040018DE RID: 6366
		private Dictionary<int, Row> _rowCollection = new Dictionary<int, Row>();

		// Token: 0x040018DF RID: 6367
		private Table _table;
	}
}
