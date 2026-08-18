using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000114 RID: 276
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateVetsBenefitApplicationCurrentSemesterResp
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x000030C3 File Offset: 0x000012C3
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x000030CB File Offset: 0x000012CB
		[DataMember]
		public Guid? NewBenefitApplicationId { get; set; }
	}
}
