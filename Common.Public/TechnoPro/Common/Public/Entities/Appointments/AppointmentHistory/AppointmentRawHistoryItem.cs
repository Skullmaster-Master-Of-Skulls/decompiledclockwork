using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Appointments.AppointmentHistory
{
	// Token: 0x020004C9 RID: 1225
	public class AppointmentRawHistoryItem
	{
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x00027FA7 File Offset: 0x000261A7
		// (set) Token: 0x06002514 RID: 9492 RVA: 0x00027FAF File Offset: 0x000261AF
		public DateTime AuditDateTime { get; set; }

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x00027FB8 File Offset: 0x000261B8
		// (set) Token: 0x06002516 RID: 9494 RVA: 0x00027FC0 File Offset: 0x000261C0
		public BasicPerson AuditOwner { get; set; }

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x00027FC9 File Offset: 0x000261C9
		// (set) Token: 0x06002518 RID: 9496 RVA: 0x00027FD1 File Offset: 0x000261D1
		public BaseBasicAppointment AppointmentBeforeChange { get; set; }

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x00027FDA File Offset: 0x000261DA
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x00027FE2 File Offset: 0x000261E2
		public bool IsDeleted { get; set; }
	}
}
