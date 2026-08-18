using System;
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
	// Token: 0x0200007D RID: 125
	public class HideColumns : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x0001B468 File Offset: 0x00019668
		public HideColumns()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001B483 File Offset: 0x00019683
		public HideColumns(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001B4A1 File Offset: 0x000196A1
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001B4A9 File Offset: 0x000196A9
		public OperationContext OpContext { get; set; }

		// Token: 0x060004BF RID: 1215 RVA: 0x0001B4B4 File Offset: 0x000196B4
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null;
			if (!flag)
			{
				DataView dataView = new DataView();
				bool flag2 = string.IsNullOrEmpty(primaryDataTable.TableName);
				if (flag2)
				{
					primaryDataTable.TableName = "t";
				}
				dataView.Table = primaryDataTable;
				try
				{
					dataView.RowFilter = function.GetDefaultFunctionParameter();
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
					string text = string.Format("Common.Core.Reports.ReportFunctionExecutions.HideColumns:err={0}", ex.ToString());
					result.Result = new RunFunctionResult
					{
						Status = new RunStatus
						{
							ErrorMessage = text,
							LastStatusStep = eRunStatusStep.Failed
						},
						Function = function
					};
					CWLogger.Logger.Error(text);
				}
			}
		}

		// Token: 0x040000E9 RID: 233
		private ReportDAO dao;
	}
}
