using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF7 RID: 2807
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAvailability2MarkerResp
	{
		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x0001CD87 File Offset: 0x0001AF87
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x0001CD8F File Offset: 0x0001AF8F
		[DataMember]
		public int Availability2MarkerId { get; set; }
	}
}
