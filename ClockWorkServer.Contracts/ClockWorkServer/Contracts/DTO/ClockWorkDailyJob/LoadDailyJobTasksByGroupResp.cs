using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x0200088E RID: 2190
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDailyJobTasksByGroupResp
	{
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x00014F80 File Offset: 0x00013180
		// (set) Token: 0x06002C52 RID: 11346 RVA: 0x00014F88 File Offset: 0x00013188
		[DataMember]
		public IList<DailyJobTaskDTO> DailyJobResults { get; set; }
	}
}
