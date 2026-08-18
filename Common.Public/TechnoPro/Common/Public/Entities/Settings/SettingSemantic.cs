using System;
using System.Drawing;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001DB RID: 475
	public enum SettingSemantic
	{
		// Token: 0x04000CEA RID: 3306
		[SemanticType(SystemType = typeof(int), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.IntSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		INTEGER,
		// Token: 0x04000CEB RID: 3307
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.StringSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		XML,
		// Token: 0x04000CEC RID: 3308
		[SemanticType(SystemType = typeof(string), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.BrowsableSettingCtrl2, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		HTML,
		// Token: 0x04000CED RID: 3309
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = null, WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		TEXT,
		// Token: 0x04000CEE RID: 3310
		[SemanticType(SystemType = typeof(Image), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.ImageSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		IMAGE,
		// Token: 0x04000CEF RID: 3311
		[SemanticType(SystemType = typeof(int[]), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.ReferenceListSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		REFERENCE_ARRAY,
		// Token: 0x04000CF0 RID: 3312
		[SemanticType(SystemType = typeof(Color), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.ColorSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		COLOR,
		// Token: 0x04000CF1 RID: 3313
		[SemanticType(SystemType = typeof(DateTime), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.DatetimeSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		DATETIME,
		// Token: 0x04000CF2 RID: 3314
		[SemanticType(SystemType = typeof(bool), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.BooleanSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		BOOLEAN,
		// Token: 0x04000CF3 RID: 3315
		[SemanticType(SystemType = typeof(string), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CtrlEmailTemplateSetting, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		EMAIL_TEMPLATE,
		// Token: 0x04000CF4 RID: 3316
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.ChannelsSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		CHANNELS,
		// Token: 0x04000CF5 RID: 3317
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.AppointmentBooking.CtrlAppointmentBookingChannelsAndAvailabilities, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		SCHEDULE_TYPES,
		// Token: 0x04000CF6 RID: 3318
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.TestBooking.AssetsSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		ASSETS,
		// Token: 0x04000CF7 RID: 3319
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.TestBooking.RulesSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		TESTRULES,
		// Token: 0x04000CF8 RID: 3320
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.TestBooking.RoomsSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		ROOMS,
		// Token: 0x04000CF9 RID: 3321
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.TestBooking.SpecialAccommodationsCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		SPECIALACCOMMODATIONS,
		// Token: 0x04000CFA RID: 3322
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.PasswordSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		PASSWORD,
		// Token: 0x04000CFB RID: 3323
		[SemanticType(SystemType = typeof(string), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.StringSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		CSHARPCODE,
		// Token: 0x04000CFC RID: 3324
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.LoginAuthenticationsCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		LOGINAUTHENTICATIONMETHODS,
		// Token: 0x04000CFD RID: 3325
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.ClockWorkOutlookSyncUserCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		CLOCKWORKSYNCUSERS,
		// Token: 0x04000CFE RID: 3326
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CutoffTimeCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		CUTOFFTIME,
		// Token: 0x04000CFF RID: 3327
		[SemanticType(SystemType = typeof(TimeSpan), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.TimeSettingCtrl, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		TIME,
		// Token: 0x04000D00 RID: 3328
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.AuthenticationAuthorisation.CtrlAuthorisationContextSetting, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		AUTHORIZATION_CONTEXT,
		// Token: 0x04000D01 RID: 3329
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.AuthenticationAuthorisation.CtrlAuthenticationContextSetting, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		AUTHENTICATION_CONTEXT,
		// Token: 0x04000D02 RID: 3330
		[SemanticType(SystemType = typeof(int), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CtrlControlIdChooser, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = "multiselect=false,allowedformtypes=0")]
		CONTROLID_PERSTUDENT,
		// Token: 0x04000D03 RID: 3331
		[SemanticType(SystemType = typeof(int), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CtrlControlIdChooser, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = "multiselect=false,allowedformtypes=3")]
		CONTROLID_ACCOMM,
		// Token: 0x04000D04 RID: 3332
		[SemanticType(SystemType = typeof(int), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CtrlControlIdChooser, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = "multiselect=false,allowedformtypes=25")]
		CONTROLID_PERDATE,
		// Token: 0x04000D05 RID: 3333
		[SemanticType(SystemType = typeof(int), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CtrlControlIdChooser, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = "multiselect=false,allowedformtypes=1")]
		CONTROLID_PERAPP,
		// Token: 0x04000D06 RID: 3334
		[SemanticType(SystemType = typeof(string), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CtrlControlIdChooser, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = "multiselect=true,allowedformtypes=0")]
		CONTROLIDS_PERSTUDENT,
		// Token: 0x04000D07 RID: 3335
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.RequiredSessionForms.CtrlRequiredSessionFormsSetting, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		REQUIRED_SESSION_FORMS,
		// Token: 0x04000D08 RID: 3336
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.StudentFilesForms.CtrlStudentFilesRulesSetting, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		STUDENT_FILES_RULES,
		// Token: 0x04000D09 RID: 3337
		[SemanticType(SystemType = typeof(string), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.CtrlControlIdChooser, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = "multiselect=true,allowedformtypes=4,allowalltobeselected=true")]
		CONTROLIDS_ACCOMMODATIONS,
		// Token: 0x04000D0A RID: 3338
		[SemanticType(SystemType = typeof(string), IsFullScreenEditor = true, WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.TestBooking.CtrlCampusesWithEmailTemplateIdsListEditor, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		CAMPUSES_WITH_EMAILTEMPLATEIDS,
		// Token: 0x04000D0B RID: 3339
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.AltFormat.CtrlAccommodationsAltFormatTypesMappingsEditor, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		ACCOMMODATIONS_ALTFORMATTYPES_MAPPINGS,
		// Token: 0x04000D0C RID: 3340
		[SemanticType(SystemType = typeof(string), WinFormsEditorControlClass = "TechnoPro.Common.UI.WinForms.Settings.SettingCtrls.AppointmentBooking.Controls.CtrlPreCalendarQuestionnaireOptions, Common.UI.WinForms", WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs = null)]
		APPOINTMENTBOOKING_PRECALENDAR_QUESTIONNAIRE
	}
}
