using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsWorkshops
{
	// Token: 0x020004AA RID: 1194
	[Serializable]
	public class AppointmentWorkshopInfo
	{
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000273E3 File Offset: 0x000255E3
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x000273EB File Offset: 0x000255EB
		public virtual int WorkshopId { get; set; }

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000273F4 File Offset: 0x000255F4
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x000273FC File Offset: 0x000255FC
		public virtual string WorkshopTitle { get; set; }

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x00027405 File Offset: 0x00025605
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x0002740D File Offset: 0x0002560D
		public int MaxAttendeeCount { get; set; }
	}
}
