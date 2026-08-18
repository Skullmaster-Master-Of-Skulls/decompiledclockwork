using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000C3 RID: 195
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCurrentInstanceResp
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000025C3 File Offset: 0x000007C3
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x000025CB File Offset: 0x000007CB
		[DataMember]
		public string InstanceName { get; set; }
	}
}
