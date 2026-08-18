using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E3 RID: 2019
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorsBySearchStringResp
	{
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x00013989 File Offset: 0x00011B89
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x00013991 File Offset: 0x00011B91
		[DataMember]
		public IList<LookupInstructorDTO> Instructors { get; set; }
	}
}
