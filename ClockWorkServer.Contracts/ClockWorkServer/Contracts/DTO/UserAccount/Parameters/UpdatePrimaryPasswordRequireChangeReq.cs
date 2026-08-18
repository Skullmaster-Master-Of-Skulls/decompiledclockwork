using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000157 RID: 343
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePrimaryPasswordRequireChangeReq : BaseMessageReq
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00003D5C File Offset: 0x00001F5C
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x00003D64 File Offset: 0x00001F64
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00003D6D File Offset: 0x00001F6D
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x00003D75 File Offset: 0x00001F75
		[DataMember]
		public bool NewDoesRequirePasswordChange { get; set; }
	}
}
