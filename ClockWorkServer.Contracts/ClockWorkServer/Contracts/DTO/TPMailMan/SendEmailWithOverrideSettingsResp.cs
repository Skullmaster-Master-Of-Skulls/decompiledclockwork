using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001BA RID: 442
	[DataContract(Namespace = "http://tpro.ca")]
	public class SendEmailWithOverrideSettingsResp
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00004945 File Offset: 0x00002B45
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0000494D File Offset: 0x00002B4D
		[DataMember]
		public TPMailResultDTO SendMailResult { get; set; }
	}
}
