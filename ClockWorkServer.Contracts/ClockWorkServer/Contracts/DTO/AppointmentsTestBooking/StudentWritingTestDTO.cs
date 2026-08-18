using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B4 RID: 2484
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentWritingTestDTO
	{
		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x000189B2 File Offset: 0x00016BB2
		// (set) Token: 0x060032A8 RID: 12968 RVA: 0x000189BA File Offset: 0x00016BBA
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000189C3 File Offset: 0x00016BC3
		// (set) Token: 0x060032AA RID: 12970 RVA: 0x000189CB File Offset: 0x00016BCB
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x000189D4 File Offset: 0x00016BD4
		// (set) Token: 0x060032AC RID: 12972 RVA: 0x000189DC File Offset: 0x00016BDC
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x000189E5 File Offset: 0x00016BE5
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x000189ED File Offset: 0x00016BED
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000189F6 File Offset: 0x00016BF6
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x000189FE File Offset: 0x00016BFE
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x00018A07 File Offset: 0x00016C07
		// (set) Token: 0x060032B2 RID: 12978 RVA: 0x00018A0F File Offset: 0x00016C0F
		[DataMember]
		public bool IsCancelled { get; set; }

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x00018A18 File Offset: 0x00016C18
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x00018A20 File Offset: 0x00016C20
		[DataMember]
		public bool IsTentative { get; set; }

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x00018A29 File Offset: 0x00016C29
		// (set) Token: 0x060032B6 RID: 12982 RVA: 0x00018A31 File Offset: 0x00016C31
		[DataMember]
		public bool? InstructorAcknowledgedValue { get; set; }

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x00018A3A File Offset: 0x00016C3A
		// (set) Token: 0x060032B8 RID: 12984 RVA: 0x00018A42 File Offset: 0x00016C42
		[DataMember]
		public DateTime? InstructorAcknowledgedDate { get; set; }

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x00018A4B File Offset: 0x00016C4B
		// (set) Token: 0x060032BA RID: 12986 RVA: 0x00018A53 File Offset: 0x00016C53
		[DataMember]
		public string Location { get; set; }

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x060032BB RID: 12987 RVA: 0x00018A5C File Offset: 0x00016C5C
		// (set) Token: 0x060032BC RID: 12988 RVA: 0x00018A64 File Offset: 0x00016C64
		[DataMember]
		public string SubTitle { get; set; }

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x00018A6D File Offset: 0x00016C6D
		// (set) Token: 0x060032BE RID: 12990 RVA: 0x00018A75 File Offset: 0x00016C75
		[DataMember]
		public AppTypeDTO AppointmentType { get; set; }
	}
}
