using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200054F RID: 1359
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoanByIdResp
	{
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0000CED6 File Offset: 0x0000B0D6
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x0000CEDE File Offset: 0x0000B0DE
		[DataMember]
		public InventoryArchivedLoanDTO ReturnedLoan { get; set; }
	}
}
