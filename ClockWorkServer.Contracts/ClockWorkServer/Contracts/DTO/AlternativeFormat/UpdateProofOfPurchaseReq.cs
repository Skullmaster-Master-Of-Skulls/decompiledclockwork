using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C5C RID: 3164
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProofOfPurchaseReq : BaseReportMessageReq
	{
		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x000203C2 File Offset: 0x0001E5C2
		// (set) Token: 0x060041FF RID: 16895 RVA: 0x000203CA File Offset: 0x0001E5CA
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}
