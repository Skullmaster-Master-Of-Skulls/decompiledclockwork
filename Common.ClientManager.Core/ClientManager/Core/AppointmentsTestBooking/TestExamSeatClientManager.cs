using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000093 RID: 147
	public class TestExamSeatClientManager : ITestExamSeatClientManager, IWebService
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x000179C4 File Offset: 0x00015BC4
		public IList<AppointmentRoomDTO> LoadAllowedSeats(eTestExamSeatType TestType)
		{
			LoadAllowedSeatsReq loadAllowedSeatsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllowedSeatsReq>();
			loadAllowedSeatsReq.ClassTestType = TestType;
			return ClientServiceFactory.GetClientInstance<ITestExamSeat>().LoadAllowedSeats(loadAllowedSeatsReq).Seats;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000179FC File Offset: 0x00015BFC
		public AppointmentRoomDTO LoadSeatById(int RoomId)
		{
			LoadSeatByIdReq loadSeatByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadSeatByIdReq>();
			loadSeatByIdReq.RoomId = RoomId;
			return ClientServiceFactory.GetClientInstance<ITestExamSeat>().LoadSeatById(loadSeatByIdReq).Seat;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00017A34 File Offset: 0x00015C34
		public IList<AppointmentRoomWithAvailabilityDTO> LoadRoomsWithAvailability(eTestExamSeatType TestType, DateTime StartDateTime, DateTime EndDateTime, IList<int> RoomIdsToIgnore)
		{
			LoadRoomsWithAvailabilityReq loadRoomsWithAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRoomsWithAvailabilityReq>();
			loadRoomsWithAvailabilityReq.TestType = TestType;
			loadRoomsWithAvailabilityReq.StartDateTime = StartDateTime;
			loadRoomsWithAvailabilityReq.EndDateTime = EndDateTime;
			loadRoomsWithAvailabilityReq.RoomIdsToIgnore = RoomIdsToIgnore;
			return ClientServiceFactory.GetClientInstance<ITestExamSeat>().LoadRoomsWithAvailability(loadRoomsWithAvailabilityReq).RoomsWithAvailability;
		}
	}
}
