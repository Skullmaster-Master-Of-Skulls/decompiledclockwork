using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF5 RID: 2805
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailability2MarkersResp
	{
		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x0001CD65 File Offset: 0x0001AF65
		// (set) Token: 0x06003B44 RID: 15172 RVA: 0x0001CD6D File Offset: 0x0001AF6D
		[DataMember]
		public IList<Availability2MarkerDTO> Markers { get; set; }
	}
}
