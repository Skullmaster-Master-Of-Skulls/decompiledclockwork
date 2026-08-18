using System;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DynamicCompile;
using TechnoPro.Common.ICore.DynamicCompile;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000076 RID: 118
	public class ExecuteCSharp : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0001AB2C File Offset: 0x00018D2C
		public ExecuteCSharp()
		{
			this.dcm = new DynamicCompileManager(this.OpContext);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001AB47 File Offset: 0x00018D47
		public ExecuteCSharp(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dcm = new DynamicCompileManager(opContext);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0001AB65 File Offset: 0x00018D65
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0001AB6D File Offset: 0x00018D6D
		public OperationContext OpContext { get; set; }

		// Token: 0x0600049A RID: 1178 RVA: 0x0001AB78 File Offset: 0x00018D78
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			int num = defaultFunctionParameter.IndexOf("using migration;");
			bool flag = num == 0;
			if (flag)
			{
				throw new NotImplementedException("Execute script migration is not implemented from here.");
			}
			RunReportResult runReportResult = new RunReportResult
			{
				PrimaryData = result.Data
			};
			this.dcm.RunCustomReportCode(ref runReportResult, defaultFunctionParameter, CurrentWholeReportResult);
			bool flag2 = runReportResult.PrimaryData == null;
			if (flag2)
			{
				runReportResult.PrimaryData = new RunFunctionData();
			}
			result.Data = runReportResult.PrimaryData;
			result.Data.IsPrimary = true;
		}

		// Token: 0x040000DE RID: 222
		private IDynamicCompileManager dcm;
	}
}
