using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000112 RID: 274
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateVetsBenefitApplicationResp
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x000030A1 File Offset: 0x000012A1
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x000030A9 File Offset: 0x000012A9
		[DataMember]
		public Guid? NewBenefitApplicationId { get; set; }
	}
}
