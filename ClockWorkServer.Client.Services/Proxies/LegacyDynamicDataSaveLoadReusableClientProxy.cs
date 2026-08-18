using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000094 RID: 148
	public class LegacyDynamicDataSaveLoadReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyDynamicDataSaveLoad>, ILegacyDynamicDataSaveLoad, IService
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x00010FAA File Offset: 0x0000F1AA
		public LegacyDynamicDataSaveLoadReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00010FB5 File Offset: 0x0000F1B5
		public LegacyDynamicDataSaveLoadReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public SaveDataPSResp SaveDataPS(SaveDataPSReq Request)
		{
			return this.WrapServiceMethod<SaveDataPSResp>(() => this.Proxy.SaveDataPS(Request));
		}
	}
}
