using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007FD RID: 2045
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsWithCourseAndAccommodationInfosByCoursesResp
	{
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x00013C97 File Offset: 0x00011E97
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x00013C9F File Offset: 0x00011E9F
		[DataMember]
		public IList<StudentWithCourseAndAccommodationInfoDTO> StudentsWithCourseAndAccommodationInfos { get; set; }
	}
}
