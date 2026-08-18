using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000254 RID: 596
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsByStudentAndDateReq : BaseMessageReq
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00006269 File Offset: 0x00004469
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x00006271 File Offset: 0x00004471
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0000627A File Offset: 0x0000447A
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x00006282 File Offset: 0x00004482
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0000628B File Offset: 0x0000448B
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x00006293 File Offset: 0x00004493
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
