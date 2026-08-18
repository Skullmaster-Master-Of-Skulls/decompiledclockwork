using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000449 RID: 1097
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteLectureNoteReq : BaseReportMessageReq
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0000AE06 File Offset: 0x00009006
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x0000AE0E File Offset: 0x0000900E
		[DataMember]
		public int NotetakerDocumentId { get; set; }
	}
}
