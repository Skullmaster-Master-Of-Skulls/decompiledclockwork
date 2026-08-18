using System;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200008E RID: 142
	public class Nop : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0000672B File Offset: 0x0000492B
		public Nop()
		{
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001D808 File Offset: 0x0001BA08
		public Nop(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001D81A File Offset: 0x0001BA1A
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001D822 File Offset: 0x0001BA22
		public OperationContext OpContext { get; set; }

		// Token: 0x06000524 RID: 1316 RVA: 0x0001D82B File Offset: 0x0001BA2B
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			result.Data.IsPrimary = false;
		}
	}
}
