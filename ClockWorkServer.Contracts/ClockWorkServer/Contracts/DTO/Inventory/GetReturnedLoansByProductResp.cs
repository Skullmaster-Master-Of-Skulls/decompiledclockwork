using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000551 RID: 1361
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByProductResp
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0000CF00 File Offset: 0x0000B100
		[DataMember]
		public IList<InventoryArchivedLoanDTO> ReturnedLoans { get; set; }
	}
}
