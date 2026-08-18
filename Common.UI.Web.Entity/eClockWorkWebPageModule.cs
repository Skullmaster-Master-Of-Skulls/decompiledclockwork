using System;

namespace TechnoPro.Common.UI.Web.Entity
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public enum eClockWorkWebPageModule
	{
		// Token: 0x0400000A RID: 10
		[ClockWorkWebPageModule("~/user/TutoringTutors/", "Tutoring (tutors)", "fa-user-plus")]
		TutoringTutors,
		// Token: 0x0400000B RID: 11
		[ClockWorkWebPageModule("~/user/TutoringStudents/", "Tutoring (students)", "fa-comments-o")]
		TutoringStudents,
		// Token: 0x0400000C RID: 12
		[ClockWorkWebPageModule("~/user/NotetakingNotetakers/", "Note-takers", "fa-edit")]
		NotetakingNotetakers,
		// Token: 0x0400000D RID: 13
		[ClockWorkWebPageModule("~/user/NotetakingStudents/", "Lecture notes", "fa-thumb-tack")]
		NotetakingStudents,
		// Token: 0x0400000E RID: 14
		[ClockWorkWebPageModule("~/user/test/", "Schedule a test or exam", "fa-book")]
		TestBooking,
		// Token: 0x0400000F RID: 15
		[ClockWorkWebPageModule("~/user/instructor/", "Instructor access", "fa-user-circle")]
		Instructor,
		// Token: 0x04000010 RID: 16
		[ClockWorkWebPageModule("~/staff/schedule/", "Staff access", "fa-id-card-o")]
		Staff,
		// Token: 0x04000011 RID: 17
		[ClockWorkWebPageModule("~/admin/settings/", "Admin access", "fa-lock")]
		Admin,
		// Token: 0x04000012 RID: 18
		[ClockWorkWebPageModule("~/user/workshop2/", "Workshops and events", "fa-users")]
		WorkshopBooking,
		// Token: 0x04000013 RID: 19
		[ClockWorkWebPageModule("~/user/appt/", "Schedule an appointment", "fa-calendar-plus-o")]
		AppointmentBooking,
		// Token: 0x04000014 RID: 20
		[ClockWorkWebPageModule("~/user/vet/", "Veteran benefits", "fa-check-square-o")]
		Veterans,
		// Token: 0x04000015 RID: 21
		[ClockWorkWebPageModule("~/user/SelfRegC/", "Accommodation registration", "fa-check-square-o")]
		SelfRegistration,
		// Token: 0x04000016 RID: 22
		[ClockWorkWebPageModule("~/user/Intake/", "Intake registration", "fa-register")]
		OnlineIntake,
		// Token: 0x04000017 RID: 23
		[ClockWorkWebPageModule("~/user/Survey/", "Surveys", "fa-th-list")]
		Survey,
		// Token: 0x04000018 RID: 24
		[ClockWorkWebPageModule("~/alternateformat", "Alternate format text-books and media", "fa-bookmark")]
		AlternateFormat,
		// Token: 0x04000019 RID: 25
		[ClockWorkWebPageModule("~/user/test", "Accommodation letters", "fa-envelop")]
		AccommodationLetters
	}
}
