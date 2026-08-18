using System;
using System.Runtime.CompilerServices;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x02000004 RID: 4
	public class ControlIdsInSettings : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002050 File Offset: 0x00000250
		public ControlIdsInSettings()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002534 File Offset: 0x00000734
		public ControlIdsInSettings(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002546 File Offset: 0x00000746
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000254E File Offset: 0x0000074E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002558 File Offset: 0x00000758
		public AuditResult ExecuteAudit()
		{
			AuditResult auditResult = new AuditResult(eClockWorkAuditType.ControlIdsInSettings);
			eSettingCode[] array = new eSettingCode[10];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.B717AEDDD02399546B32C81E7549F2942F8E69FA874C9524D95B6AB6EC86AAC4).FieldHandle);
			eSettingCode[] array2 = array;
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			foreach (eSettingCode settingCode in array2)
			{
				int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, settingCode);
				DynamicField dynamicField = (settingValue_Int > 0) ? dynamicFieldManager.LoadFieldByControlId(settingValue_Int) : null;
				auditResult.Checks.Add((dynamicField == null) ? new AuditCheck("Check " + settingCode.ToString(), eAuditStatus.Failed, new string[]
				{
					"Setting not set or field does not exist:CidInSetting={0}",
					settingValue_Int.ToString()
				}) : new AuditCheck("Check " + settingCode.ToString(), eAuditStatus.CompletedSuccessful, new string[]
				{
					"FieldCaption={0}",
					dynamicField.GetCaptionForDisplay()
				}));
			}
			Setting[] array4 = new Setting[]
			{
				Setting.TESTBOOKING_AccommodationsExpiryDateCid
			};
			ISettingManager currentInstance = SettingManager.CurrentInstance;
			foreach (Setting setting in array4)
			{
				int settingValue = currentInstance.GetSettingValue<int>(setting);
				DynamicField dynamicField2 = (settingValue > 0) ? dynamicFieldManager.LoadFieldByControlId(settingValue) : null;
				auditResult.Checks.Add((dynamicField2 == null) ? new AuditCheck("Check web setting " + setting.ToString(), eAuditStatus.Failed, new string[]
				{
					"Web setting not set or field does not exist:CidInSetting={0}",
					settingValue.ToString()
				}) : new AuditCheck("Check web setting " + setting.ToString(), eAuditStatus.CompletedSuccessful, new string[]
				{
					"FieldCaption={0}",
					dynamicField2.GetCaptionForDisplay()
				}));
			}
			return auditResult;
		}
	}
}
