using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000830 RID: 2096
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesWithStudentSpecificInfosResp
	{
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00014453 File Offset: 0x00012653
		// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x0001445B File Offset: 0x0001265B
		[DataMember]
		public List<CourseRegistrationWithStudentSpecificInfoDTO> CourseRegistrationsWithStudentSpecificInfos { get; set; }
	}
}
