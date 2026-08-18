using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D1 RID: 2001
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUniqueCourseDateRangesBySessionReq : BaseMessageReq
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000135D0 File Offset: 0x000117D0
		// (set) Token: 0x060028E5 RID: 10469 RVA: 0x000135D8 File Offset: 0x000117D8
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}
