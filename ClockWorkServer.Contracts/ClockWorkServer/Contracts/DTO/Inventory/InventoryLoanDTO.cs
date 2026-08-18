using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200052F RID: 1327
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryLoanDTO
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x0000CB94 File Offset: 0x0000AD94
		[DataMember]
		public int LoanId { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0000CB9D File Offset: 0x0000AD9D
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
		[DataMember]
		public InventoryProductDTO LoanedProduct { get; set; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x0000CBB6 File Offset: 0x0000ADB6
		[DataMember]
		public InventoryLoanGroupDTO Group { get; set; }
	}
}
