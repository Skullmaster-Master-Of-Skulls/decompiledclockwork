using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AF6 RID: 2806
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAvailability2MarkerReq : BaseMessageReq
	{
		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x0001CD76 File Offset: 0x0001AF76
		// (set) Token: 0x06003B47 RID: 15175 RVA: 0x0001CD7E File Offset: 0x0001AF7E
		[DataMember]
		public Availability2MarkerDTO Marker { get; set; }
	}
}
