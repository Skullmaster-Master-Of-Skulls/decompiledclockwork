using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A93 RID: 2707
	[DataContract(Namespace = "http://tpro.ca")]
	public class AccommodationBasicDTO
	{
		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x0001BA1E File Offset: 0x00019C1E
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x0001BA26 File Offset: 0x00019C26
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x0001BA2F File Offset: 0x00019C2F
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x0001BA37 File Offset: 0x00019C37
		[DataMember]
		public string ControlCaptionAndValue { get; set; }
	}
}
