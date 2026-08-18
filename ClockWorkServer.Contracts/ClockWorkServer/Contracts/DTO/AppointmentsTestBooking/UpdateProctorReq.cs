using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A03 RID: 2563
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProctorReq : BaseMessageReq
	{
		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x00019DD0 File Offset: 0x00017FD0
		// (set) Token: 0x06003535 RID: 13621 RVA: 0x00019DD8 File Offset: 0x00017FD8
		[DataMember]
		public ProctorDTO Proctor { get; set; }
	}
}
