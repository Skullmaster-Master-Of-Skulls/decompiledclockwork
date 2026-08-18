using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A3 RID: 1955
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupCourseDisplayOptionsDTO
	{
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x00013173 File Offset: 0x00011373
		// (set) Token: 0x06002839 RID: 10297 RVA: 0x0001317B File Offset: 0x0001137B
		public bool IncludeSubjectLongIfAvailable { get; set; }
	}
}
