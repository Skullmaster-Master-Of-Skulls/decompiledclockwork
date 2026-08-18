using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x02000888 RID: 2184
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunDailyJobResp
	{
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x00014F09 File Offset: 0x00013109
		// (set) Token: 0x06002C3E RID: 11326 RVA: 0x00014F11 File Offset: 0x00013111
		[DataMember]
		public IList<DailyJobTaskResultDTO> DailyJobResults { get; set; }
	}
}
