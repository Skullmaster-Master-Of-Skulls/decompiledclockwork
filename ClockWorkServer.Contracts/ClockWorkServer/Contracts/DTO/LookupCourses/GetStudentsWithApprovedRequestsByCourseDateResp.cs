using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007FB RID: 2043
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentsWithApprovedRequestsByCourseDateResp
	{
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00013C53 File Offset: 0x00011E53
		// (set) Token: 0x060029AD RID: 10669 RVA: 0x00013C5B File Offset: 0x00011E5B
		[DataMember]
		public IList<StudentWithRequestAndCourseInfoDTO> StudentsWithApprovedRequests { get; set; }
	}
}
