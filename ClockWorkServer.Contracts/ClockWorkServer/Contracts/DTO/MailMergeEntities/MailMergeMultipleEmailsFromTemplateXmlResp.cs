using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000493 RID: 1171
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMultipleEmailsFromTemplateXmlResp
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0000BA03 File Offset: 0x00009C03
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x0000BA0B File Offset: 0x00009C0B
		[DataMember]
		public IDictionary<MailMergeContextDTO, TPMailMessageDTO> MailMessages { get; set; }
	}
}
