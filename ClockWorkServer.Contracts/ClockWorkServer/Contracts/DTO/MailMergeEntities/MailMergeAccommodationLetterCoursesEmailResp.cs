using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200048B RID: 1163
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationLetterCoursesEmailResp
	{
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0000B926 File Offset: 0x00009B26
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x0000B92E File Offset: 0x00009B2E
		[DataMember]
		public Dictionary<int, TPMailMessageDTO> EmailsWithLucids { get; set; }
	}
}
