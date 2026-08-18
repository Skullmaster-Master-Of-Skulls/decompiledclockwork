using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200014A RID: 330
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPasswordsReq : BaseMessageReq
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00003BF7 File Offset: 0x00001DF7
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00003BFF File Offset: 0x00001DFF
		[DataMember]
		public int PersonId { get; set; }
	}
}
