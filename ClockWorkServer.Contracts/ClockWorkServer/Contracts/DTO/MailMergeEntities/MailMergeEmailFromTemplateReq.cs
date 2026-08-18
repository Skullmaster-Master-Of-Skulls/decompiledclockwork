using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000492 RID: 1170
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeEmailFromTemplateReq : BaseReportMessageReq
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0000B9E1 File Offset: 0x00009BE1
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x0000B9E9 File Offset: 0x00009BE9
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0000B9F2 File Offset: 0x00009BF2
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x0000B9FA File Offset: 0x00009BFA
		[DataMember]
		public int TemplateId { get; set; }
	}
}
