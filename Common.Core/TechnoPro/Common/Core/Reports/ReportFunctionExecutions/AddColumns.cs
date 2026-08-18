using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200005B RID: 91
	public class AddColumns : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0000672B File Offset: 0x0000492B
		public AddColumns()
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00014AF4 File Offset: 0x00012CF4
		public AddColumns(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00014B06 File Offset: 0x00012D06
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00014B0E File Offset: 0x00012D0E
		public OperationContext OpContext { get; set; }

		// Token: 0x060003E8 RID: 1000 RVA: 0x00014B18 File Offset: 0x00012D18
		private IList<ReportDataColumn> GetColumns(string reportParams)
		{
			string[] array = reportParams.Split(new char[]
			{
				'`'
			});
			List<ReportDataColumn> list = new List<ReportDataColumn>();
			char[] separator = new char[]
			{
				','
			};
			foreach (string text in array)
			{
				string[] array3 = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				ReportDataColumn item = new ReportDataColumn
				{
					ColumnName = array3[0].Trim(),
					ColumnDataType = this.GetDataType((array3.Length > 1) ? array3[1] : ""),
					DefaultValue = ((array3.Length > 2) ? array3[2] : null)
				};
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00014BD0 File Offset: 0x00012DD0
		private Type GetDataType(string dataTypeString)
		{
			string text = dataTypeString.Trim().ToLower();
			bool flag = text.Length > 0;
			if (flag)
			{
				string text2 = text;
				string a = text2;
				if (a == "int")
				{
					return typeof(int);
				}
				if (a == "datetime" || a == "date")
				{
					return typeof(DateTime);
				}
				if (a == "double")
				{
					return typeof(double);
				}
			}
			return typeof(string);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00014C70 File Offset: 0x00012E70
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			IList<ReportDataColumn> columns = this.GetColumns(Function.GetDefaultFunctionParameter());
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			foreach (ReportDataColumn reportDataColumn in columns)
			{
				bool flag = !primaryDataTable.Columns.Contains(reportDataColumn.ColumnName);
				if (flag)
				{
					DataColumn column = new DataColumn(reportDataColumn.ColumnName, reportDataColumn.ColumnDataType);
					primaryDataTable.Columns.Add(column);
					bool flag2 = !string.IsNullOrEmpty(reportDataColumn.DefaultValue);
					if (flag2)
					{
						foreach (object obj in primaryDataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							string text = reportDataColumn.DefaultValue ?? "";
							bool flag3 = reportDataColumn.ColumnDataType == typeof(int);
							if (flag3)
							{
								int num;
								bool flag4 = int.TryParse(text, out num);
								if (flag4)
								{
									dataRow[column] = num;
								}
							}
							else
							{
								bool flag5 = reportDataColumn.ColumnDataType == typeof(double);
								if (flag5)
								{
									double num2;
									bool flag6 = double.TryParse(text, out num2);
									if (flag6)
									{
										dataRow[column] = num2;
									}
								}
								else
								{
									bool flag7 = reportDataColumn.ColumnDataType == typeof(DateTime);
									if (flag7)
									{
										DateTime dateTime;
										bool flag8 = DateTime.TryParse(text, out dateTime);
										if (flag8)
										{
											dataRow[column] = dateTime;
										}
									}
									else
									{
										dataRow[column] = text;
									}
								}
							}
						}
					}
				}
			}
			result.Data.Table = CurrentWholeReportResult.GetPrimaryDataView();
		}
	}
}
