using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000309 RID: 777
	public class InventoryProductSnapshot : BusinessBase<int>
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0001CF7C File Offset: 0x0001B17C
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ProductSnapshotId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0001CF94 File Offset: 0x0001B194
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x0001CF9C File Offset: 0x0001B19C
		public Guid ProductUniqueId { get; set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0001CFA5 File Offset: 0x0001B1A5
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0001CFAD File Offset: 0x0001B1AD
		public int ProductDynamicDataId { get; set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0001CFB6 File Offset: 0x0001B1B6
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x0001CFBE File Offset: 0x0001B1BE
		public string ProductName { get; set; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0001CFC7 File Offset: 0x0001B1C7
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0001CFCF File Offset: 0x0001B1CF
		public string BarCode { get; set; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
		public string SerialNumber { get; set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0001CFE9 File Offset: 0x0001B1E9
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x0001CFF1 File Offset: 0x0001B1F1
		public string CategoryName { get; set; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0001CFFA File Offset: 0x0001B1FA
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0001D002 File Offset: 0x0001B202
		public string Location { get; set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0001D00B File Offset: 0x0001B20B
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x0001D013 File Offset: 0x0001B213
		public DateTime? LocationDate { get; set; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0001D01C File Offset: 0x0001B21C
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x0001D024 File Offset: 0x0001B224
		public PersonBase InChargePerson { get; set; }

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0001D02D File Offset: 0x0001B22D
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x0001D035 File Offset: 0x0001B235
		public string GroupName { get; set; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0001D03E File Offset: 0x0001B23E
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x0001D046 File Offset: 0x0001B246
		public string ProductStatus { get; set; }

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0001D04F File Offset: 0x0001B24F
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x0001D057 File Offset: 0x0001B257
		public IList<InventoryProductAccessory> Accessories { get; set; }

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0001D060 File Offset: 0x0001B260
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x0001D068 File Offset: 0x0001B268
		public int ReturnLoanId { get; set; }

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0001D071 File Offset: 0x0001B271
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x0001D079 File Offset: 0x0001B279
		public int LoanGroupId { get; set; }

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0001D082 File Offset: 0x0001B282
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x0001D08A File Offset: 0x0001B28A
		public DateTime? LoanedDate { get; set; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0001D093 File Offset: 0x0001B293
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0001D09B File Offset: 0x0001B29B
		public DateTime? DueDate { get; set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		public DateTime? ReturnedDate { get; set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0001D0B5 File Offset: 0x0001B2B5
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x0001D0BD File Offset: 0x0001B2BD
		public PersonBase LoanedTo { get; set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0001D0C6 File Offset: 0x0001B2C6
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0001D0CE File Offset: 0x0001B2CE
		public string LoanLocation { get; set; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0001D0D7 File Offset: 0x0001B2D7
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x0001D0DF File Offset: 0x0001B2DF
		public PersonBase WhoLoaned { get; set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0001D0E8 File Offset: 0x0001B2E8
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
		public PersonBase WhoReturned { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0001D0F9 File Offset: 0x0001B2F9
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x0001D101 File Offset: 0x0001B301
		public string LoanNotes { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0001D10A File Offset: 0x0001B30A
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x0001D112 File Offset: 0x0001B312
		public string ReturnedStatus { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0001D11B File Offset: 0x0001B31B
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x0001D123 File Offset: 0x0001B323
		public string ReturnedNotes { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0001D12C File Offset: 0x0001B32C
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x0001D134 File Offset: 0x0001B334
		public PersonBase WhoModified { get; set; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0001D13D File Offset: 0x0001B33D
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x0001D145 File Offset: 0x0001B345
		public DateTime ModifiedDate { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0001D14E File Offset: 0x0001B34E
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x0001D156 File Offset: 0x0001B356
		public eInventoryProductSnapshotReason Reason { get; set; }
	}
}
