using System;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x0200000C RID: 12
	public class SelfRegButton : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002050 File Offset: 0x00000250
		public SelfRegButton()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003F30 File Offset: 0x00002130
		public SelfRegButton(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003F42 File Offset: 0x00002142
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00003F4A File Offset: 0x0000214A
		public OperationContext OpContext { get; set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00003F54 File Offset: 0x00002154
		public AuditResult ExecuteAudit()
		{
			AuditResult auditResult = new AuditResult(eClockWorkAuditType.SelfRegButton);
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_UseAccommodationRequests);
			bool settingValue = SettingManager.CurrentInstance.GetSettingValue<bool>(Setting.MODULES_ENABLED_SelfReg);
			bool flag = settingValue_Bool && settingValue;
			if (flag)
			{
				auditResult.Checks.Add(new AuditCheck("Check self reg button", eAuditStatus.CompletedSuccessful, new string[]
				{
					"Self reg is enabled on the web and self reg button is enabled in ClockWork"
				}));
			}
			else
			{
				bool flag2 = !settingValue_Bool && !settingValue;
				if (flag2)
				{
					auditResult.Checks.Add(new AuditCheck("Check self reg button", eAuditStatus.CompletedSuccessful, new string[]
					{
						"Self reg is DISABLED on the web and self reg button is DISABLED in ClockWork"
					}));
				}
				else
				{
					auditResult.Checks.Add(new AuditCheck("Check self reg button", eAuditStatus.Failed, new string[]
					{
						string.Format("Self reg is {0} on the web and self reg button is {1} in ClockWork", settingValue_Bool ? "ENABLED" : "DISABLED", settingValue ? "ENABLED" : "DISABLED")
					}));
				}
			}
			return auditResult;
		}
	}
}
