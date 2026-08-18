using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004AF RID: 1199
	public class AppointmentRoomWithAvailability : AppointmentRoom
	{
		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x000276A4 File Offset: 0x000258A4
		// (set) Token: 0x0600243A RID: 9274 RVA: 0x000276AC File Offset: 0x000258AC
		public bool IsAvailable { get; set; }

		// Token: 0x0600243B RID: 9275 RVA: 0x000171D4 File Offset: 0x000153D4
		public AppointmentRoomWithAvailability()
		{
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000276B8 File Offset: 0x000258B8
		public AppointmentRoomWithAvailability(AppointmentRoom room)
		{
			bool flag = room == null;
			if (!flag)
			{
				this.RoomId = room.RoomId;
				base.RoomDescription = room.RoomDescription;
				base.RoomTitle = room.RoomTitle;
				base.RoomUniqueId = room.RoomUniqueId;
			}
		}
	}
}
