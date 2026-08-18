using System;
using System.Collections.Generic;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000534 RID: 1332
	public class AutoBookTestExamPreviewResult
	{
		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x0002BD2C File Offset: 0x00029F2C
		// (set) Token: 0x06002A48 RID: 10824 RVA: 0x0002BD34 File Offset: 0x00029F34
		public bool Succeeded { get; set; }

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x0002BD3D File Offset: 0x00029F3D
		// (set) Token: 0x06002A4A RID: 10826 RVA: 0x0002BD45 File Offset: 0x00029F45
		public int AppliedBreakMinutes { get; set; }

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x0002BD4E File Offset: 0x00029F4E
		// (set) Token: 0x06002A4C RID: 10828 RVA: 0x0002BD56 File Offset: 0x00029F56
		public IList<TryToBookFailure> Failures { get; set; }

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x0002BD5F File Offset: 0x00029F5F
		// (set) Token: 0x06002A4E RID: 10830 RVA: 0x0002BD67 File Offset: 0x00029F67
		public DateTime? PotentialStartDateTime { get; set; }

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x0002BD70 File Offset: 0x00029F70
		// (set) Token: 0x06002A50 RID: 10832 RVA: 0x0002BD78 File Offset: 0x00029F78
		public DateTime? PotentialEndDateTime { get; set; }

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x0002BD81 File Offset: 0x00029F81
		// (set) Token: 0x06002A52 RID: 10834 RVA: 0x0002BD89 File Offset: 0x00029F89
		public BasicPerson Student { get; set; }

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x0002BD92 File Offset: 0x00029F92
		// (set) Token: 0x06002A54 RID: 10836 RVA: 0x0002BD9A File Offset: 0x00029F9A
		public LookupCourseBase Course { get; set; }

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x0002BDA3 File Offset: 0x00029FA3
		// (set) Token: 0x06002A56 RID: 10838 RVA: 0x0002BDAB File Offset: 0x00029FAB
		public AppointmentRoom PotentialRoom { get; set; }

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x0002BDB4 File Offset: 0x00029FB4
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x0002BDBC File Offset: 0x00029FBC
		public IList<int> AccommodationCids { get; set; }
	}
}
