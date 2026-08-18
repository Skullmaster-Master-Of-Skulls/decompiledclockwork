using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000295 RID: 661
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCourseRegistrationReq : BaseMessageReq
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00007557 File Offset: 0x00005757
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0000755F File Offset: 0x0000575F
		[DataMember]
		public SPProviderCourseRegistrationDTO CourseRegistration { get; set; }
	}
}
