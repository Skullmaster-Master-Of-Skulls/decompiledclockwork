using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF3 RID: 2803
	[DataContract(Namespace = "http://tpro.ca")]
	public class FixAvailabilityAppointmentMappingsReq : BaseMessageReq
	{
		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x0001CD43 File Offset: 0x0001AF43
		// (set) Token: 0x06003B3E RID: 15166 RVA: 0x0001CD4B File Offset: 0x0001AF4B
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x0001CD54 File Offset: 0x0001AF54
		// (set) Token: 0x06003B40 RID: 15168 RVA: 0x0001CD5C File Offset: 0x0001AF5C
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
