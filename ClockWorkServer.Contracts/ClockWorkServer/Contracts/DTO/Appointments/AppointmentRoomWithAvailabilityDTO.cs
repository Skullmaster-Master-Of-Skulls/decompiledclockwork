using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x0200092D RID: 2349
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentRoomWithAvailabilityDTO : AppointmentRoomDTO
	{
		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x00016E04 File Offset: 0x00015004
		// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x00016E0C File Offset: 0x0001500C
		[DataMember]
		public bool IsAvailable { get; set; }
	}
}
