using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000437 RID: 1079
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteByIdReq : BaseReportMessageReq
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		[DataMember]
		public int NotetakerDocumentId { get; set; }
	}
}
