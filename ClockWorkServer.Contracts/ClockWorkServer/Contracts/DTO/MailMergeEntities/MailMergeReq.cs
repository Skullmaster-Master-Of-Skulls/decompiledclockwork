using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004AB RID: 1195
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeReq : BaseReportMessageReq
	{
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0000BC45 File Offset: 0x00009E45
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x0000BC4D File Offset: 0x00009E4D
		[DataMember]
		public MailMergeCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0000BC56 File Offset: 0x00009E56
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x0000BC5E File Offset: 0x00009E5E
		[DataMember]
		public IList<MailMergeCodeDTO> Codes { get; set; }
	}
}
