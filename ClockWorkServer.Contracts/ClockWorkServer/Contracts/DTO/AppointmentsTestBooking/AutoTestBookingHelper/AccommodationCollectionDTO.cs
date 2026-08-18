using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A95 RID: 2709
	[DataContract(Namespace = "http://tpro.ca")]
	public class AccommodationCollectionDTO
	{
		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x0001BA95 File Offset: 0x00019C95
		// (set) Token: 0x060038FD RID: 14589 RVA: 0x0001BA9D File Offset: 0x00019C9D
		[DataMember]
		public IList<AccommodationDTO> Accommodations { get; set; }
	}
}
