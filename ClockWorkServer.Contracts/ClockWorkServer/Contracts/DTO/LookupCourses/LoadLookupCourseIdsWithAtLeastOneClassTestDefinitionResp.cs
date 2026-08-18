using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D0 RID: 2000
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionResp
	{
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000135BF File Offset: 0x000117BF
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x000135C7 File Offset: 0x000117C7
		[DataMember]
		public IList<int> Lucids { get; set; }
	}
}
