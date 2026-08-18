using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200093C RID: 2364
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCancelInfoByAppointmentIdResp
	{
		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x00017B71 File Offset: 0x00015D71
		// (set) Token: 0x06003088 RID: 12424 RVA: 0x00017B79 File Offset: 0x00015D79
		[DataMember]
		public AppCancelInfoDTO AppCancelInfo { get; set; }
	}
}
