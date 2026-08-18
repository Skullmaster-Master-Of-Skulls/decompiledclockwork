using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200048A RID: 1162
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationSingleLetterEmailReq : BaseReportMessageReq
	{
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0000B8F3 File Offset: 0x00009AF3
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0000B8FB File Offset: 0x00009AFB
		[DataMember]
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0000B904 File Offset: 0x00009B04
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x0000B90C File Offset: 0x00009B0C
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0000B915 File Offset: 0x00009B15
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x0000B91D File Offset: 0x00009B1D
		[DataMember]
		public int TemplateId { get; set; }
	}
}
