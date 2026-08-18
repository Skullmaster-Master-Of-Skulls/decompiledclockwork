using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000255 RID: 597
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsByStudentAndDateResp
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0000629C File Offset: 0x0000449C
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x000062A4 File Offset: 0x000044A4
		[DataMember]
		public IList<StudentCourseAccommodationRequestDTO> CourseAccommodationRequests { get; set; }
	}
}
