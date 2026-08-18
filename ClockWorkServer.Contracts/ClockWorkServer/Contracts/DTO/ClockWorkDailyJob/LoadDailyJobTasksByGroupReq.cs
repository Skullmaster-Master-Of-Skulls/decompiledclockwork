using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x0200088D RID: 2189
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDailyJobTasksByGroupReq : BaseReportMessageReq
	{
		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x00014F6F File Offset: 0x0001316F
		// (set) Token: 0x06002C4F RID: 11343 RVA: 0x00014F77 File Offset: 0x00013177
		[DataMember]
		public int TaskGroupId { get; set; }
	}
}
