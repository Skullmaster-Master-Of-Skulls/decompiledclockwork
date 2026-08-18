using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.WebSettings;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000CB RID: 203
	public class LegacyWebSettingsReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyWebSettings>, ILegacyWebSettings, IService
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00014DEE File Offset: 0x00012FEE
		public LegacyWebSettingsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00014DF9 File Offset: 0x00012FF9
		public LegacyWebSettingsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00014E08 File Offset: 0x00013008
		public GetWebSettingValueResp GetWebSettingValue(GetWebSettingValueReq Request)
		{
			return this.WrapServiceMethod<GetWebSettingValueResp>(() => this.Proxy.GetWebSettingValue(Request));
		}
	}
}
