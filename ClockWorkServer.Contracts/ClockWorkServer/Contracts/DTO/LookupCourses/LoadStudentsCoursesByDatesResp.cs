using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007CE RID: 1998
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesByDatesResp
	{
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x0001357B File Offset: 0x0001177B
		// (set) Token: 0x060028D8 RID: 10456 RVA: 0x00013583 File Offset: 0x00011783
		[DataMember]
		public IList<CourseRegistrationDTO> Courses { get; set; }
	}
}
