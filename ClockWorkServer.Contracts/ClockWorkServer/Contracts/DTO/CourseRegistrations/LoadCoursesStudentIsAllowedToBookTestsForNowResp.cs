using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000828 RID: 2088
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesStudentIsAllowedToBookTestsForNowResp
	{
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x0001432C File Offset: 0x0001252C
		public IList<CourseRegistrationDTO> CourseRegistrations
		{
			get
			{
				StudentCourseListDTO courseList = this.CourseList;
				return (courseList != null) ? courseList.Courses : null;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x00014340 File Offset: 0x00012540
		// (set) Token: 0x06002A91 RID: 10897 RVA: 0x00014348 File Offset: 0x00012548
		[DataMember]
		public StudentCourseListDTO CourseList { get; set; }
	}
}
