using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F3 RID: 2035
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorCoursesResp
	{
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x00013B43 File Offset: 0x00011D43
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x00013B4B File Offset: 0x00011D4B
		[DataMember]
		public List<LookupCourseDTO> Courses { get; set; }
	}
}
