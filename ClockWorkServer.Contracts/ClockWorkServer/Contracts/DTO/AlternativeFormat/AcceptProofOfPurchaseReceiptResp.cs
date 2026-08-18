using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C67 RID: 3175
	[DataContract(Namespace = "http://tpro.ca")]
	public class AcceptProofOfPurchaseReceiptResp
	{
		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x0002047D File Offset: 0x0001E67D
		// (set) Token: 0x06004220 RID: 16928 RVA: 0x00020485 File Offset: 0x0001E685
		[DataMember]
		public ProofOfPurchaseInfoDTO ProofOfPurchase { get; set; }
	}
}
