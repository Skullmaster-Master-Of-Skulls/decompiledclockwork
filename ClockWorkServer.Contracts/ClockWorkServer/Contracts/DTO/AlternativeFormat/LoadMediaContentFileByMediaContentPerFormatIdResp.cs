using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B62 RID: 2914
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByMediaContentPerFormatIdResp
	{
		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x0001E587 File Offset: 0x0001C787
		// (set) Token: 0x06003DCF RID: 15823 RVA: 0x0001E58F File Offset: 0x0001C78F
		[DataMember]
		public IList<MediaContentFileWithoutDataDTO> MediaContentFileList { get; set; }
	}
}
