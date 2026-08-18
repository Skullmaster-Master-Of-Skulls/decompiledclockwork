using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E8 RID: 2024
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorByUsernameReq : BaseMessageReq
	{
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x00013A11 File Offset: 0x00011C11
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x00013A19 File Offset: 0x00011C19
		[DataMember]
		public string Username { get; set; }
	}
}
