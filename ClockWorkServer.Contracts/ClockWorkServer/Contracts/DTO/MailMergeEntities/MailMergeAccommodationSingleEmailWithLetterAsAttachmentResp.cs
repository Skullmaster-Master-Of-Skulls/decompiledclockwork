using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000477 RID: 1143
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationSingleEmailWithLetterAsAttachmentResp
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0000B618 File Offset: 0x00009818
		// (set) Token: 0x06001891 RID: 6289 RVA: 0x0000B620 File Offset: 0x00009820
		[DataMember]
		public TPMailMessageDTO Email { get; set; }
	}
}
