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
	// Token: 0x02000075 RID: 117
	public class EncryptData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0001AA5E File Offset: 0x00018C5E
		public EncryptData()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001AA79 File Offset: 0x00018C79
		public EncryptData(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001AA97 File Offset: 0x00018C97
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0001AA9F File Offset: 0x00018C9F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000495 RID: 1173 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null && primaryDataTable.Columns.Count > 0;
			if (flag)
			{
				string text = Function.GetDefaultFunctionParameter();
				int num = text.LastIndexOf('`');
				bool flag2 = num >= 0;
				if (flag2)
				{
					text = text.Substring(num + 1);
				}
				string[] colsToEncrypt = text.Split(new char[]
				{
					','
				});
				result.Data.Table = this.dao.EncryptData(primaryDataTable, colsToEncrypt);
			}
		}

		// Token: 0x040000DC RID: 220
		private ReportDAO dao;
	}
}
