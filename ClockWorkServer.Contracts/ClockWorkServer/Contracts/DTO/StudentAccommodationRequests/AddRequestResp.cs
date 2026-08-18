using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200024D RID: 589
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddRequestResp
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00006203 File Offset: 0x00004403
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x0000620B File Offset: 0x0000440B
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }
	}
}
