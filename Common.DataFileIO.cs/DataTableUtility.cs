using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TechnoPro.Common.DataFileIO.cs
{
	// Token: 0x02000002 RID: 2
	public static class DataTableUtility
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string ExportToFormattedText(DataView dv, bool showColumnNames = true)
		{
			DataTable table = dv.Table;
			StringBuilder stringBuilder = new StringBuilder();
			int[] array = new int[table.Columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = table.Columns[i].ColumnName.Length + 1;
			}
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				for (int j = 0; j < table.Columns.Count; j++)
				{
					DataColumn dataColumn = table.Columns[j];
					bool flag = dataColumn.ColumnMapping != MappingType.Hidden;
					if (flag)
					{
						int num = row[j].ToString().Trim().Length + 1;
						bool flag2 = num > array[j];
						if (flag2)
						{
							array[j] = num;
						}
					}
				}
			}
			if (showColumnNames)
			{
				for (int k = 0; k < table.Columns.Count; k++)
				{
					DataColumn dataColumn2 = table.Columns[k];
					bool flag3 = dataColumn2.ColumnMapping != MappingType.Hidden;
					if (flag3)
					{
						string text = dataColumn2.ColumnName;
						int num2 = array[k] - text.Length;
						bool flag4 = num2 > 0;
						if (flag4)
						{
							text += new string(' ', num2);
						}
						stringBuilder.Append(text);
					}
				}
				stringBuilder.AppendLine();
				for (int l = 0; l < table.Columns.Count; l++)
				{
					DataColumn dataColumn3 = table.Columns[l];
					bool flag5 = dataColumn3.ColumnMapping != MappingType.Hidden;
					if (flag5)
					{
						string text2 = "";
						int num3 = array[l] - text2.Length - 1;
						bool flag6 = num3 > 0;
						if (flag6)
						{
							text2 += new string('=', num3);
						}
						text2 += "+";
						stringBuilder.Append(text2);
					}
				}
				stringBuilder.AppendLine();
			}
			foreach (object obj2 in dv)
			{
				DataRowView dataRowView2 = (DataRowView)obj2;
				DataRow row2 = dataRowView2.Row;
				for (int m = 0; m < table.Columns.Count; m++)
				{
					DataColumn dataColumn4 = table.Columns[m];
					bool flag7 = dataColumn4.ColumnMapping != MappingType.Hidden;
					if (flag7)
					{
						string text3 = row2[m].ToString();
						int num4 = array[m] - text3.Length;
						bool flag8 = num4 > 0;
						if (flag8)
						{
							text3 += new string(' ', num4);
						}
						stringBuilder.Append(text3);
					}
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000023AC File Offset: 0x000005AC
		public static DataTable JoinTables(DataTable LeftTable, DataTable RightTable, string LeftPrimaryColumn, string RightPrimaryColumn)
		{
			DataTable dataTable = new DataSet
			{
				Tables = 
				{
					" "
				}
			}.Tables[0];
			DataColumn[] array = new DataColumn[LeftTable.Columns.Count];
			LeftTable.Columns.CopyTo(array, 0);
			foreach (DataColumn dataColumn in array)
			{
				bool flag = !dataTable.Columns.Contains(dataColumn.ToString());
				if (flag)
				{
					dataTable.Columns.Add(dataColumn.ToString());
				}
			}
			DataColumn[] array3 = new DataColumn[RightTable.Columns.Count];
			RightTable.Columns.CopyTo(array3, 0);
			foreach (DataColumn dataColumn2 in array3)
			{
				bool flag2 = !dataTable.Columns.Contains(dataColumn2.ToString());
				if (flag2)
				{
					bool flag3 = dataColumn2.ToString() != RightPrimaryColumn;
					if (flag3)
					{
						dataTable.Columns.Add(dataColumn2.ToString());
					}
				}
			}
			foreach (object obj in LeftTable.Rows)
			{
				DataRow row = (DataRow)obj;
				dataTable.ImportRow(row);
			}
			ArrayList arrayList = new ArrayList();
			arrayList = DataTableUtility.DataSetToArrayList(LeftTable.Columns[LeftPrimaryColumn].Ordinal, LeftTable);
			foreach (object obj2 in RightTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				bool flag4 = arrayList.Contains(dataRow[RightPrimaryColumn]);
				if (flag4)
				{
					string filterExpression = string.Concat(new string[]
					{
						"[",
						dataTable.Columns[LeftPrimaryColumn].ColumnName,
						"]='",
						dataRow[RightPrimaryColumn].ToString(),
						"'"
					});
					DataRow[] array5 = dataTable.Select(filterExpression);
					int index = dataTable.Rows.IndexOf(array5[0]);
					foreach (object obj3 in RightTable.Columns)
					{
						DataColumn dataColumn3 = (DataColumn)obj3;
						bool flag5 = dataColumn3.ColumnName != RightPrimaryColumn;
						if (flag5)
						{
							dataTable.Rows[index][dataColumn3.ColumnName.ToString().Trim()] = dataRow[dataColumn3.ColumnName.ToString().Trim()].ToString();
						}
					}
				}
				else
				{
					int count = dataTable.Rows.Count;
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2[0] = dataRow[0].ToString();
					dataTable.Rows.Add(dataRow2);
					foreach (object obj4 in RightTable.Columns)
					{
						DataColumn dataColumn4 = (DataColumn)obj4;
						bool flag6 = dataColumn4.ColumnName != RightPrimaryColumn;
						if (flag6)
						{
							dataTable.Rows[count][dataColumn4.ColumnName.ToString().Trim()] = dataRow[dataColumn4.ColumnName.ToString().Trim()].ToString();
						}
					}
				}
			}
			return dataTable;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000027D0 File Offset: 0x000009D0
		private static ArrayList DataSetToArrayList(int ColumnIndex, DataTable dataTable)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				arrayList.Add(dataRow[ColumnIndex]);
			}
			return arrayList;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002840 File Offset: 0x00000A40
		private static string GetNewColumnName(string possibleColName, DataTable tableItsGoingInto)
		{
			string text = possibleColName;
			int num = 1;
			while (tableItsGoingInto.Columns.Contains(text) && num < 10000)
			{
				text = possibleColName + "_" + num.ToString();
				num++;
			}
			return (num < 10000) ? text : null;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000289C File Offset: 0x00000A9C
		public static DataTable JoinTables2<T>(DataTable dataTable1, DataTable dataTable2, string uniqueColName)
		{
			bool flag = !dataTable1.Columns.Contains(uniqueColName) || !dataTable2.Columns.Contains(uniqueColName);
			if (flag)
			{
				throw new Exception("One or more tables do not contain the unique column name: " + uniqueColName);
			}
			List<DataTableUtility.JoinColumn> list = new List<DataTableUtility.JoinColumn>();
			foreach (object obj in dataTable2.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				string columnName = dataColumn.ColumnName;
				bool flag2 = !columnName.Equals(uniqueColName, StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					string newColumnName = DataTableUtility.GetNewColumnName(columnName, dataTable1);
					bool flag3 = !string.IsNullOrEmpty(newColumnName);
					if (flag3)
					{
						list.Add(new DataTableUtility.JoinColumn
						{
							OriginalDataColumn = dataColumn,
							ColumnNameInNewTable = newColumnName
						});
					}
				}
			}
			bool flag4 = list.Count < 1;
			DataTable result;
			if (flag4)
			{
				result = dataTable1;
			}
			else
			{
				foreach (DataTableUtility.JoinColumn joinColumn in list)
				{
					dataTable1.Columns.Add(joinColumn.ColumnNameInNewTable, joinColumn.OriginalDataColumn.DataType);
				}
				foreach (object obj2 in dataTable1.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					bool flag5 = dataRow[uniqueColName] != DBNull.Value;
					if (flag5)
					{
						T t = (T)((object)dataRow[uniqueColName]);
						DataRow[] array = dataTable2.Select(uniqueColName + "='" + t.ToString().Replace("'", "''") + "'");
						bool flag6 = array.Length != 0;
						if (flag6)
						{
							DataRow dataRow2 = array[0];
							foreach (DataTableUtility.JoinColumn joinColumn2 in list)
							{
								dataRow[joinColumn2.ColumnNameInNewTable] = dataRow2[joinColumn2.OriginalDataColumn.ColumnName];
							}
						}
					}
				}
				result = dataTable1;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002B20 File Offset: 0x00000D20
		public static DataTable JoinTables(DataTable dataTable1, DataTable dataTable2, string uniqueColName)
		{
			return DataTableUtility.JoinTables2<int>(dataTable1, dataTable2, uniqueColName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002B3C File Offset: 0x00000D3C
		public static IList<T> ExtractNonNullColumnValues<T>(this DataTable dataTable, string colName)
		{
			List<T> list = new List<T>();
			bool flag = dataTable == null;
			IList<T> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					bool flag2 = dataRow[colName] != DBNull.Value;
					if (flag2)
					{
						T item = (T)((object)dataRow[colName]);
						bool flag3 = item is string;
						T t;
						if (flag3)
						{
							string str = (string)dataRow[colName];
							t = list.FirstOrDefault((T g) => g.ToString().Equals(str, StringComparison.OrdinalIgnoreCase));
						}
						else
						{
							t = list.FirstOrDefault((T g) => g.Equals(item));
						}
						bool flag4 = t != null;
						if (flag4)
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002C70 File Offset: 0x00000E70
		public static void RenameColumns(ref DataTable table, string nameValuePairs)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] array = nameValuePairs.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				bool flag = num > 0;
				if (flag)
				{
					string key = text.Substring(0, num);
					string value = text.Substring(num + 1);
					bool flag2 = !dictionary.ContainsKey(key);
					if (flag2)
					{
						dictionary.Add(key, value);
					}
				}
			}
			DataTableUtility.RenameColumns(ref table, dictionary);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002D04 File Offset: 0x00000F04
		public static void RenameColumns(ref DataTable table, IDictionary<string, string> nameValuePairs)
		{
			foreach (KeyValuePair<string, string> keyValuePair in nameValuePairs)
			{
				string key = keyValuePair.Key;
				bool flag = table.Columns.Contains(key);
				if (flag)
				{
					table.Columns[key].ColumnName = keyValuePair.Value;
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002D80 File Offset: 0x00000F80
		public static string ConvertDataViewToCsv(this DataView dv)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataTable table = dv.Table;
			bool flag = table.Columns.Count == 0;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (object obj in table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					bool flag2 = dataColumn.ColumnMapping != MappingType.Hidden;
					if (flag2)
					{
						stringBuilder.Append("\"" + dataColumn.ToString().Replace("\"", "\"\"") + "\",");
					}
				}
				stringBuilder.AppendLine();
				foreach (object obj2 in dv)
				{
					DataRowView dataRowView = (DataRowView)obj2;
					for (int i = 0; i < table.Columns.Count; i++)
					{
						bool flag3 = table.Columns[i].ColumnMapping != MappingType.Hidden;
						if (flag3)
						{
							object obj3 = dataRowView.Row.ItemArray[i];
							stringBuilder.Append("\"" + obj3.ToString().Replace("\"", "\"\"") + "\",");
						}
					}
					stringBuilder.AppendLine();
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002F2C File Offset: 0x0000112C
		public static string ConvertDataViewToCharDelimited(this DataView dv, string colDelimiter, string rowDelimiter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataTable table = dv.Table;
			string oldValue = '\t'.ToString();
			bool flag = table.Columns.Count == 0;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = true;
				foreach (object obj in table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					bool flag3 = dataColumn.ColumnMapping != MappingType.Hidden;
					if (flag3)
					{
						bool flag4 = flag2;
						if (flag4)
						{
							flag2 = false;
						}
						else
						{
							stringBuilder.Append(colDelimiter);
						}
						stringBuilder.Append(dataColumn.ToString().Replace(oldValue, " "));
					}
				}
				stringBuilder.AppendLine();
				bool flag5 = true;
				foreach (object obj2 in dv)
				{
					DataRowView dataRowView = (DataRowView)obj2;
					bool flag6 = flag5;
					if (flag6)
					{
						flag5 = false;
					}
					else
					{
						stringBuilder.Append(rowDelimiter);
					}
					flag2 = true;
					for (int i = 0; i < table.Columns.Count; i++)
					{
						bool flag7 = table.Columns[i].ColumnMapping != MappingType.Hidden;
						if (flag7)
						{
							bool flag8 = flag2;
							if (flag8)
							{
								flag2 = false;
							}
							else
							{
								stringBuilder.Append(colDelimiter);
							}
							object obj3 = dataRowView.Row.ItemArray[i];
							stringBuilder.Append(obj3.ToString().Replace(oldValue, " "));
						}
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0200000E RID: 14
		internal class JoinColumn
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000034 RID: 52 RVA: 0x000048CD File Offset: 0x00002ACD
			// (set) Token: 0x06000035 RID: 53 RVA: 0x000048D5 File Offset: 0x00002AD5
			public DataColumn OriginalDataColumn { get; set; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000036 RID: 54 RVA: 0x000048DE File Offset: 0x00002ADE
			// (set) Token: 0x06000037 RID: 55 RVA: 0x000048E6 File Offset: 0x00002AE6
			public string ColumnNameInNewTable { get; set; }
		}
	}
}
