using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000115 RID: 277
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateVetsBenefitApplicationNextSemesterReq : BaseMessageReq
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000030D4 File Offset: 0x000012D4
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x000030DC File Offset: 0x000012DC
		[DataMember]
		public int PersonId { get; set; }
	}
}
