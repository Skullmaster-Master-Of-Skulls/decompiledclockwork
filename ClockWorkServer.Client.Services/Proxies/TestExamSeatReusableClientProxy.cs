using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200003A RID: 58
	public class TestExamSeatReusableClientProxy : WCFTokenBasedReusableClientProxy<ITestExamSeat>, ITestExamSeat, IService
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000964A File Offset: 0x0000784A
		public TestExamSeatReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00009655 File Offset: 0x00007855
		public TestExamSeatReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00009664 File Offset: 0x00007864
		public LoadAllowedSeatsResp LoadAllowedSeats(LoadAllowedSeatsReq Request)
		{
			return this.WrapServiceMethod<LoadAllowedSeatsResp>(() => this.Proxy.LoadAllowedSeats(Request));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000969C File Offset: 0x0000789C
		public LoadRoomsWithAvailabilityResp LoadRoomsWithAvailability(LoadRoomsWithAvailabilityReq Request)
		{
			return this.WrapServiceMethod<LoadRoomsWithAvailabilityResp>(() => this.Proxy.LoadRoomsWithAvailability(Request));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000096D4 File Offset: 0x000078D4
		public LoadSeatByIdResp LoadSeatById(LoadSeatByIdReq Request)
		{
			return this.WrapServiceMethod<LoadSeatByIdResp>(() => this.Proxy.LoadSeatById(Request));
		}
	}
}
