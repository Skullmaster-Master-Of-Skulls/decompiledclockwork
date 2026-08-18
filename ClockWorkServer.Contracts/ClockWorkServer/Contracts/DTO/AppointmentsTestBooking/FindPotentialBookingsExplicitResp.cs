using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009CA RID: 2506
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindPotentialBookingsExplicitResp
	{
		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x00019430 File Offset: 0x00017630
		// (set) Token: 0x060033F6 RID: 13302 RVA: 0x00019438 File Offset: 0x00017638
		[DataMember]
		public FindPotentialBookingsRespDTO Result { get; set; }
	}
}
