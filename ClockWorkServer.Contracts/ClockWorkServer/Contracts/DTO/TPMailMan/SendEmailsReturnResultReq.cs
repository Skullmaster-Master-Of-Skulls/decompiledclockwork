using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001BB RID: 443
	[DataContract(Namespace = "http://tpro.ca")]
	public class SendEmailsReturnResultReq : BaseMessageReq
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00004956 File Offset: 0x00002B56
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0000495E File Offset: 0x00002B5E
		[DataMember]
		public IList<TPMailMessageDTO> MailMessages { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00004967 File Offset: 0x00002B67
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0000496F File Offset: 0x00002B6F
		[DataMember]
		public string EmailTestModeAddress { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00004978 File Offset: 0x00002B78
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00004980 File Offset: 0x00002B80
		[DataMember]
		public string ContextForLogging { get; set; }
	}
}
