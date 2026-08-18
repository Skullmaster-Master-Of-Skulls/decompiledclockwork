using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000106 RID: 262
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBenefitApplicationByIdResp
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00002EB4 File Offset: 0x000010B4
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00002EBC File Offset: 0x000010BC
		[DataMember]
		public VetsBenefitApplicationDTO Application { get; set; }
	}
}
