using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000408 RID: 1032
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteOnlineFormQueueItemResp
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0000A748 File Offset: 0x00008948
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x0000A750 File Offset: 0x00008950
		[DataMember]
		public bool CompletedSuccessfully { get; set; }
	}
}
