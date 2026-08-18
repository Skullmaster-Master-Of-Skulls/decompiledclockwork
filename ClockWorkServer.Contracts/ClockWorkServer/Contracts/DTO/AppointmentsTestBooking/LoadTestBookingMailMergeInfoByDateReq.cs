using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A0C RID: 2572
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestBookingMailMergeInfoByDateReq : BaseMessageReq
	{
		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x00019E69 File Offset: 0x00018069
		// (set) Token: 0x06003550 RID: 13648 RVA: 0x00019E71 File Offset: 0x00018071
		[DataMember]
		public DateTime Date { get; set; }

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x00019E7A File Offset: 0x0001807A
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x00019E82 File Offset: 0x00018082
		[DataMember]
		public bool ExcludeCancelled { get; set; }

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x00019E8B File Offset: 0x0001808B
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x00019E93 File Offset: 0x00018093
		[DataMember]
		public IList<int> AppTypeIdsToExclude { get; set; }
	}
}
