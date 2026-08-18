using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B1 RID: 1969
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddSessionResp
	{
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x0001334A File Offset: 0x0001154A
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x00013352 File Offset: 0x00011552
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
