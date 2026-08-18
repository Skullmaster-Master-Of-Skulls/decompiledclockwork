using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D4 RID: 2516
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoBookTestOrExamPreviewResp
	{
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000196B6 File Offset: 0x000178B6
		// (set) Token: 0x0600344C RID: 13388 RVA: 0x000196BE File Offset: 0x000178BE
		[DataMember]
		public AutoBookTestExamPreviewResultDTO AutoBookTestExamPreviewResult { get; set; }
	}
}
