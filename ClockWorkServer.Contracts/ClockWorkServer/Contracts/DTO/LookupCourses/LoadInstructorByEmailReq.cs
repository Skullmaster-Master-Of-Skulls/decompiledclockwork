using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007EA RID: 2026
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorByEmailReq : BaseMessageReq
	{
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x00013A33 File Offset: 0x00011C33
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x00013A3B File Offset: 0x00011C3B
		[DataMember]
		public string Email { get; set; }
	}
}
