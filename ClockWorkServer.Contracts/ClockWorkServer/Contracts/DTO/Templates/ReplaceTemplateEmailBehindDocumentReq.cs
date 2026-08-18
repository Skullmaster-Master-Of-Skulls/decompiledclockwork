using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Templates
{
	// Token: 0x020001CA RID: 458
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReplaceTemplateEmailBehindDocumentReq : BaseMessageReq
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00004DE0 File Offset: 0x00002FE0
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00004DE8 File Offset: 0x00002FE8
		[DataMember]
		public int TemplateId { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00004DF1 File Offset: 0x00002FF1
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00004DF9 File Offset: 0x00002FF9
		[DataMember]
		public TPMailMessageDTO EmailTemplate { get; set; }
	}
}
