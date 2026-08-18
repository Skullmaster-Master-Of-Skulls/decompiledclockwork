using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000804 RID: 2052
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveSubjectResp
	{
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x00013DC5 File Offset: 0x00011FC5
		// (set) Token: 0x060029D5 RID: 10709 RVA: 0x00013DCD File Offset: 0x00011FCD
		[DataMember]
		public int SubjectId { get; set; }
	}
}
