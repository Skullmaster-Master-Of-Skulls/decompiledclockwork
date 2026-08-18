using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A87 RID: 2695
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRoomsWithAvailabilityReq : BaseMessageReq
	{
		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x0001B5A1 File Offset: 0x000197A1
		// (set) Token: 0x06003863 RID: 14435 RVA: 0x0001B5A9 File Offset: 0x000197A9
		[DataMember]
		public eTestExamSeatType TestType { get; set; }

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x0001B5B2 File Offset: 0x000197B2
		// (set) Token: 0x06003865 RID: 14437 RVA: 0x0001B5BA File Offset: 0x000197BA
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x0001B5C3 File Offset: 0x000197C3
		// (set) Token: 0x06003867 RID: 14439 RVA: 0x0001B5CB File Offset: 0x000197CB
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x0001B5D4 File Offset: 0x000197D4
		// (set) Token: 0x06003869 RID: 14441 RVA: 0x0001B5DC File Offset: 0x000197DC
		[DataMember]
		public IList<int> RoomIdsToIgnore { get; set; }
	}
}
