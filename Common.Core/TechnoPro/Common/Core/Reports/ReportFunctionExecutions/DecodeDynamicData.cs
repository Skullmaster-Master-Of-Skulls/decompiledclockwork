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
	// Token: 0x02000073 RID: 115
	public class DecodeDynamicData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0001A908 File Offset: 0x00018B08
		public DecodeDynamicData()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001A923 File Offset: 0x00018B23
		public DecodeDynamicData(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001A946 File Offset: 0x00018B46
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0001A94E File Offset: 0x00018B4E
		public OperationContext OpContext { get; set; }

		// Token: 0x0600048B RID: 1163 RVA: 0x0001A958 File Offset: 0x00018B58
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null && primaryDataTable.Rows.Count > 0;
			if (flag)
			{
				DataTable dataTable = this.dao.DecodeDynamicData(primaryDataTable, Function, Function.GetDefaultFunctionParameter());
				bool flag2 = dataTable != null;
				if (flag2)
				{
					Result.Data.Table = dataTable;
				}
			}
		}

		// Token: 0x040000D8 RID: 216
		private ReportDAO dao;
	}
}
