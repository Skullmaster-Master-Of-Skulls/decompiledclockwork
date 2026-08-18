using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200086C RID: 2156
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddClockWorkServerExecutingLogResp
	{
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x00014C45 File Offset: 0x00012E45
		// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x00014C4D File Offset: 0x00012E4D
		[DataMember]
		public int LogId { get; set; }
	}
}
