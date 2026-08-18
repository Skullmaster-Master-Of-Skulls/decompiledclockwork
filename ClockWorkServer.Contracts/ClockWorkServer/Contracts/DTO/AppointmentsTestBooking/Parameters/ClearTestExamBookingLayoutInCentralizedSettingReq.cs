using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A7C RID: 2684
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearTestExamBookingLayoutInCentralizedSettingReq : BaseMessageReq
	{
		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x0001B4C4 File Offset: 0x000196C4
		// (set) Token: 0x0600383E RID: 14398 RVA: 0x0001B4CC File Offset: 0x000196CC
		[DataMember]
		public eTestExamBookingGridViewType View { get; set; }

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x0001B4D5 File Offset: 0x000196D5
		// (set) Token: 0x06003840 RID: 14400 RVA: 0x0001B4DD File Offset: 0x000196DD
		[DataMember]
		public int TestExamBookingGridViewTypeInstanceId { get; set; }
	}
}
