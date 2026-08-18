using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D5 RID: 2005
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesInDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x00013614 File Offset: 0x00011814
		// (set) Token: 0x060028F1 RID: 10481 RVA: 0x0001361C File Offset: 0x0001181C
		[DataMember]
		public LookupCourseDateRangeDTO DateRange { get; set; }
	}
}
