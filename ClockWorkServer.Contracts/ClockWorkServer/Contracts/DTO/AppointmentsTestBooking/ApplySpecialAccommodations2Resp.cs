using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009BD RID: 2493
	[DataContract(Namespace = "http://tpro.ca")]
	public class ApplySpecialAccommodations2Resp
	{
		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x00019287 File Offset: 0x00017487
		// (set) Token: 0x060033B7 RID: 13239 RVA: 0x0001928F File Offset: 0x0001748F
		[DataMember]
		public ApplySpecialAccommodationsRespDTO Result { get; set; }
	}
}
