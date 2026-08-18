using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions
{
	// Token: 0x02000006 RID: 6
	public interface IOldUserSettingClientManager : IWebService
	{
		// Token: 0x06000015 RID: 21
		OldUserSettingDTO GetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode);

		// Token: 0x06000016 RID: 22
		void SetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode, int IntVal, string StringVal);

		// Token: 0x06000017 RID: 23
		string GetSettingValue_String(eSettingCode SettingCode);
	}
}
