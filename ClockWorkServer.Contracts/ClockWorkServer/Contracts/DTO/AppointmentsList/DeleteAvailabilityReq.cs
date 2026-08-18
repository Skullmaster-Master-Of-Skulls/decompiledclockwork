using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ADD RID: 2781
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAvailabilityReq : BaseMessageReq
	{
		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x0001CA68 File Offset: 0x0001AC68
		// (set) Token: 0x06003AD2 RID: 15058 RVA: 0x0001CA70 File Offset: 0x0001AC70
		[DataMember]
		public List<int> AvailabilityIds { get; set; }
	}
}
