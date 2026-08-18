using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A9 RID: 425
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTutorAppointmentResp
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0000464D File Offset: 0x0000284D
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00004655 File Offset: 0x00002855
		[DataMember]
		public TutorAppointmentDTO TutorAppointment { get; set; }
	}
}
