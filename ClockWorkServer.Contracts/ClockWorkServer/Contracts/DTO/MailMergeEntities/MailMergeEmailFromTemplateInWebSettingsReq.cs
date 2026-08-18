using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000490 RID: 1168
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeEmailFromTemplateInWebSettingsReq : BaseReportMessageReq
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0000B9AE File Offset: 0x00009BAE
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x0000B9B6 File Offset: 0x00009BB6
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0000B9BF File Offset: 0x00009BBF
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x0000B9C7 File Offset: 0x00009BC7
		[DataMember]
		public int WebSetting { get; set; }
	}
}
