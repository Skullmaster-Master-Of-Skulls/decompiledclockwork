using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000362 RID: 866
	[Serializable]
	public enum eControlCode
	{
		// Token: 0x04001598 RID: 5528
		[DynamicControl]
		Unknown,
		// Token: 0x04001599 RID: 5529
		[DynamicControl("TextBox", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlTextBox, Common.UI.Web.DynamicControls", true, "A single-line or multi-line text box with or without spell-check.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo, EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.Setting3, EncryptedFlagValue = 1, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemTextbox, Common.Public")]
		TextBox,
		// Token: 0x0400159A RID: 5530
		[DynamicControl("CheckBox", "TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.DynamicCheckbox, Common.UI.WinForms.DynamicFormsControls", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlCheckBox, Common.UI.Web.DynamicControls", true, "A single checkbox that can be true (checked) or false (un-checked).", PresentationDataType = typeof(bool), StorageLocation = eDynamicDataStorageLocation.MainInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemCheckbox, Common.Public")]
		CheckBox,
		// Token: 0x0400159B RID: 5531
		[DynamicControl("Drop List", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlDropList, Common.UI.Web.DynamicControls", true, "A drop list allows selection of a single item from a list of items.  This drop-list can also optionally allow free-form text to be entered.", PresentationDataType = typeof(string), StorageLocation = (eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo), EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.Setting3, EncryptedFlagValue = -1, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemDropListGeneral, Common.Public")]
		DropList,
		// Token: 0x0400159C RID: 5532
		[DynamicControl("Radio Button", "", "", true, "A single radio button. This control is deprecated; please use the radio group control instead.", StorageLocation = eDynamicDataStorageLocation.MainInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemCheckbox, Common.Public", PresentationDataType = typeof(string))]
		RadioButton,
		// Token: 0x0400159D RID: 5533
		[DynamicControl("Label", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlLabel, Common.UI.Web.DynamicControls", false, "A label is a static display string.  This control cannot store data and is for display purposes only.  Labels are useful for titles or additional information for the user.")]
		Label,
		// Token: 0x0400159E RID: 5534
		[DynamicControl("Date Chooser", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlDatePicker, Common.UI.Web.DynamicControls", true, "A date chooser allows selection of a single date.  This date chooser allows a null (empty) selection.", PresentationDataType = typeof(DateTime), StorageLocation = eDynamicDataStorageLocation.DateTimeInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemDateChooser, Common.Public")]
		Date,
		// Token: 0x0400159F RID: 5535
		[DynamicControl("Time Chooser", "", "", true, "A time chooser allows selection of a single time.  This time chooser allows a null (empty) selection.", PresentationDataType = typeof(DateTime), StorageLocation = eDynamicDataStorageLocation.DateTimeInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemDateChooser, Common.Public")]
		Time,
		// Token: 0x040015A0 RID: 5536
		[DynamicControl("Horizontal Rule", "", "", false, "A horizontal rule is a line that can be used as a separator between items on a form.  This control cannot store data and is for display purposes only.", StorageLocation = eDynamicDataStorageLocation.Unknown)]
		HorizontalRule,
		// Token: 0x040015A1 RID: 5537
		[DynamicControl("Blank Space", "", "", false, "A blank space can be used to separate items on a form.  This control cannot store data and is for display purposes only.")]
		BlankSpace,
		// Token: 0x040015A2 RID: 5538
		[DynamicControl("List View", "", "", true, "A list view is a table that requires a list of columns.  The user will be able to add rows to the table with information.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemListView, Common.Public")]
		ListView,
		// Token: 0x040015A3 RID: 5539
		[DynamicControl("My CheckBox", "", "", true, "MyCheckBox has been deprecated; please use CheckBox instead.", PresentationDataType = typeof(bool), StorageLocation = eDynamicDataStorageLocation.MainInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemCheckbox, Common.Public")]
		MyCheckBox = 12,
		// Token: 0x040015A4 RID: 5540
		[DynamicControl("My TextBox", "", "", true, "MyTextBox has been deprecated; please use TextBox instead.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemTextbox, Common.Public")]
		MyTextBox = 11,
		// Token: 0x040015A5 RID: 5541
		[DynamicControl("Indent", "", "", false, "Indent can be used to indent controls on the form.  This control cannot store data and is for display purposes only.")]
		Indent = 13,
		// Token: 0x040015A6 RID: 5542
		[DynamicControl("Radio Group", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlRadioGroup, Common.UI.Web.DynamicControls", true, "A radio group allows the user to select a single item from a list.  It is exactly similar to a drop list control in functionality, but allows the user to see all options without having to click first.  Radio groups are useful for selections from small lists.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.MainInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemRadioButtonGroup, Common.Public")]
		RadioGroup,
		// Token: 0x040015A7 RID: 5543
		[DynamicControl("File List", "TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlFileList", "", true, "A file list is a table that allows the user to store files (each file is one row in the table).  You must specify a list of columns for the file list; a date and filename column are automatically added at the end of the table.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemFileList, Common.Public")]
		FileList = 20,
		// Token: 0x040015A8 RID: 5544
		[DynamicControl("Picture", "", "", true, "A picture control allows storage of an image.  This is useful for photos and signature images.", PresentationDataType = typeof(byte[]), StorageLocation = eDynamicDataStorageLocation.ImageInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemPicture, Common.Public")]
		Picture,
		// Token: 0x040015A9 RID: 5545
		[DynamicControl("Dynamic Table", "", "", true, "Dynamic Table has been deprecated; please use List View instead.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo)]
		DynamicTable = 25,
		// Token: 0x040015AA RID: 5546
		[DynamicControl("Group Box", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlPanel, Common.UI.Web.DynamicControls", false, true, false, "A group box is a box that can store other controls.  It can be used to separate and organize other controls on the form.  This control cannot store data and is for display purposes only.")]
		PanelStart = 30,
		// Token: 0x040015AB RID: 5547
		[DynamicControl("Group Box Close", "", "", false, false, true, "Panel close")]
		PanelClose,
		// Token: 0x040015AC RID: 5548
		[DynamicControl("Tab Control", "", "", false, true, false, "A tab control can be used to separate a form into several tabs for organizational and display purposes. You can only use one tab control per form and it should be the first control on your form.  This control cannot store data and is for display purposes only.")]
		TabControlStart,
		// Token: 0x040015AD RID: 5549
		[DynamicControl("Tab Page", "", "", false, true, false, "Tab page start")]
		TabPageStart,
		// Token: 0x040015AE RID: 5550
		[DynamicControl("Tab Page Close", "", "", false, false, true, "Tab page close")]
		TabPageClose,
		// Token: 0x040015AF RID: 5551
		[DynamicControl("Tab Control Close", "", "", false, false, true, "Tabl control close")]
		TabControlClose,
		// Token: 0x040015B0 RID: 5552
		[DynamicControl("Table Control", "", "", true, "Table control is deprecated; please use ListView instead.")]
		TableControl = 40,
		// Token: 0x040015B1 RID: 5553
		[DynamicControl("Column Break", "", "", false, "A column break will stop controls from filling up the current column on-screen and is similar to a page break.  All controls after this one will appear starting at the top of the next column.  This control cannot store data and is for display purposes only.")]
		ColumnBreak = 50,
		// Token: 0x040015B2 RID: 5554
		[DynamicControl("Staff Drop List", "", "", true, "A staff drop list is the same as a drop list but provides a list of users from any ClockWork group.  The default group is Staff.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.MainInfo, EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.AlwaysEncrypted, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemStaffDropList, Common.Public")]
		StaffComboBox = 100,
		// Token: 0x040015B3 RID: 5555
		[DynamicControl("School Year Chooser", "", "", true, "School year chooser", PresentationDataType = typeof(string))]
		SchoolYearChooser = 200,
		// Token: 0x040015B4 RID: 5556
		[DynamicControl("Masked TextBox", "", "", true, "A masked text box is the same as a text box, but allows you set an input mask.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo, EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.Setting3, EncryptedFlagValue = 1, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemTextbox, Common.Public")]
		MaskedTextBox = 300,
		// Token: 0x040015B5 RID: 5557
		[DynamicControl("List Select Item", "", "", true, "A list select item is the same as a checkbox except a group of these is displayed as two boxes, where the user can move items from one to the other to indicate selection(s).", PresentationDataType = typeof(bool), StorageLocation = eDynamicDataStorageLocation.MainInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemCheckbox, Common.Public")]
		ListSelect,
		// Token: 0x040015B6 RID: 5558
		[DynamicControl("Single File", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlFileChooser, Common.UI.Web.DynamicControls", true, "A single file control allows the user to store a single electronic file.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.ImageInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemFile, Common.Public")]
		File = 400,
		// Token: 0x040015B7 RID: 5559
		[DynamicControl("Multi-CheckBox", "", "", true, "", PresentationDataType = typeof(string), EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.Setting3, EncryptedFlagValue = -1, StorageLocation = (eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo), DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemMultiCheckbox, Common.Public")]
		MultiCheckBox = 500,
		// Token: 0x040015B8 RID: 5560
		[DynamicControl("Multi-CheckBox With Text", "", "", true, "", PresentationDataType = typeof(string), EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.Setting3, EncryptedFlagValue = 1, StorageLocation = eDynamicDataStorageLocation.Unknown, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemMultiCheckboxWithText, Common.Public")]
		MultiCheckBoxText = 510,
		// Token: 0x040015B9 RID: 5561
		[DynamicControl("Multi-CheckBox With Drop List", "", "", true, "", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.Unknown, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemMultiCheckboxWithDroplist, Common.Public")]
		MultiCheckBoxDropList = 520,
		// Token: 0x040015BA RID: 5562
		[DynamicControl("Multi-Label Header", "", "", false, "")]
		MultiLabelHeader = 530,
		// Token: 0x040015BB RID: 5563
		[DynamicControl("Rich TextBox", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlTextBox, Common.UI.Web.DynamicControls", true, "A rich text box is the same as a textbox, but allows the user to use rich text, such as bold, bullets, etc., and also allows unlimited text.  This data is better for storing large amounts of text but more difficult to do reporting on.", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.ImageInfo, EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.Setting3, EncryptedFlagValue = 1, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemRichTextbox, Common.Public")]
		RtfTextBox = 600,
		// Token: 0x040015BC RID: 5564
		[DynamicControl("Multi-Line TextBox", "", "", true, "", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemMultiLineTextbox, Common.Public")]
		MultiLineTextBox = 620,
		// Token: 0x040015BD RID: 5565
		[DynamicControl("Accommodation CheckBox", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlCheckBox, Common.UI.Web.DynamicControls", true, "", PresentationDataType = typeof(bool), StorageLocation = eDynamicDataStorageLocation.MainInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemAccommodationCheckbox, Common.Public")]
		AccommodationCheckBox = 700,
		// Token: 0x040015BE RID: 5566
		[DynamicControl("Accommodation TextBox", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlTextBox, Common.UI.Web.DynamicControls", true, "", PresentationDataType = typeof(string), StorageLocation = eDynamicDataStorageLocation.OtherInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemAccommodationTextbox, Common.Public")]
		AccommodationTextBox,
		// Token: 0x040015BF RID: 5567
		[DynamicControl("AccommodationDatePicker", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlDatePicker, Common.UI.Web.DynamicControls", true, "", PresentationDataType = typeof(DateTime), StorageLocation = eDynamicDataStorageLocation.DateTimeInfo, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemAccommodationDateChooser, Common.Public")]
		AccommodationDatePicker,
		// Token: 0x040015C0 RID: 5568
		[DynamicControl("Accommodation Drop List", "", "TechnoPro.Common.UI.Web.DynamicControls.Controls.CtrlDropList, Common.UI.Web.DynamicControls", true, "", PresentationDataType = typeof(string), StorageLocation = (eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo), EncryptedFlagEncryptionProperty = eDynamicControlPropertyEncryptionProperty.Setting3, EncryptedFlagValue = -1, DynamicDataItemClass = "TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation.DynamicDataItemAccommodationDropList, Common.Public")]
		AccommodationDropList,
		// Token: 0x040015C1 RID: 5569
		[DynamicControl("Form Settings", "", "", true, "A form settings control allows specification of form attributes, and also optional c# code behind for a form.  There should only be one of these per form.  This control cannot store data.")]
		FormSettings = 800,
		// Token: 0x040015C2 RID: 5570
		[DynamicControl("Dynamic Controls Chooser", "", "", true, "", PresentationDataType = typeof(string))]
		DynamicControlsChooser,
		// Token: 0x040015C3 RID: 5571
		[DynamicControl("Multi-Database-Item Chooser", "", "", true, "", PresentationDataType = typeof(string))]
		MultiDatabaseitemChooser,
		// Token: 0x040015C4 RID: 5572
		[DynamicControl("Info Display Box", "", "", true, "")]
		InfoDisplayBox,
		// Token: 0x040015C5 RID: 5573
		[DynamicControl("Calculation", "", "", true, "")]
		CalcButton,
		// Token: 0x040015C6 RID: 5574
		[DynamicControl("Cases Table", "", "", true, "")]
		PMTable,
		// Token: 0x040015C7 RID: 5575
		[DynamicControl("Cases Drop List", "", "", true, "")]
		CaseComboBox,
		// Token: 0x040015C8 RID: 5576
		[DynamicControl("Email History", "", "", false, "")]
		EmailHistory,
		// Token: 0x040015C9 RID: 5577
		[DynamicControl("Appointment History", "", "", false, "")]
		AppointmentHistory
	}
}
