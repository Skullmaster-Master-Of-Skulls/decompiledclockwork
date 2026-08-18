using System;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200007F RID: 127
	public class ImportCustomCourses : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000672B File Offset: 0x0000492B
		public ImportCustomCourses()
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001B71C File Offset: 0x0001991C
		public ImportCustomCourses(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0001B72E File Offset: 0x0001992E
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0001B736 File Offset: 0x00019936
		public OperationContext OpContext { get; set; }

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001B740 File Offset: 0x00019940
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(this.OpContext);
			dataSyncCourseManager.CreateLookupCoursesFromCustomCoursesTable();
		}
	}
}
