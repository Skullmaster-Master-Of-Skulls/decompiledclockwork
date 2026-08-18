using System;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkController
{
	// Token: 0x0200000B RID: 11
	public class Misc
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000047CC File Offset: 0x000029CC
		public static string GetContactInformationHtml(Setting primarySource)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(primarySource);
			bool flag = string.IsNullOrEmpty(settingValue);
			if (flag)
			{
				settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DepartmentContactInformation);
			}
			return string.IsNullOrEmpty(settingValue) ? "" : string.Format(" ({0})", settingValue);
		}
	}
}
