using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000095 RID: 149
	internal class LegacyDynamicDataSaveLoadClientBaseProxy : ClientBase<ILegacyDynamicDataSaveLoad>, ILegacyDynamicDataSaveLoad, IService
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public LegacyDynamicDataSaveLoadClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00011007 File Offset: 0x0000F207
		public LegacyDynamicDataSaveLoadClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00011014 File Offset: 0x0000F214
		public SaveDataPSResp SaveDataPS(SaveDataPSReq Request)
		{
			return base.Channel.SaveDataPS(Request);
		}
	}
}
