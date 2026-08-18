using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000791 RID: 1937
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignAlternateContactToCourseReq : BaseMessageReq
	{
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x00012C63 File Offset: 0x00010E63
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x00012C6B File Offset: 0x00010E6B
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x00012C74 File Offset: 0x00010E74
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x00012C7C File Offset: 0x00010E7C
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
