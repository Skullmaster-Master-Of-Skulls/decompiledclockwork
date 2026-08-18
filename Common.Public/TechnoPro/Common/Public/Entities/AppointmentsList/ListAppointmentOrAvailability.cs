using System;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;

namespace TechnoPro.Common.Public.Entities.AppointmentsList
{
	// Token: 0x02000556 RID: 1366
	public class ListAppointmentOrAvailability
	{
		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000310A5 File Offset: 0x0002F2A5
		// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x000310AD File Offset: 0x0002F2AD
		public ListAppointment Appointment { get; set; }

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x000310B6 File Offset: 0x0002F2B6
		// (set) Token: 0x06002BFA RID: 11258 RVA: 0x000310BE File Offset: 0x0002F2BE
		public Availability2Item Availability { get; set; }

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000310C7 File Offset: 0x0002F2C7
		// (set) Token: 0x06002BFC RID: 11260 RVA: 0x000310CF File Offset: 0x0002F2CF
		public int BackgroundColorArgB { get; set; }
	}
}
