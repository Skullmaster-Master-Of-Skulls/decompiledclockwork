using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.ICore.UserSettingsPermissions
{
	// Token: 0x02000015 RID: 21
	public interface IOldUserSettingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600007B RID: 123
		List<int> GetSettingValue_ConcatenatedIntList(int WhoAmI, eSettingCode settingCode);

		// Token: 0x0600007C RID: 124
		Task<List<int>> GetSettingValue_ConcatenatedIntListAsync(int WhoAmI, eSettingCode settingCode);

		// Token: 0x0600007D RID: 125
		int GetSettingValue_Int(int WhoAmI, eSettingCode settingCode);

		// Token: 0x0600007E RID: 126
		string GetSettingValue_String(int WhoAmI, eSettingCode settingCode, bool concatenateValues = false);

		// Token: 0x0600007F RID: 127
		bool GetSettingValue_Bool(int WhoAmI, eSettingCode settingCode);

		// Token: 0x06000080 RID: 128
		bool GetSettingValue_Bool(int WhoAmI, eSettingCode settingCode, bool defaultValue);

		// Token: 0x06000081 RID: 129
		List<OldUserSetting> LoadAllUserSettings(int WhoAmI);

		// Token: 0x06000082 RID: 130
		Task<List<OldUserSetting>> LoadAllUserSettingsAsync(int WhoAmI);

		// Token: 0x06000083 RID: 131
		void UpdateUserSettings(int WhoAmI, int PersonId, List<OldUserSetting> Settings);

		// Token: 0x06000084 RID: 132
		void UpdateGroupSettings(int WhoAmI, int GroupId, List<OldUserSetting> Settings);

		// Token: 0x06000085 RID: 133
		void SaveSettings(IList<OldUserSetting> Settings);

		// Token: 0x06000086 RID: 134
		IList<OldUserSetting> LoadPersonSettings(int PersonId);

		// Token: 0x06000087 RID: 135
		IList<OldUserSetting> LoadGroupSettings(int GroupId);

		// Token: 0x06000088 RID: 136
		IList<OldUserSetting> LoadEveryoneSettings();

		// Token: 0x06000089 RID: 137
		void ClearCacheForUser(int PersonId);

		// Token: 0x0600008A RID: 138
		OldUserSetting GetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode);

		// Token: 0x0600008B RID: 139
		void SetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode, int IntVal, string StringVal);

		// Token: 0x0600008C RID: 140
		OldUserSettingReportForUserSet LoadUserSettingReportForUserSet(int PersonId);

		// Token: 0x0600008D RID: 141
		bool UserHasAnySettings(int WhoAmI, eSettingCode settingCode);
	}
}
