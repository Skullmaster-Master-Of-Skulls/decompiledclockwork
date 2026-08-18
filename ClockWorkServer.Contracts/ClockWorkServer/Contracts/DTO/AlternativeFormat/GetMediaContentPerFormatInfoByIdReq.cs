using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B7E RID: 2942
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentPerFormatInfoByIdReq : BaseMessageReq
	{
		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x0001E7C9 File Offset: 0x0001C9C9
		// (set) Token: 0x06003E2F RID: 15919 RVA: 0x0001E7D1 File Offset: 0x0001C9D1
		[DataMember]
		public int MediaContentPerFormatId { get; set; }
	}
}
