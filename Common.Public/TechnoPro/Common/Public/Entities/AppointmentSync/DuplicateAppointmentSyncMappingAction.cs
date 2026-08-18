using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004CF RID: 1231
	public class DuplicateAppointmentSyncMappingAction
	{
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x00028085 File Offset: 0x00026285
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x0002808D File Offset: 0x0002628D
		public eDuplicateAppointmentSyncMappingAction Action { get; set; }

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x00028096 File Offset: 0x00026296
		// (set) Token: 0x06002532 RID: 9522 RVA: 0x0002809E File Offset: 0x0002629E
		public int ClockWorkAppointmentId { get; set; }

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000280A7 File Offset: 0x000262A7
		// (set) Token: 0x06002534 RID: 9524 RVA: 0x000280AF File Offset: 0x000262AF
		public string UniqueId2 { get; set; }

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000280B8 File Offset: 0x000262B8
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x000280C0 File Offset: 0x000262C0
		public string UniqueId { get; set; }

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000280C9 File Offset: 0x000262C9
		// (set) Token: 0x06002538 RID: 9528 RVA: 0x000280D1 File Offset: 0x000262D1
		public string GlobalId { get; set; }

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x000280DA File Offset: 0x000262DA
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x000280E2 File Offset: 0x000262E2
		public bool Completed { get; set; }

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000280EB File Offset: 0x000262EB
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x000280F3 File Offset: 0x000262F3
		public string ErrorMessage { get; set; }
	}
}
