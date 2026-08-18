using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ICore.Settings
{
	// Token: 0x0200003B RID: 59
	public interface IWebSettingManager : IBaseOperationContext<SettingsOperationContext>
	{
		// Token: 0x06000181 RID: 385
		IList<AppSetting> GetSettings(Group group);

		// Token: 0x06000182 RID: 386
		AppSetting GetSetting(Setting setting);

		// Token: 0x06000183 RID: 387
		T GetSettingValue<T>(Setting setting);

		// Token: 0x06000184 RID: 388
		T GetSettingValue<T>(int settingCode);

		// Token: 0x06000185 RID: 389
		void Save(AppSetting setting);

		// Token: 0x06000186 RID: 390
		void RemoveSettings(Group group);

		// Token: 0x06000187 RID: 391
		void ClearCache();

		// Token: 0x06000188 RID: 392
		IList<string> GetInstanceNames();

		// Token: 0x06000189 RID: 393
		AppSetting GetSetting(Setting setting, string sValue);
	}
}
