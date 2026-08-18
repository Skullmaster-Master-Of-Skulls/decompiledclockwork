using System;
using TechnoPro.Common.Public.Entities.SettingsPermissionsGeneral;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	public enum Setting
	{
		// Token: 0x0400097B RID: 2427
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.ALTERNATEFORMAT, SettingSemantic.DATETIME, IsHidden = true)]
		ALTERNATEFORMAT_LastModifiedTime = 350000,
		// Token: 0x0400097C RID: 2428
		[SettingData("Notification email sent to student when an alternate media request is completed", "Emails", "This email is sent when an alternate media request is completed", Group.ALTERNATEFORMAT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~from~#</from>\r\n     <to>#~studentemail~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Alternate Format]: Your alternate media request has been completed </subject>\r\n     <attachments></attachments>\r\n     <body>Hi #~firstname~# #~lastname~#,\r\n\r\nYour alternate media request for '#~alternateformatrequestmediacontenttitle~#' in #~alternateformatrequestmediacontentformat~# format has been completed on #~alternateformatrequestcompleteddatetime~# and it is ready to download. \r\n\r\nPlease go to Alternate Media Online and download it.\r\n\r\nThanks\r\n\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		ALTERNATEFORMAT_Email_ReadyToDownloadFileStudentNotification = 350003,
		// Token: 0x0400097D RID: 2429
		[SettingData("Welcome message for the Alternate Format", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Welcome to the alternate format website. You can use this website to:</p>\r\n        <ul>\r\n            <li>Create <a href='../StudentRequests/NewRequest'>new request</a></li>\r\n            <li>View <a href='../StudentRequests/List'>my requests</a></li>\r\n            <li>Download <a href='../StudentFiles/List'>my files</a></li>\r\n        </ul>\r\n        <p>Please select an option from the menu in order to get started. You will be asked to login using your school login account.</p>")]
		ALTERNATEFORMAT_WelcomePageText,
		// Token: 0x0400097E RID: 2430
		[SettingData("Welcome title text for the Alternate Format", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "Welcome to Alternate Format")]
		ALTERNATEFORMAT_WelcomePageTitleText,
		// Token: 0x0400097F RID: 2431
		[SettingData("Requests submitted thank you message", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Thank you for submitting your alternate format requests. All of your requests were submitted successfully. You will receive a confirmation email promptly with the details.</p>")]
		ALTERNATEFORMAT_RequestsSubmittedThankYouText,
		// Token: 0x04000980 RID: 2432
		[SettingData("Requests submitted title message", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "Requests submitted result")]
		ALTERNATEFORMAT_RequestsSubmittedThankYouTitleText,
		// Token: 0x04000981 RID: 2433
		[SettingData("Alternate Format student files page title", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "My files")]
		ALTERNATEFORMAT_StudentFilesPageTitleText,
		// Token: 0x04000982 RID: 2434
		[SettingData("Alternate Format student files description", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Please select a session to show your available Alternate Format files. You can download a media content in a specific format or download all formats for the media content.</p>")]
		ALTERNATEFORMAT_StudentFilesPageText,
		// Token: 0x04000983 RID: 2435
		[SettingData("New student request by course page title", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "Request by course")]
		ALTERNATEFORMAT_StudentRequestByCoursePageTitleText,
		// Token: 0x04000984 RID: 2436
		[SettingData("New student request by course page description", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Please select a session and course to show all media content by course in order of creating Alternate Format requests.</p>")]
		ALTERNATEFORMAT_StudentRequestByCoursePageText,
		// Token: 0x04000985 RID: 2437
		[SettingData("Student request list page title", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "My requests")]
		ALTERNATEFORMAT_StudentRequestsPageTitleText,
		// Token: 0x04000986 RID: 2438
		[SettingData("Student request list page description", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Please select a session to display all your content requested during this session.</p>")]
		ALTERNATEFORMAT_StudentRequestsPageText,
		// Token: 0x04000987 RID: 2439
		[SettingData("New student request by searching page title", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "Request by searching")]
		ALTERNATEFORMAT_StudentRequestBySearchingPageTitleText,
		// Token: 0x04000988 RID: 2440
		[SettingData("New student request by searching page description", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Please search our media content database and Web by content title or ISBN. Create new request from your results.</p>")]
		ALTERNATEFORMAT_StudentRequestBySearchingPageText,
		// Token: 0x04000989 RID: 2441
		[SettingData("Student confidentiality agreement", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<h3>Website Privacy & Security Policy</h3>\r\n\r\n<p>We are committed to ensuring the privacy and accuracy of your confidential information. We have the utmost respect for your privacy and will not share your personal information with anyone without your explicit permission. All services provided on this Website are alternatively available in person.</p>\r\n\r\n<h4>Information we collect about you</h4>\r\n================================\r\n<p>We will only collect and process your personal data for the purposes of providing the services delivered by this Website.  In addition some information is automatically collected and stored in the server logs, such as your Ip address.  Providing personal data is voluntary.  There will be a minimum data that we need to collect from you for the services that you sign up to.  We will let you know what data we require, if you wish to use our services, by indicating in the relevant fields of the webforms.</p>\r\n\r\n<h4>Statistics</h4>\r\n==========\r\n<p>The Website is regularly monitored in order to supply you with the best service and to meet your expectations. For this purpose, we consult the statistics relating to use of our Website and develop the Website on the basis of this data.  Your information may also be used in our reports. User statistics are anonymous.</p>\r\n\r\n<h4>Security</h4>\r\n========\r\n<p>The Website uses a secure server to protect your information data. Secure server software is used to encrypt the information exchanged between your Web browser and our Website. This measure ensures the security of all your transactions when you use the Sites. We follow strict security procedures when filing and using the information you supply, and may request proof of your identity before supplying you with information. We take all reasonable steps to ensure the secrecy of your personal data and passwords.\r\nYou are fully responsible for maintaining the confidentiality of your login and your password and abstaining from communicating it to any other person and you are solely liable for activities that occur under your login and password. We disclaim all liabilities for inaccuracy of your personal data and in case of theft, loss, misuse, communication, fraudulent use of your login and password arising from your failure to comply with the above.</p>\r\n\r\n<h4>Cookies</h4>\r\n=======\r\n<p>The Website may use cookies to ensure the smooth operation of your transactions.  Cookies are small information files that a Website can send to the hard disk of a personal computer for traceability reasons. They are not executable programs, and cannot contain viruses or applications. The cookies used only take up a minimal amount of space on your hard disk. You can always prevent cookies from being recorded on your computer by using the options provided by your browser. However, if you do so, some parts of the Site may not be functional.</p>")]
		ALTERNATEFORMAT_StudentConfidentialityAgreementText,
		// Token: 0x0400098A RID: 2442
		[SettingData("Student confidentiality agreement page title", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "Alternate format student confidentiality agreement")]
		ALTERNATEFORMAT_StudentConfidentialityAgreementPageTitleText,
		// Token: 0x0400098B RID: 2443
		[SettingData("Student confidentiality agreement page description", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Please agree with the confidentiality agreement below to start using Alternate Format.</p>")]
		ALTERNATEFORMAT_StudentConfidentialityAgreementPageText,
		// Token: 0x0400098C RID: 2444
		[SettingData("Requests submitted failed message", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Thank you for submitting your alternate format requests. You will receive a confirmation email promptly with the details.\r\nUnfortunately the following requests failed when submitting. They have been kept in your pending requests list. Please try again later.</p>")]
		ALTERNATEFORMAT_RequestsSubmittedFailedText = 3500019,
		// Token: 0x0400098D RID: 2445
		[SettingData("New media content request page title", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "New media content request")]
		ALTERNATEFORMAT_NewMediaContentRequestPageTitleText = 350020,
		// Token: 0x0400098E RID: 2446
		[SettingData("New media content request page description", "Display", "", Group.ALTERNATEFORMAT, SettingSemantic.HTML, DefaultValue = "<p>Please fill all the fields below to create a new media content request.</p>")]
		ALTERNATEFORMAT_NewMediaContentRequestPageText,
		// Token: 0x0400098F RID: 2447
		[SettingData("Notification email sent to student when proof of purchase receipt is rejected", "Emails", "This email is sent to students when proof of purchase receipt is rejected", Group.ALTERNATEFORMAT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~from~#</from>\r\n     <to>#~studentemail~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Alternate Format]: Proof of Purchase Receipt rejected </subject>\r\n     <attachments></attachments>\r\n     <body>Hi #~firstname~# #~lastname~#,\r\n\r\nProof of Purchase receipt for media content title '#~mediacontenttitle~#' has been rejected. Please submit a valid proof of purchase receipt for the book.\r\n\r\nThanks\r\n\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		ALTERNATEFORMAT_Email_ProofOfPurchaseReceiptRejectedNotification,
		// Token: 0x04000990 RID: 2448
		[SettingData("Notification email sent to student when a file is ready but it is waiting for proof of purchase", "Emails", "This email is sent to student when a file is ready but pending of proof of purchase", Group.ALTERNATEFORMAT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~from~#</from>\r\n     <to>#~studentemail~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Alternate Format]: A file is pending for a Proof of Purchase Receipt</subject>\r\n     <attachments></attachments>\r\n     <body>Hi #~firstname~# #~lastname~#,\r\n\r\nAlternate media file for '#~alternateformatrequestmediacontenttitle~#' in #~alternateformatrequestmediacontentformat~# format has been completed on #~alternateformatrequestcompleteddatetime~# but is pending of a Proof of Purchase Receipt to be able to download. Please submit a Proof of Purchase Receipt.\r\n\r\nThanks\r\n\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		ALTERNATEFORMAT_Email_FilePendingOfProofOfPurchaseStudentNotification,
		// Token: 0x04000991 RID: 2449
		[SettingData("Notification email sent to student after receiving their Alternate Media requests", "Emails", "This email is sent to student just after receiving their Alternate Media requests", Group.ALTERNATEFORMAT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~from~#</from>\r\n     <to>#~studentemail~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Alternate Format]: Alternate Media requests received</subject>\r\n     <attachments></attachments>\r\n     <body>Hi #~firstname~# #~lastname~#,\r\n\r\nWe have received your requests for the following Alternate Media:\r\n\r\n#~alternatemediacontentlist~#\r\n\r\nThanks\r\n\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		ALTERNATEFORMAT_Email_StudentAlternateMediaRequestsNotification,
		// Token: 0x04000992 RID: 2450
		[SettingData("Alternate format accommodation control ids", "Accommodation template form control id(s) for student alternate format accommodation", Group.ALTERNATEFORMAT, SettingSemantic.CONTROLIDS_ACCOMMODATIONS)]
		ALTERNATEFORMAT_Accommodation_Template_Control_Id,
		// Token: 0x04000993 RID: 2451
		[SettingData("Allow students to select their preferred format type when submitting a request", "Preferred format type", "Enabling this will force the student to select a preferred format type each time they submit a request.  Ensure that the setting 'Accommodation to format-types mappings' is completely filled in if using this system.", Group.ALTERNATEFORMAT, SettingSemantic.BOOLEAN, DefaultValue = false)]
		ALTERNATEFORMAT_AllowStudentsToSelectPreferredFormatTypeWhenSubmittingAltFormatRequest,
		// Token: 0x04000994 RID: 2452
		[SettingData("Accommodation to format-types mappings", "Preferred format type", "Indicate which format-type(s) are associated with each accommodation.  Note: only active if 'Allow students to select their preferred format type when submitting a request' system is enabled.", Group.ALTERNATEFORMAT, SettingSemantic.ACCOMMODATIONS_ALTFORMATTYPES_MAPPINGS)]
		ALTERNATEFORMAT_Accommodation_to_FormatTypes_Mappings,
		// Token: 0x04000995 RID: 2453
		[SettingData("Notification email sent to student when an alternate media request is cancelled", "Emails", "This email is sent when an alternate media request is cancelled", Group.ALTERNATEFORMAT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~from~#</from>\r\n     <to>#~studentemail~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Alternate Format]: Your alternate media request has been cancelled </subject>\r\n     <attachments></attachments>\r\n     <body>Hi #~firstname~# #~lastname~#,\r\n\r\nYour alternate media request for '#~alternateformatrequestmediacontenttitle~#' in #~alternateformatrequestmediacontentformat~# format has been cancelled on #~alternateformatrequestcompleteddatetime~#. \r\n\r\nIf you have any question, please do not hesitate to contact your department.\r\n\r\nThanks\r\n\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		ALTERNATEFORMAT_Email_CancelledStudentRequestNotification,
		// Token: 0x04000996 RID: 2454
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.TUTORING, SettingSemantic.DATETIME, IsHidden = true)]
		TUTORING_LastModifiedTime = 340000,
		// Token: 0x04000997 RID: 2455
		[ReferenceSetting("Bio Form Num", "_Main settings", "Form number of the per-student form that holds the tutor bio", Group.TUTORING, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", AllowMultipleSelections = false)]
		TUTORING_BioFormNum = 340002,
		// Token: 0x04000998 RID: 2456
		[SettingData("Tutor Confidentiality Agreement", "Rules - Tutor", "The confidentiality agreement the tutor has to agree to.", Group.TUTORING, SettingSemantic.TEXT, DefaultValue = "Website Privacy & Security Policy\r\n\r\nWe are committed to ensuring the privacy and accuracy of your confidential information. We have the utmost respect for your privacy and will not share your personal information with anyone without your explicit permission. All services provided on this Website are alternatively available in person.\r\n\r\nInformation we collect about you\r\n================================\r\nWe will only collect and process your personal data for the purposes of providing the services delivered by this Website.  In addition some information is automatically collected and stored in the server logs, such as your Ip address.  Providing personal data is voluntary.  There will be a minimum data that we need to collect from you for the services that you sign up to.  We will let you know what data we require, if you wish to use our services, by indicating in the relevant fields of the webforms.\r\n\r\nStatistics\r\n==========\r\nThe Website is regularly monitored in order to supply you with the best service and to meet your expectations. For this purpose, we consult the statistics relating to use of our Website and develop the Website on the basis of this data.  Your information may also be used in our reports. User statistics are anonymous.\r\n\r\nSecurity\r\n========\r\nThe Website uses a secure server to protect your information data. Secure server software is used to encrypt the information exchanged between your Web browser and our Website. This measure ensures the security of all your transactions when you use the Sites. We follow strict security procedures when filing and using the information you supply, and may request proof of your identity before supplying you with information. We take all reasonable steps to ensure the secrecy of your personal data and passwords.\r\nYou are fully responsible for maintaining the confidentiality of your login and your password and abstaining from communicating it to any other person and you are solely liable for activities that occur under your login and password. We disclaim all liabilities for inaccuracy of your personal data and in case of theft, loss, misuse, communication, fraudulent use of your login and password arising from your failure to comply with the above.\r\n\r\nCookies\r\n=======\r\nThe Website may use cookies to ensure the smooth operation of your transactions.  Cookies are small information files that a Website can send to the hard disk of a personal computer for traceability reasons. They are not executable programs, and cannot contain viruses or applications. The cookies used only take up a minimal amount of space on your hard disk. You can always prevent cookies from being recorded on your computer by using the options provided by your browser. However, if you do so, some parts of the Site may not be functional.")]
		TUTORING_TutorConfidentialityAgreement = 340005,
		// Token: 0x04000999 RID: 2457
		[SettingData("Help Text", "Tutee Display", "Instructions for the help page", Group.TUTORING, SettingSemantic.HTML, DefaultValue = "<h1>Student tutoring</h1>\r\nWelcome to the online access point for student tutoring.  You will be able to access the following areas on this website:\r\n<br />\r\n<ul>\r\n    <li><a href='calendar.aspx'>View your upcoming appointments</a></li>\r\n    <li><a href='MyTutors.aspx'>View your tutors</a> (tutors you have searched for or have already met with)</li>\r\n    <li><a href='book.aspx'>Schedule a tutoring appointment</a></li>\r\n</ul>\r\n<br />\r\nThe best place to begin is to <a href='book.aspx'>schedule a tutoring appointment</a>.  If you have any questions or concerns please do not hesitate to contact us.")]
		TUTORING_TuteeHelpText,
		// Token: 0x0400099A RID: 2458
		[SettingData("Help Text", "Tutor Display", "Instructions for the help page", Group.TUTORING, SettingSemantic.HTML, DefaultValue = "<h1>Tutors</h1>\r\nWelcome to the online access point for tutors.  You will be able to access the following areas on this website:\r\n<br />\r\n<ul>\r\n    <li><a href='bio.aspx'>Update your profile</a></li>\r\n    <li><a href='calendar.aspx'>View your calendar</a> (both availability and scheduled appointments)</li>\r\n    <li>Set your availability for tutoring appointments</li>\r\n    <li>Mark student attendance and enter session notes for your appointments</li>\r\n    <li>Cancel your appointments with students</li>\r\n</ul>\r\n<br />\r\nThe best place to begin is the <a href='bio.aspx'>profile screen</a>.  If you have any questions or concerns please do not hesitate to contact us.")]
		TUTORING_TutorHelpText,
		// Token: 0x0400099B RID: 2459
		[SettingData("Maximum number of appointments per week", "Rules - Student", "The student will not be able to book more than this number per week using the online interface.  Use 0 to disable this setting.", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = 7)]
		TUTORING_BookingRules_MaxNumberPerWeek = 340009,
		// Token: 0x0400099C RID: 2460
		[SettingData("Maximum number of appointments in the future", "Rules - Student", "The student will only be able to have this number of appointments maximum in the future at any given time.  Use 0 to disable this setting.", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = 14)]
		TUTORING_BookingRules_MaxNumberInFuture,
		// Token: 0x0400099D RID: 2461
		[SettingData("Maximum number of consecutive no-shows ending with their last appointment", "Rules - Student", "The student will not be able to schedule a new appointment if they have this number of no-shows consecutively ending with their last appointment.  Use 0 to disable this setting.", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = 14)]
		TUTORING_BookingRules_MaxNumberConsecutiveNoShowsEndingWithLastAppointment,
		// Token: 0x0400099E RID: 2462
		[SettingData("Cutoff time for new bookings", "Rules - Student", "The student will not be able to schedule a new appointment if the current date and time is not before this cutoff date/time.", Group.TUTORING, SettingSemantic.CUTOFFTIME, DefaultValue = "1")]
		TUTORING_BookingRules_CutoffForSchedulingNewAppointments,
		// Token: 0x0400099F RID: 2463
		[SettingData("Maximum number of appointments per day", "Rules - Student", "The student will not be able to book more than this number per day using the online interface.  Use 0 to disable this setting.", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = 2)]
		TUTORING_BookingRules_MaxNumberPerDay = 340014,
		// Token: 0x040009A0 RID: 2464
		[SettingData("Appointment type to schedule appointments with", "Rules - General", "The appointment type id to use for all new bookings", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = -1)]
		TUTORING_Appointment_Type_Id,
		// Token: 0x040009A1 RID: 2465
		[SettingData("Cutoff time for tutors to cancel their appointments", "Rules - Tutor", "The tutor will not be able to cancel an appointment if the current date and time is not before this cutoff date/time.  The tutor will have to contact the department / student directly if they are unable to cancel online.", Group.TUTORING, SettingSemantic.CUTOFFTIME, DefaultValue = "1")]
		TUTORING_CutoffForTutorCancellingAppointments = 340017,
		// Token: 0x040009A2 RID: 2466
		[SettingData("Cutoff time for students to cancel their appointments", "Rules - Student", "The student will not be able to cancel their appointment if the current date and time is not before this cutoff date/time.  The student will have to contact the department if they are unable to cancel online.", Group.TUTORING, SettingSemantic.CUTOFFTIME, DefaultValue = "1")]
		TUTORING_CutoffForStudentCancellingAppointments,
		// Token: 0x040009A3 RID: 2467
		[SettingData("Availability Group", "Rules - General", "The availability schedule group type id to use for checking availabilities", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = 3)]
		TUTORING_Availability_Schedule_Id,
		// Token: 0x040009A4 RID: 2468
		[SettingData("Availability Group", "Rules - Tutor", "What availability durations can the tutors use to schedule their availabilities (comma, in minutes)", Group.TUTORING, SettingSemantic.TEXT, DefaultValue = "60,30,45")]
		TUTORING_Availability_DurationsAvailable,
		// Token: 0x040009A5 RID: 2469
		[SettingData("Tutor confidentiality agreement re-sign policy: 0 for each school year, 1 for each term, 2 for one time only, 3 for disabled", "Rules - Tutor", "", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = 0)]
		TUTORING_TutorConfidentialityResignPolicy,
		// Token: 0x040009A6 RID: 2470
		[SettingData("Tutor is authorized control id", "_Main settings", "The control id of the (per-student form) checkbox that indicates a tutor is authorized", Group.TUTORING, SettingSemantic.CONTROLID_PERSTUDENT)]
		TUTORING_TutorIsAuthorizedCid,
		// Token: 0x040009A7 RID: 2471
		[SettingData("Student is authorized to receive tutoring (control id)", "_Main settings", "The control id of the (per student OR template accommodation form) checkbox that indicates a student is authorized to receive tutoring.  Leave blank to authorize all students.", Group.TUTORING, SettingSemantic.CONTROLID_PERSTUDENT)]
		TUTORING_StudentIsAuthorizedCid,
		// Token: 0x040009A8 RID: 2472
		[SettingData("Student confidentiality agreement re-sign policy: 0 for each school year, 1 for each term, 2 for one time only, 3 for disabled", "Rules - Student", "", Group.TUTORING, SettingSemantic.INTEGER, DefaultValue = 0)]
		TUTORING_StudentConfidentialityResignPolicy,
		// Token: 0x040009A9 RID: 2473
		[SettingData("Student Confidentiality Agreement", "Rules - Student", "The confidentiality agreement the student has to agree to.", Group.TUTORING, SettingSemantic.TEXT, DefaultValue = "Website Privacy & Security Policy\r\n\r\nWe are committed to ensuring the privacy and accuracy of your confidential information. We have the utmost respect for your privacy and will not share your personal information with anyone without your explicit permission. All services provided on this Website are alternatively available in person.\r\n\r\nInformation we collect about you\r\n================================\r\nWe will only collect and process your personal data for the purposes of providing the services delivered by this Website.  In addition some information is automatically collected and stored in the server logs, such as your Ip address.  Providing personal data is voluntary.  There will be a minimum data that we need to collect from you for the services that you sign up to.  We will let you know what data we require, if you wish to use our services, by indicating in the relevant fields of the webforms.\r\n\r\nStatistics\r\n==========\r\nThe Website is regularly monitored in order to supply you with the best service and to meet your expectations. For this purpose, we consult the statistics relating to use of our Website and develop the Website on the basis of this data.  Your information may also be used in our reports. User statistics are anonymous.\r\n\r\nSecurity\r\n========\r\nThe Website uses a secure server to protect your information data. Secure server software is used to encrypt the information exchanged between your Web browser and our Website. This measure ensures the security of all your transactions when you use the Sites. We follow strict security procedures when filing and using the information you supply, and may request proof of your identity before supplying you with information. We take all reasonable steps to ensure the secrecy of your personal data and passwords.\r\nYou are fully responsible for maintaining the confidentiality of your login and your password and abstaining from communicating it to any other person and you are solely liable for activities that occur under your login and password. We disclaim all liabilities for inaccuracy of your personal data and in case of theft, loss, misuse, communication, fraudulent use of your login and password arising from your failure to comply with the above.\r\n\r\nCookies\r\n=======\r\nThe Website may use cookies to ensure the smooth operation of your transactions.  Cookies are small information files that a Website can send to the hard disk of a personal computer for traceability reasons. They are not executable programs, and cannot contain viruses or applications. The cookies used only take up a minimal amount of space on your hard disk. You can always prevent cookies from being recorded on your computer by using the options provided by your browser. However, if you do so, some parts of the Site may not be functional.")]
		TUTORING_StudentConfidentialityAgreement,
		// Token: 0x040009AA RID: 2474
		[SettingData("Tutoring contact information", "Tutor Display", "Contact information for students/tutors", Group.TUTORING, SettingSemantic.HTML)]
		TUTORING_ContactInfo,
		// Token: 0x040009AB RID: 2475
		[SettingData("Tutee 'Submit a comment' email mail merge template", "Emails - student", "Mail merge template for 'submit a comment' function for tutees.", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~from~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>ClockWork Tutee Submit a Comment</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[A 'Submit a comment' note has been submitted by a student:\r\n\r\nName: <b>#~firstname~# #~lastname~# #~student_no~# </b>\r\n\r\nEmail: #~email~#\r\n\r\nComments:\r\n#~comment~#\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TuteeEmail_SubmitComment = 340008,
		// Token: 0x040009AC RID: 2476
		[SettingData("Tutor 'Submit a comment' email mail merge template", "Emails - tutor", "Mail merge template for 'submit a comment' function for tutors.", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~from~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>ClockWork Tutor Submit a Comment</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[A 'Submit a comment' note has been submitted by a tutor:\r\n\r\nName: <b>#~firstname~# #~lastname~# #~student_no~# </b>\r\n\r\nEmail: #~email~#\r\n\r\nComments:\r\n#~comment~#\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TutorEmail_SubmitComment = 340028,
		// Token: 0x040009AD RID: 2477
		[SettingData("Tutee booking confirmation email", "Emails - student", "Sent after an appointment is successfully booked.", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Appointment Confirmation</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[Hi #~firstname~#,\r\n\r\nThis is an automated confirmation email. You have successfully scheduled a tutoring appointment. Please verify the details below.\r\n\r\n<b>Tutor</b>\r\n\r\nTutor name: #~alt_firstname~# #~alt_lastname~#\r\n\r\nTutor email: #~alt_email~#\r\n\r\n<b>Appointment</b>\r\n\r\n#~scheduledstartdatetime~# - #~appendtime~# (duration: #~appduration~#)\r\n\r\nNotes: #~memo~#\r\n\r\nWe require at least 24 hour notice for cancellations.\r\n\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TuteeEmail_BookingConfirmation = 340013,
		// Token: 0x040009AE RID: 2478
		[SettingData("Tutor 'new booking' email mail merge template", "Emails - tutor", "Mail merge template for email sent to the tutor when a student books an appointment with the tutor.", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>New appointment notification</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[Hi #~firstname~#,\r\n\r\nThis is an automated confirmation email. You have been scheduled for a tutoring appointment. Please verify the details below.\r\n\r\n<b>Student</b>\r\n\r\nStudent name: #~alt_firstname~# #~alt_lastname~#\r\n\r\nStudent email: #~alt_email~#\r\n\r\n<b>Appointment</b>\r\n\r\n#~scheduledstartdatetime~# - #~appendtime~# (duration: #~appduration~#)\r\n\r\nNotes: #~memo~#\r\n\r\nWe require at least 24 hour notice for cancellations.\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TutorEmail_StudentBookedAppointmentNotification = 340027,
		// Token: 0x040009AF RID: 2479
		[SettingData("Tutee cancellation notice email", "Emails - student", "Sent after an appointment is cancelled by the tutor.", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notice of appointment cancellation</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[Hi #~firstname~#,\r\n\r\nThis is an automated confirmation email. Your tutoring appointment has been cancelled. Please verify the details below.\r\n\r\n<b>Tutor</b>\r\n\r\nTutor name: #~alt_firstname~# #~alt_lastname~#\r\n\r\nTutor email: #~alt_email~#\r\n\r\n<b>Appointment</b>\r\n\r\n#~scheduledstartdatetime#~ - #~appendtime~# (duration: #~appduration~#)\r\n\r\nNotes: #~memo~#\r\n\r\nWe require at least 24 hour notice for cancellations.\r\n\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TuteeEmail_CancellationNotice = 340016,
		// Token: 0x040009B0 RID: 2480
		[SettingData("Tutor email sent when a student cancels an appointment", "Emails - tutor", "Mail merge template for student cancelled appointment.", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notice of appointment cancellation</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[Hi #~firstname~#,\r\n\r\nThis is an automated notification email. A tutoring appointment you were previously scheduled for has been cancelled:\r\n\r\n<b>Student</b>\r\n\r\nStudent name: #~alt_firstname~# #~alt_lastname~#\r\n\r\nStudent email: #~alt_email~#\r\n\r\n<b>Appointment</b>\r\n\r\n#~scheduledstartdatetime~# - #~appendtime~# (duration: #~appduration~#)\r\n\r\nNotes: #~memo~#\r\n\r\nWe require at least 24 hour notice for cancellations.\r\n\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TutorEmail_StudentCancelledAppointment = 340029,
		// Token: 0x040009B1 RID: 2481
		[SettingData("Tutor new registration email mail merge template", "Emails - tutor", "Mail merge template for email sent when a tutor completes registration online.", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Tutor registration confirmation</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[Hi #~firstname~#,\r\n\r\nThis is an automated confirmation email. Thank you for registering as a tutor. Your request was sent to us and is still pending approval. You will be notified when your submission has been approved.\r\n\r\nIf you have any questions, please do not hesitate to contact us.\r\n\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TutorEmail_RegisteredConfirmation = 340034,
		// Token: 0x040009B2 RID: 2482
		[SettingData("Tutor email sent when a staff marks a tutor as active", "Emails - tutor", "", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Active tutor status confirmation</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[Hi #~firstname~#,\r\n\r\nThis is an automated confirmation email to inform you that your request has been approved. You may now log in online to set your availability.\r\n\r\nIf you have any questions, please do not hesitate to contact us.\r\n\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TutorEmail_TutorApproved = 340030,
		// Token: 0x040009B3 RID: 2483
		[SettingData("Enable links for student to send notification email when can't find tutor or availability", "Emails - student", "If enabled, links will appear for the student to click if they cannot find a tutor or availability - if clicked the links will send an email to the tutoring coordinator.", Group.TUTORING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TUTORING_TuteeEmail_EnableCantFindTutorOrAvailabilityLinks,
		// Token: 0x040009B4 RID: 2484
		[SettingData("Can't find a tutor", "Emails - student", "Email sent when student indicates they cannot find a tutor", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~from~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Tutoring - student can't find a tutor</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[This is an automated email in response to a student clicking a link on the online tutoring indicating that they are not able to find a tutor.\r\n\r\nStudent: #~firstname~# #~lastname~# (#~student_no~#)\r\nDate submitted: #~date~#\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TuteeEmail_CantFindTutor,
		// Token: 0x040009B5 RID: 2485
		[SettingData("Can't find availability", "Emails - student", "Email sent when student indicates they cannot find an availability", Group.TUTORING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~from~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Tutoring - student can't find an availability</subject>\r\n    <isactive>1</isactive>\r\n<body><![CDATA[This is an automated email in response to a student clicking a link on the online tutoring indicating that they are not able to find an availability for a tutor.\r\n\r\nStudent: #~firstname~# #~lastname~# (#~student_no~#)\r\nTutors: \r\n#~tutors~#\r\nDate submitted: #~date~#\r\n\r\n#~signature~# ]]></body>\r\n </email>")]
		TUTORING_TuteeEmail_CantFindAvailability,
		// Token: 0x040009B6 RID: 2486
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.VETERANS, SettingSemantic.DATETIME, IsHidden = true)]
		VETERANS_LastModifiedTime = 330000,
		// Token: 0x040009B7 RID: 2487
		[SettingData("Chapter drop list field", "_Main settings", "The control id of the chapter drop list (should be on a per-student form)", Group.VETERANS, SettingSemantic.CONTROLID_PERSTUDENT, DefaultValue = 708)]
		VETERANS_ChapterCid = 330002,
		// Token: 0x040009B8 RID: 2488
		[SettingData("Application status page title", "Display", "The title on the application status page (main page) for the student.", Group.VETERANS, SettingSemantic.TEXT, DefaultValue = "Veterans Application Process Status")]
		VETERANS_ApplicationStatusTitle = 330004,
		// Token: 0x040009B9 RID: 2489
		[SettingData("Application status page intro", "Display", "The intro paragraph (directly beneath the title) on the application status page (main page) for the student.", Group.VETERANS, SettingSemantic.HTML, DefaultValue = "Your application process is listed below.  Once you complete a step the next step will become active.  Start the process by registering with us in Step 2 below.  If you have any questions at any point during the application process please contact us by phone or email.")]
		VETERANS_ApplicationStatusIntro,
		// Token: 0x040009BA RID: 2490
		[SettingData("Counselor status dop list field.", "_Main settings", "The control id of the counselor status drop list field.", Group.VETERANS, SettingSemantic.CONTROLID_PERSTUDENT, DefaultValue = 1116)]
		VETERANS_CounselorStatusCid,
		// Token: 0x040009BB RID: 2491
		[SettingData("Counselor note to student text field.", "_Main settings", "The control id of the counselor note to student.", Group.VETERANS, SettingSemantic.CONTROLID_PERSTUDENT, DefaultValue = 1174)]
		VETERANS_CounselorNoteToStudentCid,
		// Token: 0x040009BC RID: 2492
		[SettingData("Admin status drop list field.", "_Main settings", "The control id of the Admin status drop list field.", Group.VETERANS, SettingSemantic.CONTROLID_PERSTUDENT, DefaultValue = 1123)]
		VETERANS_AdminStatusCid,
		// Token: 0x040009BD RID: 2493
		[SettingData("Admin note to student text field.", "_Main settings", "The control id of the Admin note to student.", Group.VETERANS, SettingSemantic.CONTROLID_PERSTUDENT, DefaultValue = 1175)]
		VETERANS_AdminNoteToStudentCid,
		// Token: 0x040009BE RID: 2494
		[ReferenceSetting("Main package screen number", "_Main settings", "Form number of the per-date main package form", Group.VETERANS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", DefaultValue = 23)]
		VETERANS_PackageFormNum,
		// Token: 0x040009BF RID: 2495
		[ReferenceSetting("Agreement screen number", "_Main settings", "Form number of the per-date agreement form", Group.VETERANS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", DefaultValue = 18)]
		VETERANS_AgreementFormNum,
		// Token: 0x040009C0 RID: 2496
		[ReferenceSetting("Per student screen number", "_Main settings", "Form number of the per-student form that holds all fields stored as per-student data (as opposed to per-date data that is the default for all fields)", Group.VETERANS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", DefaultValue = 24)]
		VETERANS_StaticPerStudentForm,
		// Token: 0x040009C1 RID: 2497
		[SettingData("Student confirmation email after agreement form submit", "Emails", "Sent to student when they complete and submit the agreement form.", Group.VETERANS, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Application submission notice</subject>\r\n    <isactive>1</isactive>\r\n<body>Hello #~firstname~#,\r\n\r\nThank you for submitting your veterans application package.  Your information has been submitted and will be processed shortly.  You will be notified by email when there is a status update, and all status updates will be posted on the website.\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		VETERANS_Email_StudentConfirmationOnAgreementFormSubmit,
		// Token: 0x040009C2 RID: 2498
		[ReferenceSetting("Report to use to retrieve veteran data from DataSync.  This report should be equivalent to the 'Preview student data' data sync report for regular students, except it should lookup by web username instead of student number.", "Veteran signup", Group.VETERANS, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false)]
		VETERANS_DataSync_PreviewNotetakerDataReportId,
		// Token: 0x040009C3 RID: 2499
		[SettingData("Student confirmation email after initial registration", "Emails", "Sent to student when they submit the registration form.", Group.VETERANS, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Registration confirmation</subject>\r\n    <isactive>1</isactive>\r\n<body>Hello #~firstname~#,\r\n\r\nThank you for submitting your veterans application registration.  Your information has been submitted and received, and you are now able to start working through the steps required to complete your application package online.  \r\n\r\nIf you have any questions or problems completing your application form please contact us by email or phone (see contact info below).\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		VETERANS_Email_RegistrationConfirmation,
		// Token: 0x040009C4 RID: 2500
		[SettingData("Maximum number of 'Change in Benefit' request submissions.", "Display", "The student will be able to submit at most this number of change in benefit requests after completing step 5 (Consent to agreement form).  Set to 0 to disable change in benefit requests.", Group.VETERANS, SettingSemantic.INTEGER, DefaultValue = 3)]
		VETERANS_MaxChangeInBenefitRequestSubmissions,
		// Token: 0x040009C5 RID: 2501
		[ReferenceSetting("Change in benefit request form", "_Main settings", "Form to use for the student-filled change in benefit request form", Group.VETERANS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		VETERANS_ChangeInBenefitScreenNum,
		// Token: 0x040009C6 RID: 2502
		[SettingData("Change in Benefit request status drop list field.", "_Main settings", "The control id of the Change in Benefit request status drop list field.", Group.VETERANS, SettingSemantic.CONTROLID_PERSTUDENT)]
		VETERANS_ChangeInBenefitStatusCid,
		// Token: 0x040009C7 RID: 2503
		[SettingData("Student confirmation email after change in benefit request submission.", "Emails", "Sent to student when they submit a new change in benefit form.", Group.VETERANS, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Change in benefit submission confirmation</subject>\r\n    <isactive>1</isactive>\r\n<body>Hello #~firstname~#,\r\n\r\nThank you for submitting your veterans change in benefit request.  Your information has been submitted and will be processed.\r\n\r\nIf you have any questions or problems completing your application form please contact us by email or phone (see contact info below).\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		VETERANS_Email_ChangeInBenefitRequestSubmissionConfirmation,
		// Token: 0x040009C8 RID: 2504
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.INTAKE, SettingSemantic.DATETIME, IsHidden = true)]
		SELFREGC_LastModifiedTime = 310000,
		// Token: 0x040009C9 RID: 2505
		[Obsolete]
		[ReferenceSetting("Read only student form (Confidentiality agreement)", "_Main settings", "Form number with read only student info (email,phone,etc.).  This will show on the top of the confidentiality agreement.", Group.SELFREGC, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", IsHidden = true)]
		SELFREGC_FormNum,
		// Token: 0x040009CA RID: 2506
		[SettingData("Confidentiality Agreement", "Display", "The confidentiality agreement the student has to agree to each time they submit a renewal request.", Group.SELFREGC, SettingSemantic.HTML, DefaultValue = "Website Privacy & Security Policy\r\n\r\nWe are committed to ensuring the privacy and accuracy of your confidential information. We have the utmost respect for your privacy and will not share your personal information with anyone without your explicit permission. All services provided on this Website are alternatively available in person.\r\n\r\nInformation we collect about you\r\n================================\r\nWe will only collect and process your personal data for the purposes of providing the services delivered by this Website.  In addition some information is automatically collected and stored in the server logs, such as your Ip address.  Providing personal data is voluntary.  There will be a minimum data that we need to collect from you for the services that you sign up to.  We will let you know what data we require, if you wish to use our services, by indicating in the relevant fields of the webforms.\r\n\r\nStatistics\r\n==========\r\nThe Website is regularly monitored in order to supply you with the best service and to meet your expectations. For this purpose, we consult the statistics relating to use of our Website and develop the Website on the basis of this data.  Your information may also be used in our reports. User statistics are anonymous.\r\n\r\nSecurity\r\n========\r\nThe Website uses a secure server to protect your information data. Secure server software is used to encrypt the information exchanged between your Web browser and our Website. This measure ensures the security of all your transactions when you use the Sites. We follow strict security procedures when filing and using the information you supply, and may request proof of your identity before supplying you with information. We take all reasonable steps to ensure the secrecy of your personal data and passwords.\r\nYou are fully responsible for maintaining the confidentiality of your login and your password and abstaining from communicating it to any other person and you are solely liable for activities that occur under your login and password. We disclaim all liabilities for inaccuracy of your personal data and in case of theft, loss, misuse, communication, fraudulent use of your login and password arising from your failure to comply with the above.\r\n\r\nCookies\r\n=======\r\nThe Website may use cookies to ensure the smooth operation of your transactions.  Cookies are small information files that a Website can send to the hard disk of a personal computer for traceability reasons. They are not executable programs, and cannot contain viruses or applications. The cookies used only take up a minimal amount of space on your hard disk. You can always prevent cookies from being recorded on your computer by using the options provided by your browser. However, if you do so, some parts of the Site may not be functional.")]
		SELFREGC_ConfidentialityAgreement,
		// Token: 0x040009CB RID: 2507
		[Obsolete]
		[ReferenceSetting("Confidentiality last filled in date control", "_Main settings", "The control id of the date field that stores the date the last confidentiality agreement was agreed to by the student.", Group.SELFREGC, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL ORDER BY s.description,dc.controlcaption", IsHidden = true)]
		SELFREGC_ConfidentialityLastAgreetToControl,
		// Token: 0x040009CC RID: 2508
		[Obsolete]
		[ReferenceSetting("'Must see coordinator' accommodation control", "_Main settings", "Controlid for 'must see coordinator' accommodation checkbox", Group.SELFREGC, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE dsc.screennum=4 AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL ORDER BY s.description,dc.controlcaption", IsHidden = true)]
		SELFREGC_MustSeeCoordinatorControl,
		// Token: 0x040009CD RID: 2509
		[Obsolete]
		[ReferenceSetting("Temporary accommodation control", "_Main settings", "Controlid for 'temporary accommodations' accommodation checkbox", Group.SELFREGC, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE dsc.screennum=4 AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL ORDER BY s.description,dc.controlcaption", IsHidden = true)]
		SELFREGC_TempAccommodationControl,
		// Token: 0x040009CE RID: 2510
		[Obsolete("Reactivation is deprecated")]
		[SettingData("Re-activate students on submit", "_Main settings", "", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		SELFREGC_ReactivateStudentsOnSubmit,
		// Token: 0x040009CF RID: 2511
		[SettingData("Student confirmation email", "Emails", "Sent to student when they submit their accommodation requests", Group.SELFREGC, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~# </to>\r\n    <from>#~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Submit Accommodations confirmation</subject>\r\n    <isactive>1</isactive>\r\n<body>Hello #~firstname~#,\r\n\r\nThank you for submitting your accommodation request(s).  Your information has been submitted and will be processed shortly.  The following request(s) were received:\r\n\r\n#~coursedescriptionswithstatus~#\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		SELFREGC_Email_StudentConfirmation,
		// Token: 0x040009D0 RID: 2512
		[ReferenceSetting("Confidentiality agreement data form", "_Main settings", "", Group.SELFREGC, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", IsHidden = true)]
		SELFREGC_ConfidentialityFormNum,
		// Token: 0x040009D1 RID: 2513
		[SettingData("Help Text", "Display", "Instructions for the help page", Group.SELFREGC, SettingSemantic.HTML, DefaultValue = "<h2>Registration Guide</h2>\r\n<ol>\r\n<li>Enter your accommodation information for each course</li>\r\n<li>Submit your courses and accommodations for approval</li>\r\n</ol>\r\n")]
		SELFREGC_HelpText,
		// Token: 0x040009D2 RID: 2514
		[SettingData("Instructor notification email", "Emails", "Sent to instructor when the student submits their accommodation request", Group.SELFREGC, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~instructoremail~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Accommodation Letter Notification for #~coursedescriptionplain~#</subject>\r\n    <isactive>1</isactive>\r\n<body>Dear Professor #~instructorname~#,\r\n\r\nOne of your students has registered with the Disability Services Office and has an accommodation letter ready for your review.\r\n\r\nPlease click on the link below to login and review the letter:\r\n\r\n#~url~#\r\n\r\nIf you have any questions or concerns please do not hesitate to contact us.\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		SELFREGC_Email_InstructorNotification,
		// Token: 0x040009D3 RID: 2515
		[SettingData("Staff notification email", "Emails", "Sent to staff when the student submits their accommodation request.", Group.SELFREGC, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~from~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Accommodation Letter Notification for #~student_no~#: #~coursedescriptionsplain~#</subject>\r\n    <isactive>1</isactive>\r\n<body>A student has submitted an accommodation request.\r\n\r\nStudent: #~student_no~#\r\nCourse(s): #~coursedescriptionswithstatus~#\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		SELFREGC_Email_StaffNotification,
		// Token: 0x040009D4 RID: 2516
		[SettingData("Info page text", "_Main settings", "The welcome message in the main 'Info' page", Group.SELFREGC, SettingSemantic.HTML, DefaultValue = "<p>\r\nYou can request your course accommodations using the tools in this section.  Once a request is submitted it will be automatically approved \r\nif you do not require any changes to your accommodations.  If you do require changes your advisor will be notified and the process will begin to\r\nupdate your accommodations.  You may need to meet with your advisor as part of this process.\r\n</p>\r\n\r\n<p>\r\nOnce a request has been approved your instructor will receive an email and instructions on how to access your accommodation letter by logging into the instructor portion of this website.  \r\nYou are able to access your own accommodation letter on this website at any time once your request has been approved.\r\n</p>\r\n\r\n<p>\r\nIf you have any questions or require assistance please contact your advisor.  Click the <a href=\"courses.aspx\">'Accommodations' button</a> in the main menu in order to get started.\r\n</p>")]
		SELFREGC_InfoText,
		// Token: 0x040009D5 RID: 2517
		[SettingData("Send a staff notification directly to the student's assigned advisor by email", "Emails", "The email only goes out if if an accommodation change is requested by the student, or the student enters a note to their advisor, or the instructor email is missing and the notification cannot be sent.", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		SELFREGC_SendEmailToAssignedAdvisor,
		// Token: 0x040009D6 RID: 2518
		[SettingData("Control id for a checkbox (on accommodations form) that indicates the student is allowed to use this accommodations request system.", "Rules", "If the checkbox is un-checked for the student they will be told to contact their advisor.  The checkbox must be places on the accommodations form.", Group.SELFREGC, SettingSemantic.CONTROLID_PERSTUDENT)]
		SELFREGC_ControlIdToAuthorizeStudentForAccommodationsRequestSystem,
		// Token: 0x040009D7 RID: 2519
		[SettingData("Message to student when they are not allowed because of accommodation authorization control id setting", "Messages", "Only used if the 'Control id for a checkbox that indicates the student is allowed to use this accommodations request system.' is set.", Group.SELFREGC, SettingSemantic.TEXT)]
		SELFREGC_ControlIdToAuthorizeStudentForAccommodationsRequestSystemMessageOnFail,
		// Token: 0x040009D8 RID: 2520
		[SettingData("Special accommodation control ids - notify by email", "Rules", "If the student has any of these accommodations an email will go to the assigned advisor or general email if no assigned advisor is set.", Group.SELFREGC, SettingSemantic.TEXT)]
		SELFREGC_SpecialAccommodationControlIds,
		// Token: 0x040009D9 RID: 2521
		[SettingData("Special accommodation notification email", "Emails", "Sent to the Student's assigned advisor when the student submits their accommodation request and has at least one accommodation in the list of special accommodations defined in the settings. If the student does not have an assigned advisor listed, the email will be sent to an email address listed in the 'Cc:' or 'Bcc:' fields.\r\n*Note that the email address in the 'To:' field is not used for this email template.", Group.SELFREGC, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to> #~from~# </to>\r\n    <from> #~from~# </from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Special Accommodation Notification for #~student_no~#: #~coursedescriptionsplain~# </subject>\r\n    <isactive>1</isactive>\r\n<body>A student has submitted an accommodation request that has at least one accommodation marked as priority.\r\n\r\nStudent: #~student_no~#\r\nCourse(s): #~coursedescriptionswithstatus~#\r\n\r\nAccommodations: \r\n#~selectedaccommodations~#\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		SELFREGC_Email_SpecialAccommodationStaffNotification,
		// Token: 0x040009DA RID: 2522
		[SettingData("Extension on course end date for authorization for students", "Rules", "The number of days the end date of the course will be virtually extended when checking if the student is allowed to request letters for this course.", Group.SELFREGC, SettingSemantic.INTEGER, DefaultValue = 0)]
		SELFREGC_CourseEndDateAuthorizationExtensionInDays,
		// Token: 0x040009DB RID: 2523
		[SettingData("Show the confidentiality agreement as HTML", "Display", "The confidentiality agreement shows by default as plain text within a box with a scroll bar.  This provides the advantage of allowing a larger confidentiality agreement text without using up a lot of space on the page.  If you require links or wish to have the agreement displayed with formatting you should enable this setting.", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGC_ShowConfidentialityAgreementAsHtml,
		// Token: 0x040009DC RID: 2524
		[SettingData("Wording: My accommodations are correct the way they are", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "My accommodation(s) are correct the way they are")]
		SELFREGC_Wording_MyAccommodationsAreCorrectTheWayTheyAre,
		// Token: 0x040009DD RID: 2525
		[SettingData("Wording: I need additional accommodations", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "I need additional accommodations")]
		SELFREGC_Wording_INeedAdditionalAccommodations,
		// Token: 0x040009DE RID: 2526
		[SettingData("Wording: I need to change or remove an accommodation", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "I need to change or remove an accommodation")]
		SELFREGC_Wording_INeedToChangeOrRemoveAnAccommodation,
		// Token: 0x040009DF RID: 2527
		[SettingData("Wording (Status): Please click 'request' button", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Please click the 'Request' button to the right in order to complete the request process.")]
		SELFREGC_Wording_Status_PleaseClickRequest,
		// Token: 0x040009E0 RID: 2528
		[SettingData("Wording (Status): Course has ended", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "N/A (course has ended)")]
		SELFREGC_Wording_Status_CourseHasEnded,
		// Token: 0x040009E1 RID: 2529
		[SettingData("Wording (Status): Accommodation letter has been sent to your instructor", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Your accommodation letter has been sent to your instructor and is awaiting Confirmation.")]
		SELFREGC_Wording_Status_AccommodationLetterHasBeenSentToYourInstructorAndIsAwaitingConfirmation,
		// Token: 0x040009E2 RID: 2530
		[SettingData("Wording (Status): Accommodation letter has been confirmed by prof", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Your Accommodation Letter has been Confirmed by your instructor.")]
		SELFREGC_Wording_Status_AccommodationLetterHasBeenConfirmedByYourInstructor,
		// Token: 0x040009E3 RID: 2531
		[SettingData("Wording (Status): Please contact your advisor", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Please contact your advisor for additional information")]
		SELFREGC_Wording_Status_PleaseContactYourAdvisorForAdditionalInfo,
		// Token: 0x040009E4 RID: 2532
		[SettingData("Wording (Status): Your advisor will review", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Your advisor will review the information and update the status; you will be notified by email when this happens.")]
		SELFREGC_Wording_Status_YourAdvisorWillReview,
		// Token: 0x040009E5 RID: 2533
		[SettingData("Wording (Status): Your advisor has updated your accommodations; new request required", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Your advisor has updated your accommodations.  Please click the 'request' button to the right in order to complete the process and provide your instructor with the new accommodation letter.")]
		SELFREGC_Wording_Status_YourAdvisorHasUpdatedYourAccommodationsPleaseClickRequest,
		// Token: 0x040009E6 RID: 2534
		[SettingData("Wording (Status): Please contact us", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Please contact us")]
		SELFREGC_Wording_Status_UnknownPleaseContactUs,
		// Token: 0x040009E7 RID: 2535
		[SettingData("Never automatically approve requests - send them back to staff instead of approving", "_Main settings", "", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGC_NeverApprove,
		// Token: 0x040009E8 RID: 2536
		[ReferenceSetting("Override accommodations mail merge template for PDF button", "_Main settings", "If set to a non-default value, this template will be used to generate the PDF letter instead of the template defined in the instructor module settings", Group.SELFREGC, SettingSemantic.REFERENCE_ARRAY, "emailtemplates", "templateid", "efrom", OverrideSql = "SELECT templateid,efrom FROM emailtemplates WHERE efrom LIKE 'accommodations_%' ORDER BY efrom")]
		SELFREGC_OverrideAccommodationLetterTemplateId,
		// Token: 0x040009E9 RID: 2537
		[SettingData("Auto approve accommodation control ids hidden from student", "Rules", "All hidden accommodations will be automatically approved during the self registration process.", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		SELFREGC_HiddenControlIds_AutoApproveHiddenAccommodations = 310034,
		// Token: 0x040009EA RID: 2538
		[SettingData("Allow students to download their letters", "Rules", "Normally the students can download their accommodation letters after their course has been approved.  Setting this to false will never allow them to download their letters.", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		SELFREGC_AllowStudentsToDownloadTheirLetter,
		// Token: 0x040009EB RID: 2539
		[SettingData("Wording (Introduction text)", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "Please review the information listed below and indicate whether you need changes to your accommodations at this time.  If your accommodations require changes your request will be submitted for review.")]
		SELFREGC_Wording_Request_IntroductionText,
		// Token: 0x040009EC RID: 2540
		[SettingData("Present accommodations as all un-checked by default to the student when they are submitting a request", "Rules", "", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGC_AllAccommodationsShouldBeUncheckedByDefault,
		// Token: 0x040009ED RID: 2541
		[SettingData("Extra emails for self reg (with logic)", "Rules", "", Group.SELFREGC, SettingSemantic.XML, DefaultValue = "", IsHidden = true)]
		SELFREGC_LogicEmailsRules,
		// Token: 0x040009EE RID: 2542
		[SettingData("Auto approve accommodation control ids visible to the student (but not un-checkable)", "Rules", "These accommodations will be automatically approved during the self registration process.  They will be visible to the student but the student will not abe able to un-check them from the list.", Group.SELFREGC, SettingSemantic.CONTROLIDS_ACCOMMODATIONS)]
		SELFREGC_VisibleButUncheckableControlIds_AutoApproveHiddenAccommodations,
		// Token: 0x040009EF RID: 2543
		[SettingData("Create a POC for the student when they submit a request", "Rules", "", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGC_CreatePocsForSubmittedRequests,
		// Token: 0x040009F0 RID: 2544
		[SettingData("Disable 'check all' and 'check none' buttons for accommodations", "Display", "", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGC_DisableCheckAllCheckNoneButtonsForAccommodations,
		// Token: 0x040009F1 RID: 2545
		[SettingData("Don't allow students to complete self reg for courses that start after their accommodations expire.", "Rules", "If the student has an accommodations expiry date and this setting is set to true, they will not be allowed to complete self registration requests for courses that start after their accommodations expiry date.  If the student has no expiry date set (the field is blank) then they will be allowed to complete self reg for all courses.", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		SELFREGC_DontAllowStudentsToCompleteSelfRegForCoursesStartingAfterAccommodationsExpiryDate,
		// Token: 0x040009F2 RID: 2546
		[SettingData("Wording (Status): Accommodations are expired", "Display", "", Group.SELFREGC, SettingSemantic.TEXT, DefaultValue = "N/A (your accommodations are expired)")]
		SELFREGC_Wording_Status_AccommodationsAreExpired,
		// Token: 0x040009F3 RID: 2547
		[SettingData("If enabled a text box will be provided to the student to submit a note.  If disabled the text box will be hidden.", "Rules", "", Group.SELFREGC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		SELFREGC_AllowStudentsToSubmitANoteWhenCompletingTheirSelfRegRequests,
		// Token: 0x040009F4 RID: 2548
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.INTAKE, SettingSemantic.DATETIME, IsHidden = true)]
		INTAKE_LastModifiedTime = 300000,
		// Token: 0x040009F5 RID: 2549
		[SettingData("Information page text", "Display", "", Group.INTAKE, SettingSemantic.HTML, DefaultValue = "<h2>Registration Information</h2>\r\n<p>\r\nIn order to register with us you must complete a registration form.  Click on the 'Registration' link in the menu to access this form.\r\n</p>\r\n\r\n<p>\r\nIf you have any questions or concerns please contact us at:\r\n</p>\r\n")]
		INTAKE_InformationPageText,
		// Token: 0x040009F6 RID: 2550
		[SettingData("Registration instructions", "Display", "Instructions to the student, displayed directly above the intake form.", Group.INTAKE, SettingSemantic.TEXT, DefaultValue = "Please complete the form below and click the 'Submit' button when you are done. The 'Submit' button is located at the very bottom of this page.")]
		INTAKE_RegistrationInstructions,
		// Token: 0x040009F7 RID: 2551
		[SettingData("Thank you message", "Display", "Displayed after the user submits their registration form.", Group.INTAKE, SettingSemantic.HTML, DefaultValue = "<h1>Thank you for your submission</h1> You will receive a confirmation email shortly.")]
		INTAKE_ThankYouMessage,
		// Token: 0x040009F8 RID: 2552
		[SettingData("Student confirmation email", "Emails", "Sent to the student after they submit their registration form", Group.INTAKE, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Registration confirmation</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\n\r\nThank you for submitting your registration.\r\n#~signature~#\r\n</body>\r\n </email>")]
		INTAKE_StudentConfirmation,
		// Token: 0x040009F9 RID: 2553
		[SettingData("Student email control id", "_Main settings", "The control id of the email field on the registration form.  The student confirmation email will be sent to this address.", Group.INTAKE, SettingSemantic.CONTROLID_PERSTUDENT)]
		INTAKE_EmailCid,
		// Token: 0x040009FA RID: 2554
		[ReferenceSetting("Dynamic ClockWork form number to use as the registration form", "_Main settings", "", Group.INTAKE, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		INTAKE_FormNum,
		// Token: 0x040009FB RID: 2555
		[SettingData("Require students to login first", "Rules", "If true, students will be asked to login before they will be able to fill out the registration form.", Group.INTAKE, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		INTAKE_RequireStudentsToLoginFirst,
		// Token: 0x040009FC RID: 2556
		[SettingData("Send student to the first available form if they are already registered when attempting to fill in the intake form.", "Rules", "Make sure to configure forms to setup Form A, B, or C to the form you want the student to fill in.", Group.INTAKE, SettingSemantic.BOOLEAN, DefaultValue = false, SettingLevel = eSettingLevel.Advanced)]
		INTAKE_SendClockWorkStudentsWithPidToFirstAvailableForm,
		// Token: 0x040009FD RID: 2557
		[SettingData("Disable the captcha control on the intake form", "Rules", "", Group.INTAKE, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INTAKE_HideCaptcha,
		// Token: 0x040009FE RID: 2558
		[SettingData("Allow a student to complete a new intake form if their student number already exists in ClockWork.", "_Main settings", "", Group.INTAKE, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INTAKE_AllowStudentToFillOutIntakeFormIfTheirStudentNumberIsAlreadyInClockWork,
		// Token: 0x040009FF RID: 2559
		[SettingData("Multi-department drop-list to group mappings", "Rules", "Format should be: <droplistgroupmappings cid=\"32\"><mapping lookuplistid=\"55\" groupid=\"14\" /><mapping lookuplistid=\"56\" groupid=\"15\" /></droplistgroupmappings>", Group.INTAKE, SettingSemantic.XML, IsHidden = true)]
		INTAKE_MultiDepartmentDropListGroupMappings,
		// Token: 0x04000A00 RID: 2560
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.DATETIME, IsHidden = true)]
		CLOCKWORKAPPOINTMENTSYNC_LastModifiedTime = 290000,
		// Token: 0x04000A01 RID: 2561
		[SettingData("Appointment slow sync is active", "_Main settings", "The ClockWork-Outlook appointment slow sync will not run if this is set to false", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		CLOCKWORKAPPOINTMENTSYNC_AppointmentSyncIsActive,
		// Token: 0x04000A02 RID: 2562
		[SettingData("Connect to Exchange Server", "_Exchange settings", "If set to false the sync will connect to the local Outlook instance.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		CLOCKWORKAPPOINTMENTSYNC_ConnectToExchangeServer,
		// Token: 0x04000A03 RID: 2563
		[SettingData("Sync Server Url", "_Main settings", "Url to the Syn Web Service. e.g. http://www.google.com or http://[Exchange hostname]/EWS/Exchange.asmx", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.TEXT, DefaultValue = "")]
		CLOCKWORKAPPOINTMENTSYNC_ServerUrl,
		// Token: 0x04000A04 RID: 2564
		[SettingData("Application Sync Delegate Username", "_Main settings", "Username of the delegate user.  The delegate user is an user that has read and write permissions to all staff calendars.  This is the account the ClockWork-Outlook sync uses to do it's work.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.TEXT, DefaultValue = "")]
		CLOCKWORKAPPOINTMENTSYNC_DelegateUserName,
		// Token: 0x04000A05 RID: 2565
		[SettingData("ClockWork users to sync", "Sync settings", "A list of ClockWork users whose calendars will be synced with the Sync Application.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.CLOCKWORKSYNCUSERS, DefaultValue = "")]
		CLOCKWORKAPPOINTMENTSYNC_ClockWorkUsersToSync,
		// Token: 0x04000A06 RID: 2566
		[SettingData("Last sync date time", "Internally used settings", "Used to remember the last date and time a sync was run", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.DATETIME, IsHidden = true)]
		CLOCKWORKAPPOINTMENTSYNC_LastSyncDate,
		// Token: 0x04000A07 RID: 2567
		[SettingData("Sync frequency (in minutes)", "Advanced Google settings", "The sync service will run the sync on this schedule", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.INTEGER, DefaultValue = 30)]
		CLOCKWORKAPPOINTMENTSYNC_SyncFequency,
		// Token: 0x04000A08 RID: 2568
		[SettingData("Sync chunk day count", "Advanced settings", "A single sync job will be split into 'chunks' consisting of this number of days, and run sequentially.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.INTEGER, DefaultValue = 14)]
		CLOCKWORKAPPOINTMENTSYNC_SyncChunkDayCount,
		// Token: 0x04000A09 RID: 2569
		[SettingData("Sync chunk iteration count", "Advanced settings", "A single sync job will be split into 'chunks' and run sequentially.  This setting controls the number of chunks that will be run, so if [Sync chunk day count]=14 and this setting is equal to 4, the result will be 4 runs of 14 days each, or roughly two months.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.INTEGER, DefaultValue = 2)]
		CLOCKWORKAPPOINTMENTSYNC_SyncChunkIterationCount,
		// Token: 0x04000A0A RID: 2570
		[SettingData("Application Sync Delegate Password", "_Main settings", "Password of the delegate user.  The delegate user is an user that has read and write permissions to all staff calendars.  This is the account the ClockWork-Outlook sync uses to do it's work.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.PASSWORD)]
		CLOCKWORKAPPOINTMENTSYNC_DelegatePassword,
		// Token: 0x04000A0B RID: 2571
		[SettingData("Application Sync Version", "_Main settings", "Version of the Application Sync server or API.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.TEXT, DefaultValue = "Exchange2007_SP1")]
		CLOCKWORKAPPOINTMENTSYNC_ServerVersion,
		// Token: 0x04000A0C RID: 2572
		[SettingData("Don't show student names or non-sync user names in external calendar", "_Main settings", "If set to true, new external appointments created by the sync process will include any non-external application attendees from the ClockWork appointment in the external appointment memo.  Note that this includes student names.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		CLOCKWORKAPPOINTMENTSYNC_ShowNonExternalApplicationUsersInExternalAppointmentMemo = 290015,
		// Token: 0x04000A0D RID: 2573
		[SettingData("Paging size", "Advanced settings", "Number of appointments per page.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.INTEGER, DefaultValue = 25)]
		CLOCKWORKAPPOINTMENTSYNC_PagingSize,
		// Token: 0x04000A0E RID: 2574
		[SettingData("Skip all day extenal appointments", "Advanced settings", "All day external appointments will not be synced", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		CLOCKWORKAPPOINTMENTSYNC_SkipAllDayAppointments,
		// Token: 0x04000A0F RID: 2575
		[SettingData("Use Autodiscover Url", "Advanced settings", "Use Autodiscover Url", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		CLOCKWORKAPPOINTMENTSYNC_UseAutodiscoverUrl,
		// Token: 0x04000A10 RID: 2576
		[SettingData("Skip private appointments", "Advanced settings", "Private appointments will not be synced", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		CLOCKWORKAPPOINTMENTSYNC_SkipPrivateAppointments,
		// Token: 0x04000A11 RID: 2577
		[SettingData("Exceeding total sync user licences email", "Advanced settings", "Email user about exceeding total sync user licences", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021~#</from>\r\n     <to>#~syncuseremail~#</to>\r\n     <cc>#~value`websettingid=50021~#</cc>\r\n     <bcc></bcc>\r\n     <subject>ClockWork Calendar Sync for #~firstname~# #~lastname~# did not run on #~date~# #~time~# </subject>\r\n     <attachments></attachments>\r\n     <body>The total number of ClockWork Calendar Sync user licences was exceeded. \r\nYour current number of ClockWork Sync licences is #~totalsyncuserlicences~# and you have #~currentsyncusers~# users active in your Calendar Sync application. \r\nThe calendar for user #~firstname~# #~lastname~# was not synced on #~date~# #~time~#.\r\n\r\nPlease contact TechnoPro Computer Solutions by phone: (1) 416-848-0520 or submit a ticket at https://support.clockworks.ca\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		CLOCKWORKAPPOINTMENTSYNC_ExceededTotalSyncUserLicencesEmail,
		// Token: 0x04000A12 RID: 2578
		[SettingData("No valid sync licence", "Advanced settings", "No valid sync licence key found", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021~#</from>\r\n     <to>#~value`websettingid=50021~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>ClockWork Calendar Sync: No valid licence key was found for your account on #~date~# #~time~# </subject>\r\n     <attachments></attachments>\r\n     <body>Your ClockWork Calendar will not sync until a valid licence key is added.\r\n\r\nPlease contact TechnoPro Computer Solutions by phone: (1) 416-848-0520 or submit a ticket at https://support.clockworks.ca\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		CLOCKWORKAPPOINTMENTSYNC_InvalidLicenseEmail,
		// Token: 0x04000A13 RID: 2579
		[SettingData("Appointment fast sync is active", "Advanced Outlook settings", "The ClockWork-Outlook appointment fast sync will not run if this is set to false", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = true)]
		CLOCKWORKAPPOINTMENTSYNC_AppointmentFastSyncIsActive,
		// Token: 0x04000A14 RID: 2580
		[SettingData("Fast Sync frequency (in minutes)", "Advanced Outlook settings", "The fast sync service will run again after this time elapsed", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.INTEGER, DefaultValue = 3)]
		CLOCKWORKAPPOINTMENTSYNC_FastSyncFrequency,
		// Token: 0x04000A15 RID: 2581
		[SettingData("Slow Sync running schedule", "Advanced Outlook settings", "Slow sync running schedule in 24h format (one per line). e.g 10:00 17:00", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.TEXT, DefaultValue = "12:00\r\n20:00")]
		CLOCKWORKAPPOINTMENTSYNC_SlowSyncRunningSchedule,
		// Token: 0x04000A16 RID: 2582
		[SettingData("Google Service Account Private key filename", "Advanced Google settings", "Full path to Google service account private key file (.p12 file).", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.TEXT)]
		CLOCKWORKAPPOINTMENTSYNC_GoogleServiceAccountPKCS12Filename,
		// Token: 0x04000A17 RID: 2583
		[SettingData("Google Service Account email", "Advanced Google settings", "Google service account email. e.g. [user]@developer.gserviceaccount.com", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.TEXT)]
		CLOCKWORKAPPOINTMENTSYNC_GoogleServiceAccountEmail,
		// Token: 0x04000A18 RID: 2584
		[SettingData("Google Service Account client id", "Advanced Google settings", "Google service account client id.", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.TEXT)]
		CLOCKWORKAPPOINTMENTSYNC_GoogleServiceAccountClientId,
		// Token: 0x04000A19 RID: 2585
		[SettingData("Skip recurring appointments in Fast Sync", "Advanced Outlook settings", "Recurring appointments will not be syncing in fast sync", Group.CLOCKWORKAPPOINTMENTSYNC, SettingSemantic.BOOLEAN, DefaultValue = false)]
		CLOCKWORKAPPOINTMENTSYNC_SkipRecurringAppointmentsInFastSync = 290030,
		// Token: 0x04000A1A RID: 2586
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.CLOCKWORKSERVER, SettingSemantic.DATETIME, IsHidden = true)]
		CLOCKWORKSERVER_LastModifiedTime = 280000,
		// Token: 0x04000A1B RID: 2587
		[SettingData("Drop box notification email activation", "Administration", Group.CLOCKWORKSERVER, SettingSemantic.BOOLEAN, DefaultValue = false)]
		CLOCKWORKSERVER_DROPBOX_NOTIFICATION_EMAIL_ACTIVE = 280002,
		// Token: 0x04000A1C RID: 2588
		[SettingData("Windows Service Updater activation", "Administration", Group.CLOCKWORKSERVER, SettingSemantic.BOOLEAN, DefaultValue = true)]
		CLOCKWORKSERVER_WinService_Updater_Active,
		// Token: 0x04000A1D RID: 2589
		[SettingData("ClockWorkServer jobs enabled", "Jobs", Group.CLOCKWORKSERVER, SettingSemantic.BOOLEAN, DefaultValue = true)]
		CLOCKWORKSERVER_Jobs_Enabled,
		// Token: 0x04000A1E RID: 2590
		[SettingData("Email ClockWork Admin on jobs failure", "Jobs", Group.CLOCKWORKSERVER, SettingSemantic.BOOLEAN, DefaultValue = true)]
		CLOCKWORKSERVER_Jobs_Email_Admin_On_Failure,
		// Token: 0x04000A1F RID: 2591
		[SettingData("Notification email on ClockWorkServer job fails", "Jobs", "This email will only be sent to ClockWork Admin when a ClockWorkServer job fails.", Group.CLOCKWORKSERVER, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021~#</from>\r\n     <to>#~value`websettingid=50021~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWorkServer job failed]: Job #~jobtitle~# failed</subject>\r\n     <attachments></attachments>\r\n     <body>ClockWorkServer job #~jobtitle~# (Id = #~jobid~#) failed on #~executiondatetime~#. Please go to ClockWorkAdmin->Miscellaneous->ClockWorkServer jobs for details.\r\n     Error message: #~errormessage~#.</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		CLOCKWORKSERVER_Jobs_Email_Failure,
		// Token: 0x04000A20 RID: 2592
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.TESTBOOKINGALT, SettingSemantic.DATETIME, IsHidden = true)]
		TESTBOOKINGALT_LastModifiedTime = 270001,
		// Token: 0x04000A21 RID: 2593
		[SettingData("Welcome message for booking wizard", "Display", "", Group.TESTBOOKINGALT, SettingSemantic.HTML, DefaultValue = "<h1>Online test booking request form</h1><p>You can use this form to submit a request to write your test.  Click the 'next' button below to begin.</p>")]
		TESTBOOKINGALT_WizardSetting_WelcomeMsg,
		// Token: 0x04000A22 RID: 2594
		[SettingData("Require user to login before submitting a test request", "_Main settings", Group.TESTBOOKINGALT, SettingSemantic.BOOLEAN, DefaultValue = true)]
		TESTBOOKINGALT_RequireLogin,
		// Token: 0x04000A23 RID: 2595
		[SettingData("Student booking request confirmation email", "Emails", "Gets automatically sent to the student each time they submit a test request. ", Group.TESTBOOKINGALT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>support@tpro.ca</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Confirmation of your test booking request for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nYou have successfully submitted your test booking request for #~course~#:\r\n\r\n#~startdate~# . #~starttime~# to #~endtime~#\r\n\r\nIf you have any questions, or need to cancel or re-schedule your test, please contact us for more information.\r\n    </body>\r\n </email>")]
		TESTBOOKINGALT_Email_BookingRequestConfirmation,
		// Token: 0x04000A24 RID: 2596
		[SettingData("Minimum number of days ahead of the class that a student can book a test", "Rules", "", Group.TESTBOOKINGALT, SettingSemantic.INTEGER, DefaultValue = 7)]
		TESTBOOKINGALT_WizardSetting_MinDaysAheadToBook,
		// Token: 0x04000A25 RID: 2597
		[SettingData("Welcome message for main menu", "Display", "", Group.TESTBOOKINGALT, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Online Test Booking Request Form</h1>\r\n<p>Welcome to the Online Test Booking Request website. This site is for non-accommodated tests and exams only.  If you require accommodations for your test please <a href='../test/default.aspx'>click here</a>.  To begin please click the 'Submit a test' link in the menu on the left.</p>")]
		TESTBOOKINGALT_MainMenu_WelcomeMsg,
		// Token: 0x04000A26 RID: 2598
		[ReferenceSetting("Test booking group ids", "", Group.TESTBOOKINGALT, SettingSemantic.REFERENCE_ARRAY, "Groups", "GroupID", "description", DefaultValue = new int[]
		{
			3
		})]
		TESTBOOKINGALT_ROOM_GROUP,
		// Token: 0x04000A27 RID: 2599
		[SettingData("Thank you page text", "Display", "This text will be displayed to the student after they have submitted their test request.", Group.TESTBOOKINGALT, SettingSemantic.HTML, DefaultValue = "Thank you for your submission")]
		TESTBOOKINGALT_ThankyouMessage,
		// Token: 0x04000A28 RID: 2600
		[SettingData("Reset password email", "Emails", "Sent to the student when they request a new password.  Code for new password is newpassword.", Group.TESTBOOKINGALT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>support@tpro.ca</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Password reset</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\n\r\nYou have successfully reset your test booking password.  The new password is:\r\n\r\n#~newpassword~#\r\n\r\nIf you have any questions please contact us for more information.\r\n    </body>\r\n </email>")]
		TESTBOOKINGALT_Email_ResetPassword,
		// Token: 0x04000A29 RID: 2601
		[SettingData("New account created email", "Emails", "Sent to the student when they create a new account.  Code for new password is newpassword.", Group.TESTBOOKINGALT, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>support@tpro.ca</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>New account created</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\n\r\nYou have successfully created an account with us.  Your username is your email address, and your new password is:\r\n\r\n#~newpassword~#\r\n\r\nIf you have any questions please contact us for more information.\r\n    </body>\r\n </email>")]
		TESTBOOKINGALT_Email_NewAccount,
		// Token: 0x04000A2A RID: 2602
		[ReferenceSetting("TestBookingAlt User Group", "Admin", Group.TESTBOOKINGALT, SettingSemantic.REFERENCE_ARRAY, "Groups", "GroupID", "description", AllowMultipleSelections = false, IsHidden = true)]
		TESTBOOKINGALT_USER_GROUP,
		// Token: 0x04000A2B RID: 2603
		[SettingData("Allow checking for updates", "Updates", "Users will be allowed to check for and install updates if this is marked as true", Group.TESTBOOKINGALT, SettingSemantic.BOOLEAN, DefaultValue = true)]
		TESTBOOKINGALT_AllowCheckForUpdates = 270020,
		// Token: 0x04000A2C RID: 2604
		[SettingData("Force update to version", "Updates", "If a user has a client software that is lower than this version, they will be prompted to begin a software update when they launch the client software", Group.TESTBOOKINGALT, SettingSemantic.TEXT, DefaultValue = "")]
		TESTBOOKINGALT_ForceUpdateToVersion,
		// Token: 0x04000A2D RID: 2605
		[ReferenceSetting("Accommodations mail merge template", "_Main settings", "Used to generate the PDF letter for the student", Group.ACCOMMODATIONS, SettingSemantic.REFERENCE_ARRAY, "emailtemplates", "templateid", "efrom", OverrideSql = "SELECT templateid,efrom FROM emailtemplates WHERE efrom LIKE 'accommodations_%' ORDER BY efrom")]
		ACCOMMODATIONS_LetterTemplateId = 220000,
		// Token: 0x04000A2E RID: 2606
		[ReferenceSetting("French accommodations mail merge template", "_Main settings", "Used to generate the PDF letter for the student", Group.ACCOMMODATIONS, SettingSemantic.REFERENCE_ARRAY, "emailtemplates", "templateid", "efrom", OverrideSql = "SELECT templateid,efrom FROM emailtemplates WHERE efrom LIKE 'accommodations_%' ORDER BY efrom", IsHidden = true)]
		ACCOMMODATIONS_FrenchLetterTemplateId,
		// Token: 0x04000A2F RID: 2607
		[SettingData("Counsellor drop list", "_Main settings", "The control id of the staff drop list that points to this student's counsellor.", Group.ACCOMMODATIONS, SettingSemantic.CONTROLID_PERSTUDENT)]
		ACCOMMODATIONS_CounsellorCid,
		// Token: 0x04000A30 RID: 2608
		[SettingData("Let students choose a French or English letter", "Display", "Allows the students to choose whether they want their letters to generate in English or French.", Group.ACCOMMODATIONS, SettingSemantic.BOOLEAN, IsHidden = true)]
		ACCOMMODATIONS_LetStudentsGenerateLettersInFrench,
		// Token: 0x04000A31 RID: 2609
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.ACCOMMODATIONS, SettingSemantic.DATETIME, IsHidden = true)]
		ACCOMMODATIONS_LastModifiedTime,
		// Token: 0x04000A32 RID: 2610
		[SettingData("Allow students to access their accommodation letters online", "_Main settings", "Students will be able to view and print their accommodation letters online if this is set to true.", Group.ACCOMMODATIONS, SettingSemantic.BOOLEAN, DefaultValue = true)]
		ACCOMMODATIONS_StudentsAllowedToAccessAccommodationLettersOnline,
		// Token: 0x04000A33 RID: 2611
		[SettingData("Instructions for students on the course listing page for accommodations letters", "Display", "Appears under title, above course list", Group.ACCOMMODATIONS, SettingSemantic.TEXT, DefaultValue = "")]
		ACCOMMODATIONS_IndividualCourseInstructions = 220010,
		// Token: 0x04000A34 RID: 2612
		[SettingData("Instructions for students on the course page for accommodations letters", "Display", "Appears under title, above 'Generate PDF' and 'Back to course listing' buttons", Group.ACCOMMODATIONS, SettingSemantic.TEXT, DefaultValue = "")]
		ACCOMMODATIONS_CourseListInstructions,
		// Token: 0x04000A35 RID: 2613
		[SettingData("Allow students to generate the instructor letter", "Rules", "If enabled, the student will be given the option of generating their letter, or the letter for the professor.  The template for the prof letter is under the Instructor section in the settings.", Group.ACCOMMODATIONS, SettingSemantic.BOOLEAN, DefaultValue = false)]
		ACCOMMODATIONS_StudentAllowedToGenerateProfLetter,
		// Token: 0x04000A36 RID: 2614
		[ReferenceSetting("Authorization field for generating prof letter (not allowed checkbox)", "Rules", "If students are allowed to generate the prof letter, what data field on the accommodations form will determine when they will be NOT allowed to generate this letter?", Group.ACCOMMODATIONS, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE dsc.screennum=4 AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL ORDER BY s.description,dc.controlcaption")]
		ACCOMMODATIONS_AuthorizationControlIdForWhenAStudentIsAllowedToGenerateProfLetter,
		// Token: 0x04000A37 RID: 2615
		[SettingData("Notice to student in accommodation letter courses list", "Display", "Appears underneath the list of courses", Group.ACCOMMODATIONS, SettingSemantic.TEXT, DefaultValue = "<b>* you will be able to view and print the letter for your instructor when your advisor approves it.</b>")]
		ACCOMMODATIONS_NoticeToStudentInAccommodationLetterCoursesList,
		// Token: 0x04000A38 RID: 2616
		[SettingData("Allow students to generate the student letter", "Rules", "", Group.ACCOMMODATIONS, SettingSemantic.BOOLEAN, DefaultValue = true)]
		ACCOMMODATIONS_StudentAllowedToGenerateStudentLetter,
		// Token: 0x04000A39 RID: 2617
		[SettingData("Show all accommodations on letters (ie. ignore show on test letter, show on class letter, etc.)", "Rules", "Accommodations that don't have at least one of 'GroupInstructor', 'GroupOther', 'GroupReport', 'GroupTestExam' checked on the accommodations form (in the form editor) will NOT appear in the letters.", Group.ACCOMMODATIONS, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		ACCOMMODATIONS_ShowAllAccommodationsOnLetters_IgnoreShowOnLetter = 220020,
		// Token: 0x04000A3A RID: 2618
		[SettingData("Message to students when they try to access acommodation letters but they have been disabled", "Error Messages", "", Group.ACCOMMODATIONS, SettingSemantic.TEXT, DefaultValue = "Accommodation letters are not currently available online.")]
		ACCOMMODATIONS_ErrorMessage_ModuleDisabled,
		// Token: 0x04000A3B RID: 2619
		[SettingData("Only show template accommodations letter", "Rules", "If enabled the student will not download a letter for each course; they will download a single letter that can be used for all courses.  Note that this excludes the use of course-specific accommodations (only template accomodations will appear on the letter)", Group.ACCOMMODATIONS, SettingSemantic.BOOLEAN, DefaultValue = false)]
		ACCOMMODATIONS_TemplateAccommodationLetterOnly,
		// Token: 0x04000A3C RID: 2620
		[SettingData("Template chooser Sql for student letter", "_Main settings", "Override Sql code to use for choosing which template to use for the student letter.  Return NULL to use the default template defined in the settings. Supports @pid (eg SELECT @pid = SELECT 34), @lucid (eg SELECT @lucid = SELECT 5556), @lucids (eg SELECT @lucids = SELECT '1234,3333,2221').  Example: SELECT CASE WHEN EXISTS(SELECT controlvalue FROM maininfops WHERE controlid=123 AND personid=@pid AND NOT controlvalue=0) THEN 1 ELSE CAST(NULL AS int) END AS templateid", Group.ACCOMMODATIONS, SettingSemantic.TEXT, DefaultValue = "")]
		ACCOMMODATIONS_TemplateChooserForStudent_OverrideSql,
		// Token: 0x04000A3D RID: 2621
		[SettingData("Template chooser Sql for instructor PDF letter", "_Main settings", "Override Sql code to use for choosing which template to use for the instructor letter.  Return NULL to use the default template defined in the settings. Supports @pid (eg SELECT @pid = SELECT 34), @lucid (eg SELECT @lucid = SELECT 5556), @lucids (eg SELECT @lucids = SELECT '1234,3333,2221').  Example: SELECT CASE WHEN EXISTS(SELECT controlvalue FROM maininfops WHERE controlid=123 AND personid=@pid AND NOT controlvalue=0) THEN 1 ELSE CAST(NULL AS int) END AS templateid", Group.ACCOMMODATIONS, SettingSemantic.TEXT, DefaultValue = "")]
		ACCOMMODATIONS_TemplateChooserForInstructor_OverrideSql,
		// Token: 0x04000A3E RID: 2622
		[SettingData("Template chooser Sql for instructor HTML letter", "_Main settings", "Override Sql code to use for choosing which template to use for the instructor HTML letter. Return NULL to use the default template defined in the settings.  Supports @pid (eg SELECT @pid = SELECT 34), @lucid (eg SELECT @lucid = SELECT 5556), @lucids (eg SELECT @lucids = SELECT '1234,3333,2221').  Example: SELECT CASE WHEN EXISTS(SELECT controlvalue FROM maininfops WHERE controlid=123 AND personid=@pid AND NOT controlvalue=0) THEN 1 ELSE CAST(NULL AS int) END AS templateid", Group.ACCOMMODATIONS, SettingSemantic.TEXT, DefaultValue = "")]
		ACCOMMODATIONS_TemplateChooserForInstructorHtml_OverrideSql,
		// Token: 0x04000A3F RID: 2623
		[SettingData("Accommodation control ids to hide from student", "Rules", "These accommodations will not be shown to the student.  This does not control what accommodations appear on accommodation letter mail merge documents.", Group.ACCOMMODATIONS, SettingSemantic.TEXT)]
		ACCOMMODATIONS_HiddenControlIds,
		// Token: 0x04000A40 RID: 2624
		[SettingData("Cutoff time for students to still download their accommodation letters after the course has ended", "Rules", "If disabled, the student will always be able to download their letter even if the course has ended.  Otherwise, if set to 7-days for example, the student will be allowed to download their letter up to 7 days after the course has ended.", Group.ACCOMMODATIONS, SettingSemantic.CUTOFFTIME, DefaultValue = "7")]
		ACCOMMODATIONS_AllowStudentToViewLettersForCoursesThatHaveEnded,
		// Token: 0x04000A41 RID: 2625
		[SettingData("Scanning path", "Path to the scanning documents", Group.NOTETAKINGSCANNER, SettingSemantic.TEXT)]
		NOTETAKINGSCANNER_ScanningPath = 210000,
		// Token: 0x04000A42 RID: 2626
		[SettingData("Welcome page URL", "URL or file name to the html welcome page", Group.NOTETAKINGSCANNER, SettingSemantic.HTML)]
		NOTETAKINGSCANNER_WelcomePageURL = 210006,
		// Token: 0x04000A43 RID: 2627
		[SettingData("Option page URL", "URL or file name to the html page showing the scanning methods", Group.NOTETAKINGSCANNER, SettingSemantic.HTML)]
		NOTETAKINGSCANNER_OptionPageURL,
		// Token: 0x04000A44 RID: 2628
		[SettingData("Instruction 1 page URL", "URL or file name to the html instruction 1 page", Group.NOTETAKINGSCANNER, SettingSemantic.HTML)]
		NOTETAKINGSCANNER_Instruction1PageURL,
		// Token: 0x04000A45 RID: 2629
		[SettingData("Instruction 2 page URL", "URL or file name to the html instruction 2 page", Group.NOTETAKINGSCANNER, SettingSemantic.HTML)]
		NOTETAKINGSCANNER_Instruction2PageURL,
		// Token: 0x04000A46 RID: 2630
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.NOTETAKINGSCANNER, SettingSemantic.DATETIME, IsHidden = true)]
		NOTETAKINGSCANNER_LastModifiedTime,
		// Token: 0x04000A47 RID: 2631
		[SettingData("Using LDAP", "Using LDAP authentication method?", Group.NOTETAKINGSCANNER, SettingSemantic.BOOLEAN)]
		NOTETAKINGSCANNER_UsingLDAP,
		// Token: 0x04000A48 RID: 2632
		[SettingData("Initial image zoom percent", "Initial zoom percent for scanned images", Group.NOTETAKINGSCANNER, SettingSemantic.INTEGER, DefaultValue = 0)]
		NOTETAKINGSCANNER_InitialZoomPercent = 210013,
		// Token: 0x04000A49 RID: 2633
		[SettingData("Upload lecture notes message", "Message notes for uploding", Group.NOTETAKINGSCANNER, SettingSemantic.TEXT)]
		NOTETAKINGSCANNER_UploadLectureNotesMessage,
		// Token: 0x04000A4A RID: 2634
		[SettingData("Custom setting A", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_A = 10000000,
		// Token: 0x04000A4B RID: 2635
		[SettingData("Custom setting B", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_B,
		// Token: 0x04000A4C RID: 2636
		[SettingData("Custom setting C", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_C,
		// Token: 0x04000A4D RID: 2637
		[SettingData("Custom setting D", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_D,
		// Token: 0x04000A4E RID: 2638
		[SettingData("Custom setting E", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_E,
		// Token: 0x04000A4F RID: 2639
		[SettingData("Custom setting F", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_F,
		// Token: 0x04000A50 RID: 2640
		[SettingData("Custom setting G", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_G,
		// Token: 0x04000A51 RID: 2641
		[SettingData("Custom setting H", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_H,
		// Token: 0x04000A52 RID: 2642
		[SettingData("Custom setting I", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_I,
		// Token: 0x04000A53 RID: 2643
		[SettingData("Custom setting J", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_J,
		// Token: 0x04000A54 RID: 2644
		[SettingData("Custom setting K", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_K,
		// Token: 0x04000A55 RID: 2645
		[SettingData("Custom setting L", "Group 1", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_L,
		// Token: 0x04000A56 RID: 2646
		[SettingData("Custom setting M", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_M,
		// Token: 0x04000A57 RID: 2647
		[SettingData("Custom setting N", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_N,
		// Token: 0x04000A58 RID: 2648
		[SettingData("Custom setting O", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_O,
		// Token: 0x04000A59 RID: 2649
		[SettingData("Custom setting P", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_P = 10000016,
		// Token: 0x04000A5A RID: 2650
		[SettingData("Custom setting Q", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_Q,
		// Token: 0x04000A5B RID: 2651
		[SettingData("Custom setting R", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_R,
		// Token: 0x04000A5C RID: 2652
		[SettingData("Custom setting S", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_S,
		// Token: 0x04000A5D RID: 2653
		[SettingData("Custom setting T", "Group 2", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_T,
		// Token: 0x04000A5E RID: 2654
		[SettingData("Custom setting U", "Group 3", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_U,
		// Token: 0x04000A5F RID: 2655
		[SettingData("Custom setting V", "Group 3", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_V,
		// Token: 0x04000A60 RID: 2656
		[SettingData("Custom setting W", "Group 3", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_W,
		// Token: 0x04000A61 RID: 2657
		[SettingData("Custom setting X", "Group 3", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_X,
		// Token: 0x04000A62 RID: 2658
		[SettingData("Custom setting Y", "Group 3", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_Y,
		// Token: 0x04000A63 RID: 2659
		[SettingData("Custom setting Z", "Group 3", "A custom setting that can be used for any custom development using ClockWork, such as custom login scripts.", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_Setting_Z,
		// Token: 0x04000A64 RID: 2660
		[SettingData("Custom password setting 1", "Group 4", "A custom password setting that can be used for any custom development using ClockWork, such as custom login passwords.", Group.CUSTOM, SettingSemantic.PASSWORD)]
		CUSTOM_Password_Setting_1 = 10000101,
		// Token: 0x04000A65 RID: 2661
		[SettingData("Custom password setting 2", "Group 4", "A custom password setting that can be used for any custom development using ClockWork, such as custom login passwords.", Group.CUSTOM, SettingSemantic.PASSWORD)]
		CUSTOM_Password_Setting_2,
		// Token: 0x04000A66 RID: 2662
		[SettingData("Custom password setting 3", "Group 4", "A custom password setting that can be used for any custom development using ClockWork, such as custom login passwords.", Group.CUSTOM, SettingSemantic.PASSWORD)]
		CUSTOM_Password_Setting_3 = 1000103,
		// Token: 0x04000A67 RID: 2663
		[SettingData("Custom password setting 4", "Group 4", "A custom password setting that can be used for any custom development using ClockWork, such as custom login passwords.", Group.CUSTOM, SettingSemantic.PASSWORD)]
		CUSTOM_Password_Setting_4 = 10000104,
		// Token: 0x04000A68 RID: 2664
		[SettingData("Custom password setting 5", "Group 4", "A custom password setting that can be used for any custom development using ClockWork, such as custom login passwords.", Group.CUSTOM, SettingSemantic.PASSWORD)]
		CUSTOM_Password_Setting_5,
		// Token: 0x04000A69 RID: 2665
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.CUSTOM, SettingSemantic.DATETIME, IsHidden = true)]
		CUSTOM_LastModifiedTime = 10000015,
		// Token: 0x04000A6A RID: 2666
		[SettingData("Custom settings definitions", "_Main settings", "", Group.CUSTOM, SettingSemantic.TEXT)]
		CUSTOM_SettingsDefinitions = 10000100,
		// Token: 0x04000A6B RID: 2667
		[SettingData("Welcome page info", "Display", "", Group.WORKSHOPS, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Workshop / Event Booking</h1>\r\n<p>Welcome to the Workshop / Event Booking website.  You can use this website to:</p>\r\n<ul>\r\n    <li>View a list of workshops/events that we offer</li>\r\n    <li>Sign-up for a workshop or event</li>\r\n    <li><li>Check your upcoming scheduled appointments</li>\r\n</ul>\r\n<p>\r\n    Please click the <a href='workshops.aspx'>Available workshops</a> link in the menu in order to get started.  You will be asked to login using your school login account when you are ready to book a workshop.\r\n</p>")]
		WORKSHOPS_WelcomeMessage = 140000,
		// Token: 0x04000A6C RID: 2668
		[ReferenceSetting("Workshops to publish", "_Main settings", "Which workshops/events should appear on the booking form for students.  You must create the workshops and schedules first in ClockWork, and then specify which of those are published online here.", Group.WORKSHOPS, SettingSemantic.REFERENCE_ARRAY, "workshops", "workshopid", "workshoptitle")]
		WORKSHOPS_PublishedWorkshops,
		// Token: 0x04000A6D RID: 2669
		[ReferenceSetting("Post booking form number", "Rules", "", Group.WORKSHOPS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", AllowMultipleSelections = false)]
		WORKSHOPS_PostBookFormNumber,
		// Token: 0x04000A6E RID: 2670
		[ReferenceSetting("Form for collecting info from user during booking.", "Rules", "", Group.WORKSHOPS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		WORKSHOPS_BookFormNumber,
		// Token: 0x04000A6F RID: 2671
		[ReferenceSetting("Facilitator workshop form number", "Rules", "", Group.WORKSHOPS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", AllowMultipleSelections = false)]
		WORKSHOPS_FacilitatorWorkshopFormNumber,
		// Token: 0x04000A70 RID: 2672
		[SettingData("Allow non-ClockWork students to automatically create an account in ClockWork.", "Rules", "", Group.WORKSHOPS, SettingSemantic.BOOLEAN, DefaultValue = false)]
		WORKSHOPS_allowNonClockWorkStudentsToRegister,
		// Token: 0x04000A71 RID: 2673
		[ReferenceSetting("New user registration form.", "Rules", "", Group.WORKSHOPS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		WORKSHOPS_registrationScreenNum,
		// Token: 0x04000A72 RID: 2674
		[ReferenceSetting("Apply form number", "Rules", "", Group.WORKSHOPS, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", AllowMultipleSelections = false)]
		WORKSHOPS_ApplyFormNum,
		// Token: 0x04000A73 RID: 2675
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.WORKSHOPS, SettingSemantic.DATETIME, IsHidden = true)]
		WORKSHOPS_LastModifiedTime,
		// Token: 0x04000A74 RID: 2676
		[SettingData("Student confirmation of booking email", "Email", "Sent to the student each time they successfully book a workshop.  Mail merge codes include (workshoptitle,workshopdescription,date,time,appointmentid,personid)", Group.WORKSHOPS, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Confirmation of your workshop signup</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nYou have successfully signed up for a workshop with us.  Here are the details:\r\n\r\n#~workshoptitle~#\r\n#~workshopdescription~#\r\n#~date~# #~time~#\r\n\r\nPlease contact us if you have any questions, or if you need to cancel or reschedule.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		WORKSHOPS_StudentEmailConfirmation = 140010,
		// Token: 0x04000A75 RID: 2677
		[SettingData("Workshop listing page instructions", "Display", "Appears below the title, just above the list of workshops", Group.WORKSHOPS, SettingSemantic.TEXT, DefaultValue = "Available events are listed below.  Click on the 'Book' button beside the event you would like to signup for.")]
		WORKSHOPS_WorkshopsListingPageInstructions = 140020,
		// Token: 0x04000A76 RID: 2678
		[SettingData("Cutoff time for students to book a workshop", "Rules", "A student will not be able to book a workshop that happens after the cutoff time (from today's date).  The default value is 24 hours.", Group.WORKSHOPS, SettingSemantic.CUTOFFTIME)]
		WORKSHOPS_CutoffTimeForStudentToBookWorkshop,
		// Token: 0x04000A77 RID: 2679
		KIOSK_Title = 150000,
		// Token: 0x04000A78 RID: 2680
		KIOSK_WelcomeMessage,
		// Token: 0x04000A79 RID: 2681
		KIOSK_FormNumber,
		// Token: 0x04000A7A RID: 2682
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.KIOSK, SettingSemantic.DATETIME, IsHidden = true)]
		KIOSK_LastModifiedTime,
		// Token: 0x04000A7B RID: 2683
		[SettingData("File types that the instructor is allowed to upload", "Rules", "", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = ".pdf,.doc,.docx,.txt,.rtf,.xls,.xlsx,.ppt,.pptx,.zip,.wpd")]
		INSTRUCTOR_allowedfiletypes = 130001,
		// Token: 0x04000A7C RID: 2684
		[ReferenceSetting("Test submission form", "_Main settings", "The form to use to collect information from the instructor when they review and confirm an upcoming test", Group.INSTRUCTOR, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		INSTRUCTOR_uploadscreennum,
		// Token: 0x04000A7D RID: 2685
		[SettingData("Exams department contact information for the instructor.", "_Main settings", "", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_contactInfo,
		// Token: 0x04000A7E RID: 2686
		[SettingData("Welcome page info", "Display", "The text that will show on the welcome page for the instructor.  The welcome page is the default page the instructor will be sent to after logging in.  The page should provide some information to the instructor on what they can expect to find on the web module, what their responsibilities are, and who they can contact if they require assistance.", Group.INSTRUCTOR, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Instructor Information</h1>\r\n<p>\r\n    Welcome to the Instructor Information website.  You can use this website to:\r\n    <ul>\r\n      <li>View accommodations that have been assigned to your students</li>\r\n      <li>Tell us about your upcoming mid-terms, tests or quizzes</li>\r\n    </ul>\r\n</p>\r\n<p>\r\n    Please click the <a href='courses.aspx'>courses</a> link in the menu in order to get started.  You will be asked to login using your school login account.\r\n</p>")]
		INSTRUCTOR_WelcomeMessage,
		// Token: 0x04000A7F RID: 2687
		[SettingData("Submit test info intro text", "Display", "This text is blank by default, and will appear on the first page of the submit-test-info wizard for the instructor if you fill in some text here.", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_SubmitTestPageIntro,
		// Token: 0x04000A80 RID: 2688
		[ReferenceSetting("Test submission form - fields to hide", "_Main settings", "The fields listed will not be shown to the instructor.", Group.INSTRUCTOR, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL AND NOT dsc.typecode=1 AND NOT dsc.typecode=2 ORDER BY s.description,dc.controlcaption")]
		INSTRUCTOR_uploadScreenExemptControlIds,
		// Token: 0x04000A81 RID: 2689
		[SettingData("Instructor reset password email template", "Emails", "This email will be sent to the instructor when they reset their password", Group.INSTRUCTOR, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~instructoremail~#</to>\r\n    <from>#~testcoordinatoremail~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Reset password notification</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\n\r\nThis is an automated email in response to your recent password change.  Please follow the directions below to login with your new password:\r\n\r\n#~message~#\r\n\r\nIf you have any questions or concerns please contact us at:\r\n\r\n#~testprofcontactinfo~#\r\n\r\nThank you.\r\n</body>\r\n </email>", IsHidden = true)]
		INSTRUCTOR_ResetPasswordEmailTemplate,
		// Token: 0x04000A82 RID: 2690
		[SettingData("Instructor confirm (for Final Exam Requests workflow only): Show the list of students booked so far to the instructor when they are updating a test/exam.", "Display", "Show the list of students booked so far to the instructor when they are updating a test/exam", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = true)]
		INSTRUCTOR_InstructorConfirm_ShowStudentList = 130009,
		// Token: 0x04000A83 RID: 2691
		[SettingData("Instructor confirm: show each student date/time", "Display", "", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, IsHidden = true)]
		INSTRUCTOR_InstructorConfirm_ShowEachStudentDateTime,
		// Token: 0x04000A84 RID: 2692
		[SettingData("Instructor confirm: show each student accommodation", "Display", "", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, IsHidden = true)]
		INSTRUCTOR_InstructorConfirm_ShowEachStudentAccommodation,
		// Token: 0x04000A85 RID: 2693
		[SettingData("Instructor confirm: message", "Display", "", Group.INSTRUCTOR, SettingSemantic.TEXT, IsHidden = true)]
		INSTRUCTOR_InstructorConfirm_Message,
		// Token: 0x04000A86 RID: 2694
		[SettingData("Instructor confirm: student list message", "Display", "", Group.INSTRUCTOR, SettingSemantic.TEXT, IsHidden = true)]
		INSTRUCTOR_InstructorConfirm_StudentListMessage,
		// Token: 0x04000A87 RID: 2695
		[SettingData("Submit electronic test message", "Display", "", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_SubmitElectronicTest_Message,
		// Token: 0x04000A88 RID: 2696
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.INSTRUCTOR, SettingSemantic.DATETIME, IsHidden = true)]
		INSTRUCTOR_LastModifiedTime,
		// Token: 0x04000A89 RID: 2697
		[ReferenceSetting("Accommodations mail merge template for PDF button", "Accommodation letters", "Used to generate the PDF letter for the instructor", Group.INSTRUCTOR, SettingSemantic.REFERENCE_ARRAY, "emailtemplates", "templateid", "efrom", OverrideSql = "SELECT templateid,efrom FROM emailtemplates WHERE efrom LIKE 'accommodations_%' ORDER BY efrom")]
		INSTRUCTOR_AccommodationLetterTemplateId = 130020,
		// Token: 0x04000A8A RID: 2698
		[ReferenceSetting("French accommodations mail merge template", "Accommodation letters", "Used to generate the PDF letter for the instructor (in french).", Group.INSTRUCTOR, SettingSemantic.REFERENCE_ARRAY, "emailtemplates", "templateid", "efrom", OverrideSql = "SELECT templateid,efrom FROM emailtemplates WHERE efrom LIKE 'accommodations_%' ORDER BY efrom", IsHidden = true)]
		INSTRUCTOR_AccommodationLetterTemplateIdFrench,
		// Token: 0x04000A8B RID: 2699
		[SettingData("Ask Instructor for confirmation on each letter", "Accommodation letters", "When the instructor views a student's accommodation letter, ask them for confirmation that they have received the information.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, IsHidden = true)]
		INSTRUCTOR_AskInstructorForConfirmationOnEachLetter,
		// Token: 0x04000A8C RID: 2700
		[SettingData("Instructor Confirmation Accommodation Letter Message", "Accommodation letters", "The message to display to the instructor just before the checkbox for them to agree.", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_InstructorAccommodationLetterConfirmationMessage,
		// Token: 0x04000A8D RID: 2701
		[SettingData("Instructor Confirmation Accommodation Letter I Agree text", "Accommodation letters", "The text for the 'I agree' checkbox for the instructor to check to acknowledge receipt of the student's accommodation letter.", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_InstructorAccommodationLetterIAgreeText,
		// Token: 0x04000A8E RID: 2702
		[SettingData("Don't check instructor password when an instructor logs in (use for debugging / testing ONLY!)", "Rules", "", Group.INSTRUCTOR, SettingSemantic.BOOLEAN)]
		INSTRUCTOR_InstructorLoginDebugMode,
		// Token: 0x04000A8F RID: 2703
		[SettingData("Don't ask instructors to confirm test bookings", "Tests", "", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		INSTRUCTOR_DontConfirmTests = 130030,
		// Token: 0x04000A90 RID: 2704
		[SettingData("Accommodation letters for instructors enabled", "Accommodation letters enabled", "Allow instructors to view accommodation letters for their students.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = true)]
		INSTRUCTOR_LettersEnabled,
		// Token: 0x04000A91 RID: 2705
		[SettingData("Tests for instructors enabled", "Tests enabled", "Allow instructors to access and update test information for their students.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = true)]
		INSTRUCTOR_TestsEnabled,
		// Token: 0x04000A92 RID: 2706
		[SettingData("Instructor thank you message after submitting exam info", "Tests", "The text to display on the 'Thank-you for your submission' page the instructor is sent to after updating or submitting test information.", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_InstructorThankyouForSubmittingExamInfoMessage,
		// Token: 0x04000A93 RID: 2707
		[SettingData("Accommodation checkbox to indicate not to show this student's name to the instructor", "Accommodation letters", "The controlid of the accommodation checkbox that indicates not to show the student to the prof.", Group.INSTRUCTOR, SettingSemantic.CONTROLID_PERSTUDENT)]
		INSTRUCTOR_DontShowStudentAccommodationCid,
		// Token: 0x04000A94 RID: 2708
		[SettingData("Test confirmation Date/Time message", "Tests", "The message to display to the prof that appears between the date and time of the test on the test confirmation page.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Please enter the original test start and end times manually, or click on the clock icons to pick from a list. We will calculate and apply appropriate time extensions. If you must cancel this test booking, please contact us.")]
		INSTRUCTOR_TestConfirmationDateTimeMessage = 130040,
		// Token: 0x04000A95 RID: 2709
		[SettingData("Show accommodations list preview to instructor", "Accommodation Letter Rules", "If true, a list of accommodations the student is eligible for will be displayed to the instructor on the page where they have the option to open the pdf version of the letter.  The summary list is a quick summary list of accommodations without any formatting meant for quick access.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		INSTRUCTOR_Accommodations_ShowAccommodationsListPreview = 130045,
		// Token: 0x04000A96 RID: 2710
		[SettingData("Instructor login title", "Login", "Override the title for the login page (default is 'Instructor Log In')", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_Login_Title,
		// Token: 0x04000A97 RID: 2711
		[SettingData("Instructor login message", "Login", "Override the intro message for the login page (default is 'Please enter you school login and password below.')", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Please enter you school username and password below.")]
		INSTRUCTOR_Login_Intro,
		// Token: 0x04000A98 RID: 2712
		[SettingData("Instructor login username label text", "Login", "Override the username label for the login page (default is 'Your school username:')", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Your school username:")]
		INSTRUCTOR_Login_Username_Label,
		// Token: 0x04000A99 RID: 2713
		[SettingData("Hide Additional Options section", "Login", "Hide the Additional-Options section where the instructor can say 'I do not have a password, or I have forgotten my password'.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		INSTRUCTOR_Login_Hide_AdditionalOptions,
		// Token: 0x04000A9A RID: 2714
		[SettingData("Login failed message to instructor", "Login", "If the instructor tries to login and fail, they receive the message that you specify here.  You can explain that the login has failed and contact info for if they require assistance.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Your login attempt was not successful.  Please try again.")]
		INSTRUCTOR_Login_LoginFailedMessage,
		// Token: 0x04000A9B RID: 2715
		[SettingData("Email to student when instructor indicates they have reviewed the accommodations letter.", "Emails", "Once the instructor indicates that they have received and reviewed the accommodation letter for the student, this email will be sent to the student if it is enabled.", Group.INSTRUCTOR, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Your instructor has received and reviewed your LOA</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nYour instructor has acknownledged receipt of your accommodations letter for the course #~coursedescription~#.\r\n\r\nThis is a notification only and no action is required on your part.\r\n\r\nPlease contact us if you have any questions.\r\n#~signature~#\r\n    </body>\r\n </email>\r\n")]
		INSTRUCTOR_AccommodationLetter_EmailToStudentOnInstructorAcknowledgeReceived = 130055,
		// Token: 0x04000A9C RID: 2716
		[SettingData("Instructions to instructor for file upload", "Display", "The text here is intended to provide additional instructions and help for the instructor regarding file uploads.  It is displayed directly above the file chooser section.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Note: If you are not able to upload a digital copy of the exam here, a paper copy of the exams, booklets and/or scantron sheets (for each student registered in your course) must be delivered to the Exams Office in advance of your scheduled test or exam, at least one business day in advance.")]
		INSTRUCTOR_SubmitFileInstructions,
		// Token: 0x04000A9D RID: 2717
		[SettingData("Ask instructor to acknowledge receipt of test requests", "Tests", "The instructor will be asked to acknowledge receipt of test requests, with or without questions, during the step in their wizard that shows the list of students.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests,
		// Token: 0x04000A9E RID: 2718
		[SettingData("Should instructor acknowledge each individual test request?", "Tests", "This setting only applies if the previous 'Ask instructor to acknowledge receipt of test requests' is enabled.  If this value is set to false the instructor will be asked to acknowledge all student tests requests with one click.  If this value is set to true, the instructor will be asked to acknowledge each individual student request in the list.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = true)]
		INSTRUCTOR_InstructorConfirm_ConfirmEachStudent = 130008,
		// Token: 0x04000A9F RID: 2719
		[SettingData("Message for instructor to acknowledge receipt of exam requests", "Tests", "Will only be shown if the previous 'Ask instructor to acknowledge receipt of test requests' is enabled.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "I acknowledge receipt of this exam request and agree to provide a copy of the test.")]
		INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_AcknowledgeMessage = 130058,
		// Token: 0x04000AA0 RID: 2720
		[SettingData("Message for instructor to indicate they have questions about the exam requests", "Tests", "Will only be shown if the previous 'Ask instructor to acknowledge receipt of test requests' is enabled.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "I have questions about this request and will contact the disability services department")]
		INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_QuestionsMessage,
		// Token: 0x04000AA1 RID: 2721
		[SettingData("Email after instructor completes test update wizard.", "Emails", "Once the instructor submits the test information in the wizard, this email will be sent out if it is enabled.", Group.INSTRUCTOR, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~testcoordinatoremail~#</to>\r\n    <from>#~testcoordinatoremail~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Instructor submitted test info for #~coursedescription~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>The following instructor has acknownledged receipt of the test requests:\r\n\r\nInstructor: #~instructorname~#\r\nCourse: #~coursedescription~#\r\nStudents: #~students~#\r\nDate: #~date~# #~time~#\r\n\r\nThis is a notification only and no action is required on your part.\r\n#~signature~#\r\n</body>\r\n </email>\r\n")]
		INSTRUCTOR_Tests_EmailOnTestUpdate,
		// Token: 0x04000AA2 RID: 2722
		[SettingData("Instructions for the instructor, at the top of the student list", "Display", "Instructions will appear directly above the list of students in the test update wizard.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Below is the list of students that have registered to write this test with us so far.  Please review this list and click the 'Next' button at the bottom of the page to continue.")]
		INSTRUCTOR_Tests_InstructionsForStudentsList,
		// Token: 0x04000AA3 RID: 2723
		[ReferenceSetting("List of instructors that are allowed to use the online system.", "Rules", "If anything is checked here, only those instructors who are checked will be able to use the online system.  All other instructors will be sent to a page explaining that the site is currently not available.", Group.INSTRUCTOR, SettingSemantic.REFERENCE_ARRAY, "lucoursedata", "lucoursedataid", "altlookupstring", IsValueEncrypted = false, OverrideSql = "SELECT lucoursedataid,altlookupstring + ' (' + STR(lucoursedataid) + ')' AS altlookupstring FROM lucoursedata WHERE lookuplisttype=1 ORDER BY altlookupstring", OverrideSortByDisplayName = false)]
		INSTRUCTOR_RestrictLoginTo,
		// Token: 0x04000AA4 RID: 2724
		[SettingData("Instructor course list instructions", "Display", "Shows under the title and just above the table that lists the instructors courses", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Your courses are listed below.  For instructions, click the 'Help' link.")]
		INSTRUCTOR_CourseListInstructionsText,
		// Token: 0x04000AA5 RID: 2725
		[SettingData("Instructor exam list instructions", "Display", "Shows under the title and just before the table that lists the tests/exams", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Your scheduled accommodated examinations for this course are listed below.  Please select the Update link for the test you are providing information or materials for.")]
		INSTRUCTOR_ExamListInstructionsText,
		// Token: 0x04000AA6 RID: 2726
		[SettingData("Force instructor login for 'Help' default page", "Login", "Normally the welcome/information page the instructor is sent to be default after logging in does not require login (ie. an instructor can access this page without first logging in).  Changing this setting to 'true' will send anyone who tries to access the page without first logging in, to the login page.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_ForceLoginOnInstructionsDefaultPage,
		// Token: 0x04000AA7 RID: 2727
		[SettingData("Hide 'Add test' option for instructors on tests/exams listing page", "Rules", "If set to true, the instructor will not have the option of adding a new test or exam to the list themselves.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_HideAddTestOption,
		// Token: 0x04000AA8 RID: 2728
		[SettingData("Cutoff time for instructors to update test info", "Rules", "An instructor will not be able to update a test after this date passes", Group.INSTRUCTOR, SettingSemantic.CUTOFFTIME, DefaultValue = "1")]
		INSTRUCTOR_CutoffForUpdatingTests,
		// Token: 0x04000AA9 RID: 2729
		[SettingData("Cutoff time for instructors to update test date and time only", "Rules", "An instructor will not be able to update the test date/time after this date passes.  They will continue to be able to update other test info.", Group.INSTRUCTOR, SettingSemantic.CUTOFFTIME, DefaultValue = "1")]
		INSTRUCTOR_CutoffForUpdatingTestDateTime,
		// Token: 0x04000AAA RID: 2730
		[SettingData("Invalid file format message for file uploads", "Display", "This message will be displayed to the instructor if they attempt to upload a file that is not in the allowed file types list.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "The examination you attempted to upload is not a supported file type.  For security protection this system only accepts the following file extensions: pdf, doc, docx, txt, rtf, xls, xlsx, ppt, pptx, wpd, or zip. Please convert your file to one the accepted file types and resubmit.")]
		INSTRUCTOR_InvalidFileFormatUploadMessage,
		// Token: 0x04000AAB RID: 2731
		[SettingData("File too big message for file uploads", "Display", "This message will be displayed to the instructor if they attempt to upload a file that is bigger than the maximum allowed file size (100MB).", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "The file you attempted to upload was rejected because it exceeds the 100MB file size limit.  Please zip your file to reduce its size and resubmit if the file size is less than 100MB.")]
		INSTRUCTOR_FileTooLargeUploadMessage,
		// Token: 0x04000AAC RID: 2732
		[SettingData("Confirm exam details intro message", "Display", "Appears on the last step of the test/exam info wizard", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "Please review the information below and click the 'Submit changes' button at the bottom of this form to submit your changes.  If you have any questions or concerns please do not hesitate to contact us.")]
		INSTRUCTOR_ConfirmExamDetaislIntroMessage = 130075,
		// Token: 0x04000AAD RID: 2733
		[ReferenceSetting("Override test info form for exam booking", "Final Exam Request System", Group.INSTRUCTOR, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		INSTRUCTOR_OverrideExamInfoFormNum = 130090,
		// Token: 0x04000AAE RID: 2734
		[SettingData("Message to instructor when they try to access the site but are not part of the pilot that's currently enabled.", "Error Messages", "", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "The website you are trying to access is currently in a pilot.  Your name is not currently listed as part of the pilot.")]
		INSTRUCTOR_ErrorMessage_Pilot,
		// Token: 0x04000AAF RID: 2735
		[SettingData("Submit exam info intro text", "Display", "This text is blank by default, and will appear on the first page of the submit--final-exam-info wizard for the instructor if you fill in some text here.", Group.INSTRUCTOR, SettingSemantic.TEXT)]
		INSTRUCTOR_SubmitExamPageIntro,
		// Token: 0x04000AB0 RID: 2736
		[SettingData("Message to instructor when they try to access the site but are not found in ClockWork.", "Error Messages", "", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "There are no active courses listed for you in our system.  This would normally happen if none of your students are registered with us.  If this is not the case please contact us for additional information.")]
		INSTRUCTOR_ErrorMessage_NotRegistered,
		// Token: 0x04000AB1 RID: 2737
		[SettingData("Email after instructor completes exam update wizard.", "Emails", "Once the instructor submits the exam information in the wizard, this email will be sent out if it is enabled.", Group.INSTRUCTOR, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~testcoordinatoremail~#</to>\r\n    <from>#~testcoordinatoremail~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Instructor submitted exam info for #~coursedescription~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>The following instructor has acknownledged receipt of the exam requests:\r\n\r\nInstructor: #~instructorname~#\r\nCourse: #~coursedescription~#\r\nStudents: #~students~#\r\nDate: #~date~# #~time~#\r\n\r\nThis is a notification only and no action is required on your part.\r\n#~signature~#\r\n</body>\r\n </email>\r\n")]
		INSTRUCTOR_Tests_EmailOnExamUpdate = 130095,
		// Token: 0x04000AB2 RID: 2738
		[SettingData("Test/Exam Submit Page final note for Final Exam Page", "Display", "", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "<span style='color: Red; font-weight: bold;'>Please note</span> that you must click the <span style='color: blue;'><u><i>'Submit changes'</i></u></span> button at the bottom of this page to confirm your examination to us.")]
		INSTRUCTOR_Tests_FinalExamSubmitPageFinalNote,
		// Token: 0x04000AB3 RID: 2739
		[SettingData("Test/Exam Submit Page final note for Test/Midterm Page", "Display", "", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "<span style='color: Red; font-weight: bold;'>Please note</span> that you must click the {0} button in order to confirm your test to us.")]
		INSTRUCTOR_Tests_TestSubmitPageFinalNote,
		// Token: 0x04000AB4 RID: 2740
		[SettingData("Login problems - ask the instructor to click a button to notify staff if they are having email problems", "Login", "If enabled, a message and button will appear after an instructor has failed a login attempt.  Clicking on the button will result in an email being sent to staff to notify them that the instructor is having difficulty.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_LoginProblems_Enabled,
		// Token: 0x04000AB5 RID: 2741
		[SettingData("Login problems message", "Login", "The message to the instructor after they have failed a login attempt.", Group.INSTRUCTOR, SettingSemantic.TEXT, DefaultValue = "If you are having difficulty logging in please click the button below to notify us.")]
		INSTRUCTOR_LoginProblems_Message,
		// Token: 0x04000AB6 RID: 2742
		[SettingData("Login problems email", "Login", "The email that will be sent out when the instructor clicks the button provided", Group.INSTRUCTOR, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~testcoordinatoremail~#</to>\r\n    <from>#~testcoordinatoremail~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Instructor is having login problems: #~username~# (#~course~#)</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>The following instructor has indicated they are having difficulty logging in:\r\n\r\nAttempted login username: #~username~#\r\nCourse (if available): #~course~#\r\n#~signature~#\r\n</body>\r\n </email>\r\n")]
		INSTRUCTOR_LoginProblems_Email,
		// Token: 0x04000AB7 RID: 2743
		[SettingData("For new tests don't ask the instructor if it is a test or final exam.", "Rules", "Determines whether or not the instructor will have to choose a test type (final or midterm/test/quiz) when they are submitting information online about a new upcoming test or exam.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_DontAskIfTestOrExam,
		// Token: 0x04000AB8 RID: 2744
		[SettingData("Test / Exam extension on course end date for authorization for students", "Rules", "The number of days the end date of the course will be virtually extended when checking if the instructor is allowed to view/edit test/exam info.", Group.INSTRUCTOR, SettingSemantic.INTEGER, DefaultValue = 0)]
		INSTRUCTOR_TestExamCourseEndDateAuthorizationExtensionInDays,
		// Token: 0x04000AB9 RID: 2745
		[SettingData("Accommodation letters extension on course end date for authorization for students", "Rules", "The number of days the end date of the course will be virtually extended when checking if the instructor is allowed to view letters for a student.", Group.INSTRUCTOR, SettingSemantic.INTEGER, DefaultValue = 7)]
		INSTRUCTOR_AccommodationLetterCourseEndDateAuthorizationExtensionInDays,
		// Token: 0x04000ABA RID: 2746
		[SettingData("Exam request choice rules for instructor", "Rules", "", Group.INSTRUCTOR, SettingSemantic.XML, DefaultValue = "<examrequestrules>\r\n    <dates>2012-12-10,2012-12-11,2012-12-12,2012-12-13,2012-12-14</dates>\r\n    <times>8:30,12:30,16:30</times>\r\n    <closeddates>2012-12-13 8:30,x2012-12-13 16:30</closeddates>\r\n</examrequestrules>")]
		INSTRUCTOR_ExamRequestRules,
		// Token: 0x04000ABB RID: 2747
		[ReferenceSetting("Accommodations mail merge template for HTML button", "Accommodation letters", "Used to generate the HTML letter for the instructor.  If this is not set the HTML button will not appear.", Group.INSTRUCTOR, SettingSemantic.REFERENCE_ARRAY, "emailtemplates", "templateid", "efrom", OverrideSql = "SELECT templateid,efrom FROM emailtemplates WHERE efrom LIKE 'accommodations_%' ORDER BY efrom")]
		INSTRUCTOR_AccommodationLetterHTMLTemplateId,
		// Token: 0x04000ABC RID: 2748
		[SettingData("Don't allow the instructors to upload tests/exams.", "Rules", "The instructors will not be able to upload electronic copies of their test or exam online.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_DontAllowInstructorToUploadTestsExams,
		// Token: 0x04000ABD RID: 2749
		[SettingData("Allow instructors to view accommodation letters for students as long as their accommodation expiry date is active", "Rules", "Must have the accommodation expiry date cid setting properly entered in the web test booking section.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDate,
		// Token: 0x04000ABE RID: 2750
		[SettingData("Instructor changed date/time of test email", "Emails", "The email that will be sent out if the instructor changes the date and or time of the test/exam", Group.INSTRUCTOR, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~testcoordinatoremail~#</to>\r\n    <from>#~testcoordinatoremail~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Instructor has changed date/time of a test/exam: #~coursedescription~# to #~testdate~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>The following instructor has indicated the date and or time of the test/exam has changed:\r\n\r\nName: #~instructorname~#\r\nEmail: #~instructoremail~#\r\nPhone# #~instructorphone~#\r\nCourse: #~coursedescription~#\r\nOld date/time of test: #~olddatetime~# (#~oldduration~#)\r\nNew date/time of test: #~testdate~# (#~testduration~#)\r\nStudents affected: #~students~#\r\n\r\nThe affected tests will need to be rescheduled manually for the students.  You will find them highlighted in yellow in the ClockWork test listing.\r\n</body>\r\n </email>\r\n")]
		INSTRUCTOR_Email_ChangedDateTimeOfTest,
		// Token: 0x04000ABF RID: 2751
		[SettingData("Show the student's accommodations they chose when booking their test (for the student list in the test/exam edit wizard for the instructor)", "Display", "If set to true, the accommodations the student chose for their test will appear beside their name in the edit test/exam wizard for the instructor.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		INSTRUCTOR_ShowStudentAccommodationsChosenForTestOnStudentList,
		// Token: 0x04000AC0 RID: 2752
		[SettingData("Redirect email links for individual letters to single page showing all letters that are available.", "Rules", "If true, email links generated from ClockWork (generate accommodations letter) or self reg module, will be redirected to a single page that lists all of the instructors students with letters available, instead of directly to the letter for a single student.course.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = true)]
		INSTRUCTOR_RedirectEmailLinksForIndividualLettersToLettersPage,
		// Token: 0x04000AC1 RID: 2753
		[SettingData("Allow instructors to view accommodation letters for students if the letter was previously generated in ClockWork for that student/course", "Rules", "The letter can only be generated by staff in ClockWork - includes if the letter generated is an email to the prof, but the email must actually be sent successfully to mark it as generated.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = true)]
		INSTRUCTOR_ShowStudentAccommodationLettersForStudentsWhereTheLetterWasGenerated,
		// Token: 0x04000AC2 RID: 2754
		[SettingData("Accommodation checkbox - reverse the logic", "Accommodation letters", "Reverses the logic for 'Accommodation checkbox to indicate not to show this student's name to the instructor', so that the instructor can only view the student's letter if the checkbox is checked.  This requires the 'Accommodation checkbox to indicate not to show this student's name to the instructor' to be set to a checkbox on the accommodation form.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_ReverseDontShowStudentAccommodationCid,
		// Token: 0x04000AC3 RID: 2755
		[SettingData("Disable the final exam request interface for instructors", "Final Exam Request System", "If set to true (and 'Enable final exam request system' is also set to true), the instructor will be sent to the regular midterm/exam confirmation wizard instead of the final exam request one.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_DisableExamRequestInterfaceForInstructors,
		// Token: 0x04000AC4 RID: 2756
		[SettingData("Allow instructors to view accommodation letters for students as long as both are true: their accommodation expiry date is active and self reg is approved", "Rules", "Must have the accommodation expiry date cid setting properly entered in the web test booking section.", Group.INSTRUCTOR, SettingSemantic.BOOLEAN, DefaultValue = false)]
		INSTRUCTOR_ShowStudentAccommodationLettersForAnyStudentWithActiveAccommodationExpiryDateAndSelfRegApproved,
		// Token: 0x04000AC5 RID: 2757
		[SettingData("Cutoff time for creating a new test", "Rules", "The instructor won't be able to create a new test once this cutoff time has passed.", Group.INSTRUCTOR, SettingSemantic.CUTOFFTIME, DefaultValue = "")]
		INSTRUCTOR_CutoffNewClasTestCreateDate,
		// Token: 0x04000AC6 RID: 2758
		NOTETAKING_coursesmsg = 120000,
		// Token: 0x04000AC7 RID: 2759
		NOTETAKING_coursenotesmsg,
		// Token: 0x04000AC8 RID: 2760
		NOTETAKING_notespath,
		// Token: 0x04000AC9 RID: 2761
		NOTETAKING_coursefilenametemplate,
		// Token: 0x04000ACA RID: 2762
		NOTETAKING_lecturedateformat,
		// Token: 0x04000ACB RID: 2763
		NOTETAKING_notetakerappmsg,
		// Token: 0x04000ACC RID: 2764
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.NOTETAKING, SettingSemantic.DATETIME, IsHidden = true)]
		NOTETAKING_LastModifiedTime,
		// Token: 0x04000ACD RID: 2765
		[SettingData("Show tutor bios link", "Display", "Shows the menu link for viewing tutor bios.", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, IsHidden = true)]
		APPOINTMENTBOOKING_showTutorBiosLink = 110005,
		// Token: 0x04000ACE RID: 2766
		[SettingData("Tutor bio text field", "Display", "The ClockWork textbox field that holds the text for display in the 'Bios' section on the web", Group.APPOINTMENTBOOKING, SettingSemantic.CONTROLID_PERSTUDENT, IsHidden = true)]
		APPOINTMENTBOOKING_tutorBioTextCid = 110026,
		// Token: 0x04000ACF RID: 2767
		[SettingData("Waiting list enabled", "Waiting list", "Use the waiting list feature.", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, IsHidden = true)]
		APPOINTMENTBOOKING_useWaitingList = 110006,
		// Token: 0x04000AD0 RID: 2768
		[SettingData("Waiting list maximum entries", "Waiting list", "", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER, IsHidden = true)]
		APPOINTMENTBOOKING_waitingListMaxEntries,
		// Token: 0x04000AD1 RID: 2769
		[SettingData("Add name to waiting list email confirmation template", "Emails", "", Group.APPOINTMENTBOOKING, SettingSemantic.EMAIL_TEMPLATE, IsHidden = true, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject></subject>\r\n    <attachments></attachments>\r\n    <active>0</active>\r\n    <body>\r\n    </body>\r\n </email>")]
		APPOINTMENTBOOKING_email_waitinglistsignup = 110015,
		// Token: 0x04000AD2 RID: 2770
		[SettingData("Remove name from waiting list email confirmation template", "Emails", "", Group.APPOINTMENTBOOKING, SettingSemantic.EMAIL_TEMPLATE, IsHidden = true, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject></subject>\r\n    <attachments></attachments>\r\n    <active>0</active>\r\n    <body>\r\n    </body>\r\n </email>")]
		APPOINTMENTBOOKING_email_waitinglistcancel,
		// Token: 0x04000AD3 RID: 2771
		[SettingData("Auto booking from waiting list email confirmation template", "Emails", "", Group.APPOINTMENTBOOKING, SettingSemantic.EMAIL_TEMPLATE, IsHidden = true, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject></subject>\r\n    <attachments></attachments>\r\n    <active>0</active>\r\n    <body>\r\n    </body>\r\n </email>")]
		APPOINTMENTBOOKING_email_waitinglistbook,
		// Token: 0x04000AD4 RID: 2772
		[SettingData("The url the student should be sent to when they click 'I forgot my password'.", Group.APPOINTMENTBOOKING, SettingSemantic.TEXT, DefaultValue = "javascript: alert('Please go to the main school website for instructions on how to reset your password.');", IsHidden = true)]
		APPOINTMENTBOOKING_LOGIN_student_forgot_password_url = 110034,
		// Token: 0x04000AD5 RID: 2773
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.APPOINTMENTBOOKING, SettingSemantic.DATETIME, IsHidden = true)]
		APPOINTMENTBOOKING_LastModifiedTime,
		// Token: 0x04000AD6 RID: 2774
		[SettingData("Show Booked Appointments on Schedule", "Display", "If true, shows booked appointments as non-bookable entries on the calendar.  Students can click on these to add their name to a wait list.", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, IsHidden = true)]
		APPOINTMENTBOOKING_ShowBookedAppointmentsOnSchedule = 110037,
		// Token: 0x04000AD7 RID: 2775
		[SettingData("Enable additional info page", "Display", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, IsHidden = true)]
		APPOINTMENTBOOKING_AdditionalInfoPageEnabled = 110039,
		// Token: 0x04000AD8 RID: 2776
		[SettingData("Additional info page link text", "Display", "", Group.APPOINTMENTBOOKING, SettingSemantic.TEXT, IsHidden = true)]
		APPOINTMENTBOOKING_AdditionalInfoPageLinkText,
		// Token: 0x04000AD9 RID: 2777
		[SettingData("Read-only booking form to show facilitators when entering their notes.", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER, DefaultValue = 0, IsHidden = true)]
		APPOINTMENTBOOKING_ReadOnlyBookingFormForFacilitators = 110048,
		// Token: 0x04000ADA RID: 2778
		[SettingData("Availability Rules", "_Main Settings", "Who is available and when", Group.APPOINTMENTBOOKING, SettingSemantic.SCHEDULE_TYPES, DefaultValue = "<scheduletypes>\r\n                                <scheduletype>\r\n\t\t\t                        <availabilitygroupids>3</availabilitygroupids>\r\n\t\t\t                        <apptypeid>2</apptypeid>\r\n\t\t\t                        <displayname>Disability appointment</displayname>\r\n                                    <displaysummary></displaysummary>\r\n                                    <prebookscreennum>2</prebookscreennum>\r\n                                    <people> \r\n                                        <person>  \r\n                                            <displayname>Mike D.</displayname>                                          \r\n                                            <displaysummary>Mike is a 3rd year engineering student who has been tutoring for us for 3 years.</displaysummary>\r\n                                            <pids>3333</pids>                   \r\n                                        </person>                    \r\n                                       <person>  \r\n                                            <displayname>Sally J.</displayname>                                         \r\n                                            <displaysummary></displaysummary>\r\n                                            <pids>13</pids>                   \r\n                                       </person>   \r\n                                    </people>\r\n\t\t\t                        <duration>60</duration>\r\n\t\t\t                        <prebooknotice></prebooknotice>\r\n\t\t\t                        <postbooknotice></postbooknotice>\r\n\t\t\t                        <bookingformscreennum>2</bookingformscreennum>\r\n\t\t                            <maxnuminfuture>2</maxnuminfuture>\r\n\t\t                        </scheduletype>\r\n                                <scheduletype>\r\n\t\t\t                        <availabilitygroupids>5</availabilitygroupids>\r\n\t\t\t                        <apptypeid>3</apptypeid>\r\n\t\t\t                        <displayname>Counselling appointment</displayname>\r\n                                    <displaysummary></displaysummary>\r\n                                    <prebookscreennum>-1</prebookscreennum>\r\n                                    <people> \r\n                                        <person>  \r\n                                            <displayname>Mike D.</displayname>\r\n                                            <displaysummary>Mike is a 3rd year engineering student who has been tutoring for us for 3 years.</displaysummary>              \r\n                                            <pids>1</pids>                   \r\n                                        </person>                    \r\n                                        <person>  \r\n                                            <displayname>Sally J.</displayname>  \r\n                                            <displaysummary></displaysummary>            \r\n                                            <pids>13</pids>                   \r\n                                        </person>   \r\n                                    </people>\r\n\t\t\t                        <duration>60</duration>\r\n\t\t\t                        <prebooknotice></prebooknotice>\r\n\t\t\t                        <postbooknotice></postbooknotice>\r\n\t\t\t                        <bookingformscreennum>2</bookingformscreennum>\r\n\t\t                            <maxnuminfuture>2</maxnuminfuture>\r\n\t\t                        </scheduletype>\r\n                            </scheduletypes>")]
		APPOINTMENTBOOKING_availabilitygroupidsdurations = 110000,
		// Token: 0x04000ADB RID: 2779
		[SettingData("Appointment booking channels", "_Main Settings", "Channels to use for the online appointment booking.  A person's single availability schedule (one type) can be split among various channels, so that the availability schedule will switch to that channel if the user selects it for the view.", Group.APPOINTMENTBOOKING, SettingSemantic.CHANNELS, IsHidden = true, DefaultValue = "<channels>\r\n   <channel>\r\n      <title>General Tutoring</title>\r\n      <id>TUT</id>\r\n      <description></description>\r\n      <apptypeid>2</apptypeid>\r\n      <duration>30</duration>\r\n      <bookingformscreennum>0</bookingformscreennum>\r\n      <isactive>0</isactive>\r\n   </channel>\r\n</channels>")]
		APPOINTMENTBOOKING_Channels = 110036,
		// Token: 0x04000ADC RID: 2780
		[SettingData("Group(s) containing the tutors", "_Main Settings", "Which group(s) do the tutors/facilitators/staff belong to?", Group.APPOINTMENTBOOKING, SettingSemantic.TEXT, DefaultValue = "11", IsHidden = true)]
		APPOINTMENTBOOKING_TutorGids = 110001,
		// Token: 0x04000ADD RID: 2781
		[SettingData("Number of no-shows in a row to ban.", "Banning", "The user will be automatically banned if they are marked no-show this many times in a row.", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER, DefaultValue = 0, IsHidden = true)]
		APPOINTMENTBOOKING_bannedNumNoshows,
		// Token: 0x04000ADE RID: 2782
		[SettingData("Number of days banned is in effect (from date of ban)", "Banning", "", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER, DefaultValue = 28, IsHidden = true)]
		APPOINTMENTBOOKING_bannedNumDays,
		// Token: 0x04000ADF RID: 2783
		[SettingData("Banned expiry date control id", "Banning", "", Group.APPOINTMENTBOOKING, SettingSemantic.CONTROLID_PERSTUDENT, IsHidden = true)]
		APPOINTMENTBOOKING_bannedExpiryDateCid,
		// Token: 0x04000AE0 RID: 2784
		[SettingData("Allow students who are not in ClockWork to create new accounts in ClockWork online.", "Registration", "Students who have authenticated (got past the login) will be sent to a registration form and a new account will automatically be created for them in ClockWork.", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister = 110008,
		// Token: 0x04000AE1 RID: 2785
		[SettingData("Registration I agree table field", "Registration", "This is currently only used on the workshop booking module.", Group.APPOINTMENTBOOKING, SettingSemantic.CONTROLID_PERSTUDENT)]
		APPOINTMENTBOOKING_registrationIAgreeTableCid = 110010,
		// Token: 0x04000AE2 RID: 2786
		[SettingData("Registration form to use", "Registration", "", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER)]
		APPOINTMENTBOOKING_registrationScreenNum,
		// Token: 0x04000AE3 RID: 2787
		[SettingData("New registration email confirmation template", "Emails", "Email that gets sent to the user after they register as a new user.", Group.APPOINTMENTBOOKING, SettingSemantic.EMAIL_TEMPLATE)]
		APPOINTMENTBOOKING_email_registration,
		// Token: 0x04000AE4 RID: 2788
		[SettingData("Booking email confirmation template", "Emails", "", Group.APPOINTMENTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Appointment booking confirmation</subject>\r\n    <attachments></attachments>\r\n    <active>0</active>\r\n    <body>Hello #~firstname~#,\r\n\r\nYou have successfully scheduled an appointment with us:\r\n\r\n#~appdate~# . #~starttime~# to #~endtime~#\r\n\r\nPlease contact us if you have any questions, or need to cancel or reschedule your appointment.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		APPOINTMENTBOOKING_email_book,
		// Token: 0x04000AE5 RID: 2789
		[SettingData("Cancel booking email confirmation template", "Emails", "", Group.APPOINTMENTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Cancellation confirmation</subject>\r\n    <attachments></attachments>\r\n    <active>0</active>\r\n    <body>This is an automated email to confirm your appointment cancellation:\r\n\r\n#~appdate~# . #~starttime~# to #~endtime~#\r\n\r\nPlease contact us if you have any questions.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		APPOINTMENTBOOKING_email_cancel,
		// Token: 0x04000AE6 RID: 2790
		[SettingData("Banned email confirmation template", "Emails", "", Group.APPOINTMENTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject></subject>\r\n    <attachments></attachments>\r\n    <active>0</active>\r\n    <body>\r\n    </body>\r\n </email>", IsHidden = true)]
		APPOINTMENTBOOKING_email_banned = 110018,
		// Token: 0x04000AE7 RID: 2791
		[SettingData("Visible start time (military)", "Display", "The start time to display on the calendar (in military time)", Group.APPOINTMENTBOOKING, SettingSemantic.TEXT, DefaultValue = "8:30")]
		APPOINTMENTBOOKING_starttime = 110021,
		// Token: 0x04000AE8 RID: 2792
		[SettingData("Visible end time (military)", "Display", "The end time to display on the calendar (in military time)", Group.APPOINTMENTBOOKING, SettingSemantic.TEXT, DefaultValue = "16:30")]
		APPOINTMENTBOOKING_endtime,
		// Token: 0x04000AE9 RID: 2793
		[SettingData("Appointment booking information", "Display", "The text to display on the main Information page.", Group.APPOINTMENTBOOKING, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Book an appointment</h1>\r\n<p>Welcome to the appointment booking website.  You can use this website to:</p>\r\n<ul>\r\n    <li>Book a appointment</li>\r\n    <li>Check your upcoming scheduled appointments</li>\r\n</ul>\r\n<p>\r\n    Please click the <a href='book.aspx'>Book an appointment</a> link in the menu in order to get started.  You will be asked to login using your school login account once you are ready to book your appointment.\r\n</p>\r\n")]
		APPOINTMENTBOOKING_info = 110025,
		// Token: 0x04000AEA RID: 2794
		[SettingData("Confidentiality agreement", "Registration", "The text for the confidentiality agreement the student has to agree to when signing up for the first time.", Group.APPOINTMENTBOOKING, SettingSemantic.HTML, DefaultValue = "<h2>Website Privacy & Security Policy</h2>\r\n<p>\r\nWe are committed to ensuring the privacy and accuracy of your confidential information. We have the utmost respect for your privacy and will not share your personal information with anyone without your explicit permission. All services provided on this Website are alternatively available in person.\r\n</p>\r\n<h2>Information we collect about you</h2>\r\n<p>\r\nWe will only collect and process your personal data for the purposes of providing the services delivered by this Website.  In addition some information is automatically collected and stored in the server logs, such as your Ip address.  Providing personal data is voluntary.  There will be a minimum data that we need to collect from you for the services that you sign up to.  We will let you know what data we require, if you wish to use our services, by indicating in the relevant fields of the webforms.\r\n</p>\r\n<h2>Statistics</h2>\r\n<p>\r\nThe Website is regularly monitored in order to supply you with the best service and to meet your expectations. For this purpose, we consult the statistics relating to use of our Website and develop the Website on the basis of this data.  Your information may also be used in our reports. User statistics are anonymous.\r\n</p>\r\n<h2>Security</h2>\r\n<p>\r\nThe Website uses a secure server to protect your information data. Secure server software is used to encrypt the information exchanged between your Web browser and our Website. This measure ensures the security of all your transactions when you use the Sites. We follow strict security procedures when filing and using the information you supply, and may request proof of your identity before supplying you with information. We take all reasonable steps to ensure the secrecy of your personal data and passwords.\r\n</p>\r\n<p>\r\nYou are fully responsible for maintaining the confidentiality of your login and your password and abstaining from communicating it to any other person and you are solely liable for activities that occur under your login and password. We disclaim all liabilities for inaccuracy of your personal data and in case of theft, loss, misuse, communication, fraudulent use of your login and password arising from your failure to comply with the above.\r\n</p>\r\n<h2>Cookies</h2>\r\n<p>\r\nThe Website may use cookies to ensure the smooth operation of your transactions.  Cookies are small information files that a Website can send to the hard disk of a personal computer for traceability reasons. They are not executable programs, and cannot contain viruses or applications. The cookies used only take up a minimal amount of space on your hard disk. You can always prevent cookies from being recorded on your computer by using the options provided by your browser. However, if you do so, some parts of the Site may not be functional.\r\n</p>")]
		APPOINTMENTBOOKING_confidentialityAgreement = 110027,
		// Token: 0x04000AEB RID: 2795
		[ReferenceSetting("ClockWork group to put new users into", "Registration", "", Group.APPOINTMENTBOOKING, SettingSemantic.REFERENCE_ARRAY, "groups", "groupid", "description")]
		APPOINTMENTBOOKING_clientGid,
		// Token: 0x04000AEC RID: 2796
		[SettingData("Maximum number of appointments in the future", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER, DefaultValue = 6)]
		APPOINTMENTBOOKING_maxNumApptsInFuture,
		// Token: 0x04000AED RID: 2797
		[SettingData("Make Students Confirm Tentative Appointments", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN)]
		APPOINTMENTBOOKING_MakeStudentsConfirmTentativeApps = 110038,
		// Token: 0x04000AEE RID: 2798
		[SettingData("FAQ Text", "Display", "The text to display on the 'FAQ' page.", Group.APPOINTMENTBOOKING, SettingSemantic.HTML, DefaultValue = "<h1 style='align:center;'>Appointment Booking Procedures</h1>\r\n\r\n<fieldset>\r\n<legend><h2>Q. How can I view my upcoming appointments?</h2></legend>\r\n<div>\r\nYou can access a list of your future appointments by clicking the 'Calendar' button in the menu.\r\n</div>\r\n</fieldset>\r\n\r\n<fieldset>\r\n<legend><h2>Q: How can I schedule an appointment?</h2></legend>\r\n<div>\r\n    You can browse the schedule for existing availabilities.  Here are the steps:\r\n    <br />\r\n    <ol>\r\n        <li>Click on 'Schedule an appointment' in the menu</li>\r\n        <li>Use the left and right arrows at the top left of the calendar to browse the schedule.  Note that availabilities will appear as boxes on the calendar.</li>\r\n        <li>Click once on an availability slot to schedule that it</li>\r\n        <li>Follow the prompts to confirm and finalize the booking</li>\r\n    </ol>\r\n</div>\r\n</fieldset>\r\n\r\n<fieldset>\r\n<legend><h2>Q. How can I cancel an appointment?</h2></legend>\r\n<div>\r\n    If you see a 'cancel' link beside your appointment on the 'Calendar' tab you can use this to cancel your appointment.  If you do not see this link please call or visit to cancel your appointment.\r\n</div>\r\n</fieldset>\r\n\r\n<fieldset>\r\n<legend><h2>Q. What can I do if I have a question or concern?</h2></legend>\r\n<div>\r\nIf your question/concern is urgent you should contact us through phone or in-person.  Otherwise you can click the 'Submit a comment' item in the menu to send us a question or comment.\r\n</div>\r\n</fieldset>\r\n")]
		APPOINTMENTBOOKING_HelpPageOverrideInfo = 110041,
		// Token: 0x04000AEF RID: 2799
		[SettingData("Additional info Page info", "Display", "", Group.APPOINTMENTBOOKING, SettingSemantic.HTML, IsHidden = true, DefaultValue = "<h1 class='PageTitle'>Additional Information</h1>\r\n<p>\r\n    Place your message here by using the tools in the ClockWork admin.\r\n</p>")]
		APPOINTMENTBOOKING_AdditionalInfoPageInfo,
		// Token: 0x04000AF0 RID: 2800
		[SettingData("Can the user cancel an appointment?", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = "false")]
		APPOINTMENTBOOKING_CanUserCancelAppointments,
		// Token: 0x04000AF1 RID: 2801
		[SettingData("Show the 'location/room' on the 'My upcoming appointments' page.", "Display", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		APPOINTMENTBOOKING_MyUpcomingAppointments_ShowLocation,
		// Token: 0x04000AF2 RID: 2802
		[ReferenceSetting("Facilitator notes screen number (use 0 to disable notes for facilitator)", "Facilitator", Group.APPOINTMENTBOOKING, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", IsHidden = true)]
		APPOINTMENTBOOKING_FacilitatorNotesScreenNum,
		// Token: 0x04000AF3 RID: 2803
		[SettingData("Maximum number of appointments per week (Sun-Sat)", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER, DefaultValue = 2)]
		APPOINTMENTBOOKING_MaxNumAppsPerWeek,
		// Token: 0x04000AF4 RID: 2804
		[SettingData("Don't allow booking of overlapping appointments for any user", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		APPOINTMENTBOOKING_NoConsecutiveOrOverlapping,
		// Token: 0x04000AF5 RID: 2805
		[SettingData("Forms for students to fill in for their own appointments (apptypeid=screennum,apptypeid=screennum...)", "Behaviour", "", Group.APPOINTMENTBOOKING, SettingSemantic.TEXT, DefaultValue = "", IsHidden = true)]
		APPOINTMENTBOOKING_StudentAppFormScreenNums = 110055,
		// Token: 0x04000AF6 RID: 2806
		[SettingData("My upcoming appointments intro", "Display", "", Group.APPOINTMENTBOOKING, SettingSemantic.HTML, DefaultValue = "")]
		APPOINTMENTBOOKING_MyUpcomingAppointmentsIntro,
		// Token: 0x04000AF7 RID: 2807
		[SettingData("Enable booking appointments", "Behaviour", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		APPOINTMENTBOOKING_EnableBookingAppointments,
		// Token: 0x04000AF8 RID: 2808
		[SettingData("Remove staff/tutors from the calendar who don't have any availability for the current view.  Note that this will mean that the calendar view will be constantly changing as the student moves back and forth through days.", "Behaviour", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		APPOINTMENTBOOKING_RemoveStaffTutorsFromCalendarWhoDontHaveAnyAvailability = 110060,
		// Token: 0x04000AF9 RID: 2809
		[ReferenceSetting("Appointment types to show", "My upcoming events", "Leave blank to show all appointment types.  Otherwise, only appointment types that are checked will appear in the 'My upcoming events' section on the web for the student to see.", Group.APPOINTMENTBOOKING, SettingSemantic.REFERENCE_ARRAY, "appointmenttypes", "apptypeid", "description", OverrideSql = "SELECT at.apptypeid,COALESCE(atg.title,'Ungrouped') + ': ' + at.description AS description FROM appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid WHERE at.isactive=1 ORDER BY atg.title,at.description")]
		APPOINTMENTBOOKING_AppointmentTypesToAllowInMyUpcomingEventsList = 110070,
		// Token: 0x04000AFA RID: 2810
		[SettingData("Cutoff time for booking appointments (only availabilities after the cutoff time will be available to the student)", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.CUTOFFTIME)]
		APPOINTMENTBOOKING_CutoffForBooking,
		// Token: 0x04000AFB RID: 2811
		[SettingData("Cutoff time for cancelling appointments", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.CUTOFFTIME, DefaultValue = "1")]
		APPOINTMENTBOOKING_CutoffForCancelling,
		// Token: 0x04000AFC RID: 2812
		[SettingData("Optional username control id", "Rules", "If not set, the data sync should update the username field anyway.  If this is set, the username field will be updated before calling the data sync after registration takes place.", Group.APPOINTMENTBOOKING, SettingSemantic.CONTROLID_PERSTUDENT, IsHidden = true)]
		APPOINTMENTBOOKING_OptionalUsernameCid,
		// Token: 0x04000AFD RID: 2813
		[SettingData("Hide the F.A.Q.", "Display", "", Group.APPOINTMENTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		APPOINTMENTBOOKING_HideFaq,
		// Token: 0x04000AFE RID: 2814
		[SettingData("Banned message to student", "Display", "The student will see this message if they have been banned and attempt to view the bookings calendar online.", Group.APPOINTMENTBOOKING, SettingSemantic.HTML, DefaultValue = "Your online booking privileges are currently suspended.  Please contact us in order to book your appointment directly.", IsHidden = true)]
		APPOINTMENTBOOKING_BannedMessageToStudent,
		// Token: 0x04000AFF RID: 2815
		[SettingData("Maximum number of appointments per day", "Rules", "", Group.APPOINTMENTBOOKING, SettingSemantic.INTEGER, DefaultValue = 2)]
		APPOINTMENTBOOKING_MaxNumAppsPerDay,
		// Token: 0x04000B00 RID: 2816
		[SettingData("Pre Calendar Questionnaire", "Rules", "If enabled this will redirect the student to a questionnaire form before they can reach the calendar showing availabilities.  The answers they select on the form will determine what types of availability channels are available to them.", Group.APPOINTMENTBOOKING, SettingSemantic.APPOINTMENTBOOKING_PRECALENDAR_QUESTIONNAIRE, SettingLevel = eSettingLevel.Advanced)]
		APPOINTMENTBOOKING_PreCalendarQuestionnaire,
		// Token: 0x04000B01 RID: 2817
		[Obsolete]
		[SettingData("Message when not agree to profile popup", "Display", "", Group.SELFREGISTRATION, SettingSemantic.TEXT)]
		SELFREGISTRATION_messageWhenNotAgreeToProfilePopup = 100000,
		// Token: 0x04000B02 RID: 2818
		[Obsolete]
		[SettingData("Message when not agree to courses popup", "Display", "", Group.SELFREGISTRATION, SettingSemantic.TEXT)]
		SELFREGISTRATION_messageWhenNotAgreeToCoursesPopup,
		// Token: 0x04000B03 RID: 2819
		[Obsolete]
		[SettingData("Message when not agree to accommodations popup", "Display", "", Group.SELFREGISTRATION, SettingSemantic.TEXT)]
		SELFREGISTRATION_messageWhenNotAgreeToAccommodationsPopup,
		// Token: 0x04000B04 RID: 2820
		[Obsolete]
		[ReferenceSetting("Profile form", "Rules", "", Group.SELFREGISTRATION, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description", AllowMultipleSelections = false)]
		SELFREGISTRATION_profileScreenNum,
		// Token: 0x04000B05 RID: 2821
		[Obsolete]
		[SettingData("Book appointment", "Rules", "", Group.SELFREGISTRATION, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGISTRATION_bookApp,
		// Token: 0x04000B06 RID: 2822
		[Obsolete]
		[SettingData("Message when not agree to accommodations summary", "Display", "", Group.SELFREGISTRATION, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGISTRATION_messageWhenNotAgreeToAccommodationsSummary,
		// Token: 0x04000B07 RID: 2823
		[Obsolete]
		[SettingData("Message when not agree to courses summary", "Display", "", Group.SELFREGISTRATION, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGISTRATION_messageWhenNotAgreeToCoursesSummary,
		// Token: 0x04000B08 RID: 2824
		[Obsolete]
		[SettingData("Message when not agree to profile summary", "Display", "", Group.SELFREGISTRATION, SettingSemantic.BOOLEAN, DefaultValue = false)]
		SELFREGISTRATION_messageWhenNotAgreeToProfileSummary,
		// Token: 0x04000B09 RID: 2825
		[Obsolete]
		[SettingData("Wizard list control id", "Behaviour", "", Group.SELFREGISTRATION, SettingSemantic.CONTROLID_PERSTUDENT, DefaultValue = 0)]
		SELFREGISTRATION_wizardListCid,
		// Token: 0x04000B0A RID: 2826
		[Obsolete]
		[SettingData("Done message", "Display", "", Group.SELFREGISTRATION, SettingSemantic.TEXT)]
		SELFREGISTRATION_doneMessage = 100024,
		// Token: 0x04000B0B RID: 2827
		[Obsolete]
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.SELFREGISTRATION, SettingSemantic.DATETIME, IsHidden = true)]
		SELFREGISTRATION_LastModifiedTime,
		// Token: 0x04000B0C RID: 2828
		[Obsolete]
		[SettingData("Student confirmation email", "Emails", "Gets sent to the student (and/or department) after the student completes the online registration.", Group.SELFREGISTRATION, SettingSemantic.EMAIL_TEMPLATE)]
		SELFREGISTRATION_StudentConfirmationEmail = 100030,
		// Token: 0x04000B0D RID: 2829
		[Obsolete]
		[SettingData("Instructor email", "Emails", "Gets sent to the instructor after the student completes the online registration.", Group.SELFREGISTRATION, SettingSemantic.EMAIL_TEMPLATE)]
		SELFREGISTRATION_InstructorEmail = 100032,
		// Token: 0x04000B0E RID: 2830
		[Obsolete]
		[SettingData("Re-activate students on submit", "_Main settings", "", Group.SELFREGISTRATION, SettingSemantic.BOOLEAN, DefaultValue = true)]
		SELFREGISTRATION_ReactivateStudentsOnSubmit = 100035,
		// Token: 0x04000B0F RID: 2831
		[Obsolete]
		[SettingData("Confidentiality agreement", "Rules", "Confidentiality agreement for the student to agree to", Group.SELFREGISTRATION, SettingSemantic.TEXT, DefaultValue = "<h3>Website Privacy & Security Policy</h3>\r\n<p>\r\nWe are committed to ensuring the privacy and accuracy of your confidential information. We have the utmost respect for your privacy and will not share your personal information with anyone without your explicit permission. All services provided on this Website are alternatively available in person.\r\n</p>\r\n<h3>Information we collect about you</h3>\r\n<p>\r\nWe will only collect and process your personal data for the purposes of providing the services delivered by this Website.  In addition some information is automatically collected and stored in the server logs, such as your Ip address.  Providing personal data is voluntary.  There will be a minimum data that we need to collect from you for the services that you sign up to.  We will let you know what data we require, if you wish to use our services, by indicating in the relevant fields of the webforms.\r\n</p>\r\n<h3>Statistics</h3>\r\n<p>\r\nThe Website is regularly monitored in order to supply you with the best service and to meet your expectations. For this purpose, we consult the statistics relating to use of our Website and develop the Website on the basis of this data.  Your information may also be used in our reports. User statistics are anonymous.\r\n</p>\r\n<h3>Security</h3>\r\n<p>\r\nThe Website uses a secure server to protect your information data. Secure server software is used to encrypt the information exchanged between your Web browser and our Website. This measure ensures the security of all your transactions when you use the Sites. We follow strict security procedures when filing and using the information you supply, and may request proof of your identity before supplying you with information. We take all reasonable steps to ensure the secrecy of your personal data and passwords.\r\n</p>\r\n<p>\r\nYou are fully responsible for maintaining the confidentiality of your login and your password and abstaining from communicating it to any other person and you are solely liable for activities that occur under your login and password. We disclaim all liabilities for inaccuracy of your personal data and in case of theft, loss, misuse, communication, fraudulent use of your login and password arising from your failure to comply with the above.\r\n</p>\r\n<h3>Cookies</h3>\r\n<p>\r\nThe Website may use cookies to ensure the smooth operation of your transactions.  Cookies are small information files that a Website can send to the hard disk of a personal computer for traceability reasons. They are not executable programs, and cannot contain viruses or applications. The cookies used only take up a minimal amount of space on your hard disk. You can always prevent cookies from being recorded on your computer by using the options provided by your browser. However, if you do so, some parts of the Site may not be functional.\r\n</p>")]
		SELFREGISTRATION_ConfidentialityAgreement = 10040,
		// Token: 0x04000B10 RID: 2832
		[Obsolete]
		[SettingData("Wizard welcome page text", "Display", "The text for the welcome page of the wizard", Group.SELFREGISTRATION, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Self Registration</h1>\r\n<p>Welcome to the Self Registration Wizard.  This wizard will guide you through the process of re-registering, and is for returning students only.  You may abort this process at any time by clicking the 'Cancel' button at the bottom of each page.</p>\r\n<p>Click the 'Next' button below to get started.</p>\r\n")]
		SELFREGISTRATION_WizardWelcomeText = 10042,
		// Token: 0x04000B11 RID: 2833
		[Obsolete]
		[SettingData("No accommodations message", "Error Messages", "", Group.SELFREGISTRATION, SettingSemantic.TEXT, DefaultValue = "You do not have any active accommodations at the current time.")]
		SELFREGISTRATION_ErrorMessage_NoAccommodations = 10045,
		// Token: 0x04000B12 RID: 2834
		[Obsolete]
		[SettingData("Only allow students who have been authorized", "Rules", "Enter a control id for a checkbox, or a control id and a lookuplist id for a drop list or radio group.  Example: 45, 45.2", Group.SELFREGISTRATION, SettingSemantic.CONTROLID_PERSTUDENT)]
		SELFREGISTRATION_AuthorizeStudentsControlId,
		// Token: 0x04000B13 RID: 2835
		[Obsolete]
		[SettingData("Not authorized by control id", "Error Messages", "", Group.SELFREGISTRATION, SettingSemantic.TEXT, DefaultValue = "You are not authorized to use the self-registration system.  Please contact your coordinator if you have any questions.")]
		SELFREGISTRATION_ErrorMessage_NotAuthorizedByControlId,
		// Token: 0x04000B14 RID: 2836
		[Obsolete]
		[SettingData("Profile step introduction text", "Display", "The text that will appear just under the title in the Profile step of the self registration wizard", Group.SELFREGISTRATION, SettingSemantic.HTML, DefaultValue = "Please review the information below.")]
		SELFREGISTRATION_ProfileStepIntro,
		// Token: 0x04000B15 RID: 2837
		[Obsolete]
		[SettingData("Cutoff: System active start", "Rules", "The self registration system will turn on here", Group.SELFREGISTRATION, SettingSemantic.CUTOFFTIME)]
		SELFREGISTRATION_Cutoff_start,
		// Token: 0x04000B16 RID: 2838
		[Obsolete]
		[SettingData("Cutoff: System active end", "Rules", "The self registration system will turn off here", Group.SELFREGISTRATION, SettingSemantic.CUTOFFTIME)]
		SELFREGISTRATION_Cutoff_end,
		// Token: 0x04000B17 RID: 2839
		[Obsolete]
		[SettingData("Access is disabled because of cutoff dates", "Error Messages", "", Group.SELFREGISTRATION, SettingSemantic.TEXT, DefaultValue = "The self-registration system is not available yet.  Please contact your coordinator if you have any questions.")]
		SELFREGISTRATION_ErrorMessage_AccessCutoff = 10055,
		// Token: 0x04000B18 RID: 2840
		[SettingData("Show notetaker confidentiality form", "Notetaker signup", "", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = true)]
		NOTETAKINGB_showNotetakerConfidentiality = 90000,
		// Token: 0x04000B19 RID: 2841
		[SettingData("Confidentiality agreement checkbox text", "Notetaker signup", "The text for the checkbox the notetaker has to check off to indicate they agree to the confidentiality agreement.  The confidentiality agreement text is found under 'Confidentiality agreement text'", Group.NOTETAKINGB, SettingSemantic.TEXT, DefaultValue = "I agree")]
		NOTETAKINGB_notetakerConfidentialityIAgreeWording,
		// Token: 0x04000B1A RID: 2842
		[SettingData("New notetaker welcome message", "Notetaker signup", "The welcome message for a new notetaker.", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_newNotetakerWelcomeMessage,
		// Token: 0x04000B1B RID: 2843
		[SettingData("Sample notes upload instructions", "Notetaker signup", "Sample notes upload instructions", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_sampleNotesUploadInstructions,
		// Token: 0x04000B1C RID: 2844
		[SettingData("Notetaker welcome message", "Notetakers display", "Main notetakers welcome message (on notetakingnotetakers/default.aspx)", Group.NOTETAKINGB, SettingSemantic.HTML, DefaultValue = "Your welcome message text should go here.  See 'Notetakers welcome message' in the settings.")]
		NOTETAKINGB_welcomeMsg,
		// Token: 0x04000B1D RID: 2845
		[SettingData("Notetaker additional info", "Notetakers display", "This message appears at the bottom of the main notetaker page (notetakerapp.aspx).", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_AdditionalInfoNotetaker,
		// Token: 0x04000B1E RID: 2846
		[SettingData("Student additional info", "Students display", "This message appears at the bottom of the main notetakee page (courses.aspx).", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_AdditionalInfoNotetakee,
		// Token: 0x04000B1F RID: 2847
		[SettingData("Confidentiality agreement text", "Notetaker signup", "This message appears for a new notetaker to agree to when they first sign up each year.  The text for the checkbox they have to check off to indicate agreement is in another setting (Confidentiality agreement checkbox text).", Group.NOTETAKINGB, SettingSemantic.HTML, DefaultValue = "Your confidentiality agreement text should go here (see the 'Confidentiality agreement text' in the settings).")]
		NOTETAKINGB_ConfidentialityAgreement,
		// Token: 0x04000B20 RID: 2848
		[SettingData("Label text message for address field on notetaker application form", "Notetaker signup", "", Group.NOTETAKINGB, SettingSemantic.TEXT)]
		NOTETAKINGB_NotetakerApplicationAddressIntro,
		// Token: 0x04000B21 RID: 2849
		[SettingData("Label text message for email field on notetaker application form", "Notetaker signup", "", Group.NOTETAKINGB, SettingSemantic.TEXT)]
		NOTETAKINGB_EmailIntro,
		// Token: 0x04000B22 RID: 2850
		[SettingData("Additional info for sample notes screen (notetakers)", "Notetakers display", "", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_sampleNotesAdditionalInfoNotetaker = 90011,
		// Token: 0x04000B23 RID: 2851
		[SettingData("Notetaker FAQ text", "Notetakers display", "The text for the body of the FAQ page (help.aspx).", Group.NOTETAKINGB, SettingSemantic.HTML, DefaultValue = "<h2 style='align:center;'>Note-taking Procedures</h2>\r\n\r\n<h2>Step 1</h2>\r\n<div>\r\nYou must upload your sample notes so that you can be selected as a notetaker.  Click on the 'Courses / Notes' link in the menu on the left, then click on the 'Upload sample notes' link for each course in the list and follow the instructions to upload your sample notes.\r\n</div>\r\n\r\n<h2>Step 2</h2>\r\n<div>\r\nYou will receive an email once you have been selected as a notetaker, to notify you that you should begin uploading your notes.\r\n</div>\r\n\r\n<h2>Step 3</h2>\r\n<div>\r\nOnce you have been selected as a notetaker, you must upload your lecture notes after each lecture.  Click on the 'Upload notes' button beside the course you have been selected for, and follow the directions to upload your notes.\r\n</div>\r\n")]
		NOTETAKINGB_Faq = 90013,
		// Token: 0x04000B24 RID: 2852
		[SettingData("Notetaker upload lecture notes message", "Notetakers display", "The text for the body of the FAQ page (help.aspx).", Group.NOTETAKINGB, SettingSemantic.TEXT)]
		NOTETAKINGB_UploadLectureNotesMessage,
		// Token: 0x04000B25 RID: 2853
		[SettingData("No sample notes Info", "Notetaker signup", "The message the student gets if they are signup up and don't have any sample notes to upload.", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_NoSampleNotesInfo = 90016,
		// Token: 0x04000B26 RID: 2854
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.NOTETAKINGB, SettingSemantic.DATETIME, IsHidden = true)]
		NOTETAKINGB_LastModifiedTime,
		// Token: 0x04000B27 RID: 2855
		[SettingData("Message for students when choosing a notetaker", "Students display", "", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_ChooseNotetakerInfo,
		// Token: 0x04000B28 RID: 2856
		[SettingData("The small message to display when defining the star for students choosing a notetaker (the star means the notetaker has already been chosen for at least one other student)", "Students display", "", Group.NOTETAKINGB, SettingSemantic.TEXT, Description = "Star note at bottom of table on choose notetaker page", DefaultValue = "Note: The star <img src='../../img/star_yellow.png' alt='Yellow Star' /> identifies notetakers who are currently providing notes to one or more students for this course.")]
		NOTETAKINGB_ChooseNotetakerInfoStarNote,
		// Token: 0x04000B29 RID: 2857
		[SettingData("Download lecture notes message", "Students display", "The message to display to students (or notetakers) when they are downloading lecture notes for a course.", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_DownloadLectureNotesInfo,
		// Token: 0x04000B2A RID: 2858
		[SettingData("Sample lecture notes message", "Students display", "The message to display to students when they are downloading sample notes for a course.", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_SampleNotesDownloadInfo,
		// Token: 0x04000B2B RID: 2859
		[SettingData("Sample notes additional info for students", "Students display", "", Group.NOTETAKINGB, SettingSemantic.HTML)]
		NOTETAKINGB_SampleNotesAdditionalInfoNotetakee,
		// Token: 0x04000B2C RID: 2860
		[SettingData("New notetaker signup email", "Notetaker emails", "", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notetaker signup</subject>\r\n    <isactive>1</isactive>\r\n<body>Hello #~firstname~#,\r\n\r\nThank you for your interest in becoming a volunteer note taker with AccessAbility Services.  \r\nIn order for your note taker application to be complete, please ensure that you upload your sample notes for:\r\n\r\n#~courses~#\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_RequestSampleNotes = 90024,
		// Token: 0x04000B2D RID: 2861
		[SettingData("Thank you for uploading sample notes email", "Notetaker emails", "Only sent when the notetaker uploads the first sample note for each course", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Thank you for uploading your sample notes for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nThank you for uploading your sample notes for #~course~#.  You will be contacted by email once a student selects you to become a notetaker for this course.\r\n\r\nPlease contact us if you have any questions.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_ThankyouForUploadingSampleNotes,
		// Token: 0x04000B2E RID: 2862
		[SettingData("Selected as notetaker email", "Notetaker emails", "Sent when a student selects a notetaker (only the first time a student selects them for a specific course)", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notification of selection as a notetaker for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nYou have been selected to be a notetaker for #~course~#.  Please login to our website and begin uploading your lecture notes.\r\n\r\nPlease contact us if you have any questions.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_SelectedAsNotetaker,
		// Token: 0x04000B2F RID: 2863
		[SettingData("Notetaker dropped out email", "Student emails", "Sent to all students receiving notes for a specific course when the notetaker indicates they can no longer take notes for that course.", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notification of notetaker un-availability for #~coursedescription~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nYour notetaker for #~coursedescription~# has indicated that they are no longer able to provide notes for this class.  \r\nYou may select another notetaker online if one is currently available.\r\n\r\nPlease contact us if you have any questions.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_NotetakerNotAvailable,
		// Token: 0x04000B30 RID: 2864
		[SettingData("Allow students to choose their own notetakers", "Student rules", "If true, the students will be able to select their own notetakers", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = true)]
		NOTETAKINGB_AllowStudentsToChooseTheirOwnNotetakers = 90029,
		// Token: 0x04000B31 RID: 2865
		[SettingData("Maximum sample notes a notetaker can upload", "Notetaker rules", "If this is greater than 0 the notetakers will be prompted to upload sample notes as part of the application process (ie. before they have been selected).", Group.NOTETAKINGB, SettingSemantic.INTEGER, DefaultValue = 3)]
		NOTETAKINGB_NotetakersMaxSampleNotesUploadCount,
		// Token: 0x04000B32 RID: 2866
		[SettingData("Equivalent courses stored procedure number", "_Main settings", "A stored procedure is used to find potential matching courses in the notetaker applications for a student course requirement - the stored procedure is called EquivalentCoursesx, where x is the number that matches the desired matchings.", Group.NOTETAKINGB, SettingSemantic.INTEGER, DefaultValue = "1")]
		NOTETAKINGB_EquivalentCourseStoredProcedureNumber,
		// Token: 0x04000B33 RID: 2867
		[SettingData("Welcome message for student", "Students display", "", Group.NOTETAKINGB, SettingSemantic.HTML, DefaultValue = "<div align='center'><h2>Note Taking Program</h2><br />\r\n<br />\r\nPlease click on ‘Courses / Notes’ on the left menu to view your note taking requests.<br />\r\n<br />\r\nIf you have any questions or need assistance, please contact us by phone or email<br />\r\n</div>\r\n")]
		NOTETAKINGB_welcomeMsgStudents,
		// Token: 0x04000B34 RID: 2868
		[SettingData("Allow students to cancel an assigned notetaker", "Student rules", "Note: This will only allow the student to cancel a notetaker for their course once.  If they assign a notetaker to a specific course, then cancel it, then assign another one, they will not be able to cancel themselves at this point.  They will be instructed to contact the department if they need to cancel the next notetaker.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = true)]
		NOTETAKINGB_AllowStudentsToCancelNotetaker = 90034,
		// Token: 0x04000B35 RID: 2869
		[SettingData("Allow notetakers to withdraw themselves as a notetaker", "Notetaker rules", "", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = true)]
		NOTETAKINGB_AllowNotetakersToCancelThemselves,
		// Token: 0x04000B36 RID: 2870
		[SettingData("Show a notetaker's sample notes in the 'Download notes' list for the students receiving notes", "Notetaker rules", "", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = true)]
		NOTETAKINGB_ShowSampleNotesInDownloadNotesList,
		// Token: 0x04000B37 RID: 2871
		[ReferenceSetting("Deprecated. Report to use to retrieve notetaker data from DataSync.", "Notetaker signup", Group.NOTETAKINGB, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false, IsHidden = true)]
		NOTETAKINGB_DataSync_PreviewNotetakerDataReportId,
		// Token: 0x04000B38 RID: 2872
		[ReferenceSetting("Deprecated. Report to use to retrieve notetaker course registrations from DataSync", "Notetaker signup", Group.NOTETAKINGB, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false, IsHidden = true)]
		NOTETAKINGB_DataSync_PreviewNotetakerCoursesReportId,
		// Token: 0x04000B39 RID: 2873
		[SettingData("Administrator email", "_Main settings", "", Group.NOTETAKINGB, SettingSemantic.TEXT)]
		NOTETAKINGB_AdminEmail = 90050,
		// Token: 0x04000B3A RID: 2874
		[SettingData("Minimum sample notes a notetaker can upload", "Notetaker rules", "If this is greater than 0 the notetakers will only show for the students to pick if they have uploaded at least this number of sample notes as part of the application process (ie. before they have been selected).", Group.NOTETAKINGB, SettingSemantic.INTEGER, DefaultValue = 3)]
		NOTETAKINGB_NotetakersMinSampleNotesUploadCount,
		// Token: 0x04000B3B RID: 2875
		[SettingData("Allow notetakers to upload sample notes", "Notetaker rules", "", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = true)]
		NOTETAKINGB_AllowNotetakersToUploadSampleNotes = 90060,
		// Token: 0x04000B3C RID: 2876
		[SettingData("Students faq", "Students display", "", Group.NOTETAKINGB, SettingSemantic.HTML, DefaultValue = "<h2 style='align:center;'>Note-taking Procedures</h2>\r\n<div class='ImportantNote'>You must be approved by your advisor for a notetaker accommodation before you can use the note-taking system.  The following procedures only apply to you if you have been approved for a note-taking accommodation.</div>\r\n\r\n<h2>Step 1</h2>\r\n<div>\r\nYou must indicate that you require a note-taker for each course that you would like to receive notes for.  Click on the 'Courses / Notes' link in the main menu, then click on 'No - change this' beside each course for which you require a note-taker.\r\n</div>\r\n\r\n<h2>Step 2</h2>\r\n<div>\r\nIf a note-taker is available for a course you will see a button called 'select a notetaker' in the 'Notetaker availability' column of your course list.  Click on the button to view the list of available notetakers.  You are able to browse sample notes that each notetaker has uploaded in order to help you make your decision.\r\n</div>\r\n\r\n<h2>Step 3</h2>\r\n<div>\r\nOnce you have selected a notetaker, you are able to download the notes that have been uploaded by your notetaker.  Click on the 'Notes' button beside the course you wish to download notes for.  Note that you should download your notes at least once per week.  If you wait until the night before a test or exam, the system could be unavailable and no one will be able to assist you after hours.\r\n</div>\r\n")]
		NOTETAKINGB_StudentsFaq = 90070,
		// Token: 0x04000B3D RID: 2877
		[SettingData("Notetaker approved for all courses accommodation checkbox control id", "_Main settings", "The control id of the accommodation checkbox that indicates the student should receive a notetaker for all of their courses.  The notetaker request does not have to be entered if this option is used; the first time the student logs in they will receive the option of indicating that they require notes for each course and the request will be created at that time.", Group.NOTETAKINGB, SettingSemantic.CONTROLID_PERSTUDENT)]
		NOTETAKINGB_NotetakerApprovedForAllCoursesCid = 90095,
		// Token: 0x04000B3E RID: 2878
		[SettingData("Notetaker dropped out email - staff notification", "Student emails", "Sent once to staff when the notetaker indicates they can no longer take notes for that course.  This email will only be sent if at least one student was receiving notes from this notetaker.", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notification of notetaker un-availability for #~coursedescription~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\n\r\nThis is an automated notification that a notetaker has dropped their availability for a course.  The notetaker for #~coursedescription~# has indicated that they are no longer able to provide notes for this class.  \r\nAll students who were receiving notes from this notetaker have received an automated notification email, directing them to login and select another notetaker if one is available.  The following students were affected by this:\r\n\r\n#~students~#\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_NotetakerNotAvailable_ForStaff,
		// Token: 0x04000B3F RID: 2879
		[SettingData("Notetaking intake form - choose courses message", "Notetakers display", "", Group.NOTETAKINGB, SettingSemantic.TEXT, DefaultValue = "Please select the course(s) you are available to become a potential notetaker for:")]
		NOTETAKINGB_NotetakerIntakeChooseCoursesMessage,
		// Token: 0x04000B40 RID: 2880
		[SettingData("New notetaker signup email (staff notification)", "Notetaker emails", "Sent once to staff when the notetaker fills out the online intake form and submits it.", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notification of notetaker sign-up</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>Hello,\r\n\r\nThis is an automated notification that a notetaker has signed up on the online system:\r\n\r\nName: #~firstname~# #~lastname~#\r\nEmail: #~email~#\r\nStudent no.: #~student_no~#\r\nCourses: #~courses~#\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_NewNotetakerSignup,
		// Token: 0x04000B41 RID: 2881
		[SettingData("Don't allow students to choose different notetakers for the same class", "Student rules", "If set to true, the first student to select a notetaker for a class will be able to choose from the full list of available potential notetakers, but students coming in after that will only be given the notetaker the first student selected as an option when selecting their notetaker.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_Dont_Allow_Students_To_Pick_Different_Notetakers_For_the_Same_Class = 90100,
		// Token: 0x04000B42 RID: 2882
		[ReferenceSetting("Report to use to retrieve notetaker student number from username.", "Notetaker signup", Group.NOTETAKINGB, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false)]
		NOTETAKINGB_ReportIdToRetreiveNotetakerStudentNumberFromUsername,
		// Token: 0x04000B43 RID: 2883
		[ReferenceSetting("Report to use to preview notetaker current registered course listing.", "Notetaker signup", Group.NOTETAKINGB, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false, IsHidden = true)]
		NOTETAKINGB_ReportIdToPreviewNotetakerRegisteredCourses,
		// Token: 0x04000B44 RID: 2884
		[ReferenceSetting("List of student web usernames that are allowed to use the notetaker section of the online notetaking system.", "Notetaker rules", "If anything is checked here, only those students who are checked will be able to use the online system.  All other students will be sent to a page explaining that the site is currently not available.", Group.NOTETAKINGB, SettingSemantic.REFERENCE_ARRAY, "people", "personid", "student_no", IsValueEncrypted = true, OverrideSql = "SELECT p.personid,p.student_no FROM people p WHERE p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)", OverrideSortByDisplayName = true)]
		NOTETAKINGB_RestrictLoginTo_Usernames,
		// Token: 0x04000B45 RID: 2885
		[SettingData("Intro message to students receiving notes on Course listing page", "Students display", "", Group.NOTETAKINGB, SettingSemantic.TEXT, DefaultValue = "Your courses are listed below.  Please ensure the 'I require a note taker' reads 'Yes' for each course that you require notes for.")]
		NOTETAKINGB_StudentCoursesIntroText,
		// Token: 0x04000B46 RID: 2886
		[SettingData("Allow students to view their assigned notetaker's name and contact information.", "Student rules", "", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_AllowStudentsToSeeNotetakerContactInfoAndName,
		// Token: 0x04000B47 RID: 2887
		[SettingData("Allow students to access notes from other notetakers.", "Student rules", "The student must have an assigned notetaker in order to view other notetaker's notes for the same course.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_AllowStudentsToAccessNotesFromOtherNotetakers,
		// Token: 0x04000B48 RID: 2888
		[SettingData("Student requested notes", "Student emails", "Sent each time the student changes 'I require a note taker' on a course, but only if no potential notetakers are currently available.", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~instructoremail~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notification of student requiring lecture notes for #~coursedescription~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>Hello #~instructorname~#,\r\n\r\nThis is an automated notification that a student requires a notetaker for one of your classes.\r\n\r\nName: #~firstname~# #~lastname~#\r\nEmail: #~email~#\r\nStudent no.: #~student_no~#\r\nCourse: #~courses~#\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_StudentRequestedNotes,
		// Token: 0x04000B49 RID: 2889
		[SettingData("Student requested notes - send the email out every time, even if a potential notetaker is currently available.", "Student emails", "", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_StudentRequestedNotes_SendEmailEveryTime,
		// Token: 0x04000B4A RID: 2890
		[SettingData("Only allow notetakers access if the 'Registration complete' checkbox on their profile is checked", "Notetaker rules", "If this is set to false notetakers will be allowed in even if 'Registration complete' is un-checked", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_NotetakerOnlyAllowAccessIfRegistrationIsComplete,
		// Token: 0x04000B4B RID: 2891
		[SettingData("Registration in-complete message to student", "Notetakers display", "This message will be displayed to the student if their registration is not complete (ie. 'registration complete' checkbox is NOT checked on their profile in ClockWork).  The setting called 'Only allow notetakers access if the Registration complete checkbox on their profile is checked' must be set to true for this setting to be relevant.", Group.NOTETAKINGB, SettingSemantic.HTML, DefaultValue = "Your registration is not complete; please check back again later or contact us for more information.")]
		NOTETAKINGB_Message_RegistrationIncomplete,
		// Token: 0x04000B4C RID: 2892
		[SettingData("The student's username is actually their student number", "Notetaker signup", "", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_UsernameIsActuallyStudentNumber,
		// Token: 0x04000B4D RID: 2893
		[SettingData("Allow students to access notes from other notetakers - include un-assigned notetakers notes.", "Student rules", "The student must have an assigned notetaker in order to view other notetaker's notes for the same course (this setting is only active if 'Allow students to access notes from other notetakers' is set to true).", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_AllowStudentsToAccessNotesFromOtherNotetakers_IncludeUnassignedNotetakersNotes,
		// Token: 0x04000B4E RID: 2894
		[SettingData("Allow students to access notes even if they don't have an assigned provider.", "Student rules", "The student will be able to access the 'Notes' button to download any available notes even if they don't have an assigned notetaker for their course.  Note that the student must still indicate 'I require notes for this course' first.  This would only make sense to use if you were allowing the student to access notes from other notetakers as well.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_AllowStudentsToAccessNotesEvenIfTheyDontHaveAnAssignedNotetaker,
		// Token: 0x04000B4F RID: 2895
		[SettingData("'sample notes' wording", "Notetakers display", "", Group.NOTETAKINGB, SettingSemantic.TEXT, DefaultValue = "sample notes")]
		NOTETAKINGB_SampleNotesWording,
		// Token: 0x04000B50 RID: 2896
		[SettingData("Notetaker uploaded new notes email", "Student emails", "Sent to all students receiving notes for a specific course when the notetaker uploads new lecture notes.", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>New lecture note(s) available for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nThis is an automatic notification.  Your notetaker for #~course~# has uploaded one or more new lecture notes, which are currently available for you to download.\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_NotetakerUploadedNewNotes,
		// Token: 0x04000B51 RID: 2897
		[SettingData("Make the 'School email' field mandatory on the profile", "Notetaker profile", "If this is set to false notetakers will be allowed to leave this field blank in their profile.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_NotetakerMandatoryEmail1,
		// Token: 0x04000B52 RID: 2898
		[SettingData("Make the 'Alternate email' field mandatory on the profile", "Notetaker profile", "If this is set to false notetakers will be allowed to leave this field blank in their profile.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_NotetakerMandatoryEmail2,
		// Token: 0x04000B53 RID: 2899
		[SettingData("Make the 'Home phone' field mandatory on the profile", "Notetaker profile", "If this is set to false notetakers will be allowed to leave this field blank in their profile.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_NotetakerMandatoryPhone1,
		// Token: 0x04000B54 RID: 2900
		[SettingData("Make the 'Alternate phone' field mandatory on the profile", "Notetaker profile", "If this is set to false notetakers will be allowed to leave this field blank in their profile.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_NotetakerMandatoryPhone2,
		// Token: 0x04000B55 RID: 2901
		[SettingData("Make the 'Mailing address' field mandatory on the profile", "Notetaker profile", "If this is set to false notetakers will be allowed to leave this field blank in their profile.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_NotetakerMandatoryAddress1,
		// Token: 0x04000B56 RID: 2902
		[SettingData("Make the 'Permanent address' field mandatory on the profile", "Notetaker profile", "If this is set to false notetakers will be allowed to leave this field blank in their profile.", Group.NOTETAKINGB, SettingSemantic.BOOLEAN, DefaultValue = false)]
		NOTETAKINGB_NotetakerMandatoryAddress2,
		// Token: 0x04000B57 RID: 2903
		[SettingData("Student cancelled notetaker email", "Student emails", "Sent once each time a student cancels a notetaker for a specific course. Please use service provider mail merge codes to access notetaker information, and the mail merge code 'why' to access the reason the student filled in explaining why they cancelled the notetaker. Use mail merge code 'notetakercoursedescription' to display the notetaker's course ('coursedescription' displays the student's course).", Group.NOTETAKINGB, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~adminemail~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Notification of notetaker cancellation</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>Hello,\r\n\r\nThis is an automated notification that a student has cancelled a notetaker for one of their courses:\r\n\r\nName: #~firstname~# #~lastname~# (#~student_no~#)\r\nEmail: #~email~#\r\nCourse: #~notetakercoursedescription~#\r\nNotetaker cancelled: #~serviceprovidername~# (#~serviceproviderstudentnumber~#)\r\nWhy student cancelled: #~why~#\r\n\r\n#~signature~#\r\n    </body>\r\n </email>")]
		NOTETAKINGB_Email_StudentCancelledNotetaker,
		// Token: 0x04000B58 RID: 2904
		[SettingData("How many days after the course end date should the student still be able to download notes for the course?", "Student rules", "You can use a positive number like 14 to mean 14 days after the course end date, or a negative number like -7 to mean 7 days before the course end date.", Group.NOTETAKINGB, SettingSemantic.INTEGER, DefaultValue = 14)]
		NOTETAKINGB_NumberOfDaysLectureNoteDownloadsWillBeAvailableAfterCourseEndDate,
		// Token: 0x04000B59 RID: 2905
		CLUBS_exemptProfileCids = 80000,
		// Token: 0x04000B5A RID: 2906
		CLUBS_profileScreenNum,
		// Token: 0x04000B5B RID: 2907
		CLUBS_groupNameCid,
		// Token: 0x04000B5C RID: 2908
		CLUBS_clubUserGroupId,
		// Token: 0x04000B5D RID: 2909
		CLUBS_profileFileCidMain,
		// Token: 0x04000B5E RID: 2910
		CLUBS_profileFileCidTemp,
		// Token: 0x04000B5F RID: 2911
		CLUBS_eventAppFormScreenNum,
		// Token: 0x04000B60 RID: 2912
		CLUBS_profileScreenNumTemp,
		// Token: 0x04000B61 RID: 2913
		CLUBS_organizationId,
		// Token: 0x04000B62 RID: 2914
		CLUBS_exemptEventFormCids,
		// Token: 0x04000B63 RID: 2915
		CLUBS_eventAppStartDateCids,
		// Token: 0x04000B64 RID: 2916
		CLUBS_eventAppStartTimeCid,
		// Token: 0x04000B65 RID: 2917
		CLUBS_eventAppEndDateCids,
		// Token: 0x04000B66 RID: 2918
		CLUBS_eventAppEndTimeCid,
		// Token: 0x04000B67 RID: 2919
		CLUBS_eventAppAppTypeId,
		// Token: 0x04000B68 RID: 2920
		[ReferenceSetting("Clubs user group ids", "_Main settings", Group.CLUBS, SettingSemantic.REFERENCE_ARRAY, "groups", "GroupID", "description")]
		CLUBS_userGids,
		// Token: 0x04000B69 RID: 2921
		CLUBS_userScreenNum,
		// Token: 0x04000B6A RID: 2922
		CLUBS_expiryDateCid,
		// Token: 0x04000B6B RID: 2923
		CLUBS_stakeholderAutoHideControls,
		// Token: 0x04000B6C RID: 2924
		CLUBS_authorizedUserNameOrPidCids,
		// Token: 0x04000B6D RID: 2925
		CLUBS_eventApprovedCid,
		// Token: 0x04000B6E RID: 2926
		CLUBS_eventNameCid,
		// Token: 0x04000B6F RID: 2927
		CLUBS_group_PrimaryContactFirstNameCid,
		// Token: 0x04000B70 RID: 2928
		CLUBS_group_PrimaryContactLastNameCid,
		// Token: 0x04000B71 RID: 2929
		CLUBS_group_PrimaryContactStudentNumCid,
		// Token: 0x04000B72 RID: 2930
		CLUBS_group_PrimaryContactIdCid,
		// Token: 0x04000B73 RID: 2931
		CLUBS_event_approvedLocationCid,
		// Token: 0x04000B74 RID: 2932
		CLUBS_stakeholderSubmittedCid,
		// Token: 0x04000B75 RID: 2933
		CLUBS_LOGIN_username_label,
		// Token: 0x04000B76 RID: 2934
		CLUBS_LOGIN_standalone_client_intake_screennum,
		// Token: 0x04000B77 RID: 2935
		CLUBS_LOGIN_standalone_create_account_message,
		// Token: 0x04000B78 RID: 2936
		CLUBS_publishedEventsCheckboxCid,
		// Token: 0x04000B79 RID: 2937
		CLUBS_publishedCids,
		// Token: 0x04000B7A RID: 2938
		CLUBS_publishedCidsCaptions,
		// Token: 0x04000B7B RID: 2939
		CLUBS_publishedDetailCids,
		// Token: 0x04000B7C RID: 2940
		CLUBS_publishedDetailCaptions,
		// Token: 0x04000B7D RID: 2941
		CLUBS_publishedDetailMultilineCids,
		// Token: 0x04000B7E RID: 2942
		CLUBS_group_emailCid,
		// Token: 0x04000B7F RID: 2943
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.CLUBS, SettingSemantic.DATETIME, IsHidden = true)]
		CLUBS_LastModifiedTime,
		// Token: 0x04000B80 RID: 2944
		[SettingData("Connection string", Group.LOG, SettingSemantic.TEXT, Description = "Use the ClockWork connection tool to generate the connection string")]
		LOG_ConnectionString = 60000,
		// Token: 0x04000B81 RID: 2945
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.LOG, SettingSemantic.DATETIME, IsHidden = true)]
		LOG_LastModifiedTime,
		// Token: 0x04000B82 RID: 2946
		[SettingData("Modules enabled appointment booking", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the online appointment booking module")]
		MODULES_ENABLED_AppointmentBooking = 10000,
		// Token: 0x04000B83 RID: 2947
		[SettingData("Modules enabled test booking", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the online test booking module")]
		MODULES_ENABLED_TestBooking,
		// Token: 0x04000B84 RID: 2948
		[SettingData("Modules enabled tutoring", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the online tutoring booking module")]
		MODULES_ENABLED_Tutoring,
		// Token: 0x04000B85 RID: 2949
		[SettingData("Modules enabled notetaking", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the notetaking module", IsHidden = true)]
		MODULES_ENABLED_Notetaking,
		// Token: 0x04000B86 RID: 2950
		[SettingData("Modules enabled notetaking", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the notetaking module")]
		MODULES_ENABLED_Notetakingb,
		// Token: 0x04000B87 RID: 2951
		[SettingData("Modules enabled instructor", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the instructor module")]
		MODULES_ENABLED_Instructor,
		// Token: 0x04000B88 RID: 2952
		[SettingData("Modules enabled clubs", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the clubs module")]
		MODULES_ENABLED_Clubs,
		// Token: 0x04000B89 RID: 2953
		[SettingData("Modules enabled tutors", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the tutors module")]
		MODULES_ENABLED_Tutors,
		// Token: 0x04000B8A RID: 2954
		[SettingData("Modules enabled Self-Registration", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the self-registration module")]
		MODULES_ENABLED_SelfReg,
		// Token: 0x04000B8B RID: 2955
		[SettingData("Modules enabled workshops", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the workshops module")]
		MODULES_ENABLED_Workshops,
		// Token: 0x04000B8C RID: 2956
		[SettingData("Modules kiosk clubs", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the kiosk module")]
		MODULES_ENABLED_Kiosk,
		// Token: 0x04000B8D RID: 2957
		[SettingData("Modules enabled staff calendar", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the staff calendar module")]
		MODULES_STAFF_CALENDAR,
		// Token: 0x04000B8E RID: 2958
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.MODULES, SettingSemantic.DATETIME, IsHidden = true)]
		MODULES_LastModifiedTime,
		// Token: 0x04000B8F RID: 2959
		[SettingData("Modules enabled AT", "Active Modules", "", Group.MODULES, SettingSemantic.BOOLEAN, Description = "Enable the AT module")]
		MODULES_ENABLED_AT,
		// Token: 0x04000B90 RID: 2960
		[SettingData("Message Appointment Booking", "Module messages", "", Group.MODULES, SettingSemantic.HTML, Description = "")]
		MODULES_MessageAppointmentBooking,
		// Token: 0x04000B91 RID: 2961
		[SettingData("Message Instructor", "Module messages", "", Group.MODULES, SettingSemantic.HTML)]
		MODULES_MessageInstructor,
		// Token: 0x04000B92 RID: 2962
		[SettingData("Modules enabled Install Check", "Module messages", "Enable the Install Check module", Group.MODULES, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		MODULES_ENABLED_InstallCheck,
		// Token: 0x04000B93 RID: 2963
		[SettingData("Modules enabled Admin Settings", "Active Modules", "Enable the Admin Settings module", Group.MODULES, SettingSemantic.BOOLEAN, DefaultValue = true)]
		MODULES_ENABLED_AdminSettings,
		// Token: 0x04000B94 RID: 2964
		[SettingData("Modules enabled Test Booking Non-Accommodated", "Active Modules", "Enable the Test Booking Non-Accommodated module", Group.MODULES, SettingSemantic.BOOLEAN, DefaultValue = false)]
		MODULES_ENABLED_TestBookingAlt = 10017,
		// Token: 0x04000B95 RID: 2965
		[SettingData("Modules enabled Intake form", "Active Modules", "Enable the Intake Registration Form module", Group.MODULES, SettingSemantic.BOOLEAN, DefaultValue = false)]
		MODULES_ENABLED_Intake = 10020,
		// Token: 0x04000B96 RID: 2966
		[SettingData("Modules enabled Veterans", "Active Modules", "Enable the Veteran module", Group.MODULES, SettingSemantic.BOOLEAN, DefaultValue = false)]
		MODULES_ENABLED_Veterans,
		// Token: 0x04000B97 RID: 2967
		[SettingData("Modules enabled Online Forms", "Active Modules", "Enable the Online Forms module", Group.MODULES, SettingSemantic.BOOLEAN, DefaultValue = false)]
		MODULES_ENABLED_OnlineForms,
		// Token: 0x04000B98 RID: 2968
		[SettingData("Login authentication method", Group.LOGIN, SettingSemantic.TEXT, IsHidden = true, DefaultValue = "debug")]
		LOGIN_AuthenticationMethod = 20000,
		// Token: 0x04000B99 RID: 2969
		[SettingData("Login username type", "Misc", "", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "student_no")]
		LOGIN_UsernameType,
		// Token: 0x04000B9A RID: 2970
		[SettingData("Login message (optional - appears underneath the 'Login' button)", "Login display", "", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "")]
		LOGIN_LoginMessage = 20004,
		// Token: 0x04000B9B RID: 2971
		[SettingData("I don't have a login url", Group.LOGIN, SettingSemantic.TEXT, IsHidden = true)]
		LOGIN_IDontHaveALoginUrl = 2005,
		// Token: 0x04000B9C RID: 2972
		[SettingData("I forgot my password url", Group.LOGIN, SettingSemantic.TEXT, IsHidden = true)]
		LOGIN_IForgotMyPasswordUrl,
		// Token: 0x04000B9D RID: 2973
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.LOGIN, SettingSemantic.DATETIME, IsHidden = true)]
		LOGIN_LastModifiedTime,
		// Token: 0x04000B9E RID: 2974
		[SettingData("DEPRECATED: Login authentication methods", "Deprecated", "", Group.LOGIN, SettingSemantic.LOGINAUTHENTICATIONMETHODS, DefaultValue = "<authenticationmethods>\r\n\t                            <authenticationmethod name=\"ldap\" type=\"ldap\" Ldapserver=\"builtin\" activedirectory=\"0\" enabled=\"1\" />\r\n\t                            <authenticationmethod name=\"ldap2\" type=\"ldap\" Ldapserver=\"ldap.tpro.ca\" ldapport=\"\" enabled=\"1\" />\r\n\t                            <authenticationmethod name=\"instructor\" type=\"instructor\" enabled=\"1\" />\r\n\t                            <authenticationmethod name=\"clockwork\" type=\"clockwork\" groupids=\"1,2,10\" enabled=\"1\" />\r\n\t                            <authenticationmethod name=\"custom\" type=\"custom\" enabled=\"1\" />\r\n                                <authenticationmethod name=\"customnotetaker\" type=\"custom\" mode=\"notetaker\" enabled=\"1\" />\r\n                            </authenticationmethods>")]
		LOGIN_AuthenticationMethods = 20010,
		// Token: 0x04000B9F RID: 2975
		[SettingData("DEPRECATED: Login groups", "Deprecated", "", Group.LOGIN, SettingSemantic.XML, DefaultValue = "<groups>\r\n\t                            <group type=\"student\" enabled=\"1\">\r\n\t\t                            <authenticationmethods>\r\n\t\t\t                            <authenticationmethod name=\"ldap\" enabled=\"1\" >\r\n\t\t\t\t                            <lookupmethods>\r\n\t\t\t\t\t                            <lookupmethod type=\"dynamictextbox\" postfix=\"@tpro.ca\" cid=\"332\" />\r\n\t\t\t\t\t                            <lookupmethod type=\"dynamictextbox\" postfix=\"@technoprosolutions.com\" cid=\"332\" />\r\n                                            </lookupmethods>\r\n\t\t\t                            </authenticationmethod>\r\n                                        <authenticationmethod name=\"custom\" enabled=\"1\" >\r\n\t\t\t\t                            <lookupmethods>\r\n                        \t\t\t\t\t    <lookupmethod type=\"student_no\" />\r\n                                            </lookupmethods>\r\n\t\t\t                            </authenticationmethod>\r\n\t\t                            </authenticationmethods>\r\n\t                            </group>\r\n\t                            <group type=\"staff\" enabled=\"1\">\r\n\t\t                            <authenticationmethods>\r\n\t\t\t                            <authenticationmethod name=\"ldap\" enabled=\"1\" >\r\n\t\t\t\t                            <lookupmethods>\r\n\t\t\t\t\t                            <lookupmethod type=\"dynamictextbox\" postfix=\"@tpro.ca\" cid=\"442\" />\t\t\t\t\r\n                                            </lookupmethods>\r\n\t\t\t                            </authenticationmethod>\r\n\t\t\t                            <authenticationmethod name=\"clockwork\" enabled=\"1\">\r\n\t\t\t\t                            <lookupmethods>\r\n\t\t\t\t\t                            <lookupmethod type=\"userinfo\" groupids=\"1,10\" />\r\n                                            </lookupmethods>\r\n\t\t\t                            </authenticationmethod>\r\n\t\t                            </authenticationmethods>\r\n\t                            </group>\r\n                                <group type=\"notetakers\" enabled=\"1\">\r\n\t                                <authenticationmethods>\r\n\t\t                                <authenticationmethod name=\"ldap\" enabled=\"1\">\r\n\t\t\t                                <lookupmethods>\r\n\t\t\t\t                                <lookupmethod type=\"notetakeremail\" postfix=\"@tpro.ca\" />\t\t\t\t\r\n                                            </lookupmethods>\r\n\t\t                                </authenticationmethod>\r\n                                        <authenticationmethod name=\"customnotetaker\" enabled=\"1\" >\r\n\t\t\t\t                            <lookupmethods>\r\n                        \t\t\t\t\t    <lookupmethod type=\"notetakeremail\" postfix=\"@tpro.ca\" />\r\n                                            </lookupmethods>\r\n\t\t\t                            </authenticationmethod>\r\n\t                                </authenticationmethods>\r\n                                </group>\r\n\t                            <group type=\"instructors\" enabled=\"1\">\r\n\t\t                            <authenticationmethods>\r\n\t\t\t                            <authenticationmethod name=\"ldap\" enabled=\"1\">\r\n\t\t\t\t                            <lookupmethods>\r\n\t\t\t\t\t                            <lookupmethod type=\"instructoremail\" postfix=\"@tpro.ca\" />\r\n                                            </lookupmethods>\r\n\t\t\t                            </authenticationmethod>\r\n\t\t\t                            <authenticationmethod name=\"instructor\" enabled=\"1\">\r\n\t\t\t\t                            <lookupmethods>\r\n\t\t\t\t\t                            <lookupmethod type=\"instructoremail\" />\r\n                                            </lookupmethods>\r\n\t\t\t                            </authenticationmethod>\r\n\t\t                            </authenticationmethods>\r\n\t                            </group>\r\n                            </groups>")]
		LOGIN_Groups,
		// Token: 0x04000BA0 RID: 2976
		[SettingData("Try to login first without credentials", "Portal", "If true, the login page will attempt to login the user without credentials first.  If this passes, the user will never see the ClockWork login form and will be immediately re-directed back to where they were trying to go in the first page.  If this fails the user will be directed to the login page as normal.", Group.LOGIN, SettingSemantic.BOOLEAN, DefaultValue = false)]
		LOGIN_LoginFirstWithoutCredenntials = 20020,
		// Token: 0x04000BA1 RID: 2977
		[SettingData("Url for login page (where credentials are collected)", "Portal", "This is defaulted to 'login.aspx', which is the local login page for the module.  If the user should never be sent here, then overriding it is a good idea.  Or, if you have your own login page you can override this to send the user there to provide their credentials.  Your login page should send back to a page you create somewhere in the custom/login folder in the ClockWork application - this page will check some form or session variable(s) to see if the user was successfully authenticated, and then return to the page the user was originally trying to get to.", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "Login.aspx")]
		LOGIN_CollectCredentialsUrl = 20026,
		// Token: 0x04000BA2 RID: 2978
		[SettingData("Student login instruction text", "Login display", "", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "To access this site you will need to log in with your user name and password. Please remember to log out when you are done.")]
		LOGIN_StudentLoginInstructionText = 20031,
		// Token: 0x04000BA3 RID: 2979
		[SettingData("Student login username label text", "Login display", "", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "User name:")]
		LOGIN_StudentLoginUsernameLabelText,
		// Token: 0x04000BA4 RID: 2980
		[SettingData("Student login title", "Login display", "", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "Log In")]
		LOGIN_StudentLoginTitle = 20050,
		// Token: 0x04000BA5 RID: 2981
		[SettingData("Allowed to login as a student/instructor/notetaker", "Staff Options", "Enter a comma separated list of personids who are allowed to login as students/instructors/notetakers", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "")]
		LOGIN_AllowedToLoginAsAStudentInstructorNotetaker_pids,
		// Token: 0x04000BA6 RID: 2982
		[SettingData("CAS ServiceValidate Url", "CAS Authentication", "Eg: https://school.ca/cgi-bin/WebObjects/cas.woa/wa/serviceValidate", Group.LOGIN, SettingSemantic.TEXT, DefaultValue = "")]
		LOGIN_CAS_ServiceValidateUrl,
		// Token: 0x04000BA7 RID: 2983
		[SettingData("Authentication Context", "_Main settings", "", Group.LOGIN, SettingSemantic.AUTHENTICATION_CONTEXT, DefaultValue = "")]
		LOGIN_AuthenticationContext,
		// Token: 0x04000BA8 RID: 2984
		[SettingData("Authorization Context", "_Main settings", "", Group.LOGIN, SettingSemantic.AUTHORIZATION_CONTEXT, DefaultValue = "<AuthorizationContext>\r\n    <AuthorizationContextItem type=\"2\" title=\"Student by username\" lookupmethod=\"1\" lookupmethodcid=\"0\" />\r\n    <AuthorizationContextItem type=\"4\" title=\"Notetaker by username\" lookupmethod=\"1\" />\r\n    <AuthorizationContextItem type=\"8\" title=\"Instructor by username\" lookupmethod=\"1\" />\r\n    <AuthorizationContextItem type=\"16\" title=\"Alt contact by username\" lookupmethod=\"1\" />\r\n    <AuthorizationContextItem type=\"1\" title=\"Staff by username\" lookupmethod=\"2\" />\r\n</AuthorizationContext>")]
		LOGIN_AuthorizationContext,
		// Token: 0x04000BA9 RID: 2985
		[SettingData("Student username field", "Misc", "Optional - the username field can be extracted from the authorization rules under normal circumstance.  The username field is required for intake forms.", Group.LOGIN, SettingSemantic.CONTROLID_PERSTUDENT)]
		LOGIN_StudentUsernameControlId,
		// Token: 0x04000BAA RID: 2986
		[SettingData("Verbose logging for authentication/authorization enabled", "Misc", "", Group.LOGIN, SettingSemantic.BOOLEAN, Description = "Enable the verbose logging for authentication and authorization", DefaultValue = false)]
		LOGIN_EnableVerboseLoggingForAuthenticationAuthorization,
		// Token: 0x04000BAB RID: 2987
		[SettingData("Private hashing authentication key", "Single Sign-On", "", Group.LOGIN, SettingSemantic.PASSWORD, Description = "Private key to verify hashing tokens - this key will be added to the plaintext before hashing.")]
		LOGIN_Hashing_Authentication_key,
		// Token: 0x04000BAC RID: 2988
		[SettingData("Private hashing authentication salt", "Single Sign-On", "", Group.LOGIN, SettingSemantic.PASSWORD, Description = "Secret salt - this will be used to generate the hash that will be compared with the provided hash")]
		LOGIN_Hashing_Authentication_salt,
		// Token: 0x04000BAD RID: 2989
		[ReferenceSetting("Report to use to format environment variables before sending them to the authentication functions.", "_Main settings", "Leave blank or 0 to disable", Group.LOGIN, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false)]
		LOGIN_ReportToTransformIncomingEnvironmentVariablesForAuthentication,
		// Token: 0x04000BAE RID: 2990
		[ReferenceSetting("Report to use further process authentication results after each main authentication completes", "_Main settings", "Leave blank or 0 to disable. The report will get the following parameters (all string values): contextitemtype (2=ldap,3=ad,4=cas,5=shib,6=portal,1=clockwork), authentication=1 or 0, resusername=authenticatedusername if available, resstudent_no=authenticated snum if available, username=original username if available.  The report should return authenticated to override the authenticated value (if no override is necessary then return DBNull or don't add authenticated as a column), student_no (optional), username (optional).", Group.LOGIN, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false)]
		LOGIN_PostAuthenticationReport,
		// Token: 0x04000BAF RID: 2991
		[SettingData("Force 'Authentication required' for all pages", "_Main settings", "If set to True, this will make all pages in the ClockWork web application require authentication for the user.  This is useful for systems like Shibboleth.", Group.LOGIN, SettingSemantic.BOOLEAN)]
		LOGIN_ForceAuthenticationRequiredForAllPages,
		// Token: 0x04000BB0 RID: 2992
		[SettingData("Students allowed to book exams", "_Main settings", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_StudentsAllowedToBookExams = 230010,
		// Token: 0x04000BB1 RID: 2993
		[Obsolete("Use EXAMBOOKING_CutoffBookingDate instead")]
		[SettingData("Minimum number of days ahead of the class that a student can book an exam.", "Rules", "", Group.EXAMBOOKING, SettingSemantic.INTEGER, DefaultValue = 7, IsHidden = true)]
		EXAMBOOKING_WizardSetting_MinDaysAheadToBook = 230017,
		// Token: 0x04000BB2 RID: 2994
		[SettingData("Confirm booking finish button text", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT)]
		EXAMBOOKING_WizardSetting_ConfirmBookingFinishButtonText,
		// Token: 0x04000BB3 RID: 2995
		[SettingData("Confirm booking finish message", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT)]
		EXAMBOOKING_WizardSetting_ConfirmBookingMsg,
		// Token: 0x04000BB4 RID: 2996
		[SettingData("Accommodations checked by default", "Rules", "If true, all accommodations will be checked by default when a student is booking a test (they will have to un-check ones they don't need for the test).  If false, all accommodations will be un-checked and the student will have to check the ones they need.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_WizardSetting_AccommodationsDefaultChecked,
		// Token: 0x04000BB5 RID: 2997
		[SettingData("Hide the continuing education drop list", "Display", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		EXAMBOOKING_WizardSetting_HideContinuingEducationDropList,
		// Token: 0x04000BB6 RID: 2998
		[SettingData("Welcome message for the exam booking wizard (step 1)", "Display", "", Group.EXAMBOOKING, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Online Final Exam Booking</h1>\r\n<p>Welcome to the Online Final Exam Booking wizard.  This wizard will guide you through the process of scheduling your final exam with us.  You may abort this process at any time by clicking the 'Cancel' button at the bottom of each page.</p>\r\n<p>Please be aware that your instructor will receive an email notification with the details of your exam booking.</p>\r\n<p>You will need to have the following information handy in order to successfully schedule your test:</p>\r\n<ol>\r\n    <li>The name of the course you want to schedule a test for, and the instructor's name and email address</li>\r\n    <li>The date, start time and duration of the test the class will be writing</li>\r\n    <li>You must be scheduling your exam a minimum of seven (7) days before the class is writing</li>\r\n</ol>\r\n<p>Click the 'Next' button below to get started.</p>")]
		EXAMBOOKING_WizardSetting_WelcomeMsg,
		// Token: 0x04000BB7 RID: 2999
		[SettingData("Booking completed message", "Display", "This message is displayed to the student after they have booked their test.", Group.EXAMBOOKING, SettingSemantic.HTML, DefaultValue = "<h1>Thank you for your submission.</h1>")]
		EXAMBOOKING_WizardSetting_FinishedBookingMsg,
		// Token: 0x04000BB8 RID: 3000
		[SettingData("Exam booking wizard setting test types enabled", "Display", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		EXAMBOOKING_WizardSetting_TestTypesEnabled,
		// Token: 0x04000BB9 RID: 3001
		[SettingData("Confirmation page - I agree text", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT)]
		EXAMBOOKING_WizardSetting_ConfirmationPage_IAgreeText,
		// Token: 0x04000BBA RID: 3002
		[SettingData("Confirmation page - Intro text", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT)]
		EXAMBOOKING_WizardSetting_ConfirmationPage_IntroText,
		// Token: 0x04000BBB RID: 3003
		[ReferenceSetting("Additional information form", "Display", "", Group.EXAMBOOKING, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum,
		// Token: 0x04000BBC RID: 3004
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.EXAMBOOKING, SettingSemantic.DATETIME, IsHidden = true)]
		EXAMBOOKING_LastModifiedTime,
		// Token: 0x04000BBD RID: 3005
		[SettingData("Assets", "_Main settings", "", Group.EXAMBOOKING, SettingSemantic.ASSETS, DefaultValue = "<assets>\r\n  <asset>\r\n    <title>Computer</title>\r\n    <id>COMPUTER</id>\r\n    <description>\r\n    </description>\r\n    <score>100</score>\r\n    <accommodations>\r\n    </accommodations>\r\n    <isactive>1</isactive>\r\n  </asset>\r\n</assets>")]
		EXAMBOOKING_Assets,
		// Token: 0x04000BBE RID: 3006
		[SettingData("Rooms", "_Main settings", "", Group.EXAMBOOKING, SettingSemantic.ROOMS, DefaultValue = "<rooms>\r\n\t<room pid=\"32\" title=\"32\" type=\"RegularRoom\">\r\n\t\t<assets>\r\n\t\t\t<asset id=\"COMPUTER\" />\r\n\t\t</assets>\r\n\t</room>            \r\n</rooms>")]
		EXAMBOOKING_Rooms,
		// Token: 0x04000BBF RID: 3007
		[SettingData("Special Accommodations", "_Main settings", "", Group.EXAMBOOKING, SettingSemantic.SPECIALACCOMMODATIONS, DefaultValue = "<specialaccommodations>\r\n</specialaccommodations>")]
		EXAMBOOKING_SpecialAccommodations,
		// Token: 0x04000BC0 RID: 3008
		[SettingData("Student booking confirmation email", "Emails", "Gets automatically sent to the student each time they book a test. Codes available: [classstartdate,classenddate,classstarttime,classendtime,classduration,startdate,enddate,classstarttime,classendtime,duration,room,email,firstname,lastname,student_no,name,accommodations,course,personid,appointmentid,instructor,instructoremail]", Group.EXAMBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Confirmation of your test booking request for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nYou have successfully submitted your test booking request for #~course~#:\r\n\r\n#~startdate~# . #~starttime~# to #~endtime~#\r\n\r\nPlease contact us if you have any questions, or need to cancel or reschedule your test.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		EXAMBOOKING_Email_StudentBookingConfirmation = 230050,
		// Token: 0x04000BC1 RID: 3009
		[ReferenceSetting("Appointment type to use for exam bookings", "Rules", "", Group.EXAMBOOKING, SettingSemantic.REFERENCE_ARRAY, "appointmenttypes", "apptypeid", "description", OverrideSql = "SELECT at.apptypeid,coalesce(atg.title,'Ungrouped') + ': ' + at.description AS description FROM appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid WHERE at.isactive=1 ORDER BY atg.title,at.description")]
		EXAMBOOKING_AppointmentTypeToUseForBooking = 230053,
		// Token: 0x04000BC2 RID: 3010
		[SettingData("Are exams to be booked as tentative?", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_BookTestsAsTentative,
		// Token: 0x04000BC3 RID: 3011
		[SettingData("Is the student allowed to select a date and time for the exam that hasn't already been entered into the system?", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		EXAMBOOKING_StudentAllowedToSelectOwnDateTime,
		// Token: 0x04000BC4 RID: 3012
		[ReferenceSetting("Room to use for all room availability", "_Main settings", "", Group.EXAMBOOKING, SettingSemantic.REFERENCE_ARRAY, "people", "apptypeid", "firstname", IsValueEncrypted = true, OverrideSql = "SELECT p.personid,p.firstname FROM people p WHERE p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)")]
		EXAMBOOKING_OverrideRoomPidForAvailability = 230070,
		// Token: 0x04000BC5 RID: 3013
		[SettingData("'Select a date time' message to students (override)", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Please select a date and time from the list of available dates and times below.  If none of the date/times in the list below will work for you then please contact us to see if alternate arrangements can be made.  We can be reached at (ask for assistance with test booking).")]
		EXAMBOOKING_SelectADateTimeMessageToStudents,
		// Token: 0x04000BC6 RID: 3014
		[SettingData("Is the student allowed to select from a list of dates/times for previously submitted exams?", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_StudentAllowedToSelectPreviousDateTimes,
		// Token: 0x04000BC7 RID: 3015
		[SettingData("Institution Name", "_Main settings", "", Group.EXAMBOOKING, SettingSemantic.TEXT, IsHidden = true)]
		EXAMBOOKING_InstitutionName,
		// Token: 0x04000BC8 RID: 3016
		[SettingData("Is the student allowed to select from a list of dates/times class test definitions?", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions = 230075,
		// Token: 0x04000BC9 RID: 3017
		[SettingData("Is the student allowed to select from a list of dates/times class test definitions with typecode='F'?", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar,
		// Token: 0x04000BCA RID: 3018
		[SettingData("Instructor email when student books an exam", "Emails", "Gets automatically sent to the instructor each time a student books an exam.  Note that the instructor may receive several emails for the same course if they have multiple students registered with Disability Services. Codes available: [classstartdate,classenddate,classstarttime,classendtime,classduration,startdate,enddate,classstarttime,classendtime,duration,room,email,firstname,lastname,student_no,name,accommodations,course,personid,appointmentid,instructor,instructoremail]", Group.EXAMBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~instructoremail~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Confirmation of exam booking request for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>Hello #~instructorname~#,\r\n\r\nA student has just submitted an exam booking request for #~course~#:\r\n\r\n#~firstname~# #~lastname~# . #~student_no~#\r\n#~startdate~# . #~starttime~# to #~endtime~#\r\n\r\nPlease contact us if you have any questions, or need to cancel or reschedule your exam.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		EXAMBOOKING_Email_StudentBookingConfirmationForInstructor = 230085,
		// Token: 0x04000BCB RID: 3019
		[SettingData("Exam booking coordinator email", "Emails", "Used for communication to the test booking coordinator from the ClockWork software.  (if a student modifies or enters a new instructor name or email, etc.)", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		EXAMBOOKING_TestBookingCoordinatorEmail = 230087,
		// Token: 0x04000BCC RID: 3020
		[SettingData("Contact information", "Display", "Contact information for the department that will be displayed to the student.", Group.EXAMBOOKING, SettingSemantic.HTML)]
		EXAMBOOKING_DepartmentContactInformation,
		// Token: 0x04000BCD RID: 3021
		[SettingData("Rules", "_Main settings", "", Group.EXAMBOOKING, SettingSemantic.TESTRULES, DefaultValue = "<rules>\r\n\t<rule ordernum=\"1\" title=\"All non-virtual rooms\"\r\n        active=\"1\"\r\n        roomstoexclude=\"\"\r\n        includenonvirtualrooms=\"1\" includevirtualrooms=\"0\"\r\n        allowedminutesbefore=\"0\" allowedminutesafter=\"0\"\r\n        stoplookingiffoundatleastone=\"1\" \r\n        shifttimetomatchendofday=\"1\" enforceoverlapwithclasstime=\"0\">\r\n    </rule>\r\n    <rule ordernum=\"2\" title=\"Virtual rooms only\" \r\n        active=\"1\"\r\n        roomstoexclude=\"\"\r\n        includenonvirtualrooms=\"0\" includevirtualrooms=\"1\" \r\n        allowedminutesbefore=\"0\" allowedminutesafter=\"0\"\r\n        stoplookingiffoundatleastone=\"1\" \r\n        shifttimetomatchendofday=\"1\" enforceoverlapwithclasstime=\"0\">\r\n\t</rule>            \r\n</rules>")]
		EXAMBOOKING_Rules = 230090,
		// Token: 0x04000BCE RID: 3022
		[SettingData("code_FindPotentialBookingsStart", "Custom Rules", "Custom c# code for finding potential bookings", Group.EXAMBOOKING, SettingSemantic.CSHARPCODE)]
		EXAMBOOKING_code_FindPotentialBookingsStart,
		// Token: 0x04000BCF RID: 3023
		[SettingData("code_FindPotentialBookingsMid", "Custom Rules", "Custom c# code for finding potential bookings", Group.EXAMBOOKING, SettingSemantic.CSHARPCODE)]
		EXAMBOOKING_code_FindPotentialBookingsMid,
		// Token: 0x04000BD0 RID: 3024
		[SettingData("code_FindPotentialBookingsEnd", "Custom Rules", "Custom c# code for finding potential bookings", Group.EXAMBOOKING, SettingSemantic.CSHARPCODE)]
		EXAMBOOKING_code_FindPotentialBookingsEnd,
		// Token: 0x04000BD1 RID: 3025
		[SettingData("code_FindPotentialBookingsMisc", "Custom Rules", "Custom c# code for finding potential bookings.  Function definition(s) should be included, this code will be part of the empty area of the class.", Group.EXAMBOOKING, SettingSemantic.CSHARPCODE)]
		EXAMBOOKING_code_FindPotentialBookingsMisc,
		// Token: 0x04000BD2 RID: 3026
		[SettingData("Dont ask student to confirm instructor information", "Display", "This will remove step 3 from the booking wizard", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_dontAskStudentToConfirmInstructorInformation,
		// Token: 0x04000BD3 RID: 3027
		[SettingData("Instruction message for select course step (appears underneath the course drop-list)", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		EXAMBOOKING_SelectCourseInstructionMessage = 230110,
		// Token: 0x04000BD4 RID: 3028
		[ReferenceSetting("Non-negotiable accommodations", "Rules", "The student will not be able to un-check the accommodations in this list (assuming they are approved for them) when booking a test.", Group.EXAMBOOKING, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL AND dsc.screennum=4 ORDER BY s.description,dc.controlcaption")]
		EXAMBOOKING_NonNegotiableAccommodationCids = 230121,
		// Token: 0x04000BD5 RID: 3029
		[SettingData("Restrict courses by campus", "Rules", "Blank means any campus is ok, otherwise provide a comma separated list of approved campuses", Group.EXAMBOOKING, SettingSemantic.TEXT)]
		EXAMBOOKING_RestrictCoursesToCampus,
		// Token: 0x04000BD6 RID: 3030
		[SettingData("Ask student for instructor phone", "Display", "The student will be able to enter or update the instructor phone number (as an optional field) in addition to the instructor name and email address", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_AskStudentForInstructorPhone,
		// Token: 0x04000BD7 RID: 3031
		[SettingData("Instructions for Choose Accommodations step", "Display", "This text appears in the choose accommodations step of the wizard, at the top of the page under the title.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Listed below are the accommodation(s) that have already been approved for you by your counsellor. Please check off the accommodation(s) that you feel are necessary for this exam.")]
		EXAMBOOKING_ChooseAccommodationsInstructions,
		// Token: 0x04000BD8 RID: 3032
		[SettingData("Note for Choose Accommodations step", "Display", "This text appears in the choose accommodations step of the wizard, right before the list of accommodations the student is able to check.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "* note: Only accommodations with a check will be used for your exam booking.")]
		EXAMBOOKING_ChooseAccommodationsNote,
		// Token: 0x04000BD9 RID: 3033
		[SettingData("Show the accommodations the student did not select, instead of the accommodations they did select, on the confirmation page at the end of the booking wizard", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation,
		// Token: 0x04000BDA RID: 3034
		[ReferenceSetting("Only allow the student to book a test if one of the selected fields has been filled out for the student.", "Rules", "The controls must be on a per student form.  For example, checking the disability fields will make sure the student has a disability selected before they are allowed to book online.", Group.EXAMBOOKING, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL AND dsc.screennum IN (SELECT screennum FROM screens WHERE typecode=0) ORDER BY s.description,dsc.ordernum", AllowMultipleSelections = true)]
		EXAMBOOKING_NoBookingIfNotAtLeastOneFieldFilledOut_cids,
		// Token: 0x04000BDB RID: 3035
		[SettingData("Allow the student to select the date and time", "Display", "If yes, the student will be prompted to select a date/time from the approved date/time list in step 5.  If there is only one available date/time it will automatically be selected for the student.  If false, the student will be shown a single date/time that has been selected for them.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		EXAMBOOKING_AllowStudentToSelectFromApprovedDateTimes = 230130,
		// Token: 0x04000BDC RID: 3036
		[SettingData("Ask student to enter alternate contact info for course", "Display", "The alternate course contact info is optional for the student to enter", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_AskStudentForCourseAlternateContactInfo,
		// Token: 0x04000BDD RID: 3037
		[SettingData("Important note for student when selecting an available exam date/time", "Display", "The default text is: Note: if none of the dates/times below are possible for you then you have the option of calling or visiting us in person to see if alternate arrangements can be made.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		EXAMBOOKING_AvailableTestDateTimesImportantNote,
		// Token: 0x04000BDE RID: 3038
		[SettingData("Cutoff time for students to cancel their exam bookings", "Rules", "", Group.EXAMBOOKING, SettingSemantic.CUTOFFTIME, IsHidden = true)]
		EXAMBOOKING_CutoffTimeForStudentsToCancelTheirExamBookings = 230135,
		// Token: 0x04000BDF RID: 3039
		[SettingData("Room not found message", "Display", "The message to the student when no room could be found.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "No available spaces could be found for you to write your exam.  Please click the 'Cancel' button at the bottom and contact us to see if alternate arrangements can be made.")]
		EXAMBOOKING_NoRoomFoundMessage = 230140,
		// Token: 0x04000BE0 RID: 3040
		[SettingData("Room found message", "Display", "The message to the student when a room was found.  This will not be used unless the 'Allow the student to select the date and time' setting is set to False.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "A spot was found for you to write your test; please click the 'Next' button below to continue scheduling your exam.")]
		EXAMBOOKING_RoomFoundMessage,
		// Token: 0x04000BE1 RID: 3041
		[SettingData("Ignore student's timetable for other courses when booking exams", "Rules", "Normally, exams will not be booked over top of other course timetables.  Setting this to 'True' will force the computer to ignore timetable times for other courses.  The result may be an exam that overlaps a class time for another course.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		EXAMBOOKING_IgnoreStudentTimetable,
		// Token: 0x04000BE2 RID: 3042
		[SettingData("Allow students to book multiple exams at once (requires lookup exam info to be available)", "Rules", "Instead of booking one exam at a time, the student can book multiple exams with one pass through the wizard.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_AllowStudentsToBookMultipleExams = 230145,
		// Token: 0x04000BE3 RID: 3043
		[ReferenceSetting("Pull exam lookup info from this report instead of from the exams table", "Rules", Group.EXAMBOOKING, SettingSemantic.REFERENCE_ARRAY, "searchinfo", "searchinfoid", "title", DefaultValue = new int[]
		{
			0
		}, AllowMultipleSelections = false)]
		EXAMBOOKING_ReportForLookingUpExamInfo,
		// Token: 0x04000BE4 RID: 3044
		[SettingData("Enable Final Exam Request System", "Final Exam Request System", "If true, the system switches to 'Final Exam Request' mode, instead of normal exam booking mode.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_FinalExamRequest_Enabled,
		// Token: 0x04000BE5 RID: 3045
		[SettingData("Final exam period begin", "Rules", "", Group.EXAMBOOKING, SettingSemantic.DATETIME)]
		EXAMBOOKING_FinalExamRequest_FinalsStartDate,
		// Token: 0x04000BE6 RID: 3046
		[SettingData("Final exam period end", "Rules", "", Group.EXAMBOOKING, SettingSemantic.DATETIME)]
		EXAMBOOKING_FinalExamRequest_FinalsEndDate,
		// Token: 0x04000BE7 RID: 3047
		[SettingData("Ignore student's schedule (other tests/exams/appointments in ClockWork) when booking exams", "Rules", "Normally exams will not be booked over top of a student's other appointments/tests/exams.  Setting this to 'True' will force the computer to ignore the student's schedule when finding a time for the student to write.  The result may be an exam that overlaps one of the student's other appointments.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_IgnoreStudentSchedule = 230160,
		// Token: 0x04000BE8 RID: 3048
		[SettingData("Ignore a student writing the same exam on the same day for the same course", "Rules", "Normally a student is only allowed to schedule a single exam for a single course on any given day.  Setting this to 'True' will skip the check for this.  The result may be a student booking two or more exams for the same course on the same day.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_IgnoreStudentTwoTestsSameCourseSameDay = 230162,
		// Token: 0x04000BE9 RID: 3049
		[SettingData("Maximum duration (in minutes).", "Rules", "The student will not be able to enter a class exam time with a duration longer than this (extra time is applied on top of the class exam duration).  Set to 0 to disable.", Group.EXAMBOOKING, SettingSemantic.INTEGER, DefaultValue = 0)]
		EXAMBOOKING_MaxDuration,
		// Token: 0x04000BEA RID: 3050
		[SettingData("Maximum duration - use timetable class length instead of maximum duration", "Rules", "Maximum duration setting will be used if timetable info is not present", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		EXAMBOOKING_MaxDurationUseTimetable,
		// Token: 0x04000BEB RID: 3051
		[SettingData("Message to students when they try to book a test the test booking module is not active", "Error messages", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Final exam booking is currently not available.")]
		EXAMBOOKING_ErrorMessage_ModuleInactive = 230170,
		// Token: 0x04000BEC RID: 3052
		[SettingData("Message to students when they try to book a test but have no active courses.", "Error messages", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "You do not have any active courses in our system at the current time.")]
		EXAMBOOKING_ErrorMessage_NoCourses,
		// Token: 0x04000BED RID: 3053
		[SettingData("Message to students when they have expired accommodations.", "Error messages", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Your accommodations are expired.")]
		EXAMBOOKING_ErrorMessage_AccommodationsExpired,
		// Token: 0x04000BEE RID: 3054
		[SettingData("Message to students when they have missing per student data fields.", "Error messages", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "One of the data checks has failed and you do not currently meet the necessary requirements.")]
		EXAMBOOKING_ErrorMessage_MissingPerStudentData,
		// Token: 0x04000BEF RID: 3055
		[SettingData("Message to students when they try to book a test but are not part of the pilot that is running.", "Error messages", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Test booking is currently running in a pilot.  Your name is not on the pilot list.")]
		EXAMBOOKING_ErrorMessage_Pilot,
		// Token: 0x04000BF0 RID: 3056
		[SettingData("Cutoff time for booking an exam (the student is only allowed to book an exam online before the date that this rule determines).", "Rules", "", Group.EXAMBOOKING, SettingSemantic.CUTOFFTIME, DefaultValue = "")]
		EXAMBOOKING_CutoffBookingDate = 230180,
		// Token: 0x04000BF1 RID: 3057
		[SettingData("Restrict courses by campus - enable restrict room by campus", "Rules", "If true, the campus of the course the student is booking an exam for will be matched to rooms, so that only rooms that match the campus will be included in the search.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom,
		// Token: 0x04000BF2 RID: 3058
		[SettingData("The notification email to the staff if the student has an accommodation that triggers the special accommodation email rule", "Emails", "This email will only be sent if a special accommodation rule is triggered.", Group.EXAMBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~coordinatoremail~#</from>\r\n     <to>#~coordinatoremail~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>Exam booking special accommodation notification</subject>\r\n     <attachments></attachments>\r\n     <body>&lt;table border='1' cellspacing='4' cellpadding='4'&gt;\r\n&lt;tr&gt; &lt;td&gt; Student: &lt;/td&gt; &lt;td&gt; #~name~# (#~student_no~#) &lt;/td&gt; &lt;/tr&gt;\r\n &lt;td&gt;Scheduled test: &lt;/td&gt; &lt;td&gt; #~startdate~# . #~starttime~# to #~endtime~# &lt;/td&gt; &lt;/tr&gt;\r\n &lt;tr&gt; &lt;td&gt; Accommodations: &lt;/td&gt; &lt;td&gt; #~accommodations~# &lt;/td&gt; &lt;/tr&gt;\r\n &lt;tr&gt; &lt;td&gt; Notice for: &lt;/td&gt; &lt;td&gt; #~list~# &lt;/td&gt; &lt;/tr&gt; &lt;/table&gt; </body>\r\n     <isactive>1</isactive>\r\n</email>")]
		EXAMBOOKING_SpecialAccommodationsEmailTemplate = 230185,
		// Token: 0x04000BF3 RID: 3059
		[SettingData("Course list time of day filter", "Rules", "Comma separated list of items that should indicate the course should be filtered out of the list the student chooses a course from when booking an exam.", Group.EXAMBOOKING, SettingSemantic.TEXT)]
		EXAMBOOKING_FilterCourseListByTimeOfDay,
		// Token: 0x04000BF4 RID: 3060
		[SettingData("Only allow the student to book their exam if the accommodation letter has been generated for the course.", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated = 230189,
		// Token: 0x04000BF5 RID: 3061
		[SettingData("Select the class exam date/time instruction message", "Display", "Intro message on the 'select class date/time step' - only show when a student is picking the date/time from freeform controls (not from list)", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Please specify when the exam is taking place.  Enter class exam duration in minutes.")]
		EXAMBOOKING_SelectClassDateTimeInstruction,
		// Token: 0x04000BF6 RID: 3062
		[SettingData("Custom wizard step title re-wording enabled", "Display", "If set to true ClockWork will re-word the wizard step titles to the corresponding setting values.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_CustomWizardStepRewording_Enabled,
		// Token: 0x04000BF7 RID: 3063
		[SettingData("Custom wizard step wording: Welcome", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Welcome")]
		EXAMBOOKING_CustomWizardStepRewording_StepWelcome,
		// Token: 0x04000BF8 RID: 3064
		[SettingData("Custom wizard step wording: Select course", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "1. Select course")]
		EXAMBOOKING_CustomWizardStepRewording_StepSelectCourse,
		// Token: 0x04000BF9 RID: 3065
		[SettingData("Custom wizard step wording: Indicate class date/time", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "2. Class exam date and time")]
		EXAMBOOKING_CustomWizardStepRewording_StepIndicateClassDateTime,
		// Token: 0x04000BFA RID: 3066
		[SettingData("Custom wizard step wording: Confirm instructor info", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "3. Confirm prof info")]
		EXAMBOOKING_CustomWizardStepRewording_StepConfirmInstructorInfo,
		// Token: 0x04000BFB RID: 3067
		[SettingData("Custom wizard step wording: Additional info", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "4. Additional requirements")]
		EXAMBOOKING_CustomWizardStepRewording_StepAdditionalInfo,
		// Token: 0x04000BFC RID: 3068
		[SettingData("Custom wizard step wording: Choose accommodations", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "4. Choose accommodations")]
		EXAMBOOKING_CustomWizardStepRewording_StepChooseAccommodations,
		// Token: 0x04000BFD RID: 3069
		[SettingData("Custom wizard step wording: Select scheduled time", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "5. Select your exam time")]
		EXAMBOOKING_CustomWizardStepRewording_StepSelectScheduledTime,
		// Token: 0x04000BFE RID: 3070
		[SettingData("Custom wizard step wording: Confirm and complete", "Display", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "6. Confirm and complete")]
		EXAMBOOKING_CustomWizardStepRewording_StepConfirmAndComplete,
		// Token: 0x04000BFF RID: 3071
		[SettingData("Custom allow student to book enabled", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_CustomAllowStudentToBookCheckSqlEnabled,
		// Token: 0x04000C00 RID: 3072
		[SettingData("Custom allow student to book exam check", "Rules", "Enter a Sql statement, @pid is the student's person id in ClockWork.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "SELECT 'You are not currently registered with us.  Please contact us for more information.' FROM people WHERE personid=@pid AND isactive=0")]
		EXAMBOOKING_CustomAllowStudentToBookCheckSql,
		// Token: 0x04000C01 RID: 3073
		[SettingData("Only allow the student to book their exam for courses that have an approved accommodation request (from online self-registration).", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest,
		// Token: 0x04000C02 RID: 3074
		[SettingData("Only allow the student to book their exam for courses where the instructor has confirmed receipt of the accommodation letter online.", "Rules", "Instructor confirms on the page where they view the accommodation letter, in 'Step 2'.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline,
		// Token: 0x04000C03 RID: 3075
		[SettingData("Exam registration date range - enable", "Rules", "", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_EnforceRegistrationDateRange,
		// Token: 0x04000C04 RID: 3076
		[SettingData("Exam registration begin date", "Rules", "", Group.EXAMBOOKING, SettingSemantic.DATETIME)]
		EXAMBOOKING_RegistrationStartDate,
		// Token: 0x04000C05 RID: 3077
		[SettingData("Exam registration end date", "Rules", "", Group.EXAMBOOKING, SettingSemantic.DATETIME)]
		EXAMBOOKING_RegistrationEndDate,
		// Token: 0x04000C06 RID: 3078
		[SettingData("Message to students when they try to book a final exam outside of the registration period.", "Error messages", "", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "Final exam booking is not available because the registration period is currently closed.")]
		EXAMBOOKING_ErrorMessage_NotInRegistrationDateRange,
		// Token: 0x04000C07 RID: 3079
		[SettingData("Class date and time entry wizard step intro message", "Display", "Appears at the top just under the wizard page title.  Leave blank to hide.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		EXAMBOOKING_ClassDateTimeIntro,
		// Token: 0x04000C08 RID: 3080
		[SettingData("Message to student when no existing class date/times are available to choose from.", "Display", "This is only relevant if 'Allow students to select an existing class/date time...' is enabled.", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "There are no dates and times that are available at this time.  Note that exams in the past are not part of the search.")]
		EXAMBOOKING_MessageWhenNoClassDatesAndTimesAreAvailableToChooseFrom,
		// Token: 0x04000C09 RID: 3081
		[SettingData("Special accommodations to ignore", "Rules", "A comma separated list of special accommodation types to ignore.  For example, if you list 100 for extra time, extra time will not be applied during the online booking process.  (100=Extra time,200=Breaks,300=Add Icon,400=Email Coordinator,500=Can't book online,600=Time of day,700=Max per day,800=Days rest,900=Start/end of day slide,1000=Snap time)", Group.EXAMBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		EXAMBOOKING_SpecialAccommodationsToIgnore,
		// Token: 0x04000C0A RID: 3082
		[SettingData("Extension on course end date for authorization for students", "Rules", "The number of days the end date of the course will be virtually extended when checking if the student is allowed to book an exam for this course.", Group.EXAMBOOKING, SettingSemantic.INTEGER, DefaultValue = 0)]
		EXAMBOOKING_CourseEndDateAuthorizationExtensionInDays,
		// Token: 0x04000C0B RID: 3083
		[SettingData("The notification email to the staff if the student changed the professor information", "Emails", "This email will only be sent if the student changes the professor info.", Group.EXAMBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~adminemail~#</to>\r\n    <from></from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>ClockWork: Student entered a different instructor name and/or email for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\nA student booked a test and submitted a new instructor name and/or email:\r\n\r\nCurrent instructor name and email: #~instructorname~# #~instructoremail~#\r\nStudent entered instructor name and email: #~newinstructorname~# #~newinstructoremail~#\r\n\r\nPlease verify this information and enter the correct instructor name and email into ClockWork.  \r\n    </body>\r\n </email>")]
		EXAMBOOKING_StudentChangeProfInfoEmailTemplate,
		// Token: 0x04000C0C RID: 3084
		[SettingData("Hide the 'Check all'/'Check none' links when choosing which accommodations for the test/exam", "Display", "Setting this to true will result in the student not having access to the 'check all' or 'check none' links, and will mean they will have to check each individual accommodation they require for their test.", Group.EXAMBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		EXAMBOOKING_HideCheckAllCheckNone = 230214,
		// Token: 0x04000C0D RID: 3085
		[SettingData("Room availability mapping", "_Main settings", "Format: Room with availability personid:comma separated list of room personids;...  Example: 32:3,5,19;45:2,91,2  If this setting is set it will override the 'Room to use for all availability' setting.", Group.EXAMBOOKING, SettingSemantic.TEXT)]
		EXAMBOOKING_RoomAvailabilityMappings,
		// Token: 0x04000C0E RID: 3086
		[SettingData("Which template to use for student booking confirmation email", "Emails", "Gets automatically sent to the student each time they book an exam.  This setting will determine which template should be used based on the campus for the course the student is booking an exam for.  Codes available: [classstartdate,classenddate,classstarttime,classendtime,classduration,startdate,enddate,classstarttime,classendtime,duration,room,email,firstname,lastname,student_no,name,accommodations,course,personid,appointmentid,instructor,instructoremail]", Group.EXAMBOOKING, SettingSemantic.CAMPUSES_WITH_EMAILTEMPLATEIDS)]
		EXAMBOOKING_Email_StudentBookingConfirmation_TemplateRules,
		// Token: 0x04000C0F RID: 3087
		[Obsolete("Use TESTBOOKING_CutoffBookingDate instead")]
		[SettingData("Minimum number of days ahead of the class that a student can book a test.", "Rules", "", Group.TESTBOOKING, SettingSemantic.INTEGER, DefaultValue = 7, IsHidden = true)]
		TESTBOOKING_WizardSetting_MinDaysAheadToBook = 30017,
		// Token: 0x04000C10 RID: 3088
		[SettingData("Confirm booking finish button text", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT)]
		TESTBOOKING_WizardSetting_ConfirmBookingFinishButtonText,
		// Token: 0x04000C11 RID: 3089
		[SettingData("Confirm booking finish message", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT)]
		TESTBOOKING_WizardSetting_ConfirmBookingMsg,
		// Token: 0x04000C12 RID: 3090
		[SettingData("Accommodations checked by default", "Rules", "If true, all accommodations will be checked by default when a student is booking a test (they will have to un-check ones they don't need for the test).  If false, all accommodations will be un-checked and the student will have to check the ones they need.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_WizardSetting_AccommodationsDefaultChecked,
		// Token: 0x04000C13 RID: 3091
		[SettingData("Hide the continuing education drop list", "Display", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		TESTBOOKING_WizardSetting_HideContinuingEducationDropList,
		// Token: 0x04000C14 RID: 3092
		[SettingData("Welcome message for the test booking wizard (step 1)", "Display", "", Group.TESTBOOKING, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Online Test Booking</h1>\r\n<p>Welcome to the Online Test Booking wizard.  This wizard will guide you through the process of scheduling your test with us.  You may abort this process at any time by clicking the 'Cancel' button at the bottom of each page.</p>\r\n<p>Please be aware that your instructor will receive an email notification with the details of your test booking.</p>\r\n<p>You will need to have the following information handy in order to successfully schedule your test:</p>\r\n<ol>\r\n    <li>The name of the course you want to schedule a test for, and the instructor's name and email address</li>\r\n    <li>The date, start time and duration of the test the class will be writing</li>\r\n    <li>You must be scheduling your test a minimum of seven (7) days before the class is writing</li>\r\n</ol>\r\n<p>Click the 'Next' button below to get started.</p>")]
		TESTBOOKING_WizardSetting_WelcomeMsg,
		// Token: 0x04000C15 RID: 3093
		[SettingData("Booking completed message", "Display", "This message is displayed to the student after they have booked their test.", Group.TESTBOOKING, SettingSemantic.HTML, DefaultValue = "<h1>Thank you for your submission.</h1>")]
		TESTBOOKING_WizardSetting_FinishedBookingMsg,
		// Token: 0x04000C16 RID: 3094
		[SettingData("Testbooking wizard setting test types enabled", "Display", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true, IsHidden = true)]
		TESTBOOKING_WizardSetting_TestTypesEnabled,
		// Token: 0x04000C17 RID: 3095
		[SettingData("Confirmation page - I agree text", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT)]
		TESTBOOKING_WizardSetting_ConfirmationPage_IAgreeText,
		// Token: 0x04000C18 RID: 3096
		[SettingData("Confirmation page - Intro text", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT)]
		TESTBOOKING_WizardSetting_ConfirmationPage_IntroText,
		// Token: 0x04000C19 RID: 3097
		[ReferenceSetting("Additional information form", "Display", "", Group.TESTBOOKING, SettingSemantic.REFERENCE_ARRAY, "screens", "screennum", "description")]
		TESTBOOKING_WizardSetting_AdditionalInformationScreenNum,
		// Token: 0x04000C1A RID: 3098
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.TESTBOOKING, SettingSemantic.DATETIME, IsHidden = true)]
		TESTBOOKING_LastModifiedTime,
		// Token: 0x04000C1B RID: 3099
		[SettingData("Assets", "_Main settings", "", Group.TESTBOOKING, SettingSemantic.ASSETS, DefaultValue = "<assets>\r\n  <asset>\r\n    <title>Computer</title>\r\n    <id>COMPUTER</id>\r\n    <description>\r\n    </description>\r\n    <score>100</score>\r\n    <accommodations>\r\n    </accommodations>\r\n    <isactive>1</isactive>\r\n  </asset>\r\n</assets>")]
		TESTBOOKING_Assets,
		// Token: 0x04000C1C RID: 3100
		[SettingData("Rooms", "_Main settings", "", Group.TESTBOOKING, SettingSemantic.ROOMS, DefaultValue = "<rooms>\r\n\t<room pid=\"0\" title=\"fake room\" type=\"RegularRoom\">\r\n\t\t<assets>\r\n\t\t\t<asset id=\"COMPUTER\" />\r\n\t\t</assets>\r\n\t</room>            \r\n</rooms>")]
		TESTBOOKING_Rooms,
		// Token: 0x04000C1D RID: 3101
		[SettingData("Special Accommodations", "_Main settings", "", Group.TESTBOOKING, SettingSemantic.SPECIALACCOMMODATIONS, DefaultValue = "<specialaccommodations>\r\n</specialaccommodations>")]
		TESTBOOKING_SpecialAccommodations,
		// Token: 0x04000C1E RID: 3102
		[SettingData("Student booking confirmation email", "Emails", "Gets automatically sent to the student each time they book a test. Codes available: [classstartdate,classenddate,classstarttime,classendtime,classduration,startdate,enddate,classstarttime,classendtime,duration,room,email,firstname,lastname,student_no,name,accommodations,course,personid,appointmentid,instructor,instructoremail]", Group.TESTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~email~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Confirmation of your test booking request for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello #~firstname~#,\r\n\r\nYou have successfully completed your test booking request for #~course~#:\r\n\r\n#~startdate~# . #~starttime~# to #~endtime~#\r\n\r\nPlease contact us if you have any questions, or need to cancel or reschedule your test.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		TESTBOOKING_Email_StudentBookingConfirmation = 30050,
		// Token: 0x04000C1F RID: 3103
		[ReferenceSetting("Appointment type to use for bookings", "Rules", "", Group.TESTBOOKING, SettingSemantic.REFERENCE_ARRAY, "appointmenttypes", "apptypeid", "description", OverrideSql = "SELECT at.apptypeid,coalesce(atg.title,'Ungrouped') + ': ' + at.description AS description FROM appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid WHERE at.isactive=1 ORDER BY atg.title,at.description")]
		TESTBOOKING_AppointmentTypeToUseForBooking = 30053,
		// Token: 0x04000C20 RID: 3104
		[SettingData("Are tests to be booked as tentative?", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_BookTestsAsTentative,
		// Token: 0x04000C21 RID: 3105
		[SettingData("Is the student allowed to select a date and time for the test that hasn't already been entered into the system?", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		TESTBOOKING_StudentAllowedToSelectOwnDateTime,
		// Token: 0x04000C22 RID: 3106
		[SettingData("Welcome message for the information (home) page", "Display", "", Group.TESTBOOKING, SettingSemantic.HTML, DefaultValue = "<h1 class='PageTitle'>Student Test-Booking and Accommodations</h1>\r\n<p>Welcome to the Student Test-Booking and Accommodations website.  You can use this website to:</p>\r\n<ul>\r\n    <li>Schedule a test, mid-term or quiz</li>\r\n    <li>Schedule a final exam</li>\r\n    <li>Check your upcoming scheduled appointments, tests and exams</li>\r\n    <li>View your accommodations that have been assigned by your advisor</li>\r\n    <li>Print out a pdf copy of your accommodations letter</li>\r\n</ul>\r\n<p>\r\n    Please click the <a href='book.aspx'>Schedule a test, mid-term or quiz</a> link in the menu in order to schedule a test, or choose the menu option that you would like to use.  You will be asked to login using your school login account.\r\n</p>")]
		TESTBOOKING_Info = 30060,
		// Token: 0x04000C23 RID: 3107
		[SettingData("Students allowed to book tests", "_Main settings", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		TESTBOOKING_StudentsAllowedToBookTests = 30065,
		// Token: 0x04000C24 RID: 3108
		[ReferenceSetting("Room to use for all room availability", "_Main settings", "", Group.TESTBOOKING, SettingSemantic.REFERENCE_ARRAY, "people", "apptypeid", "firstname", IsValueEncrypted = true, OverrideSql = "SELECT p.personid,p.firstname FROM people p WHERE p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)")]
		TESTBOOKING_OverrideRoomPidForAvailability = 30070,
		// Token: 0x04000C25 RID: 3109
		[SettingData("Select a date time message to students (override)", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Please select a date and time from the list of available dates and times below.  If none of the date/times in the list below will work for you then please contact us to see if alternate arrangements can be made.  We can be reached at (ask for assistance with test booking).")]
		TESTBOOKING_SelectADateTimeMessageToStudents,
		// Token: 0x04000C26 RID: 3110
		[SettingData("Is the student allowed to select from a list of dates/times for previously submitted tests?", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_StudentAllowedToSelectPreviousDateTimes,
		// Token: 0x04000C27 RID: 3111
		[SettingData("Institution Name", "_Main settings", "", Group.TESTBOOKING, SettingSemantic.TEXT, IsHidden = true)]
		TESTBOOKING_InstitutionName,
		// Token: 0x04000C28 RID: 3112
		[SettingData("Is the student allowed to select from a list of dates/times class test definitions?", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions = 30075,
		// Token: 0x04000C29 RID: 3113
		[SettingData("Is the student allowed to select from a list of dates/times class test definitions with typecode='F'?", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar,
		// Token: 0x04000C2A RID: 3114
		[SettingData("Instructor email when student books a test", "Emails", "Gets automatically sent to the instructor each time a student books a test.  Note that the instructor may receive several emails for the same course if they have multiple students registered with Disability Services. Codes available: [classstartdate,classenddate,classstarttime,classendtime,classduration,startdate,enddate,classstarttime,classendtime,duration,room,email,firstname,lastname,student_no,name,accommodations,course,personid,appointmentid,instructor,instructoremail]", Group.TESTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = " <email>\r\n    <to>#~instructoremail~#</to>\r\n    <from>#~from~#</from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>Confirmation of test booking request for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>0</isactive>\r\n    <body>Hello #~instructorname~#,\r\n\r\nA student has just submitted a test booking request for #~course~#:\r\n\r\n#~firstname~# #~lastname~# . #~student_no~#\r\n#~startdate~# . #~starttime~# to #~endtime~#\r\n\r\nPlease contact us for more information if you have any questions, or need to cancel or reschedule your test.\r\n#~signature~#\r\n    </body>\r\n </email>")]
		TESTBOOKING_Email_StudentBookingConfirmationForInstructor = 30085,
		// Token: 0x04000C2B RID: 3115
		[SettingData("Url to send the student to when they click 'cancel' while booking a test", "Behaviour", "The default is to send the student back to the main test booking page (default.aspx)", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "default.aspx")]
		TESTBOOKING_TestBookingCancelUrl,
		// Token: 0x04000C2C RID: 3116
		[SettingData("Test booking coordinator email", "Emails", "Used for communication to the test booking coordinator from the ClockWork software.  (if a student modifies or enters a new instructor name or email, etc.)", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		TESTBOOKING_TestBookingCoordinatorEmail,
		// Token: 0x04000C2D RID: 3117
		[SettingData("Contact information", "Display", "Contact information for the department that will be displayed to the student.  This will be displayed to the student if a problem is encountered during the booking process.  It can also be used in emails by using the mail merge code 'testcontactinfo'.", Group.TESTBOOKING, SettingSemantic.HTML)]
		TESTBOOKING_DepartmentContactInformation,
		// Token: 0x04000C2E RID: 3118
		[SettingData("Rules", "_Main settings", "", Group.TESTBOOKING, SettingSemantic.TESTRULES, DefaultValue = "<rules>\r\n\t<rule ordernum=\"1\" title=\"All non-virtual rooms\"\r\n        active=\"1\"\r\n        roomstoexclude=\"\"\r\n        includenonvirtualrooms=\"1\" includevirtualrooms=\"0\"\r\n        allowedminutesbefore=\"0\" allowedminutesafter=\"0\"\r\n        stoplookingiffoundatleastone=\"1\" \r\n        shifttimetomatchendofday=\"1\" enforceoverlapwithclasstime=\"0\">\r\n    </rule>\r\n    <rule ordernum=\"2\" title=\"Virtual rooms only\" \r\n        active=\"1\"\r\n        roomstoexclude=\"\"\r\n        includenonvirtualrooms=\"0\" includevirtualrooms=\"1\" \r\n        allowedminutesbefore=\"0\" allowedminutesafter=\"0\"\r\n        stoplookingiffoundatleastone=\"1\" \r\n        shifttimetomatchendofday=\"1\" enforceoverlapwithclasstime=\"0\">\r\n\t</rule>           \r\n</rules>")]
		TESTBOOKING_Rules = 30090,
		// Token: 0x04000C2F RID: 3119
		[SettingData("code_FindPotentialBookingsStart", "Custom Rules", "Custom c# code for finding potential bookings", Group.TESTBOOKING, SettingSemantic.CSHARPCODE)]
		TESTBOOKING_code_FindPotentialBookingsStart,
		// Token: 0x04000C30 RID: 3120
		[SettingData("code_FindPotentialBookingsMid", "Custom Rules", "Custom c# code for finding potential bookings", Group.TESTBOOKING, SettingSemantic.CSHARPCODE)]
		TESTBOOKING_code_FindPotentialBookingsMid,
		// Token: 0x04000C31 RID: 3121
		[SettingData("code_FindPotentialBookingsEnd", "Custom Rules", "Custom c# code for finding potential bookings", Group.TESTBOOKING, SettingSemantic.CSHARPCODE)]
		TESTBOOKING_code_FindPotentialBookingsEnd,
		// Token: 0x04000C32 RID: 3122
		[SettingData("code_FindPotentialBookingsMisc", "Custom Rules", "Custom c# code for finding potential bookings.  Function definition(s) should be included, this code will be part of the empty area of the class.", Group.TESTBOOKING, SettingSemantic.CSHARPCODE)]
		TESTBOOKING_code_FindPotentialBookingsMisc,
		// Token: 0x04000C33 RID: 3123
		[SettingData("Dont ask student to confirm instructor information", "Display", "This will remove step 3 from the booking wizard", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_dontAskStudentToConfirmInstructorInformation,
		// Token: 0x04000C34 RID: 3124
		[SettingData("Accommodations expiry date control", "_Main settings", "The dynamic control that indicates the date of expiry for all of the student's accommodations.  They will not be allowed to book their test if their accommodations are expired", Group.TESTBOOKING, SettingSemantic.CONTROLID_PERSTUDENT)]
		TESTBOOKING_AccommodationsExpiryDateCid = 30098,
		// Token: 0x04000C35 RID: 3125
		[SettingData("Treat empty expiry date as expired", "_Main settings", "If the advisor has not set the expiry date for a student (ie. the expiry date field is empty instead of containing a date), then 'True' means this should be treated as expired, 'False' means it will be treated as meaning not expired. (note: this setting only takes effect if the 'Accommodations expiry date control' setting is set)", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired,
		// Token: 0x04000C36 RID: 3126
		[SettingData("Instruction message for select course step (appears underneath the course drop-list)", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		TESTBOOKING_SelectCourseInstructionMessage = 30110,
		// Token: 0x04000C37 RID: 3127
		[ReferenceSetting("Non-negotiable accommodations", "Rules", "The student will not be able to un-check the accommodations in this list (assuming they are approved for them) when booking a test.", Group.TESTBOOKING, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL AND dsc.screennum=4 ORDER BY s.description,dc.controlcaption")]
		TESTBOOKING_NonNegotiableAccommodationCids = 30121,
		// Token: 0x04000C38 RID: 3128
		[SettingData("Restrict courses by campus", "Rules", "Blank means any campus is ok, otherwise provide a comma separated list of approved campuses", Group.TESTBOOKING, SettingSemantic.TEXT)]
		TESTBOOKING_RestrictCoursesToCampus,
		// Token: 0x04000C39 RID: 3129
		[SettingData("Ask student for instructor phone", "Display", "The student will be able to enter or update the instructor phone number (as an optional field) in addition to the instructor name and email address", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_AskStudentForInstructorPhone,
		// Token: 0x04000C3A RID: 3130
		[SettingData("Instructions for Choose Accommodations step", "Display", "This text appears in the choose accommodations step of the wizard, at the top of the page under the title.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Listed below are the accommodation(s) that have already been approved for you by your counsellor. Please check off the accommodation(s) that you feel are necessary for this test.")]
		TESTBOOKING_ChooseAccommodationsInstructions,
		// Token: 0x04000C3B RID: 3131
		[SettingData("Note for Choose Accommodations step", "Display", "This text appears in the choose accommodations step of the wizard, right before the list of accommodations the student is able to check.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "* note: Only accommodations with a check will be used for your test booking.")]
		TESTBOOKING_ChooseAccommodationsNote,
		// Token: 0x04000C3C RID: 3132
		[SettingData("Show the accommodations the student did not select, instead of the accommodations they did select, on the confirmation page at the end of the booking wizard", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation,
		// Token: 0x04000C3D RID: 3133
		[ReferenceSetting("Only allow the student to book a test if one of the selected fields has been filled out for the student.", "Rules", "The control(s) must be on a per student form.  For example, checking the disability fields will make sure the student has a disability selected before they are allowed to book online.", Group.TESTBOOKING, SettingSemantic.REFERENCE_ARRAY, "dynamiccontrols", "controlid", "caption", OverrideSql = "SELECT dc.controlid,s.description + ': ' + dc.controlcaption AS caption FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum WHERE NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) AND NOT s.description IS NULL AND NOT dc.controlcaption IS NULL AND dsc.screennum IN (SELECT screennum FROM screens WHERE typecode=0) ORDER BY s.description,dsc.ordernum", AllowMultipleSelections = true)]
		TESTBOOKING_NoBookingIfNotAtLeastOneFieldFilledOut_cids,
		// Token: 0x04000C3E RID: 3134
		[SettingData("Allow the student to select the date and time", "Display", "If yes, the student will be prompted to select a date/time from the approved date/time list in step 6.  If there is only one available date/time it will automatically be selected for the student.  If false, the student will be shown a single date/time that has been selected for them.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		TESTBOOKING_AllowStudentToSelectFromApprovedDateTimes = 30130,
		// Token: 0x04000C3F RID: 3135
		[SettingData("Ask student to enter alternate contact info for course", "Display", "The alternate course contact info is optional for the student to enter", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_AskStudentForCourseAlternateContactInfo,
		// Token: 0x04000C40 RID: 3136
		[SettingData("Important note for student when selecting an available test date/time", "Display", "The default text is: Note: if none of the dates/times below are possible for you then you have the option of calling or visiting us in person to see if alternate arrangements can be made.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		TESTBOOKING_AvailableTestDateTimesImportantNote,
		// Token: 0x04000C41 RID: 3137
		[SettingData("Message to students when they try to book a test but are not in ClockWork (ie. not registered", "Error messages", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "You are not registered with us yet.  To register with us please contact us (see contact info below).")]
		TESTBOOKING_ErrorMessage_NotRegistered,
		// Token: 0x04000C42 RID: 3138
		[ReferenceSetting("List of students that are allowed to use the test booking online system.", "Rules", "If anything is checked here, only those students who are checked will be able to use the online system.  All other students will be sent to a page explaining that the site is currently not available.", Group.TESTBOOKING, SettingSemantic.REFERENCE_ARRAY, "people", "personid", "student_no", IsValueEncrypted = true, OverrideSql = "SELECT p.personid,p.student_no FROM people p WHERE p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)", OverrideSortByDisplayName = true)]
		TESTBOOKING_RestrictLoginTo,
		// Token: 0x04000C43 RID: 3139
		[SettingData("Cutoff time for students to cancel their test bookings", "Rules", "", Group.TESTBOOKING, SettingSemantic.CUTOFFTIME)]
		TESTBOOKING_CutoffTimeForStudentsToCancelTheirTestBookings,
		// Token: 0x04000C44 RID: 3140
		[SettingData("Make students confirm tentative test bookings", "Test Confirm", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_MakeStudentsConfirmTentativeTests,
		// Token: 0x04000C45 RID: 3141
		[SettingData("Student test confirmation start time", "Test Confirm", "If students are confirming tentative test bookings, this is the time they can start confirming their tests.", Group.TESTBOOKING, SettingSemantic.CUTOFFTIME)]
		TESTBOOKING_ConfirmTestsStart,
		// Token: 0x04000C46 RID: 3142
		[SettingData("Student test confirmation end time", "Test Confirm", "If students are confirming tentative test bookings, this is the last time they can confirm their tests.", Group.TESTBOOKING, SettingSemantic.CUTOFFTIME)]
		TESTBOOKING_ConfirmTestsEnd,
		// Token: 0x04000C47 RID: 3143
		[SettingData("Show the test location on the 'My upcoming events' listing", "Display", "Setting to disabled will not show the location at all.", Group.TESTBOOKING, SettingSemantic.CUTOFFTIME)]
		TESTBOOKING_ShowTestLocationOnMyUpcomingEvents_StartShowingDate,
		// Token: 0x04000C48 RID: 3144
		[SettingData("Room not found message", "Display", "The message to the student when no room could be found.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "No available spaces could be found for you to write your test.  Please click the 'Cancel' button at the bottom and contact us to see if alternate arrangements can be made.")]
		TESTBOOKING_NoRoomFoundMessage,
		// Token: 0x04000C49 RID: 3145
		[SettingData("Room found message", "Display", "The message to the student when a room was found.  This will not be used unless the 'Allow the student to select the date and time' setting is set to False.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "A spot was found for you to write your test; please click the 'Next' button below to continue scheduling your test.")]
		TESTBOOKING_RoomFoundMessage,
		// Token: 0x04000C4A RID: 3146
		[SettingData("Show the class date time only in the 'My upcoming appointments' list for tests and exams", "Display", "The actual scheduled time will be hidden, if this setting is enabled and the cutoff time has not been reached yet.  After the cutoff time the scheduled date and time will be shown.", Group.TESTBOOKING, SettingSemantic.CUTOFFTIME)]
		TESTBOOKING_ShowClassDateTimeInsteadOfScheduledDateTimeInMyUpcomingApptsCutoff,
		// Token: 0x04000C4B RID: 3147
		[SettingData("Ignore student's timetable for other courses when booking tests", "Rules", "Normally, tests will not be booked over top of other course timetables.  Setting this to 'True' will force the computer to ignore timetable times for other courses.  The result may be a test that overlaps a class time for another course.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		TESTBOOKING_IgnoreStudentTimetable,
		// Token: 0x04000C4C RID: 3148
		[ReferenceSetting("Show the test location: location display format", "Display", "If the test location is showing in the 'My upcoming events' listing, how should it appear? The default is seat only.", Group.TESTBOOKING, SettingSemantic.REFERENCE_ARRAY, "", "displaycode", "displaytype", OverrideSql = "SELECT 1 AS displaycode,'Assigned seat' AS displaytype UNION SELECT 2 AS displaycode,'Alternate location' AS displaytype UNION SELECT 4 AS displaycode,'First word of assigned seat' AS displaytype")]
		TESTBOOKING_ShowTestLocationFormat,
		// Token: 0x04000C4D RID: 3149
		[SettingData("Ignore student's schedule (other tests/exams/appointments in ClockWork) when booking tests", "Rules", "Normally tests will not be booked over top of a student's other appointments/tests/exams.  Setting this to 'True' will force the computer to ignore the student's schedule when finding a time for the student to write.  The result may be a test that overlaps one of the student's other appointments.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_IgnoreStudentSchedule = 30160,
		// Token: 0x04000C4E RID: 3150
		[SettingData("Ignore a student writing the same test on the same day for the same course", "Rules", "Normally a student is only allowed to schedule a single test for a single course on any given day.  Setting this to 'True' will skip the check for this.  The result may be a student booking two or more tests for the same course on the same day.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_IgnoreStudentTwoTestsSameCourseSameDay = 30162,
		// Token: 0x04000C4F RID: 3151
		[SettingData("Maximum duration (in minutes)", "Rules", "The student will not be able to enter a class test time with a duration longer than this (extra time is applied on top of the class test duration).  Set to 0 to disable", Group.TESTBOOKING, SettingSemantic.INTEGER, DefaultValue = 0)]
		TESTBOOKING_MaxDuration,
		// Token: 0x04000C50 RID: 3152
		[SettingData("Maximum duration - use timetable class length instead of maximum duration", "Rules", "Maximum duration setting will be used if timetable info is not present", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		TESTBOOKING_MaxDurationUseTimetable,
		// Token: 0x04000C51 RID: 3153
		[SettingData("Message to students when they try to book a test the test booking module is not active", "Error messages", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Test booking is currently not available.")]
		TESTBOOKING_ErrorMessage_ModuleInactive = 30170,
		// Token: 0x04000C52 RID: 3154
		[SettingData("Message to students when they try to book a test but have no active courses.", "Error messages", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "You do not have any active courses in our system at the current time.")]
		TESTBOOKING_ErrorMessage_NoCourses,
		// Token: 0x04000C53 RID: 3155
		[SettingData("Message to students when they have expired accommodations.", "Error messages", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Your accommodations are expired.")]
		TESTBOOKING_ErrorMessage_AccommodationsExpired,
		// Token: 0x04000C54 RID: 3156
		[SettingData("Message to students when they have missing per student data fields.", "Error messages", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "One of the data checks has failed and you do not currently meet the necessary requirements.")]
		TESTBOOKING_ErrorMessage_MissingPerStudentData,
		// Token: 0x04000C55 RID: 3157
		[SettingData("Message to students when they try to book a test but are not part of the pilot that is running.", "Error messages", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Test booking is currently running in a pilot.  Your name is not on the pilot list.")]
		TESTBOOKING_ErrorMessage_Pilot,
		// Token: 0x04000C56 RID: 3158
		[SettingData("Cutoff time for booking a test (the student is only allowed to book a test online before the date that this rule determines).", "Rules", "", Group.TESTBOOKING, SettingSemantic.CUTOFFTIME, DefaultValue = "")]
		TESTBOOKING_CutoffBookingDate = 30180,
		// Token: 0x04000C57 RID: 3159
		[SettingData("Restrict courses by campus - enable restrict room by campus", "Rules", "If true, the campus of the course the student is booking a test for will be matched to rooms, so that only rooms that match the campus will be included in the search.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom,
		// Token: 0x04000C58 RID: 3160
		[SettingData("The notification email to the staff if the student has an accommodation that triggers the special accommodation email rule", "Emails", "This email will only be sent if a special accommodation rule is triggered.", Group.TESTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from> #~coordinatoremail~# </from>\r\n     <to> #~coordinatoremail~# </to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>Test booking special accommodation notification</subject>\r\n     <attachments></attachments>\r\n     <body>&lt;table border='1' cellspacing='4' cellpadding='4'&gt;\r\n&lt;tr&gt;&lt;td&gt;Student:&lt;/td&gt;&lt;td&gt; #~name~# (#~student_no~#)&lt;/td&gt;&lt;/tr&gt;\r\n&lt;td&gt;Scheduled test:&lt;/td&gt;&lt;td&gt; #~startdate~# . #~starttime~# to #~endtime~# &lt;/td&gt;&lt;/tr&gt;\r\n&lt;tr&gt;&lt;td&gt;Accommodations:&lt;/td&gt;&lt;td&gt; #~accommodations~# &lt;/td&gt;&lt;/tr&gt;\r\n&lt;tr&gt;&lt;td&gt;Notice for:&lt;/td&gt;&lt;td&gt; #~list~# &lt;/td&gt;&lt;/tr&gt;&lt;/table&gt;</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		TESTBOOKING_SpecialAccommodationsEmailTemplate = 30185,
		// Token: 0x04000C59 RID: 3161
		[SettingData("Course list time of day filter", "Rules", "Comma separated list of items that should indicate the course should be filtered out of the list the student chooses a course from when booking a test.", Group.TESTBOOKING, SettingSemantic.TEXT)]
		TESTBOOKING_FilterCourseListByTimeOfDay,
		// Token: 0x04000C5A RID: 3162
		[SettingData("Buffer minutes pre", "Rules", "Number of minutes that should be kept unbooked before each test booking.", Group.TESTBOOKING, SettingSemantic.INTEGER, DefaultValue = 0)]
		TESTBOOKING_BufferMinutesPre,
		// Token: 0x04000C5B RID: 3163
		[SettingData("Buffer minutes post", "Rules", "Number of minutes that should be kept unbooked after each test booking.", Group.TESTBOOKING, SettingSemantic.INTEGER, DefaultValue = 0)]
		TESTBOOKING_BufferMinutesPost,
		// Token: 0x04000C5C RID: 3164
		[SettingData("Only allow the student to book their test if the accommodation letter has been generated for the course.", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated,
		// Token: 0x04000C5D RID: 3165
		[SettingData("Select the class test date/time instruction message", "Display", "Intro message on the 'select class date/time step' - only show when a student is picking the date/time from freeform controls (not from list)", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Please specify when the test is taking place.  Enter class test duration in minutes.")]
		TESTBOOKING_SelectClassDateTimeInstruction,
		// Token: 0x04000C5E RID: 3166
		[SettingData("Custom wizard step title re-wording enabled", "Display", "If set to true ClockWork will re-word the wizard step titles to the corresponding setting values.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_CustomWizardStepRewording_Enabled,
		// Token: 0x04000C5F RID: 3167
		[SettingData("Custom wizard step wording: Welcome", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Welcome")]
		TESTBOOKING_CustomWizardStepRewording_StepWelcome,
		// Token: 0x04000C60 RID: 3168
		[SettingData("Custom wizard step wording: Select course", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "1. Select course")]
		TESTBOOKING_CustomWizardStepRewording_StepSelectCourse,
		// Token: 0x04000C61 RID: 3169
		[SettingData("Custom wizard step wording: Indicate class date/time", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "2. Class test date and time")]
		TESTBOOKING_CustomWizardStepRewording_StepIndicateClassDateTime,
		// Token: 0x04000C62 RID: 3170
		[SettingData("Custom wizard step wording: Confirm instructor info", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "3. Confirm prof info")]
		TESTBOOKING_CustomWizardStepRewording_StepConfirmInstructorInfo,
		// Token: 0x04000C63 RID: 3171
		[SettingData("Custom wizard step wording: Additional info", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "4. Additional requirements")]
		TESTBOOKING_CustomWizardStepRewording_StepAdditionalInfo,
		// Token: 0x04000C64 RID: 3172
		[SettingData("Custom wizard step wording: Choose accommodations", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "4. Choose accommodations")]
		TESTBOOKING_CustomWizardStepRewording_StepChooseAccommodations,
		// Token: 0x04000C65 RID: 3173
		[SettingData("Custom wizard step wording: Select scheduled time", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "5. Select your test time")]
		TESTBOOKING_CustomWizardStepRewording_StepSelectScheduledTime,
		// Token: 0x04000C66 RID: 3174
		[SettingData("Custom wizard step wording: Confirm and complete", "Display", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "6. Confirm and complete")]
		TESTBOOKING_CustomWizardStepRewording_StepConfirmAndComplete,
		// Token: 0x04000C67 RID: 3175
		[SettingData("Custom allow student to book enabled", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_CustomAllowStudentToBookCheckSqlEnabled,
		// Token: 0x04000C68 RID: 3176
		[SettingData("Custom allow student to book test check", "Rules", "Enter a Sql statement, @pid is the student's person id in ClockWork.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "SELECT 'You are not currently registered with us.  Please contact us for more information.' FROM people WHERE personid=@pid AND isactive=0")]
		TESTBOOKING_CustomAllowStudentToBookCheckSql,
		// Token: 0x04000C69 RID: 3177
		[SettingData("Only allow the student to book their test for courses that have an approved accommodation request (from online self-registration).", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest,
		// Token: 0x04000C6A RID: 3178
		[SettingData("Only allow the student to book their test for courses where the instructor has confirmed receipt of the accommodation letter online.", "Rules", "Instructor confirms on the page where they view the accommodation letter, in 'Step 2'.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline,
		// Token: 0x04000C6B RID: 3179
		[SettingData("Exam registration date range - enable", "Rules", "", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_EnforceRegistrationDateRange,
		// Token: 0x04000C6C RID: 3180
		[SettingData("Exam registration begin date", "Rules", "", Group.TESTBOOKING, SettingSemantic.DATETIME)]
		TESTBOOKING_RegistrationStartDate,
		// Token: 0x04000C6D RID: 3181
		[SettingData("Exam registration end date", "Rules", "", Group.TESTBOOKING, SettingSemantic.DATETIME)]
		TESTBOOKING_RegistrationEndDate,
		// Token: 0x04000C6E RID: 3182
		[SettingData("Message to students when they try to book a test outside of the registration period.", "Error messages", "", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "Test booking is not available because the registration period is currently closed.")]
		TESTBOOKING_ErrorMessage_NotInRegistrationDateRange,
		// Token: 0x04000C6F RID: 3183
		[SettingData("Class date and time entry wizard step intro message", "Display", "Appears at the top just under the wizard page title.  Leave blank to hide.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		TESTBOOKING_ClassDateTimeIntro,
		// Token: 0x04000C70 RID: 3184
		[SettingData("Message to student when no existing class date/times are available to choose from.", "Display", "This is only relevant if 'Allow students to select an existing class/date time...' is enabled.", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "There are no dates and times that are available at this time.  Note that exams in the past are not part of the search.")]
		TESTBOOKING_MessageWhenNoClassDatesAndTimesAreAvailableToChooseFrom,
		// Token: 0x04000C71 RID: 3185
		[SettingData("Special accommodations to ignore", "Rules", "A comma separated list of special accommodation types to ignore.  For example, if you list 100 for extra time, extra time will not be applied during the online booking process.  (100=Extra time,200=Breaks,300=Add Icon,400=Email Coordinator,500=Can't book online,600=Time of day,700=Max per day,800=Days rest,900=Start/end of day slide,1000=Snap time)", Group.TESTBOOKING, SettingSemantic.TEXT, DefaultValue = "")]
		TESTBOOKING_SpecialAccommodationsToIgnore,
		// Token: 0x04000C72 RID: 3186
		[SettingData("Extension on course end date for authorization for students", "Rules", "The number of days the end date of the course will be virtually extended when checking if the student is allowed to book a test for this course.", Group.TESTBOOKING, SettingSemantic.INTEGER, DefaultValue = 0)]
		TESTBOOKING_CourseEndDateAuthorizationExtensionInDays,
		// Token: 0x04000C73 RID: 3187
		[SettingData("The notification email to the staff if the student changed the professor information", "Emails", "This email will only be sent if the student changes the professor info.", Group.TESTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~adminemail~#</to>\r\n    <from></from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>ClockWork: Student entered a different instructor name and/or email for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\nA student booked a test and submitted a new instructor name and/or email:\r\n\r\nCurrent instructor name and email: #~instructorname~# #~instructoremail~#\r\nStudent entered instructor name and email: #~newinstructorname~# #~newinstructoremail~#\r\n\r\nPlease verify this information and enter the correct instructor name and email into ClockWork.  \r\n    </body>\r\n </email>")]
		TESTBOOKING_StudentChangeProfInfoEmailTemplate,
		// Token: 0x04000C74 RID: 3188
		[SettingData("The notification email to the staff if the instructor notification email (not the automatic reminder emails) are enabled and the instructor does not have a valid email address in the system.", "Emails", "This email will only be sent if both of: 1. the immediate instructor notification email template is enabled, and 2. the instructor does not have an email in the system", Group.TESTBOOKING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n    <to>#~email~#</to>\r\n    <from></from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>ClockWork: Unable to send email to instructor after test booking for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\nA student booked a test but the system was unable to email the instructor because the instructor email address is missing or invalid:\r\n\r\nStudent: #~name~#\r\nCourse: #~course~#\r\nDate of booking: #~startdatetime~#\r\n\r\nPlease fill in the missing instructor email address and notify the instructor of this test booking.\r\n    </body>\r\n </email>")]
		TESTBOOKING_InstructorEmail_MissingEmailForInstructorEmailTemplate,
		// Token: 0x04000C75 RID: 3189
		[SettingData("Hide the 'Check all'/'Check none' links when choosing which accommodations for the test/exam", "Display", "Setting this to true will result in the student not having access to the 'check all' or 'check none' links, and will mean they will have to check each individual accommodation they require for their test.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_HideCheckAllCheckNone,
		// Token: 0x04000C76 RID: 3190
		[SettingData("Room availability mapping", "_Main settings", "Format: Room with availability personid:comma separated list of room personids;...  Example: 32:3,5,19;45:2,91,2  If this setting is set it will override the 'Room to use for all availability' setting.", Group.TESTBOOKING, SettingSemantic.TEXT)]
		TESTBOOKING_RoomAvailabilityMappings,
		// Token: 0x04000C77 RID: 3191
		[SettingData("Use the old 'Find potential room' algorithm", "_Main settings", "Do not set this 'true' unless instructed to do so by TechnoPro support staff.  This will use the older algorithm instead of the newer one.", Group.TESTBOOKING, SettingSemantic.BOOLEAN, DefaultValue = false)]
		TESTBOOKING_UseOldPotentialRoomAlgorithm,
		// Token: 0x04000C78 RID: 3192
		[SettingData("Which template to use for student booking confirmation email", "Emails", "Gets automatically sent to the student each time they book a test.  This setting will determine which template should be used based on the campus for the course the student is booking a test for.  Codes available: [classstartdate,classenddate,classstarttime,classendtime,classduration,startdate,enddate,classstarttime,classendtime,duration,room,email,firstname,lastname,student_no,name,accommodations,course,personid,appointmentid,instructor,instructoremail]", Group.TESTBOOKING, SettingSemantic.CAMPUSES_WITH_EMAILTEMPLATEIDS)]
		TESTBOOKING_Email_StudentBookingConfirmation_TemplateRules,
		// Token: 0x04000C79 RID: 3193
		LDAP_server = 40000,
		// Token: 0x04000C7A RID: 3194
		LDAP_port,
		// Token: 0x04000C7B RID: 3195
		LDAP_domain,
		// Token: 0x04000C7C RID: 3196
		LDAP_lookupattribute,
		// Token: 0x04000C7D RID: 3197
		LDAP_returnattribute,
		// Token: 0x04000C7E RID: 3198
		LDAP_authtype,
		// Token: 0x04000C7F RID: 3199
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.LDAP, SettingSemantic.DATETIME, IsHidden = true)]
		LDAP_LastModifiedTime,
		// Token: 0x04000C80 RID: 3200
		[SettingData("Email control id", "Email", "", Group.GENERAL, SettingSemantic.CONTROLID_PERSTUDENT, Description = "Use the ClockWork admin (Manage Forms) to lookup the controlid for the email", IsHidden = false)]
		GENERAL_EmailCid = 50000,
		// Token: 0x04000C81 RID: 3201
		[SettingData("Email encrypted", "Email", "", Group.GENERAL, SettingSemantic.BOOLEAN, Description = "Is the email field encrypted (probably it is)", DefaultValue = true, IsHidden = false)]
		GENERAL_EmailEncrypted,
		// Token: 0x04000C82 RID: 3202
		[SettingData("Email suffix", "Email", "", Group.GENERAL, SettingSemantic.TEXT, Description = "What should be added onto the student's username in order to make it their school email address (ex. @school.ca)", IsHidden = false)]
		GENERAL_EmailSuffix,
		// Token: 0x04000C83 RID: 3203
		[SettingData("Email suffix 2", "Email", "", Group.GENERAL, SettingSemantic.TEXT, Description = "Is there an alternate email suffix (leave this blank if not required)", IsHidden = true)]
		GENERAL_EmailSuffix2,
		// Token: 0x04000C84 RID: 3204
		[SettingData("Hide errors", Group.GENERAL, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		GENERAL_ShowErrors,
		// Token: 0x04000C85 RID: 3205
		[SettingData("Administrator's personal ids", Group.GENERAL, SettingSemantic.TEXT, Description = "A comma separated list of personids for users who will be super administrators of this system.", DefaultValue = "1", IsHidden = true)]
		GENERAL_AdminPersonIds,
		// Token: 0x04000C86 RID: 3206
		[SettingData("Minutes to cache user data", "Caching", "Number of minutes to cache user data", Group.GENERAL, SettingSemantic.INTEGER, DefaultValue = 10)]
		GENERAL_Caching_MinutesToCacheUserData,
		// Token: 0x04000C87 RID: 3207
		[SettingData("Minutes to cache public data", "Caching", "Number of minutes to cache public data", Group.GENERAL, SettingSemantic.INTEGER, DefaultValue = 25)]
		GENERAL_Caching_MinutesToCachePublicData,
		// Token: 0x04000C88 RID: 3208
		[SettingData("Minutes to cache form definitions", "Caching", "Number of minutes to cache form definitions", Group.GENERAL, SettingSemantic.INTEGER, DefaultValue = 60)]
		GENERAL_Caching_MinutesToCacheFormDefinitions,
		// Token: 0x04000C89 RID: 3209
		[SettingData("Info message for 'My upcoming Events'", "Display", "", Group.GENERAL, SettingSemantic.TEXT, DefaultValue = "")]
		GENERAL_MyUpcomingAppointments_Info,
		// Token: 0x04000C8A RID: 3210
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.GENERAL, SettingSemantic.DATETIME, IsHidden = true)]
		GENERAL_LastModifiedTime,
		// Token: 0x04000C8B RID: 3211
		[FormSetting("Smtp server", Group.GENERAL, SettingSemantic.TEXT, 101, FormSettingType.STRING, Description = "Server name for incoming/outgoing emails", DefaultValue = "", IsHidden = true)]
		GENERAL_SMTP_Server,
		// Token: 0x04000C8C RID: 3212
		[FormSetting("Smtp port", Group.GENERAL, SettingSemantic.INTEGER, 102, FormSettingType.INTEGER, Description = "Port number for incoming/outgoing emails", DefaultValue = 25, IsHidden = true)]
		GENERAL_SMTP_Port,
		// Token: 0x04000C8D RID: 3213
		[FormSetting("Smtp username", Group.GENERAL, SettingSemantic.TEXT, 104, FormSettingType.STRING, Description = "User name for incoming/outgoing emails", IsHidden = true)]
		GENERAL_SMTP_Username,
		// Token: 0x04000C8E RID: 3214
		[FormSetting("Smtp password", Group.GENERAL, SettingSemantic.PASSWORD, 105, FormSettingType.PASSWORD, Description = "Password for incoming/outgoing emails", IsHidden = true)]
		GENERAL_SMTP_Password,
		// Token: 0x04000C8F RID: 3215
		[SettingData("Application is in a Portal environment", "Behaviour", "Changes some things (hides all logout buttons)", Group.GENERAL, SettingSemantic.BOOLEAN, DefaultValue = false)]
		GENERAL_InPortalEnvironment = 50020,
		// Token: 0x04000C90 RID: 3216
		[SettingData("Admin email", "Email", "", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_AdminEmail,
		// Token: 0x04000C91 RID: 3217
		[SettingData("Default from email address", "Email", "The from address to be used for emails (this can be overriden in individual templates, but some emails that are sent by the system will use this", Group.GENERAL, SettingSemantic.TEXT, DefaultValue = "")]
		GENERAL_FromEmailAddress,
		// Token: 0x04000C92 RID: 3218
		[SettingData("Contact information", "Display", "Contact information for the department that will be displayed to the student.", Group.GENERAL, SettingSemantic.HTML)]
		GENERAL_DepartmentContactInformation,
		// Token: 0x04000C93 RID: 3219
		[SettingData("Language / Country Code", "Regional settings", "Example: en-CA (Canada), en-US (U.S.), fr (France), fr-CA (French - Canada), es-MX (Spanish - Mexico), es-ES (Spanish - Spain)", Group.GENERAL, SettingSemantic.TEXT, DefaultValue = "en-CA")]
		GENERAL_LanguageCountryCode,
		// Token: 0x04000C94 RID: 3220
		[SettingData("Name for 'Session' (examples: session, semester, term)", "Display", "You can provide the preferred term that means 'session' here.  References to session on any of the web pages will use the name you provide here.  The default term is 'session'.", Group.GENERAL, SettingSemantic.TEXT, DefaultValue = "session")]
		GENERAL_TermForSession = 50030,
		// Token: 0x04000C95 RID: 3221
		[SettingData("Application is in a Portal environment - always show logout link", "Behaviour", "Only used if the 'Application is in a Portal Environment' setting is 'true' - the logout link will show if this is true.", Group.GENERAL, SettingSemantic.BOOLEAN, DefaultValue = false)]
		GENERAL_InPortalEnvironment_OverrideShowLogoutLink,
		// Token: 0x04000C96 RID: 3222
		[SettingData("Message to students when they try to do something that requires them to be in ClockWork, but they are not in ClockWork.", "Error messages", "", Group.GENERAL, SettingSemantic.TEXT, DefaultValue = "You are not currently registered with us.")]
		GENERAL_ErrorMessage_NotAClockWorkStudent = 50135,
		// Token: 0x04000C97 RID: 3223
		[SettingData("Instance title", "_Main settings", "The title of the instance this web application is using.  You must enter the instance title in /custom/ExternalAppSettings.config.", Group.GENERAL, SettingSemantic.TEXT, DefaultValue = "ClockWork")]
		GENERAL_InstanceName,
		// Token: 0x04000C98 RID: 3224
		[SettingData("Contact info", "_Main settings", "Contact info for display", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_ContactInfo,
		// Token: 0x04000C99 RID: 3225
		[SettingData("Test/Exam Booking: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_TestExam,
		// Token: 0x04000C9A RID: 3226
		[SettingData("Notetaking: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_Notetaking,
		// Token: 0x04000C9B RID: 3227
		[SettingData("Instructor: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_Instructor,
		// Token: 0x04000C9C RID: 3228
		[SettingData("Workshops: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_Workshops,
		// Token: 0x04000C9D RID: 3229
		[SettingData("Appointment booking: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_AppointmentBooking,
		// Token: 0x04000C9E RID: 3230
		[SettingData("Self registration: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_SelfRegistration,
		// Token: 0x04000C9F RID: 3231
		[SettingData("Surveys: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_Surveys,
		// Token: 0x04000CA0 RID: 3232
		[SettingData("Test/Exam Booking: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_TestExam,
		// Token: 0x04000CA1 RID: 3233
		[SettingData("Notetaking: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_Notetaking,
		// Token: 0x04000CA2 RID: 3234
		[SettingData("Instructor: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_Instructor,
		// Token: 0x04000CA3 RID: 3235
		[SettingData("Workshops: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_Workshops,
		// Token: 0x04000CA4 RID: 3236
		[SettingData("Appointment booking: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_AppointmentBooking,
		// Token: 0x04000CA5 RID: 3237
		[SettingData("Self registration: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_SelfRegistration,
		// Token: 0x04000CA6 RID: 3238
		[SettingData("Surveys: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_Surveys,
		// Token: 0x04000CA7 RID: 3239
		[SettingData("Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature,
		// Token: 0x04000CA8 RID: 3240
		[SettingData("Student accommodations: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_StudentAccommodations,
		// Token: 0x04000CA9 RID: 3241
		[SettingData("Student accommodations: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_StudentAccommodations,
		// Token: 0x04000CAA RID: 3242
		[SettingData("Intake: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_Intake,
		// Token: 0x04000CAB RID: 3243
		[SettingData("Intake: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_Intake,
		// Token: 0x04000CAC RID: 3244
		[SettingData("Veterans: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_Veterans,
		// Token: 0x04000CAD RID: 3245
		[SettingData("Veterans: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_Veterans,
		// Token: 0x04000CAE RID: 3246
		[SettingData("Tutoring: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_Tutoring,
		// Token: 0x04000CAF RID: 3247
		[SettingData("Tutoring: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_Tutoring,
		// Token: 0x04000CB0 RID: 3248
		[SettingData("Department logo image", "_Main settings", "Logo image for department. 50x50 pxls", Group.GENERAL, SettingSemantic.IMAGE)]
		GENERAL_DepartmentLogoImage,
		// Token: 0x04000CB1 RID: 3249
		[SettingData("Allow ClockWork web to be embedded in a frame of another website (NOT recommended)", "Security", "It is considered a security vulnerability to allow an application to be embedded in a frame.  It is recommended to not set this setting to true.", Group.GENERAL, SettingSemantic.BOOLEAN, DefaultValue = false)]
		GENERAL_AllowClockWorkInFrame,
		// Token: 0x04000CB2 RID: 3250
		[SettingData("Required form: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_RequiredForm,
		// Token: 0x04000CB3 RID: 3251
		[SettingData("Required form: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_RequiredForm,
		// Token: 0x04000CB4 RID: 3252
		[SettingData("Student files: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT, IsHidden = true)]
		GENERAL_DefaultSignature_StudentFiles,
		// Token: 0x04000CB5 RID: 3253
		[SettingData("Student files: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT, IsHidden = true)]
		GENERAL_DefaultFrom_StudentFiles,
		// Token: 0x04000CB6 RID: 3254
		[SettingData("Hide all 'Submit comment' menu items", "Display", "", Group.GENERAL, SettingSemantic.BOOLEAN)]
		GENERAL_HideAllSubmitCommentMenuItems,
		// Token: 0x04000CB7 RID: 3255
		[SettingData("Online Forms: Default from address", "Email", "The mail merge code is #<from>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultFrom_OnlineForms,
		// Token: 0x04000CB8 RID: 3256
		[SettingData("Online Forms: Default signature", "Email signatures", "The mail merge code is #<signature>#", Group.GENERAL, SettingSemantic.TEXT)]
		GENERAL_DefaultSignature_OnlineForms,
		// Token: 0x04000CB9 RID: 3257
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.OTHER, SettingSemantic.DATETIME, IsHidden = true)]
		OTHER_LastModifiedTime = 24000,
		// Token: 0x04000CBA RID: 3258
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.INVENTORYSYSTEM, SettingSemantic.DATETIME, IsHidden = true)]
		INVENTORYSYSTEM_LastModifiedTime = 250000,
		// Token: 0x04000CBB RID: 3259
		[SettingData("Temporary files path", "_Main settings", "Path to the temporary files", Group.INVENTORYSYSTEM, SettingSemantic.TEXT)]
		INVENTORYSYSTEM_TempFilesPath,
		// Token: 0x04000CBC RID: 3260
		[SettingData("Product barcode prefix", "_Main settings", "Prefix use in product barcode generator", Group.INVENTORYSYSTEM, SettingSemantic.TEXT, DefaultValue = "")]
		INVENTORYSYSTEM_ProductBarcodePrefix,
		// Token: 0x04000CBD RID: 3261
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.STAFF, SettingSemantic.DATETIME, IsHidden = true)]
		STAFF_LastModifiedTime = 260000,
		// Token: 0x04000CBE RID: 3262
		[SettingData("Appointment types allowed", "Calendar", "Appointment types allowed (comma separated listing of apptypeids)", Group.STAFF, SettingSemantic.TEXT)]
		STAFF_Appointments_AllowedAppTypeIds,
		// Token: 0x04000CBF RID: 3263
		[SettingData("Groups allowed to view calendars for", "Calendar", "List of comma separated groupids", Group.STAFF, SettingSemantic.TEXT)]
		STAFF_Appointments_AllowedViewCalendarGroupIds,
		// Token: 0x04000CC0 RID: 3264
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.AUTOMATICUPDATING, SettingSemantic.DATETIME, IsHidden = true)]
		AUTOMATICUPDATING_LastModifiedTime = 320000,
		// Token: 0x04000CC1 RID: 3265
		[SettingData("Test Mode", "True if Developing environment is in used, false if Production environment is in used", Group.AUTOMATICUPDATING, SettingSemantic.BOOLEAN, DefaultValue = false, IsHidden = true)]
		AUTOMATICUPDATING_TestMode,
		// Token: 0x04000CC2 RID: 3266
		[SettingData("The notification email sent to 'admin' when an on schedule update is cancelled", "Emails", "This email will only be sent when an on schedule update is cancel because a new update came.", Group.AUTOMATICUPDATING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021|320003~#</from>\r\n     <to>#~value`websettingid=320003|50021~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Automatic Updates from #~institutionname~#]: On schedule #~updatetype~# was cancelled</subject>\r\n     <attachments></attachments>\r\n     <body>On schedule update #~updatefilename~# was cancelled because of new #~dependentupdatetype~# was coming on #~executiondatetime~#.</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		AUTOMATICUPDATING_Email_OnScheduleCancellationNotification,
		// Token: 0x04000CC3 RID: 3267
		[SettingData("Updates notification email addresses", "_Main Settings", "Comma-separated list of emails for receiving notification about ClockWork updates", Group.AUTOMATICUPDATING, SettingSemantic.TEXT)]
		AUTOMATICUPDATING_AdminEmail,
		// Token: 0x04000CC4 RID: 3268
		[SettingData("The notification email to 'admin' when an update was successfully applied", "Emails", "This email will only be sent when an on schedule update is successfully applied.", Group.AUTOMATICUPDATING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021|320003~#</from>\r\n     <to>#~value`websettingid=320003|50021~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Automatic Updates from #~institutionname~#]: On schedule #~updatetype~# was successfully applied</subject>\r\n     <attachments></attachments>\r\n     <body>On schedule update #~updatefilename~# was successfully applied on #~executiondatetime~#.</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		AUTOMATICUPDATING_Email_UpdateSuccessNotification,
		// Token: 0x04000CC5 RID: 3269
		[SettingData("The notification email to 'admin' when an update was executed with errors", "Emails", "This email will only be sent when an on schedule update is executed with errors.", Group.AUTOMATICUPDATING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021|320003~#</from>\r\n     <to>#~value`websettingid=320003|50021~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Automatic Updates from #~institutionname~#]: On schedule #~updatetype~# was executed with errors</subject>\r\n     <attachments></attachments>\r\n     <body>On schedule update #~updatefilename~# was executed with errors on #~executiondatetime~#.\r\n     Error message: #~errormessage~#.</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		AUTOMATICUPDATING_Email_UpdateWithErrorNotification,
		// Token: 0x04000CC6 RID: 3270
		[SettingData("The notification email to 'admin' when updates are available for installation", "Emails", "This email will only be sent when new updates are available for installation.", Group.AUTOMATICUPDATING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021|320003~#</from>\r\n     <to>#~value`websettingid=320003|50021~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Automatic Updates from #~institutionname~#]: A new #~updatetype~# is ready to be installed</subject>\r\n     <attachments></attachments>\r\n     <body>A new  #~updatefilename~# is ready to install. Please, go to ClockWork Admin and schedule it for installation.\r\n\r\nIf you want to know what is new in this version. Please, follow the link below:\r\n\r\n&lt;a href='#~updatechangesurl~#'&gt; #~updatechangesurl~# &lt;/a&gt;\r\n     </body>\r\n     <isactive>1</isactive>\r\n</email>")]
		AUTOMATICUPDATING_Email_NewUpdatesNotification,
		// Token: 0x04000CC7 RID: 3271
		[SettingData("Enable email notifications", "Admin", "Enable all email notifications associated with automatic updates", Group.AUTOMATICUPDATING, SettingSemantic.BOOLEAN, DefaultValue = true)]
		AUTOMATICUPDATING_EnableEmailNotification,
		// Token: 0x04000CC8 RID: 3272
		[SettingData("Notification email to 'admin' when reports have been successfully imported", "Emails", "This email will only be sent when reports have been successfully imported into the system.", Group.AUTOMATICUPDATING, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~value`websettingid=50022|50021|320003~#</from>\r\n     <to>#~value`websettingid=320003|50021~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>[ClockWork Automatic Updates from #~institutionname~#]: Successfully imported reports</subject>\r\n     <attachments></attachments>\r\n     <body>The following report ids have been successfully imported into your system on #~importeddatetime~#:\r\n\r\n    #~reportidlist~#\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		AUTOMATICUPDATING_Email_SuccessfullyImportedReports,
		// Token: 0x04000CC9 RID: 3273
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.SURVEYS, SettingSemantic.DATETIME, IsHidden = true)]
		SURVEYS_LastModifiedTime = 360000,
		// Token: 0x04000CCA RID: 3274
		[SettingData("Per date form number for form A", "Form A", "Set to blank to disable form A", Group.SURVEYS, SettingSemantic.INTEGER)]
		SURVEYS_Form_A_ScreenNum,
		// Token: 0x04000CCB RID: 3275
		[SettingData("Form A Title", "Form A", "Web page title for Form A", Group.SURVEYS, SettingSemantic.TEXT)]
		SURVEYS_Form_A_Title,
		// Token: 0x04000CCC RID: 3276
		[SettingData("Form A Confirmation Email", "Form A", "This email will be sent on successful submission of this form", Group.SURVEYS, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n            <to>#~email~#</to>\r\n            <from>#~from~#</from>\r\n            <cc></cc>\r\n            <bcc></bcc>\r\n            <subject>Confirmation of form submission</subject>\r\n            <isactive>1</isactive>\r\n        <body>Hello #~firstname~#,\r\n\r\n        Thank you for submitting your form.  Your information has been submitted and will be processed shortly.  You will be notified by email when there is a status update.\r\n\r\n        #~signature~#\r\n            </body>\r\n         </email>")]
		SURVEYS_Form_A_ConfirmationEmail,
		// Token: 0x04000CCD RID: 3277
		[SettingData("Form A CheckBox (control id) indicating this form is available for the student to fill in.", "Form A", "The value of this checkbox in the student file will be set to false after the student successfully submits this form.", Group.SURVEYS, SettingSemantic.INTEGER)]
		SURVEYS_Form_A_CheckboxControlIndicatingOkToFillInNewForm,
		// Token: 0x04000CCE RID: 3278
		[SettingData("Per date form number for form B", "Form B", "Set to blank to disable form B", Group.SURVEYS, SettingSemantic.INTEGER)]
		SURVEYS_Form_B_ScreenNum = 360021,
		// Token: 0x04000CCF RID: 3279
		[SettingData("Form A Title", "Form B", "Web page title for Form B", Group.SURVEYS, SettingSemantic.TEXT)]
		SURVEYS_Form_B_Title,
		// Token: 0x04000CD0 RID: 3280
		[SettingData("Form A Confirmation Email", "Form B", "This email will be sent on successful submission of this form", Group.SURVEYS, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n            <to>#~email~#</to>\r\n            <from>#~from~#</from>\r\n            <cc></cc>\r\n            <bcc></bcc>\r\n            <subject>Confirmation of form submission</subject>\r\n            <isactive>1</isactive>\r\n        <body>Hello #~firstname~#,\r\n\r\n        Thank you for submitting your form.  Your information has been submitted and will be processed shortly.  You will be notified by email when there is a status update.\r\n\r\n        #~signature~#\r\n            </body>\r\n         </email>")]
		SURVEYS_Form_B_ConfirmationEmail,
		// Token: 0x04000CD1 RID: 3281
		[SettingData("Form A CheckBox (control id) indicating this form is available for the student to fill in.", "Form B", "The value of this checkbox in the student file will be set to false after the student successfully submits this form.", Group.SURVEYS, SettingSemantic.INTEGER)]
		SURVEYS_Form_B_CheckboxControlIndicatingOkToFillInNewForm,
		// Token: 0x04000CD2 RID: 3282
		[SettingData("Per date form number for form C", "Form C", "Set to blank to disable form C", Group.SURVEYS, SettingSemantic.INTEGER)]
		SURVEYS_Form_C_ScreenNum = 360031,
		// Token: 0x04000CD3 RID: 3283
		[SettingData("Form C Title", "Form C", "Web page title for Form C", Group.SURVEYS, SettingSemantic.TEXT)]
		SURVEYS_Form_C_Title,
		// Token: 0x04000CD4 RID: 3284
		[SettingData("Form C Confirmation Email", "Form C", "This email will be sent on successful submission of this form", Group.SURVEYS, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n            <to>#~email~#</to>\r\n            <from>#~from~#</from>\r\n            <cc></cc>\r\n            <bcc></bcc>\r\n            <subject>Confirmation of form submission</subject>\r\n            <isactive>1</isactive>\r\n        <body>Hello #~firstname~#,\r\n\r\n        Thank you for submitting your form.  Your information has been submitted and will be processed shortly.  You will be notified by email when there is a status update.\r\n\r\n        #~signature~#\r\n            </body>\r\n         </email>")]
		SURVEYS_Form_C_ConfirmationEmail,
		// Token: 0x04000CD5 RID: 3285
		[SettingData("Form C CheckBox (control id) indicating this form is available for the student to fill in.", "Form C", "The value of this checkbox in the student file will be set to false after the student successfully submits this form.", Group.SURVEYS, SettingSemantic.INTEGER)]
		SURVEYS_Form_C_CheckboxControlIndicatingOkToFillInNewForm,
		// Token: 0x04000CD6 RID: 3286
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.ONLINEFORMS, SettingSemantic.DATETIME, IsHidden = true)]
		ONLINEFORMS_LastModifiedTime = 390000,
		// Token: 0x04000CD7 RID: 3287
		[SettingData("Online forms page intro", "Display", "The intro paragraph on the online forms listing page for the student.", Group.ONLINEFORMS, SettingSemantic.HTML, DefaultValue = "")]
		ONLINEFORMS_StudentFilesIntro,
		// Token: 0x04000CD8 RID: 3288
		[SettingData("Show status for form submissions", "Display", "If enabled, the current status of the form submission will show beside in the list for the student to see.", Group.ONLINEFORMS, SettingSemantic.BOOLEAN, DefaultValue = true)]
		ONLINEFORMS_ShowFormSubmissionStatus,
		// Token: 0x04000CD9 RID: 3289
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.REQUIREDSESSIONFORM, SettingSemantic.DATETIME, IsHidden = true)]
		REQUIREDSESSIONFORM_LastModifiedTime = 370000,
		// Token: 0x04000CDA RID: 3290
		[SettingData("Enable the required forms system", "_Main settings", "The system will only force the student to fill in the required form if this setting is set to true.", Group.REQUIREDSESSIONFORM, SettingSemantic.BOOLEAN, DefaultValue = false)]
		REQUIREDSESSIONFORM_RequiredFormsEnabled,
		// Token: 0x04000CDB RID: 3291
		[SettingData("Required form settings", "_Main settings", "", Group.REQUIREDSESSIONFORM, SettingSemantic.REQUIRED_SESSION_FORMS, DefaultValue = "")]
		REQUIREDSESSIONFORM_RequiredFormInfos,
		// Token: 0x04000CDC RID: 3292
		[DatetimeModifiedSetting("Last modified time", "Last time the group was modified", Group.STUDENTFILES, SettingSemantic.DATETIME, IsHidden = true)]
		STUDENTFILES_LastModifiedTime = 380000,
		// Token: 0x04000CDD RID: 3293
		[SettingData("Files students can access", "_Main settings", "", Group.STUDENTFILES, SettingSemantic.STUDENT_FILES_RULES, DefaultValue = "")]
		STUDENTFILES_FilesToShow = 380002,
		// Token: 0x04000CDE RID: 3294
		[SettingData("Student files page intro", "Display", "The intro paragraph on the student files page for the student.", Group.STUDENTFILES, SettingSemantic.HTML, DefaultValue = "Your files are listed below.  Click the 'download file' link beside each file to download it.")]
		STUDENTFILES_StudentFilesIntro,
		// Token: 0x04000CDF RID: 3295
		[SettingData("Allow students to upload files", "_Main settings", "If enabled, links will appear for the student to click to upload files.", Group.STUDENTFILES, SettingSemantic.BOOLEAN, DefaultValue = false)]
		STUDENTFILES_EnableStudentFileUploads,
		// Token: 0x04000CE0 RID: 3296
		[SettingData("File upload instructions", "Display", "The intro paragraph on the student files page for the student.", Group.STUDENTFILES, SettingSemantic.HTML, DefaultValue = "You can submit documents to us here.  Examples include ...")]
		STUDENTFILES_FileUploadInstructions,
		// Token: 0x04000CE1 RID: 3297
		[SettingData("File upload file list control id", "Rules", "The control id of the (per-student form) file list control that all student file uploads will be placed into.", Group.STUDENTFILES, SettingSemantic.CONTROLID_PERSTUDENT)]
		STUDENTFILES_FileUploadControlId,
		// Token: 0x04000CE2 RID: 3298
		[SettingData("File types that the student is allowed to upload", "Rules", "", Group.STUDENTFILES, SettingSemantic.TEXT, DefaultValue = ".ppt,.pdf,.doc,.docx,.txt,.rtf,.html,.zip,.xls,.xlsx,.pptx,.jpg,.jpeg,.bmp,.gif,.png,.rar,.tif,.tiff,.wpd")]
		STUDENTFILES_AllowedFileTypes,
		// Token: 0x04000CE3 RID: 3299
		[SettingData("Invalid file format message for file uploads", "Display", "This message will be displayed to the student if they attempt to upload a file that is not in the allowed file types list.", Group.STUDENTFILES, SettingSemantic.TEXT, DefaultValue = "The document you are attempting to upload is not a supported file type.  For security protection this system only accepts the following file extensions: #<filetypes>#. Please convert your file to one of the accepted file types and re-submit.  Tip: Zip files are allowed, so zip your file before uploading if you are not able to convert it.")]
		STUDENTFILES_InvalidFileFormatUploadMessage,
		// Token: 0x04000CE4 RID: 3300
		[SettingData("File too big message for file uploads", "Display", "This message will be displayed to the student if they attempt to upload a file that is bigger than the maximum allowed file size (100MB).", Group.STUDENTFILES, SettingSemantic.TEXT, DefaultValue = "The file you attempted to upload was rejected because it exceeds the 100MB file size limit.  Please zip your file to reduce its size and re-submit.")]
		STUDENTFILES_FileTooLargeUploadMessage,
		// Token: 0x04000CE5 RID: 3301
		[SettingData("Successful file upload message", "Display", "This message will be displayed to the student after they have successfully uploaded a file.  This message should indicate that the upload was a success, and what the next steps are.", Group.STUDENTFILES, SettingSemantic.TEXT, DefaultValue = "Thank you - your document has been successfully submitted!  You will receive a confirmation email shortly.")]
		STUDENTFILES_SuccessfulUploadMessage,
		// Token: 0x04000CE6 RID: 3302
		[SettingData("Notification email sent to student after they have successfully uploaded a new document.", "Emails", "", Group.STUDENTFILES, SettingSemantic.EMAIL_TEMPLATE, DefaultValue = "<email>\r\n     <from>#~from~#</from>\r\n     <to>#~studentemail~#</to>\r\n     <cc></cc>\r\n     <bcc></bcc>\r\n     <subject>Document has been submitted</subject>\r\n     <attachments></attachments>\r\n     <body>Hi #~firstname~# #~lastname~#,\r\n\r\nYour document #~filename~# has been successfully submitted.\r\n\r\nYour comment was:\r\n\r\n#~comment~#\r\n\r\nYou will receive a follow up email once this document has been processed.\r\n\r\nThank you!\r\n\r\n#~signature~#\r\n</body>\r\n     <isactive>1</isactive>\r\n</email>")]
		STUDENTFILES_Email_SuccessfulUploadNotification,
		// Token: 0x04000CE7 RID: 3303
		[SettingData("Show file status for previously uploaded files", "Display", "If enabled, the current status of the upload will show beside the filename in the list for the student to see.", Group.STUDENTFILES, SettingSemantic.BOOLEAN, DefaultValue = true)]
		STUDENTFILES_ShowUploadedFileStatuses,
		// Token: 0x04000CE8 RID: 3304
		[DatetimeModifiedSetting("Unknown", Group.UNKNOWN, SettingSemantic.TEXT, IsHidden = true, DefaultValue = "")]
		UNKNOWN = 0
	}
}
