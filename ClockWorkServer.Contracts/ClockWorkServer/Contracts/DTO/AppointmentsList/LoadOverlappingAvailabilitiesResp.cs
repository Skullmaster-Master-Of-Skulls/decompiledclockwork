using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD2 RID: 2770
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOverlappingAvailabilitiesResp
	{
		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x0001C947 File Offset: 0x0001AB47
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x0001C94F File Offset: 0x0001AB4F
		[DataMember]
		public List<Availability2ItemDTO> Items { get; set; }
	}
}
