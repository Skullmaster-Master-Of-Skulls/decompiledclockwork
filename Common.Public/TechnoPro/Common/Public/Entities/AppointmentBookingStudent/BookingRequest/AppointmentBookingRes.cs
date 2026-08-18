using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200056D RID: 1389
	public class AppointmentBookingRes
	{
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x00031C12 File Offset: 0x0002FE12
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x00031C1A File Offset: 0x0002FE1A
		public bool PassedChecks { get; set; }

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x00031C23 File Offset: 0x0002FE23
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x00031C2B File Offset: 0x0002FE2B
		public int AppointmentId { get; set; }

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x00031C34 File Offset: 0x0002FE34
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x00031C3C File Offset: 0x0002FE3C
		public string PublicMessage { get; set; }

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x00031C45 File Offset: 0x0002FE45
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x00031C4D File Offset: 0x0002FE4D
		public string PrivateMessage { get; set; }
	}
}
