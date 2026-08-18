using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200024E RID: 590
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequestReq : BaseMessageReq
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00006214 File Offset: 0x00004414
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x0000621C File Offset: 0x0000441C
		[DataMember]
		public StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest { get; set; }
	}
}
