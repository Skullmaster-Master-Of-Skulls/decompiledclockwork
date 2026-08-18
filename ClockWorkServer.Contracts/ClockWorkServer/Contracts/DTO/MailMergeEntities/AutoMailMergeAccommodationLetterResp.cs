using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000486 RID: 1158
	public class AutoMailMergeAccommodationLetterResp
	{
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0000B827 File Offset: 0x00009A27
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x0000B82F File Offset: 0x00009A2F
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
