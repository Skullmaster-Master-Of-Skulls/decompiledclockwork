using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C69 RID: 3177
	[DataContract(Namespace = "http://tpro.ca")]
	public class RejectProofOfPurchaseReceiptResp
	{
		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x0002049F File Offset: 0x0001E69F
		// (set) Token: 0x06004226 RID: 16934 RVA: 0x000204A7 File Offset: 0x0001E6A7
		[DataMember]
		public bool Rejected { get; set; }
	}
}
