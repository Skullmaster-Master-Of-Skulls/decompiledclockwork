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
	// Token: 0x02000097 RID: 151
	public class RunAnotherReportAndConcatenateUniqueResultsToCurrentTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0000672B File Offset: 0x0000492B
		public RunAnotherReportAndConcatenateUniqueResultsToCurrentTable()
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001F4A2 File Offset: 0x0001D6A2
		public RunAnotherReportAndConcatenateUniqueResultsToCurrentTable(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001F4B4 File Offset: 0x0001D6B4
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0001F4BC File Offset: 0x0001D6BC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000556 RID: 1366 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
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

		// Token: 0x06000557 RID: 1367 RVA: 0x0001F574 File Offset: 0x0001D774
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
			string matchingColsStr = array[1];
			string colsToImportStr = array[2];
			RunAnotherReport runAnotherReport = new RunAnotherReport(this.OpContext);
			RunReportResult runReportResult = runAnotherReport.RunAnotherClockWorkReport(num.ToString(), CurrentWholeReportResult);
			DataTable dataTable2 = (runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table;
			bool flag2 = dataTable2 != null;
			if (flag2)
			{
				bool flag3 = array.Length > 3;
				if (flag3)
				{
					string[] array2 = array[3].Split(new char[]
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
					bool flag6 = array.Length > 4;
					if (flag6)
					{
						string[] array5 = array[4].Trim().Split(new char[]
						{
							','
						});
						bool flag7 = array5.Length >= 3;
						if (flag7)
						{
							TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.StampTable(ref dataTable2, array5[0], array5[1], array5[2]);
						}
					}
					TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.RunAnotherReportAndConcatenateRowsThatArentAlreadyThere(ref dataTable2, dataTable, matchingColsStr, colsToImportStr);
				}
				result.Data.Table = dataTable2;
			}
		}
	}
}
