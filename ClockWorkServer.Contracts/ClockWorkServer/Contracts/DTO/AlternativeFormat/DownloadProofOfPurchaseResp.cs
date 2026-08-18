using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C59 RID: 3161
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadProofOfPurchaseResp
	{
		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x0002038F File Offset: 0x0001E58F
		// (set) Token: 0x060041F6 RID: 16886 RVA: 0x00020397 File Offset: 0x0001E597
		[DataMember]
		public ProofOfPurchaseInfoDTO ProofOfPurchase { get; set; }
	}
}
