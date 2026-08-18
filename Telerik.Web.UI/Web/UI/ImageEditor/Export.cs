using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000520 RID: 1312
	[ToolboxItem(false)]
	public class Export : ImageEditorDialog
	{
		// Token: 0x06002ED9 RID: 11993 RVA: 0x0009905D File Offset: 0x0009725D
		public Export(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x00099067 File Offset: 0x00097267
		public override string DialogName
		{
			get
			{
				return "Export";
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x0009906E File Offset: 0x0009726E
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x00099075 File Offset: 0x00097275
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Export_Title;
			}
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x0009908C File Offset: 0x0009728C
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			this._cbOverwrite = (CheckBox)base.FindControlRecursive("cbOverwrite");
			this._btnList = (RadioButtonList)base.FindControlRecursive("btnList");
			this._saveFileName = (Label)base.FindControlRecursive("saveFileName");
			this._txtFileName = (TextBox)base.FindControlRecursive("TxtFileName");
			this._btnCancel = (RadButton)base.FindControlRecursive("btnCancel");
			this._btnOk = (RadButton)base.FindControlRecursive("btnOk");
			this._ddExtension = (RadDropDownList)base.FindControlRecursive("fileExtension");
			if (this._btnList != null)
			{
				this._btnList.Items[0].Text = dialogs.Export_ClientRadioText;
				this._btnList.Items[1].Text = dialogs.Export_ServerRadioText;
				switch (base.ParentImageEditor.AllowedSavingLocation)
				{
				case AllowedSavingLocation.Client:
					this._btnList.Items[1].Enabled = false;
					this._btnList.Items[0].Selected = true;
					this._cbOverwrite.Enabled = false;
					break;
				case AllowedSavingLocation.Server:
					this._btnList.Items[0].Enabled = false;
					this._btnList.Items[1].Selected = true;
					break;
				default:
					this._btnList.Items[0].Selected = true;
					break;
				}
			}
			if (this._saveFileName != null)
			{
				this._saveFileName.Text = dialogs.Export_FileNameLabel + ": ";
			}
			if (this._txtFileName != null)
			{
				this._txtFileName.ToolTip = dialogs.Export_FileNameToolTip;
				this._txtFileName.Text = base.ParentImageEditor.ExtractFileNameFromImageUrl();
			}
			if (this._ddExtension != null)
			{
				this._ddExtension.ToolTip = dialogs.Export_ChooseExtension;
				base.SetChildControlRenderMode(this._ddExtension);
				DropDownListItem dropDownListItem = this._ddExtension.Items.FindChildByValue<DropDownListItem>(this.GetImageExtension());
				if (dropDownListItem != null)
				{
					dropDownListItem.Selected = true;
				}
			}
			if (this._cbOverwrite != null)
			{
				this._cbOverwrite.Text = dialogs.Common_OverwriteExisting;
			}
			if (this._btnOk != null)
			{
				this._btnOk.Text = (this._btnOk.ToolTip = dialogs.Common_OK);
				base.SetChildControlRenderMode(this._btnOk);
			}
			if (this._btnCancel != null)
			{
				this._btnCancel.Text = (this._btnCancel.ToolTip = dialogs.Common_Cancel);
				base.SetChildControlRenderMode(this._btnCancel);
			}
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x00099347 File Offset: 0x00097547
		private string GetImageExtension()
		{
			return base.ParentImageEditor.GetEditableImage().Format;
		}

		// Token: 0x04000C4F RID: 3151
		private RadioButtonList _btnList;

		// Token: 0x04000C50 RID: 3152
		private Label _saveFileName;

		// Token: 0x04000C51 RID: 3153
		private TextBox _txtFileName;

		// Token: 0x04000C52 RID: 3154
		private CheckBox _cbOverwrite;

		// Token: 0x04000C53 RID: 3155
		private RadButton _btnOk;

		// Token: 0x04000C54 RID: 3156
		private RadButton _btnCancel;

		// Token: 0x04000C55 RID: 3157
		private RadDropDownList _ddExtension;
	}
}
