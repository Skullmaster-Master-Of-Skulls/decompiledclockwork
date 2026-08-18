using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews
{
	// Token: 0x020009A8 RID: 2472
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUnbookedFinalExamsReq : BaseMessageReq
	{
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x00018581 File Offset: 0x00016781
		// (set) Token: 0x06003222 RID: 12834 RVA: 0x00018589 File Offset: 0x00016789
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x00018592 File Offset: 0x00016792
		// (set) Token: 0x06003224 RID: 12836 RVA: 0x0001859A File Offset: 0x0001679A
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x000185A3 File Offset: 0x000167A3
		// (set) Token: 0x06003226 RID: 12838 RVA: 0x000185AB File Offset: 0x000167AB
		[DataMember]
		public bool RequiresApprovedSelfReg { get; set; }

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000185B4 File Offset: 0x000167B4
		// (set) Token: 0x06003228 RID: 12840 RVA: 0x000185BC File Offset: 0x000167BC
		[DataMember]
		public bool RequiresUnexpiredAccommodations { get; set; }

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000185C5 File Offset: 0x000167C5
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x000185CD File Offset: 0x000167CD
		[DataMember]
		public bool RequiresLoaGeneratedByStaff { get; set; }
	}
}
