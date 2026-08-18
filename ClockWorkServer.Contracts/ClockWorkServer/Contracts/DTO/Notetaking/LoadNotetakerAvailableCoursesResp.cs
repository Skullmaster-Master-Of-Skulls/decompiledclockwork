using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200044D RID: 1101
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerAvailableCoursesResp
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0000AE6C File Offset: 0x0000906C
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x0000AE74 File Offset: 0x00009074
		[DataMember]
		public IList<LookupCourseBaseDTO> CourseBases { get; set; }
	}
}
