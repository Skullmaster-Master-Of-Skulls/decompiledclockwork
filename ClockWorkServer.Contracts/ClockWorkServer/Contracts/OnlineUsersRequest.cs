using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000AF RID: 175
	[DataContract(Namespace = "http://tpro.ca")]
	public class OnlineUsersRequest
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x000023A1 File Offset: 0x000005A1
		[DataMember]
		public string Role { get; set; }
	}
}
