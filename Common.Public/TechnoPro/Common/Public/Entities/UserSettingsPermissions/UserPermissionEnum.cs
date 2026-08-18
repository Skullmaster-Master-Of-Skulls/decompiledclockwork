using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	public enum UserPermissionEnum
	{
		// Token: 0x040002F8 RID: 760
		[UserPermission("View other people's schedules", UserPermissionGroup.USERSANDSTUDENTS)]
		ViewOthersSchedlue = -1,
		// Token: 0x040002F9 RID: 761
		[UserPermission("See student list", UserPermissionGroup.USERSANDSTUDENTS)]
		SeeStudentList = -2,
		// Token: 0x040002FA RID: 762
		[UserPermission("See staff list", UserPermissionGroup.USERSANDSTUDENTS)]
		SeeStaffList = -3,
		// Token: 0x040002FB RID: 763
		[Obsolete("Use AddLookupCourse instead")]
		[UserPermission("Add custom lookup courses", UserPermissionGroup.DISABILITY, IsHidden = true)]
		AddCustomLUCourses = 1,
		// Token: 0x040002FC RID: 764
		[UserPermission("Book appointments", UserPermissionGroup.APPOINTMENTS)]
		BookAppointments = -4,
		// Token: 0x040002FD RID: 765
		[UserPermission("Modify appointments", UserPermissionGroup.APPOINTMENTS)]
		ModifyAppointments = -5,
		// Token: 0x040002FE RID: 766
		[UserPermission("Delete appointments", UserPermissionGroup.APPOINTMENTS)]
		DeleteAppointments = -6,
		// Token: 0x040002FF RID: 767
		[UserPermission("Add students", UserPermissionGroup.USERSANDSTUDENTS)]
		AddStudent = -7,
		// Token: 0x04000300 RID: 768
		[UserPermission("Edit students", UserPermissionGroup.USERSANDSTUDENTS)]
		EditStudent = -8,
		// Token: 0x04000301 RID: 769
		[UserPermission("See groups (not used)", UserPermissionGroup.APPOINTMENTS, IsHidden = true)]
		SeeGroup = -9,
		// Token: 0x04000302 RID: 770
		[UserPermission("See person (not used)", UserPermissionGroup.APPOINTMENTS, IsHidden = true)]
		SeePerson = -10,
		// Token: 0x04000303 RID: 771
		[UserPermission("Send instant messages", UserPermissionGroup.USERSANDSTUDENTS)]
		SendSocketMessages = -11,
		// Token: 0x04000304 RID: 772
		[UserPermission("View workshop group in workshop screen", UserPermissionGroup.EVENTSANDWORKSHOPS, PermissionSemantic = PermissionSemantic.Workshop)]
		ViewWorkshopGroupInWorkshopsScreen = -12,
		// Token: 0x04000305 RID: 773
		[UserPermission("Use notetaking scanner software", UserPermissionGroup.DISABILITY)]
		UseNotetakingScannerSoftware = -13,
		// Token: 0x04000306 RID: 774
		[UserPermission("Import from kiosk", UserPermissionGroup.USERSANDSTUDENTS)]
		ImportFromKiosk = -14,
		// Token: 0x04000307 RID: 775
		[UserPermission("Create an appointment with a blank description", UserPermissionGroup.APPOINTMENTS)]
		CreateModifyAppWithNoAppType = -15,
		// Token: 0x04000308 RID: 776
		[UserPermission("Generate accommodation letters", UserPermissionGroup.DISABILITY)]
		DenyGenerateAccommodationLetters = -16,
		// Token: 0x04000309 RID: 777
		[UserPermission("Assign notetakees", UserPermissionGroup.DISABILITY)]
		DenyAssignNotetakee = -17,
		// Token: 0x0400030A RID: 778
		[UserPermission("Mark accommodation letter returned date", UserPermissionGroup.DISABILITY)]
		DenyMarkDateLetterReturned = -18,
		// Token: 0x0400030B RID: 779
		[UserPermission("Change 'Show time as' drop-list value", UserPermissionGroup.APPOINTMENTS)]
		DenyChangeShowTimeAs = -19,
		// Token: 0x0400030C RID: 780
		[UserPermission("Book 'multiple' appointments", UserPermissionGroup.APPOINTMENTS, Description = "Recurring appointments ('multiple' tab on the appointment edit box)")]
		BookMultipleAppointments = -20,
		// Token: 0x0400030D RID: 781
		[UserPermission("Delete Point of Contact", UserPermissionGroup.APPOINTMENTS)]
		DeletePOC = -25,
		// Token: 0x0400030E RID: 782
		[UserPermission("Delete appointments the user didn't create", UserPermissionGroup.APPOINTMENTS)]
		DeleteAppointmentsIDidntCreate = -26,
		// Token: 0x0400030F RID: 783
		[UserPermission("Modify the user's own availability schedule", UserPermissionGroup.APPOINTMENTS)]
		ModifyOwnAvailabilitySchedule = -27,
		// Token: 0x04000310 RID: 784
		[UserPermission("Allowed to edit student name and number", UserPermissionGroup.USERSANDSTUDENTS)]
		EditStudentNameAndNumber = -28,
		// Token: 0x04000311 RID: 785
		[UserPermission("See student's alternate format information", UserPermissionGroup.DISABILITY)]
		ViewStudentsAltFormatInfo = -29,
		// Token: 0x04000312 RID: 786
		[UserPermission("View form", UserPermissionGroup.FORMS, IsHidden = false, PermissionSemantic = PermissionSemantic.Form)]
		ViewScreen = 2,
		// Token: 0x04000313 RID: 787
		[UserPermission("Modify form", UserPermissionGroup.FORMS, IsHidden = false, PermissionSemantic = PermissionSemantic.Form)]
		ModifyScreen,
		// Token: 0x04000314 RID: 788
		[UserPermission("Use the ClockWork Admin software", UserPermissionGroup.CLOCKWORKADMIN)]
		UseAdminProgram,
		// Token: 0x04000315 RID: 789
		[UserPermission("Delete students", UserPermissionGroup.USERSANDSTUDENTS)]
		DeleteStudent,
		// Token: 0x04000316 RID: 790
		[UserPermission("Create form", UserPermissionGroup.FORMS, IsHidden = false, PermissionSemantic = PermissionSemantic.Form)]
		CreateScreen,
		// Token: 0x04000317 RID: 791
		[UserPermission("Add a workshop", UserPermissionGroup.EVENTSANDWORKSHOPS)]
		AddWorkshop,
		// Token: 0x04000318 RID: 792
		[UserPermission("Delete a workshop", UserPermissionGroup.EVENTSANDWORKSHOPS)]
		DeleteWorkshop,
		// Token: 0x04000319 RID: 793
		[UserPermission("View students courses", UserPermissionGroup.DISABILITY)]
		ViewCourses,
		// Token: 0x0400031A RID: 794
		[UserPermission("View students accommodations", UserPermissionGroup.DISABILITY)]
		ViewAccommodations,
		// Token: 0x0400031B RID: 795
		[UserPermission("View the service providers system", UserPermissionGroup.DISABILITY)]
		ViewNotetaking,
		// Token: 0x0400031C RID: 796
		[UserPermission("View tutoring", UserPermissionGroup.DISABILITY, IsHidden = true)]
		ViewTutoring,
		// Token: 0x0400031D RID: 797
		[UserPermission("View staff information", UserPermissionGroup.USERSANDSTUDENTS)]
		ViewStaffInfo,
		// Token: 0x0400031E RID: 798
		[Obsolete("Use AddLookupCourse instead")]
		[UserPermission("Add a lookup course to ClockWork", UserPermissionGroup.DISABILITY, IsHidden = true)]
		AddCourseManually,
		// Token: 0x0400031F RID: 799
		[UserPermission("Manage cross-listed courses", UserPermissionGroup.DISABILITY)]
		ManageCrossListedCourses,
		// Token: 0x04000320 RID: 800
		[UserPermission("Use the TechnoPro Send Email software", UserPermissionGroup.USERSANDSTUDENTS)]
		UseTPEmailer,
		// Token: 0x04000321 RID: 801
		[Obsolete("Deprecated")]
		[UserPermission("Use Student Files", UserPermissionGroup.USERSANDSTUDENTS, IsHidden = true)]
		UseStudentFiles,
		// Token: 0x04000322 RID: 802
		[UserPermission("Use tests screen", UserPermissionGroup.DISABILITY)]
		UseTestsScreen,
		// Token: 0x04000323 RID: 803
		[UserPermission("Use contacts (unused)", UserPermissionGroup.USERSANDSTUDENTS, IsHidden = true)]
		UseContacts,
		// Token: 0x04000324 RID: 804
		[UserPermission("Allow non-admin disabling of Student Alerts (Trigger system)", UserPermissionGroup.USERSANDSTUDENTS)]
		DisableStudentAlerts,
		// Token: 0x04000325 RID: 805
		[UserPermission("View other staff members 'Student's I've had appointments with'", UserPermissionGroup.USERSANDSTUDENTS)]
		ViewOtherStaff_StudentsInMyAppsListing,
		// Token: 0x04000326 RID: 806
		[UserPermission("Use ClockWork Reports", UserPermissionGroup.MISC)]
		UseReports,
		// Token: 0x04000327 RID: 807
		[UserPermission("Modify availability schedule types", UserPermissionGroup.APPOINTMENTS)]
		ModifyAvailabilityGroups,
		// Token: 0x04000328 RID: 808
		[UserPermission("View appointment modification history", UserPermissionGroup.APPOINTMENTS)]
		ViewAppointmentModificationsHistory = 25,
		// Token: 0x04000329 RID: 809
		[UserPermission("Manually import and update students from external source", UserPermissionGroup.USERSANDSTUDENTS)]
		ManuallyImportAndUpdateStudents,
		// Token: 0x0400032A RID: 810
		[UserPermission("Modify accommodation templates", UserPermissionGroup.DISABILITY)]
		ModifyAccommodationTemplates,
		// Token: 0x0400032B RID: 811
		[UserPermission("Merge two student accounts", UserPermissionGroup.USERSANDSTUDENTS)]
		MergeTwoStudents,
		// Token: 0x0400032C RID: 812
		[UserPermission("Use an Admin section - Users and Resources", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_UsersResources,
		// Token: 0x0400032D RID: 813
		[UserPermission("Use an Admin section - Appointments", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_Appointments,
		// Token: 0x0400032E RID: 814
		[UserPermission("Use an Admin section - Courses", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_Courses,
		// Token: 0x0400032F RID: 815
		[UserPermission("Use an Admin section - Forms", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_Forms,
		// Token: 0x04000330 RID: 816
		[UserPermission("Use an Admin section - Security", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_Security,
		// Token: 0x04000331 RID: 817
		[UserPermission("Use an Admin section - Miscelleanous", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_Misc,
		// Token: 0x04000332 RID: 818
		[UserPermission("Use an Admin section - Settings", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_StudentWeb,
		// Token: 0x04000333 RID: 819
		[UserPermission("Use an Admin section - Manage students", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_ManageStudents,
		// Token: 0x04000334 RID: 820
		[UserPermission("Edit mail merge templates", UserPermissionGroup.USERSANDSTUDENTS)]
		EditMailMergeTemplates,
		// Token: 0x04000335 RID: 821
		[UserPermission("Change current session chooser default for everyone", UserPermissionGroup.MISC)]
		ChangeCurrentSessionChooserDefaultForEveryone,
		// Token: 0x04000336 RID: 822
		[UserPermission("Use an Admin section - Diagnostic", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_Diagnostic,
		// Token: 0x04000337 RID: 823
		[UserPermission("Use an Admin section - Software Updates", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_SoftwareUpdates,
		// Token: 0x04000338 RID: 824
		[UserPermission("Use an Admin section - Inventory", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_Inventory,
		// Token: 0x04000339 RID: 825
		[UserPermission("Use an Admin section - ClockWork Main Settings", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_ClockWorkMainSettings,
		// Token: 0x0400033A RID: 826
		[UserPermission("Use an Admin section - Tools menu items", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_ToolsMenuItems,
		// Token: 0x0400033B RID: 827
		[UserPermission("Use an Admin section - Manage rooms", UserPermissionGroup.CLOCKWORKADMIN)]
		Admin_ManageRooms,
		// Token: 0x0400033C RID: 828
		[UserPermission("Edit lookup course info", UserPermissionGroup.DISABILITY)]
		EditLookupCourse = -31,
		// Token: 0x0400033D RID: 829
		[UserPermission("Add a lookup course to ClockWork", UserPermissionGroup.DISABILITY)]
		AddLookupCourse = -32,
		// Token: 0x0400033E RID: 830
		[UserPermission("Use the File Uploads Queue (staff interface)", UserPermissionGroup.USERSANDSTUDENTS)]
		UseFileUploadsQueue = 45,
		// Token: 0x0400033F RID: 831
		[UserPermission("Use the Online Forms Queue (staff interface)", UserPermissionGroup.USERSANDSTUDENTS)]
		UseOnlineFormsQueue,
		// Token: 0x04000340 RID: 832
		[UserPermission("Use the Survey Queue (staff interface)", UserPermissionGroup.USERSANDSTUDENTS, IsHidden = true)]
		UseSurveyQueue
	}
}
