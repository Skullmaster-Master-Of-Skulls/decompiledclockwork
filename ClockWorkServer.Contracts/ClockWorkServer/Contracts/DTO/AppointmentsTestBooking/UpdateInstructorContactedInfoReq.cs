using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A37 RID: 2615
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateInstructorContactedInfoReq : BaseMessageReq
	{
		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x0001A298 File Offset: 0x00018498
		// (set) Token: 0x060035F9 RID: 13817 RVA: 0x0001A2A0 File Offset: 0x000184A0
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x0001A2A9 File Offset: 0x000184A9
		// (set) Token: 0x060035FB RID: 13819 RVA: 0x0001A2B1 File Offset: 0x000184B1
		[DataMember]
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x0001A2BA File Offset: 0x000184BA
		// (set) Token: 0x060035FD RID: 13821 RVA: 0x0001A2C2 File Offset: 0x000184C2
		[DataMember]
		public string InstructorContactedNote { get; set; }
	}
}
