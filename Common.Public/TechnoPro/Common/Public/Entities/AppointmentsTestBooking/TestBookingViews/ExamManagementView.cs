using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x02000523 RID: 1315
	public class ExamManagementView
	{
		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x0002A2D0 File Offset: 0x000284D0
		// (set) Token: 0x060028A4 RID: 10404 RVA: 0x0002A2D8 File Offset: 0x000284D8
		public string Title { get; set; }

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0002A2E1 File Offset: 0x000284E1
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x0002A2E9 File Offset: 0x000284E9
		public string Description { get; set; }

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x0002A2F2 File Offset: 0x000284F2
		// (set) Token: 0x060028A8 RID: 10408 RVA: 0x0002A2FA File Offset: 0x000284FA
		public eExamManagementViewGroup Group { get; set; }

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0002A303 File Offset: 0x00028503
		// (set) Token: 0x060028AA RID: 10410 RVA: 0x0002A30B File Offset: 0x0002850B
		public eExamManagementViewType ViewType { get; set; }

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x0002A314 File Offset: 0x00028514
		// (set) Token: 0x060028AC RID: 10412 RVA: 0x0002A31C File Offset: 0x0002851C
		public eExamManagementQueryType QueryType { get; set; }

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x0002A325 File Offset: 0x00028525
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x0002A32D File Offset: 0x0002852D
		public int? StartDaysFromToday { get; set; }

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x0002A336 File Offset: 0x00028536
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x0002A33E File Offset: 0x0002853E
		public int? EndNumDays { get; set; }

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0002A347 File Offset: 0x00028547
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x0002A34F File Offset: 0x0002854F
		public int OrderNum { get; set; }

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x0002A358 File Offset: 0x00028558
		// (set) Token: 0x060028B4 RID: 10420 RVA: 0x0002A360 File Offset: 0x00028560
		public bool IsDisabled { get; set; }

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x0002A369 File Offset: 0x00028569
		// (set) Token: 0x060028B6 RID: 10422 RVA: 0x0002A371 File Offset: 0x00028571
		public int ReportId { get; set; }
	}
}
