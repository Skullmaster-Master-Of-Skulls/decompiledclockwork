using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007ED RID: 2029
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorResp
	{
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x00013A66 File Offset: 0x00011C66
		// (set) Token: 0x06002965 RID: 10597 RVA: 0x00013A6E File Offset: 0x00011C6E
		[DataMember]
		public LookupInstructorDTO Instructor { get; set; }
	}
}
