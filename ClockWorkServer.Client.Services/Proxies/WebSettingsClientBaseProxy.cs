using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000136 RID: 310
	internal class WebSettingsClientBaseProxy : ClientBase<IWebSettings>, IWebSettings, IService
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x0001E82D File Offset: 0x0001CA2D
		public WebSettingsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0001E838 File Offset: 0x0001CA38
		public WebSettingsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0001E844 File Offset: 0x0001CA44
		public GetInstanceNameResp GetInstanceNames(GetInstanceNameReq instanceNameReq)
		{
			return base.Channel.GetInstanceNames(instanceNameReq);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0001E864 File Offset: 0x0001CA64
		public GetSettingsByGroupResp GetSettings(GetSettingsByGroupReq group)
		{
			return base.Channel.GetSettings(group);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001E884 File Offset: 0x0001CA84
		public GetSettingResp GetSetting(GetSettingReq settingReq)
		{
			return base.Channel.GetSetting(settingReq);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		public GetSettingFromStringResp GetSettingFromString(GetSettingFromStringReq settingReq)
		{
			return base.Channel.GetSettingFromString(settingReq);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0001E8C2 File Offset: 0x0001CAC2
		public void SaveSetting(SaveSettingReq setting)
		{
			base.Channel.SaveSetting(setting);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0001E8D2 File Offset: 0x0001CAD2
		public void ClearSettingsCache(ClearSettingsCacheByGroupReq group)
		{
			base.Channel.ClearSettingsCache(group);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0001E8E2 File Offset: 0x0001CAE2
		public void ClearSettingsCache(ClearSettingsCacheReq clearSettingsCacheReq)
		{
			base.Channel.ClearSettingsCache(clearSettingsCacheReq);
		}
	}
}
