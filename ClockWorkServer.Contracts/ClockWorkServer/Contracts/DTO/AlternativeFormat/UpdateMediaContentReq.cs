using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B78 RID: 2936
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaContentReq : BaseMessageReq
	{
		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x0001E785 File Offset: 0x0001C985
		// (set) Token: 0x06003E21 RID: 15905 RVA: 0x0001E78D File Offset: 0x0001C98D
		[DataMember]
		public MediaContentDTO MediaContent { get; set; }
	}
}
