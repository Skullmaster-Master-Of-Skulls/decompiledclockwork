using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B15 RID: 2837
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentExtendedInfoReq : BaseMessageReq
	{
		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x0001D199 File Offset: 0x0001B399
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x0001D1A1 File Offset: 0x0001B3A1
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
