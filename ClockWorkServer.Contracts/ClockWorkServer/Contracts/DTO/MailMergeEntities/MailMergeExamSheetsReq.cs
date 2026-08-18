using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000474 RID: 1140
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeExamSheetsReq : BaseReportMessageReq
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0000B590 File Offset: 0x00009790
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x0000B598 File Offset: 0x00009798
		[DataMember]
		public IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries { get; set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0000B5A1 File Offset: 0x000097A1
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x0000B5A9 File Offset: 0x000097A9
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0000B5B2 File Offset: 0x000097B2
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0000B5BA File Offset: 0x000097BA
		[DataMember]
		public int TemplateId { get; set; }
	}
}
