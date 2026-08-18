using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200043C RID: 1084
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadEquivalentCoursesResp
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0000ACF6 File Offset: 0x00008EF6
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x0000ACFE File Offset: 0x00008EFE
		[DataMember]
		public List<LookupCourseBaseDTO> Courses { get; set; }
	}
}
