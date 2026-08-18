using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000498 RID: 1176
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMultipleEmailsFromTemplateInWebSettingsReq : BaseReportMessageReq
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0000BA7A File Offset: 0x00009C7A
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0000BA82 File Offset: 0x00009C82
		[DataMember]
		public IList<MailMergeContextWithCustomDictionaryDTO> ContextsWithCustomDictionaries { get; set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x0000BA8B File Offset: 0x00009C8B
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x0000BA93 File Offset: 0x00009C93
		[DataMember]
		public int WebSettingId { get; set; }
	}
}
