using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002E3 RID: 739
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProviderCoursesResp
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00007EAD File Offset: 0x000060AD
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x00007EB5 File Offset: 0x000060B5
		[DataMember]
		public IList<LookupCourseBaseDTO> CourseBases { get; set; }
	}
}
