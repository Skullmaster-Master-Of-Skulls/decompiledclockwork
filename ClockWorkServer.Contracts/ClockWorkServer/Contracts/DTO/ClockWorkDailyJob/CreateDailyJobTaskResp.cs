using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x0200088A RID: 2186
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateDailyJobTaskResp
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x00014F2B File Offset: 0x0001312B
		// (set) Token: 0x06002C44 RID: 11332 RVA: 0x00014F33 File Offset: 0x00013133
		[DataMember]
		public int WindowsJobTaskId { get; set; }
	}
}
