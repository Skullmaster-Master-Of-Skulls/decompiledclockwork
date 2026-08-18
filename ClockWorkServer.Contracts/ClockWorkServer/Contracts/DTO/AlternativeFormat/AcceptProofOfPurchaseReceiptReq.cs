using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C66 RID: 3174
	[DataContract(Namespace = "http://tpro.ca")]
	public class AcceptProofOfPurchaseReceiptReq : BaseReportMessageReq
	{
		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x0002046C File Offset: 0x0001E66C
		// (set) Token: 0x0600421D RID: 16925 RVA: 0x00020474 File Offset: 0x0001E674
		[DataMember]
		public ProofOfPurchaseInfoDTO ProofOfPurchase { get; set; }
	}
}
