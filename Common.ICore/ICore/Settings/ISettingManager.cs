using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ICore.Settings
{
	// Token: 0x0200003A RID: 58
	[Obsolete("Use IWebSettingManager instead")]
	public interface ISettingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000174 RID: 372
		// (set) Token: 0x06000175 RID: 373
		string InstanceName { get; set; }

		// Token: 0x06000176 RID: 374
		IList<AppSetting> GetSettings(Group group);

		// Token: 0x06000177 RID: 375
		AppSetting GetSetting(Setting setting);

		// Token: 0x06000178 RID: 376
		AppSetting GetSetting(LookupSetting lookupSetting);

		// Token: 0x06000179 RID: 377
		AppSetting GetSetting(int settingCode);

		// Token: 0x0600017A RID: 378
		T GetSettingValue<T>(Setting setting);

		// Token: 0x0600017B RID: 379
		T GetSettingValue<T>(int settingCode);

		// Token: 0x0600017C RID: 380
		void Save(AppSetting setting);

		// Token: 0x0600017D RID: 381
		void SetStringValue(AppSetting setting, string sValue);

		// Token: 0x0600017E RID: 382
		void RemoveSettings(Group group);

		// Token: 0x0600017F RID: 383
		void ClearCache();

		// Token: 0x06000180 RID: 384
		IList<string> GetInstanceNames();
	}
}
