using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200095B RID: 2395
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeeByIdReq : BaseMessageReq
	{
		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x00017DE6 File Offset: 0x00015FE6
		// (set) Token: 0x060030F1 RID: 12529 RVA: 0x00017DEE File Offset: 0x00015FEE
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x00017DF7 File Offset: 0x00015FF7
		// (set) Token: 0x060030F3 RID: 12531 RVA: 0x00017DFF File Offset: 0x00015FFF
		[DataMember]
		public int PersonId { get; set; }
	}
}
