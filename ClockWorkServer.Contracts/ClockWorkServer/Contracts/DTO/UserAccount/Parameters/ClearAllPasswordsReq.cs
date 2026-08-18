using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters
{
	// Token: 0x0200015D RID: 349
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearAllPasswordsReq : BaseMessageReq
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00003E17 File Offset: 0x00002017
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00003E1F File Offset: 0x0000201F
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00003E28 File Offset: 0x00002028
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00003E30 File Offset: 0x00002030
		[DataMember]
		public bool ClearPrimaryPassword { get; set; }
	}
}
