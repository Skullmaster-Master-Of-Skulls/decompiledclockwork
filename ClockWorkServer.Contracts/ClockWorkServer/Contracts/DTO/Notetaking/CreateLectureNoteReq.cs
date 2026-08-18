using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000445 RID: 1093
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLectureNoteReq : BaseReportMessageReq
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0000ADD3 File Offset: 0x00008FD3
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x0000ADDB File Offset: 0x00008FDB
		[DataMember]
		public LectureNoteDTO LectureNote { get; set; }
	}
}
