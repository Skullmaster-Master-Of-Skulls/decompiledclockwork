using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsRecurring
{
	// Token: 0x0200054E RID: 1358
	public class RecurringInstance
	{
		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x00030ECC File Offset: 0x0002F0CC
		// (set) Token: 0x06002BD8 RID: 11224 RVA: 0x00030ED4 File Offset: 0x0002F0D4
		public int AppointmentId { get; set; }

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x00030EDD File Offset: 0x0002F0DD
		// (set) Token: 0x06002BDA RID: 11226 RVA: 0x00030EE5 File Offset: 0x0002F0E5
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x00030EEE File Offset: 0x0002F0EE
		// (set) Token: 0x06002BDC RID: 11228 RVA: 0x00030EF6 File Offset: 0x0002F0F6
		public DateTime EndDateTime { get; set; }
	}
}
