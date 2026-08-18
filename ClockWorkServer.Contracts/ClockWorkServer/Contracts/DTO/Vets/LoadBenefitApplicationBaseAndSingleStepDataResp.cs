using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000108 RID: 264
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBenefitApplicationBaseAndSingleStepDataResp
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00002EE7 File Offset: 0x000010E7
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00002EEF File Offset: 0x000010EF
		[DataMember]
		public VetsBenefitApplicationDTO Application { get; set; }
	}
}
