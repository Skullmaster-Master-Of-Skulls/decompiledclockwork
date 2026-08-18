using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007EE RID: 2030
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveInstructorReq : BaseMessageReq
	{
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x00013A77 File Offset: 0x00011C77
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x00013A7F File Offset: 0x00011C7F
		[DataMember]
		public LookupInstructorDTO Instructor { get; set; }
	}
}
