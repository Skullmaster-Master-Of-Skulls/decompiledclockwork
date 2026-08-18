using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007CC RID: 1996
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesBySessionResp
	{
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x00013537 File Offset: 0x00011737
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x0001353F File Offset: 0x0001173F
		[DataMember]
		public IList<CourseRegistrationDTO> Courses { get; set; }
	}
}
