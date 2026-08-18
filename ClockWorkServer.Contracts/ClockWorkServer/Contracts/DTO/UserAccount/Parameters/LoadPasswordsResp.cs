using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200014B RID: 331
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPasswordsResp
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00003C08 File Offset: 0x00001E08
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00003C10 File Offset: 0x00001E10
		[DataMember]
		public IList<UserInfoPasswordDTO> PasswordInfos { get; set; }
	}
}
