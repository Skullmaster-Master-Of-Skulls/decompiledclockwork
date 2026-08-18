using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C9C RID: 3228
	internal class DataTableDataSourceView : IDataSourceView, IReadOnlyList<object>, IReadOnlyCollection<object>, IEnumerable<object>, IEnumerable
	{
		// Token: 0x06007959 RID: 31065 RVA: 0x001BE630 File Offset: 0x001BC830
		public DataTableDataSourceView(DataTable dataTable)
		{
			this.rows = new List<object>();
			foreach (object item in dataTable.Rows)
			{
				this.rows.Add(item);
			}
		}

		// Token: 0x17002721 RID: 10017
		// (get) Token: 0x0600795A RID: 31066 RVA: 0x001BE69C File Offset: 0x001BC89C
		public int Count
		{
			get
			{
				return this.rows.Count;
			}
		}

		// Token: 0x17002722 RID: 10018
		public object this[int index]
		{
			get
			{
				return this.rows[index];
			}
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x001BE6B7 File Offset: 0x001BC8B7
		public IEnumerator<object> GetEnumerator()
		{
			return this.rows.GetEnumerator();
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x001BE6C9 File Offset: 0x001BC8C9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.rows.GetEnumerator();
		}

		// Token: 0x0400212A RID: 8490
		private List<object> rows;
	}
}
