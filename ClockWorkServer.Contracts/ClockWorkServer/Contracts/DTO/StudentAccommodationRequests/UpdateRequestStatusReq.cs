using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000245 RID: 581
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequestStatusReq : BaseMessageReq
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00006104 File Offset: 0x00004304
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x0000610C File Offset: 0x0000430C
		[DataMember]
		public eStudentCourseAccommodationRequestStatusDTO Statuses { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00006115 File Offset: 0x00004315
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x0000611D File Offset: 0x0000431D
		[DataMember]
		public int StudentAccommodationRequestId { get; set; }
	}
}
