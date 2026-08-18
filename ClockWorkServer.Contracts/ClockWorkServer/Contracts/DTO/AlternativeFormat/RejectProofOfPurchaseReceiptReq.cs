using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C68 RID: 3176
	[DataContract(Namespace = "http://tpro.ca")]
	public class RejectProofOfPurchaseReceiptReq : BaseReportMessageReq
	{
		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x0002048E File Offset: 0x0001E68E
		// (set) Token: 0x06004223 RID: 16931 RVA: 0x00020496 File Offset: 0x0001E696
		[DataMember]
		public ProofOfPurchaseInfoDTO ProofOfPurchase { get; set; }
	}
}
