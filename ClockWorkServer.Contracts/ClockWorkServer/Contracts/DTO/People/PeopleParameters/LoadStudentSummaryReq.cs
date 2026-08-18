using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E9 RID: 1001
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentSummaryReq : BaseMessageReq
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0000A33B File Offset: 0x0000853B
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0000A343 File Offset: 0x00008543
		[DataMember]
		public int PersonId { get; set; }
	}
}
