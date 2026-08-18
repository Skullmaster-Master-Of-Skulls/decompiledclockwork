using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000489 RID: 1161
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeAccommodationSingleLetterEmailResp
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0000B8E2 File Offset: 0x00009AE2
		// (set) Token: 0x060018F7 RID: 6391 RVA: 0x0000B8EA File Offset: 0x00009AEA
		[DataMember]
		public TPMailMessageDTO Email { get; set; }
	}
}
