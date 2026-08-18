using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Accommodation;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C2 RID: 194
	internal class LegacyAccommodationClientBaseProxy : ClientBase<ILegacyAccommodation>, ILegacyAccommodation, IService
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x000148DC File Offset: 0x00012ADC
		public LegacyAccommodationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000148E7 File Offset: 0x00012AE7
		public LegacyAccommodationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000148F4 File Offset: 0x00012AF4
		public LogLoaIssuedDateResp LogLoaIssuedDate(LogLoaIssuedDateReq Request)
		{
			return base.Channel.LogLoaIssuedDate(Request);
		}
	}
}
