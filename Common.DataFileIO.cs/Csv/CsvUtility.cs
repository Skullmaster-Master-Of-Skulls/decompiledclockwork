using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Spire.DataExport.Common;
using Spire.DataExport.TXT;
using TechnoPro.Common.DataFileIO.cs.Excel;

namespace TechnoPro.Common.DataFileIO.cs.Csv
{
	// Token: 0x0200000A RID: 10
	public static class CsvUtility
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00003F50 File Offset: 0x00002150
		public static void ExportDataTableToExcel(string fileName, DataTable t, FileActionAfterExport fileActionAfterExport)
		{
			TXTExport txtexport = new TXTExport();
			txtexport.DataSource = ExportSource.DataTable;
			txtexport.DataTable = t;
			if (fileActionAfterExport != FileActionAfterExport.OpenView)
			{
				if (fileActionAfterExport != FileActionAfterExport.Print)
				{
					txtexport.ActionAfterExport = ActionType.None;
				}
				else
				{
					txtexport.ActionAfterExport = ActionType.Print;
				}
			}
			else
			{
				txtexport.ActionAfterExport = ActionType.OpenView;
			}
			txtexport.FileName = fileName;
			txtexport.SaveToFile();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003FB4 File Offset: 0x000021B4
		public static void ExportDataTableToCsv(string filename, DataTable t, bool createHeader = true)
		{
			bool flag = t == null;
			if (!flag)
			{
				List<DataColumn> list = (from DataColumn dc in t.Columns
				where dc.ColumnMapping != MappingType.Hidden
				select dc).ToList<DataColumn>();
				int count = list.Count;
				int num = count - 1;
				using (StreamWriter streamWriter = new StreamWriter(filename, false))
				{
					if (createHeader)
					{
						for (int i = 0; i < count; i++)
						{
							DataColumn dataColumn = list[i];
							string text = dataColumn.ColumnName;
							bool flag2 = CsvUtility.IsRequiresEscapeInCsv(text);
							if (flag2)
							{
								text = CsvUtility.EscapeCsv(text);
							}
							streamWriter.Write(text);
							bool flag3 = i != num;
							if (flag3)
							{
								streamWriter.Write(",");
							}
						}
						streamWriter.WriteLine();
					}
					foreach (object obj in t.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						bool flag4 = dataRow.RowState == DataRowState.Deleted;
						if (!flag4)
						{
							for (int j = 0; j < count; j++)
							{
								object obj2 = dataRow[list[j].ColumnName];
								bool flag5 = obj2 is DBNull;
								string text2;
								if (flag5)
								{
									text2 = string.Empty;
								}
								else
								{
									text2 = obj2.ToString();
									bool flag6 = CsvUtility.IsRequiresEscapeInCsv(text2);
									if (flag6)
									{
										text2 = CsvUtility.EscapeCsv(text2);
									}
								}
								streamWriter.Write(text2 ?? "");
								bool flag7 = j != num;
								if (flag7)
								{
									streamWriter.Write(",");
								}
							}
							streamWriter.WriteLine();
						}
					}
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000041D4 File Offset: 0x000023D4
		private static string EscapeCsv(string s)
		{
			return "\"" + s.Replace("\"", "\"\"\"\"") + "\"";
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004208 File Offset: 0x00002408
		private static bool IsRequiresEscapeInCsv(string s)
		{
			bool flag = string.IsNullOrWhiteSpace(s);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = s.Contains(',') || s.Contains('"') || s.Contains('\n');
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004250 File Offset: 0x00002450
		public static DataTable ImportTableFromCsv(string fileName, bool headersInFirstRow)
		{
			DataTable dataTable = new DataTable("t");
			using (TextReader textReader = new StreamReader(fileName))
			{
				CsvStream csvStream = new CsvStream(textReader);
				string[] nextRow = csvStream.GetNextRow();
				bool flag = nextRow == null;
				if (flag)
				{
					return dataTable;
				}
				List<string> list;
				if (headersInFirstRow)
				{
					list = nextRow.ToList<string>();
				}
				else
				{
					list = new List<string>();
					for (int i = 0; i < nextRow.Length; i++)
					{
						list.Add("col" + i.ToString());
					}
				}
				foreach (string columnName in list)
				{
					dataTable.Columns.Add(columnName);
				}
				for (string[] array = headersInFirstRow ? csvStream.GetNextRow() : nextRow; array != null; array = csvStream.GetNextRow())
				{
					int num = array.Length - dataTable.Columns.Count;
					bool flag2 = num > 0;
					if (flag2)
					{
						for (int j = 0; j < num; j++)
						{
							dataTable.Columns.Add("column" + j++.ToString());
						}
					}
					DataRowCollection rows = dataTable.Rows;
					object[] values = array;
					rows.Add(values);
				}
			}
			return dataTable;
		}
	}
}
