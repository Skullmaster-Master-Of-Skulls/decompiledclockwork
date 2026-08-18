using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B19 RID: 2841
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkAppointmentTentativeReq : BaseMessageReq
	{
		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x0001D1DD File Offset: 0x0001B3DD
		// (set) Token: 0x06003BEC RID: 15340 RVA: 0x0001D1E5 File Offset: 0x0001B3E5
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
