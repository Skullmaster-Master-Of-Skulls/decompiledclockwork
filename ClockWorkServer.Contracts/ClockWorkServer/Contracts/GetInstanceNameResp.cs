using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000C5 RID: 197
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetInstanceNameResp
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x000025DC File Offset: 0x000007DC
		[DataMember]
		public IList<string> InstanceNames { get; set; }
	}
}
