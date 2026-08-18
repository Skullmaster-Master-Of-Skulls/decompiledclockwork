using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200071D RID: 1821
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewDataByStudentNumberReq : BaseReportMessageReq
	{
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x00011232 File Offset: 0x0000F432
		// (set) Token: 0x06002586 RID: 9606 RVA: 0x0001123A File Offset: 0x0000F43A
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x00011243 File Offset: 0x0000F443
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x0001124B File Offset: 0x0000F44B
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
