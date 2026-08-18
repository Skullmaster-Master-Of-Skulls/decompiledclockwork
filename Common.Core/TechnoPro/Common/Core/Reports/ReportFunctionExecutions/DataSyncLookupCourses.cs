using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200006B RID: 107
	public class DataSyncLookupCourses : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0000672B File Offset: 0x0000492B
		public DataSyncLookupCourses()
		{
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00018992 File Offset: 0x00016B92
		public DataSyncLookupCourses(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000189A4 File Offset: 0x00016BA4
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x000189AC File Offset: 0x00016BAC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000453 RID: 1107 RVA: 0x000189B8 File Offset: 0x00016BB8
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null || primaryDataTable.Rows.Count <= 0;
			if (!flag)
			{
				IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(this.OpContext);
				IList<DataSyncExternalCourseSyncResult> list = dataSyncCourseManager.DataSyncLookupCourses(primaryDataTable);
				DataTable dataTable = new DataTable
				{
					TableName = "Results"
				};
				dataTable.Columns.Add("msg");
				foreach (DataSyncExternalCourseSyncResult result2 in list)
				{
					dataTable.Rows.Add(new object[]
					{
						result2.GetResultString()
					});
				}
				result.Data.Table = dataTable;
			}
		}
	}
}
