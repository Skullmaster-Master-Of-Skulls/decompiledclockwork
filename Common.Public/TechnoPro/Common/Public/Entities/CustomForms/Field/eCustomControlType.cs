using System;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.Common.Public.Entities.CustomForms.Field
{
	// Token: 0x0200041F RID: 1055
	[Serializable]
	public enum eCustomControlType
	{
		// Token: 0x04001884 RID: 6276
		[CustomControlType(IsHidden = true)]
		Unknown,
		// Token: 0x04001885 RID: 6277
		[CustomControlType("chk", "Checkbox", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.Boolean
		})]
		CheckBox,
		// Token: 0x04001886 RID: 6278
		[CustomControlType("txt", "Textbox", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.String
		})]
		TextBox,
		// Token: 0x04001887 RID: 6279
		[CustomControlType("cmb", "Drop list", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.ListItem
		})]
		DropList,
		// Token: 0x04001888 RID: 6280
		[CustomControlType("rtb", "Rich textbox", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.String
		})]
		RichTextBox,
		// Token: 0x04001889 RID: 6281
		[CustomControlType("radio", "Radio button group", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.ListItem
		})]
		RadioGroup,
		// Token: 0x0400188A RID: 6282
		[CustomControlType("grp", "Group box", new eCustomDataPrimitiveType[]
		{

		})]
		GroupBox,
		// Token: 0x0400188B RID: 6283
		[CustomControlType("lbl", "Static label", new eCustomDataPrimitiveType[]
		{

		})]
		Label,
		// Token: 0x0400188C RID: 6284
		[CustomControlType("captcha", "Captcha control", new eCustomDataPrimitiveType[]
		{

		})]
		Captcha,
		// Token: 0x0400188D RID: 6285
		[CustomControlType("blank", "Blank space", new eCustomDataPrimitiveType[]
		{

		})]
		BlankSpace,
		// Token: 0x0400188E RID: 6286
		[CustomControlType("txtn", "Number textbox", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.Int
		})]
		TextBoxNumber,
		// Token: 0x0400188F RID: 6287
		[CustomControlType("file", "File", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.File
		})]
		File,
		// Token: 0x04001890 RID: 6288
		[CustomControlType("filelist", "File list", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.File
		})]
		FileList,
		// Token: 0x04001891 RID: 6289
		[CustomControlType("yesno", "Yes/no chooser", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.BooleanNullable
		})]
		CustomYesNoChooser,
		// Token: 0x04001892 RID: 6290
		[CustomControlType("grppop", "Popup group box", new eCustomDataPrimitiveType[]
		{

		})]
		GroupBoxPopup,
		// Token: 0x04001893 RID: 6291
		[CustomControlType("txtlist", "Textbox list", new eCustomDataPrimitiveType[]
		{
			eCustomDataPrimitiveType.String
		})]
		TextBoxList
	}
}
