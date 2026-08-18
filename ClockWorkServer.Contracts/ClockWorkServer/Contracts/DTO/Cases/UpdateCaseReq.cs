using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x020008A3 RID: 2211
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCaseReq : BaseMessageReq
	{
		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00015375 File Offset: 0x00013575
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x0001537D File Offset: 0x0001357D
		[DataMember]
		public CaseDTO Case { get; set; }
	}
}
