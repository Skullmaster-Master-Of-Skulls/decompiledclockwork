using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000531 RID: 1329
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryArchivedLoanDTO
	{
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x0000CC14 File Offset: 0x0000AE14
		[DataMember]
		public int LoanId { get; set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0000CC1D File Offset: 0x0000AE1D
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x0000CC25 File Offset: 0x0000AE25
		[DataMember]
		public InventoryLoanGroupDTO Group { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0000CC2E File Offset: 0x0000AE2E
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0000CC36 File Offset: 0x0000AE36
		[DataMember]
		public InventoryProductSnapshotDTO LoanedProduct { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0000CC3F File Offset: 0x0000AE3F
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0000CC47 File Offset: 0x0000AE47
		[DataMember]
		public PersonBaseDTO WhoReturned { get; set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0000CC50 File Offset: 0x0000AE50
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0000CC58 File Offset: 0x0000AE58
		[DataMember]
		public string ReturnedNotes { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0000CC61 File Offset: 0x0000AE61
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0000CC69 File Offset: 0x0000AE69
		[DataMember]
		public DateTime ReturnedDate { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0000CC72 File Offset: 0x0000AE72
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0000CC7A File Offset: 0x0000AE7A
		[DataMember]
		public InventoryLoanStatusDTO ReturnedStatus { get; set; }
	}
}
