using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007AF RID: 1967
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesBySubjectAndSessionResp
	{
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x00013317 File Offset: 0x00011517
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x0001331F File Offset: 0x0001151F
		[DataMember]
		public List<LookupCourseDTO> Courses { get; set; }
	}
}
