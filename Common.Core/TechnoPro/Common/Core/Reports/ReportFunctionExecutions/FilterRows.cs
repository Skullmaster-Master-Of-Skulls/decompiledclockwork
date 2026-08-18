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
	// Token: 0x0200007C RID: 124
	public class FilterRows : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0001B33F File Offset: 0x0001953F
		public FilterRows()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001B35A File Offset: 0x0001955A
		public FilterRows(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0001B378 File Offset: 0x00019578
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0001B380 File Offset: 0x00019580
		public OperationContext OpContext { get; set; }

		// Token: 0x060004BA RID: 1210 RVA: 0x0001B38C File Offset: 0x0001958C
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null;
			if (flag)
			{
				string defaultFunctionParameter = function.GetDefaultFunctionParameter();
				try
				{
					DataRow[] array = primaryDataTable.Select(defaultFunctionParameter);
					DataTable dataTable = primaryDataTable.Clone();
					foreach (DataRow row in array)
					{
						dataTable.ImportRow(row);
					}
					result.Data.Table = dataTable;
				}
				catch (Exception ex)
				{
					string text = string.Format("Common.Core.Reports.ReportFunctionExecutions.Filter:err={0}", ex.ToString());
					result.Result = new RunFunctionResult
					{
						Status = new RunStatus
						{
							ErrorMessage = text,
							LastStatusStep = eRunStatusStep.Failed
						},
						Function = function
					};
					CWLogger.Logger.Warn(text);
				}
			}
		}

		// Token: 0x040000E7 RID: 231
		private ReportDAO dao;
	}
}
