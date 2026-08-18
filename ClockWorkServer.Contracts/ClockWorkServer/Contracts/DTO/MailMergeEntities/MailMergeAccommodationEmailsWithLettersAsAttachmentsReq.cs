using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000476 RID: 1142
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationEmailsWithLettersAsAttachmentsReq : BaseReportMessageReq
	{
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0000B5D4 File Offset: 0x000097D4
		// (set) Token: 0x06001888 RID: 6280 RVA: 0x0000B5DC File Offset: 0x000097DC
		[DataMember]
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0000B5E5 File Offset: 0x000097E5
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x0000B5ED File Offset: 0x000097ED
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0000B5F6 File Offset: 0x000097F6
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0000B5FE File Offset: 0x000097FE
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0000B607 File Offset: 0x00009807
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x0000B60F File Offset: 0x0000980F
		[DataMember]
		public int TemplateId { get; set; }
	}
}
