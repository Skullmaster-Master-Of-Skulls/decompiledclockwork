using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008DA RID: 2266
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticationAndAuthorizationResultDTO
	{
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x00015B72 File Offset: 0x00013D72
		// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x00015B7A File Offset: 0x00013D7A
		[DataMember]
		public ClockWorkUserDTO ClockWorkUser { get; set; }

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x00015B83 File Offset: 0x00013D83
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x00015B8B File Offset: 0x00013D8B
		[DataMember]
		public bool PassedAuthentication { get; set; }
	}
}
