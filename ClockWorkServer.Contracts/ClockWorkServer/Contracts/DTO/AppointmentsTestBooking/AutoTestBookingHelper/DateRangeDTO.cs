using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A9E RID: 2718
	[DataContract(Namespace = "http://tpro.ca")]
	public class DateRangeDTO
	{
		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		// (set) Token: 0x0600394A RID: 14666 RVA: 0x0001BCE8 File Offset: 0x00019EE8
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x0600394B RID: 14667 RVA: 0x0001BCF1 File Offset: 0x00019EF1
		// (set) Token: 0x0600394C RID: 14668 RVA: 0x0001BCF9 File Offset: 0x00019EF9
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x0001BD02 File Offset: 0x00019F02
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x0001BD0A File Offset: 0x00019F0A
		[DataMember]
		public int Scope { get; set; }
	}
}
