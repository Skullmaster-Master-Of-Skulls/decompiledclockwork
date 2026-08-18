using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000395 RID: 917
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnDeleteUserResp
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00009BB6 File Offset: 0x00007DB6
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x00009BBE File Offset: 0x00007DBE
		[DataMember]
		public PersonBaseDTO User { get; set; }
	}
}
