using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x02000152 RID: 338
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPasswordReq : BaseMessageReq
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00003CF6 File Offset: 0x00001EF6
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x00003CFE File Offset: 0x00001EFE
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00003D07 File Offset: 0x00001F07
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x00003D0F File Offset: 0x00001F0F
		[DataMember]
		public string UserName { get; set; }
	}
}
