using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000154 RID: 340
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPrimaryPasswordReq : BaseMessageReq
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00003D29 File Offset: 0x00001F29
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x00003D31 File Offset: 0x00001F31
		[DataMember]
		public int PersonId { get; set; }
	}
}
