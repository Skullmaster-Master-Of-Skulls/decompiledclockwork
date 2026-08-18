using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000CC RID: 204
	public interface ITestExamSeatManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000630 RID: 1584
		IList<AppointmentRoom> LoadAllowedSeats(eTestExamSeatType ClassTestType);

		// Token: 0x06000631 RID: 1585
		AppointmentRoom LoadSeatById(int RoomId);

		// Token: 0x06000632 RID: 1586
		IList<AppointmentRoomWithAvailability> LoadRoomsWithAvailability(eTestExamSeatType TestType, DateTime StartDateTime, DateTime EndDateTime, IList<int> RoomIdsToIgnore);
	}
}
