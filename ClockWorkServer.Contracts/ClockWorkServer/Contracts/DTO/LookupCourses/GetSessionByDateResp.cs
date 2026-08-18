using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D8 RID: 2008
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSessionByDateResp
	{
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x00013647 File Offset: 0x00011847
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x0001364F File Offset: 0x0001184F
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
