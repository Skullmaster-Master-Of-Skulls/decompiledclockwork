using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D0 RID: 2512
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToFindBookingResp
	{
		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x00019595 File Offset: 0x00017795
		// (set) Token: 0x06003426 RID: 13350 RVA: 0x0001959D File Offset: 0x0001779D
		[DataMember]
		public TryToBookResultDTO FindBookingResult { get; set; }
	}
}
