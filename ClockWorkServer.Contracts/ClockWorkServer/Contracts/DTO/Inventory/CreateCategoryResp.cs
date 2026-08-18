using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200051C RID: 1308
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCategoryResp
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0000CA5A File Offset: 0x0000AC5A
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x0000CA62 File Offset: 0x0000AC62
		[DataMember]
		public bool WasCreated { get; set; }
	}
}
