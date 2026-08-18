using System;
using System.Data;

namespace ClockWorkAPI
{
	// Token: 0x020000A9 RID: 169
	public class DataTableFunctions
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00032A44 File Offset: 0x00031A44
		public static DataRow FindDataRow(DataTable t, int colInd, int colVal)
		{
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted && dataRow[colInd] != DBNull.Value)
				{
					int num = (int)dataRow[colInd];
					if (num == colVal)
					{
						return dataRow;
					}
				}
			}
			return null;
		}
	}
}
