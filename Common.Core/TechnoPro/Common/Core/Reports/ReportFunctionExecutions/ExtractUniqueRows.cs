using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200007B RID: 123
	public class ExtractUniqueRows : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0000672B File Offset: 0x0000492B
		public ExtractUniqueRows()
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001B120 File Offset: 0x00019320
		public ExtractUniqueRows(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001B132 File Offset: 0x00019332
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001B13A File Offset: 0x0001933A
		public OperationContext OpContext { get; set; }

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001B144 File Offset: 0x00019344
		private bool ObjectArraysMatch(object[] o1, object[] o2)
		{
			for (int i = 0; i < o1.Length; i++)
			{
				bool flag = o1[i] != o2[i];
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001B180 File Offset: 0x00019380
		private object[] GetDataRowValuesForComparing(DataTable t, DataRow dr, string[] columnsMustMatch)
		{
			object[] array = new object[columnsMustMatch.Length];
			for (int i = 0; i < columnsMustMatch.Length; i++)
			{
				string text = columnsMustMatch[i];
				bool flag = t.Columns[text].DataType == typeof(string);
				object obj;
				if (flag)
				{
					obj = ((dr[text] == DBNull.Value) ? "" : ((string)dr[text]));
				}
				else
				{
					obj = dr[text];
				}
				array[i] = obj;
			}
			return array;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001B214 File Offset: 0x00019414
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string[] array = function.GetDefaultFunctionParameter().Split(new char[]
			{
				','
			});
			bool flag = array.Length < 1;
			if (!flag)
			{
				DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
				bool flag2 = primaryDataTable == null;
				if (!flag2)
				{
					DataTable dataTable = primaryDataTable.Clone();
					DataView dataView = new DataView();
					bool flag3 = string.IsNullOrEmpty(primaryDataTable.TableName);
					if (flag3)
					{
						primaryDataTable.TableName = "t";
					}
					dataView.Table = primaryDataTable;
					dataView.Sort = function.GetDefaultFunctionParameter();
					int j;
					for (int i = 0; i < dataView.Count; i = j)
					{
						DataRow row = dataView[i].Row;
						object[] dataRowValuesForComparing = this.GetDataRowValuesForComparing(primaryDataTable, row, array);
						for (j = i + 1; j < dataView.Count; j++)
						{
							DataRow row2 = dataView[j].Row;
							object[] dataRowValuesForComparing2 = this.GetDataRowValuesForComparing(primaryDataTable, row2, array);
							bool flag4 = !this.ObjectArraysMatch(dataRowValuesForComparing, dataRowValuesForComparing2);
							if (flag4)
							{
								break;
							}
						}
						dataTable.ImportRow(row);
					}
					result.Data.Table = dataTable;
				}
			}
		}
	}
}
