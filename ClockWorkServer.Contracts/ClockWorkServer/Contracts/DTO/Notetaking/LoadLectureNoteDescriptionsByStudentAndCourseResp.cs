using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200042E RID: 1070
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsByStudentAndCourseResp
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x0000ABDD File Offset: 0x00008DDD
		[DataMember]
		public List<LectureNoteDescriptionDTO> LectureNoteDescriptions { get; set; }
	}
}
