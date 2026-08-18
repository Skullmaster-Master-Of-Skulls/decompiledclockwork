using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000515 RID: 1301
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportFromTemplateResp
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0000C9DA File Offset: 0x0000ABDA
		[DataMember]
		public int CatalogId { get; set; }
	}
}
