using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000481 RID: 1153
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeDocFromDocumentResp
	{
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0000B78E File Offset: 0x0000998E
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0000B796 File Offset: 0x00009996
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
