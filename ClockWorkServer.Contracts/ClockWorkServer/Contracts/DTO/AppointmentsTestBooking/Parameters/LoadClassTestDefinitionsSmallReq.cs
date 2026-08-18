using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A69 RID: 2665
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionsSmallReq : BaseReportMessageReq
	{
		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x0001B2B5 File Offset: 0x000194B5
		// (set) Token: 0x060037ED RID: 14317 RVA: 0x0001B2BD File Offset: 0x000194BD
		[DataMember]
		public DateTime? StartDate { get; set; }

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x0001B2C6 File Offset: 0x000194C6
		// (set) Token: 0x060037EF RID: 14319 RVA: 0x0001B2CE File Offset: 0x000194CE
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x0001B2D7 File Offset: 0x000194D7
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x0001B2DF File Offset: 0x000194DF
		[DataMember]
		public ClassTestDefinitionsManagementContextDTO Context { get; set; }
	}
}
