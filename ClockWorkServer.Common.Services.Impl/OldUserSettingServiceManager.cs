using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Core.Mappers.UserSettingsPermissions;
using TechnoPro.Common.Core.Mappers.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200009E RID: 158
	public class OldUserSettingServiceManager : IOldUserSetting, IService
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		public void UpdateUserSettings(UpdateUserSettingsReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			oldUserSettingManager.UpdateUserSettings(Request.WhoAmI, Request.PersonId, Request.Settings.ConvertAll<OldUserSetting>((OldUserSettingDTO f) => f.ToDomainObject()));
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001AD30 File Offset: 0x00018F30
		public void UpdateGroupSettings(UpdateGroupSettingsReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			oldUserSettingManager.UpdateGroupSettings(Request.WhoAmI, Request.GroupId, Request.Settings.ConvertAll<OldUserSetting>((OldUserSettingDTO f) => f.ToDomainObject()));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001AD88 File Offset: 0x00018F88
		public LoadAllUserSettingsResp LoadAllUserSettings(LoadAllUserSettingsReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			List<OldUserSetting> list = oldUserSettingManager.LoadAllUserSettings(Request.WhoAmI);
			LoadAllUserSettingsResp loadAllUserSettingsResp = new LoadAllUserSettingsResp();
			IList<OldUserSettingDTO> settings;
			if (list != null)
			{
				settings = list.ConvertAll<OldUserSettingDTO>((OldUserSetting f) => f.ToDTO());
			}
			else
			{
				settings = null;
			}
			loadAllUserSettingsResp.Settings = settings;
			return loadAllUserSettingsResp;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001ADEC File Offset: 0x00018FEC
		public void SaveSettings(SaveSettingsReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			oldUserSettingManager.SaveSettings(Request.Settings.ToList<OldUserSettingDTO>().ConvertAll<OldUserSetting>((OldUserSettingDTO f) => f.ToDomainObject()));
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001AE3C File Offset: 0x0001903C
		public LoadPersonSettingsResp LoadPersonSettings(LoadPersonSettingsReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			IList<OldUserSetting> list = oldUserSettingManager.LoadPersonSettings((Request.PersonId < 1) ? Request.WhoAmI : Request.PersonId);
			LoadPersonSettingsResp loadPersonSettingsResp = new LoadPersonSettingsResp();
			IList<OldUserSettingDTO> settings;
			if (list != null)
			{
				settings = list.ToList<OldUserSetting>().ConvertAll<OldUserSettingDTO>((OldUserSetting f) => f.ToDTO());
			}
			else
			{
				settings = null;
			}
			loadPersonSettingsResp.Settings = settings;
			return loadPersonSettingsResp;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001AEB4 File Offset: 0x000190B4
		public LoadGroupSettingsResp LoadGroupSettings(LoadGroupSettingsReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			IList<OldUserSetting> list = oldUserSettingManager.LoadGroupSettings(Request.GroupId);
			LoadGroupSettingsResp loadGroupSettingsResp = new LoadGroupSettingsResp();
			IList<OldUserSettingDTO> settings;
			if (list != null)
			{
				settings = list.ToList<OldUserSetting>().ConvertAll<OldUserSettingDTO>((OldUserSetting f) => f.ToDTO());
			}
			else
			{
				settings = null;
			}
			loadGroupSettingsResp.Settings = settings;
			return loadGroupSettingsResp;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001AF1C File Offset: 0x0001911C
		public LoadEveryoneSettingsResp LoadEveryoneSettings(LoadEveryoneSettingsReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			IList<OldUserSetting> list = oldUserSettingManager.LoadEveryoneSettings();
			LoadEveryoneSettingsResp loadEveryoneSettingsResp = new LoadEveryoneSettingsResp();
			IList<OldUserSettingDTO> settings;
			if (list != null)
			{
				settings = list.ToList<OldUserSetting>().ConvertAll<OldUserSettingDTO>((OldUserSetting f) => f.ToDTO());
			}
			else
			{
				settings = null;
			}
			loadEveryoneSettingsResp.Settings = settings;
			return loadEveryoneSettingsResp;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001AF80 File Offset: 0x00019180
		public void ClearCacheForUser(ClearCacheForUserReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			oldUserSettingManager.ClearCacheForUser(Request.PersonId);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public GetUserPersonalSettingValueResp GetUserPersonalSettingValue(GetUserPersonalSettingValueReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			OldUserSetting userPersonalSettingValue = oldUserSettingManager.GetUserPersonalSettingValue(Request.PersonId, Request.SettingCode);
			return new GetUserPersonalSettingValueResp
			{
				SettingValue = ((userPersonalSettingValue != null) ? userPersonalSettingValue.ToDTO() : null)
			};
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001AFF4 File Offset: 0x000191F4
		public void SetUserPersonalSettingValue(SetUserPersonalSettingValueReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			oldUserSettingManager.SetUserPersonalSettingValue(Request.PersonId, Request.SettingCode, Request.IntVal, Request.StringVal);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001B030 File Offset: 0x00019230
		public LoadUserSettingReportForUserSetResp LoadUserSettingReportForUserSet(LoadUserSettingReportForUserSetReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			OldUserSettingReportForUserSet oldUserSettingReportForUserSet = oldUserSettingManager.LoadUserSettingReportForUserSet(Request.PersonId);
			return new LoadUserSettingReportForUserSetResp
			{
				ReportSet = ((oldUserSettingReportForUserSet != null) ? oldUserSettingReportForUserSet.ToDTO() : null)
			};
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001B074 File Offset: 0x00019274
		public GetSettingValueStringResp GetSettingValueString(GetSettingValueStringReq Request)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(Request.GetOperationContext());
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(Request.WhoAmI, Request.SettingCode, false);
			return new GetSettingValueStringResp
			{
				SettingValue = settingValue_String
			};
		}
	}
}
