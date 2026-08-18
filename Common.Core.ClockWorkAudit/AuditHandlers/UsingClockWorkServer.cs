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
	// Token: 0x0200000E RID: 14
	public class UsingClockWorkServer : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002050 File Offset: 0x00000250
		public UsingClockWorkServer()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000421F File Offset: 0x0000241F
		public UsingClockWorkServer(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004231 File Offset: 0x00002431
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00004239 File Offset: 0x00002439
		public OperationContext OpContext { get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x00004244 File Offset: 0x00002444
		public AuditResult ExecuteAudit()
		{
			AuditResult auditResult = new AuditResult(eClockWorkAuditType.UsingClockWorkServer);
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_UseClockWorkServer);
			auditResult.Checks.Add(new AuditCheck("Check 'Use ClockWork Server' is enabled", settingValue_Bool ? eAuditStatus.CompletedSuccessful : eAuditStatus.Failed, Array.Empty<string>()));
			return auditResult;
		}
	}
}
