using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE1 RID: 2785
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityResp
	{
		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		// (set) Token: 0x06003AE6 RID: 15078 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		[DataMember]
		public IList<Availability2ItemDTO> Availability { get; set; }
	}
}
