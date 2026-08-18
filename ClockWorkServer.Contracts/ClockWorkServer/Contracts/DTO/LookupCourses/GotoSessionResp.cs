using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B7 RID: 1975
	[DataContract(Namespace = "http://tpro.ca")]
	public class GotoSessionResp
	{
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x000133E3 File Offset: 0x000115E3
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x000133EB File Offset: 0x000115EB
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
