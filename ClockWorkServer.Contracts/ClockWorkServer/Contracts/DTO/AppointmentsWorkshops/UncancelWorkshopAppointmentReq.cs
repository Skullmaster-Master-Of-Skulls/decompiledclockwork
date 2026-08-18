using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008FD RID: 2301
	[DataContract(Namespace = "http://tpro.ca")]
	public class UncancelWorkshopAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x000164D5 File Offset: 0x000146D5
		// (set) Token: 0x06002EE4 RID: 12004 RVA: 0x000164DD File Offset: 0x000146DD
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
