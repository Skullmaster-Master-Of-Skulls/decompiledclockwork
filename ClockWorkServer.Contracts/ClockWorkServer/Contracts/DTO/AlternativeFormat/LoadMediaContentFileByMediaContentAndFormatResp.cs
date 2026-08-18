using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B64 RID: 2916
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByMediaContentAndFormatResp
	{
		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x0001E5CB File Offset: 0x0001C7CB
		// (set) Token: 0x06003DD9 RID: 15833 RVA: 0x0001E5D3 File Offset: 0x0001C7D3
		[DataMember]
		public IList<MediaContentFileWithoutDataDTO> MediaContentFileList { get; set; }
	}
}
