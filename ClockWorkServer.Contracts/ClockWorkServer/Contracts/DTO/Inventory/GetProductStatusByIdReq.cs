using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005AC RID: 1452
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductStatusByIdReq : BaseMessageReq
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0000DAD9 File Offset: 0x0000BCD9
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x0000DAE1 File Offset: 0x0000BCE1
		[DataMember]
		public int ProductStatusId { get; set; }
	}
}
