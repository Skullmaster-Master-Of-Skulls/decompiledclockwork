using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000497 RID: 1175
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMultipleEmailsFromTemplateInWebSettingsResp
	{
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0000BA69 File Offset: 0x00009C69
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0000BA71 File Offset: 0x00009C71
		[DataMember]
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMessages { get; set; }
	}
}
