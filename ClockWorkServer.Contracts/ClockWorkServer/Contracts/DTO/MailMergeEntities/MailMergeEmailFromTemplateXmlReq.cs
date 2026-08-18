using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200048E RID: 1166
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeEmailFromTemplateXmlReq : BaseReportMessageReq
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0000B97B File Offset: 0x00009B7B
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0000B983 File Offset: 0x00009B83
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0000B98C File Offset: 0x00009B8C
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x0000B994 File Offset: 0x00009B94
		[DataMember]
		public string TemplateXml { get; set; }
	}
}
