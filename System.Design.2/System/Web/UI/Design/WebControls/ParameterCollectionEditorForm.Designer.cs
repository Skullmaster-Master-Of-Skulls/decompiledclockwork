namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F6 RID: 246
	internal partial class ParameterCollectionEditorForm : global::System.Web.UI.Design.Util.DesignerForm
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x000303D8 File Offset: 0x0002E5D8
		private void InitializeComponent()
		{
			this._okButton = new global::System.Windows.Forms.Button();
			this._cancelButton = new global::System.Windows.Forms.Button();
			this._parameterEditorUserControl = new global::System.Web.UI.Design.WebControls.ParameterEditorUserControl(base.ServiceProvider, this._control);
			base.SuspendLayout();
			this._parameterEditorUserControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._parameterEditorUserControl.Location = new global::System.Drawing.Point(12, 12);
			this._parameterEditorUserControl.Size = new global::System.Drawing.Size(560, 278);
			this._parameterEditorUserControl.TabIndex = 10;
			this._okButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._okButton.Location = new global::System.Drawing.Point(416, 299);
			this._okButton.TabIndex = 20;
			this._okButton.Click += new global::System.EventHandler(this.OnOkButtonClick);
			this._cancelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._cancelButton.Location = new global::System.Drawing.Point(497, 299);
			this._cancelButton.TabIndex = 30;
			this._cancelButton.Click += new global::System.EventHandler(this.OnCancelButtonClick);
			base.AcceptButton = this._okButton;
			base.CancelButton = this._cancelButton;
			base.ClientSize = new global::System.Drawing.Size(584, 334);
			base.Controls.Add(this._parameterEditorUserControl);
			base.Controls.Add(this._cancelButton);
			base.Controls.Add(this._okButton);
			this.MinimumSize = new global::System.Drawing.Size(484, 272);
			base.InitializeForm();
			base.ResumeLayout(false);
		}

		// Token: 0x040004F4 RID: 1268
		private global::System.Web.UI.Control _control;

		// Token: 0x040004F5 RID: 1269
		private global::System.Windows.Forms.Button _okButton;

		// Token: 0x040004F6 RID: 1270
		private global::System.Windows.Forms.Button _cancelButton;

		// Token: 0x040004F7 RID: 1271
		private global::System.Web.UI.Design.WebControls.ParameterEditorUserControl _parameterEditorUserControl;
	}
}
