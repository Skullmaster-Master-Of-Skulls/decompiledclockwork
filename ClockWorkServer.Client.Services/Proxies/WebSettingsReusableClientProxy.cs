using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000135 RID: 309
	public class WebSettingsReusableClientProxy : WCFTokenBasedReusableClientProxy<IWebSettings>, IWebSettings, IService
	{
		// Token: 0x06000C1D RID: 3101 RVA: 0x0001E68E File Offset: 0x0001C88E
		public WebSettingsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0001E699 File Offset: 0x0001C899
		public WebSettingsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		public void ClearSettingsCache(ClearSettingsCacheReq clearSettingsCacheReq)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearSettingsCache(clearSettingsCacheReq);
			});
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		public void ClearSettingsCache(ClearSettingsCacheByGroupReq group)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearSettingsCache(group);
			});
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0001E718 File Offset: 0x0001C918
		public GetInstanceNameResp GetInstanceNames(GetInstanceNameReq instanceNameReq)
		{
			return this.WrapServiceMethod<GetInstanceNameResp>(() => this.Proxy.GetInstanceNames(instanceNameReq));
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0001E750 File Offset: 0x0001C950
		public GetSettingResp GetSetting(GetSettingReq settingReq)
		{
			return this.WrapServiceMethod<GetSettingResp>(() => this.Proxy.GetSetting(settingReq));
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0001E788 File Offset: 0x0001C988
		public GetSettingFromStringResp GetSettingFromString(GetSettingFromStringReq settingReq)
		{
			return this.WrapServiceMethod<GetSettingFromStringResp>(() => this.Proxy.GetSettingFromString(settingReq));
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		public GetSettingsByGroupResp GetSettings(GetSettingsByGroupReq group)
		{
			return this.WrapServiceMethod<GetSettingsByGroupResp>(() => this.Proxy.GetSettings(group));
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0001E7F8 File Offset: 0x0001C9F8
		public void SaveSetting(SaveSettingReq setting)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveSetting(setting);
			});
		}
	}
}
