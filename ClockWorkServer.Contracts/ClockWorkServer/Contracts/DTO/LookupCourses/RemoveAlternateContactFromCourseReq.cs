using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000792 RID: 1938
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveAlternateContactFromCourseReq : BaseMessageReq
	{
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x00012C85 File Offset: 0x00010E85
		// (set) Token: 0x060027E2 RID: 10210 RVA: 0x00012C8D File Offset: 0x00010E8D
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x00012C96 File Offset: 0x00010E96
		// (set) Token: 0x060027E4 RID: 10212 RVA: 0x00012C9E File Offset: 0x00010E9E
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
