using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200047F RID: 1151
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeDocFromTemplateResp
	{
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0000B74A File Offset: 0x0000994A
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0000B752 File Offset: 0x00009952
		[DataMember]
		public BinaryFileDTO Document { get; set; }
	}
}
