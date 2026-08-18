using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x02000890 RID: 2192
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDailyJobTaskByIdResp
	{
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x00014FA2 File Offset: 0x000131A2
		// (set) Token: 0x06002C58 RID: 11352 RVA: 0x00014FAA File Offset: 0x000131AA
		[DataMember]
		public DailyJobTaskDTO DailyJobResults { get; set; }
	}
}
