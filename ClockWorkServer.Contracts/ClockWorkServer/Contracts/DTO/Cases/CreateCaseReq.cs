using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x020008A0 RID: 2208
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCaseReq : BaseMessageReq
	{
		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x00015342 File Offset: 0x00013542
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x0001534A File Offset: 0x0001354A
		[DataMember]
		public CaseDTO Case { get; set; }
	}
}
