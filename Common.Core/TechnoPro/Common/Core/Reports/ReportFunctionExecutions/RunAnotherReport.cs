using System;
using System.Collections.Generic;
using System.Linq;
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
	// Token: 0x02000095 RID: 149
	public class RunAnotherReport : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x0001EE91 File Offset: 0x0001D091
		public RunAnotherReport()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001EEAC File Offset: 0x0001D0AC
		public RunAnotherReport(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001EECA File Offset: 0x0001D0CA
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001EED2 File Offset: 0x0001D0D2
		public OperationContext OpContext { get; set; }

		// Token: 0x0600054A RID: 1354 RVA: 0x0001EEDC File Offset: 0x0001D0DC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			RunReportResult runReportResult = this.RunAnotherClockWorkReport(defaultFunctionParameter, CurrentWholeReportResult);
			bool flag = runReportResult != null && runReportResult.ReportStatus != null && runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
			bool flag2 = flag;
			if (flag2)
			{
				result.Data.Table = ((runReportResult == null || runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table);
			}
			else
			{
				string text = string.Format("Common.Core.Reports.ReportFunctionExecutions.RunAnotherReport:ExecuteReport2 Failed:{0}", (runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : (runReportResult.ReportStatus.ErrorMessage ?? "null"));
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

		// Token: 0x0600054B RID: 1355 RVA: 0x0001EFBC File Offset: 0x0001D1BC
		public RunReportResult RunAnotherClockWorkReport(string parameters, RunReportResult CurrentWholeReportResult)
		{
			ReportManager reportManager = new ReportManager(this.OpContext);
			int num = parameters.IndexOf('.');
			int num2 = 0;
			List<int> list = new List<int>();
			bool flag = num > 0;
			if (flag)
			{
				List<string> list2 = parameters.Split(new char[]
				{
					'.'
				}).ToList<string>();
				bool flag2 = list2.Count > 0;
				if (flag2)
				{
					int.TryParse(list2[0], out num2);
				}
				for (int i = 1; i < list2.Count; i++)
				{
					int item;
					bool flag3 = int.TryParse(list2[i], out item);
					if (flag3)
					{
						list.Add(item);
					}
				}
			}
			else
			{
				int.TryParse(parameters, out num2);
			}
			ReportCollection reportCollection = reportManager.LoadReports(new ReportContext
			{
				ReturnReportDisplayInformationOnly = false,
				ReportSource = eReportSource.All,
				ReportIds = new List<int>
				{
					num2
				}
			});
			bool flag4 = reportCollection == null;
			if (flag4)
			{
				CWLogger.Logger.Warn("Common.Core.Reports.ReportFunctionExecutions.RunAnotherReport:Report id {0} could not be found", num2.ToString());
			}
			IList<ReportParameter> currentReportParameters = CurrentWholeReportResult.CurrentReportParameters;
			return reportManager.ExecuteReport2(num2, (list.Count > 0) ? list : null, null, (currentReportParameters == null) ? new ReportParameter[0] : currentReportParameters.ToArray<ReportParameter>());
		}

		// Token: 0x04000109 RID: 265
		private ReportDAO dao;
	}
}
