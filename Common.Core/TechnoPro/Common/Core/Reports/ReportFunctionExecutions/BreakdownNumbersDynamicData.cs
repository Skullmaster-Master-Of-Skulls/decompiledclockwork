using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000062 RID: 98
	public class BreakdownNumbersDynamicData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00017908 File Offset: 0x00015B08
		public BreakdownNumbersDynamicData()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00017923 File Offset: 0x00015B23
		public BreakdownNumbersDynamicData(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00017941 File Offset: 0x00015B41
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00017949 File Offset: 0x00015B49
		public OperationContext OpContext { get; set; }

		// Token: 0x06000424 RID: 1060 RVA: 0x00017954 File Offset: 0x00015B54
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, TechnoPro.Common.Public.Entities.Reports.ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null;
			if (flag)
			{
				DataTable table = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.BreakdownData(primaryDataTable, function.GetDefaultFunctionParameter());
				result.Data.Table = table;
			}
		}

		// Token: 0x040000BD RID: 189
		private ReportDAO dao;
	}
}
