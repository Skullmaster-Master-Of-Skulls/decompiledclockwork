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
	// Token: 0x02000074 RID: 116
	public class DecryptData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0001A9B1 File Offset: 0x00018BB1
		public DecryptData()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001A9CC File Offset: 0x00018BCC
		public DecryptData(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001A9EA File Offset: 0x00018BEA
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0001A9F2 File Offset: 0x00018BF2
		public OperationContext OpContext { get; set; }

		// Token: 0x06000490 RID: 1168 RVA: 0x0001A9FC File Offset: 0x00018BFC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null && primaryDataTable.Columns.Count > 0;
			if (flag)
			{
				string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
				string[] colsToDecrypt = defaultFunctionParameter.Split(new char[]
				{
					','
				});
				result.Data.Table = this.dao.DecryptData(primaryDataTable, colsToDecrypt);
			}
		}

		// Token: 0x040000DA RID: 218
		private ReportDAO dao;
	}
}
