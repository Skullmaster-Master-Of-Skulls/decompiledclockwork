using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000526 RID: 1318
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProductGroupResp
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0000CB15 File Offset: 0x0000AD15
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x0000CB1D File Offset: 0x0000AD1D
		[DataMember]
		public int GroupId { get; set; }
	}
}
