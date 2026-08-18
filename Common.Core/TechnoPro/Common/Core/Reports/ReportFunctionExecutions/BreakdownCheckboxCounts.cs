using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000060 RID: 96
	public class BreakdownCheckboxCounts : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000672B File Offset: 0x0000492B
		public BreakdownCheckboxCounts()
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001698E File Offset: 0x00014B8E
		public BreakdownCheckboxCounts(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000169A0 File Offset: 0x00014BA0
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x000169A8 File Offset: 0x00014BA8
		public OperationContext OpContext { get; set; }

		// Token: 0x06000411 RID: 1041 RVA: 0x000169B4 File Offset: 0x00014BB4
		private void ParseParameters(string parameters, out IList<string> uniqueColumns, out IList<string> booleanColumns)
		{
			int num = parameters.IndexOf("~");
			bool flag = num > 0;
			string str;
			string str2;
			if (flag)
			{
				str = parameters.Substring(num + 1).Trim();
				str2 = parameters.Substring(0, num);
			}
			else
			{
				str = "";
				str2 = parameters;
			}
			uniqueColumns = this.ParseColumnNameListString(str);
			booleanColumns = this.ParseColumnNameListString(str2);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00016A10 File Offset: 0x00014C10
		private IList<string> ParseColumnNameListString(string str)
		{
			string text = (str ?? "").Trim();
			bool flag = text.Length < 1;
			IList<string> result;
			if (flag)
			{
				result = new List<string>();
			}
			else
			{
				List<string> list = new List<string>();
				int i = 0;
				int length = text.Length;
				while (i < length)
				{
					int j = i;
					bool flag2 = false;
					StringBuilder stringBuilder = new StringBuilder();
					while (j < length)
					{
						char c = text[j];
						bool flag3 = !flag2 && c == ',';
						if (flag3)
						{
							break;
						}
						char c2 = c;
						char c3 = c2;
						if (c3 != '[')
						{
							if (c3 != ']')
							{
								stringBuilder.Append(c);
							}
							else
							{
								flag2 = false;
							}
						}
						else
						{
							flag2 = true;
						}
						j++;
					}
					list.Add(stringBuilder.ToString().Trim());
					i = j;
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00016AF8 File Offset: 0x00014CF8
		private IEnumerable<string> AutoDetectBooleanColumns(DataTable t)
		{
			List<string> list = new List<string>();
			using (IEnumerator enumerator = t.Columns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataColumn dc = (DataColumn)enumerator.Current;
					string colName = dc.ColumnName;
					bool flag = false;
					bool flag2 = dc.DataType == typeof(bool);
					if (flag2)
					{
						flag = true;
					}
					else
					{
						bool flag3 = dc.DataType == typeof(string);
						if (flag3)
						{
							bool flag4 = (from DataRow dr in t.Rows
							where dr[dc] != DBNull.Value
							select dr[dc].ToString().ToLower()).All((string sb) => sb.Length <= 0 || "trueyes".IndexOf(sb) >= 0);
							bool flag5 = !flag4;
							if (flag5)
							{
								continue;
							}
							flag = true;
						}
					}
					bool flag6 = !flag || list.FirstOrDefault((string g) => g.Equals(colName, StringComparison.OrdinalIgnoreCase)) != null;
					if (!flag6)
					{
						list.Add(colName);
					}
				}
			}
			return list;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00016C74 File Offset: 0x00014E74
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null;
			if (!flag)
			{
				IList<string> uniqueColumns;
				IList<string> list;
				this.ParseParameters(function.GetDefaultFunctionParameter(), out uniqueColumns, out list);
				bool flag2 = uniqueColumns.Count < 1;
				if (flag2)
				{
					uniqueColumns.Add("student_no");
				}
				bool flag3 = list.Count < 1;
				if (flag3)
				{
					list = (from g in this.AutoDetectBooleanColumns(primaryDataTable)
					where uniqueColumns.FirstOrDefault((string h) => h.Equals(g, StringComparison.OrdinalIgnoreCase)) == null
					select g).ToList<string>();
				}
				DataTable dataTable = new DataTable("t");
				dataTable.Columns.Add("Field");
				dataTable.Columns.Add("Count", typeof(int));
				foreach (string text in list)
				{
					List<string> list2 = new List<string>();
					int num = 0;
					foreach (object obj in primaryDataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < uniqueColumns.Count; i++)
						{
							stringBuilder.Append(i.ToString() + ":" + dataRow[uniqueColumns[i]].ToString().Trim().ToLower());
						}
						string item = stringBuilder.ToString();
						bool flag4 = dataRow[text] == DBNull.Value || ((!(dataRow[text] is bool) || !(bool)dataRow[text]) && "trueyes".IndexOf(dataRow[text].ToString().ToLower()) < 0) || list2.Contains(item);
						if (!flag4)
						{
							list2.Add(item);
							num++;
						}
					}
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2[0] = text;
					dataRow2[1] = num;
					dataTable.Rows.Add(dataRow2);
				}
				result.Data.Table = dataTable;
			}
		}
	}
}
