using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar.StudentAppointmentBooking
{
	// Token: 0x02000561 RID: 1377
	public class AvailabilityForChannelCalendar
	{
		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x00031509 File Offset: 0x0002F709
		// (set) Token: 0x06002C42 RID: 11330 RVA: 0x00031511 File Offset: 0x0002F711
		public IList<int> PersonIds { get; set; }

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x0003151A File Offset: 0x0002F71A
		// (set) Token: 0x06002C44 RID: 11332 RVA: 0x00031522 File Offset: 0x0002F722
		public int AvailabilityGroupId { get; set; }

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x0003152B File Offset: 0x0002F72B
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x00031533 File Offset: 0x0002F733
		public string AvailabilityTitle { get; set; }

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x0003153C File Offset: 0x0002F73C
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x00031544 File Offset: 0x0002F744
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x0003154D File Offset: 0x0002F74D
		// (set) Token: 0x06002C4A RID: 11338 RVA: 0x00031555 File Offset: 0x0002F755
		public DateTime EndDateTime { get; set; }
	}
}
