using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000155 RID: 341
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPrimaryPasswordResp
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00003D3A File Offset: 0x00001F3A
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00003D42 File Offset: 0x00001F42
		[DataMember]
		public UserInfoPasswordDTO PasswordInfo { get; set; }
	}
}
