using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C9 RID: 713
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRequestCourseReq : BaseMessageReq
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x000078ED File Offset: 0x00005AED
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x000078F5 File Offset: 0x00005AF5
		[DataMember]
		public int SPRequestId { get; set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x000078FE File Offset: 0x00005AFE
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x00007906 File Offset: 0x00005B06
		[DataMember]
		public SPRequestCourseDTO RequestCourse { get; set; }
	}
}
