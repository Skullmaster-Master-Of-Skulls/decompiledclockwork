using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000249 RID: 585
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentCourseAccommodationRequestHistoryResp
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0000617B File Offset: 0x0000437B
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x00006183 File Offset: 0x00004383
		[DataMember]
		public StudentCourseAccommodationRequestHistoryDTO AccommodationRequestHistory { get; set; }
	}
}
