using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000116 RID: 278
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateVetsBenefitApplicationNextSemesterResp
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000030E5 File Offset: 0x000012E5
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x000030ED File Offset: 0x000012ED
		[DataMember]
		public Guid? NewBenefitApplicationId { get; set; }
	}
}
