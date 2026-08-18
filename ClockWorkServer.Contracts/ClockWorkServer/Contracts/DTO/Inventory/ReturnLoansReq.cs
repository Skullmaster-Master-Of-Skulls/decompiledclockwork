using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200054A RID: 1354
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReturnLoansReq : BaseMessageReq
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0000CEA3 File Offset: 0x0000B0A3
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0000CEAB File Offset: 0x0000B0AB
		[DataMember]
		public IList<InventoryReturnedLoanDTO> ReturnedLoans { get; set; }
	}
}
