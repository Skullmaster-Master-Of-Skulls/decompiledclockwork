using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000542 RID: 1346
	[DataContract(Namespace = "http://tpro.ca")]
	public class MakeLoanReq : BaseMessageReq
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0000CE2C File Offset: 0x0000B02C
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x0000CE34 File Offset: 0x0000B034
		[DataMember]
		public InventoryLoanGroupDTO Loan { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0000CE3D File Offset: 0x0000B03D
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0000CE45 File Offset: 0x0000B045
		[DataMember]
		public IList<string> LoanedProductUniqueIds { get; set; }
	}
}
