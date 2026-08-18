using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000444 RID: 1092
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteResp
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0000ADC2 File Offset: 0x00008FC2
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x0000ADCA File Offset: 0x00008FCA
		[DataMember]
		public IList<DownloadedLectureNoteDTO> DownloadedLectureNotes { get; set; }
	}
}
