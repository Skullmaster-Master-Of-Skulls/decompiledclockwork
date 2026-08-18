using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200047C RID: 1148
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationLetterReq : BaseReportMessageReq
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0000B6D3 File Offset: 0x000098D3
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x0000B6DB File Offset: 0x000098DB
		[DataMember]
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0000B6E4 File Offset: 0x000098E4
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0000B6EC File Offset: 0x000098EC
		[DataMember]
		public MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary { get; set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0000B6F5 File Offset: 0x000098F5
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x0000B6FD File Offset: 0x000098FD
		[DataMember]
		public eFileFormatDTO OutputFileFormat { get; set; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0000B706 File Offset: 0x00009906
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x0000B70E File Offset: 0x0000990E
		[DataMember]
		public int TemplateId { get; set; }
	}
}
