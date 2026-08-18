using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.ICore.Reports
{
	// Token: 0x02000025 RID: 37
	public interface IReportFunctionExecute : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000F7 RID: 247
		void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function);
	}
}
