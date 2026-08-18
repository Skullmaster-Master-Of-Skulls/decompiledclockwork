using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x02000084 RID: 132
	public partial class AppDelete : Form
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0002ABBF File Offset: 0x00029BBF
		public AppDelete()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002ABE0 File Offset: 0x00029BE0
		public AppDelete(AppDeleteFunctionality appDeleteFunctionality)
		{
			this.appDeleteFunctionality = appDeleteFunctionality;
			this.InitializeComponent();
			if (appDeleteFunctionality == AppDeleteFunctionality.CheckboxIsHidden)
			{
				this.btn_deleteThisApp.Enabled = true;
				this.btn_removeMeFromThisApp.Enabled = true;
				this.chk_iUnderstand.Visible = false;
			}
			else if (appDeleteFunctionality == AppDeleteFunctionality.CheckboxDisablesButton1)
			{
				this.btn_deleteThisApp.Enabled = true;
			}
			else if (appDeleteFunctionality == AppDeleteFunctionality.CheckboxDisablesButton2)
			{
				this.btn_removeMeFromThisApp.Enabled = true;
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0002AC80 File Offset: 0x00029C80
		public static AppDeleteButton ShowAppDelete(IWin32Window owner, string text, string caption, string button1Text, string button2Text, string button3Text, AppDeleteButtonLayout layout, AppDeleteFunctionality appDeleteFunctionality, string checkboxText)
		{
			AppDelete appDelete = new AppDelete(appDeleteFunctionality);
			if (!string.IsNullOrEmpty(button1Text))
			{
				appDelete.Button1Text = button1Text;
			}
			if (!string.IsNullOrEmpty(button2Text))
			{
				appDelete.Button2Text = button2Text;
			}
			if (!string.IsNullOrEmpty(button3Text))
			{
				appDelete.Button3Text = button3Text;
			}
			if (layout == AppDeleteButtonLayout.Button2Button3)
			{
				appDelete.DisableRemoveMeFromThisApp();
			}
			if (!string.IsNullOrEmpty(checkboxText))
			{
				appDelete.CheckboxIUnderstandText = checkboxText;
			}
			appDelete.Title = caption;
			appDelete.Text = text;
			DialogResult dialogResult = appDelete.ShowDialog(owner);
			AppDeleteButton result;
			if (dialogResult == DialogResult.Yes)
			{
				result = AppDeleteButton.Button2;
			}
			else if (dialogResult == DialogResult.No)
			{
				result = AppDeleteButton.Button1;
			}
			else if (dialogResult == DialogResult.Cancel)
			{
				result = AppDeleteButton.Button3;
			}
			else
			{
				result = AppDeleteButton.None;
			}
			return result;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0002B6D8 File Offset: 0x0002A6D8
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0002B6F0 File Offset: 0x0002A6F0
		public AppDeleteFunctionality AppDeleteFunctionality
		{
			get
			{
				return this.appDeleteFunctionality;
			}
			set
			{
				this.appDeleteFunctionality = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0002B6FC File Offset: 0x0002A6FC
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0002B719 File Offset: 0x0002A719
		public string Title
		{
			get
			{
				return this.lbl_title.Text;
			}
			set
			{
				this.lbl_title.Text = value;
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002B729 File Offset: 0x0002A729
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0002B734 File Offset: 0x0002A734
		public int ButtonClicked
		{
			get
			{
				return this.buttonClicked;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002B74C File Offset: 0x0002A74C
		private void btn_removeMeFromThisApp_Click(object sender, EventArgs e)
		{
			this.buttonClicked = 1;
			base.DialogResult = DialogResult.No;
			base.Close();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002B765 File Offset: 0x0002A765
		private void btn_deleteThisApp_Click(object sender, EventArgs e)
		{
			this.buttonClicked = 2;
			base.DialogResult = DialogResult.Yes;
			base.Close();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002B77E File Offset: 0x0002A77E
		public void DisableRemoveMeFromThisApp()
		{
			base.Height -= this.btn_removeMeFromThisApp.Height;
			this.btn_removeMeFromThisApp.Visible = false;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002B7A7 File Offset: 0x0002A7A7
		private void AppDelete_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0002B7AA File Offset: 0x0002A7AA
		public void SetOkButtonText(string text, Image image)
		{
			this.btn_deleteThisApp.Text = text;
			this.btn_deleteThisApp.Image = image;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0002B7C8 File Offset: 0x0002A7C8
		private void chk_iUnderstand_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.chk_iUnderstand.Checked;
			if (this.appDeleteFunctionality == AppDeleteFunctionality.CheckboxDisablesButton1And2)
			{
				this.btn_deleteThisApp.Enabled = @checked;
				this.btn_removeMeFromThisApp.Enabled = @checked;
			}
			else if (this.appDeleteFunctionality == AppDeleteFunctionality.CheckboxDisablesButton1)
			{
				this.btn_removeMeFromThisApp.Enabled = @checked;
			}
			else if (this.appDeleteFunctionality == AppDeleteFunctionality.CheckboxDisablesButton2)
			{
				this.btn_deleteThisApp.Enabled = @checked;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0002B84C File Offset: 0x0002A84C
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0002B869 File Offset: 0x0002A869
		public string CheckboxIUnderstandText
		{
			get
			{
				return this.chk_iUnderstand.Text;
			}
			set
			{
				this.chk_iUnderstand.Text = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0002B87C File Offset: 0x0002A87C
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0002B899 File Offset: 0x0002A899
		public string Button1Text
		{
			get
			{
				return this.btn_removeMeFromThisApp.Text;
			}
			set
			{
				this.btn_removeMeFromThisApp.Text = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0002B8AC File Offset: 0x0002A8AC
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0002B8C9 File Offset: 0x0002A8C9
		public string Button2Text
		{
			get
			{
				return this.btn_deleteThisApp.Text;
			}
			set
			{
				this.btn_deleteThisApp.Text = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0002B8DC File Offset: 0x0002A8DC
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0002B8F9 File Offset: 0x0002A8F9
		public string Button3Text
		{
			get
			{
				return this.btn_cancel.Text;
			}
			set
			{
				this.btn_cancel.Text = value;
			}
		}

		// Token: 0x04000460 RID: 1120
		private AppDeleteFunctionality appDeleteFunctionality = AppDeleteFunctionality.CheckboxDisablesButton1And2;

		// Token: 0x04000461 RID: 1121
		private int buttonClicked = 0;
	}
}
