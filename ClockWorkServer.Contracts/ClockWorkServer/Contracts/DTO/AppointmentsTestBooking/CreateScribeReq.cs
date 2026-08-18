using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009FE RID: 2558
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateScribeReq : BaseMessageReq
	{
		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x00019D7B File Offset: 0x00017F7B
		// (set) Token: 0x06003526 RID: 13606 RVA: 0x00019D83 File Offset: 0x00017F83
		[DataMember]
		public ProctorDTO Proctor { get; set; }
	}
}
