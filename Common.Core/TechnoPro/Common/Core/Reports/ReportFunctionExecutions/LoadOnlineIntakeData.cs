using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000088 RID: 136
	public class LoadOnlineIntakeData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x0000672B File Offset: 0x0000492B
		public LoadOnlineIntakeData()
		{
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001CB88 File Offset: 0x0001AD88
		public LoadOnlineIntakeData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001CB9A File Offset: 0x0001AD9A
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0001CBA2 File Offset: 0x0001ADA2
		public OperationContext OpContext { get; set; }

		// Token: 0x060004FC RID: 1276 RVA: 0x0001CBAC File Offset: 0x0001ADAC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable dataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = dataTable != null && dataTable.Rows.Count > 0;
			if (flag)
			{
				string text = function.GetDefaultFunctionParameter().Trim().ToLower();
				int num;
				bool flag2 = text.Length < 1 || !int.TryParse(text, out num);
				if (flag2)
				{
					num = 0;
				}
				bool flag3 = num < 1;
				if (flag3)
				{
					num = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.INTAKE_FormNum);
				}
				bool flag4 = num > 0;
				if (flag4)
				{
					IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(this.OpContext);
					dataTable = dataSyncInfoManager.LoadOnlineIntakeFormDataAndMergeWithExternalData(dataTable, num, dataTable.Rows[0]["student_no"].ToString());
				}
			}
			result.Data.Table = dataTable;
		}
	}
}
