using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A8 RID: 1960
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseBasesBySearchStringResp
	{
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0001326D File Offset: 0x0001146D
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x00013275 File Offset: 0x00011475
		[DataMember]
		public IList<LookupCourseBaseDTO> CourseBases { get; set; }
	}
}
