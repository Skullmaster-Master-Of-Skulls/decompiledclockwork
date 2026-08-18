using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200048F RID: 1167
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeEmailFromTemplateInWebSettingsResp
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0000B99D File Offset: 0x00009B9D
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x0000B9A5 File Offset: 0x00009BA5
		[DataMember]
		public TPMailMessageDTO MailMessage { get; set; }
	}
}
