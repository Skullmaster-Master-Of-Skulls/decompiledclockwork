using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A0 RID: 1440
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductAvailabilityReq : BaseMessageReq
	{
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x0000D998 File Offset: 0x0000BB98
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		[DataMember]
		public Guid ProductUniqueId { get; set; }

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0000D9B1 File Offset: 0x0000BBB1
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0000D9BA File Offset: 0x0000BBBA
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0000D9C2 File Offset: 0x0000BBC2
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0000D9CB File Offset: 0x0000BBCB
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0000D9D3 File Offset: 0x0000BBD3
		[DataMember]
		public int ReservationId { get; set; }

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		[DataMember]
		public int LoanId { get; set; }

		// Token: 0x04000A6F RID: 2671
		[DataMember(EmitDefaultValue = false)]
		public bool IncludeReservations = true;

		// Token: 0x04000A70 RID: 2672
		[DataMember(EmitDefaultValue = false)]
		public bool IncludeLoans = true;
	}
}
