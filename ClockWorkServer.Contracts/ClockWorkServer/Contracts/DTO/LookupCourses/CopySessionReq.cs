using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007BA RID: 1978
	[DataContract(Namespace = "http://tpro.ca")]
	public class CopySessionReq : BaseMessageReq
	{
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x00013405 File Offset: 0x00011605
		// (set) Token: 0x06002898 RID: 10392 RVA: 0x0001340D File Offset: 0x0001160D
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
