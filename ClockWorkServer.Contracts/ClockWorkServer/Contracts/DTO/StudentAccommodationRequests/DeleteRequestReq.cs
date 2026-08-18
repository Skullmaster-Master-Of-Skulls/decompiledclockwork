using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200024F RID: 591
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteRequestReq : BaseMessageReq
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00006225 File Offset: 0x00004425
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0000622D File Offset: 0x0000442D
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }
	}
}
