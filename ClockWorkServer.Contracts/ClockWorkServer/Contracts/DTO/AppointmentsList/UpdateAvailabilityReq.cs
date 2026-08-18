using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ADE RID: 2782
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAvailabilityReq : BaseMessageReq
	{
		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x0001CA79 File Offset: 0x0001AC79
		// (set) Token: 0x06003AD5 RID: 15061 RVA: 0x0001CA81 File Offset: 0x0001AC81
		[DataMember]
		public List<Availability2ItemDTO> Availabilities { get; set; }
	}
}
