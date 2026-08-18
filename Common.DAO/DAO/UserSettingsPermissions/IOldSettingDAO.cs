using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.DAO.UserSettingsPermissions
{
	// Token: 0x02000018 RID: 24
	public interface IOldSettingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600003A RID: 58
		List<OldUserSetting> LoadAllUserSettings(int WhoAmI);

		// Token: 0x0600003B RID: 59
		Task<List<OldUserSetting>> LoadAllUserSettingsAsync(int WhoAmI);

		// Token: 0x0600003C RID: 60
		int CreateOrUpdatePersonSettingValue(OldUserSetting Setting);

		// Token: 0x0600003D RID: 61
		int CreateOrUpdateGroupSettingValue(OldUserSetting Setting);

		// Token: 0x0600003E RID: 62
		void DeletePersonSettingValue(OldUserSetting Setting);

		// Token: 0x0600003F RID: 63
		void DeleteGroupSettingValue(OldUserSetting Setting);

		// Token: 0x06000040 RID: 64
		IList<OldUserSetting> LoadPersonSettings(int PersonId);

		// Token: 0x06000041 RID: 65
		IList<OldUserSetting> LoadGroupSettings(int GroupId);

		// Token: 0x06000042 RID: 66
		IList<OldUserSetting> LoadEveryoneSettings();

		// Token: 0x06000043 RID: 67
		OldUserSetting GetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode);

		// Token: 0x06000044 RID: 68
		void SetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode, int IntVal, string StringVal);
	}
}
