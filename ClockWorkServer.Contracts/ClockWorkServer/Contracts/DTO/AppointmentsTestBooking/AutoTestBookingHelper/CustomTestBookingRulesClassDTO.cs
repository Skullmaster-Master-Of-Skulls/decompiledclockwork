using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A9D RID: 2717
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomTestBookingRulesClassDTO
	{
		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06003940 RID: 14656 RVA: 0x0001BC9C File Offset: 0x00019E9C
		// (set) Token: 0x06003941 RID: 14657 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		[DataMember]
		public string BinPath { get; set; }

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06003942 RID: 14658 RVA: 0x0001BCAD File Offset: 0x00019EAD
		// (set) Token: 0x06003943 RID: 14659 RVA: 0x0001BCB5 File Offset: 0x00019EB5
		[DataMember]
		public string Code_FindPotentialBookingsStart { get; set; }

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x0001BCBE File Offset: 0x00019EBE
		// (set) Token: 0x06003945 RID: 14661 RVA: 0x0001BCC6 File Offset: 0x00019EC6
		[DataMember]
		public string Code_FindPotentialBookingsEnd { get; set; }

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x0001BCCF File Offset: 0x00019ECF
		// (set) Token: 0x06003947 RID: 14663 RVA: 0x0001BCD7 File Offset: 0x00019ED7
		[DataMember]
		public string Code_FindPotentialBookingsMid { get; set; }
	}
}
