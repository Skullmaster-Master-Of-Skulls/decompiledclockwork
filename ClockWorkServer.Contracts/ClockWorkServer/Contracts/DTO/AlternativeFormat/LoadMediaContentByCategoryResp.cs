using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B75 RID: 2933
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByCategoryResp
	{
		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x0001E752 File Offset: 0x0001C952
		// (set) Token: 0x06003E18 RID: 15896 RVA: 0x0001E75A File Offset: 0x0001C95A
		[DataMember]
		public IList<MediaContentDTO> MediaContents { get; set; }
	}
}
