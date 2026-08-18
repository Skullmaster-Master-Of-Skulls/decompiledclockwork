using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000433 RID: 1075
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsByNotetakeeAndCourseReq : BaseReportMessageReq
	{
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0000AC3B File Offset: 0x00008E3B
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x0000AC43 File Offset: 0x00008E43
		[DataMember]
		public int NotetakeePersonId { get; set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0000AC4C File Offset: 0x00008E4C
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x0000AC54 File Offset: 0x00008E54
		[DataMember]
		public int NotetakeeLuCourseId { get; set; }
	}
}
