using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C9 RID: 2505
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindPotentialBookingsExplicitReq : BaseMessageReq
	{
		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x0001941F File Offset: 0x0001761F
		// (set) Token: 0x060033F3 RID: 13299 RVA: 0x00019427 File Offset: 0x00017627
		[DataMember]
		public FindPotentialBookingsReqDTO Request { get; set; }
	}
}
