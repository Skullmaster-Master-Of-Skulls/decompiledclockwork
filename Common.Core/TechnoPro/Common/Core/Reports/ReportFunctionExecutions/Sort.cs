using System;
using System.Collections.Generic;
using System.Data;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200009B RID: 155
	public class Sort : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x0001FE67 File Offset: 0x0001E067
		public Sort()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001FE82 File Offset: 0x0001E082
		public Sort(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0001FEA0 File Offset: 0x0001E0A0
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
		public OperationContext OpContext { get; set; }

		// Token: 0x0600056C RID: 1388 RVA: 0x0001FEB4 File Offset: 0x0001E0B4
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null;
			if (flag)
			{
				string defaultFunctionParameter = function.GetDefaultFunctionParameter();
				string[] array = defaultFunctionParameter.Split(new char[]
				{
					','
				});
				List<string> list = new List<string>();
				foreach (string text in array)
				{
					string text2 = text.Trim();
					bool flag2 = primaryDataTable.Columns.Contains(text2) && !list.Contains(text2);
					if (flag2)
					{
						list.Add(text2);
					}
				}
				bool flag3 = list.Count > 0;
				if (flag3)
				{
					try
					{
						DataView dataView = new DataView();
						bool flag4 = string.IsNullOrEmpty(primaryDataTable.TableName);
						if (flag4)
						{
							primaryDataTable.TableName = "t";
						}
						dataView.Table = primaryDataTable;
						dataView.Sort = string.Join(",", list.ToArray());
						DataTable dataTable = primaryDataTable.Clone();
						dataTable.TableName = primaryDataTable.TableName;
						foreach (object obj in dataView)
						{
							DataRowView dataRowView = (DataRowView)obj;
							dataTable.ImportRow(dataRowView.Row);
						}
						result.Data.Table = dataTable;
					}
					catch (Exception ex)
					{
						string text3 = string.Format("Common.Core.Reports.ReportFunctionExecutions.Sort:err={0}", ex.ToString());
						result.Result = new RunFunctionResult
						{
							Status = new RunStatus
							{
								ErrorMessage = text3,
								LastStatusStep = eRunStatusStep.Failed
							},
							Function = function
						};
						CWLogger.Logger.Warn(text3);
					}
				}
			}
		}

		// Token: 0x04000112 RID: 274
		private ReportDAO dao;
	}
}
