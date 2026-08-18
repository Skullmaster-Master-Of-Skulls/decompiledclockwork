using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Accommodation;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C1 RID: 193
	public class LegacyAccommodationReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyAccommodation>, ILegacyAccommodation, IService
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0001488A File Offset: 0x00012A8A
		public LegacyAccommodationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00014895 File Offset: 0x00012A95
		public LegacyAccommodationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000148A4 File Offset: 0x00012AA4
		public LogLoaIssuedDateResp LogLoaIssuedDate(LogLoaIssuedDateReq Request)
		{
			return this.WrapServiceMethod<LogLoaIssuedDateResp>(() => this.Proxy.LogLoaIssuedDate(Request));
		}
	}
}
