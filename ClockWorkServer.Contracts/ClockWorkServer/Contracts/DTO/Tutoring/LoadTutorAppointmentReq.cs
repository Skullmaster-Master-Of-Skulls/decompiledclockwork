using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A8 RID: 424
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTutorAppointmentReq : BaseMessageReq
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0000463C File Offset: 0x0000283C
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00004644 File Offset: 0x00002844
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
