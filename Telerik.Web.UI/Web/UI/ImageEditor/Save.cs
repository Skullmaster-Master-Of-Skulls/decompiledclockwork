using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E4E RID: 3662
	[ToolboxItem(false)]
	public class Save : ImageEditorDialog
	{
		// Token: 0x06008AE2 RID: 35554 RVA: 0x001FA1AA File Offset: 0x001F83AA
		public Save(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002BDD RID: 11229
		// (get) Token: 0x06008AE3 RID: 35555 RVA: 0x001FA1B4 File Offset: 0x001F83B4
		public override string DialogName
		{
			get
			{
				return "Save";
			}
		}

		// Token: 0x17002BDE RID: 11230
		// (get) Token: 0x06008AE4 RID: 35556 RVA: 0x001FA1BB File Offset: 0x001F83BB
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002BDF RID: 11231
		// (get) Token: 0x06008AE5 RID: 35557 RVA: 0x001FA1C2 File Offset: 0x001F83C2
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Save_Title;
			}
		}

		// Token: 0x06008AE6 RID: 35558 RVA: 0x001FA1DC File Offset: 0x001F83DC
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
			if (this._btnList != null)
			{
				this._btnList.Items[0].Text = dialogs.Save_ClientRadioText;
				this._btnList.Items[1].Text = dialogs.Save_ServerRadioText;
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
				this._saveFileName.Text = dialogs.Save_FileNameLabel + ": ";
			}
			if (this._txtFileName != null)
			{
				this._txtFileName.ToolTip = dialogs.Save_FileNameToolTip;
				this._txtFileName.Text = base.ParentImageEditor.ExtractFileNameFromImageUrl();
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

		// Token: 0x040026D9 RID: 9945
		private RadioButtonList _btnList;

		// Token: 0x040026DA RID: 9946
		private Label _saveFileName;

		// Token: 0x040026DB RID: 9947
		private TextBox _txtFileName;

		// Token: 0x040026DC RID: 9948
		private CheckBox _cbOverwrite;

		// Token: 0x040026DD RID: 9949
		private RadButton _btnOk;

		// Token: 0x040026DE RID: 9950
		private RadButton _btnCancel;
	}
}
