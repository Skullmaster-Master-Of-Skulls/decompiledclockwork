using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x0200088C RID: 2188
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeTaskActiveStatusReq : BaseReportMessageReq
	{
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x00014F4D File Offset: 0x0001314D
		// (set) Token: 0x06002C4A RID: 11338 RVA: 0x00014F55 File Offset: 0x00013155
		[DataMember]
		public int WindowsTaskJobId { get; set; }

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x00014F5E File Offset: 0x0001315E
		// (set) Token: 0x06002C4C RID: 11340 RVA: 0x00014F66 File Offset: 0x00013166
		[DataMember]
		public bool NewIsActive { get; set; }
	}
}
