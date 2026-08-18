using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000576 RID: 1398
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryProductSnapshotDTO
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0000D37C File Offset: 0x0000B57C
		// (set) Token: 0x06001CEA RID: 7402 RVA: 0x0000D384 File Offset: 0x0000B584
		[DataMember]
		public int ProductSnapshotId { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0000D38D File Offset: 0x0000B58D
		// (set) Token: 0x06001CEC RID: 7404 RVA: 0x0000D395 File Offset: 0x0000B595
		[DataMember]
		public Guid ProductUniqueId { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x0000D39E File Offset: 0x0000B59E
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x0000D3A6 File Offset: 0x0000B5A6
		[DataMember]
		public int ProductDynamicDataId { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0000D3AF File Offset: 0x0000B5AF
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x0000D3B7 File Offset: 0x0000B5B7
		[DataMember]
		public string ProductName { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		[DataMember]
		public string BarCode { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0000D3D1 File Offset: 0x0000B5D1
		// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x0000D3D9 File Offset: 0x0000B5D9
		[DataMember]
		public string SerialNumber { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x0000D3EA File Offset: 0x0000B5EA
		[DataMember]
		public string CategoryName { get; set; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0000D3F3 File Offset: 0x0000B5F3
		// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x0000D3FB File Offset: 0x0000B5FB
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0000D404 File Offset: 0x0000B604
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x0000D40C File Offset: 0x0000B60C
		[DataMember]
		public DateTime? LocationDate { get; set; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0000D415 File Offset: 0x0000B615
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x0000D41D File Offset: 0x0000B61D
		[DataMember]
		public PersonBaseDTO InChargePerson { get; set; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0000D426 File Offset: 0x0000B626
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x0000D42E File Offset: 0x0000B62E
		[DataMember]
		public string GroupName { get; set; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0000D437 File Offset: 0x0000B637
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x0000D43F File Offset: 0x0000B63F
		[DataMember]
		public string ProductStatus { get; set; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0000D448 File Offset: 0x0000B648
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0000D450 File Offset: 0x0000B650
		[DataMember]
		public int ReturnLoanId { get; set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0000D459 File Offset: 0x0000B659
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x0000D461 File Offset: 0x0000B661
		[DataMember]
		public int LoanGroupId { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0000D46A File Offset: 0x0000B66A
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x0000D472 File Offset: 0x0000B672
		[DataMember]
		public DateTime? LoanedDate { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0000D47B File Offset: 0x0000B67B
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x0000D483 File Offset: 0x0000B683
		[DataMember]
		public DateTime? DueDate { get; set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0000D48C File Offset: 0x0000B68C
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x0000D494 File Offset: 0x0000B694
		[DataMember]
		public DateTime? ReturnedDate { get; set; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0000D49D File Offset: 0x0000B69D
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x0000D4A5 File Offset: 0x0000B6A5
		[DataMember]
		public PersonBaseDTO LoanedTo { get; set; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x0000D4B6 File Offset: 0x0000B6B6
		[DataMember]
		public string LoanLocation { get; set; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0000D4BF File Offset: 0x0000B6BF
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0000D4C7 File Offset: 0x0000B6C7
		[DataMember]
		public PersonBaseDTO WhoLoaned { get; set; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		[DataMember]
		public PersonBaseDTO WhoReturned { get; set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0000D4E1 File Offset: 0x0000B6E1
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x0000D4E9 File Offset: 0x0000B6E9
		[DataMember]
		public string LoanNotes { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0000D4F2 File Offset: 0x0000B6F2
		// (set) Token: 0x06001D16 RID: 7446 RVA: 0x0000D4FA File Offset: 0x0000B6FA
		[DataMember]
		public string ReturnedStatus { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0000D503 File Offset: 0x0000B703
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x0000D50B File Offset: 0x0000B70B
		[DataMember]
		public string ReturnedNotes { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0000D514 File Offset: 0x0000B714
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x0000D51C File Offset: 0x0000B71C
		[DataMember]
		public PersonBaseDTO WhoModified { get; set; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0000D525 File Offset: 0x0000B725
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0000D52D File Offset: 0x0000B72D
		[DataMember]
		public DateTime ModifiedDate { get; set; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0000D536 File Offset: 0x0000B736
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x0000D53E File Offset: 0x0000B73E
		[DataMember]
		public eInventoryProductSnapshotReason Reason { get; set; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0000D547 File Offset: 0x0000B747
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x0000D54F File Offset: 0x0000B74F
		[DataMember]
		public IList<InventoryProductAccessoryDTO> Accessories { get; set; }
	}
}
