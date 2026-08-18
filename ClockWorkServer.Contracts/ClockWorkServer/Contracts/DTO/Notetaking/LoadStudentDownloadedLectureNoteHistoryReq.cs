using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000441 RID: 1089
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentDownloadedLectureNoteHistoryReq : BaseReportMessageReq
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0000AD6D File Offset: 0x00008F6D
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x0000AD75 File Offset: 0x00008F75
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x0000AD7E File Offset: 0x00008F7E
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x0000AD86 File Offset: 0x00008F86
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
