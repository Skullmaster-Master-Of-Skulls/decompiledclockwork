using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B3 RID: 1971
	[DataContract(Namespace = "http://tpro.ca")]
	public class SubtractSessionResp
	{
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x0001337D File Offset: 0x0001157D
		// (set) Token: 0x06002881 RID: 10369 RVA: 0x00013385 File Offset: 0x00011585
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
