using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Startup
{
	// Token: 0x02000264 RID: 612
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkClientStartupResp
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00006A62 File Offset: 0x00004C62
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x00006A6A File Offset: 0x00004C6A
		[DataMember]
		public ClockWorkClientStartupDTO StartupValues { get; set; }
	}
}
