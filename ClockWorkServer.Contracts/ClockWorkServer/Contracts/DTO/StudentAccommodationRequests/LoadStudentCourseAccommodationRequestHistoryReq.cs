using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000248 RID: 584
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentCourseAccommodationRequestHistoryReq : BaseMessageReq
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00006159 File Offset: 0x00004359
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00006161 File Offset: 0x00004361
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0000616A File Offset: 0x0000436A
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00006172 File Offset: 0x00004372
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
