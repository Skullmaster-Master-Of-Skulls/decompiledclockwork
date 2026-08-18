using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000246 RID: 582
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsWithRequestByStatusReq : BaseMessageReq
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00006126 File Offset: 0x00004326
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0000612E File Offset: 0x0000432E
		[DataMember]
		public eStudentCourseAccommodationRequestStatusDTO Statuses { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00006137 File Offset: 0x00004337
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0000613F File Offset: 0x0000433F
		[DataMember]
		public Range<DateTime> RestrictCourseDates { get; set; }
	}
}
