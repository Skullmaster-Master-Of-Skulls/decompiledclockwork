using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200082A RID: 2090
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesStudentIsAllowedToBookFinalExamsForNowResp
	{
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x00014362 File Offset: 0x00012562
		public IList<CourseRegistrationDTO> CourseRegistrations
		{
			get
			{
				StudentCourseListDTO courseList = this.CourseList;
				return (courseList != null) ? courseList.Courses : null;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x00014376 File Offset: 0x00012576
		// (set) Token: 0x06002A98 RID: 10904 RVA: 0x0001437E File Offset: 0x0001257E
		[DataMember]
		public StudentCourseListDTO CourseList { get; set; }
	}
}
