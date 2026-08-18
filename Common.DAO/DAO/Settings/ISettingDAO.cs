using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.DAO.Settings
{
	// Token: 0x0200002F RID: 47
	public interface ISettingDAO : IBaseOperationContext<SettingsOperationContext>
	{
		// Token: 0x060000C3 RID: 195
		IList<AppSetting> GetSettings(Group group);

		// Token: 0x060000C4 RID: 196
		AppSetting GetSetting(Setting setting);

		// Token: 0x060000C5 RID: 197
		AppSetting GetSetting(Setting setting, string sValue);

		// Token: 0x060000C6 RID: 198
		void Save(AppSetting setting);

		// Token: 0x060000C7 RID: 199
		void SetStringValue(AppSetting setting, string sValue);

		// Token: 0x060000C8 RID: 200
		IList<string> GetInstanceNames();
	}
}
