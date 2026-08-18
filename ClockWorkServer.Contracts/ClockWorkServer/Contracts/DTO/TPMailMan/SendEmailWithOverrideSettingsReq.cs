using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B9 RID: 441
	[DataContract(Namespace = "http://tpro.ca")]
	public class SendEmailWithOverrideSettingsReq : BaseMessageReq
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00004923 File Offset: 0x00002B23
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0000492B File Offset: 0x00002B2B
		[DataMember]
		public TPSmtpClientDTO SmtpSettings { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00004934 File Offset: 0x00002B34
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0000493C File Offset: 0x00002B3C
		[DataMember]
		public TPMailMessageDTO Message { get; set; }
	}
}
