using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C5B RID: 3163
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadProofOfPurchaseResp
	{
		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x000203B1 File Offset: 0x0001E5B1
		// (set) Token: 0x060041FC RID: 16892 RVA: 0x000203B9 File Offset: 0x0001E5B9
		[DataMember]
		public int ProofOfPurchaseId { get; set; }
	}
}
