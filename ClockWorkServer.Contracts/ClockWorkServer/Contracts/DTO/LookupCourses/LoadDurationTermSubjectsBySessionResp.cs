using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007CA RID: 1994
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDurationTermSubjectsBySessionResp
	{
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x00013504 File Offset: 0x00011704
		// (set) Token: 0x060028C6 RID: 10438 RVA: 0x0001350C File Offset: 0x0001170C
		[DataMember]
		public IList<LookupDurationTermSubjectDTO> DurationTermSubjects { get; set; }
	}
}
