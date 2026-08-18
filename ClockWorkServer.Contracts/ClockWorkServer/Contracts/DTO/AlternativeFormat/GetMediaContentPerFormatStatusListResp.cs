using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B83 RID: 2947
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentPerFormatStatusListResp
	{
		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x0001E82F File Offset: 0x0001CA2F
		// (set) Token: 0x06003E40 RID: 15936 RVA: 0x0001E837 File Offset: 0x0001CA37
		[DataMember]
		public IList<MediaContentPerFormatStatusInfoDTO> MediaContentPerFormatStatusList { get; set; }
	}
}
