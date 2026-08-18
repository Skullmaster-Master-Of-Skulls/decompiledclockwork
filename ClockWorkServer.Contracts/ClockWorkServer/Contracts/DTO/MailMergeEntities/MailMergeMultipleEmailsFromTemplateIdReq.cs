using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000496 RID: 1174
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMultipleEmailsFromTemplateIdReq : BaseReportMessageReq
	{
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0000BA47 File Offset: 0x00009C47
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0000BA4F File Offset: 0x00009C4F
		[DataMember]
		public IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries { get; set; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0000BA58 File Offset: 0x00009C58
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x0000BA60 File Offset: 0x00009C60
		[DataMember]
		public int TemplateId { get; set; }
	}
}
