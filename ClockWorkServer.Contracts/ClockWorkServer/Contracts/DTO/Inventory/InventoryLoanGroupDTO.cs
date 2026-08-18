using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000532 RID: 1330
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryLoanGroupDTO
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x0000CC83 File Offset: 0x0000AE83
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x0000CC8B File Offset: 0x0000AE8B
		[DataMember]
		public int LoanGroupId { get; set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0000CC94 File Offset: 0x0000AE94
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		[DataMember]
		public DateTime LoanedDate { get; set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0000CCA5 File Offset: 0x0000AEA5
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x0000CCAD File Offset: 0x0000AEAD
		[DataMember]
		public DateTime DueDate { get; set; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0000CCB6 File Offset: 0x0000AEB6
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x0000CCBE File Offset: 0x0000AEBE
		[DataMember]
		public string LoanNotes { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0000CCC7 File Offset: 0x0000AEC7
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x0000CCCF File Offset: 0x0000AECF
		[DataMember]
		public PersonBaseDTO LoanedTo { get; set; }

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		[DataMember]
		public PersonBaseDTO WhoLoaned { get; set; }

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0000CCE9 File Offset: 0x0000AEE9
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0000CCF1 File Offset: 0x0000AEF1
		[DataMember]
		public InventoryLocationDTO Location { get; set; }
	}
}
