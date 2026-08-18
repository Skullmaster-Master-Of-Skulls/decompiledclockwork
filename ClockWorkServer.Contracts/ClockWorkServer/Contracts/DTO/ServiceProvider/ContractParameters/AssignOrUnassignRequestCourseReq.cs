using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002D5 RID: 725
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignOrUnassignRequestCourseReq : BaseMessageReq
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00007986 File Offset: 0x00005B86
		// (set) Token: 0x0600105C RID: 4188 RVA: 0x0000798E File Offset: 0x00005B8E
		[DataMember]
		public int SPRequestCourseId { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00007997 File Offset: 0x00005B97
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x0000799F File Offset: 0x00005B9F
		[DataMember]
		public SPRequestCourseAssignmentDTO RequestCourseAssignment { get; set; }
	}
}
