using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200047A RID: 1146
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationSingleLetterReq : BaseReportMessageReq
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0000B67E File Offset: 0x0000987E
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x0000B686 File Offset: 0x00009886
		[DataMember]
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0000B68F File Offset: 0x0000988F
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x0000B697 File Offset: 0x00009897
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0000B6A0 File Offset: 0x000098A0
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x0000B6A8 File Offset: 0x000098A8
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0000B6B1 File Offset: 0x000098B1
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x0000B6B9 File Offset: 0x000098B9
		[DataMember]
		public int TemplateId { get; set; }
	}
}
