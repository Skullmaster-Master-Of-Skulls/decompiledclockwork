using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D3 RID: 2003
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCourseDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x000135F2 File Offset: 0x000117F2
		// (set) Token: 0x060028EB RID: 10475 RVA: 0x000135FA File Offset: 0x000117FA
		[DataMember]
		public LookupCourseDateRangeDTO OldDateRange { get; set; }

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x00013603 File Offset: 0x00011803
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x0001360B File Offset: 0x0001180B
		[DataMember]
		public LookupCourseDateRangeDTO NewDateRange { get; set; }
	}
}
