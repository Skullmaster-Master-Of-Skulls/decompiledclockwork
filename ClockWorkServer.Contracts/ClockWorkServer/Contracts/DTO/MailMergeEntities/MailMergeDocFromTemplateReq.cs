using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000480 RID: 1152
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeDocFromTemplateReq : BaseReportMessageReq
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0000B75B File Offset: 0x0000995B
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x0000B763 File Offset: 0x00009963
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0000B76C File Offset: 0x0000996C
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x0000B774 File Offset: 0x00009974
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0000B77D File Offset: 0x0000997D
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0000B785 File Offset: 0x00009985
		[DataMember]
		public int TemplateId { get; set; }
	}
}
