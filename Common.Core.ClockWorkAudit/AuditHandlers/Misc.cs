using System;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x0200000A RID: 10
	public class Misc : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002050 File Offset: 0x00000250
		public Misc()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003AF6 File Offset: 0x00001CF6
		public Misc(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003B08 File Offset: 0x00001D08
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00003B10 File Offset: 0x00001D10
		public OperationContext OpContext { get; set; }

		// Token: 0x06000035 RID: 53 RVA: 0x00003B1C File Offset: 0x00001D1C
		public AuditResult ExecuteAudit()
		{
			AuditResult auditResult = new AuditResult(eClockWorkAuditType.Misc);
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_ButtonHide_NotifyStaff);
			auditResult.Checks.Add(new AuditCheck("Check Hide Notify Staff", settingValue_Bool ? eAuditStatus.CompletedSuccessful : eAuditStatus.Failed, new string[]
			{
				"ButtonHide-Notify-staff setting should be set to true"
			}));
			bool settingValue_Bool2 = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_UseOldTestScreen);
			auditResult.Checks.Add(new AuditCheck("Check Use Old Test Screen", settingValue_Bool2 ? eAuditStatus.Failed : eAuditStatus.CompletedSuccessful, new string[]
			{
				"Use-old-test-screen should be set to false"
			}));
			bool settingValue_Bool3 = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_UseStudentMiddleNames);
			auditResult.Checks.Add(new AuditCheck("Check Use Student Middle Names", settingValue_Bool3 ? eAuditStatus.CompletedSuccessful : eAuditStatus.Failed, new string[]
			{
				"Use-Student-Middlenames setting should be set to true"
			}));
			return auditResult;
		}
	}
}
