using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000081 RID: 129
	public class BatchDataSyncOldCourses : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0000672B File Offset: 0x0000492B
		public BatchDataSyncOldCourses()
		{
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001B852 File Offset: 0x00019A52
		public BatchDataSyncOldCourses(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001B864 File Offset: 0x00019A64
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0001B86C File Offset: 0x00019A6C
		public OperationContext OpContext { get; set; }

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001B878 File Offset: 0x00019A78
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentReportResult.GetPrimaryDataTable();
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			DataSyncBatchParameters batchSyncParameters = defaultFunctionParameter.ConvertXmlToDataSyncBatchParameters();
			IDataSyncManager dataSyncManager = new DataSyncManager(this.OpContext);
			dataSyncManager.RunBatchDataSyncForOldCourses(primaryDataTable, batchSyncParameters);
		}
	}
}
