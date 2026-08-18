using System;
using System.Data;
using System.IO;

namespace ImportExportClassLibrary
{
	// Token: 0x0200003A RID: 58
	public class ImportData
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00015F80 File Offset: 0x00014F80
		public static DataTable ImportDataTable(string filename, string tableName, out Exception ex)
		{
			string text = Path.GetExtension(filename).ToLower().Trim();
			DataTable result;
			try
			{
				DataTable dataTable = null;
				string a;
				if ((a = text) == null || (!(a == ".xls") && !(a == ".mdb")))
				{
					dataTable = CsvParser.Parse(new StreamReader(filename));
				}
				ex = null;
				result = dataTable;
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = null;
			}
			return result;
		}
	}
}
