using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000253 RID: 595
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPendingRequestsForTodayResp
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00006258 File Offset: 0x00004458
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x00006260 File Offset: 0x00004460
		[DataMember]
		public IList<StudentCourseAccommodationRequestDTO> CourseAccommodationRequests { get; set; }
	}
}
