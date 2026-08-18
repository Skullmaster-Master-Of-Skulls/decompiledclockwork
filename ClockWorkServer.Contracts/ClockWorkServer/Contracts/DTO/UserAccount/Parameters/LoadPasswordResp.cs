using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000153 RID: 339
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPasswordResp
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00003D18 File Offset: 0x00001F18
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x00003D20 File Offset: 0x00001F20
		[DataMember]
		public UserInfoPasswordDTO PasswordInfo { get; set; }
	}
}
