using System;
using System.Data;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Intake;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000084 RID: 132
	public class ImportUserData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0000672B File Offset: 0x0000492B
		public ImportUserData()
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001BA79 File Offset: 0x00019C79
		public ImportUserData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001BA8B File Offset: 0x00019C8B
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0001BA93 File Offset: 0x00019C93
		public OperationContext OpContext { get; set; }

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001BA9C File Offset: 0x00019C9C
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, TechnoPro.Common.Public.Entities.Reports.ReportFunction function)
		{
			DataTable primaryDataView = CurrentWholeReportResult.GetPrimaryDataView();
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			bool flag = primaryDataView == null || !primaryDataView.Columns.Contains("student_no");
			if (flag)
			{
				CWLogger.Logger.Warn("Common.Core.Reports.ReportFunctionExecutions.ImportUserData:table is null or does not contain 'student_no' column");
				result.Data.Table = primaryDataView;
			}
			else
			{
				OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Intake_MultiDepartmentIntakeSettings);
				MultiDepartmentIntakeSettings multiDepartmentIntakeSettings = (settingValue_String ?? "").DeserializeMultiDepartmentIntakeSettings();
				bool flag2 = multiDepartmentIntakeSettings != null && multiDepartmentIntakeSettings.IsEnabled;
				bool flag3 = !flag2;
				if (flag3)
				{
					bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_Intake_DisableAutoIntakeDataSync);
					bool flag4 = !settingValue_Bool;
					if (flag4)
					{
						string studentNumber = (primaryDataView.Rows.Count > 0) ? primaryDataView.Rows[0]["student_no"].ToString().Trim().ToUpper() : "";
						IDataSyncDataManager dataSyncDataManager = new DataSyncDataManager(this.OpContext);
						dataSyncDataManager.DataSyncIntakeData(studentNumber, true);
					}
				}
				DataView dataView = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.ImportStudents(primaryDataView.DefaultView, defaultFunctionParameter, true, this.OpContext);
				result.Data.Table = ((dataView != null) ? dataView.Table : null);
			}
		}
	}
}
