using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B3 RID: 2483
	[DataContract(Namespace = "http://tpro.ca")]
	public class SittingBaseDTO
	{
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x00018908 File Offset: 0x00016B08
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x00018910 File Offset: 0x00016B10
		[DataMember]
		public int SittingId { get; set; }

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x00018919 File Offset: 0x00016B19
		// (set) Token: 0x06003295 RID: 12949 RVA: 0x00018921 File Offset: 0x00016B21
		[DataMember]
		public AppointmentRoomDTO Room { get; set; }

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x0001892A File Offset: 0x00016B2A
		// (set) Token: 0x06003297 RID: 12951 RVA: 0x00018932 File Offset: 0x00016B32
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x0001893B File Offset: 0x00016B3B
		// (set) Token: 0x06003299 RID: 12953 RVA: 0x00018943 File Offset: 0x00016B43
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x0001894C File Offset: 0x00016B4C
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x00018954 File Offset: 0x00016B54
		[DataMember]
		public DateTime ExamDate { get; set; }

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x0001895D File Offset: 0x00016B5D
		// (set) Token: 0x0600329D RID: 12957 RVA: 0x00018965 File Offset: 0x00016B65
		[DataMember]
		public PersonBaseDTO Invigilator { get; set; }

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x0001896E File Offset: 0x00016B6E
		// (set) Token: 0x0600329F RID: 12959 RVA: 0x00018976 File Offset: 0x00016B76
		[DataMember]
		public bool Cancelled { get; set; }

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x0001897F File Offset: 0x00016B7F
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x00018987 File Offset: 0x00016B87
		[DataMember]
		public DateTime? ScheduledStartDateTime { get; set; }

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x00018990 File Offset: 0x00016B90
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x00018998 File Offset: 0x00016B98
		[DataMember]
		public DateTime? ScheduledEndDateTime { get; set; }

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000189A1 File Offset: 0x00016BA1
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x000189A9 File Offset: 0x00016BA9
		[DataMember]
		public bool IsPrivate { get; set; }
	}
}
