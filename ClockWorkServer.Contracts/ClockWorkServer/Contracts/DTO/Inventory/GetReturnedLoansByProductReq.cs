using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000550 RID: 1360
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByProductReq : BaseMessageReq
	{
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0000CEE7 File Offset: 0x0000B0E7
		// (set) Token: 0x06001C3A RID: 7226 RVA: 0x0000CEEF File Offset: 0x0000B0EF
		[DataMember]
		public string ProductUniqueId { get; set; }
	}
}
