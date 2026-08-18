using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008FF RID: 2303
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteWorkshopAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000164E6 File Offset: 0x000146E6
		// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x000164EE File Offset: 0x000146EE
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
