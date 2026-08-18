using System;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005F2 RID: 1522
	public static class SettingsAdapter
	{
		// Token: 0x060030DD RID: 12509 RVA: 0x00043178 File Offset: 0x00041378
		public static string GetCacheKey(this Group settingGroup, string instanceName)
		{
			return string.Format("{0}.{1}", instanceName, settingGroup.ToString());
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000431A4 File Offset: 0x000413A4
		public static string GetCacheKey(this Setting setting, string instanceName)
		{
			return string.Format("{0}.{1}", instanceName, setting.ToString());
		}
	}
}
