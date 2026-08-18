using System;
using System.Data;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x0200017C RID: 380
	public static class DataTableAdapter
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x00078EBC File Offset: 0x000770BC
		public static bool ContainsColumn(this DataTable dt, string columnName)
		{
			return dt.Columns.Contains(columnName);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00078EDC File Offset: 0x000770DC
		public static bool ContainsColumn(this DataRow row, string columnName)
		{
			return row.Table.Columns.Contains(columnName);
		}
	}
}
