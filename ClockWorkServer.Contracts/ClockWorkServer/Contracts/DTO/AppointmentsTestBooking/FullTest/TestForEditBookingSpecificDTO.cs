using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest
{
	// Token: 0x02000A8A RID: 2698
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestForEditBookingSpecificDTO
	{
		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x0001B629 File Offset: 0x00019829
		// (set) Token: 0x06003876 RID: 14454 RVA: 0x0001B631 File Offset: 0x00019831
		[DataMember]
		public string StudentNote { get; set; }

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x0001B63A File Offset: 0x0001983A
		// (set) Token: 0x06003878 RID: 14456 RVA: 0x0001B642 File Offset: 0x00019842
		[DataMember]
		public string AccommodationsForTestCachedList { get; set; }

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x0001B64B File Offset: 0x0001984B
		// (set) Token: 0x0600387A RID: 14458 RVA: 0x0001B653 File Offset: 0x00019853
		[DataMember]
		public string BookingNote { get; set; }

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x0001B65C File Offset: 0x0001985C
		// (set) Token: 0x0600387C RID: 14460 RVA: 0x0001B664 File Offset: 0x00019864
		[DataMember]
		public string PrivateNote { get; set; }

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x0001B66D File Offset: 0x0001986D
		// (set) Token: 0x0600387E RID: 14462 RVA: 0x0001B675 File Offset: 0x00019875
		[DataMember]
		public bool UpdateStudentReportedClassTime { get; set; }

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x0001B67E File Offset: 0x0001987E
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x0001B686 File Offset: 0x00019886
		[DataMember]
		public DateTime? StudentReportedClassStartTime { get; set; }

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x0001B68F File Offset: 0x0001988F
		// (set) Token: 0x06003882 RID: 14466 RVA: 0x0001B697 File Offset: 0x00019897
		[DataMember]
		public DateTime? StudentReportedClassEndTime { get; set; }

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x0001B6A0 File Offset: 0x000198A0
		// (set) Token: 0x06003884 RID: 14468 RVA: 0x0001B6A8 File Offset: 0x000198A8
		[DataMember]
		public IList<int> AccommodationCids { get; set; }

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x0001B6B1 File Offset: 0x000198B1
		// (set) Token: 0x06003886 RID: 14470 RVA: 0x0001B6B9 File Offset: 0x000198B9
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x0001B6C2 File Offset: 0x000198C2
		// (set) Token: 0x06003888 RID: 14472 RVA: 0x0001B6CA File Offset: 0x000198CA
		[DataMember]
		public string TestPickedUpNote { get; set; }

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06003889 RID: 14473 RVA: 0x0001B6D3 File Offset: 0x000198D3
		// (set) Token: 0x0600388A RID: 14474 RVA: 0x0001B6DB File Offset: 0x000198DB
		[DataMember]
		public DateTime? InstructorAcknowledgeDate { get; set; }

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x0600388B RID: 14475 RVA: 0x0001B6E4 File Offset: 0x000198E4
		// (set) Token: 0x0600388C RID: 14476 RVA: 0x0001B6EC File Offset: 0x000198EC
		[DataMember]
		public bool InstructorAcknowledgedOnline { get; set; }

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x0001B6F5 File Offset: 0x000198F5
		// (set) Token: 0x0600388E RID: 14478 RVA: 0x0001B6FD File Offset: 0x000198FD
		[DataMember]
		public int ExamStatusLookupId { get; set; }
	}
}
