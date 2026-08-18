using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection
{
	// Token: 0x02000882 RID: 2178
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerConnectionInfoResp
	{
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x00014E3D File Offset: 0x0001303D
		// (set) Token: 0x06002C21 RID: 11297 RVA: 0x00014E45 File Offset: 0x00013045
		[DataMember]
		public ClockWorkServerPreferredConnectionInfoDTO ServerConnectionInfo { get; set; }
	}
}
