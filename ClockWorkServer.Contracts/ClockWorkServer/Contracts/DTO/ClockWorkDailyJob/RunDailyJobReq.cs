using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x02000887 RID: 2183
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunDailyJobReq : BaseReportMessageReq
	{
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x00014EF8 File Offset: 0x000130F8
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x00014F00 File Offset: 0x00013100
		[DataMember]
		public int GroupId { get; set; }
	}
}
