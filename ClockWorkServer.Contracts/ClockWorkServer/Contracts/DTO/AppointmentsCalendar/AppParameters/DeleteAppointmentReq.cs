using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B13 RID: 2835
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAppointmentReq : BaseMessageReq
	{
		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x0001D177 File Offset: 0x0001B377
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x0001D17F File Offset: 0x0001B37F
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
