using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x0200089F RID: 2207
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCaseByIdResp
	{
		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x00015331 File Offset: 0x00013531
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x00015339 File Offset: 0x00013539
		[DataMember]
		public CaseDTO Case { get; set; }
	}
}
