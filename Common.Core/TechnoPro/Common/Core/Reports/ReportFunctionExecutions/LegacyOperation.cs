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
	// Token: 0x02000085 RID: 133
	public class LegacyOperation : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001BBF5 File Offset: 0x00019DF5
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0001BBFD File Offset: 0x00019DFD
		public OperationContext OpContext { get; set; }

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001BC06 File Offset: 0x00019E06
		public LegacyOperation()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001BC21 File Offset: 0x00019E21
		public LegacyOperation(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001BC44 File Offset: 0x00019E44
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable table = this.dao.ExecuteLegacyFunction(CurrentWholeReportResult.GetPrimaryDataTable(), Function, Function.GetDefaultFunctionParameter());
			Result.Data.Table = table;
		}

		// Token: 0x040000F6 RID: 246
		private ReportDAO dao;
	}
}
