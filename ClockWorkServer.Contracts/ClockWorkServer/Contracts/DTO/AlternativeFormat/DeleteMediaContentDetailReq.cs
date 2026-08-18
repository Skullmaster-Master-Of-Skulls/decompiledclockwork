using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B8C RID: 2956
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteMediaContentDetailReq : BaseMessageReq
	{
		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x0001E8B7 File Offset: 0x0001CAB7
		// (set) Token: 0x06003E59 RID: 15961 RVA: 0x0001E8BF File Offset: 0x0001CABF
		[DataMember]
		public MediaContentDetailDTO MediaContentDetail { get; set; }
	}
}
