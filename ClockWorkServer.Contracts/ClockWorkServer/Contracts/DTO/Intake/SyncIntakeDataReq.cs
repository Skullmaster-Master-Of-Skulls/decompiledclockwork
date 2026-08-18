using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005D4 RID: 1492
	[DataContract(Namespace = "http://tpro.ca")]
	public class SyncIntakeDataReq : BaseMessageReq
	{
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0000DE80 File Offset: 0x0000C080
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x0000DE88 File Offset: 0x0000C088
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0000DE91 File Offset: 0x0000C091
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0000DE99 File Offset: 0x0000C099
		[DataMember]
		public bool RemoveIntakeWhenDone { get; set; }
	}
}
