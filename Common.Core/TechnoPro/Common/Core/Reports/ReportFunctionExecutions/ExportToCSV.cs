using System;
using System.Data;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DataFileIO.cs.Csv;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200007A RID: 122
	public class ExportToCSV : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001B02D File Offset: 0x0001922D
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001B035 File Offset: 0x00019235
		public OperationContext OpContext { get; set; }

		// Token: 0x060004AC RID: 1196 RVA: 0x0001B03E File Offset: 0x0001923E
		public ExportToCSV()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001B059 File Offset: 0x00019259
		public ExportToCSV(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001B078 File Offset: 0x00019278
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null;
			if (flag)
			{
				try
				{
					string defaultFunctionParameter = function.GetDefaultFunctionParameter();
					CsvUtility.ExportDataTableToCsv(defaultFunctionParameter, primaryDataTable, true);
					result.Data.Table = primaryDataTable;
				}
				catch (Exception ex)
				{
					string text = string.Format("Common.Core.Reports.ReportFunctionExecutions.ExportToCSV:err={0}", ex.ToString());
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

		// Token: 0x040000E4 RID: 228
		private ReportDAO dao;
	}
}
