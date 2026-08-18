using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x0200040F RID: 1039
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueFormsWithOpenItemsCountReq : BaseMessageReq
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0000A7BF File Offset: 0x000089BF
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x0000A7C7 File Offset: 0x000089C7
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0000A7D0 File Offset: 0x000089D0
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x0000A7D8 File Offset: 0x000089D8
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0000A7E1 File Offset: 0x000089E1
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x0000A7E9 File Offset: 0x000089E9
		[DataMember]
		public int FilterByAssignedCounsellorPid { get; set; }
	}
}
