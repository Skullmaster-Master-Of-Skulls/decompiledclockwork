using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000471 RID: 1137
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMailingLabelsReq : BaseReportMessageReq
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0000B53B File Offset: 0x0000973B
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x0000B543 File Offset: 0x00009743
		[DataMember]
		public IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries { get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0000B54C File Offset: 0x0000974C
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0000B554 File Offset: 0x00009754
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0000B55D File Offset: 0x0000975D
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0000B565 File Offset: 0x00009765
		[DataMember]
		public int TemplateId { get; set; }
	}
}
