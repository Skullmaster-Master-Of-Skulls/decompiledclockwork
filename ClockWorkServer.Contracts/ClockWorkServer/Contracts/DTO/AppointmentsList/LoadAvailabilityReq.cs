using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE2 RID: 2786
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityReq : BaseMessageReq
	{
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x0001CB01 File Offset: 0x0001AD01
		// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x0001CB09 File Offset: 0x0001AD09
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x0001CB12 File Offset: 0x0001AD12
		// (set) Token: 0x06003AEB RID: 15083 RVA: 0x0001CB1A File Offset: 0x0001AD1A
		[DataMember]
		public int NumDays { get; set; }

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x0001CB23 File Offset: 0x0001AD23
		// (set) Token: 0x06003AED RID: 15085 RVA: 0x0001CB2B File Offset: 0x0001AD2B
		[DataMember]
		public IList<int> PersonIds { get; set; }
	}
}
