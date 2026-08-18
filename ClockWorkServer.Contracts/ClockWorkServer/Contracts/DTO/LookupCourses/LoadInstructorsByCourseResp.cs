using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E1 RID: 2017
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorsByCourseResp
	{
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x00013967 File Offset: 0x00011B67
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x0001396F File Offset: 0x00011B6F
		[DataMember]
		public IList<LookupInstructorDTO> Instructors { get; set; }
	}
}
