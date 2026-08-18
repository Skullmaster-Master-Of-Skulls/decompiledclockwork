using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200015A RID: 346
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePrimaryPassword2Req : BaseMessageReq
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00003DC2 File Offset: 0x00001FC2
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x00003DCA File Offset: 0x00001FCA
		[DataMember]
		public UserInfoPasswordDTO PasswordInfo { get; set; }
	}
}
