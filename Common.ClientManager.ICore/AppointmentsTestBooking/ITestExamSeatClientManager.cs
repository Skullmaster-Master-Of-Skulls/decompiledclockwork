using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x0200008D RID: 141
	public interface ITestExamSeatClientManager : IWebService
	{
		// Token: 0x06000438 RID: 1080
		IList<AppointmentRoomDTO> LoadAllowedSeats(eTestExamSeatType TestType);

		// Token: 0x06000439 RID: 1081
		AppointmentRoomDTO LoadSeatById(int RoomId);

		// Token: 0x0600043A RID: 1082
		IList<AppointmentRoomWithAvailabilityDTO> LoadRoomsWithAvailability(eTestExamSeatType TestType, DateTime StartDateTime, DateTime EndDateTime, IList<int> RoomIdsToIgnore);
	}
}
