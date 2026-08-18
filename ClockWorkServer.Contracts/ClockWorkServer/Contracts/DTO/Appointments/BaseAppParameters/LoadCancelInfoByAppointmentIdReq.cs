using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200093B RID: 2363
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCancelInfoByAppointmentIdReq : BaseMessageReq
	{
		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x00017B60 File Offset: 0x00015D60
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x00017B68 File Offset: 0x00015D68
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
