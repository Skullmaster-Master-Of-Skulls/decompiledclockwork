using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200044E RID: 1102
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUniqueStudentsReceivingNotesReq : BaseReportMessageReq
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0000AE7D File Offset: 0x0000907D
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x0000AE85 File Offset: 0x00009085
		[DataMember]
		public int NotetakerId { get; set; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0000AE8E File Offset: 0x0000908E
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x0000AE96 File Offset: 0x00009096
		[DataMember]
		public int NotetakerLuCourseId { get; set; }
	}
}
