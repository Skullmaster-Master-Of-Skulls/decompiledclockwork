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
	// Token: 0x0200008C RID: 140
	public class MakeATableTheCurrentTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0001D4D7 File Offset: 0x0001B6D7
		public MakeATableTheCurrentTable()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001D4F2 File Offset: 0x0001B6F2
		public MakeATableTheCurrentTable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001D510 File Offset: 0x0001B710
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0001D518 File Offset: 0x0001B718
		public OperationContext OpContext { get; set; }

		// Token: 0x06000518 RID: 1304 RVA: 0x0001D524 File Offset: 0x0001B724
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			RunFunctionData runFunctionData = this.FindDataByName(defaultFunctionParameter, CurrentWholeReportResult);
			bool flag = runFunctionData != null;
			if (flag)
			{
				result.Data = runFunctionData;
			}
			else
			{
				string text = string.Format("Common.Core.REports.ReportFunctionExecutiosn.MakeATableTheCurrentTable:Can't find table with name={0}", defaultFunctionParameter);
				result.Result = new RunFunctionResult
				{
					Status = new RunStatus
					{
						LastStatusStep = eRunStatusStep.Failed,
						ErrorMessage = text
					}
				};
				CWLogger.Logger.Warn(text);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001D59C File Offset: 0x0001B79C
		public RunFunctionData FindDataByName(string name, RunReportResult CurrentWholeReportResult)
		{
			bool flag = CurrentWholeReportResult.AdditionalData == null;
			if (flag)
			{
				CurrentWholeReportResult.AdditionalData = new List<RunFunctionData>();
			}
			return CurrentWholeReportResult.AdditionalData.FirstOrDefault((RunFunctionData g) => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x040000FE RID: 254
		private ReportDAO dao;
	}
}
