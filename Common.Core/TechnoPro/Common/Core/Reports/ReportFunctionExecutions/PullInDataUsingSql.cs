using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000093 RID: 147
	public class PullInDataUsingSql : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0000672B File Offset: 0x0000492B
		public PullInDataUsingSql()
		{
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001EC15 File Offset: 0x0001CE15
		public PullInDataUsingSql(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001EC27 File Offset: 0x0001CE27
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0001EC2F File Offset: 0x0001CE2F
		public OperationContext OpContext { get; set; }

		// Token: 0x0600053F RID: 1343 RVA: 0x0001EC38 File Offset: 0x0001CE38
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null || primaryDataTable.Rows.Count < 1 || string.IsNullOrEmpty(defaultFunctionParameter);
			if (!flag)
			{
				DataTable dataTable = ReportFunctionsLegacy.PullInData(primaryDataTable, defaultFunctionParameter, this.OpContext);
				bool flag2 = dataTable != null;
				if (flag2)
				{
					result.Data.Table = dataTable;
				}
			}
		}
	}
}
