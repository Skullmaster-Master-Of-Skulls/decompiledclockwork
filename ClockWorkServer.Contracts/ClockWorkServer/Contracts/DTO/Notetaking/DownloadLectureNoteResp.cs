using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000428 RID: 1064
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadLectureNoteResp
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0000AB6F File Offset: 0x00008D6F
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x0000AB77 File Offset: 0x00008D77
		[DataMember]
		public LectureNoteDTO LectureNote { get; set; }
	}
}
