using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x0200088B RID: 2187
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateDailyJobTaskReq : BaseReportMessageReq
	{
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x00014F3C File Offset: 0x0001313C
		// (set) Token: 0x06002C47 RID: 11335 RVA: 0x00014F44 File Offset: 0x00013144
		[DataMember]
		public DailyJobTaskDTO DailyJobTask { get; set; }
	}
}
