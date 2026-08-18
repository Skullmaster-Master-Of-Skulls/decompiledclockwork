using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000986 RID: 2438
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeGroupByIdReq : BaseMessageReq
	{
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x00018226 File Offset: 0x00016426
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x0001822E File Offset: 0x0001642E
		[DataMember]
		public int AppointmentTypeGroupId { get; set; }
	}
}
