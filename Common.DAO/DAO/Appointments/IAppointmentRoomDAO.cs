using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000A9 RID: 169
	public interface IAppointmentRoomDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600045A RID: 1114
		IList<AppointmentRoom> LoadAllRooms();

		// Token: 0x0600045B RID: 1115
		AppointmentRoom LoadRoomById(int RoomId);

		// Token: 0x0600045C RID: 1116
		IList<AppointmentRoomWithAvailability> LoadRoomsWithAvailability(IList<int> RoomIds, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600045D RID: 1117
		IList<AppointmentRoom> LoadRoomsInGrousp(params int[] GroupIds);
	}
}
