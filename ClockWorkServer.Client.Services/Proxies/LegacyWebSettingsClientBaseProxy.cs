using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.WebSettings;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000CC RID: 204
	internal class LegacyWebSettingsClientBaseProxy : ClientBase<ILegacyWebSettings>, ILegacyWebSettings, IService
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00014E40 File Offset: 0x00013040
		public LegacyWebSettingsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00014E4B File Offset: 0x0001304B
		public LegacyWebSettingsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00014E58 File Offset: 0x00013058
		public GetWebSettingValueResp GetWebSettingValue(GetWebSettingValueReq Request)
		{
			return base.Channel.GetWebSettingValue(Request);
		}
	}
}
