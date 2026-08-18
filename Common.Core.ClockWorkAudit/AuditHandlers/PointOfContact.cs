using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x0200000B RID: 11
	public class PointOfContact : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002050 File Offset: 0x00000250
		public PointOfContact()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003C0D File Offset: 0x00001E0D
		public PointOfContact(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003C1F File Offset: 0x00001E1F
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00003C27 File Offset: 0x00001E27
		public OperationContext OpContext { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00003C30 File Offset: 0x00001E30
		public AuditResult ExecuteAudit()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> pocAppTypeGroupIds = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_PointOfContactAppointmentTypeGroupIds).Distinct<int>().ToList<int>();
			bool flag = pocAppTypeGroupIds.Any((int g) => g > 0);
			AuditResult auditResult = new AuditResult(eClockWorkAuditType.PointOfContact);
			AuditResult auditResult2 = auditResult;
			List<AuditCheck> list = new List<AuditCheck>();
			List<AuditCheck> list2 = list;
			string title = "Check point of contact appointment type group ids setting";
			eAuditStatus status = flag ? eAuditStatus.CompletedSuccessful : eAuditStatus.Failed;
			string[] array = new string[2];
			array[0] = "POC app type group ids: {0}";
			array[1] = string.Join(", ", (from g in pocAppTypeGroupIds
			select g.ToString()).ToArray<string>());
			list2.Add(new AuditCheck(title, status, array));
			auditResult2.Checks = list;
			AuditResult auditResult3 = auditResult;
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			List<AppType> source = appointmentTypeManager.LoadAllAppTypes();
			List<AppType> list3 = (from m in source
			where pocAppTypeGroupIds.Any((int g) => m.Group != null && g == m.Group.AppointmentTypeGroupId)
			select m).ToList<AppType>();
			bool flag2 = list3.Count < 1;
			if (flag2)
			{
				auditResult3.Checks.Add(new AuditCheck("Check point of contact appTypes exist", eAuditStatus.Failed, new string[]
				{
					"Can't find any app types in the POC app type groups"
				}));
			}
			else
			{
				ICollection<AuditCheck> checks = auditResult3.Checks;
				string title2 = "Check point of contact appTypes exist";
				eAuditStatus status2 = eAuditStatus.CompletedSuccessful;
				string[] array2 = new string[2];
				array2[0] = "App types found: {0}";
				array2[1] = string.Join(", ", (from g in list3
				select g.AppTypeId.ToString()).ToArray<string>());
				checks.Add(new AuditCheck(title2, status2, array2));
				foreach (AppType appType in list3)
				{
					List<int> list4 = (from g in appointmentTypeManager.GetAppointmentTypeAssociatedPerAppScreenNums(appType.AppTypeId)
					where g > 0
					select g).Distinct<int>().ToList<int>();
					ICollection<AuditCheck> checks2 = auditResult3.Checks;
					string title3 = string.Concat(new string[]
					{
						"Check POC apptype has associated perappt screen [",
						appType.AppTypeId.ToString(),
						": ",
						appType.Description ?? "",
						"]"
					});
					eAuditStatus status3 = (list4.Count < 1) ? eAuditStatus.Failed : eAuditStatus.CompletedSuccessful;
					string[] array3 = new string[2];
					array3[0] = "ScreenNums={0}";
					array3[1] = string.Join(", ", (from g in list4
					select g.ToString()).ToArray<string>());
					checks2.Add(new AuditCheck(title3, status3, array3));
				}
			}
			return auditResult3;
		}
	}
}
