using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B80 RID: 2944
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentPerFormatInfoByMediaContentReq : BaseMessageReq
	{
		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x0001E7EB File Offset: 0x0001C9EB
		// (set) Token: 0x06003E35 RID: 15925 RVA: 0x0001E7F3 File Offset: 0x0001C9F3
		[DataMember]
		public Guid MediaContentId { get; set; }
	}
}
