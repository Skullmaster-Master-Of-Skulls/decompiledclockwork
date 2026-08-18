using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C5D RID: 3165
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProofOfPurchaseResp
	{
		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x000203D3 File Offset: 0x0001E5D3
		// (set) Token: 0x06004202 RID: 16898 RVA: 0x000203DB File Offset: 0x0001E5DB
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}
