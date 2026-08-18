using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002D7 RID: 727
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignOrUnassignRequestEventReq : BaseMessageReq
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x000079A8 File Offset: 0x00005BA8
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x000079B0 File Offset: 0x00005BB0
		[DataMember]
		public int SPRequestEventId { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x000079B9 File Offset: 0x00005BB9
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x000079C1 File Offset: 0x00005BC1
		[DataMember]
		public SPRequestEventAssignmentDTO RequestEventAssignment { get; set; }
	}
}
