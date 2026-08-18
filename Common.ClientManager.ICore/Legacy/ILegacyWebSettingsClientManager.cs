using System;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Legacy
{
	// Token: 0x02000047 RID: 71
	public interface ILegacyWebSettingsClientManager : IWebService
	{
		// Token: 0x060001F7 RID: 503
		string GetWebSettingValue(int webSetting, string instanceName);
	}
}
