using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000431 RID: 1073
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerBaseByNotetakeeAndCourseReq : BaseReportMessageReq
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0000AC08 File Offset: 0x00008E08
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x0000AC10 File Offset: 0x00008E10
		[DataMember]
		public int NotetakeePersonId { get; set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0000AC19 File Offset: 0x00008E19
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x0000AC21 File Offset: 0x00008E21
		[DataMember]
		public int NotetakeeLuCourseId { get; set; }
	}
}
