using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000491 RID: 1169
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeEmailFromTemplateResp
	{
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		[DataMember]
		public TPMailMessageDTO MailMessage { get; set; }
	}
}
