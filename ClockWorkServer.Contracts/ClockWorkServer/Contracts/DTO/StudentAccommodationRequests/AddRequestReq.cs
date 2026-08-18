using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200024C RID: 588
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddRequestReq : BaseMessageReq
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x000061E1 File Offset: 0x000043E1
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x000061E9 File Offset: 0x000043E9
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000061F2 File Offset: 0x000043F2
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x000061FA File Offset: 0x000043FA
		[DataMember]
		public StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest { get; set; }
	}
}
