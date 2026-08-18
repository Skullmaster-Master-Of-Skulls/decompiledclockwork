using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200048C RID: 1164
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationLetterCoursesEmailReq : BaseReportMessageReq
	{
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0000B937 File Offset: 0x00009B37
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x0000B93F File Offset: 0x00009B3F
		[DataMember]
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0000B948 File Offset: 0x00009B48
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x0000B950 File Offset: 0x00009B50
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0000B959 File Offset: 0x00009B59
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0000B961 File Offset: 0x00009B61
		[DataMember]
		public int TemplateId { get; set; }
	}
}
