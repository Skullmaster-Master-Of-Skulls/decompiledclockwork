using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Login;
using TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MiscTableSettings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.MiscTableSettings
{
	// Token: 0x0200002D RID: 45
	public class MiscTableSettingsRestClientManagers : BearerTokenRestProxy<IMiscTableSettingsClientManagers>, IMiscTableSettingsClientManagers, IWebService
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00005E69 File Offset: 0x00004069
		public MiscTableSettingsRestClientManagers(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005E73 File Offset: 0x00004073
		public MiscTableSettingsRestClientManagers(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005E7E File Offset: 0x0000407E
		public string LoadMiscSettingValue(int code)
		{
			return base.Get<string>(string.Format("misctablesettings/miscsettingvalue/code/{0}", code), true);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005E98 File Offset: 0x00004098
		public void SaveMiscSettingValue(int code, string value)
		{
			SaveMiscSettingValueReq saveMiscSettingValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveMiscSettingValueReq>();
			saveMiscSettingValueReq.Code = code;
			saveMiscSettingValueReq.Value = value;
			base.Post<SaveMiscSettingValueReq>(saveMiscSettingValueReq, "misctablesettings/savemiscsettingvalue");
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005ECA File Offset: 0x000040CA
		public LdapConnectionInfoDTO LoadLdapConnectionInfo()
		{
			return base.Get<LdapConnectionInfoDTO>("misctablesettings/ldapconnectioninfo", true);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005ED8 File Offset: 0x000040D8
		public void SaveLdapConnectionInfo(LdapConnectionInfoDTO info)
		{
			base.Post<LdapConnectionInfoDTO>(info, "misctablesettings/ldapconnectioninfo");
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005EE6 File Offset: 0x000040E6
		public eLoginMethodDTO GetLoginMethod()
		{
			return base.Get<eLoginMethodDTO>("misctablesettings/loginmethod", true);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005EF4 File Offset: 0x000040F4
		public void SetLoginMethod(eLoginMethodDTO loginMethod)
		{
			base.Post<eLoginMethodDTO>(loginMethod, "misctablesettings/loginmethod");
		}
	}
}
