using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000290 RID: 656
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationByIdResp
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00007513 File Offset: 0x00005713
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x0000751B File Offset: 0x0000571B
		[DataMember]
		public SPProviderCourseRegistrationDTO CourseRegistration { get; set; }
	}
}
