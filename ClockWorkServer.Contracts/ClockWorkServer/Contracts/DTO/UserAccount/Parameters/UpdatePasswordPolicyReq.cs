using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000162 RID: 354
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePasswordPolicyReq : BaseMessageReq
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00003E7D File Offset: 0x0000207D
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00003E85 File Offset: 0x00002085
		[DataMember]
		public PasswordPolicyDTO PasswordPolicy { get; set; }
	}
}
