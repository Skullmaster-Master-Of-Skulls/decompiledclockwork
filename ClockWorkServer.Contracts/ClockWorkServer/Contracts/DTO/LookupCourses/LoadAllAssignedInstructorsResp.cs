using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F5 RID: 2037
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAssignedInstructorsResp
	{
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x00013B54 File Offset: 0x00011D54
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x00013B5C File Offset: 0x00011D5C
		[DataMember]
		public List<LookupInstructorDTO> Instructors { get; set; }
	}
}
