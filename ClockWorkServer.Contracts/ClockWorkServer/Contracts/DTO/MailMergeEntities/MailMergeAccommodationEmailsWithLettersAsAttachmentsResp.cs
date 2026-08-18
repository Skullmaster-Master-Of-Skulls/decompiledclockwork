using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000475 RID: 1141
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationEmailsWithLettersAsAttachmentsResp
	{
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0000B5C3 File Offset: 0x000097C3
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x0000B5CB File Offset: 0x000097CB
		[DataMember]
		public IDictionary<int, TPMailMessageDTO> Emails { get; set; }
	}
}
