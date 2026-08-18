using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B81 RID: 2945
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentPerFormatInfoByMediaContentResp
	{
		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		// (set) Token: 0x06003E38 RID: 15928 RVA: 0x0001E804 File Offset: 0x0001CA04
		[DataMember]
		public IList<MediaContentPerFormatInfoDTO> MediaContentPerFormatInfoList { get; set; }
	}
}
