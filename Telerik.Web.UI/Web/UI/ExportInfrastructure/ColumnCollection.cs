using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A4C RID: 2636
	public class ColumnCollection : IEnumerable<Column>, IEnumerable
	{
		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x0600661E RID: 26142 RVA: 0x0017DFB8 File Offset: 0x0017C1B8
		// (set) Token: 0x0600661F RID: 26143 RVA: 0x0017DFC0 File Offset: 0x0017C1C0
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

		// Token: 0x06006620 RID: 26144 RVA: 0x0017DFC9 File Offset: 0x0017C1C9
		internal ColumnCollection(Table tbl)
		{
			this.Table = tbl;
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x0017DFE3 File Offset: 0x0017C1E3
		public int Count
		{
			get
			{
				return this._columnCollection.Values.Count;
			}
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x0017DFF5 File Offset: 0x0017C1F5
		public IEnumerator<Column> GetEnumerator()
		{
			return this._columnCollection.Values.GetEnumerator();
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x0017E00C File Offset: 0x0017C20C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._columnCollection.Values.GetEnumerator();
		}

		// Token: 0x170021AE RID: 8622
		public Column this[int index]
		{
			get
			{
				if (!this._columnCollection.ContainsKey(index))
				{
					this._columnCollection.Add(index, new Column(this.Table)
					{
						Index = index
					});
				}
				return this._columnCollection[index];
			}
			set
			{
				if (!this._columnCollection.ContainsKey(index))
				{
					this._columnCollection.Add(index, new Column(this.Table)
					{
						Index = index
					});
				}
				this._columnCollection[index] = value;
			}
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x0017E0B4 File Offset: 0x0017C2B4
		internal Column AddColumn(int index)
		{
			if (!this._columnCollection.ContainsKey(index))
			{
				this._columnCollection.Add(index, new Column(this.Table)
				{
					Index = index
				});
			}
			return this._columnCollection[index];
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x0017E0FB File Offset: 0x0017C2FB
		internal Column GetColumn(int index)
		{
			if (this._columnCollection.ContainsKey(index))
			{
				return this._columnCollection[index];
			}
			return null;
		}

		// Token: 0x040018B9 RID: 6329
		private Dictionary<int, Column> _columnCollection = new Dictionary<int, Column>();

		// Token: 0x040018BA RID: 6330
		private Table _table;
	}
}
