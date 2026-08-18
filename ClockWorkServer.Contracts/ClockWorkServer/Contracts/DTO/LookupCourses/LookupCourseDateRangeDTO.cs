using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A2 RID: 1954
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupCourseDateRangeDTO
	{
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x00013140 File Offset: 0x00011340
		// (set) Token: 0x06002832 RID: 10290 RVA: 0x00013148 File Offset: 0x00011348
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x00013151 File Offset: 0x00011351
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x00013159 File Offset: 0x00011359
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x00013162 File Offset: 0x00011362
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x0001316A File Offset: 0x0001136A
		[DataMember]
		public int CourseCount { get; set; }
	}
}
