using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007BB RID: 1979
	[DataContract(Namespace = "http://tpro.ca")]
	public class CopySessionResp
	{
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x00013416 File Offset: 0x00011616
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x0001341E File Offset: 0x0001161E
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
