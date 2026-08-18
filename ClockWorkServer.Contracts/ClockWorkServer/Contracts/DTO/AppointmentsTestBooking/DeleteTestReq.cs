using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A1F RID: 2591
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteTestReq : BaseMessageReq
	{
		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x0001A034 File Offset: 0x00018234
		// (set) Token: 0x06003599 RID: 13721 RVA: 0x0001A03C File Offset: 0x0001823C
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
