using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000443 RID: 1091
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNoteReq : BaseReportMessageReq
	{
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0000ADB1 File Offset: 0x00008FB1
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x0000ADB9 File Offset: 0x00008FB9
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
