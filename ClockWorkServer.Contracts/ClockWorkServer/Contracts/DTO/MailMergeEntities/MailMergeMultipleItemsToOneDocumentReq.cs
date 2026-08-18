using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000484 RID: 1156
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeMultipleItemsToOneDocumentReq : BaseReportMessageReq
	{
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0000B7E3 File Offset: 0x000099E3
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0000B7EB File Offset: 0x000099EB
		[DataMember]
		public IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries { get; set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0000B7F4 File Offset: 0x000099F4
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0000B7FC File Offset: 0x000099FC
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0000B805 File Offset: 0x00009A05
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x0000B80D File Offset: 0x00009A0D
		[DataMember]
		public int TemplateId { get; set; }
	}
}
