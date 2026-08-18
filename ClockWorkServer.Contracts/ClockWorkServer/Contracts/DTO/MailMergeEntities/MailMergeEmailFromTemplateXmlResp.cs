using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200048D RID: 1165
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeEmailFromTemplateXmlResp
	{
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0000B96A File Offset: 0x00009B6A
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x0000B972 File Offset: 0x00009B72
		[DataMember]
		public TPMailMessageDTO MailMessage { get; set; }
	}
}
