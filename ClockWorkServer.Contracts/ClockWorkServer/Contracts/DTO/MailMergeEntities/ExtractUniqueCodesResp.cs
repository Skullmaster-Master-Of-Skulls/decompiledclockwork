using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A3 RID: 1187
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExtractUniqueCodesResp
	{
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0000BB9B File Offset: 0x00009D9B
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x0000BBA3 File Offset: 0x00009DA3
		[DataMember]
		public IList<MailMergeCodeDTO> Codes { get; set; }
	}
}
