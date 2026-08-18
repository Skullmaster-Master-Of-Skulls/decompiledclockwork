using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE5 RID: 2789
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsWithAvailabilityResp
	{
		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x0001CB89 File Offset: 0x0001AD89
		// (set) Token: 0x06003AFC RID: 15100 RVA: 0x0001CB91 File Offset: 0x0001AD91
		[DataMember]
		public IList<ListAppointmentOrAvailabilityDTO> Appointments { get; set; }
	}
}
