using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F9 RID: 2041
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp
	{
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x00013BED File Offset: 0x00011DED
		// (set) Token: 0x0600299F RID: 10655 RVA: 0x00013BF5 File Offset: 0x00011DF5
		[DataMember]
		public List<LookupCourseDTO> Courses { get; set; }
	}
}
