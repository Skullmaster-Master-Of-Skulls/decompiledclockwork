using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ADC RID: 2780
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAvailabilitiesReq : BaseMessageReq
	{
		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x0001CA57 File Offset: 0x0001AC57
		// (set) Token: 0x06003ACF RID: 15055 RVA: 0x0001CA5F File Offset: 0x0001AC5F
		[DataMember]
		public List<Availability2ItemDTO> Availabilities { get; set; }
	}
}
