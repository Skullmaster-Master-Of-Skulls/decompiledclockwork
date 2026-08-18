using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD4 RID: 2772
	[DataContract(Namespace = "http://tpro.ca")]
	public class FreeTimeSearchResp
	{
		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x0001C98B File Offset: 0x0001AB8B
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x0001C993 File Offset: 0x0001AB93
		[DataMember]
		public List<Availability2ItemDTO> Items { get; set; }
	}
}
