using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B7F RID: 2943
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentPerFormatInfoByIdResp
	{
		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x0001E7DA File Offset: 0x0001C9DA
		// (set) Token: 0x06003E32 RID: 15922 RVA: 0x0001E7E2 File Offset: 0x0001C9E2
		[DataMember]
		public MediaContentPerFormatInfoDTO MediaContentPerFormatInfo { get; set; }
	}
}
