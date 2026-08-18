using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000523 RID: 1315
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCategoriesByCatalogReq : BaseMessageReq
	{
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0000CAE2 File Offset: 0x0000ACE2
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x0000CAEA File Offset: 0x0000ACEA
		[DataMember]
		public int CatalogId { get; set; }
	}
}
