using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A71 RID: 2673
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionSmallByExamIdReq : BaseReportMessageReq
	{
		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x0001B392 File Offset: 0x00019592
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x0001B39A File Offset: 0x0001959A
		[DataMember]
		public ClassTestDefinitionsManagementContextDTO Context { get; set; }

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x0001B3A3 File Offset: 0x000195A3
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x0001B3AB File Offset: 0x000195AB
		[DataMember]
		public int ExamId { get; set; }
	}
}
