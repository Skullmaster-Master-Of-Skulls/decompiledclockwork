using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B50 RID: 2896
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllMediaContenFileSortedByMediaContentFileResp
	{
		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x0001E433 File Offset: 0x0001C633
		// (set) Token: 0x06003D95 RID: 15765 RVA: 0x0001E43B File Offset: 0x0001C63B
		[DataMember]
		public IList<MediaContentFileWithoutDataDTO> MediaContentFiles { get; set; }
	}
}
