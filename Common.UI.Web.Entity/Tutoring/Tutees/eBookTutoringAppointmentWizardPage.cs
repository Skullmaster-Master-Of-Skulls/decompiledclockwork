using System;

namespace TechnoPro.Common.UI.Web.Entity.Tutoring.Tutees
{
	// Token: 0x02000025 RID: 37
	public enum eBookTutoringAppointmentWizardPage
	{
		// Token: 0x040000D3 RID: 211
		[BookTutoringAppointmentWizardPage("Search", "~/ctrls/Tutoring/Tutee/ScheduleAppointmentWizard/CtrlSearchParameters.ascx", "~/img/1_normal.png", "~/img/1_active.png", "~/img/1_normal.png")]
		Search,
		// Token: 0x040000D4 RID: 212
		[BookTutoringAppointmentWizardPage("Tutors", "~/ctrls/Tutoring/Tutee/ScheduleAppointmentWizard/CtrlTutorSearchResults.ascx", "~/img/2_normal.png", "~/img/2_active.png", "~/img/2_disable.png")]
		Tutors,
		// Token: 0x040000D5 RID: 213
		[BookTutoringAppointmentWizardPage("Availability", "~/ctrls/Tutoring/Tutee/ScheduleAppointmentWizard/CtrlAvailabilityResults.ascx", "~/img/3_normal.png", "~/img/3_active.png", "~/img/3_disable.png")]
		Availability,
		// Token: 0x040000D6 RID: 214
		[BookTutoringAppointmentWizardPage("Finalize", "~/ctrls/Tutoring/Tutee/ScheduleAppointmentWizard/CtrlBook.ascx", "~/img/4_normal.png", "~/img/4_active.png", "~/img/4_disable.png")]
		Finalize
	}
}
