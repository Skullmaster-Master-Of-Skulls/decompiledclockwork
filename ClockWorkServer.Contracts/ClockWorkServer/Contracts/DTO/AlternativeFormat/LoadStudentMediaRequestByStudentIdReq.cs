using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C34 RID: 3124
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentMediaRequestByStudentIdReq : BaseReportMessageReq
	{
		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x0002001B File Offset: 0x0001E21B
		// (set) Token: 0x06004169 RID: 16745 RVA: 0x00020023 File Offset: 0x0001E223
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x0002002C File Offset: 0x0001E22C
		// (set) Token: 0x0600416B RID: 16747 RVA: 0x00020034 File Offset: 0x0001E234
		[DataMember]
		public int CampusId { get; set; }
	}
}
