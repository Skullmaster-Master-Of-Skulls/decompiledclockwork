using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200093D RID: 2365
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteCancelInfoReq : BaseMessageReq
	{
		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x00017B82 File Offset: 0x00015D82
		// (set) Token: 0x0600308B RID: 12427 RVA: 0x00017B8A File Offset: 0x00015D8A
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
