using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200003B RID: 59
	internal class TestExamSeatClientBaseProxy : ClientBase<ITestExamSeat>, ITestExamSeat, IService
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000970C File Offset: 0x0000790C
		public TestExamSeatClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009717 File Offset: 0x00007917
		public TestExamSeatClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009724 File Offset: 0x00007924
		public LoadAllowedSeatsResp LoadAllowedSeats(LoadAllowedSeatsReq Request)
		{
			return base.Channel.LoadAllowedSeats(Request);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009744 File Offset: 0x00007944
		public LoadRoomsWithAvailabilityResp LoadRoomsWithAvailability(LoadRoomsWithAvailabilityReq Request)
		{
			return base.Channel.LoadRoomsWithAvailability(Request);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009764 File Offset: 0x00007964
		public LoadSeatByIdResp LoadSeatById(LoadSeatByIdReq Request)
		{
			return base.Channel.LoadSeatById(Request);
		}
	}
}
