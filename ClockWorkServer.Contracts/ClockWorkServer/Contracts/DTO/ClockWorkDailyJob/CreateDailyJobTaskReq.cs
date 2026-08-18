using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x02000889 RID: 2185
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateDailyJobTaskReq : BaseReportMessageReq
	{
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x00014F1A File Offset: 0x0001311A
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x00014F22 File Offset: 0x00013122
		[DataMember]
		public DailyJobTaskDTO Task { get; set; }
	}
}
