using System;
using System.Data;
using System.Data.Common;

namespace ReportFunctions
{
	// Token: 0x0200001B RID: 27
	public class DataReaderAdapter : DataAdapter
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000389BC File Offset: 0x000379BC
		public int FillFromReader(DataTable dataTable, IDataReader dataReader)
		{
			return this.Fill(dataTable, dataReader);
		}
	}
}
