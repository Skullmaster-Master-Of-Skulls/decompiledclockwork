using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E9 RID: 2025
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorByUsernameResp
	{
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x00013A22 File Offset: 0x00011C22
		// (set) Token: 0x06002959 RID: 10585 RVA: 0x00013A2A File Offset: 0x00011C2A
		[DataMember]
		public LookupInstructorDTO Instructor { get; set; }
	}
}
