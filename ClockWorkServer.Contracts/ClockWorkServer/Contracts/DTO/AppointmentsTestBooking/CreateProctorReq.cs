using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009FA RID: 2554
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProctorReq : BaseMessageReq
	{
		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x00019D37 File Offset: 0x00017F37
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x00019D3F File Offset: 0x00017F3F
		[DataMember]
		public ProctorDTO Proctor { get; set; }
	}
}
