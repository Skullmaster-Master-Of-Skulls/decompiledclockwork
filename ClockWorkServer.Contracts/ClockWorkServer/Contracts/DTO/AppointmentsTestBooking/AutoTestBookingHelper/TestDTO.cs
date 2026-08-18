using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA7 RID: 2727
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestDTO
	{
		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x0001C107 File Offset: 0x0001A307
		// (set) Token: 0x060039CD RID: 14797 RVA: 0x0001C10F File Offset: 0x0001A30F
		[DataMember]
		public string Location { get; set; }

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x0001C118 File Offset: 0x0001A318
		// (set) Token: 0x060039CF RID: 14799 RVA: 0x0001C120 File Offset: 0x0001A320
		[DataMember]
		public int Lucid { get; set; }

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x0001C129 File Offset: 0x0001A329
		// (set) Token: 0x060039D1 RID: 14801 RVA: 0x0001C131 File Offset: 0x0001A331
		[DataMember]
		public string CourseDescription { get; set; }

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x0001C13A File Offset: 0x0001A33A
		// (set) Token: 0x060039D3 RID: 14803 RVA: 0x0001C142 File Offset: 0x0001A342
		[DataMember]
		public int BreakTime { get; set; }

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x0001C14B File Offset: 0x0001A34B
		// (set) Token: 0x060039D5 RID: 14805 RVA: 0x0001C153 File Offset: 0x0001A353
		[DataMember]
		public RoomDTO Room { get; set; }

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x0001C15C File Offset: 0x0001A35C
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x0001C164 File Offset: 0x0001A364
		[DataMember]
		public int Duration { get; set; }

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x0001C16D File Offset: 0x0001A36D
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x0001C175 File Offset: 0x0001A375
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x0001C17E File Offset: 0x0001A37E
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x0001C186 File Offset: 0x0001A386
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
