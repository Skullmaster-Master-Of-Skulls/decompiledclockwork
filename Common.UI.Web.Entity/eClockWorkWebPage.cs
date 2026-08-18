using System;

namespace TechnoPro.Common.UI.Web.Entity
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public enum eClockWorkWebPage
	{
		// Token: 0x0400001B RID: 27
		[ClockWorkWebPage(IsHidden = true)]
		Unknown,
		// Token: 0x0400001C RID: 28
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "", "", false, IsHidden = true)]
		TutoringTutors_Registration,
		// Token: 0x0400001D RID: 29
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "", "", false, IsHidden = true)]
		TutoringTutors_WaitForApproval,
		// Token: 0x0400001E RID: 30
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "", "", false, IsHidden = true)]
		TutoringTutors_ConfidentialityAgreement,
		// Token: 0x0400001F RID: 31
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "Profile", "bio.aspx", false)]
		TutoringTutors_Profile,
		// Token: 0x04000020 RID: 32
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "Calendar", "TutorCalendar.aspx", true)]
		TutoringTutors_Calendar,
		// Token: 0x04000021 RID: 33
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "Availability", "availability.aspx", false)]
		TutoringTutors_Availability,
		// Token: 0x04000022 RID: 34
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "Submit a comment", "SubmitComment.aspx", false, IsSubmitCommentPage = true)]
		TutoringTutors_SubmitComment,
		// Token: 0x04000023 RID: 35
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringTutors, "Help", "default.aspx", false)]
		TutoringTutors_Help,
		// Token: 0x04000024 RID: 36
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringStudents, "", "", false, IsHidden = true)]
		TutoringStudents_ConfidentialityAgreement,
		// Token: 0x04000025 RID: 37
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringStudents, "Schedule an appointment", "book.aspx", false)]
		TutoringStudents_ScheduleAppointment,
		// Token: 0x04000026 RID: 38
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringStudents, "Calendar", "Calendar.aspx", true)]
		TutoringStudents_Calendar,
		// Token: 0x04000027 RID: 39
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringStudents, "My tutors", "MyTutors.aspx", false)]
		TutoringStudents_MyTutors,
		// Token: 0x04000028 RID: 40
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringStudents, "Submit a comment", "SubmitComment.aspx", false, IsSubmitCommentPage = true)]
		TutoringStudents_SubmitComment,
		// Token: 0x04000029 RID: 41
		[ClockWorkWebPage(eClockWorkWebPageModule.TutoringStudents, "Help", "default.aspx", false)]
		TutoringStudents_Help,
		// Token: 0x0400002A RID: 42
		[ClockWorkWebPage(eClockWorkWebPageModule.Staff, "Login as another user", "LoginOptions.aspx", true)]
		Staff_LoginAsAnotherUser,
		// Token: 0x0400002B RID: 43
		[ClockWorkWebPage(eClockWorkWebPageModule.Staff, "Calendar", "schedule.aspx", false)]
		Staff_Calendar,
		// Token: 0x0400002C RID: 44
		[ClockWorkWebPage(eClockWorkWebPageModule.Staff, "Help", "default.aspx", false)]
		Staff_Help,
		// Token: 0x0400002D RID: 45
		[ClockWorkWebPage(eClockWorkWebPageModule.AppointmentBooking, "Schedule an appointment", "book.aspx", false)]
		AppointmentBooking_ScheduleAppointment,
		// Token: 0x0400002E RID: 46
		[ClockWorkWebPage(eClockWorkWebPageModule.AppointmentBooking, "Calendar", "MyUpcomingAppts.aspx", true)]
		AppointmentBooking_Calendar,
		// Token: 0x0400002F RID: 47
		[ClockWorkWebPage(eClockWorkWebPageModule.AppointmentBooking, "FAQ", "Help.aspx", false)]
		AppointmentBooking_FAQ,
		// Token: 0x04000030 RID: 48
		[ClockWorkWebPage(eClockWorkWebPageModule.AppointmentBooking, "Submit a comment", "submitcomment.aspx", false, IsSubmitCommentPage = true)]
		AppointmentBooking_SubmitComment,
		// Token: 0x04000031 RID: 49
		[ClockWorkWebPage(eClockWorkWebPageModule.AppointmentBooking, "Help", "default.aspx", false)]
		AppointmentBooking_Help,
		// Token: 0x04000032 RID: 50
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingNotetakers, "Courses / notes", "notetakerapp.aspx", true)]
		NotetakingNotetakers_Courses,
		// Token: 0x04000033 RID: 51
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingNotetakers, "Profile", "profile.aspx", false)]
		NotetakingNotetakers_Profile,
		// Token: 0x04000034 RID: 52
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingNotetakers, "FAQ", "help.aspx", false)]
		NotetakingNotetakers_FAQ,
		// Token: 0x04000035 RID: 53
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingNotetakers, "Submit a comment", "SubmitComment.aspx", false, IsSubmitCommentPage = true)]
		NotetakingNotetakers_SubmitComment,
		// Token: 0x04000036 RID: 54
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingNotetakers, "Help", "default.aspx", false)]
		NotetakingNotetakers_Help,
		// Token: 0x04000037 RID: 55
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingStudents, "Courses / notes", "courses.aspx", true)]
		NotetakingStudents_Courses,
		// Token: 0x04000038 RID: 56
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingStudents, "FAQ", "help.aspx", false)]
		NotetakingStudents_FAQ,
		// Token: 0x04000039 RID: 57
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingStudents, "Submit a comment", "SubmitComment.aspx", false, IsSubmitCommentPage = true)]
		NotetakingStudents_SubmitComment,
		// Token: 0x0400003A RID: 58
		[ClockWorkWebPage(eClockWorkWebPageModule.NotetakingStudents, "Help", "default.aspx", false)]
		NotetakingStudents_Help,
		// Token: 0x0400003B RID: 59
		[ClockWorkWebPage(eClockWorkWebPageModule.WorkshopBooking, "Available workshops", "workshops.aspx", true)]
		WorkshopBooking_AvailableWorkshops,
		// Token: 0x0400003C RID: 60
		[ClockWorkWebPage(eClockWorkWebPageModule.WorkshopBooking, "My calendar", "MyUpcomingAppts.aspx", false)]
		WorkshopBooking_Calendar,
		// Token: 0x0400003D RID: 61
		[ClockWorkWebPage(eClockWorkWebPageModule.WorkshopBooking, "Submit a comment", "SubmitComment.aspx", false, IsSubmitCommentPage = true)]
		WorkshopBooking_SubmitComment,
		// Token: 0x0400003E RID: 62
		[ClockWorkWebPage(eClockWorkWebPageModule.WorkshopBooking, "Help", "default.aspx", false)]
		WorkshopBooking_Help,
		// Token: 0x0400003F RID: 63
		[ClockWorkWebPage(eClockWorkWebPageModule.SelfRegistration, "Accommodations", "courses.aspx", true)]
		SelfRegistration_Accommodations,
		// Token: 0x04000040 RID: 64
		[ClockWorkWebPage(eClockWorkWebPageModule.SelfRegistration, "FAQ", "help.aspx", false)]
		SelfRegistration_FAQ,
		// Token: 0x04000041 RID: 65
		[ClockWorkWebPage(eClockWorkWebPageModule.SelfRegistration, "Submit a comment", "SubmitComment.aspx", false, IsSubmitCommentPage = true)]
		SelfRegistration_SubmitComment,
		// Token: 0x04000042 RID: 66
		[ClockWorkWebPage(eClockWorkWebPageModule.SelfRegistration, "Help", "default.aspx", false)]
		SelfRegistration_Help,
		// Token: 0x04000043 RID: 67
		[ClockWorkWebPage(eClockWorkWebPageModule.Instructor, "Courses", "courses.aspx", true)]
		Instructor_Courses,
		// Token: 0x04000044 RID: 68
		[ClockWorkWebPage(eClockWorkWebPageModule.Instructor, "Accommodation letters", "letters.aspx", false)]
		Instructor_AccommodationLetters,
		// Token: 0x04000045 RID: 69
		[ClockWorkWebPage(eClockWorkWebPageModule.Instructor, "Help", "default.aspx", false)]
		Instructor_Help,
		// Token: 0x04000046 RID: 70
		[ClockWorkWebPage(eClockWorkWebPageModule.TestBooking, "Schedule a test, mid-term or quiz", "book.aspx", false)]
		TestBooking_BookTest,
		// Token: 0x04000047 RID: 71
		[ClockWorkWebPage(eClockWorkWebPageModule.TestBooking, "Schedule a final exam", "bookexam.aspx", false)]
		TestBooking_BookExam,
		// Token: 0x04000048 RID: 72
		[ClockWorkWebPage(eClockWorkWebPageModule.TestBooking, "My upcoming events", "myupcomingappts.aspx", true)]
		TestBooking_Calendar,
		// Token: 0x04000049 RID: 73
		[ClockWorkWebPage(eClockWorkWebPageModule.TestBooking, "Accommodations", "accommodationsletters.aspx", false)]
		TestBooking_Accommodations,
		// Token: 0x0400004A RID: 74
		[ClockWorkWebPage(eClockWorkWebPageModule.TestBooking, "Help", "default.aspx", false)]
		TestBooking_Help
	}
}
