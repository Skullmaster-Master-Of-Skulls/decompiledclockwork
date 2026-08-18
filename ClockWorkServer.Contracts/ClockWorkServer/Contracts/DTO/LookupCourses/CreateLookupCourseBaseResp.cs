using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A6 RID: 1958
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLookupCourseBaseResp
	{
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x00013229 File Offset: 0x00011429
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x00013231 File Offset: 0x00011431
		[DataMember]
		public LookupCourseDTO NewCourse { get; set; }
	}
}
