using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E8 RID: 1000
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentSummaryResp
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0000A32A File Offset: 0x0000852A
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0000A332 File Offset: 0x00008532
		[DataMember]
		public StudentSummaryDTO StudentSummary { get; set; }
	}
}
