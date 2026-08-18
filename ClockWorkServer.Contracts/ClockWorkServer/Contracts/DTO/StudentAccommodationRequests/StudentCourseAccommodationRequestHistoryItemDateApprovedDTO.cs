using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000259 RID: 601
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentCourseAccommodationRequestHistoryItemDateApprovedDTO
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00006699 File Offset: 0x00004899
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x000066A1 File Offset: 0x000048A1
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000066AA File Offset: 0x000048AA
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x000066B2 File Offset: 0x000048B2
		[DataMember]
		public DateTime DateApproved { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000066BB File Offset: 0x000048BB
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x000066C3 File Offset: 0x000048C3
		[DataMember]
		public PersonBaseDTO WhoApproved { get; set; }
	}
}
