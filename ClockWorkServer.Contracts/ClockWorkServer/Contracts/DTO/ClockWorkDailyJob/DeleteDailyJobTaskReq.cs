using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x02000891 RID: 2193
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteDailyJobTaskReq : BaseReportMessageReq
	{
		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x00014FB3 File Offset: 0x000131B3
		// (set) Token: 0x06002C5B RID: 11355 RVA: 0x00014FBB File Offset: 0x000131BB
		[DataMember]
		public int WindowsTaskJobId { get; set; }
	}
}
