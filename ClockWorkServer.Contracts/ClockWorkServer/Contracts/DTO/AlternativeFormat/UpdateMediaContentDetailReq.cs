using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B8A RID: 2954
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaContentDetailReq : BaseMessageReq
	{
		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06003E54 RID: 15956 RVA: 0x0001E8A6 File Offset: 0x0001CAA6
		// (set) Token: 0x06003E55 RID: 15957 RVA: 0x0001E8AE File Offset: 0x0001CAAE
		[DataMember]
		public MediaContentDetailDTO MediaContentDetail { get; set; }
	}
}
