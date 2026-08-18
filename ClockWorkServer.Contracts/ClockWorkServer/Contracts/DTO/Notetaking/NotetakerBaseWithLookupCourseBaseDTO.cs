using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200041A RID: 1050
	[DataContract(Namespace = "http://tpro.ca")]
	public class NotetakerBaseWithLookupCourseBaseDTO
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0000AA92 File Offset: 0x00008C92
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x0000AA9A File Offset: 0x00008C9A
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0000AAA3 File Offset: 0x00008CA3
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x0000AAAB File Offset: 0x00008CAB
		[DataMember]
		public NotetakerBaseDTO Notetaker { get; set; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x0000AABC File Offset: 0x00008CBC
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }
	}
}
