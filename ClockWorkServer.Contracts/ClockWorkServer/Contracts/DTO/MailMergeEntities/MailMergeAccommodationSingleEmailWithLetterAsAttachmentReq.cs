using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000478 RID: 1144
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq : BaseReportMessageReq
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0000B629 File Offset: 0x00009829
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0000B631 File Offset: 0x00009831
		[DataMember]
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0000B63A File Offset: 0x0000983A
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0000B642 File Offset: 0x00009842
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0000B64B File Offset: 0x0000984B
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0000B653 File Offset: 0x00009853
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0000B65C File Offset: 0x0000985C
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x0000B664 File Offset: 0x00009864
		[DataMember]
		public int TemplateId { get; set; }
	}
}
