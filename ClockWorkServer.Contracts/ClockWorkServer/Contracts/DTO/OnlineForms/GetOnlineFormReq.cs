using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F4 RID: 1012
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetOnlineFormReq : BaseMessageReq
	{
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0000A58E File Offset: 0x0000878E
		// (set) Token: 0x06001634 RID: 5684 RVA: 0x0000A596 File Offset: 0x00008796
		[DataMember]
		public int OnlineFormId { get; set; }
	}
}
