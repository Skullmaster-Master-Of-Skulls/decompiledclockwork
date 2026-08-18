using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001BC RID: 444
	[DataContract(Namespace = "http://tpro.ca")]
	public class SendEmailsReturnResultResp
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00004989 File Offset: 0x00002B89
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x00004991 File Offset: 0x00002B91
		[DataMember]
		public TPMailResultDTO SendEmailResult { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0000499A File Offset: 0x00002B9A
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x000049A2 File Offset: 0x00002BA2
		[DataMember]
		public List<TPMailMessageDTO> MailMessages { get; set; }
	}
}
