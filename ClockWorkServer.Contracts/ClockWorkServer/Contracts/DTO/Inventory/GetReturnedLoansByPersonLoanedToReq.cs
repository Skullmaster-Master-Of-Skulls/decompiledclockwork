using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000554 RID: 1364
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByPersonLoanedToReq : BaseMessageReq
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0000CF5E File Offset: 0x0000B15E
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x0000CF66 File Offset: 0x0000B166
		[DataMember]
		public int PersonLoanedToId { get; set; }
	}
}
