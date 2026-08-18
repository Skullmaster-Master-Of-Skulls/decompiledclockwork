using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F1 RID: 2033
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveInstructorsForCourseResp
	{
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x00013ACC File Offset: 0x00011CCC
		// (set) Token: 0x06002975 RID: 10613 RVA: 0x00013AD4 File Offset: 0x00011CD4
		[DataMember]
		public List<LookupInstructorDTO> Instructors { get; set; }
	}
}
