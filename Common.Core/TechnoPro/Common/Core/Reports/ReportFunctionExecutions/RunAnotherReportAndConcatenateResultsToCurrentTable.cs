using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000096 RID: 150
	public class RunAnotherReportAndConcatenateResultsToCurrentTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0000672B File Offset: 0x0000492B
		public RunAnotherReportAndConcatenateResultsToCurrentTable()
		{
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001F102 File Offset: 0x0001D302
		public RunAnotherReportAndConcatenateResultsToCurrentTable(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001F114 File Offset: 0x0001D314
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001F11C File Offset: 0x0001D31C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000550 RID: 1360 RVA: 0x0001F128 File Offset: 0x0001D328
		private Dictionary<string, string> GetRenameMappings(string functionParams)
		{
			string[] array = functionParams.Split(new char[]
			{
				','
			});
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in array)
			{
				bool flag = text.Trim().Length > 0;
				if (flag)
				{
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						string key = text.Substring(0, num).Trim();
						bool flag3 = !dictionary.ContainsKey(key);
						if (flag3)
						{
							dictionary.Add(key, text.Substring(num + 1));
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, TechnoPro.Common.Public.Entities.Reports.ReportFunction function)
		{
			DataTable dataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = dataTable == null;
			if (flag)
			{
				dataTable = new DataTable("t");
			}
			string[] array = function.GetDefaultFunctionParameter().Split(new char[]
			{
				'`'
			});
			int num = int.Parse(array[0]);
			RunAnotherReport runAnotherReport = new RunAnotherReport(this.OpContext);
			RunReportResult runReportResult = runAnotherReport.RunAnotherClockWorkReport(num.ToString(), CurrentWholeReportResult);
			DataTable dataTable2 = (runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table;
			bool flag2 = dataTable2 != null;
			if (flag2)
			{
				bool flag3 = array.Length > 1;
				if (flag3)
				{
					string[] array2 = array[1].Split(new char[]
					{
						','
					});
					foreach (string text in array2)
					{
						string[] array4 = text.Split(new char[]
						{
							'='
						});
						bool flag4 = array4.Length == 2;
						if (flag4)
						{
							string name = array4[0].Trim();
							string columnName = array4[1].Trim();
							bool flag5 = dataTable2.Columns.Contains(name);
							if (flag5)
							{
								dataTable2.Columns[name].ColumnName = columnName;
							}
						}
					}
					bool flag6 = array.Length > 2;
					if (flag6)
					{
						string[] array5 = array[2].Trim().Split(new char[]
						{
							','
						});
						bool flag7 = array5.Length >= 3;
						if (flag7)
						{
							TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.StampTable(ref dataTable2, array5[0], array5[1], array5[2]);
						}
					}
				}
				bool flag8 = dataTable != null && dataTable.Rows.Count > 0;
				if (flag8)
				{
					int[] array6 = new int[dataTable2.Columns.Count];
					for (int j = 0; j < dataTable2.Columns.Count; j++)
					{
						int num2 = dataTable.Columns.IndexOf(dataTable2.Columns[j].ColumnName);
						bool flag9 = num2 >= 0;
						if (flag9)
						{
							array6[j] = num2;
						}
						else
						{
							DataColumn dataColumn = dataTable.Columns.Add(dataTable2.Columns[j].ColumnName);
							array6[j] = dataColumn.Ordinal;
						}
					}
					for (int k = 0; k < dataTable2.Rows.Count; k++)
					{
						object[] array7 = new object[dataTable.Columns.Count];
						for (int l = 0; l < array6.Length; l++)
						{
							array7[array6[l]] = dataTable2.Rows[k][l];
						}
						dataTable.Rows.Add(array7);
					}
				}
				result.Data.Table = dataTable2;
			}
		}
	}
}
