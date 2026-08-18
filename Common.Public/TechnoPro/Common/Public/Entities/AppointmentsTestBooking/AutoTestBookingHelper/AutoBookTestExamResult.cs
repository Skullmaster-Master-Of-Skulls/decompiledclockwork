using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000535 RID: 1333
	public class AutoBookTestExamResult : AutoBookTestExamPreviewResult
	{
		// Token: 0x06002A5A RID: 10842 RVA: 0x0002BDC5 File Offset: 0x00029FC5
		public AutoBookTestExamResult()
		{
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0002BDD0 File Offset: 0x00029FD0
		public AutoBookTestExamResult(AutoBookTestExamPreviewResult previewResult)
		{
			bool flag = previewResult == null;
			if (!flag)
			{
				base.Succeeded = previewResult.Succeeded;
				base.Failures = previewResult.Failures;
				base.PotentialStartDateTime = previewResult.PotentialStartDateTime;
				base.PotentialEndDateTime = previewResult.PotentialEndDateTime;
				base.Student = previewResult.Student;
				base.Course = previewResult.Course;
				base.PotentialRoom = previewResult.PotentialRoom;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x0002BE4A File Offset: 0x0002A04A
		// (set) Token: 0x06002A5D RID: 10845 RVA: 0x0002BE52 File Offset: 0x0002A052
		public int AppointmentId { get; set; }

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x0002BE5B File Offset: 0x0002A05B
		// (set) Token: 0x06002A5F RID: 10847 RVA: 0x0002BE63 File Offset: 0x0002A063
		public int ExamId { get; set; }
	}
}
