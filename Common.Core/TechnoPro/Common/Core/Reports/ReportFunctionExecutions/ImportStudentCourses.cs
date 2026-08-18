using System;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000082 RID: 130
	public class ImportStudentCourses : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public ImportStudentCourses()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001B8CB File Offset: 0x00019ACB
		public ImportStudentCourses(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001B8E9 File Offset: 0x00019AE9
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			Result.Data.Table = this.dao.ImportStudentCourses(CurrentWholeReportResult.GetPrimaryDataView(), Function.GetDefaultFunctionParameter());
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001B910 File Offset: 0x00019B10
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0001B918 File Offset: 0x00019B18
		public OperationContext OpContext { get; set; }

		// Token: 0x040000F1 RID: 241
		private ReportDAO dao;
	}
}
