using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B5 RID: 1973
	[DataContract(Namespace = "http://tpro.ca")]
	public class GoToTodaysSessionResp
	{
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x0001339F File Offset: 0x0001159F
		// (set) Token: 0x06002887 RID: 10375 RVA: 0x000133A7 File Offset: 0x000115A7
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
