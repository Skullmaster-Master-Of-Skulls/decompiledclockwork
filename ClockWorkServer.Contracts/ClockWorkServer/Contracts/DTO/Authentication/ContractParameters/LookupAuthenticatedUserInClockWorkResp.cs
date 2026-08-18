using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008E7 RID: 2279
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupAuthenticatedUserInClockWorkResp
	{
		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x00015EF7 File Offset: 0x000140F7
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x00015EFF File Offset: 0x000140FF
		[DataMember]
		public ClockWorkUserDTO User { get; set; }
	}
}
