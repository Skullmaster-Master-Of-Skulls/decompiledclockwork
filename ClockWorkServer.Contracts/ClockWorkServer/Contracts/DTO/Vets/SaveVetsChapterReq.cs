using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000109 RID: 265
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveVetsChapterReq : BaseMessageReq
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00002EF8 File Offset: 0x000010F8
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00002F00 File Offset: 0x00001100
		[DataMember]
		public Guid BenefitApplicationId { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00002F09 File Offset: 0x00001109
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00002F11 File Offset: 0x00001111
		[DataMember]
		public Guid ChapterId { get; set; }
	}
}
