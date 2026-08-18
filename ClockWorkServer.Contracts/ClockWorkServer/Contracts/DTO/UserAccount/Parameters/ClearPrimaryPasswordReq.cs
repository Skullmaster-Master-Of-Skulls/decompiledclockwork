using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000156 RID: 342
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearPrimaryPasswordReq : BaseMessageReq
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00003D4B File Offset: 0x00001F4B
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x00003D53 File Offset: 0x00001F53
		[DataMember]
		public int PersonId { get; set; }
	}
}
