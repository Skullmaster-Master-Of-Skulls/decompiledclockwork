using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000E3 RID: 227
	public interface IAppointmentRoomManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000708 RID: 1800
		IList<AppointmentRoom> LoadAllowedRooms();

		// Token: 0x06000709 RID: 1801
		IList<AppointmentRoom> LoadAllRooms();

		// Token: 0x0600070A RID: 1802
		AppointmentRoom LoadRoomById(int RoomId);

		// Token: 0x0600070B RID: 1803
		IList<AppointmentRoomWithAvailability> LoadRoomsWithAvailability(IList<int> RoomIds, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600070C RID: 1804
		IList<AppointmentRoom> LoadRoomsInGrousp(params int[] GroupIds);
	}
}
