using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A9A RID: 2714
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoRescheduleTestExamResultDTO
	{
		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x0001BBF2 File Offset: 0x00019DF2
		// (set) Token: 0x0600392A RID: 14634 RVA: 0x0001BBFA File Offset: 0x00019DFA
		[DataMember]
		public bool Successful { get; set; }

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x0600392B RID: 14635 RVA: 0x0001BC03 File Offset: 0x00019E03
		// (set) Token: 0x0600392C RID: 14636 RVA: 0x0001BC0B File Offset: 0x00019E0B
		[DataMember]
		public AutoBookTestExamPreviewResultDTO PreviewResult { get; set; }
	}
}
