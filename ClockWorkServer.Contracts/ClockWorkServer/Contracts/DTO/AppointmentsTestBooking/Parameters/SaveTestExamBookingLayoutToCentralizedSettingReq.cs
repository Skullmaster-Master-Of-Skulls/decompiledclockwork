using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A7B RID: 2683
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveTestExamBookingLayoutToCentralizedSettingReq : BaseMessageReq
	{
		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x0001B491 File Offset: 0x00019691
		// (set) Token: 0x06003837 RID: 14391 RVA: 0x0001B499 File Offset: 0x00019699
		[DataMember]
		public eTestExamBookingGridViewType View { get; set; }

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x0001B4A2 File Offset: 0x000196A2
		// (set) Token: 0x06003839 RID: 14393 RVA: 0x0001B4AA File Offset: 0x000196AA
		[DataMember]
		public int TestExamBookingGridViewTypeInstanceId { get; set; }

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x0001B4B3 File Offset: 0x000196B3
		// (set) Token: 0x0600383B RID: 14395 RVA: 0x0001B4BB File Offset: 0x000196BB
		[DataMember]
		public string LayoutCompressed { get; set; }
	}
}
