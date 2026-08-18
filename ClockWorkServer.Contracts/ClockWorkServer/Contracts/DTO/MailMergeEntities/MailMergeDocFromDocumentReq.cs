using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000482 RID: 1154
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeDocFromDocumentReq : BaseReportMessageReq
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0000B79F File Offset: 0x0000999F
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0000B7A7 File Offset: 0x000099A7
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0000B7B0 File Offset: 0x000099B0
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x0000B7B8 File Offset: 0x000099B8
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0000B7C1 File Offset: 0x000099C1
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x0000B7C9 File Offset: 0x000099C9
		[DataMember]
		public BinaryFileDTO BinaryFile { get; set; }
	}
}
