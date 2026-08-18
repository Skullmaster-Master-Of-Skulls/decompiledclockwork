using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A8 RID: 1192
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExtractMailMergeCodesFromWordResp
	{
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		[DataMember]
		public List<MailMergeCodeDTO> Codes { get; set; }
	}
}
