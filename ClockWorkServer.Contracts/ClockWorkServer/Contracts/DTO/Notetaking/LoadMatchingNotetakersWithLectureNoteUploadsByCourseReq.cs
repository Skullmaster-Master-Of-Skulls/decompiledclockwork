using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000439 RID: 1081
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMatchingNotetakersWithLectureNoteUploadsByCourseReq : BaseReportMessageReq
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x0000ACCB File Offset: 0x00008ECB
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
