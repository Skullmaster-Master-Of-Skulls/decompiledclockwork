using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002CD RID: 717
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequestCourseReq : BaseMessageReq
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00007920 File Offset: 0x00005B20
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x00007928 File Offset: 0x00005B28
		[DataMember]
		public SPRequestCourseDTO RequestCourse { get; set; }
	}
}
