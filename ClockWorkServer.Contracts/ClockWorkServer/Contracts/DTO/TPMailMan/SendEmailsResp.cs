using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B6 RID: 438
	[DataContract(Namespace = "http://tpro.ca")]
	public class SendEmailsResp
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000048F0 File Offset: 0x00002AF0
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x000048F8 File Offset: 0x00002AF8
		[DataMember]
		public TPMailResultDTO SendEmailResult { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00004901 File Offset: 0x00002B01
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00004909 File Offset: 0x00002B09
		[DataMember]
		public List<TPMailMessageDTO> MailMessages { get; set; }
	}
}
