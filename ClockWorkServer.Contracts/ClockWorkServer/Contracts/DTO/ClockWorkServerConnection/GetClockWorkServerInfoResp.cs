using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServer;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection
{
	// Token: 0x02000884 RID: 2180
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerInfoResp
	{
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x00014E4E File Offset: 0x0001304E
		// (set) Token: 0x06002C25 RID: 11301 RVA: 0x00014E56 File Offset: 0x00013056
		[DataMember]
		public ClockWorkServerInfoDTO ServerInfo { get; set; }
	}
}
