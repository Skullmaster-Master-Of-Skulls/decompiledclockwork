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
	// Token: 0x0200005E RID: 94
	public class BatchDataSync : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000672B File Offset: 0x0000492B
		public BatchDataSync()
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000150A4 File Offset: 0x000132A4
		public BatchDataSync(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000150B6 File Offset: 0x000132B6
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x000150BE File Offset: 0x000132BE
		public OperationContext OpContext { get; set; }

		// Token: 0x060003FB RID: 1019 RVA: 0x000150C8 File Offset: 0x000132C8
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			DataSyncBatchParameters batchSyncParameters = defaultFunctionParameter.ConvertXmlToDataSyncBatchParameters();
			IDataSyncManager dataSyncManager = new DataSyncManager(this.OpContext);
			dataSyncManager.RunBatchDataSync(primaryDataTable, batchSyncParameters);
		}
	}
}
