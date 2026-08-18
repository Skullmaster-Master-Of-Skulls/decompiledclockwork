using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B0 RID: 1968
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddSessionReq : BaseMessageReq
	{
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x00013328 File Offset: 0x00011528
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x00013330 File Offset: 0x00011530
		[DataMember]
		public SessionDTO Session { get; set; }

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x00013339 File Offset: 0x00011539
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x00013341 File Offset: 0x00011541
		[DataMember]
		public int Count { get; set; }
	}
}
