using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200015E RID: 350
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidatePasswordAgainstPolicyReq : BaseMessageReq
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00003E39 File Offset: 0x00002039
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00003E41 File Offset: 0x00002041
		[DataMember]
		public string Password { get; set; }
	}
}
