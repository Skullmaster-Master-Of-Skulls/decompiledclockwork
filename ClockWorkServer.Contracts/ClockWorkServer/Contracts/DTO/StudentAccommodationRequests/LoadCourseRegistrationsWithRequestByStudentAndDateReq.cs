using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200024A RID: 586
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsWithRequestByStudentAndDateReq : BaseMessageReq
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0000618C File Offset: 0x0000438C
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00006194 File Offset: 0x00004394
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0000619D File Offset: 0x0000439D
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x000061A5 File Offset: 0x000043A5
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000061AE File Offset: 0x000043AE
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x000061B6 File Offset: 0x000043B6
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x000061BF File Offset: 0x000043BF
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x000061C7 File Offset: 0x000043C7
		[DataMember]
		public bool LoadAccommodations { get; set; }
	}
}
