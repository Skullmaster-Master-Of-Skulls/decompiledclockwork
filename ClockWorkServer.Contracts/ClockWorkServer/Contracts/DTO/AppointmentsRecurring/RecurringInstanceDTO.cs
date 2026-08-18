using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000ABE RID: 2750
	[DataContract(Namespace = "http://tpro.ca")]
	public class RecurringInstanceDTO
	{
		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x0001C48C File Offset: 0x0001A68C
		// (set) Token: 0x06003A4E RID: 14926 RVA: 0x0001C494 File Offset: 0x0001A694
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x0001C49D File Offset: 0x0001A69D
		// (set) Token: 0x06003A50 RID: 14928 RVA: 0x0001C4A5 File Offset: 0x0001A6A5
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x0001C4AE File Offset: 0x0001A6AE
		// (set) Token: 0x06003A52 RID: 14930 RVA: 0x0001C4B6 File Offset: 0x0001A6B6
		[DataMember]
		public DateTime EndDateTime { get; set; }
	}
}
