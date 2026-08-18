using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000495 RID: 1173
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMultipleEmailsFromTemplateIdResp
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0000BA36 File Offset: 0x00009C36
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x0000BA3E File Offset: 0x00009C3E
		[DataMember]
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMessages { get; set; }
	}
}
