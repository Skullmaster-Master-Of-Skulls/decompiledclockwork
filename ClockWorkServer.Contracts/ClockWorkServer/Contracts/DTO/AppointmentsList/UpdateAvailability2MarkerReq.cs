using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF8 RID: 2808
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAvailability2MarkerReq : BaseMessageReq
	{
		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x0001CD98 File Offset: 0x0001AF98
		// (set) Token: 0x06003B4D RID: 15181 RVA: 0x0001CDA0 File Offset: 0x0001AFA0
		[DataMember]
		public Availability2MarkerDTO Marker { get; set; }
	}
}
