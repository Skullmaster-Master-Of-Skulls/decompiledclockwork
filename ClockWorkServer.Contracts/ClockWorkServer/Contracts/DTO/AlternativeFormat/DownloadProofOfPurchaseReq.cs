using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C58 RID: 3160
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadProofOfPurchaseReq : BaseReportMessageReq
	{
		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x060041F2 RID: 16882 RVA: 0x0002037E File Offset: 0x0001E57E
		// (set) Token: 0x060041F3 RID: 16883 RVA: 0x00020386 File Offset: 0x0001E586
		[DataMember]
		public int ProofOfPurchaseId { get; set; }
	}
}
