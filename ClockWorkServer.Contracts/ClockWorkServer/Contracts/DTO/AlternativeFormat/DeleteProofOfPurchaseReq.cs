using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C5E RID: 3166
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProofOfPurchaseReq : BaseReportMessageReq
	{
		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x000203E4 File Offset: 0x0001E5E4
		// (set) Token: 0x06004205 RID: 16901 RVA: 0x000203EC File Offset: 0x0001E5EC
		[DataMember]
		public int ProofOfPurchaseId { get; set; }
	}
}
