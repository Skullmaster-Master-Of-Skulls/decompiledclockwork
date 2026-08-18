using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B7D RID: 2941
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllMediaContentWithFormatsResp
	{
		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
		// (set) Token: 0x06003E2C RID: 15916 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		[DataMember]
		public IList<MediaContentDTO> MediaContentList { get; set; }
	}
}
