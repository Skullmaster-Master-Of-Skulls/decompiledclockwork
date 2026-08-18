using System;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000099 RID: 153
	public class RunAnotherReportWithStaticParameters : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x0001FA80 File Offset: 0x0001DC80
		public RunAnotherReportWithStaticParameters()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001FA9B File Offset: 0x0001DC9B
		public RunAnotherReportWithStaticParameters(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001FAB9 File Offset: 0x0001DCB9
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0001FAC1 File Offset: 0x0001DCC1
		public OperationContext OpContext { get; set; }

		// Token: 0x06000561 RID: 1377 RVA: 0x0001FACC File Offset: 0x0001DCCC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, TechnoPro.Common.Public.Entities.Reports.ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			int num = defaultFunctionParameter.IndexOf('`');
			bool flag = num > 0;
			int num2;
			if (flag)
			{
				int.TryParse(defaultFunctionParameter.Substring(0, num), out num2);
				string[] ps = defaultFunctionParameter.Substring(num + 1).Split(new char[]
				{
					'`'
				});
				TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.SetVariablesExplicitly(ps, 0, CurrentWholeReportResult.CurrentReportParameters, this.OpContext);
			}
			else
			{
				int.TryParse(defaultFunctionParameter, out num2);
			}
			bool flag2 = num2 > 0;
			if (flag2)
			{
				RunAnotherReport runAnotherReport = new RunAnotherReport(this.OpContext);
				RunReportResult runReportResult = runAnotherReport.RunAnotherClockWorkReport(defaultFunctionParameter, CurrentWholeReportResult);
				bool flag3 = runReportResult != null && runReportResult.ReportStatus != null && runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
				bool flag4 = flag3;
				if (flag4)
				{
					result.Data.Table = ((runReportResult == null || runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table);
				}
				else
				{
					string text = string.Format("Common.Core.Reports.ReportFunctionExecutions.RunAnotherReportWithStaticParameters Failed:{0}", (runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : (runReportResult.ReportStatus.ErrorMessage ?? "null"));
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

		// Token: 0x0400010F RID: 271
		private ReportDAO dao;
	}
}
