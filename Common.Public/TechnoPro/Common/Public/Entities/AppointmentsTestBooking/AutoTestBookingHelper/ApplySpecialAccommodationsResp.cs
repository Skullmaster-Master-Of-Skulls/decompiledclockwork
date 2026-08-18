using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000531 RID: 1329
	public class ApplySpecialAccommodationsResp
	{
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x0002B48E File Offset: 0x0002968E
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x0002B496 File Offset: 0x00029696
		public IList<PrivateNote> PrivateNotes { get; set; }

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x0002B49F File Offset: 0x0002969F
		// (set) Token: 0x06002A2B RID: 10795 RVA: 0x0002B4A7 File Offset: 0x000296A7
		public StringBuilder EmailBodySb { get; set; }

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x0002B4B0 File Offset: 0x000296B0
		// (set) Token: 0x06002A2D RID: 10797 RVA: 0x0002B4B8 File Offset: 0x000296B8
		public IList<int> IconsToBookWith { get; set; }

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x0002B4C1 File Offset: 0x000296C1
		// (set) Token: 0x06002A2F RID: 10799 RVA: 0x0002B4C9 File Offset: 0x000296C9
		public Test NewTestScheduledTimeAndRoom { get; set; }
	}
}
