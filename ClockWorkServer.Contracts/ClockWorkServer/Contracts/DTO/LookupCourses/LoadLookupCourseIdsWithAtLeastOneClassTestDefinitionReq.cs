using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007CF RID: 1999
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq : BaseMessageReq
	{
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x0001358C File Offset: 0x0001178C
		// (set) Token: 0x060028DB RID: 10459 RVA: 0x00013594 File Offset: 0x00011794
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x0001359D File Offset: 0x0001179D
		// (set) Token: 0x060028DD RID: 10461 RVA: 0x000135A5 File Offset: 0x000117A5
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000135AE File Offset: 0x000117AE
		// (set) Token: 0x060028DF RID: 10463 RVA: 0x000135B6 File Offset: 0x000117B6
		[DataMember]
		public IList<int> LuCourseIds { get; set; }
	}
}
