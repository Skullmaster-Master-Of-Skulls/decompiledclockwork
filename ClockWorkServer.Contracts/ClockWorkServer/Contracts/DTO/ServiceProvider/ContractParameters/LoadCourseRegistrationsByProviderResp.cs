using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200028E RID: 654
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsByProviderResp
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000074CF File Offset: 0x000056CF
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x000074D7 File Offset: 0x000056D7
		[DataMember]
		public IList<SPProviderCourseRegistrationDTO> CourseRegistrations { get; set; }
	}
}
