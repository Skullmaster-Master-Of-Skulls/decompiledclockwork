using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003C1 RID: 961
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentWithCommonInfoDTO
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0000A082 File Offset: 0x00008282
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x0000A08A File Offset: 0x0000828A
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0000A093 File Offset: 0x00008293
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x0000A09B File Offset: 0x0000829B
		[DataMember]
		public StudentCommonInfoDTO CommonInfo { get; set; }
	}
}
