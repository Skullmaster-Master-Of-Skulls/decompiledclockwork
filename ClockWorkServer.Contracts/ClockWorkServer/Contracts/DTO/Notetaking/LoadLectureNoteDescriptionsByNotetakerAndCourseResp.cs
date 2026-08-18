using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000436 RID: 1078
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsByNotetakerAndCourseResp
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0000AC90 File Offset: 0x00008E90
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x0000AC98 File Offset: 0x00008E98
		[DataMember]
		public List<LectureNoteDescriptionDTO> LectureNoteDescriptions { get; set; }
	}
}
