using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007EB RID: 2027
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorByEmailResp
	{
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x00013A44 File Offset: 0x00011C44
		// (set) Token: 0x0600295F RID: 10591 RVA: 0x00013A4C File Offset: 0x00011C4C
		[DataMember]
		public LookupInstructorDTO Instructor { get; set; }
	}
}
