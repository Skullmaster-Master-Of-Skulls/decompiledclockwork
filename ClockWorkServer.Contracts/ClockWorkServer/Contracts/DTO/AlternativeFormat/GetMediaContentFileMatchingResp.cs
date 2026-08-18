using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B60 RID: 2912
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentFileMatchingResp
	{
		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x0001E554 File Offset: 0x0001C754
		// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x0001E55C File Offset: 0x0001C75C
		[DataMember]
		public IList<MediaContentFileWithoutDataDTO> MediaContentFileList { get; set; }
	}
}
