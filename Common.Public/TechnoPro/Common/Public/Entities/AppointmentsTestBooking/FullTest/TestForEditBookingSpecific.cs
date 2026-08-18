using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest
{
	// Token: 0x02000529 RID: 1321
	public class TestForEditBookingSpecific
	{
		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x0002AE1C File Offset: 0x0002901C
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x0002AE24 File Offset: 0x00029024
		public string StudentNote { get; set; }

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x0002AE2D File Offset: 0x0002902D
		// (set) Token: 0x060029DA RID: 10714 RVA: 0x0002AE35 File Offset: 0x00029035
		public string AccommodationsForTestCachedList { get; set; }

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x0002AE3E File Offset: 0x0002903E
		// (set) Token: 0x060029DC RID: 10716 RVA: 0x0002AE46 File Offset: 0x00029046
		public string BookingNote { get; set; }

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x0002AE4F File Offset: 0x0002904F
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x0002AE57 File Offset: 0x00029057
		public string PrivateNote { get; set; }

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x0002AE60 File Offset: 0x00029060
		// (set) Token: 0x060029E0 RID: 10720 RVA: 0x0002AE68 File Offset: 0x00029068
		public bool UpdateStudentReportedClassTime { get; set; }

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x0002AE71 File Offset: 0x00029071
		// (set) Token: 0x060029E2 RID: 10722 RVA: 0x0002AE79 File Offset: 0x00029079
		public DateTime? StudentReportedClassStartTime { get; set; }

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x0002AE82 File Offset: 0x00029082
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x0002AE8A File Offset: 0x0002908A
		public DateTime? StudentReportedClassEndTime { get; set; }

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x0002AE93 File Offset: 0x00029093
		// (set) Token: 0x060029E6 RID: 10726 RVA: 0x0002AE9B File Offset: 0x0002909B
		public IList<int> AccommodationCids { get; set; }

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x0002AEA4 File Offset: 0x000290A4
		// (set) Token: 0x060029E8 RID: 10728 RVA: 0x0002AEAC File Offset: 0x000290AC
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x0002AEB5 File Offset: 0x000290B5
		// (set) Token: 0x060029EA RID: 10730 RVA: 0x0002AEBD File Offset: 0x000290BD
		public string TestPickedUpNote { get; set; }

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x0002AEC6 File Offset: 0x000290C6
		// (set) Token: 0x060029EC RID: 10732 RVA: 0x0002AECE File Offset: 0x000290CE
		public DateTime? InstructorAcknowledgeDate { get; set; }

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x0002AED7 File Offset: 0x000290D7
		// (set) Token: 0x060029EE RID: 10734 RVA: 0x0002AEDF File Offset: 0x000290DF
		public bool InstructorAcknowledgedOnline { get; set; }

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x0002AEE8 File Offset: 0x000290E8
		// (set) Token: 0x060029F0 RID: 10736 RVA: 0x0002AEF0 File Offset: 0x000290F0
		public int ExamStatusLookupId { get; set; }
	}
}
