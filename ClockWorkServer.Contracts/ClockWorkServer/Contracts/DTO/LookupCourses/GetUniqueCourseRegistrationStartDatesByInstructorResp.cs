using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F7 RID: 2039
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUniqueCourseRegistrationStartDatesByInstructorResp
	{
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x00013B76 File Offset: 0x00011D76
		// (set) Token: 0x0600298F RID: 10639 RVA: 0x00013B7E File Offset: 0x00011D7E
		[DataMember]
		public IList<DateTime> Dates { get; set; }
	}
}
