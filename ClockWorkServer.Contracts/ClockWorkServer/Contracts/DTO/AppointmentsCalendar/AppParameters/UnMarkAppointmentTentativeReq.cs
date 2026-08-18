using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B18 RID: 2840
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnMarkAppointmentTentativeReq : BaseMessageReq
	{
		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
