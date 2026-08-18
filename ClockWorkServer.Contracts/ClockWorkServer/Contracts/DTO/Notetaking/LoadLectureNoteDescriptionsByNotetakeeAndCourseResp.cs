using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000434 RID: 1076
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsByNotetakeeAndCourseResp
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0000AC5D File Offset: 0x00008E5D
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x0000AC65 File Offset: 0x00008E65
		[DataMember]
		public List<LectureNoteDescriptionDTO> LectureNoteDescriptions { get; set; }
	}
}
