using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009BB RID: 2491
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindPotentialBookings2Resp
	{
		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000191CC File Offset: 0x000173CC
		// (set) Token: 0x0600339F RID: 13215 RVA: 0x000191D4 File Offset: 0x000173D4
		[DataMember]
		public FindPotentialBookingsRespDTO Result { get; set; }
	}
}
