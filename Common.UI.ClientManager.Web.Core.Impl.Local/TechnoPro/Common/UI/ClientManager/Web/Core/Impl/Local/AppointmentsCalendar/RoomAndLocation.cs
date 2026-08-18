using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar
{
	// Token: 0x02000025 RID: 37
	public class RoomAndLocation
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000988E File Offset: 0x00007A8E
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00009896 File Offset: 0x00007A96
		public string Location { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000989F File Offset: 0x00007A9F
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000098A7 File Offset: 0x00007AA7
		public AppointmentRoomDTO Room { get; set; }
	}
}
