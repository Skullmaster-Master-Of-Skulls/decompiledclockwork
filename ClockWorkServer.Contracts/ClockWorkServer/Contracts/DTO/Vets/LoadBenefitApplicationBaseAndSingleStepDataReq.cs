using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000107 RID: 263
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBenefitApplicationBaseAndSingleStepDataReq : BaseMessageReq
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00002EC5 File Offset: 0x000010C5
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00002ECD File Offset: 0x000010CD
		[DataMember]
		public Guid BenefitApplicationId { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00002ED6 File Offset: 0x000010D6
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00002EDE File Offset: 0x000010DE
		[DataMember]
		public eVetsBenefitApplicationStep? PreferredStep { get; set; }
	}
}
