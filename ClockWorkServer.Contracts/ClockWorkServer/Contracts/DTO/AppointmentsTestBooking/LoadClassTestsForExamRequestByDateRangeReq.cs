using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A10 RID: 2576
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestsForExamRequestByDateRangeReq : BaseMessageReq
	{
		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x00019EF1 File Offset: 0x000180F1
		// (set) Token: 0x06003564 RID: 13668 RVA: 0x00019EF9 File Offset: 0x000180F9
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x00019F02 File Offset: 0x00018102
		// (set) Token: 0x06003566 RID: 13670 RVA: 0x00019F0A File Offset: 0x0001810A
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x00019F13 File Offset: 0x00018113
		// (set) Token: 0x06003568 RID: 13672 RVA: 0x00019F1B File Offset: 0x0001811B
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x00019F24 File Offset: 0x00018124
		// (set) Token: 0x0600356A RID: 13674 RVA: 0x00019F2C File Offset: 0x0001812C
		[DataMember]
		public eClassTestType TestType { get; set; }
	}
}
