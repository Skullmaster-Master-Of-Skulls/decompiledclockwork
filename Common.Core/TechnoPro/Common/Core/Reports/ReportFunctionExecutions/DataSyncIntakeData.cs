using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200006A RID: 106
	public class DataSyncIntakeData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0000672B File Offset: 0x0000492B
		public DataSyncIntakeData()
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000188E8 File Offset: 0x00016AE8
		public DataSyncIntakeData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000188FA File Offset: 0x00016AFA
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00018902 File Offset: 0x00016B02
		public OperationContext OpContext { get; set; }

		// Token: 0x0600044E RID: 1102 RVA: 0x0001890C File Offset: 0x00016B0C
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null && primaryDataTable.Rows.Count > 0;
			if (flag)
			{
				IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(this.OpContext);
				string student_no = primaryDataTable.Columns.Contains("student_no") ? primaryDataTable.Rows[0]["student_no"].ToString() : "";
				dataSyncInfoManager.DataSyncIntakeData(student_no);
			}
			result.Data.Table = primaryDataTable;
		}
	}
}
