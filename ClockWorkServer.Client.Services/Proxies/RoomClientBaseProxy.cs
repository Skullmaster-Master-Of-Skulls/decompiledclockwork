using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000120 RID: 288
	internal class RoomClientBaseProxy : ClientBase<IRoom>, IRoom, IService
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x0001D29C File Offset: 0x0001B49C
		public RoomClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0001D2A7 File Offset: 0x0001B4A7
		public RoomClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
		public LoadAllSeatsResp LoadAllSeats(LoadAllSeatsReq Request)
		{
			return base.Channel.LoadAllSeats(Request);
		}
	}
}
