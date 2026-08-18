using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000442 RID: 1090
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentDownloadedLectureNoteHistoryResp
	{
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0000AD8F File Offset: 0x00008F8F
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x0000AD97 File Offset: 0x00008F97
		[DataMember]
		public IList<DownloadedLectureNoteDTO> DownloadedLectureNotes { get; set; }
	}
}
