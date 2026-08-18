using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000435 RID: 1077
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsByNotetakerAndCourseReq : BaseReportMessageReq
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0000AC6E File Offset: 0x00008E6E
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x0000AC76 File Offset: 0x00008E76
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0000AC7F File Offset: 0x00008E7F
		// (set) Token: 0x06001744 RID: 5956 RVA: 0x0000AC87 File Offset: 0x00008E87
		[DataMember]
		public int NotetakerLuCourseId { get; set; }
	}
}
