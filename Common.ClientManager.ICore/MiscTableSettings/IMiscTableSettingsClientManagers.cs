using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Login;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.MiscTableSettings
{
	// Token: 0x02000034 RID: 52
	public interface IMiscTableSettingsClientManagers : IWebService
	{
		// Token: 0x06000171 RID: 369
		string LoadMiscSettingValue(int code);

		// Token: 0x06000172 RID: 370
		void SaveMiscSettingValue(int code, string value);

		// Token: 0x06000173 RID: 371
		LdapConnectionInfoDTO LoadLdapConnectionInfo();

		// Token: 0x06000174 RID: 372
		void SaveLdapConnectionInfo(LdapConnectionInfoDTO info);

		// Token: 0x06000175 RID: 373
		eLoginMethodDTO GetLoginMethod();

		// Token: 0x06000176 RID: 374
		void SetLoginMethod(eLoginMethodDTO loginMethod);
	}
}
