using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000258 RID: 600
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentCourseAccommodationRequestHistoryDTO
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00006688 File Offset: 0x00004888
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x00006690 File Offset: 0x00004890
		[DataMember]
		public IList<StudentCourseAccommodationRequestHistoryItemDTO> HistoryItems { get; set; }
	}
}
