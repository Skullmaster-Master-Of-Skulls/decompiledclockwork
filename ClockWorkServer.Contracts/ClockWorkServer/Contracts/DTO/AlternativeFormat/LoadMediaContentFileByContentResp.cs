using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B56 RID: 2902
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByContentResp
	{
		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x0001E499 File Offset: 0x0001C699
		// (set) Token: 0x06003DA7 RID: 15783 RVA: 0x0001E4A1 File Offset: 0x0001C6A1
		[DataMember]
		public IList<MediaContentFileWithoutDataDTO> MediaContentFiles { get; set; }
	}
}
