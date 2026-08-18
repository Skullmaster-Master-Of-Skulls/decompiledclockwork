using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200011F RID: 287
	public class RoomReusableClientProxy : WCFTokenBasedReusableClientProxy<IRoom>, IRoom, IService
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x0001D24A File Offset: 0x0001B44A
		public RoomReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0001D255 File Offset: 0x0001B455
		public RoomReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0001D264 File Offset: 0x0001B464
		public LoadAllSeatsResp LoadAllSeats(LoadAllSeatsReq Request)
		{
			return this.WrapServiceMethod<LoadAllSeatsResp>(() => this.Proxy.LoadAllSeats(Request));
		}
	}
}
