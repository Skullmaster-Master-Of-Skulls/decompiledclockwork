using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000247 RID: 583
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsWithRequestByStatusResp
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00006148 File Offset: 0x00004348
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x00006150 File Offset: 0x00004350
		[DataMember]
		public IList<StudentCourseAccommodationRequestDTO> CoursesWithRequests { get; set; }
	}
}
