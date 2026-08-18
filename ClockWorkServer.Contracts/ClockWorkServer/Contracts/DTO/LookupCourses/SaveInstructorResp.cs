using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007EF RID: 2031
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveInstructorResp
	{
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x00013A88 File Offset: 0x00011C88
		// (set) Token: 0x0600296B RID: 10603 RVA: 0x00013A90 File Offset: 0x00011C90
		[DataMember]
		public int InstructorId { get; set; }
	}
}
