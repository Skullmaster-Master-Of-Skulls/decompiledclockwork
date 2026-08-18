using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000250 RID: 592
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestByIdReq : BaseMessageReq
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00006236 File Offset: 0x00004436
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0000623E File Offset: 0x0000443E
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }
	}
}
