using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B54 RID: 2900
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByLanguageResp
	{
		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x0001E466 File Offset: 0x0001C666
		// (set) Token: 0x06003D9F RID: 15775 RVA: 0x0001E46E File Offset: 0x0001C66E
		[DataMember]
		public IList<MediaContentFileWithoutDataDTO> MediaContentFiles { get; set; }
	}
}
