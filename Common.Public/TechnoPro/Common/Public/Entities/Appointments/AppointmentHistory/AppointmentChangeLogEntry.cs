using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Appointments.AppointmentHistory
{
	// Token: 0x020004C8 RID: 1224
	public class AppointmentChangeLogEntry
	{
		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x00027F63 File Offset: 0x00026163
		// (set) Token: 0x0600250B RID: 9483 RVA: 0x00027F6B File Offset: 0x0002616B
		public DateTime LogEntryDate { get; set; }

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x00027F74 File Offset: 0x00026174
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x00027F7C File Offset: 0x0002617C
		public BasicPerson LogEntryOwner { get; set; }

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x00027F85 File Offset: 0x00026185
		// (set) Token: 0x0600250F RID: 9487 RVA: 0x00027F8D File Offset: 0x0002618D
		public eAppointmentChangeLogEntryType LogEntryType { get; set; }

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x00027F96 File Offset: 0x00026196
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x00027F9E File Offset: 0x0002619E
		public BaseBasicAppointment CurrentAppointmentInfo { get; set; }
	}
}
