using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C8 RID: 2504
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBaseAutoTestBookingSettingsResp
	{
		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x0001940E File Offset: 0x0001760E
		// (set) Token: 0x060033F0 RID: 13296 RVA: 0x00019416 File Offset: 0x00017616
		[DataMember]
		public FindPotentialBookingsReqDTO FindPotentialBookingsRequest { get; set; }
	}
}
