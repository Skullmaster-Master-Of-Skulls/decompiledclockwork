using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000800 RID: 2048
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectsBySessionResp
	{
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x00013D81 File Offset: 0x00011F81
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x00013D89 File Offset: 0x00011F89
		[DataMember]
		public List<LookupSubjectDTO> Subjects { get; set; }
	}
}
