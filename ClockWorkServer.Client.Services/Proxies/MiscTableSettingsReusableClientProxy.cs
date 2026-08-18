using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F5 RID: 245
	public class MiscTableSettingsReusableClientProxy : WCFTokenBasedReusableClientProxy<IMiscTableSettings>, IMiscTableSettings, IService
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00018301 File Offset: 0x00016501
		public MiscTableSettingsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001830C File Offset: 0x0001650C
		public MiscTableSettingsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00018318 File Offset: 0x00016518
		public GetLoginMethodResp GetLoginMethod(GetLoginMethodReq Request)
		{
			return this.WrapServiceMethod<GetLoginMethodResp>(() => this.Proxy.GetLoginMethod(Request));
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00018350 File Offset: 0x00016550
		public LoadLdapConnectionInfoResp LoadLdapConnectionInfo(LoadLdapConnectionInfoReq Request)
		{
			return this.WrapServiceMethod<LoadLdapConnectionInfoResp>(() => this.Proxy.LoadLdapConnectionInfo(Request));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00018388 File Offset: 0x00016588
		public LoadMiscSettingValueResp LoadMiscSettingValue(LoadMiscSettingValueReq Request)
		{
			return this.WrapServiceMethod<LoadMiscSettingValueResp>(() => this.Proxy.LoadMiscSettingValue(Request));
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000183C0 File Offset: 0x000165C0
		public void SaveLdapConnectionInfo(SaveLdapConnectionInfoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveLdapConnectionInfo(Request);
			});
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000183F8 File Offset: 0x000165F8
		public void SaveMiscSettingValue(SaveMiscSettingValueReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveMiscSettingValue(Request);
			});
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00018430 File Offset: 0x00016630
		public void SetLoginMethod(SetLoginMethodReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetLoginMethod(Request);
			});
		}
	}
}
