using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A2 RID: 1442
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductsInReservationGroupReq : BaseMessageReq
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0000DA15 File Offset: 0x0000BC15
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0000DA1D File Offset: 0x0000BC1D
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0000DA26 File Offset: 0x0000BC26
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0000DA2E File Offset: 0x0000BC2E
		[DataMember]
		public int ReservationGroupId { get; set; }
	}
}
