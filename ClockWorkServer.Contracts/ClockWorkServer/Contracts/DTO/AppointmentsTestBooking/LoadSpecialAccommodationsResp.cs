using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009C2 RID: 2498
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSpecialAccommodationsResp
	{
		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x00019331 File Offset: 0x00017531
		// (set) Token: 0x060033D0 RID: 13264 RVA: 0x00019339 File Offset: 0x00017539
		[DataMember]
		public IList<SpecialAccommodationDTO> SpecialAccommodations { get; set; }
	}
}
