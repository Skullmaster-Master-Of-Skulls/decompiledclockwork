using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C5A RID: 3162
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadProofOfPurchaseReq : BaseReportMessageReq
	{
		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x000203A0 File Offset: 0x0001E5A0
		// (set) Token: 0x060041F9 RID: 16889 RVA: 0x000203A8 File Offset: 0x0001E5A8
		[DataMember]
		public ProofOfPurchaseInfoDTO ProofOfPurchaseInfo { get; set; }
	}
}
