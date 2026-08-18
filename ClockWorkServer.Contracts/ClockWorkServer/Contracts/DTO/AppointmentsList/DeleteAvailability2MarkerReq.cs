using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF9 RID: 2809
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAvailability2MarkerReq : BaseMessageReq
	{
		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x0001CDA9 File Offset: 0x0001AFA9
		// (set) Token: 0x06003B50 RID: 15184 RVA: 0x0001CDB1 File Offset: 0x0001AFB1
		[DataMember]
		public int Availability2MarkerId { get; set; }
	}
}
