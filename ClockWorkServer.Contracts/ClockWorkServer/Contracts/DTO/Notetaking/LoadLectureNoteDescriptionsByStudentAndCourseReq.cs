using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200042D RID: 1069
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsByStudentAndCourseReq : BaseReportMessageReq
	{
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0000ABBB File Offset: 0x00008DBB
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0000ABCC File Offset: 0x00008DCC
		[DataMember]
		public int StudentLuCourseId { get; set; }
	}
}
