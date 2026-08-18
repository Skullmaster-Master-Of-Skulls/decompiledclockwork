using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200053E RID: 1342
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoansByDueDateInLessThanReq : BaseMessageReq
	{
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0000CDF9 File Offset: 0x0000AFF9
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0000CE01 File Offset: 0x0000B001
		[DataMember]
		public TimeSpan DueDateIn { get; set; }
	}
}
