using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ICore.Settings
{
	// Token: 0x02000037 RID: 55
	public interface ILookupSettingManager : IBaseOperationContext<SettingsOperationContext>
	{
		// Token: 0x06000164 RID: 356
		IList<LookupSetting> GetAllLookupSettings();

		// Token: 0x06000165 RID: 357
		IList<LookupSetting> GetAllLookupSettings(Group group);

		// Token: 0x06000166 RID: 358
		LookupSetting GetLookupSetting(Setting setting);

		// Token: 0x06000167 RID: 359
		LookupSetting GetLookupSetting(int settingCode);

		// Token: 0x06000168 RID: 360
		IList<LookupSetting> GetLookupSetting(string settingName);
	}
}
