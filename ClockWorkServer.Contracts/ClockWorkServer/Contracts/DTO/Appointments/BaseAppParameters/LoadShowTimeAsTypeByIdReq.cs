using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000973 RID: 2419
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadShowTimeAsTypeByIdReq : BaseMessageReq
	{
		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000180C1 File Offset: 0x000162C1
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x000180C9 File Offset: 0x000162C9
		[DataMember]
		public int AppointmentShowTimeAsId { get; set; }
	}
}
