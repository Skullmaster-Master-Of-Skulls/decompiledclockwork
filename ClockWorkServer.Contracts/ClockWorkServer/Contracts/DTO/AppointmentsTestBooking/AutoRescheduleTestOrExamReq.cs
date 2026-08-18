using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D5 RID: 2517
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoRescheduleTestOrExamReq : BaseMessageReq
	{
		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000196C7 File Offset: 0x000178C7
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x000196CF File Offset: 0x000178CF
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
