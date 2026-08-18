using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000161 RID: 353
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPasswordPolicyResp
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00003E6C File Offset: 0x0000206C
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00003E74 File Offset: 0x00002074
		[DataMember]
		public PasswordPolicyDTO PasswordPolicy { get; set; }
	}
}
