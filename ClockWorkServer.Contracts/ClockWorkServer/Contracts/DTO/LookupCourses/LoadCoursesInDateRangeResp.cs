using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D6 RID: 2006
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesInDateRangeResp
	{
		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x00013625 File Offset: 0x00011825
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x0001362D File Offset: 0x0001182D
		[DataMember]
		public IList<LookupCourseBaseDTO> CourseBases { get; set; }
	}
}
