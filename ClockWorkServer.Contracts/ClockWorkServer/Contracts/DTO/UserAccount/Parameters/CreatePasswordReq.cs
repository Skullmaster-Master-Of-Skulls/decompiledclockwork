using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200014D RID: 333
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePasswordReq : BaseMessageReq
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00003C3B File Offset: 0x00001E3B
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00003C43 File Offset: 0x00001E43
		[DataMember]
		public UserInfoPasswordDTO PasswordInfo { get; set; }
	}
}
