using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D2 RID: 2002
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUniqueCourseDateRangesBySessionResp
	{
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000135E1 File Offset: 0x000117E1
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x000135E9 File Offset: 0x000117E9
		[DataMember]
		public IList<LookupCourseDateRangeDTO> UniqueDateRanges { get; set; }
	}
}
