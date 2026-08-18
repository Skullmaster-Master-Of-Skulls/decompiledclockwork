using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200043A RID: 1082
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMatchingNotetakersWithLectureNoteUploadsByCourseResp
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x0000ACDC File Offset: 0x00008EDC
		[DataMember]
		public List<NotetakerBaseWithLookupCourseBaseDTO> Notetakers { get; set; }
	}
}
