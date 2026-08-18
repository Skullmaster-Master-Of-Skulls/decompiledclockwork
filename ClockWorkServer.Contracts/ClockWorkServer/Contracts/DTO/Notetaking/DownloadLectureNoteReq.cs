using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000427 RID: 1063
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadLectureNoteReq : BaseReportMessageReq
	{
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0000AB5E File Offset: 0x00008D5E
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0000AB66 File Offset: 0x00008D66
		[DataMember]
		public int NotetakerDocumentId { get; set; }
	}
}
