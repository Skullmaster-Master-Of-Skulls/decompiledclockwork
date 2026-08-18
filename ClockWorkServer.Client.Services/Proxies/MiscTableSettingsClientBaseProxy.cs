using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F6 RID: 246
	internal class MiscTableSettingsClientBaseProxy : ClientBase<IMiscTableSettings>, IMiscTableSettings, IService
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x00018465 File Offset: 0x00016665
		public MiscTableSettingsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00018470 File Offset: 0x00016670
		public MiscTableSettingsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001847C File Offset: 0x0001667C
		public GetLoginMethodResp GetLoginMethod(GetLoginMethodReq Request)
		{
			return base.Channel.GetLoginMethod(Request);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001849C File Offset: 0x0001669C
		public LoadLdapConnectionInfoResp LoadLdapConnectionInfo(LoadLdapConnectionInfoReq Request)
		{
			return base.Channel.LoadLdapConnectionInfo(Request);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000184BC File Offset: 0x000166BC
		public LoadMiscSettingValueResp LoadMiscSettingValue(LoadMiscSettingValueReq Request)
		{
			return base.Channel.LoadMiscSettingValue(Request);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000184DA File Offset: 0x000166DA
		public void SaveLdapConnectionInfo(SaveLdapConnectionInfoReq Request)
		{
			base.Channel.SaveLdapConnectionInfo(Request);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000184EA File Offset: 0x000166EA
		public void SaveMiscSettingValue(SaveMiscSettingValueReq Request)
		{
			base.Channel.SaveMiscSettingValue(Request);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000184FA File Offset: 0x000166FA
		public void SetLoginMethod(SetLoginMethodReq Request)
		{
			base.Channel.SetLoginMethod(Request);
		}
	}
}
