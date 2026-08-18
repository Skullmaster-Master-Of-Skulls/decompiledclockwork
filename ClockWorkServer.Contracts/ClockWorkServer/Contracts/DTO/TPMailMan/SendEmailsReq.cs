using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B5 RID: 437
	[DataContract(Namespace = "http://tpro.ca")]
	public class SendEmailsReq : BaseMessageReq
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000048DF File Offset: 0x00002ADF
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x000048E7 File Offset: 0x00002AE7
		[DataMember]
		public List<TPMailMessageDTO> MailMessages { get; set; }
	}
}
