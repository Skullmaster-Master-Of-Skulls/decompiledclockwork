using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200006D RID: 109
	public class DataSync_AddLastDataSync : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000672B File Offset: 0x0000492B
		public DataSync_AddLastDataSync()
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00018C80 File Offset: 0x00016E80
		public DataSync_AddLastDataSync(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00018C92 File Offset: 0x00016E92
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00018C9A File Offset: 0x00016E9A
		public OperationContext OpContext { get; set; }

		// Token: 0x0600045D RID: 1117 RVA: 0x00018CA4 File Offset: 0x00016EA4
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			DataTable dataTable = CurrentReportResult.GetPrimaryDataTable() ?? new DataTable("r");
			bool flag = !dataTable.Columns.Contains("LastDataSync");
			if (flag)
			{
				dataTable.Columns.Add("LastDataSync", typeof(DateTime));
			}
			DateTime now = DateTime.Now;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["LastDataSync"] = now;
			}
			Result.Data.Table = dataTable;
		}
	}
}
