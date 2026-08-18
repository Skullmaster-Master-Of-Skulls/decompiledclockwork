using System;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D9 RID: 473
	[Serializable]
	public enum Group
	{
		// Token: 0x04000957 RID: 2391
		[GroupData("Appointment booking", IconName = "clock", Description = "Settings for the online appointment booking module.", DefaultSignatureSetting = 50149, DefaultFromSetting = 50142, LicensingProductName = "")]
		APPOINTMENTBOOKING = 110000,
		// Token: 0x04000958 RID: 2392
		[GroupData("Clubs", IconName = "id_card2", Description = "Clubs group", LicensingProductName = "")]
		CLUBS = 80000,
		// Token: 0x04000959 RID: 2393
		[GroupData("General", IconName = "label", Description = "General system-wide settings that are not specific to any module.", LicensingProductName = "")]
		GENERAL = 50000,
		// Token: 0x0400095A RID: 2394
		[GroupData("Instructor", IconName = "book_blue", Description = "Settings for the online instructor test/exam and accommodation iterface.", DefaultSignatureSetting = 50147, DefaultFromSetting = 50140, LicensingProductName = "")]
		INSTRUCTOR = 130000,
		// Token: 0x0400095B RID: 2395
		[GroupData("Kiosk", IconName = "laptop2", Description = "Kiosk group", IsActive = false, LicensingProductName = "")]
		KIOSK = 150000,
		// Token: 0x0400095C RID: 2396
		[GroupData("LDAP", IsActive = false, Description = "LDAP group", LicensingProductName = "")]
		LDAP = 40000,
		// Token: 0x0400095D RID: 2397
		[GroupData("Log", IsActive = false, Description = "Log group", LicensingProductName = "")]
		LOG = 60000,
		// Token: 0x0400095E RID: 2398
		[GroupData("Login", IconName = "key1", Description = "Login settings", LicensingProductName = "")]
		LOGIN = 20000,
		// Token: 0x0400095F RID: 2399
		[GroupData("Modules", IconName = "cubes", LicensingProductName = "", Description = "Settings relevant to modules, such as which modules are currently enabled and custom module messages.")]
		MODULES = 10000,
		// Token: 0x04000960 RID: 2400
		[GroupData("Note taking old", IsActive = false, Description = "Note taking group", LicensingProductName = "Online Notetaking")]
		NOTETAKING = 120000,
		// Token: 0x04000961 RID: 2401
		[GroupData("Note taking", IconName = "notebook", Description = "Settings for the online notetaker and notetakee modules", DefaultSignatureSetting = 50146, DefaultFromSetting = 50139, LicensingProductName = "Online Notetaking")]
		NOTETAKINGB = 90000,
		// Token: 0x04000962 RID: 2402
		[Obsolete("Use SELFREGC instead")]
		[GroupData("Self registration", IconName = "preferences", Description = "", IsActive = false, LicensingProductName = "On-Line Self Registration")]
		SELFREGISTRATION = 100000,
		// Token: 0x04000963 RID: 2403
		[GroupData("Test booking", IconName = "form_blue", Description = "Settings for the online test and final exam booking system for students", DefaultSignatureSetting = 50145, DefaultFromSetting = 50138, LicensingProductName = "Online Test/Exam Booking")]
		TESTBOOKING = 30000,
		// Token: 0x04000964 RID: 2404
		[GroupData("Unknown", IsActive = false, Description = "Unknown group", LicensingProductName = "")]
		UNKNOWN = 0,
		// Token: 0x04000965 RID: 2405
		[GroupData("Workshops", IconName = "calendar", Description = "Settings for the online workshop and event booking module", DefaultSignatureSetting = 50148, DefaultFromSetting = 50141, LicensingProductName = "Online Workshop Booking")]
		WORKSHOPS = 140000,
		// Token: 0x04000966 RID: 2406
		[GroupData("Note taking scanner", Description = "Note taking scanner group", IconName = "scanner", IsActive = false, LicensingProductName = "")]
		NOTETAKINGSCANNER = 210000,
		// Token: 0x04000967 RID: 2407
		[GroupData("Custom", IconName = "briefcase_document", Description = "Settings for custom use", LicensingProductName = "")]
		CUSTOM = 10000000,
		// Token: 0x04000968 RID: 2408
		[GroupData("Accommodations", IconName = "view", Description = "Settings for the online student accommodations module", DefaultSignatureSetting = 50154, DefaultFromSetting = 50153, LicensingProductName = "")]
		ACCOMMODATIONS = 220000,
		// Token: 0x04000969 RID: 2409
		[GroupData("Exam booking", IconName = "form_red", Description = "Test booking group", DefaultSignatureSetting = 50145, DefaultFromSetting = 50138, LicensingProductName = "Online Test/Exam Booking")]
		EXAMBOOKING = 230000,
		// Token: 0x0400096A RID: 2410
		[GroupData("Other", Description = "Other group", IsActive = false, LicensingProductName = "")]
		OTHER = 24000,
		// Token: 0x0400096B RID: 2411
		[GroupData("Inventory System", Description = "Inventory system group", IconName = "clipboard", LicensingProductName = "Inventory System")]
		INVENTORYSYSTEM = 250000,
		// Token: 0x0400096C RID: 2412
		[GroupData("Staff", Description = "Staff", IconName = "key1", LicensingProductName = "")]
		STAFF = 260000,
		// Token: 0x0400096D RID: 2413
		[GroupData("Test Booking Non-Accommodated", Description = "Test Booking Non-Accommodated", IconName = "form_red", DefaultSignatureSetting = 50145, DefaultFromSetting = 50138, LicensingProductName = "", IsActive = false)]
		TESTBOOKINGALT = 270000,
		// Token: 0x0400096E RID: 2414
		[GroupData("ClockWork Server", Description = "ClockWork Server Application group", IconName = "server_time", LicensingProductName = "")]
		CLOCKWORKSERVER = 280000,
		// Token: 0x0400096F RID: 2415
		[GroupData("ClockWork Appointment Sync", Description = "ClockWork Appointment Sync Settings", IconName = "clock", LicensingProductName = "")]
		CLOCKWORKAPPOINTMENTSYNC = 290000,
		// Token: 0x04000970 RID: 2416
		[GroupData("Intake Registration", Description = "ClockWork Intake Registration Settings", IconName = "preferences", DefaultSignatureSetting = 50156, DefaultFromSetting = 50155, LicensingProductName = "Online Intake")]
		INTAKE = 300000,
		// Token: 0x04000971 RID: 2417
		[GroupData("Self Registration", Description = "Settings for the online self-registration module, for returning students to re-activate their accommodations in a new term or school year.", IconName = "form_blue", DefaultSignatureSetting = 50150, DefaultFromSetting = 50143, LicensingProductName = "On-Line Self Registration")]
		SELFREGC = 310000,
		// Token: 0x04000972 RID: 2418
		[GroupData("Automatic updating system", Description = "Automatic updating settings for Database, ClockWorkServer, Web modules and ClockWork client applications", IconName = "box_software", LicensingProductName = "")]
		AUTOMATICUPDATING = 320000,
		// Token: 0x04000973 RID: 2419
		[GroupData("Veterans", Description = "Settings for the Veterans module", IconName = "form_red", DefaultSignatureSetting = 50157, DefaultFromSetting = 50158, LicensingProductName = "")]
		VETERANS = 330000,
		// Token: 0x04000974 RID: 2420
		[GroupData("Tutoring", Description = "Tutor module settings", IconName = "form_blue", DefaultSignatureSetting = 50159, DefaultFromSetting = 50160, LicensingProductName = "")]
		TUTORING = 340000,
		// Token: 0x04000975 RID: 2421
		[GroupData("Alternate format", Description = "Settings for the alternate format textbooks and media module", IconName = "form_blue", LicensingProductName = "")]
		ALTERNATEFORMAT = 350000,
		// Token: 0x04000976 RID: 2422
		[GroupData("Surveys", Description = "Settings for the survey module", IconName = "form_blue", DefaultSignatureSetting = 50151, DefaultFromSetting = 50144, LicensingProductName = "On-Line Survey")]
		SURVEYS = 360000,
		// Token: 0x04000977 RID: 2423
		[GroupData("Required form", Description = "Settings for setting up a required form the student has to fill in once each session.", IconName = "form_blue", DefaultSignatureSetting = 50163, DefaultFromSetting = 50164, LicensingProductName = "Required Forms")]
		REQUIREDSESSIONFORM = 370000,
		// Token: 0x04000978 RID: 2424
		[GroupData("Student files", Description = "Settings for setting up student access to their files online.", IconName = "form_blue", DefaultSignatureSetting = 50165, DefaultFromSetting = 50166, LicensingProductName = "Student Files")]
		STUDENTFILES = 380000,
		// Token: 0x04000979 RID: 2425
		[GroupData("Online forms", Description = "Settings for the online forms module", IconName = "form_green", DefaultSignatureSetting = 50169, DefaultFromSetting = 50168, LicensingProductName = "Online Forms")]
		ONLINEFORMS = 390000
	}
}
