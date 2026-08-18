using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000483 RID: 1155
	public class MailMergeMultipleItemsToOneDocumentResp
	{
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0000B7D2 File Offset: 0x000099D2
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x0000B7DA File Offset: 0x000099DA
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
