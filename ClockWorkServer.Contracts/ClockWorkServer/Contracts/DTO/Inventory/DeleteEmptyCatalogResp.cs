using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200050F RID: 1295
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteEmptyCatalogResp
	{
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0000C928 File Offset: 0x0000AB28
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x0000C930 File Offset: 0x0000AB30
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}
