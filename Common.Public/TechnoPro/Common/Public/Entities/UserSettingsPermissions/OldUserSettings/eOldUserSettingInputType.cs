using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public enum eOldUserSettingInputType
	{
		// Token: 0x0400038D RID: 909
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Unknown, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingUnknownEditor, Common.UI.Admin.WinForms")]
		unknown,
		// Token: 0x0400038E RID: 910
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingIntEditor, Common.UI.Admin.WinForms")]
		numeric,
		// Token: 0x0400038F RID: 911
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingStringEditor, Common.UI.Admin.WinForms")]
		text,
		// Token: 0x04000390 RID: 912
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingTrueFalse, Common.UI.Admin.WinForms")]
		truefalse,
		// Token: 0x04000391 RID: 913
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingStringEditor, Common.UI.Admin.WinForms")]
		password,
		// Token: 0x04000392 RID: 914
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingNumberArrayEditor, Common.UI.Admin.WinForms")]
		numberArray,
		// Token: 0x04000393 RID: 915
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingStringArrayEditor, Common.UI.Admin.WinForms")]
		stringArray,
		// Token: 0x04000394 RID: 916
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingStringEditor, Common.UI.Admin.WinForms")]
		textBig = 9,
		// Token: 0x04000395 RID: 917
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingTrueFalse, Common.UI.Admin.WinForms")]
		yesno,
		// Token: 0x04000396 RID: 918
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingScreenNumArray, Common.UI.Admin.WinForms")]
		numberArray_screennum,
		// Token: 0x04000397 RID: 919
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingScreenNumArray, Common.UI.Admin.WinForms")]
		numberArray_screennum_perapp,
		// Token: 0x04000398 RID: 920
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingGroupIdArrayEditor, Common.UI.Admin.WinForms")]
		numberArray_groupids = 30,
		// Token: 0x04000399 RID: 921
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingAppTypeArrayEditor, Common.UI.Admin.WinForms")]
		numberArray_apptypeids,
		// Token: 0x0400039A RID: 922
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingControlIdEditor, Common.UI.Admin.WinForms")]
		numeric_controlid,
		// Token: 0x0400039B RID: 923
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingColourEditor, Common.UI.Admin.WinForms")]
		Colour,
		// Token: 0x0400039C RID: 924
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingControlIdArrayEditor, Common.UI.Admin.WinForms")]
		numberArray_controlids,
		// Token: 0x0400039D RID: 925
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String)]
		accommodationExtraTimeType,
		// Token: 0x0400039E RID: 926
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlSummaryManagementViewsEditor, Common.UI.Admin.WinForms")]
		SummaryManagementViews = 40,
		// Token: 0x0400039F RID: 927
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingReportIdArrayEditor, Common.UI.Admin.WinForms")]
		numberArray_reportids,
		// Token: 0x040003A0 RID: 928
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingFilenameEditor, Common.UI.Admin.WinForms")]
		filename = 100,
		// Token: 0x040003A1 RID: 929
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingReportIdEditor, Common.UI.Admin.WinForms")]
		numeric_reportid,
		// Token: 0x040003A2 RID: 930
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingScreenNumEditor, Common.UI.Admin.WinForms")]
		numeric_screenNum,
		// Token: 0x040003A3 RID: 931
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Unknown, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingSslProtocolEditor, Common.UI.Admin.WinForms")]
		SslProtocol = 110,
		// Token: 0x040003A4 RID: 932
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.Int, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingSmtpSettingsEditor, Common.UI.Admin.WinForms")]
		smtpSettings = 120,
		// Token: 0x040003A5 RID: 933
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingCutoffTimeEditor, Common.UI.Admin.WinForms")]
		CutoffTime,
		// Token: 0x040003A6 RID: 934
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String)]
		custom = 1000,
		// Token: 0x040003A7 RID: 935
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingCatalogIdArrayEditor, Common.UI.Admin.WinForms")]
		numberArray_catalogIds = 2000,
		// Token: 0x040003A8 RID: 936
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlClockWorkStartupModeEditor, Common.UI.Admin.WinForms")]
		ClockWork_Startup_Mode_XML = 3000,
		// Token: 0x040003A9 RID: 937
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingAlertTriggers, Common.UI.Admin.WinForms")]
		AlertTriggersListXml = 3100,
		// Token: 0x040003AA RID: 938
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingFormApproval, Common.UI.Admin.WinForms")]
		FormApprovalOptionsXml = 3110,
		// Token: 0x040003AB RID: 939
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlMultiDepartmentIntakeSettingsEdit, Common.UI.Admin.WinForms")]
		MultiDepartmentIntake,
		// Token: 0x040003AC RID: 940
		[OldUserSettingInputType(StorageLocation = eOldUserSettingStorageLocation.String, WinFormsEditControlClass = "TechnoPro.Common.UI.Admin.WinForms.SettingsOld.Controls.SettingControls.CtrlOldSettingOnlineFormIdList, Common.UI.Admin.WinForms")]
		numberArray_onlineFormIds
	}
}
