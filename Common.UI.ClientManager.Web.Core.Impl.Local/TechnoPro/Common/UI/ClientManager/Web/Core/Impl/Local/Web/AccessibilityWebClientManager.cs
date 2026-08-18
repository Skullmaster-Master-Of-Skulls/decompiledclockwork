using System;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.Core.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Accessible;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x0200000C RID: 12
	public class AccessibilityWebClientManager : IAccessibilityWebClientManager
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002C78 File Offset: 0x00000E78
		public void SetStudentAccessibleViewSetting(int studentPersonId, eClockWorkWebAccessibleView accessibleView)
		{
			try
			{
				IOldUserSettingClientManager oldUserSettingClientManager = new OldUserSettingClientManager();
				oldUserSettingClientManager.SetUserPersonalSettingValue(studentPersonId, eSettingCode.SETTING_StudentOption_UseAccessibleViewsOnWeb, (int)accessibleView, "");
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.UI.ClientManager.Web.Core.Impl.Local.Web.AccessibilityWebClientManager:ChangeCurrentAccessibleViewSetting:pid={0}:err={1}", studentPersonId.ToString(), ex.ToString());
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public eClockWorkWebAccessibleView GetStudentAccessibleViewSetting(int studentPersonId)
		{
			IOldUserSettingClientManager oldUserSettingClientManager = new OldUserSettingClientManager();
			OldUserSettingDTO userPersonalSettingValue = oldUserSettingClientManager.GetUserPersonalSettingValue(studentPersonId, eSettingCode.SETTING_StudentOption_UseAccessibleViewsOnWeb);
			int num = (userPersonalSettingValue != null) ? userPersonalSettingValue.IntVal : 1;
			bool flag = !Enum.IsDefined(typeof(eClockWorkWebAccessibleView), num);
			eClockWorkWebAccessibleView result;
			if (flag)
			{
				result = eClockWorkWebAccessibleView.GraphicalView;
			}
			else
			{
				result = (eClockWorkWebAccessibleView)num;
			}
			return result;
		}
	}
}
