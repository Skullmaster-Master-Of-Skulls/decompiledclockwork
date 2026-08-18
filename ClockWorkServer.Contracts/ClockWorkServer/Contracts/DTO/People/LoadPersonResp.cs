using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200037B RID: 891
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonResp
	{
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x000099EA File Offset: 0x00007BEA
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x000099F2 File Offset: 0x00007BF2
		[DataMember]
		public PersonBaseDTO Person { get; set; }
	}
}
