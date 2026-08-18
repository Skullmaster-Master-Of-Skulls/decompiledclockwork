using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004D7 RID: 1239
	public class ClockWorkExternalAppMapping
	{
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000281A6 File Offset: 0x000263A6
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x000281AE File Offset: 0x000263AE
		public int ClockWorkAppointmentId { get; set; }

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000281B7 File Offset: 0x000263B7
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x000281BF File Offset: 0x000263BF
		public string ExternalApplicationUniqueAppointmentId { get; set; }

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000281C8 File Offset: 0x000263C8
		// (set) Token: 0x0600255A RID: 9562 RVA: 0x000281D0 File Offset: 0x000263D0
		public string ExternalApplicationUniqueAppointmentId2 { get; set; }

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000281D9 File Offset: 0x000263D9
		// (set) Token: 0x0600255C RID: 9564 RVA: 0x000281E1 File Offset: 0x000263E1
		public string ExternalApplicationGlobalAppointmentId { get; set; }

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000281EA File Offset: 0x000263EA
		// (set) Token: 0x0600255E RID: 9566 RVA: 0x000281F2 File Offset: 0x000263F2
		public string ExternalApplicationMasterRecurrenceAppointmentId { get; set; }

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x000281FB File Offset: 0x000263FB
		// (set) Token: 0x06002560 RID: 9568 RVA: 0x00028203 File Offset: 0x00026403
		public DateTime? ClockWorkLastUpdatedDate { get; set; }

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x0002820C File Offset: 0x0002640C
		// (set) Token: 0x06002562 RID: 9570 RVA: 0x00028214 File Offset: 0x00026414
		public DateTime? ExternalApplicationLastUpdatedDate { get; set; }
	}
}
