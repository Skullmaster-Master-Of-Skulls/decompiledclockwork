using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200014C RID: 332
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemovePasswordReq : BaseMessageReq
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00003C19 File Offset: 0x00001E19
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00003C21 File Offset: 0x00001E21
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00003C2A File Offset: 0x00001E2A
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00003C32 File Offset: 0x00001E32
		[DataMember]
		public string UserName { get; set; }
	}
}
