using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar
{
	// Token: 0x02000560 RID: 1376
	public class AppointmentExtendedInfo
	{
		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x000314E7 File Offset: 0x0002F6E7
		// (set) Token: 0x06002C3D RID: 11325 RVA: 0x000314EF File Offset: 0x0002F6EF
		public Appointment Appointment { get; set; }

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000314F8 File Offset: 0x0002F6F8
		// (set) Token: 0x06002C3F RID: 11327 RVA: 0x00031500 File Offset: 0x0002F700
		public int OrganizerPersonId { get; set; }
	}
}
