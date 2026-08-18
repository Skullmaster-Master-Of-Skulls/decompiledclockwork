using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Login;

namespace TechnoPro.Common.ICore.Settings
{
	// Token: 0x02000038 RID: 56
	public interface IMiscTableSettingsManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000169 RID: 361
		string LoadMiscSettingValue(int code);

		// Token: 0x0600016A RID: 362
		void SaveMiscSettingValue(int code, string value);

		// Token: 0x0600016B RID: 363
		LdapConnectionInfo LoadLdapConnectionInfo();

		// Token: 0x0600016C RID: 364
		void SaveLdapConnectionInfo(LdapConnectionInfo info);

		// Token: 0x0600016D RID: 365
		eLoginMethod GetLoginMethod();

		// Token: 0x0600016E RID: 366
		void SetLoginMethod(eLoginMethod loginMethod);
	}
}
