using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B76 RID: 2934
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaContentReq : BaseMessageReq
	{
		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x0001E763 File Offset: 0x0001C963
		// (set) Token: 0x06003E1B RID: 15899 RVA: 0x0001E76B File Offset: 0x0001C96B
		[DataMember]
		public MediaContentDTO MediaContent { get; set; }
	}
}
