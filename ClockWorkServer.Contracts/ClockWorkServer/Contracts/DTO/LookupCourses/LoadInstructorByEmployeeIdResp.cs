using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E7 RID: 2023
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorByEmployeeIdResp
	{
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x00013A00 File Offset: 0x00011C00
		// (set) Token: 0x06002953 RID: 10579 RVA: 0x00013A08 File Offset: 0x00011C08
		[DataMember]
		public LookupInstructorDTO Instructor { get; set; }
	}
}
