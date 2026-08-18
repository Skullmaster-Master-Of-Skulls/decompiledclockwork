using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000494 RID: 1172
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMultipleEmailsFromTemplateXmlReq : BaseReportMessageReq
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0000BA14 File Offset: 0x00009C14
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x0000BA1C File Offset: 0x00009C1C
		[DataMember]
		public IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries { get; set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0000BA25 File Offset: 0x00009C25
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x0000BA2D File Offset: 0x00009C2D
		[DataMember]
		public string TemplateXml { get; set; }
	}
}
