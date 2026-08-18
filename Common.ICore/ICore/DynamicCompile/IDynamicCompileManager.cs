using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.ICore.DynamicCompile
{
	// Token: 0x020000A2 RID: 162
	public interface IDynamicCompileManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004C6 RID: 1222
		void RunCustomReportCode(ref RunReportResult runReportResult, string code, RunReportResult CurrentReportResult);

		// Token: 0x060004C7 RID: 1223
		void CompileCode(string code);

		// Token: 0x060004C8 RID: 1224
		IList<ReportCompileLineWarningOrError> TryCompileCode(string code);
	}
}
