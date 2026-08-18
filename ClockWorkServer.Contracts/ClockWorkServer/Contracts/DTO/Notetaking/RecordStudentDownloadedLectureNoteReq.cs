using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000440 RID: 1088
	[DataContract(Namespace = "http://tpro.ca")]
	public class RecordStudentDownloadedLectureNoteReq : BaseReportMessageReq
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0000AD4B File Offset: 0x00008F4B
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x0000AD53 File Offset: 0x00008F53
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0000AD5C File Offset: 0x00008F5C
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x0000AD64 File Offset: 0x00008F64
		[DataMember]
		public int NotetakerDocumentId { get; set; }
	}
}
