using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000113 RID: 275
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateVetsBenefitApplicationCurrentSemesterReq : BaseMessageReq
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000030B2 File Offset: 0x000012B2
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x000030BA File Offset: 0x000012BA
		[DataMember]
		public int PersonId { get; set; }
	}
}
