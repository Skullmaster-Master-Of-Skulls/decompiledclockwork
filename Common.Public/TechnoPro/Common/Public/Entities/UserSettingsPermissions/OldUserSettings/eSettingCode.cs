using System;
using TechnoPro.Common.Public.Entities.SettingsPermissionsGeneral;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public enum eSettingCode
	{
		// Token: 0x040003C5 RID: 965
		[OldUserSetting("Online forms that can be viewed in the Online Forms Queue (staff interface)", "List of online form ids allowed", eOldUserSettingInputType.numberArray_onlineFormIds, eOldUserSettingGroup.Forms, SettingLevel = eSettingLevel.Basic)]
		SETTING_OnlineForms_AllowedOnlineFormsInOnlineFormsQueue = 99829,
		// Token: 0x040003C6 RID: 966
		[OldUserSetting("Surveys that can be viewed in the Survey Queue (staff interface)", "List of survey ids allowed", eOldUserSettingInputType.numberArray, eOldUserSettingGroup.Forms, SettingLevel = eSettingLevel.Basic, IsHidden = true)]
		SETTING_Survey_AllowedSurveysInOnlineSurveyQueue = 99827,
		// Token: 0x040003C7 RID: 967
		[OldUserSetting("Disable auto-intake-data-sync", "If set to true, the intake data for existing students in ClockWork will not be automatically synced each night as part of the regular data sync process", eOldUserSettingInputType.yesno, eOldUserSettingGroup.Forms, SettingLevel = eSettingLevel.Advanced, DefaultValueInt = 1)]
		SETTING_Intake_DisableAutoIntakeDataSync = 99826,
		// Token: 0x040003C8 RID: 968
		[OldUserSetting("Multi-department intake settings", "Allows you to enable and configure multi-department intake", eOldUserSettingInputType.MultiDepartmentIntake, eOldUserSettingGroup.Forms, SettingLevel = eSettingLevel.Advanced)]
		SETTING_Intake_MultiDepartmentIntakeSettings = 99825,
		// Token: 0x040003C9 RID: 969
		[OldUserSetting("Screen number for other media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_OtherDynamicFormId = 99824,
		// Token: 0x040003CA RID: 970
		[OldUserSetting("Screen number for exam media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_ExamDynamicFormId = 99823,
		// Token: 0x040003CB RID: 971
		[OldUserSetting("Screen number for article media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_ArticleDynamicFormId = 99822,
		// Token: 0x040003CC RID: 972
		[OldUserSetting("Screen number for document media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_DocumentDynamicFormId = 99821,
		// Token: 0x040003CD RID: 973
		[OldUserSetting("Hide Intake Queue", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons, DefaultValueInt = 0)]
		SETTING_ButtonHide_IntakeQueue = 99820,
		// Token: 0x040003CE RID: 974
		[OldUserSetting("Form approval options", eOldUserSettingInputType.FormApprovalOptionsXml, eOldUserSettingGroup.Forms, Description = "Form approval system is for appointment notes.  Trainees will write notes and submit for review; supervisors will approve notes.", SettingLevel = eSettingLevel.TechnoProOnly)]
		SETTING_FormApprovalOptions = 99819,
		// Token: 0x040003CF RID: 975
		[OldUserSetting("Auto-clear student signature for generating accommodation letters", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, DefaultValueInt = 0, SettingLevel = eSettingLevel.Basic)]
		SETTING_AutoClearStudentSignatureForAccommodationLetters = 99818,
		// Token: 0x040003D0 RID: 976
		[OldUserSetting("Auto-clear staff signature for generating accommodation letters", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, DefaultValueInt = 0, SettingLevel = eSettingLevel.Basic)]
		SETTING_AutoClearStaffSignatureForAccommodationLetters = 99817,
		// Token: 0x040003D1 RID: 977
		[OldUserSetting("Rich Textbox Control to save drag and drop emails into", "", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_DragAndDropEmailsRTBDestinationControlId = 99816,
		// Token: 0x040003D2 RID: 978
		[Obsolete("Use SETTING_FormApprovalOptions instead")]
		[OldUserSetting("Form approval options", eOldUserSettingInputType.FormApprovalOptionsXml, eOldUserSettingGroup.Forms, Description = "Form approval system is for appointment notes.  Trainees will write notes and submit for review; supervisors will approve notes.", IsHidden = true)]
		SETTING_FormApprovalOptions_Depracted = 99815,
		// Token: 0x040003D3 RID: 979
		[OldUserSetting("Use legacy export to Excel in test booking", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams, DefaultValueInt = 0, SettingLevel = eSettingLevel.Advanced, IsHidden = false)]
		SETTING_TestsExams_UseLegacyExportTestsToExcel = 99814,
		// Token: 0x040003D4 RID: 980
		[OldUserSetting("Html merge template for printing an appointment", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments, DefaultValueString = "<html>\r\n<head>\r\n<title>Test</title>\r\n<style type='text/css'>\r\n.greenbox \r\n{\r\n    border: 1px solid gray;\r\n    padding: 4px;\r\n\tbackground-color:LightGreen;\r\n}\r\n</style>\r\n</head>\r\n<body>\r\n<h2>scheduled appointment details</h2>\r\n<table class='greenbox' cellspacing='1' cellpadding='1' border='1'>\r\n<tr>\r\n    <td>\r\n        <b>Title:</b>\r\n    </td>\r\n    <td>\r\n        #<appdescription>#: #<appsubtitle>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Date:</b>\r\n    </td>\r\n    <td>\r\n        #<appdate>#, #<apptime>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Duration:</b>\r\n    </td>\r\n    <td>\r\n        #<appduration>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Location:</b>\r\n    </td>\r\n    <td>\r\n        #<roomandlocation>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Attendee count:</b>\r\n    </td>\r\n    <td>\r\n        #<attendeescount>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Attendees:</b>\r\n    </td>\r\n    <td>\r\n        #<attendees`formattype=BulletedList>#\r\n    </td>\r\n</tr>\r\n</table>\r\n</body>\r\n</html>")]
		SETTING_Calendar_PrintAppointmentTemplate = 99813,
		// Token: 0x040003D5 RID: 981
		[OldUserSetting("Number of days before course end date to abort dropping courses.", "", eOldUserSettingInputType.numeric, eOldUserSettingGroup.System, DefaultValueInt = 7, SubGroup = "Data Sync", SettingLevel = eSettingLevel.Advanced)]
		SETTING_DataSync_DropCourseEndDateBuffer = 99812,
		// Token: 0x040003D6 RID: 982
		[OldUserSetting("Buttons to hide (by name)", "A comma separated list of buttons to force to hide.  The names should match the names on the screen.", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "")]
		SETTING_ClockWorkButtonsToHide = 99811,
		// Token: 0x040003D7 RID: 983
		[OldUserSetting("Don't allow users to create/modify/delete appointments with a date past the cutoff period.", eOldUserSettingInputType.CutoffTime, eOldUserSettingGroup.Appointments, SettingLevel = eSettingLevel.Advanced)]
		SETTING_Appointments_DisallowCreatingEditingDeletingCutoff = 99810,
		// Token: 0x040003D8 RID: 984
		[OldUserSetting("Hide 'Test copy' tab in test/exam edit popup", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams, DefaultValueInt = 0, SettingLevel = eSettingLevel.Advanced)]
		SETTING_TestsExams_HideTestCopyTab = 99809,
		// Token: 0x040003D9 RID: 985
		[OldUserSetting("Number of seconds to keep regular messages notification on the screen. Zero means forever", eOldUserSettingInputType.numeric, eOldUserSettingGroup.System, DefaultValueInt = 30, SubGroup = "Messaging", SettingLevel = eSettingLevel.Advanced)]
		SETTING_Messaging_NumberOfSecondsToKeepRegularMessageOnScreen = 99808,
		// Token: 0x040003DA RID: 986
		[OldUserSetting("Number of seconds to keep student is waiting message notification on the screen. Zero means forever", eOldUserSettingInputType.numeric, eOldUserSettingGroup.System, DefaultValueInt = 30, SubGroup = "Messaging", SettingLevel = eSettingLevel.Advanced)]
		SETTING_Messaging_NumberOfSecondsToKeepStudentIsWaitingMessageOnScreen = 99807,
		// Token: 0x040003DB RID: 987
		[OldUserSetting("Use centralized layout store for test/exam screen", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams, DefaultValueInt = 1, SubGroup = "Messaging", SettingLevel = eSettingLevel.Advanced, IsHidden = true)]
		SETTING_TestsExams_UseCentralizedLayoutStore = 99806,
		// Token: 0x040003DC RID: 988
		[OldUserSetting("Preferred ClockWorkServer binding type", "Binding type used by client applications to connect to the server. e.g. NetTcpBinding, HttpBinding, ...", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_ClockWorkServer_PreferredBindingType = 99804,
		// Token: 0x040003DD RID: 989
		[OldUserSetting("Department description", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "")]
		SETTING_Department_Description = 99803,
		// Token: 0x040003DE RID: 990
		[OldUserSetting("Report id for school book search provider", eOldUserSettingInputType.numeric_reportid, eOldUserSettingGroup.AlternativeFormat, DefaultValueInt = 502526, SubGroup = "BookDataSync", SettingLevel = eSettingLevel.Advanced)]
		SETTING_AlternateFormat_SchoolBookSearchProviderReportId = 99802,
		// Token: 0x040003DF RID: 991
		[OldUserSetting("Force plain text mode in instant messaging", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, DefaultValueInt = 0, SubGroup = "Messaging", SettingLevel = eSettingLevel.Advanced)]
		SETTING_MESSAGING_ForcePlainTextMode = 99801,
		// Token: 0x040003E0 RID: 992
		[OldUserSetting("Form ordering in tabs in Student Info", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms, SettingLevel = eSettingLevel.Advanced, AllowOrderingForListControls = true, Description = "List the forms in the order you want them to appear on the student info tab.  Note that you cannot change the ordering of the Summary, Courses, Accommodations, Alternate Format, or any other built-in form tabs.")]
		SETTING_Screens_OrderingInStudentInfo = 99800,
		// Token: 0x040003E1 RID: 993
		[OldUserSetting("Disable the final exam sync", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, DefaultValueInt = 0, SubGroup = "DataSync", IsHidden = true)]
		SETTING_DataSync_DisableFinalExamSync = 99799,
		// Token: 0x040003E2 RID: 994
		[OldUserSetting("C# code for parsing magnetic card reader output (input: cardInput, output: student_no)", eOldUserSettingInputType.text, eOldUserSettingGroup.Students, DefaultValueString = "", SettingLevel = eSettingLevel.Advanced)]
		SETTING_MagneticCard_CSharp_Code_For_Parsing_Card_Reader_Output = 99798,
		// Token: 0x040003E3 RID: 995
		[OldUserSetting("Should the student use the accessible views on the web by default", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.PersonalOptions)]
		SETTING_StudentOption_UseAccessibleViewsOnWeb = 99797,
		// Token: 0x040003E4 RID: 996
		[OldUserSetting("Show Tutoring", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ShowTutoringButton = 99796,
		// Token: 0x040003E5 RID: 997
		[OldUserSetting("Use user-defined equivalent courses sql function", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.AlternativeFormat, DefaultValueInt = 0, SubGroup = "Admin")]
		SETTING_AlternateFormat_UserDefinedEquivalentCoursesFunction = 99795,
		// Token: 0x040003E6 RID: 998
		[OldUserSetting("Enable or disable Alternate Format", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.AlternativeFormat, DefaultValueInt = 1, SubGroup = "Admin")]
		SETTING_AlternateFormat_EnableAlternateFormat = 99794,
		// Token: 0x040003E7 RID: 999
		[OldUserSetting("ClockWork startup mode", eOldUserSettingInputType.ClockWork_Startup_Mode_XML, eOldUserSettingGroup.System)]
		SETTING_ClockWork_Startup_Mode = 99793,
		// Token: 0x040003E8 RID: 1000
		[OldUserSetting("Media content require proof of purchase default value", "This setting means when creating a new media content if by default it requires or not a proof of purchase. If the checkbox at the New Media Content form is check or not by default.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.AlternativeFormat, DefaultValueInt = 0, SubGroup = "Admin")]
		SETTING_AlternateFormat_MediaContentRequireProofOfPurchase = 99792,
		// Token: 0x040003E9 RID: 1001
		[OldUserSetting("Use asynchronous MSMQ services on the server side?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, DefaultValueInt = 0)]
		SETTING_ClockWorkServer_EnableServerAsyncMSMQ = 99791,
		// Token: 0x040003EA RID: 1002
		[OldUserSetting("Comma separated list of report ids for Alternate Format system", eOldUserSettingInputType.numberArray_reportids, eOldUserSettingGroup.AlternativeFormat, DefaultValueString = "502509,502510,502511,502512,502513,502514,502515,502516,502517,502518,502519,502520,502521,502522,502527", SubGroup = "Reports")]
		SETTING_AlternateFormat_User_Report_Ids = 99790,
		// Token: 0x040003EB RID: 1003
		[OldUserSetting("Total number of seconds between appointment reminders in ClockWork", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments, DefaultValueInt = 300)]
		SETTING_AppointmentsReminder_User_AppointmentsReminderTimeInterval = 99789,
		// Token: 0x040003EC RID: 1004
		[OldUserSetting("Length of student number in database", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Students)]
		SETTING_MagneticCard_Student_Number_Length_In_Database = 99788,
		// Token: 0x040003ED RID: 1005
		[OldUserSetting("Length of student number on magnetic card", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Students)]
		SETTING_MagneticCard_Student_Number_Length_On_Card = 99787,
		// Token: 0x040003EE RID: 1006
		[OldUserSetting("Comma separated list of report ids for inventory system", eOldUserSettingInputType.numberArray_reportids, eOldUserSettingGroup.Inventory, DefaultValueString = "502490,502491,502492,502493,502494,502495,502496,502497,502499,502500,502501,502502,502524", SubGroup = "Reports")]
		SETTING_Inventory_User_Report_Ids = 99786,
		// Token: 0x040003EF RID: 1007
		[OldUserSetting("Disable ClockWork 5 messaging", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System)]
		SETTING_MESSAGING_ForceMessagingDisabledV5 = 99785,
		// Token: 0x040003F0 RID: 1008
		[Obsolete]
		[OldUserSetting("Use recurring appointment schedule (note: this does not affect 'Multiple bookings')", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, IsHidden = true)]
		SETTING_ShowOldRecurringAppointmentSchedule = 99784,
		// Token: 0x040003F1 RID: 1009
		[OldUserSetting("Report to provide extended information for test/exam bookings list.", eOldUserSettingInputType.numeric_reportid, eOldUserSettingGroup.Exams)]
		SETTING_TestsExams_ExtendedInfoReportId = 99783,
		// Token: 0x040003F2 RID: 1010
		[OldUserSetting("Test manager grid template (setting string is compressed - set using CTRL + ALT in test manager in ClockWork)", eOldUserSettingInputType.textBig, eOldUserSettingGroup.Exams)]
		SETTING_TestsGridTemplateOverride = 99782,
		// Token: 0x040003F3 RID: 1011
		[OldUserSetting("Show Inventory", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ShowInventoryButtons = 99781,
		// Token: 0x040003F4 RID: 1012
		[OldUserSetting("Don't allow viewing actual files in a file list control (comma separated list of control ids)", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Unknown)]
		SETTING_FILE_LIST_NO_VIEWING_CIDS = 99780,
		// Token: 0x040003F5 RID: 1013
		[OldUserSetting("Is an Inventory Admin User?.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Inventory, DefaultValueInt = 0, SubGroup = "Admin")]
		SETTING_Inventory_IsInventoryAdmin = 99770,
		// Token: 0x040003F6 RID: 1014
		[OldUserSetting("Allowed inventory catalogs", eOldUserSettingInputType.numberArray_catalogIds, eOldUserSettingGroup.Inventory, DefaultValueString = "1", SubGroup = "Admin")]
		SETTING_Inventory_AllowedCatalogIds = 99769,
		// Token: 0x040003F7 RID: 1015
		[OldUserSetting("Screen number for course pack media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_CoursePackDynamicFormId = 99768,
		// Token: 0x040003F8 RID: 1016
		[OldUserSetting("Screen number for video file media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_VideoFileDynamicFormId = 99767,
		// Token: 0x040003F9 RID: 1017
		[OldUserSetting("Screen number for audio file media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_AudioFileDynamicFormId = 99766,
		// Token: 0x040003FA RID: 1018
		[OldUserSetting("Screen number for alternate textbook media content format.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.AlternativeFormat, SubGroup = "Extended properties forms")]
		SETTING_AlternativeFormat_AlternateTextBookDynamicFormId = 99765,
		// Token: 0x040003FB RID: 1019
		[OldUserSetting("Screen number for in place student editing in calendar.", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Students)]
		SETTING_MedicalScheduler_StudentScreenNumForStudentInPlaceEditing = 99764,
		// Token: 0x040003FC RID: 1020
		[OldUserSetting("Html merge template for printing a workshop appointment", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments, DefaultValueString = "<html>\r\n<head>\r\n<title>Test</title>\r\n<style type='text/css'>\r\n.greenbox \r\n{\r\n    border: 1px solid gray;\r\n    padding: 4px;\r\n\tbackground-color:LightGreen;\r\n}\r\n</style>\r\n</head>\r\n<body>\r\n<h2>Workshop scheduled appointment details</h2>\r\n<table class='greenbox' cellspacing='1' cellpadding='1' border='1'>\r\n<tr>\r\n    <td>\r\n        <b>Title:</b>\r\n    </td>\r\n    <td>\r\n        #<appdescription>#: #<workshop># #<appsubtitle>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Date:</b>\r\n    </td>\r\n    <td>\r\n        #<appdate>#, #<apptime>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Duration:</b>\r\n    </td>\r\n    <td>\r\n        #<appduration>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Location:</b>\r\n    </td>\r\n    <td>\r\n        #<roomandlocation>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Max attendee count:</b>\r\n    </td>\r\n    <td>\r\n        #<appmaxattendeecount>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Attendee count:</b>\r\n    </td>\r\n    <td>\r\n        #<attendeescount>#\r\n    </td>\r\n</tr>\r\n<tr>\r\n    <td>\r\n        <b>Attendees:</b>\r\n    </td>\r\n    <td>\r\n        #<attendees`formattype=BulletedList>#\r\n    </td>\r\n</tr>\r\n</table>\r\n</body>\r\n</html>")]
		SETTING_Workshop_PrintAppointmentTemplate = 99763,
		// Token: 0x040003FD RID: 1021
		[OldUserSetting("Room person ids to add to the graphical test booking calendar", eOldUserSettingInputType.numberArray, eOldUserSettingGroup.Exams, Description = "Note that the room list here will not replace the full list of rooms - any rooms added here will be added to the master list of all test/exam rooms in case it's not already there.  For example, if a user is not normally allowed to see a room or a room has been deleted.  Note: to remove a room normally displayed in the list, make the personid negative (eg. -32 instead of 32).")]
		SETTING_TestsRoomPidsForCalendar = 99762,
		// Token: 0x040003FE RID: 1022
		[OldUserSetting("Gender control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_GenderControlId = 99761,
		// Token: 0x040003FF RID: 1023
		[OldUserSetting("Student info summary tab fields to show", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Forms)]
		SETTING_StudentInfoSummaryTab_ControlIdsToShow = 99760,
		// Token: 0x04000400 RID: 1024
		[OldUserSetting("Use the old accommodations screen; this should only be used as a last resort and will eventually be discontinued.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_UseOldAccommodationsScreen = 99759,
		// Token: 0x04000401 RID: 1025
		[OldUserSetting("Show Online Accommodations Requests (self registration)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_UseAccommodationRequests = 99758,
		// Token: 0x04000402 RID: 1026
		[OldUserSetting("Show Task System", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_UseTasks = 99757,
		// Token: 0x04000403 RID: 1027
		[OldUserSetting("Text calendar notes form number", eOldUserSettingInputType.numberArray_screennum_perapp, eOldUserSettingGroup.Unknown)]
		SETTING_TextCalendarNotesScreenNum = 99756,
		// Token: 0x04000404 RID: 1028
		[OldUserSetting("Email control is NOT encrypted", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown)]
		SETTING_EmailCidIsNotEncrypted = 99755,
		// Token: 0x04000405 RID: 1029
		[OldUserSetting("Auto open 'Student Information' when selecting a student through F11 search", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_AutoOpenStudentInfoOnF11Search = 99754,
		// Token: 0x04000406 RID: 1030
		[OldUserSetting("Type of signature control", eOldUserSettingInputType.text, eOldUserSettingGroup.Unknown)]
		SETTING_SignatureControlType = 99753,
		// Token: 0x04000407 RID: 1031
		[OldUserSetting("Documents list field.  Emails and accommodations will be stored here.", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Accommodations)]
		SETTING_DocumentsControlId = 99752,
		// Token: 0x04000408 RID: 1032
		[OldUserSetting("When generating accommodation letters, provide a place for the advisor and student to sign", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown)]
		SETTING_Accommodations_StudentAndAdvisorSigns = 99751,
		// Token: 0x04000409 RID: 1033
		[OldUserSetting("Group(s) to show in the calendar drop list", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_GroupsToShowInCalendarDropList = 99749,
		// Token: 0x0400040A RID: 1034
		[OldUserSetting("Monitoring Lists (Summary Management)", eOldUserSettingInputType.text, eOldUserSettingGroup.Unknown)]
		SETTING_MonitorLists = 99748,
		// Token: 0x0400040B RID: 1035
		[OldUserSetting("Run data sync reports locally, even if ClockWork server is running.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown)]
		SETTING_RunDataSyncReportsLocally = 99747,
		// Token: 0x0400040C RID: 1036
		[OldUserSetting("Phone control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_PhoneControlId = 99746,
		// Token: 0x0400040D RID: 1037
		[OldUserSetting("BirthDate control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_BirthDateControlId = 99745,
		// Token: 0x0400040E RID: 1038
		[OldUserSetting("Medicare control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_MedicareControlId = 99744,
		// Token: 0x0400040F RID: 1039
		[OldUserSetting("Medical Calendar Enabled", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_Enabled = 99743,
		// Token: 0x04000410 RID: 1040
		[OldUserSetting("Groups allowed to close days", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_GroupsAllowedToCloseDays = 99742,
		// Token: 0x04000411 RID: 1041
		[OldUserSetting("Groups allowed to enter availability", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_GroupsAllowedToEnterAvailability = 99741,
		// Token: 0x04000412 RID: 1042
		[OldUserSetting("Doctor action form number", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_DoctorActionScreenNum = 99740,
		// Token: 0x04000413 RID: 1043
		[OldUserSetting("Nurse actions form number", eOldUserSettingInputType.numberArray_screennum_perapp, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_NurseActionScreenNum = 99739,
		// Token: 0x04000414 RID: 1044
		[OldUserSetting("Student information control", eOldUserSettingInputType.numberArray_screennum_perapp, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_StudentInformationCid = 99738,
		// Token: 0x04000415 RID: 1045
		[OldUserSetting("Important information controlid", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_ImportantInformationCid = 99737,
		// Token: 0x04000416 RID: 1046
		[OldUserSetting("Student information form number", eOldUserSettingInputType.numeric_screenNum, eOldUserSettingGroup.Unknown)]
		SETTING_MedicalScheduler_StudentScreenNum = 99736,
		// Token: 0x04000417 RID: 1047
		[OldUserSetting("Label Printer Templates", eOldUserSettingInputType.text, eOldUserSettingGroup.Unknown)]
		SETTING_LabelTemplates = 99735,
		// Token: 0x04000418 RID: 1048
		[OldUserSetting("Form buttons to show in ribbon bar (direct access to individual forms)", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms)]
		SETTING_LegacyFormsToShowInRibbonBar = 99734,
		// Token: 0x04000419 RID: 1049
		[OldUserSetting("Default all new test and exam bookings to tentative (in staff interface only)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams, SettingLevel = eSettingLevel.Advanced)]
		SETTING_BookAllTestsExamsAsTentative = 99733,
		// Token: 0x0400041A RID: 1050
		[OldUserSetting("Generate dynamic form mail merge documents as PDF", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_GenerateDocumentsAsPdf = 99732,
		// Token: 0x0400041B RID: 1051
		[OldUserSetting("Timetable display - html template", eOldUserSettingInputType.text, eOldUserSettingGroup.Courses, DefaultValueString = "<html>\r\n<body>\r\n\r\n<table width='100%' cellspacing='1' cellpadding='1' border='1' style='table-layout:fixed; font-size: .72em;'>\r\n\t<tr>\r\n\t\t<th width='123px'>&nbsp;</th>\r\n\t\t<th align='center' style='background-color: #EEE;'>Monday</th>\r\n\t\t<th align='center' style='background-color: #EEE;'>Tuesday</th>\r\n\t\t<th align='center' style='background-color: #EEE;'>Wednesday</th>\r\n\t\t<th align='center' style='background-color: #EEE;'>Thursday</th>\r\n\t\t<th align='center' style='background-color: #EEE;'>Friday</th>\r\n        <th align='center' style='background-color: #EEE;'>Saturday</th>\r\n        <th align='center' style='background-color: #EEE;'>Sunday</th>\r\n\t</tr>\r\n    <tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>7:30-8:30am</td>\r\n\t\t<td align='center'>{0}</td>\r\n\t\t<td align='center'>{1}</td>\r\n\t\t<td align='center'>{2}</td>\r\n\t\t<td align='center'>{3}</td>\r\n\t\t<td align='center'>{4}</td>\r\n\t\t<td align='center'>{5}</td>\r\n\t\t<td align='center'>{6}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>8:30-9:30am</td>\r\n\t\t<td align='center'>{7}</td>\r\n\t\t<td align='center'>{8}</td>\r\n\t\t<td align='center'>{9}</td>\r\n\t\t<td align='center'>{10}</td>\r\n\t\t<td align='center'>{11}</td>\r\n\t\t<td align='center'>{12}</td>\r\n\t\t<td align='center'>{13}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>9:30-10:30am</td>\r\n\t\t<td align='center'>{14}</td>\r\n\t\t<td align='center'>{15}</td>\r\n\t\t<td align='center'>{16}</td>\r\n\t\t<td align='center'>{17}</td>\r\n\t\t<td align='center'>{18}</td>\r\n\t\t<td align='center'>{19}</td>\r\n\t\t<td align='center'>{20}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>10:30-11:30am</td>\r\n\t\t<td align='center'>{21}</td>\r\n\t\t<td align='center'>{22}</td>\r\n\t\t<td align='center'>{23}</td>\r\n\t\t<td align='center'>{24}</td>\r\n\t\t<td align='center'>{25}</td>\r\n\t\t<td align='center'>{26}</td>\r\n\t\t<td align='center'>{27}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>11:30-12:30pm</td>\r\n\t\t<td align='center'>{28}</td>\r\n\t\t<td align='center'>{29}</td>\r\n\t\t<td align='center'>{30}</td>\r\n\t\t<td align='center'>{31}</td>\r\n\t\t<td align='center'>{32}</td>\r\n\t\t<td align='center'>{33}</td>\r\n\t\t<td align='center'>{34}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>12:30-1:30pm</td>\r\n\t\t<td align='center'>{35}</td>\r\n\t\t<td align='center'>{36}</td>\r\n\t\t<td align='center'>{37}</td>\r\n\t\t<td align='center'>{38}</td>\r\n\t\t<td align='center'>{39}</td>\r\n\t\t<td align='center'>{40}</td>\r\n\t\t<td align='center'>{41}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>1:30-2:30pm</td>\r\n\t\t<td align='center'>{42}</td>\r\n\t\t<td align='center'>{43}</td>\r\n\t\t<td align='center'>{44}</td>\r\n\t\t<td align='center'>{45}</td>\r\n\t\t<td align='center'>{46}</td>\r\n\t\t<td align='center'>{47}</td>\r\n\t\t<td align='center'>{48}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>2:30-3:30pm</td>\r\n\t\t<td align='center'>{49}</td>\r\n\t\t<td align='center'>{50}</td>\r\n\t\t<td align='center'>{51}</td>\r\n\t\t<td align='center'>{52}</td>\r\n\t\t<td align='center'>{53}</td>\r\n\t\t<td align='center'>{54}</td>\r\n\t\t<td align='center'>{55}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>3:30-4:30pm</td>\r\n\t\t<td align='center'>{56}</td>\r\n\t\t<td align='center'>{57}</td>\r\n\t\t<td align='center'>{58}</td>\r\n\t\t<td align='center'>{59}</td>\r\n\t\t<td align='center'>{60}</td>\r\n\t\t<td align='center'>{61}</td>\r\n\t\t<td align='center'>{62}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>4:30-5:30pm</td>\r\n\t\t<td align='center'>{63}</td>\r\n\t\t<td align='center'>{64}</td>\r\n\t\t<td align='center'>{65}</td>\r\n\t\t<td align='center'>{66}</td>\r\n\t\t<td align='center'>{67}</td>\r\n\t\t<td align='center'>{68}</td>\r\n\t\t<td align='center'>{69}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>5:30-6:30pm</td>\r\n\t\t<td align='center'>{70}</td>\r\n\t\t<td align='center'>{71}</td>\r\n\t\t<td align='center'>{72}</td>\r\n\t\t<td align='center'>{73}</td>\r\n\t\t<td align='center'>{74}</td>\r\n\t\t<td align='center'>{75}</td>\r\n\t\t<td align='center'>{76}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>6:30-7:30pm</td>\r\n\t\t<td align='center'>{77}</td>\r\n\t\t<td align='center'>{78}</td>\r\n\t\t<td align='center'>{79}</td>\r\n\t\t<td align='center'>{80}</td>\r\n\t\t<td align='center'>{81}</td>\r\n\t\t<td align='center'>{82}</td>\r\n\t\t<td align='center'>{83}</td>\r\n\t</tr>\r\n\t<tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>7:30-8:30pm</td>\r\n\t\t<td align='center'>{84}</td>\r\n\t\t<td align='center'>{85}</td>\r\n\t\t<td align='center'>{86}</td>\r\n\t\t<td align='center'>{87}</td>\r\n\t\t<td align='center'>{88}</td>\r\n\t\t<td align='center'>{89}</td>\r\n\t\t<td align='center'>{90}</td>\r\n\t</tr>\r\n    <tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>8:30-9:30pm</td>\r\n\t\t<td align='center'>{91}</td>\r\n\t\t<td align='center'>{92}</td>\r\n\t\t<td align='center'>{93}</td>\r\n\t\t<td align='center'>{94}</td>\r\n\t\t<td align='center'>{95}</td>\r\n\t\t<td align='center'>{96}</td>\r\n\t\t<td align='center'>{97}</td>\r\n\t</tr>\r\n    <tr >\r\n\t\t<td align='center' style='background-color: #EEE;'>9:30-10:30pm</td>\r\n\t\t<td align='center'>{98}</td>\r\n\t\t<td align='center'>{99}</td>\r\n\t\t<td align='center'>{100}</td>\r\n\t\t<td align='center'>{101}</td>\r\n\t\t<td align='center'>{102}</td>\r\n\t\t<td align='center'>{103}</td>\r\n\t\t<td align='center'>{104}</td>\r\n\t</tr>\r\n</table>\r\n<br />\r\n<b>Student:</b> {105}<br />\r\n<b>Session</b>: {106}\r\n</body>\r\n</html>")]
		SETTING_Courses_TimetableDisplayHtmlTemplate = 99731,
		// Token: 0x0400041C RID: 1052
		[OldUserSetting("Timetable display - day start time (minutes after midnight)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Courses, DefaultValueInt = 450)]
		SETTING_Courses_TimetableDisplayStartMinutes = 99730,
		// Token: 0x0400041D RID: 1053
		[OldUserSetting("When exporting the test list for a specific day to Excel, use whatever filter is currently applied to the list", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams)]
		SETTING_Exams_UseFilterWhenExportToExcelForASpecificDay = 99729,
		// Token: 0x0400041E RID: 1054
		[OldUserSetting("If set to true, accommodation letters will be shown in PDF format when the template is a Word file.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_ShowWordFilesAsPdf = 99728,
		// Token: 0x0400041F RID: 1055
		[OldUserSetting("Use old method of generating Microsoft Word files.  This should only be used if there are problems with generating Word files.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System)]
		SETTING_UseOldMethodToGenerateWordFiles = 99727,
		// Token: 0x04000420 RID: 1056
		[Obsolete("Setting has been discontinued because option to create Word note on appointments has been removed due to it's redundancy.  Create Word note is the same as Generate-Document")]
		[OldUserSetting("Allow staff to create Microsoft Word note on appointments (recommended not to use)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, IsHidden = true)]
		SETTING_AllowStaffToCreateWordNoteOnAppointment = 99726,
		// Token: 0x04000421 RID: 1057
		[OldUserSetting("Notify staff custom email subject line", eOldUserSettingInputType.text, eOldUserSettingGroup.Students)]
		SETTING_NotifyStaff_CustomSubject = 99725,
		// Token: 0x04000422 RID: 1058
		[OldUserSetting("For generating exam sheets, generate a separate Word file for each group of exams (the number here is the number of exams for each file).  Default is 100", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Exams, SettingLevel = eSettingLevel.Advanced)]
		SETTING_Tests_GenerateExamSheets_MaxExamsPerWordFile = 99724,
		// Token: 0x04000423 RID: 1059
		[OldUserSetting("Time format for test booking system (ex. h:mm tt, or H:mm)", eOldUserSettingInputType.text, eOldUserSettingGroup.Exams)]
		SETTING_Tests_TimeFormat = 99723,
		// Token: 0x04000424 RID: 1060
		[OldUserSetting("Date format for test booking system (ex. MMMM d, yyyy or yyyy-MM-dd)", eOldUserSettingInputType.text, eOldUserSettingGroup.Exams)]
		SETTING_Tests_DateFormat = 99722,
		// Token: 0x04000425 RID: 1061
		[OldUserSetting("Which additional seat groups to show for selecting a seat", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Exams)]
		SETTING_Tests_RoomGroupsToShow = 99721,
		// Token: 0x04000426 RID: 1062
		[OldUserSetting("Hide the show time as drop-list", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_Appointment_HideShowTimeAs = 99720,
		// Token: 0x04000427 RID: 1063
		[OldUserSetting("Counsellor signature control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_CounsellorSignature_controlid = 99719,
		// Token: 0x04000428 RID: 1064
		[OldUserSetting("Transparency level (0-255) for appointments (set to 0 to disable transparency)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_Appointments_TransparentAppointmentAlphaValue = 99718,
		// Token: 0x04000429 RID: 1065
		[OldUserSetting("Default rate of pay for new sittings.", eOldUserSettingInputType.text, eOldUserSettingGroup.Exams)]
		SETTING_Tests_DefaultSittingRateOfPay = 99717,
		// Token: 0x0400042A RID: 1066
		[OldUserSetting("Only allow staff to fill out the accommodations form for a student if at least one of the (Per Student Form) fields specified has been filled in for that student.  This can be pointed to the disability fields to ensure the student's disability has been filled out prior to accommodations being entered.", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_OnlyAllowStaffToFillOutAccommodationsFormIfAtLeastOnePSFieldHasBeenFilledIn_cids = 99716,
		// Token: 0x0400042B RID: 1067
		[OldUserSetting("Report id for looking up student by first or last name using the 'Lookup student' button in the intake form.", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_reportId_lookupStudentByFirstOrLastName = 99715,
		// Token: 0x0400042C RID: 1068
		[OldUserSetting("Per appointment fields to show on appointments", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Appointments)]
		SETTING_LoadAppointmentsPerAppCids = 99714,
		// Token: 0x0400042D RID: 1069
		[OldUserSetting("Rec Centre default per appointment form", eOldUserSettingInputType.numberArray_screennum_perapp, eOldUserSettingGroup.Forms, IsHidden = true)]
		SETTING_RecCentre_PerAppForm = 99712,
		// Token: 0x0400042E RID: 1070
		[OldUserSetting("Display template for course name (0=subject;1=coursecode;2=section;3=timeofday;4=startdate;5=enddate;6=duration;7=term;)", eOldUserSettingInputType.text, eOldUserSettingGroup.Courses, DefaultValueString = "{0} {1} {3} section {2}")]
		SETTING_Course_DisplayTemplate = 99710,
		// Token: 0x0400042F RID: 1071
		[OldUserSetting("ClockWork server url", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_ClockWorkServerUrl = 99709,
		// Token: 0x04000430 RID: 1072
		[OldUserSetting("End of day time for all rooms", eOldUserSettingInputType.text, eOldUserSettingGroup.Exams)]
		SETTING_Tests_EndOfDayTimeForAllRooms = 99708,
		// Token: 0x04000431 RID: 1073
		[OldUserSetting("Reports to publish for the exam booking module (will show up in the action panel on the right)", eOldUserSettingInputType.numberArray_reportids, eOldUserSettingGroup.Exams, IsHidden = true)]
		SETTING_ExamBookingReportIds = 99706,
		// Token: 0x04000432 RID: 1074
		[OldUserSetting("Use the ClockWork server.  The ClockWork server should be installed and configured before this is turned on.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System)]
		SETTING_UseClockWorkServer = 99705,
		// Token: 0x04000433 RID: 1075
		[OldUserSetting("Email method for accommodation letters (ex. smtp)", eOldUserSettingInputType.text, eOldUserSettingGroup.Accommodations)]
		SETTING_AccommodationLettersEmailMethod = 99704,
		// Token: 0x04000434 RID: 1076
		[OldUserSetting("On the appointment edit dialog, show the first per appointment form on the 'Multiple dates' (recurring) tab, instead of on it's own tab.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, SettingLevel = eSettingLevel.Advanced)]
		SETTING_AppointmentEdit_ShowFirstPerAppFormOnMultipleDatesTabInstead = 99703,
		// Token: 0x04000435 RID: 1077
		[OldUserSetting("Rec centre master table formatting rules (` separated). [colname],[conditiontype],[conditionvalue],[backcolour],[forecolour].  Example: cancelled,Equal,True,MistyRose,Black`Status,Equal,Tentative,LightGreen,Black", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms, IsHidden = true)]
		SETTING_RecCentreMasterTableCustomFormattingRules = 99702,
		// Token: 0x04000436 RID: 1078
		[OldUserSetting("List of controlids of per appointment data to show in the master Rec Centre table list as columns.  Leaving this blank will include all available data fields.", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Forms, IsHidden = true)]
		SETTING_RecCentrePerAppDataCidsToShowInTable = 99701,
		// Token: 0x04000437 RID: 1079
		[OldUserSetting("Grouped personids.  List of comma-separated person ids (first personid is primary).  Separate groups with `.  Anytime a user books one of the people or rooms in the specified groups, other rooms/people in the list will be automatically added to the attendees list.", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments)]
		SETTING_GroupedPids = 99700,
		// Token: 0x04000438 RID: 1080
		[OldUserSetting("Show a summary of a student's disability information at the top of their accommodations summary (provide the screen number of the disability form - default is 6)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_ShowDisabilityInfoInAccommodationsSummary_Screennum = 99699,
		// Token: 0x04000439 RID: 1081
		[OldUserSetting("Avery type for Rec Centre Management", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms, DefaultValueString = "5160", IsHidden = true)]
		SETTING_RecCentre_AveryType = 99697,
		// Token: 0x0400043A RID: 1082
		[OldUserSetting("Rec Centre Management Mailing Labels Template", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms, DefaultValueString = "#<attendee>#\r\n#<startdate>#\r\n#<starttime># to #<endtime>#\r\n#<description>#", IsHidden = true)]
		SETTING_RecCentre_MailingLabelTemplate,
		// Token: 0x0400043B RID: 1083
		[OldUserSetting("Don't update LOA issued date when generating letters from the accommodation screen.  This should only be enabled if you are sending the accommodation letter as an email using the ClockWork Smtp send method.  The LOA date issued will be updated when the email is successfully sent out.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_DontUpdateDateLOALastIssuedOnAccommodationsForm = 99696,
		// Token: 0x0400043C RID: 1084
		[OldUserSetting("The screen to use for showing and editing client info in the Rec Centre Management system", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms, DefaultValueString = "1", IsHidden = true)]
		SETTING_RecCentreClientsScreenNum = 99695,
		// Token: 0x0400043D RID: 1085
		[OldUserSetting("The screen number for the Rec Centre Management Generate Document function (used for editing mail-merge variable values before printing)", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms, IsHidden = true)]
		SETTING_RecCentreVariablesScreenNum = 99694,
		// Token: 0x0400043E RID: 1086
		[OldUserSetting("Show Rec Centre Management for bookings and clients", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms, IsHidden = true)]
		SETTING_RecCentreShow = 99693,
		// Token: 0x0400043F RID: 1087
		[OldUserSetting("Service provider types that match up by course", eOldUserSettingInputType.text, eOldUserSettingGroup.Accommodations, DefaultValueString = "128")]
		SETTING_ServiceProviders_ServiceProviderTypesMatchingByCourses = 99692,
		// Token: 0x04000440 RID: 1088
		[OldUserSetting("Show the 'Requires notetaker' checkbox on the courses screen even when the Service Provider system is turned on.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_ShowRequiresNotetakerOnCoursesScreenEvenIfServiceProvidersIsTurnedOn = 99691,
		// Token: 0x04000441 RID: 1089
		[OldUserSetting("Workshop form title", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments)]
		SETTING_Appointments_PJATabTitle = 99690,
		// Token: 0x04000442 RID: 1090
		[OldUserSetting("Show workshop form (pja) as separate tab instead of below attendees list.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_Appointments_ShowPJAAsTab = 99689,
		// Token: 0x04000443 RID: 1091
		[OldUserSetting("For users who are not allowed to delete appointments, you can provide a list of appointment types here that will override the 'no-delete' permissions and allow them to delete only those appointment types specified.", eOldUserSettingInputType.numberArray_apptypeids, eOldUserSettingGroup.Unknown)]
		SETTING_OverrideDeleteAppointmentNoPermissions_AppIds = 99688,
		// Token: 0x04000444 RID: 1092
		[OldUserSetting("Show Case Chooser on appointment edit form", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_ShowCaseChooserOnAppointmentEdit = 99687,
		// Token: 0x04000445 RID: 1093
		[OldUserSetting("Use the kiosk plugin labelled 'kioskimport2' instead of the default kioskimport settings", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_UseKiosk2 = 99686,
		// Token: 0x04000446 RID: 1094
		[OldUserSetting("Reports to make available on the Manage Service Providers page", eOldUserSettingInputType.numberArray_reportids, eOldUserSettingGroup.Students)]
		SETTING_ServiceProviderReportNumbers = 99685,
		// Token: 0x04000447 RID: 1095
		[OldUserSetting("The report that returns active students", eOldUserSettingInputType.numberArray_reportids, eOldUserSettingGroup.System)]
		SETTING_ActiveStudentReportNumber = 99684,
		// Token: 0x04000448 RID: 1096
		[OldUserSetting("Only show students / courses who have had their Letter of Accommodations issued in the 'Un-booked students' tab", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams, DefaultValueInt = 1)]
		SETTING_Tests_OnlyShowLetterIssued = 99683,
		// Token: 0x04000449 RID: 1097
		[OldUserSetting("Allow users to delete students on summary management screens", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_SummaryView_AllowDeleteStudent = 99682,
		// Token: 0x0400044A RID: 1098
		[OldUserSetting("Allow users to remove per data items", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_PerData_AllowRemovePmInfoItem = 99681,
		// Token: 0x0400044B RID: 1099
		[OldUserSetting("Enable service provider request 'parts'.  This allows you to split a service provider request into multiple sections.  For example, a student requires a notetaker for a course with 3 lectures per week - you can split the request into 3 parts and this will allow you to assign a different notetaker for each lecture day of the week.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_ServiceProviders_EnableRequestParts = 99680,
		// Token: 0x0400044C RID: 1100
		[OldUserSetting("SETTING_Chatter3_MessageHashPassword", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_Chatter3_MessageHashPassword = 99679,
		// Token: 0x0400044D RID: 1101
		[OldUserSetting("Chatter 3 - Server port", eOldUserSettingInputType.numeric, eOldUserSettingGroup.System, IsHidden = true)]
		SETTING_Chatter3_ServerPort = 99678,
		// Token: 0x0400044E RID: 1102
		[OldUserSetting("Chatter 3 - Server ip address", eOldUserSettingInputType.text, eOldUserSettingGroup.System, IsHidden = true)]
		SETTING_Chatter3_ServerIp = 99677,
		// Token: 0x0400044F RID: 1103
		[OldUserSetting("Workshop screen on appointments height (in pixels)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_Appointment_pjaPDataHeight = 99676,
		// Token: 0x04000450 RID: 1104
		[OldUserSetting("Show workshop screen for all appointment types (not just workshops)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_Appointment_showPjaForAllAppTypes = 99675,
		// Token: 0x04000451 RID: 1105
		[OldUserSetting("Should template accommodations be looked at when deciding which students to show in the list (if no then only course specific accommodations will be looked at)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams)]
		SETTING_Tests_IncludeTemplateAccommodationsWhenDecidingWhichStudentsToShow = 99674,
		// Token: 0x04000452 RID: 1106
		[OldUserSetting("Control ids from the instructor test form that should be shown as columns in the tests master listing", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Exams, SettingLevel = eSettingLevel.Advanced)]
		SETTING_Tests_InstructorFormCidsToShowInMasterList = 99673,
		// Token: 0x04000453 RID: 1107
		[OldUserSetting("Control ids from the accommodations form that should be shown as columns in the tests master listing", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Exams, SettingLevel = eSettingLevel.Advanced)]
		SETTING_Tests_AccommodationCidsToShowInMasterList = 99672,
		// Token: 0x04000454 RID: 1108
		[OldUserSetting("Assigned counsellor control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_AssignedCounsellorCid = 99671,
		// Token: 0x04000455 RID: 1109
		[OldUserSetting("When auto-saving generated accommodation letters, use the student's name as the organizing folder instead of the term.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_UseStudentNameFoldersInsteadOfSessionFoldersWhenAutoSavingGeneratedLetters = 99670,
		// Token: 0x04000456 RID: 1110
		[OldUserSetting("Controls to hide from course tab forms.", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_CidsToHideFromCourseTabsOnly = 99667,
		// Token: 0x04000457 RID: 1111
		[OldUserSetting("Hide the 'Offline' tab.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_HideOfflineTab = 99666,
		// Token: 0x04000458 RID: 1112
		[OldUserSetting("All courses are un-checked by default when Generating an Accommodation Letter", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_GenerateAccommodationsLetter_CoursesAllUncheckedByDefault = 99665,
		// Token: 0x04000459 RID: 1113
		[OldUserSetting("For users who are not allowed to view other's schedules (in the permissions section), this is a list of people that are overrides (ie. they can see these people schedules in full)", eOldUserSettingInputType.numberArray, eOldUserSettingGroup.Users)]
		SETTING_PERMISSIONS_viewOthersSchedule_overridePids = 99660,
		// Token: 0x0400045A RID: 1114
		[OldUserSetting("Report - Mailing label dynamic controls", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Users)]
		SETTING_Reports_MailingLabels_cids = 99659,
		// Token: 0x0400045B RID: 1115
		[OldUserSetting("Report - Mailing label 1 checkbox control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Users)]
		SETTING_Reports_MailingLabels_Label1ChkCid = 99658,
		// Token: 0x0400045C RID: 1116
		[OldUserSetting("Report - Mailing label 2 checkbox control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Users)]
		SETTING_Reports_MailingLabels_Label2ChkCid = 99657,
		// Token: 0x0400045D RID: 1117
		[OldUserSetting("Report - Mailing label 1 template", eOldUserSettingInputType.textBig, eOldUserSettingGroup.Users)]
		SETTING_Reports_MailingLabels_Label1Template = 99655,
		// Token: 0x0400045E RID: 1118
		[OldUserSetting("Report - Mailing label 2 template", eOldUserSettingInputType.textBig, eOldUserSettingGroup.Users)]
		SETTING_Reports_MailingLabels_Label2Template,
		// Token: 0x0400045F RID: 1119
		[OldUserSetting("If only allow staff in the appointment to see the assessments, should this apply to textboxes only?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_OnlyAllowPeopleInTheAppointmentToSeeAssessmentsTextBoxesOnly = 99654,
		// Token: 0x04000460 RID: 1120
		[OldUserSetting("Only allow staff in the appointment to see the assessments", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_OnlyAllowPeopleInTheAppointmentToSeeAssessments = 99653,
		// Token: 0x04000461 RID: 1121
		[OldUserSetting("Availability schedule: Which availability group id to edit?", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments, DefaultValueString = "3")]
		SETTING_AvailabilitySchedule_AvailabilityGroupIdToEdit = 99652,
		// Token: 0x04000462 RID: 1122
		[OldUserSetting("Availability schedule: Which groups of rooms to allow assigning for availabilities?", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Appointments)]
		SETTING_AvailabilitySchedule_RoomGroupIdsToEdit = 99651,
		// Token: 0x04000463 RID: 1123
		[OldUserSetting("Availability schedule: Which groups of users to allow editing availability schedule for?", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Appointments)]
		SETTING_AvailabilitySchedule_PersonGroupIdsToEdit = 99650,
		// Token: 0x04000464 RID: 1124
		[OldUserSetting("Hide Notify staff", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons, DefaultValueInt = 1)]
		SETTING_ButtonHide_NotifyStaff = 99649,
		// Token: 0x04000465 RID: 1125
		[OldUserSetting("Read only screen nums", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms)]
		SETTING_ReadOnlyScreenNums = 99647,
		// Token: 0x04000466 RID: 1126
		[OldUserSetting("Hidden forms (all fields on the form(s) will be hidden)", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Users)]
		SETTING_InvisibleScreenNums,
		// Token: 0x04000467 RID: 1127
		[OldUserSetting("Mail-merge template for the tests/exams label generator.", eOldUserSettingInputType.text, eOldUserSettingGroup.Exams)]
		SETTING_TestBookingMailingLabels_Template = 99646,
		// Token: 0x04000468 RID: 1128
		[OldUserSetting("List of groups (containing users) that can be selected from when editing room availability schedules.", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Appointments, DefaultValueString = "1")]
		SETTING_AvailabilitySchedule_PersonGroupIdsToChooseFromWhenEditingAvailabilityScheduleForRooms = 99645,
		// Token: 0x04000469 RID: 1129
		[OldUserSetting("Accommodations Last primary attachment", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown)]
		SETTING_ACCOMMODATIONS_LastPrimary_Attach = 99644,
		// Token: 0x0400046A RID: 1130
		[OldUserSetting("Additional Tools options (specify a newline delimitered list of MENUITEMNAME=PATHANDFILENAMEOFEXECUTABLETOLAUNCH).", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_ToolsAdditionalOptions = 99643,
		// Token: 0x0400046B RID: 1131
		[OldUserSetting("Specify a code that will determine the default options that are available when ClockWork starts up.  This is intended to make it quick to setup different types of departments (for example, a counselling department doesn't need several of the disability related options).", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_ClockWorkTemplateCode = 99642,
		// Token: 0x0400046C RID: 1132
		[OldUserSetting("Accommodation control ids that are only available on the template tab (i.e. not available for individual courses)", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_ControlIdsOnlyForTemplate = 99641,
		// Token: 0x0400046D RID: 1133
		[OldUserSetting("For the Notify Staff button, the default from address to be used when sending out emails directly through smtp.", eOldUserSettingInputType.text, eOldUserSettingGroup.Students)]
		SETTING_NotifyStaff_DefaultFromAddress = 99640,
		// Token: 0x0400046E RID: 1134
		[OldUserSetting("For the Notify Staff button, send emails directly through smtp using the smtp settings.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_NotifyStaff_SendEmailDirectThroughSmtp = 99639,
		// Token: 0x0400046F RID: 1135
		SETTING_NotifyStaff_IncludeStudentFullNameInEmailBody = 99638,
		// Token: 0x04000470 RID: 1136
		[OldUserSetting("For the Notify Staff button, what is the default to address for each screennum.  Use a newline separated list of screennum=toaddress.", eOldUserSettingInputType.text, eOldUserSettingGroup.Students)]
		SETTING_NotifyStaff_DefaultToEmailAddressesByScreenNum = 99637,
		// Token: 0x04000471 RID: 1137
		[Obsolete("No longer active")]
		[OldUserSetting("Use the old tests screen in ClockWork for staff (change to false to use the new tests screen).", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams, DefaultValueInt = 1, IsHidden = true)]
		SETTING_UseOldTestScreen = 99636,
		// Token: 0x04000472 RID: 1138
		[OldUserSetting("Allowed to approve accommodations ('Tools-Approve Accommodations' in the main menu)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_AllowedToApproveAccommodations = 99635,
		// Token: 0x04000473 RID: 1139
		[OldUserSetting("Enforce the accommodations approval process (staff will not be allowed to change the approved status of accommodations, and they will only be allowed to request a change for accommodations that have already been approved).", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, SettingLevel = eSettingLevel.Advanced)]
		SETTING_Accommodations_EnforceAccommodationApprovalProcess = 99634,
		// Token: 0x04000474 RID: 1140
		[OldUserSetting("Which view to use to match up equivalent courses for Service Providers? (leave blank for default, otherwise 2, 3, etc.)", eOldUserSettingInputType.text, eOldUserSettingGroup.Students)]
		SETTING_ServiceProviders_EquivalentCourseMatchingNum = 99633,
		// Token: 0x04000475 RID: 1141
		[OldUserSetting("Don't allow users to change the student's middle name", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users)]
		SETTING_DisAllowChangingStudentMiddlename = 99632,
		// Token: 0x04000476 RID: 1142
		[OldUserSetting("Don't allow users to change the student's last name", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users)]
		SETTING_DisAllowChangingStudentLastname = 99631,
		// Token: 0x04000477 RID: 1143
		[OldUserSetting("Don't allow users to change the student's first name", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users)]
		SETTING_DisAllowChangingStudentFirstname = 99630,
		// Token: 0x04000478 RID: 1144
		[OldUserSetting("Don't allow users to change the student's student-number", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users)]
		SETTING_DisAllowChangingStudentNumber = 99629,
		// Token: 0x04000479 RID: 1145
		[OldUserSetting("Allow staff to view a student's calendar by choosing the student's name and clicking the 'View schedule' button in the main ClockWork software", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_AllowViewingStudentCalendar = 99628,
		// Token: 0x0400047A RID: 1146
		[OldUserSetting("The url of the ClockWork web modules", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_WebBaseUrl = 99627,
		// Token: 0x0400047B RID: 1147
		[OldUserSetting("What email method should be used for sending out emails? (examples: Outlook)", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_EmailMethod = 99626,
		// Token: 0x0400047C RID: 1148
		[OldUserSetting("Time format for test spreadsheet view [ex: h:mm tt]", eOldUserSettingInputType.text, eOldUserSettingGroup.Exams)]
		SETTING_Tests_TimeFormatForList = 99625,
		// Token: 0x0400047D RID: 1149
		[OldUserSetting("Auto Service Provider Request (automatically create a service request entry based on a checked accommodation).  Use a newline separated list of controlid=serviceprovidertypecode", eOldUserSettingInputType.text, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_AutoServiceProviderRules_CidEqualsServiceProviderTypeCode = 99624,
		// Token: 0x0400047E RID: 1150
		[OldUserSetting("Exempt personids (these students are inactive but will appear in ClockWork anyway - they can be used as test students and won't show in reports)", eOldUserSettingInputType.text, eOldUserSettingGroup.Students)]
		SETTING_ExemptPids = 99622,
		// Token: 0x0400047F RID: 1151
		[OldUserSetting("Override hidden control ids (show these controls even if they're marked hidden somewhere else)", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Users)]
		SETTING_OverrideVisibleCids = 99621,
		// Token: 0x04000480 RID: 1152
		[OldUserSetting("Override read-only control ids (allow user to edit data for these controls even if they're marked read-only somewhere else)", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Users)]
		SETTING_OverrideEditableCids = 99620,
		// Token: 0x04000481 RID: 1153
		[OldUserSetting("Service Providers: Service Types.", eOldUserSettingInputType.text, eOldUserSettingGroup.Students, DefaultValueString = "1=Interpreter,2=Teamer,3=Professional notetaker,4=Coach,5=Specialized tutor,6=Real time captioner,7=Peer assistant,8=Peer notetaker,9=Peer tutor")]
		SETTING_ServiceProviders_ServiceTypeDescriptions = 99614,
		// Token: 0x04000482 RID: 1154
		[OldUserSetting("Use the 'Execute Sql' feature in the main menu of the main ClockWork software", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users)]
		SETTING_UseExecuteSqlInMainClockWorkSoftware = 99613,
		// Token: 0x04000483 RID: 1155
		[OldUserSetting("Override Dynamic Forms panel colours", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_Forms_OverridePanelColourEnabled = 99612,
		// Token: 0x04000484 RID: 1156
		[OldUserSetting("Override Dynamic Forms panel foreground colour (ignore the foreground colour for all group boxes in the forms editor and use this one instead)", eOldUserSettingInputType.Colour, eOldUserSettingGroup.Forms, DefaultValueInt = 0)]
		SETTING_Forms_OverridePanelForegroundColour = 99611,
		// Token: 0x04000485 RID: 1157
		[OldUserSetting("Override Dynamic Forms panel background colour (ignore the background colour for all group boxes in the forms editor and use this one instead)", eOldUserSettingInputType.Colour, eOldUserSettingGroup.Forms, DefaultValueInt = 0)]
		SETTING_Forms_OverridePanelBackgroundColour = 99610,
		// Token: 0x04000486 RID: 1158
		[OldUserSetting("Instance colour (colour will show in the main toolbar at the top to indicate which ClockWork database is currently being used.", eOldUserSettingInputType.Colour, eOldUserSettingGroup.System, DefaultValueInt = 16777215, IsHidden = true)]
		SETTING_InstanceColour = 99598,
		// Token: 0x04000487 RID: 1159
		[Obsolete]
		[OldUserSetting("Use 'Sub Title' field for appointments.  This is an editable text field that will allow the user to type any text in - it will be displayed beside the main title on the appointment.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, IsHidden = true)]
		SETTING_Appointment_UseSubTitle = 99600,
		// Token: 0x04000488 RID: 1160
		[OldUserSetting("Reports: Allowed to create new reports or edit existing reports?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users, DefaultValueInt = 1)]
		SETTING_Reports_CreateOrEditReports = 99597,
		// Token: 0x04000489 RID: 1161
		[OldUserSetting("Reports: Available report group ids", eOldUserSettingInputType.numberArray, eOldUserSettingGroup.Users)]
		SETTING_Reports_AvailableReportGroups = 99596,
		// Token: 0x0400048A RID: 1162
		[OldUserSetting("Data Sync Report: Batch data sync", eOldUserSettingInputType.numeric, eOldUserSettingGroup.System)]
		SETTING_DataSync_BatchImportReportId = 99595,
		// Token: 0x0400048B RID: 1163
		[OldUserSetting("Data Sync report id for the report that moves the data into the ClockWork tables (custom_data and custom_courses)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Unknown)]
		SETTING_DataSync_MoveDataIntoClockWorkReportid = 99594,
		// Token: 0x0400048C RID: 1164
		[OldUserSetting("Show other students names in the per app screen for any student (other students in the same appointment)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_ShowOtherStudentsNamesInPerAppScreen = 99593,
		// Token: 0x0400048D RID: 1165
		[OldUserSetting("Which per date forms should be read-only after they're initially created? (note: only for per-date type forms)", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms)]
		SETTING_PerDataFormsToLockForEditing = 99592,
		// Token: 0x0400048E RID: 1166
		[OldUserSetting("The default view a user sees when ClockWork starts up.", eOldUserSettingInputType.text, eOldUserSettingGroup.Users)]
		SETTING_DefaultViewDefinition = 99591,
		// Token: 0x0400048F RID: 1167
		[OldUserSetting("Disable the 'Last no-show warning' popup.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_DisableLastAppointmentNoShowWarningPopup = 99590,
		// Token: 0x04000490 RID: 1168
		[OldUserSetting("Disable the accommodations 'Template' tab.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_AccommodationsDisableTemplateTab = 99589,
		// Token: 0x04000491 RID: 1169
		[OldUserSetting("Updating Mode (how the updates will be performed - 0 means normal, 1 means close ClockWork and launch the updater as a process)", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_UpdateMode = 99538,
		// Token: 0x04000492 RID: 1170
		[OldUserSetting("Availability schedule - end time", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments, Example = "17:00")]
		SETTING_AvailabilitySchedule_EndTime = 99537,
		// Token: 0x04000493 RID: 1171
		[OldUserSetting("Availability schedule - start time", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments, Example = "17:00")]
		SETTING_AvailabilitySchedule_StartTime = 99536,
		// Token: 0x04000494 RID: 1172
		[OldUserSetting("Availability schedule - show a time label every x minutes", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_AvailabilitySchedule_LabelEveryXMinutes = 99535,
		// Token: 0x04000495 RID: 1173
		[OldUserSetting("Availability schedule - minutes per cell", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_AvailabilitySchedule_MinutesPerCell = 99534,
		// Token: 0x04000496 RID: 1174
		[OldUserSetting("Show Service Providers", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ShowServiceProviders = 99533,
		// Token: 0x04000497 RID: 1175
		[OldUserSetting("Show mini new student popup when adding a new student from within the appointment edit popup.  Specify at least one field here.", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Appointments)]
		SETTING_AppointmentEditShowMiniNewStudentCids = 99532,
		// Token: 0x04000498 RID: 1176
		[OldUserSetting("Hide course tabs on accommodations form", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_AccommodationsHideCourseTabs = 99531,
		// Token: 0x04000499 RID: 1177
		[OldUserSetting("Snap-to times when booking new appointments (comma-separated list, will snap to the closest time to the pixel the user double-clicked on)", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments)]
		SETTING_SCHEDULER_snapToTimesForNewApps = 99530,
		// Token: 0x0400049A RID: 1178
		[OldUserSetting("Lock docking panels by default (user won't be able to move them around - they can right-click to unlock)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users)]
		SETTING_DockingPanelsLockedByDefault = 99529,
		// Token: 0x0400049B RID: 1179
		[OldUserSetting("My students load method (how to decide which students belong to a particular counsellor/advisor - use form:controlid for assigned advisor)", eOldUserSettingInputType.text, eOldUserSettingGroup.Users, DefaultValueString = "appointments")]
		SETTING_MyStudentsLoadMethod = 99528,
		// Token: 0x0400049C RID: 1180
		[OldUserSetting("Disability clients group", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Students)]
		SETTING_DisabilityClientGroupId = 99527,
		// Token: 0x0400049D RID: 1181
		[OldUserSetting("Last appointment type group selected", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Unknown)]
		SETTING_LastAppTypeGroupSelected = 99526,
		// Token: 0x0400049E RID: 1182
		[OldUserSetting("Appointment background colour based on staff attendees (appointments with multiple staff attendees will use default appointment colour)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_SCHEDULER_AppointmentBackgroundColourBasedOnStaffAttendee = 99525,
		// Token: 0x0400049F RID: 1183
		[OldUserSetting("Calendar: is graphical or text view preferred?", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments, DefaultValueString = "graphical")]
		SETTING_SCHEDULER_GraphicalOrTextPreference = 99524,
		// Token: 0x040004A0 RID: 1184
		[OldUserSetting("Disable admin ability to login as any other user", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown)]
		SETTING_DisableAdminLoginAsAnyone = 99523,
		// Token: 0x040004A1 RID: 1185
		[OldUserSetting("Warn the user when they Save a test if the student or room is double-booked, or if the same student is writing the same course twice in the same day.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams)]
		SETTING_Tests_WarnIfDoubleBookingStudentOrCourseOrRoomOnSave = 99522,
		// Token: 0x040004A2 RID: 1186
		[OldUserSetting("Suppress the warning message when a test is being booked without a room", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams)]
		SETTING_SuppressNoRoomWarningForTestBookings = 99521,
		// Token: 0x040004A3 RID: 1187
		[OldUserSetting("Use 'Actual start time / actual end time' on appointments", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Exams)]
		SETTING_Appointment_UseActualStartEndTimes = 99520,
		// Token: 0x040004A4 RID: 1188
		[OldUserSetting("Language 2 description", eOldUserSettingInputType.text, eOldUserSettingGroup.System)]
		SETTING_Language2Description = 99519,
		// Token: 0x040004A5 RID: 1189
		[OldUserSetting("Language 1 description", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "English")]
		SETTING_Language1Description = 99518,
		// Token: 0x040004A6 RID: 1190
		[OldUserSetting("Show the groups a student belongs to when the active student is changed (in the lower right corner)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_ShowStudentGroupsOnSetActiveStudent = 99517,
		// Token: 0x040004A7 RID: 1191
		[OldUserSetting("Use the new notetaking system where a notetaker is assigned to each notetakee and course, not just to a course, and different types of notetakers are available.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, DefaultValueInt = 1, IsHidden = true)]
		SETTING_UseNotetaking2 = 99516,
		// Token: 0x040004A8 RID: 1192
		[OldUserSetting("Form to use in 'Student File' screen (use 0 or blank to disable 'Student File' screen)", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms, SettingLevel = eSettingLevel.TechnoProOnly)]
		SETTING_StudentFile_ScreenNumToUse = 99515,
		// Token: 0x040004A9 RID: 1193
		[OldUserSetting("Show first names on student appointments", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.PersonalOptions)]
		SETTING_Scheduler_ShowStudentFirstNamesOnAppointments = 99514,
		// Token: 0x040004AA RID: 1194
		[OldUserSetting("Show appointment type last on appointments", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.PersonalOptions)]
		SETTING_Scheduler_DisplayAppointmentTypeLastOnAppointments = 99513,
		// Token: 0x040004AB RID: 1195
		[OldUserSetting("Reports the user can run through the main ClockWork software", "Reports will be listed in the 'Reports' main menu item (in ClockWork) that will only appear if the user has at least one report available to them through this setting.", eOldUserSettingInputType.numberArray_reportids, eOldUserSettingGroup.Users)]
		SETTING_AvailableReports = 99512,
		// Token: 0x040004AC RID: 1196
		[OldUserSetting("Data fields that should be read-only for the user.", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Users)]
		SETTING_ReadOnlyCids = 99511,
		// Token: 0x040004AD RID: 1197
		[OldUserSetting("Data fields that should be hidden from the user.", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Users)]
		SETTING_HiddenCids = 99510,
		// Token: 0x040004AE RID: 1198
		[OldUserSetting("Show staff list as first-name then last-name", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.PersonalOptions)]
		SETTING_StaffNamesInDropLists_ShowFirstNameThenLastName = 99509,
		// Token: 0x040004AF RID: 1199
		[OldUserSetting("Don't ever add the user to new appointments automatically (even if double-clicking on their schedule).", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.PersonalOptions)]
		SETTING_Appointment_DontEverAddMeToNewAppointments = 99508,
		// Token: 0x040004B0 RID: 1200
		[OldUserSetting("Automatically set the appointment type for new appointments to the last appointment type booked.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_Appointment_RememberLastAppTypeIdUsed = 99507,
		// Token: 0x040004B1 RID: 1201
		[OldUserSetting("Messaging group name (users can only see other users in the same group)", eOldUserSettingInputType.text, eOldUserSettingGroup.Unknown, DefaultValueString = "ClockWork")]
		SETTING_ChatterQueuName = 99506,
		// Token: 0x040004B2 RID: 1202
		[OldUserSetting("Course chooser all caps fields (a comma separated list of fields that should be all caps [term,duration,subject,course,timeofday,section,instructor]", eOldUserSettingInputType.text, eOldUserSettingGroup.Accommodations)]
		SETTING_CourseChooser_AllCaps_TermDurationSubjectCourseTimeOfDaySectionInstructor = 99505,
		// Token: 0x040004B3 RID: 1203
		[OldUserSetting("Shadow forms (copies of a form that will allow you to approve and move changes to the real form).  Specify realscreennum=shadowscreennum,...", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms)]
		SETTING_ShadowForms = 99504,
		// Token: 0x040004B4 RID: 1204
		[OldUserSetting("Delay between sending accommodation letters to email (in milliseconds)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Accommodations)]
		SETTING_MillisecondDelayBetweenSendingAccommodationLettersToEmail = 99503,
		// Token: 0x040004B5 RID: 1205
		[OldUserSetting("Important Icons (will always appear first)", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments)]
		SETTING_ImportantIcons = 1000,
		// Token: 0x040004B6 RID: 1206
		[OldUserSetting("Include me", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_IncludeMe = 1,
		// Token: 0x040004B7 RID: 1207
		[OldUserSetting("Schedule view", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 2)]
		SETTING_ScheduleView,
		// Token: 0x040004B8 RID: 1208
		[OldUserSetting("Timebar layout", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 2)]
		SETTING_TimebarLayout,
		// Token: 0x040004B9 RID: 1209
		[OldUserSetting("Attendees height", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_attendeesHeight,
		// Token: 0x040004BA RID: 1210
		[OldUserSetting("Auto refresh num seconds", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1800)]
		SETTING_autoRefreshNumSeconds,
		// Token: 0x040004BB RID: 1211
		[OldUserSetting("Schedule visible start time minutes past midnight", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 510)]
		SETTING_scheduleVisibleStartTimeMinutesPastMidnight,
		// Token: 0x040004BC RID: 1212
		[OldUserSetting("Schedule num visible minutes", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 480)]
		SETTING_scheduleNumVisibleMinutes,
		// Token: 0x040004BD RID: 1213
		[OldUserSetting("Schedule show half hour", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_scheduleShowHalfHour,
		// Token: 0x040004BE RID: 1214
		[OldUserSetting("Schedule show hour", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_scheduleShowHour,
		// Token: 0x040004BF RID: 1215
		[OldUserSetting("Schedule minimum person width", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 50)]
		SETTING_scheduleMinimumPersonWidth,
		// Token: 0x040004C0 RID: 1216
		[OldUserSetting("Appointment edit X coordinate", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_appointmentEditX,
		// Token: 0x040004C1 RID: 1217
		[OldUserSetting("Appointment edit Y coordinate", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_appointmentEditY,
		// Token: 0x040004C2 RID: 1218
		[OldUserSetting("Appointment edit width", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_appointmentEditW,
		// Token: 0x040004C3 RID: 1219
		[OldUserSetting("Appointment edit height", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_appointmentEditH,
		// Token: 0x040004C4 RID: 1220
		[OldUserSetting("Appointment edit splitter 1 X coordinate", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_appointmentEditSplitter1X,
		// Token: 0x040004C5 RID: 1221
		[OldUserSetting("Appointment edit attendees width", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_appointmentEditAttendeesWidth,
		// Token: 0x040004C6 RID: 1222
		[OldUserSetting("Schedule show student names on appointments", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_scheduleShowStudentNamesOnAppointments,
		// Token: 0x040004C7 RID: 1223
		[OldUserSetting("Details (left) panel width", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_detailsPanelWidth,
		// Token: 0x040004C8 RID: 1224
		[OldUserSetting("Templates path (main)", eOldUserSettingInputType.text, eOldUserSettingGroup.Misc)]
		SETTING_TemplatesTopDirectory,
		// Token: 0x040004C9 RID: 1225
		[OldUserSetting("Workshop fees template filename only", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "WorkshopFeesTemplate.rtf")]
		SETTING_WorkshopFeesTemplateFilenameOnly,
		// Token: 0x040004CA RID: 1226
		[OldUserSetting("Treat cross listed courses as equivalent", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_TreatCrosslistedAsEquivalent,
		// Token: 0x040004CB RID: 1227
		[OldUserSetting("Notetaker default fee", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 50)]
		SETTING_NotetakerDefaultFee = 50,
		// Token: 0x040004CC RID: 1228
		[OldUserSetting("Email templates path", eOldUserSettingInputType.text, eOldUserSettingGroup.Misc)]
		SETTING_EmailTemplatesTopDirectory = 22,
		// Token: 0x040004CD RID: 1229
		[OldUserSetting("Email SMTP Server name", eOldUserSettingInputType.text, eOldUserSettingGroup.System, IsHidden = true)]
		SETTING_EmailSMTPServer,
		// Token: 0x040004CE RID: 1230
		[OldUserSetting("Student files path (Word)", eOldUserSettingInputType.text, eOldUserSettingGroup.Misc)]
		SETTING_UserStudentFilesDirectory,
		// Token: 0x040004CF RID: 1231
		[OldUserSetting("User student files template filename", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_UserStudentFilesTemplate,
		// Token: 0x040004D0 RID: 1232
		[OldUserSetting("Appointment templates path", eOldUserSettingInputType.text, eOldUserSettingGroup.Misc)]
		SETTING_AppTemplateTemplatesTopDirectory,
		// Token: 0x040004D1 RID: 1233
		[OldUserSetting("App P right width", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 440)]
		SETTING_appPRightWidth,
		// Token: 0x040004D2 RID: 1234
		[OldUserSetting("App P attendees width", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 240)]
		SETTING_appPAttendeesWidth,
		// Token: 0x040004D3 RID: 1235
		[OldUserSetting("App P courses height", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 144)]
		SETTING_appPCoursesHeight,
		// Token: 0x040004D4 RID: 1236
		[OldUserSetting("Schedule details auto hide", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_scheduleDetailsAutoHide,
		// Token: 0x040004D5 RID: 1237
		[OldUserSetting("Schedule details layout", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_scheduleDetailsLayout,
		// Token: 0x040004D6 RID: 1238
		[OldUserSetting("Treat all sections as not equivalent", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_TreatAllSectionsAsNotEquivalent = 40,
		// Token: 0x040004D7 RID: 1239
		[OldUserSetting("Email: Use default email software?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, DefaultValueInt = 0)]
		SETTING_EmailUseDefaultEmailSoftware = 100,
		// Token: 0x040004D8 RID: 1240
		[OldUserSetting("Email: Outgoing Smtp server name", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "127.0.0.1", IsHidden = true)]
		SETTING_EmailOutgoingSmtpServer,
		// Token: 0x040004D9 RID: 1241
		[OldUserSetting("Email: Outgoing Smtp port", eOldUserSettingInputType.numeric, eOldUserSettingGroup.System, DefaultValueInt = 25, IsHidden = true)]
		SETTING_EmailOutgoingSmtpPort,
		// Token: 0x040004DA RID: 1242
		[OldUserSetting("Email: Use SSL?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, DefaultValueInt = 0, IsHidden = true)]
		SETTING_EmailUseSSL,
		// Token: 0x040004DB RID: 1243
		[OldUserSetting("Email: Username", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "", IsHidden = true)]
		SETTING_EmailUserName,
		// Token: 0x040004DC RID: 1244
		[OldUserSetting("Email: Password", eOldUserSettingInputType.password, eOldUserSettingGroup.System, DefaultValueString = "", IsHidden = true)]
		SETTING_EmailUserPassword,
		// Token: 0x040004DD RID: 1245
		[OldUserSetting("Email: Send body as HTML?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, DefaultValueInt = 0, IsHidden = true)]
		SETTING_EmailSendBodyAsHtml,
		// Token: 0x040004DE RID: 1246
		[OldUserSetting("Email: Default from address", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "", IsHidden = false)]
		SETTING_EmailDefaultFromAddress,
		// Token: 0x040004DF RID: 1247
		[OldUserSetting("News url", eOldUserSettingInputType.text, eOldUserSettingGroup.Misc, DefaultValueString = "http://clockworks.ca/#/support")]
		SETTING_NewsUrl = 201,
		// Token: 0x040004E0 RID: 1248
		[OldUserSetting("Test column sizes percent", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1, DefaultValueString = "")]
		SETTING_TestColumnSizesPercent,
		// Token: 0x040004E1 RID: 1249
		SETTING_TestLocked,
		// Token: 0x040004E2 RID: 1250
		[OldUserSetting("Tests screen Filter by start date", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "")]
		SETTING_TestFilterStartDate,
		// Token: 0x040004E3 RID: 1251
		[OldUserSetting("Tests screen Filter by end date", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "")]
		SETTING_TestFilterEndDate,
		// Token: 0x040004E4 RID: 1252
		[OldUserSetting("Tests screen alone name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "")]
		SETTING_TestAloneName,
		// Token: 0x040004E5 RID: 1253
		[OldUserSetting("Tests screen group name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "")]
		SETTING_TestGroupName,
		// Token: 0x040004E6 RID: 1254
		[OldUserSetting("Notetaking notetakee note name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "Note", IsHidden = true)]
		SETTING_NotetakingNotetakeeNoteName,
		// Token: 0x040004E7 RID: 1255
		[OldUserSetting("Notetaker Custom1 name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, IsHidden = true)]
		SETTING_NotetakerCustom1,
		// Token: 0x040004E8 RID: 1256
		[OldUserSetting("Notetaker Custom2 name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, IsHidden = true)]
		SETTING_NotetakerCustom2,
		// Token: 0x040004E9 RID: 1257
		[OldUserSetting("Notetaker Custom3 name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, IsHidden = true)]
		SETTING_NotetakerCustom3,
		// Token: 0x040004EA RID: 1258
		[OldUserSetting("Show 'Save and print' button on appointment edit", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, DefaultValueInt = 0)]
		SETTING_ShowSaveAndPrintOnAppEdit = 250,
		// Token: 0x040004EB RID: 1259
		[OldUserSetting("School postal code lookup url", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms, IsHidden = true)]
		SETTING_SchoolPostalCodeLookupUrl,
		// Token: 0x040004EC RID: 1260
		[OldUserSetting("Postal service lookup url", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms, IsHidden = true)]
		SETTING_CanadaPostPostalCodeLookupUrl,
		// Token: 0x040004ED RID: 1261
		[OldUserSetting("Enable 'Duration' field", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Courses, DefaultValueInt = 0, Description = "Enable use of the 'Duration' field for courses.  The duration field can be renamed using the appropriate setting.")]
		SETTING_CoursesUseDuration,
		// Token: 0x040004EE RID: 1262
		[OldUserSetting("Enable 'Time of day' field", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Courses, DefaultValueInt = 0, Description = "Enable use of the 'Time of day' field for courses.  This field is usually used to store the type of course (eg. Lab, Lec, Tut).  The 'Time of day' field can be renamed using the appropriate setting.")]
		SETTING_CoursesUseTimeOfDay,
		// Token: 0x040004EF RID: 1263
		[OldUserSetting("User favourites", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "")]
		SETTING_UserFavourites = 256,
		// Token: 0x040004F0 RID: 1264
		[OldUserSetting("Hide lock image for encrypted screen data fields", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_HideLockImageForEncryptedDynamicData = 255,
		// Token: 0x040004F1 RID: 1265
		[OldUserSetting("Multiple accommodation templates", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_MultipleAccommodationTemplates = 257,
		// Token: 0x040004F2 RID: 1266
		[OldUserSetting("Use student middlenames", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms, DefaultValueInt = 0)]
		SETTING_UseStudentMiddleNames,
		// Token: 0x040004F3 RID: 1267
		[OldUserSetting("OK to email control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms, DefaultValueInt = -1)]
		SETTING_OkToEmailControlID,
		// Token: 0x040004F4 RID: 1268
		[OldUserSetting("Email control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_EmailControlID,
		// Token: 0x040004F5 RID: 1269
		[OldUserSetting("Courses: Term override name", eOldUserSettingInputType.text, eOldUserSettingGroup.Courses)]
		SETTING_CoursesTermOverrideName,
		// Token: 0x040004F6 RID: 1270
		[OldUserSetting("Courses: Duration override name", eOldUserSettingInputType.text, eOldUserSettingGroup.Courses)]
		SETTING_CoursesDurationOverrideName,
		// Token: 0x040004F7 RID: 1271
		[OldUserSetting("Courses: Time of day override name", eOldUserSettingInputType.text, eOldUserSettingGroup.Courses)]
		SETTING_CoursesTimeOfDayOverrideName,
		// Token: 0x040004F8 RID: 1272
		[OldUserSetting("Minimum ClockWork version", eOldUserSettingInputType.text, eOldUserSettingGroup.System, IsHidden = true)]
		SETTING_MinimumClockWorkVersion,
		// Token: 0x040004F9 RID: 1273
		[OldUserSetting("Icons quick pick", eOldUserSettingInputType.numberArray, eOldUserSettingGroup.Appointments)]
		SETTING_IconsQuickPick,
		// Token: 0x040004FA RID: 1274
		[OldUserSetting("Forms to show buttons for", eOldUserSettingInputType.text, eOldUserSettingGroup.Users, DefaultValueString = "")]
		SETTING_ScreensToShowButtonsFor,
		// Token: 0x040004FB RID: 1275
		[OldUserSetting("Generate 1 accommodation letter for all courses (No means the generated Word file will contain a page for each course)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, IsHidden = true)]
		SETTING_GenerateAccommodationLetterIgnoreCourses,
		// Token: 0x040004FC RID: 1276
		[OldUserSetting("Ungrouped appointment type Group Name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "Ungrouped")]
		SETTING_UngroupedAppTypeGroupName,
		// Token: 0x040004FD RID: 1277
		[OldUserSetting("Allow blank student numbers", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_AllowBlankStudentNumbers,
		// Token: 0x040004FE RID: 1278
		[OldUserSetting("Messaging system enabled", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, IsHidden = true)]
		SETTING_EnableRealTimeUpdates,
		// Token: 0x040004FF RID: 1279
		[OldUserSetting("Student waiting icon index", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 102)]
		SETTING_StudentWaitingIconIndex,
		// Token: 0x04000500 RID: 1280
		[OldUserSetting("Problem with date time picker custom format", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_ProblemWithDateTimePickerCustomFormat,
		// Token: 0x04000501 RID: 1281
		[OldUserSetting("Messaging: My icon", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = -1)]
		SETTING_MESSAGING_MyIcon,
		// Token: 0x04000502 RID: 1282
		[OldUserSetting("Messaging: Instant messaging enabled", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_MESSAGING_InstantMessagingEnabled,
		// Token: 0x04000503 RID: 1283
		[OldUserSetting("Messaging: Force messaging disabled", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_MESSAGING_ForceMessagingDisabled,
		// Token: 0x04000504 RID: 1284
		[OldUserSetting("Always hide current student name from status bar", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_AlwaysHideCurrentStudentNameFromStatusBar,
		// Token: 0x04000505 RID: 1285
		[OldUserSetting("Main screen layout definition", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "", IsHidden = true)]
		SETTING_MainScreenDotNetBarLayoutDefinition,
		// Token: 0x04000506 RID: 1286
		[OldUserSetting("Main screen definition", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "", IsHidden = true)]
		SETTING_MainScreenDotNetBarDefinition,
		// Token: 0x04000507 RID: 1287
		[OldUserSetting("Main toolbar font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 8, IsHidden = true)]
		SETTING_MainToolbarFontSize,
		// Token: 0x04000508 RID: 1288
		[OldUserSetting("Dynamic form override font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_DynamicScreenOverrideFontSize,
		// Token: 0x04000509 RID: 1289
		[OldUserSetting("Forms toolbar font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_ScreensToolbarFontSize,
		// Token: 0x0400050A RID: 1290
		[OldUserSetting("Schedule toolbar font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_ScheduleToolbarFontSize,
		// Token: 0x0400050B RID: 1291
		SETTING_StatusBarFontSize,
		// Token: 0x0400050C RID: 1292
		[OldUserSetting("People chooser font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_PeopleChooserFontSize,
		// Token: 0x0400050D RID: 1293
		[OldUserSetting("Calendar font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_CalendarFontSize,
		// Token: 0x0400050E RID: 1294
		[OldUserSetting("Attendees font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_AttendeesFontSize,
		// Token: 0x0400050F RID: 1295
		[OldUserSetting("Appointment details font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_AppDetailsFontSize,
		// Token: 0x04000510 RID: 1296
		[OldUserSetting("Free search font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_FreeSearchFontSize,
		// Token: 0x04000511 RID: 1297
		[OldUserSetting("Appointment edit toolbar font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_AppEditToolbarFontSize,
		// Token: 0x04000512 RID: 1298
		[OldUserSetting("Per appointment forms: Show no-show appointments?", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_PerAppointmentShowNoshowAppointments,
		// Token: 0x04000513 RID: 1299
		[OldUserSetting("Per appointment forms: Show cancelled appointments?", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_PerAppointmentShowCancelledAppointments,
		// Token: 0x04000514 RID: 1300
		[OldUserSetting("Per appointment forms: App list font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_PerAppointmentAppListFontSize,
		// Token: 0x04000515 RID: 1301
		[OldUserSetting("Per appointment forms: App list width", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_PerAppointmentAppListWidth,
		// Token: 0x04000516 RID: 1302
		[OldUserSetting("Don't allow changing instructor", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_DontAllowChangingInstructor,
		// Token: 0x04000517 RID: 1303
		[OldUserSetting("Use equivalent code for courses", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_UseEquivalentCodeForCourses,
		// Token: 0x04000518 RID: 1304
		[OldUserSetting("Tests screen: sort by ascending dates", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SortByAscendingDatesInTestsScreen,
		// Token: 0x04000519 RID: 1305
		[OldUserSetting("Course chooser: Section mask", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_CourseChooserSectionMask,
		// Token: 0x0400051A RID: 1306
		[OldUserSetting("Course chooser: Section max # characters", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_CourseChooserSectionMaxNumChars,
		// Token: 0x0400051B RID: 1307
		[OldUserSetting("Notetaking group crosslisted equivalent courses together for notetakees matched with notetakers reports", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_NotetakingGroupCrosslistedEquivalentCoursesTogetherForNotetakeesMatchedWithNotetakersReports,
		// Token: 0x0400051C RID: 1308
		[OldUserSetting("Alerts disabled", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_AlertsDisabled,
		// Token: 0x0400051D RID: 1309
		[OldUserSetting("Trigger rules", eOldUserSettingInputType.AlertTriggersListXml, eOldUserSettingGroup.Students, Description = "example: ei_,ps,4800`ei_,pa,1844")]
		SETTING_AlertsCode,
		// Token: 0x0400051E RID: 1310
		[OldUserSetting("Mailing labels: Label name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "5160")]
		SETTING_MailingLabels_labelName,
		// Token: 0x0400051F RID: 1311
		[OldUserSetting("Mailing labels: Font name", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "Courier New")]
		SETTING_MailingLabels_fontName,
		// Token: 0x04000520 RID: 1312
		[OldUserSetting("Mailing labels: Font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_MailingLabels_fontSize,
		// Token: 0x04000521 RID: 1313
		[OldUserSetting("Mailing labels: Bold first line", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_MailingLabels_boldFirstLine,
		// Token: 0x04000522 RID: 1314
		[OldUserSetting("Mailing labels: Line 1", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "#<FIRSTNAME># #<LASTNAME>#")]
		SETTING_MailingLabels_line1,
		// Token: 0x04000523 RID: 1315
		[OldUserSetting("Mailing labels: Line 2", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "#<ADDRESS>#")]
		SETTING_MailingLabels_line2,
		// Token: 0x04000524 RID: 1316
		[OldUserSetting("Mailing labels: Line 3", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "#<CITY>#, #<PROVINCE>#  #<POSTAL CODE>#")]
		SETTING_MailingLabels_line3,
		// Token: 0x04000525 RID: 1317
		[OldUserSetting("Mailing labels: Line 4", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_MailingLabels_line4,
		// Token: 0x04000526 RID: 1318
		[OldUserSetting("Mailing labels: Line 5", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_MailingLabels_line5,
		// Token: 0x04000527 RID: 1319
		[OldUserSetting("Mailing labels: Line 6", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_MailingLabels_line6,
		// Token: 0x04000528 RID: 1320
		[OldUserSetting("Department name", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "ClockWork")]
		SETTING_DATABASE_NAME,
		// Token: 0x04000529 RID: 1321
		[OldUserSetting("Mailing labels: Capitalize first line", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_MailingLabels_capitalizeFirstLine,
		// Token: 0x0400052A RID: 1322
		[OldUserSetting("Mailing labels: Caplitalize lines 2 and on", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_MailingLabels_capitalizeLines2On,
		// Token: 0x0400052B RID: 1323
		[OldUserSetting("Allow picture control in dynamic forms", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_AllowPicturesInPerStudentScreens,
		// Token: 0x0400052C RID: 1324
		[OldUserSetting("Hide temporary student number button on intake form", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_HideTemporaryStudentNumberButtonOnStudentInfoScreen = 320,
		// Token: 0x0400052D RID: 1325
		[OldUserSetting("Remind users of new students they've added with temporary student numbers on ClockWork startup", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_RemindUsersOfNewStudentsTheyveAddedWithTemporaryStudentNumbers_atClockWorkLaunch,
		// Token: 0x0400052E RID: 1326
		[OldUserSetting("Show icons on appointments for filled out anonymous forms", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_AnonymousShowIconsForFilledOutAnonymousScreensOnAppointments = 325,
		// Token: 0x0400052F RID: 1327
		[OldUserSetting("Show icons on appointments for filled out per student screens", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_PerStudentShowIconsForFilledOutPerStudentScreensOnAppointments,
		// Token: 0x04000530 RID: 1328
		[OldUserSetting("Tests main font size", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_TestsMainFontSize,
		// Token: 0x04000531 RID: 1329
		[OldUserSetting("Client connect timeout in milliseconds", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 5000)]
		SETTING_ClientConnectTimeoutMilleseconds = 350,
		// Token: 0x04000532 RID: 1330
		[OldUserSetting("The date format to use (ex. yyyy-MM-dd)", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "MMM d, yyyy")]
		SETTING_DateFormat,
		// Token: 0x04000533 RID: 1331
		[OldUserSetting("The time format to use (ex. hh:mm tt, h:mm)", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "h:mm tt")]
		SETTING_TimeFormat,
		// Token: 0x04000534 RID: 1332
		[OldUserSetting("Extensions folder", eOldUserSettingInputType.filename, eOldUserSettingGroup.Unknown)]
		SETTING_ExtensionsFolder,
		// Token: 0x04000535 RID: 1333
		[OldUserSetting("Groupids that will appear in staff drop list", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Users, DefaultValueString = "2")]
		SETTING_GroupWithStaffForDropList,
		// Token: 0x04000536 RID: 1334
		[OldUserSetting("Groupids that will appear in student drop list", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Users, DefaultValueString = "1")]
		SETTING_GroupWithStudentForDropList,
		// Token: 0x04000537 RID: 1335
		[OldUserSetting("Groupids that will appear in room drop list", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Users, DefaultValueString = "3")]
		SETTING_GroupWithRoomForDropList,
		// Token: 0x04000538 RID: 1336
		[OldUserSetting("Groupids that will appear in resource drop list", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Users, DefaultValueString = "4")]
		SETTING_GroupWithResourceForDropList,
		// Token: 0x04000539 RID: 1337
		[OldUserSetting("Appointment permissions enabled", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_AppointmentPermissionsEnabled,
		// Token: 0x0400053A RID: 1338
		[OldUserSetting("Allowed appointment types", eOldUserSettingInputType.numberArray_apptypeids, eOldUserSettingGroup.Appointments)]
		SETTING_VisibleAppTypeIds,
		// Token: 0x0400053B RID: 1339
		[OldUserSetting("Automatically fill in last successful username for ClockWork login", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_AutomaticallFillInLastSuccessfulUsernameForClockWorkLoginType,
		// Token: 0x0400053C RID: 1340
		[OldUserSetting("Tests: show cancelled exams?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_TestsShowCancelledExams,
		// Token: 0x0400053D RID: 1341
		[OldUserSetting("Workshops: All sessions checked?", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_WorkshopsAllSessionsChecked,
		// Token: 0x0400053E RID: 1342
		[OldUserSetting(DefaultValueInt = -1)]
		SETTING_WorkshopsLastSelectedWorkshopId,
		// Token: 0x0400053F RID: 1343
		[OldUserSetting("Appointment edit: check for last no-show mode", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_AppointmentEditCheckForLastNoShowMode,
		// Token: 0x04000540 RID: 1344
		[OldUserSetting("Chat: num days old to delete sonline computers items", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 3)]
		SETTING_ChatNumDaysOldToDeleteSonlineComputersItems,
		// Token: 0x04000541 RID: 1345
		[OldUserSetting("Accommodation strings to replace for accommodations on letters", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "(*),")]
		SETTING_AccommodationsStringsToReplaceForAccommodationsOnLetters,
		// Token: 0x04000542 RID: 1346
		[OldUserSetting("Students in my appts: Include cancelled appointments?", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_StudentsInMyApps_includeCancelledApps,
		// Token: 0x04000543 RID: 1347
		[OldUserSetting("Students in my appts: Filter by", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_StudentsInMyApps_filterBy,
		// Token: 0x04000544 RID: 1348
		[OldUserSetting("Students in my appts: 'Filter by' start date", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_StudentsInMyApps_filterByStartDate,
		// Token: 0x04000545 RID: 1349
		[OldUserSetting("Students in my appts: 'Filter by' end date", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_StudentsInMyApps_filterByEndDate,
		// Token: 0x04000546 RID: 1350
		[OldUserSetting("Students in my appts: Filter by all sessions?", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_StudentsInMyApps_filterByAllSessions,
		// Token: 0x04000547 RID: 1351
		[OldUserSetting("Log: Successfully applied database updates", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_LOG_successfullyAppliedDatabaseUpdates,
		// Token: 0x04000548 RID: 1352
		[OldUserSetting("Per appointment forms: Don't print false checkboxes", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_PerAppointmentPrint_DontPrintFalseCheckboxes,
		// Token: 0x04000549 RID: 1353
		[OldUserSetting("Tests: Include checked column accommodations in accommodations other", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_Tests_IncludeCheckedColumnAccommsInAccommodationsOther,
		// Token: 0x0400054A RID: 1354
		[OldUserSetting("Minimum length for student numbers", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Students)]
		SETTING_StudentNumMinLength,
		// Token: 0x0400054B RID: 1355
		[OldUserSetting("Maximum length for student numbers", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Students)]
		SETTING_StudentNumMaxLength,
		// Token: 0x0400054C RID: 1356
		[OldUserSetting("Sql to restrict the list of students in the drop list.  Example (this only shows students the logged in user has seen at least once in an appointment): p.personid IN (SELECT personid FROM attendees WHERE appointmentid IN (SELECT appointmentid FROM attendees WHERE personid=@whoamiid))", eOldUserSettingInputType.text, eOldUserSettingGroup.Users)]
		SETTING_GroupWithStudentForDropList_SQL,
		// Token: 0x0400054D RID: 1357
		[OldUserSetting("Groupids that will appear in the groups droplist", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Users)]
		SETTING_GroupWithGroupIdsForGroupsDropList,
		// Token: 0x0400054E RID: 1358
		[OldUserSetting("Caption for students singular", eOldUserSettingInputType.text, eOldUserSettingGroup.Users, DefaultValueString = "Student")]
		SETTING_StudentCaptionSingular,
		// Token: 0x0400054F RID: 1359
		[OldUserSetting("Caption for student drop list", eOldUserSettingInputType.text, eOldUserSettingGroup.Users, DefaultValueString = "Students")]
		SETTING_DropListCaption_student,
		// Token: 0x04000550 RID: 1360
		[OldUserSetting("Caption for staff drop list", eOldUserSettingInputType.text, eOldUserSettingGroup.Users, DefaultValueString = "Staff")]
		SETTING_DropListCaption_staff,
		// Token: 0x04000551 RID: 1361
		[OldUserSetting("Caption for resource drop list", eOldUserSettingInputType.text, eOldUserSettingGroup.Users, DefaultValueString = "Resources")]
		SETTING_DropListCaption_resources,
		// Token: 0x04000552 RID: 1362
		[OldUserSetting("Student info mini controls (these fields will show in the left 'Student info mini' panel whenever a student's name is selected.", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Students)]
		SETTING_ControlIdsForStudentInfoMiniPanel = 400,
		// Token: 0x04000553 RID: 1363
		[OldUserSetting("Appointment memo file reference mode", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_AppointmentMemoFileReferenceMode,
		// Token: 0x04000554 RID: 1364
		[OldUserSetting("Appointment memo shared folder path", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_AppointmentMemoSharedFolderPath,
		// Token: 0x04000555 RID: 1365
		[OldUserSetting("Ok to email: Ignore emails not ok to email", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 1)]
		SETTING_OkToEmail_IgnoreEmailsNotOkToEmail,
		// Token: 0x04000556 RID: 1366
		[OldUserSetting("Check for new deleted students every _X_ minutes", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_CheckForNewDeletedStudentsEvery_x_Minutes,
		// Token: 0x04000557 RID: 1367
		[OldUserSetting("Data Sync Report: Import student data", eOldUserSettingInputType.numeric_reportid, eOldUserSettingGroup.System)]
		SETTING_ReportNumberToRunForImportingStudentsFromExternalDatabase,
		// Token: 0x04000558 RID: 1368
		[OldUserSetting("Data Sync Report: Preview student data", eOldUserSettingInputType.numeric_reportid, eOldUserSettingGroup.System)]
		SETTING_ReportNumberToRunForPreviewingStudentsFromExternalDatabase,
		// Token: 0x04000559 RID: 1369
		[OldUserSetting("Password for encrypted data (importing students from external database", eOldUserSettingInputType.password, eOldUserSettingGroup.Unknown)]
		SETTING_PasswordForEncryptedData_ImportingStudentsFromExternalDatabase,
		// Token: 0x0400055A RID: 1370
		[OldUserSetting("Report number to run for getting group memberships for students from external database", eOldUserSettingInputType.numeric_reportid, eOldUserSettingGroup.Unknown)]
		SETTING_ReportNumberToRunForGettingGroupMembershipsForStudentsFromExternalDatabase,
		// Token: 0x0400055B RID: 1371
		[OldUserSetting("Hide Accommodations", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ButtonHide_Accommodations = 410,
		// Token: 0x0400055C RID: 1372
		[OldUserSetting("Hide Workshops / Events", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ButtonHide_Workshops,
		// Token: 0x0400055D RID: 1373
		[OldUserSetting("Hide Student Files", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ButtonHide_Files,
		// Token: 0x0400055E RID: 1374
		[OldUserSetting("Hide Notetaking", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons, DefaultValueInt = 1, IsHidden = true)]
		SETTING_ButtonHide_Notetaking,
		// Token: 0x0400055F RID: 1375
		[OldUserSetting("Hide Tests", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ButtonHide_Tests,
		// Token: 0x04000560 RID: 1376
		[OldUserSetting("Hide Courses", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Buttons)]
		SETTING_ButtonHide_Courses,
		// Token: 0x04000561 RID: 1377
		[OldUserSetting("The column width percent for the screen will be applied to the current active panel if yes, if no it will be applied to the screen width", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_PerAppScreens_ColWidthPercentOfActiveScreen = 417,
		// Token: 0x04000562 RID: 1378
		[OldUserSetting("The dynamic screen to use for workshops in the appointment edit dialog", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_PerJustAppScreen_WorkshopScreenNum = 420,
		// Token: 0x04000563 RID: 1379
		[OldUserSetting("What screens can be used to add new clients?", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms)]
		SETTING_PerStudentScreens_AllowedToAddNewUsersAs,
		// Token: 0x04000564 RID: 1380
		[OldUserSetting("Appointment Edit Dialog display mode (0 = normal; 1 = enhanced)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments, DefaultValueInt = 0)]
		SETTING_AppEditDisplayMode,
		// Token: 0x04000565 RID: 1381
		SETTING_AppEditPDataHeight,
		// Token: 0x04000566 RID: 1382
		[OldUserSetting("Appointment Edit Workshop Attendee Per App Screen (what screen to use when double-clicking on a name in the attendees list)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_WorkshopAttendeePerAppScreen,
		// Token: 0x04000567 RID: 1383
		[OldUserSetting("Appointment Edit Workshop Attendee payment amount control id (what controlid to look at for the attendee payment)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_WorkshopAttendeeFeePaymentControlId,
		// Token: 0x04000568 RID: 1384
		[OldUserSetting("Can this user see students that are in appointments not including them or booked by them (use 0 to indicate yes and 1 to indicate no)", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Users, DefaultValueInt = 0)]
		SETTING_CantSeeStudents,
		// Token: 0x04000569 RID: 1385
		[OldUserSetting("If this user is restricted to not being able to see students in appointments not including them or booked by them, this is a listing of comma separated personids that will be exceptions (if any of these people is in the appointment then they will be able to see any students in that appointment)", eOldUserSettingInputType.text, eOldUserSettingGroup.Users)]
		SETTING_CantSeeStudents_exceptionPids,
		// Token: 0x0400056A RID: 1386
		[OldUserSetting("Counsellor email control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_CounsellorEmail_controlid,
		// Token: 0x0400056B RID: 1387
		[OldUserSetting("Counsellor phone control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_CounsellorPhone_controlid,
		// Token: 0x0400056C RID: 1388
		SETTING_PerAppScreenAllSessionsChecked,
		// Token: 0x0400056D RID: 1389
		SETTING_ButtonCaption_Workshops,
		// Token: 0x0400056E RID: 1390
		[OldUserSetting("Always encrypt memos (checkbox in appointment edit screen will be checked by default)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, DefaultValueInt = 1)]
		SETTING_AlwaysEncryptMemos,
		// Token: 0x0400056F RID: 1391
		[OldUserSetting("What groups can have recurring appointments managed by this user?", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Appointments, DefaultValueString = "")]
		SETTING_GroupIdsAllowedToManageRecurringApps,
		// Token: 0x04000570 RID: 1392
		[OldUserSetting(DefaultValueInt = 1)]
		SETTING_EncryptionModuleType = 4341,
		// Token: 0x04000571 RID: 1393
		SETTING_AvailabilityScheduleGroupIdLastViewed = 435,
		// Token: 0x04000572 RID: 1394
		SETTING_OnlineCounsellingAppointmentBooking_Enabled,
		// Token: 0x04000573 RID: 1395
		SETTING_OnlineCounsellingAppointmentBooking_AppTypeIds,
		// Token: 0x04000574 RID: 1396
		[OldUserSetting(DefaultValueInt = 0)]
		SETTING_UseOldWayOfRefreshingStudentDropLists,
		// Token: 0x04000575 RID: 1397
		SETTING_LastCancelReasonGroupName,
		// Token: 0x04000576 RID: 1398
		[OldUserSetting("Use points of contact", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_UsePointsOfContact,
		// Token: 0x04000577 RID: 1399
		[OldUserSetting("Data sync report: Import student courses", eOldUserSettingInputType.numeric, eOldUserSettingGroup.System)]
		SETTING_ReportNumberToRunForImportingStudentCourses,
		// Token: 0x04000578 RID: 1400
		[OldUserSetting("Lock these screens by default", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms)]
		SETTING_DefaultLockScreenNums,
		// Token: 0x04000579 RID: 1401
		[OldUserSetting("Counsellor title control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_CounsellorTitle_controlid,
		// Token: 0x0400057A RID: 1402
		SETTING_AccommodationsEmail_CounsellorGroupId,
		// Token: 0x0400057B RID: 1403
		SETTING_AccommodationsEmail_AppTypeids,
		// Token: 0x0400057C RID: 1404
		[OldUserSetting("Disability Counsellors Group", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Users)]
		SETTING_DisabilityCounsellorsGroupId,
		// Token: 0x0400057D RID: 1405
		SETTING_MailingLabels_NonRequiredFields,
		// Token: 0x0400057E RID: 1406
		[OldUserSetting("Secondary Email control id", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Forms)]
		SETTING_EmailControlIDSecondary,
		// Token: 0x0400057F RID: 1407
		[OldUserSetting(DefaultValueInt = 1)]
		SETTING_HideDroppedCoursesFromCoursesScreen,
		// Token: 0x04000580 RID: 1408
		SETTING_AccommodationLetters_EmailManual,
		// Token: 0x04000581 RID: 1409
		[OldUserSetting("Points of Contact appointment type group ids", eOldUserSettingInputType.text, eOldUserSettingGroup.Appointments)]
		SETTING_PointOfContactAppointmentTypeGroupIds,
		// Token: 0x04000582 RID: 1410
		SETTING_PointOfContact_panelOnScheduleHeight,
		// Token: 0x04000583 RID: 1411
		SETTING_PointOfContact_width,
		// Token: 0x04000584 RID: 1412
		SETTING_PointOfContact_height,
		// Token: 0x04000585 RID: 1413
		SETTING_PointOfContact_left,
		// Token: 0x04000586 RID: 1414
		SETTING_PointOfContact_top,
		// Token: 0x04000587 RID: 1415
		SETTING_Accommodations_LastPrimary_TemplateId,
		// Token: 0x04000588 RID: 1416
		SETTING_Accommodations_LastPrimary_SendAsEmail,
		// Token: 0x04000589 RID: 1417
		SETTING_Accommodations_LastPrimary_SingleLetterForAllCourses,
		// Token: 0x0400058A RID: 1418
		SETTING_Accommodations_LastSecondary_TemplateId,
		// Token: 0x0400058B RID: 1419
		SETTING_Accommodations_LastSecondary_SendAsEmail,
		// Token: 0x0400058C RID: 1420
		SETTING_Accommodations_LastSecondary_SingleLetterForAllCourses,
		// Token: 0x0400058D RID: 1421
		[OldUserSetting("Automatically prompt the user to email the student when marking no-shows", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, IsHidden = true)]
		SETTING_AutoEmailWhenMarkNoShow,
		// Token: 0x0400058E RID: 1422
		[OldUserSetting("When clicking 'Generate and Save' in the Accommodations screen, leave the form open", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_Accommodations_UseGenerateAndSaveWithoutClosingTheScreen,
		// Token: 0x0400058F RID: 1423
		[OldUserSetting("Present appointment types as a single list without groups", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, IsHidden = true)]
		SETTING_PresentAppTypesAsSingleListNoGroups,
		// Token: 0x04000590 RID: 1424
		[Obsolete("Users can only see appointments in their allowed appointment list by default now - this setting is deprecated because of this new functionality")]
		[OldUserSetting("Only allow staff to view appointments with Allowed Appointment Types in the student appointment history and per appointment screens", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments, IsHidden = true)]
		SETTING_AppointmentTypes_RestrictToStudentAppHistoryAndPerAppScreensToo,
		// Token: 0x04000591 RID: 1425
		[OldUserSetting("Background colour for student forms title (with student name)", eOldUserSettingInputType.Colour, eOldUserSettingGroup.Forms, DefaultValueInt = 0)]
		SETTING_StudentLblTitleBackColour = 468,
		// Token: 0x04000592 RID: 1426
		[OldUserSetting("Foreground colour for student forms title (with student name)", eOldUserSettingInputType.Colour, eOldUserSettingGroup.Forms, DefaultValueInt = 0)]
		SETTING_StudentLblTitleForeColour,
		// Token: 0x04000593 RID: 1427
		SETTING_LabelPrinterSettings1,
		// Token: 0x04000594 RID: 1428
		[OldUserSetting("Template to use for printing labels from per student screens", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms)]
		SETTING_LabelTemplate1,
		// Token: 0x04000595 RID: 1429
		SETTING_LabelPrinterSettings2App,
		// Token: 0x04000596 RID: 1430
		[OldUserSetting("Template to use for printing labels from appointments", eOldUserSettingInputType.text, eOldUserSettingGroup.Forms)]
		SETTING_LabelTemplate2App,
		// Token: 0x04000597 RID: 1431
		[OldUserSetting("Follow up required controls - something filled in for any of these comma separated control ids means a follow up is required", eOldUserSettingInputType.text, eOldUserSettingGroup.Students)]
		SETTING_FollowUpRequiredCids,
		// Token: 0x04000598 RID: 1432
		[OldUserSetting("Follow up completed controls - checked means follow up is done.", eOldUserSettingInputType.text, eOldUserSettingGroup.Students)]
		SETTING_FollowUpCompletedCids,
		// Token: 0x04000599 RID: 1433
		[OldUserSetting("Alternate student number fields (these fields will appear beside the student name on data screens, and will be searchable)", eOldUserSettingInputType.numberArray_controlids, eOldUserSettingGroup.Students)]
		SETTING_AlternateStudentNumberControlIds,
		// Token: 0x0400059A RID: 1434
		[OldUserSetting("Language of choice control", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Users)]
		SETTING_LanguageChoiceControlId,
		// Token: 0x0400059B RID: 1435
		[OldUserSetting("Only allow advisors who had the appointment with the student to enter the assessment", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Forms)]
		SETTING_OnlyAllowPeopleInTheAppointmentToEnterAssessments,
		// Token: 0x0400059C RID: 1436
		[OldUserSetting("Don't import courses (from the Registrar) for students who have this checkbox checked", eOldUserSettingInputType.numeric_controlid, eOldUserSettingGroup.Students)]
		SETTING_DontImportCoursesForStudentsWithThisCheckboxChecked,
		// Token: 0x0400059D RID: 1437
		[OldUserSetting("Disable ClockWork Activation", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students, IsHidden = true)]
		SETTING_DisableClockWorkActivation,
		// Token: 0x0400059E RID: 1438
		[OldUserSetting("Use appointment 'Location' field", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_UseAppointmentLocation,
		// Token: 0x0400059F RID: 1439
		[OldUserSetting("Default to 'Templates' tab in Accommodations screen", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, IsHidden = true)]
		SETTING_DefaultToTemplateInAccommodationsScreen,
		// Token: 0x040005A0 RID: 1440
		[OldUserSetting("Disable appointment room field", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_DisableAppointmentRoom,
		// Token: 0x040005A1 RID: 1441
		[OldUserSetting("Extra time type (how is extra time specified in the accommodations form)", eOldUserSettingInputType.accommodationExtraTimeType, eOldUserSettingGroup.Accommodations, DefaultValueInt = 0)]
		SETTING_Accommodations_ExtraTimeType,
		// Token: 0x040005A2 RID: 1442
		[OldUserSetting("Default group to show on calendar after ClockWork startup", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.PersonalOptions)]
		SETTING_SCHEDULER_defaultStartGroupIds,
		// Token: 0x040005A3 RID: 1443
		[OldUserSetting("Default appointment types to show on calendar after ClockWork startup (will override 'Default group to show on calendar')", eOldUserSettingInputType.numberArray_apptypeids, eOldUserSettingGroup.PersonalOptions)]
		SETTING_SCHEDULER_defaultStartAppTypeIds,
		// Token: 0x040005A4 RID: 1444
		[OldUserSetting("Disable software updates", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System)]
		SETTING_SCHEDULER_disableClockWorkWebUpdates = 488,
		// Token: 0x040005A5 RID: 1445
		[OldUserSetting("Allow staff to create new students using existing ClockWork students they are restricted from seeing.", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Students)]
		SETTING_AllowUsersToCreateNewStudentsUsingExistingStudentsTheyCantSee,
		// Token: 0x040005A6 RID: 1446
		[OldUserSetting("Formatting characters for bulleted lists (Accommodations letter).  Use ` to separate bulletpre`bulletpost`bulletnewline`bulletheader`bulletfooter. Examples: • ``\\n`` or * ``\\par``", eOldUserSettingInputType.text, eOldUserSettingGroup.Accommodations, IsHidden = true)]
		SETTING_AccommodationsBulletPrePostNewlineHeaderFooter,
		// Token: 0x040005A7 RID: 1447
		[OldUserSetting("Screen to use for notetaker profile", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms)]
		SETTING_NotetakerProfileScreenNum,
		// Token: 0x040005A8 RID: 1448
		[OldUserSetting("Automatically save all generated accommodation letters to this folder", eOldUserSettingInputType.text, eOldUserSettingGroup.Accommodations)]
		SETTING_AccommodationsLetterAutoSaveFolder,
		// Token: 0x040005A9 RID: 1449
		[OldUserSetting("Use restrictive default settings and permissions (users will have less default permissions and you will have to open up permissions for common things manually)", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Users)]
		SETTING_Use_Restrictive_Default_Settings_and_Permissions,
		// Token: 0x040005AA RID: 1450
		[OldUserSetting("Allowed to change availability schedule for which groups of people", eOldUserSettingInputType.numberArray_groupids, eOldUserSettingGroup.Appointments)]
		SETTING_Allowed_To_Change_Availability_Schedule_For_Which_Groupids,
		// Token: 0x040005AB RID: 1451
		SETTING_TestLabelsAveryLabelType,
		// Token: 0x040005AC RID: 1452
		[OldUserSetting("Special list view definitions", eOldUserSettingInputType.SummaryManagementViews, eOldUserSettingGroup.Forms)]
		SETTING_GroupedViews,
		// Token: 0x040005AD RID: 1453
		[OldUserSetting("Screen buttons to hide", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Unknown)]
		SETTING_ScreenNumsToHide,
		// Token: 0x040005AE RID: 1454
		[OldUserSetting("Screens to ignore missing value entries for required fields", eOldUserSettingInputType.numberArray, eOldUserSettingGroup.Users)]
		SETTING_ScreenNumsToIgnoreNonOptionalDynamicControls,
		// Token: 0x040005AF RID: 1455
		[OldUserSetting("Suppress warnings for missing items when generating accommodation letters", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations)]
		SETTING_SuppressAccommodationMissingValueWarnings,
		// Token: 0x040005B0 RID: 1456
		[OldUserSetting("Override default appointment duration (minutes) for new appointments", eOldUserSettingInputType.numeric, eOldUserSettingGroup.Appointments)]
		SETTING_OverrideDefaultAppLength = 99500,
		// Token: 0x040005B1 RID: 1457
		[OldUserSetting("Message to staff (will be shown during login to ClockWork)", eOldUserSettingInputType.text, eOldUserSettingGroup.Users)]
		SETTING_StaffMessage,
		// Token: 0x040005B2 RID: 1458
		[OldUserSetting("Enable AT Form - what data form will be displayed on the right column?", eOldUserSettingInputType.numberArray_screennum, eOldUserSettingGroup.Forms)]
		SETTING_AT_PerStudentScreenNum,
		// Token: 0x040005B3 RID: 1459
		[OldUserSetting(DefaultValueInt = -16777216)]
		SETTING_SCHEDULER_foreCol = 500,
		// Token: 0x040005B4 RID: 1460
		[OldUserSetting(DefaultValueInt = -2368549)]
		SETTING_SCHEDULER_backCol1,
		// Token: 0x040005B5 RID: 1461
		[OldUserSetting(DefaultValueInt = -6447715)]
		SETTING_SCHEDULER_backCol2,
		// Token: 0x040005B6 RID: 1462
		[OldUserSetting(DefaultValueInt = -985601)]
		SETTING_SCHEDULER_timebarForeCol,
		// Token: 0x040005B7 RID: 1463
		[OldUserSetting(DefaultValueInt = -12499488)]
		SETTING_SCHEDULER_timebarBackCol1,
		// Token: 0x040005B8 RID: 1464
		[OldUserSetting(DefaultValueInt = -16777088)]
		SETTING_SCHEDULER_timebarBackCol2,
		// Token: 0x040005B9 RID: 1465
		[OldUserSetting(DefaultValueInt = -1)]
		SETTING_SCHEDULER_appForeCol,
		// Token: 0x040005BA RID: 1466
		[OldUserSetting(DefaultValueInt = -9231749)]
		SETTING_SCHEDULER_appBackCol1,
		// Token: 0x040005BB RID: 1467
		[OldUserSetting(DefaultValueInt = -4560700)]
		SETTING_SCHEDULER_appBackCol2,
		// Token: 0x040005BC RID: 1468
		[OldUserSetting(DefaultValueInt = -1)]
		SETTING_SCHEDULER_datebarForeCol,
		// Token: 0x040005BD RID: 1469
		[OldUserSetting(DefaultValueInt = -3342079)]
		SETTING_SCHEDULER_datebarBackCol1,
		// Token: 0x040005BE RID: 1470
		[OldUserSetting(DefaultValueInt = -65536)]
		SETTING_SCHEDULER_datebarBackCol2,
		// Token: 0x040005BF RID: 1471
		[OldUserSetting(DefaultValueInt = -16777216)]
		SETTING_SCHEDULER_namebarForeCol,
		// Token: 0x040005C0 RID: 1472
		[OldUserSetting(DefaultValueInt = -256)]
		SETTING_SCHEDULER_namebarBackCol1,
		// Token: 0x040005C1 RID: 1473
		[OldUserSetting(DefaultValueInt = -131118)]
		SETTING_SCHEDULER_namebarBackCol2,
		// Token: 0x040005C2 RID: 1474
		[OldUserSetting(DefaultValueInt = -12499488)]
		SETTING_SCHEDULER_tentativeAppBackCol,
		// Token: 0x040005C3 RID: 1475
		[OldUserSetting(DefaultValueString = "Arial")]
		SETTING_SCHEDULER_appFontName,
		// Token: 0x040005C4 RID: 1476
		[OldUserSetting(DefaultValueString = "Arial")]
		SETTING_SCHEDULER_datebarFontName,
		// Token: 0x040005C5 RID: 1477
		[OldUserSetting(DefaultValueString = "Arial")]
		SETTING_SCHEDULER_namebarFontName,
		// Token: 0x040005C6 RID: 1478
		[OldUserSetting(DefaultValueInt = 8)]
		SETTING_SCHEDULER_appFontSize,
		// Token: 0x040005C7 RID: 1479
		[OldUserSetting(DefaultValueInt = 0)]
		SETTING_SCHEDULER_appFontStyle,
		// Token: 0x040005C8 RID: 1480
		[OldUserSetting(DefaultValueInt = 8)]
		SETTING_SCHEDULER_datebarFontSize,
		// Token: 0x040005C9 RID: 1481
		[OldUserSetting(DefaultValueInt = 0)]
		SETTING_SCHEDULER_datebarFontStyle,
		// Token: 0x040005CA RID: 1482
		[OldUserSetting(DefaultValueInt = 8)]
		SETTING_SCHEDULER_namebarFontSize,
		// Token: 0x040005CB RID: 1483
		[OldUserSetting(DefaultValueInt = 0)]
		SETTING_SCHEDULER_namebarFontStyle,
		// Token: 0x040005CC RID: 1484
		[OldUserSetting(DefaultValueInt = 14)]
		SETTING_SCHEDULER_weekdayFontSize,
		// Token: 0x040005CD RID: 1485
		[OldUserSetting(DefaultValueInt = 0)]
		SETTING_SCHEDULER_weekdayFontStyle,
		// Token: 0x040005CE RID: 1486
		[OldUserSetting(DefaultValueInt = 14)]
		SETTING_SCHEDULER_whosScheduleFontSize,
		// Token: 0x040005CF RID: 1487
		[OldUserSetting(DefaultValueInt = 0)]
		SETTING_SCHEDULER_whosScheduleFontStyle,
		// Token: 0x040005D0 RID: 1488
		[OldUserSetting(DefaultValueInt = -985601)]
		SETTING_SCHEDULER_weekdayForeCol,
		// Token: 0x040005D1 RID: 1489
		[OldUserSetting(DefaultValueInt = -12499488)]
		SETTING_SCHEDULER_weekdayBackCol1,
		// Token: 0x040005D2 RID: 1490
		[OldUserSetting(DefaultValueInt = -12499488)]
		SETTING_SCHEDULER_weekdayBackCol2,
		// Token: 0x040005D3 RID: 1491
		[OldUserSetting(DefaultValueInt = -4866565)]
		SETTING_SCHEDULER_whosScheduleForeCol,
		// Token: 0x040005D4 RID: 1492
		[OldUserSetting(DefaultValueInt = -12499488)]
		SETTING_SCHEDULER_whosScheduleBackCol1,
		// Token: 0x040005D5 RID: 1493
		[OldUserSetting(DefaultValueInt = -12499488)]
		SETTING_SCHEDULER_whosScheduleBackCol2,
		// Token: 0x040005D6 RID: 1494
		[OldUserSetting(DefaultValueString = "Arial")]
		SETTING_SCHEDULER_weekdayFontName,
		// Token: 0x040005D7 RID: 1495
		[OldUserSetting(DefaultValueString = "Arial")]
		SETTING_SCHEDULER_whosScheduleFontName,
		// Token: 0x040005D8 RID: 1496
		[OldUserSetting(DefaultValueString = "Arial")]
		SETTING_SCHEDULER_timebarFontName,
		// Token: 0x040005D9 RID: 1497
		[OldUserSetting(DefaultValueInt = 10)]
		SETTING_SCHEDULER_timebarFontSize,
		// Token: 0x040005DA RID: 1498
		[OldUserSetting(DefaultValueInt = 0)]
		SETTING_SCHEDULER_timebarFontStyle,
		// Token: 0x040005DB RID: 1499
		[OldUserSetting("Scheduler: Show student in tooltip", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_showStudentInToolTip,
		// Token: 0x040005DC RID: 1500
		[OldUserSetting("Scheduler: Show room in tooltip", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_showRoomInToolTip,
		// Token: 0x040005DD RID: 1501
		[OldUserSetting("Scheduler: Show room on appointment", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_showRoomOnAppointment,
		// Token: 0x040005DE RID: 1502
		[OldUserSetting("Scheduler: Fast tooltips", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_fastTooltips,
		// Token: 0x040005DF RID: 1503
		[OldUserSetting("Scheduler: Show all attendees on appointment", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_showAllAttendeesOnAppointment,
		// Token: 0x040005E0 RID: 1504
		[OldUserSetting("Scheduler: Show all attendees in tooltip", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_showAllAttendeesInToolTip,
		// Token: 0x040005E1 RID: 1505
		[OldUserSetting("Scheduler: Appointment colour matches appointment title?", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_appColourSameAsQuickView,
		// Token: 0x040005E2 RID: 1506
		[OldUserSetting("Scheduler: Show memo on appointment", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 0)]
		SETTING_SCHEDULER_showMemoOnAppointment,
		// Token: 0x040005E3 RID: 1507
		[OldUserSetting("Scheduler: Hide icon text in tool tip", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_hideIconTextInToolTip,
		// Token: 0x040005E4 RID: 1508
		[OldUserSetting("Scheduler: 'Appointment colour matches qppointment title' use auto font colour", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_appColourSameAsQuickView_useAutoFontColour,
		// Token: 0x040005E5 RID: 1509
		[OldUserSetting("Always use the same font colour for text displayed on appointment blocks", eOldUserSettingInputType.Colour, eOldUserSettingGroup.Appointments, DefaultValueInt = 0)]
		SETTING_SCHEDULER_appColourSameAsQuickView_manual_FontColour,
		// Token: 0x040005E6 RID: 1510
		[OldUserSetting("Scheduler: Use Windows theme", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_SCHEDULER_useWindowsTheme = 555,
		// Token: 0x040005E7 RID: 1511
		[OldUserSetting("Scheduler: Number of minutes between gridlines", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueInt = 30)]
		SETTING_SCHEDULER_numMinutesBetweenGridlines,
		// Token: 0x040005E8 RID: 1512
		[OldUserSetting("Enable unique staff background colours for multiple schedule viewing (set colours in 'Manage staff')", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_SCHEDULER_ShowMultipleUsersInDifferentBackgroundColours,
		// Token: 0x040005E9 RID: 1513
		SETTING_SCHEDULER_UserColourForShowMultipleUsersInDifferentBackgroundColours,
		// Token: 0x040005EA RID: 1514
		[OldUserSetting("Don't display the date number inversed in the date bar", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_SCHEDULER_dontInverseDateNumberInDateBar,
		// Token: 0x040005EB RID: 1515
		[OldUserSetting("Appointment background colour based on who booked it", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Appointments)]
		SETTING_SCHEDULER_UserColourForShowWhoBookedBackgroundColours,
		// Token: 0x040005EC RID: 1516
		[OldUserSetting("Drop down button groups (specify the drop down button name, then the screen numbers comma separated in brackets).  Multiple groupings can be separated using `.", eOldUserSettingInputType.text, eOldUserSettingGroup.Unknown)]
		SETTING_Buttons_DropDownButtonGroupings,
		// Token: 0x040005ED RID: 1517
		SETTING_SCHEDULER_showStudentNumberOnAppointment = 563,
		// Token: 0x040005EE RID: 1518
		[OldUserSetting("Dynamic form reports", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_REPORTS_dynamicScreenReports = 600,
		// Token: 0x040005EF RID: 1519
		[OldUserSetting("Import courses: funny instructor names", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown, DefaultValueString = "")]
		SETTING_ImportCourses_funnyInstructorNames,
		// Token: 0x040005F0 RID: 1520
		[OldUserSetting("Accessibility appointment text template", eOldUserSettingInputType.text, eOldUserSettingGroup.Unknown, DefaultValueString = "#<title># #<coursedescription># #<workshop># #<icons># #<date># #<starttime># #<duration># #<student># #<attendeesnostudents># #<memo>#")]
		SETTING_Scheduler_AccessibleAppointmentSpokenTemplate,
		// Token: 0x040005F1 RID: 1521
		[OldUserSetting("Web notetaking: notetaking enabled", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_WEB_NOTETAKING_notetakingEnabled = 2000,
		// Token: 0x040005F2 RID: 1522
		[OldUserSetting("Web tests: Group id with exam rooms", eOldUserSettingInputType.unknown, eOldUserSettingGroup.Unknown)]
		SETTING_WEB_TESTS_groupIdWithExamRooms = 2100,
		// Token: 0x040005F3 RID: 1523
		[OldUserSetting("Enable batch accommodation letter emails", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, IsHidden = true)]
		SETTING_BATCH_ACCOMMODATION_LETTERS_ENABLED = 2200,
		// Token: 0x040005F4 RID: 1524
		[OldUserSetting("Enable batch accommodation letter emails", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.Accommodations, DefaultValueString = "JAN 15-APR 30,MAY 15-AUG 30,SEP 15-DEC 31", IsHidden = true)]
		SETTING_BATCH_ACCOMMODATION_LETTERS_TIME_FRAMES,
		// Token: 0x040005F5 RID: 1525
		[OldUserSetting("Force 'screen reader is running' mode", eOldUserSettingInputType.truefalse, eOldUserSettingGroup.System, Description = "Normally ClockWork will auto-detect if a screen reader is running.  You should leave this setting on the default value unless you are having a problem.", DefaultValueInt = 0, SettingLevel = eSettingLevel.Advanced)]
		SETTING_FORCE_SCREEN_READER_IS_RUNNING = 2205,
		// Token: 0x040005F6 RID: 1526
		[OldUserSetting("Smtp Settings", eOldUserSettingInputType.smtpSettings, eOldUserSettingGroup.System, DefaultValueString = "", SubGroup = "Email")]
		SETTING_SMTP_SETTINGS = 2210,
		// Token: 0x040005F7 RID: 1527
		[OldUserSetting("Unknown", eOldUserSettingInputType.text, eOldUserSettingGroup.System, DefaultValueString = "", IsHidden = true)]
		SETTING_UNKNOWN = 0
	}
}
