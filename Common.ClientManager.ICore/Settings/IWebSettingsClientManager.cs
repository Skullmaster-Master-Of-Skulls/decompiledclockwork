using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ClientManager.ICore.Settings
{
	// Token: 0x02000019 RID: 25
	public interface IWebSettingsClientManager : IWebService
	{
		// Token: 0x06000093 RID: 147
		IList<string> GetInstanceNames();

		// Token: 0x06000094 RID: 148
		IList<AppSetting> GetSettings(Group group);

		// Token: 0x06000095 RID: 149
		AppSetting GetSetting(Setting setting);

		// Token: 0x06000096 RID: 150
		AppSetting GetSetting(Setting setting, string sValue);

		// Token: 0x06000097 RID: 151
		void SaveSetting(AppSetting setting);

		// Token: 0x06000098 RID: 152
		void ClearSettingsCache(Group group);

		// Token: 0x06000099 RID: 153
		void ClearSettingsCache();

		// Token: 0x0600009A RID: 154
		T GetSettingValue<T>(Setting setting);
	}
}
