using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Login;
using TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MiscTableSettings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.MiscTableSettings
{
	// Token: 0x02000038 RID: 56
	public class MiscTableSettingsClientManagers : IMiscTableSettingsClientManagers, IWebService
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public string LoadMiscSettingValue(int code)
		{
			LoadMiscSettingValueReq loadMiscSettingValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMiscSettingValueReq>();
			loadMiscSettingValueReq.Code = code;
			return ClientServiceFactory.GetClientInstance<IMiscTableSettings>().LoadMiscSettingValue(loadMiscSettingValueReq).Value;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public void SaveMiscSettingValue(int code, string value)
		{
			SaveMiscSettingValueReq saveMiscSettingValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveMiscSettingValueReq>();
			saveMiscSettingValueReq.Code = code;
			saveMiscSettingValueReq.Value = value;
			ClientServiceFactory.GetClientInstance<IMiscTableSettings>().SaveMiscSettingValue(saveMiscSettingValueReq);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009B18 File Offset: 0x00007D18
		public LdapConnectionInfoDTO LoadLdapConnectionInfo()
		{
			LoadLdapConnectionInfoReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLdapConnectionInfoReq>();
			return ClientServiceFactory.GetClientInstance<IMiscTableSettings>().LoadLdapConnectionInfo(request).Info;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009B48 File Offset: 0x00007D48
		public void SaveLdapConnectionInfo(LdapConnectionInfoDTO info)
		{
			SaveLdapConnectionInfoReq saveLdapConnectionInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveLdapConnectionInfoReq>();
			saveLdapConnectionInfoReq.Info = info;
			ClientServiceFactory.GetClientInstance<IMiscTableSettings>().SaveLdapConnectionInfo(saveLdapConnectionInfoReq);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009B78 File Offset: 0x00007D78
		public eLoginMethodDTO GetLoginMethod()
		{
			GetLoginMethodReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetLoginMethodReq>();
			return ClientServiceFactory.GetClientInstance<IMiscTableSettings>().GetLoginMethod(request).Method;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009BA8 File Offset: 0x00007DA8
		public void SetLoginMethod(eLoginMethodDTO loginMethod)
		{
			SetLoginMethodReq setLoginMethodReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetLoginMethodReq>();
			setLoginMethodReq.Method = loginMethod;
			ClientServiceFactory.GetClientInstance<IMiscTableSettings>().SetLoginMethod(setLoginMethodReq);
		}
	}
}
