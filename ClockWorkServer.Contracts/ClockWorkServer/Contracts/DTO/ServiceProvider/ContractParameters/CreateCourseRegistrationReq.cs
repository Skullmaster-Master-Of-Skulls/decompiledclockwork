using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000299 RID: 665
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCourseRegistrationReq : BaseMessageReq
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0000758A File Offset: 0x0000578A
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00007592 File Offset: 0x00005792
		[DataMember]
		public SPProviderCourseRegistrationDTO CourseRegistration { get; set; }
	}
}
