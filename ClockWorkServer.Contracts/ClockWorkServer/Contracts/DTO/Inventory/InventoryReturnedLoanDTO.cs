using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000530 RID: 1328
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryReturnedLoanDTO : InventoryLoanDTO
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0000CBBF File Offset: 0x0000ADBF
		// (set) Token: 0x06001BBC RID: 7100 RVA: 0x0000CBC7 File Offset: 0x0000ADC7
		[DataMember]
		public PersonBaseDTO WhoReturned { get; set; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		[DataMember]
		public string ReturnedNotes { get; set; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		// (set) Token: 0x06001BC0 RID: 7104 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		[DataMember]
		public DateTime ReturnedDate { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x0000CBFA File Offset: 0x0000ADFA
		[DataMember]
		public InventoryLoanStatusDTO ReturnedStatus { get; set; }
	}
}
