namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004D9 RID: 1241
	internal partial class SqlDataSourceQueryEditorForm : global::System.Web.UI.Design.Util.DesignerForm
	{
		// Token: 0x06002C93 RID: 11411 RVA: 0x000FAED8 File Offset: 0x000F9ED8
		private void InitializeComponent()
		{
			this._okButton = new global::System.Windows.Forms.Button();
			this._cancelButton = new global::System.Windows.Forms.Button();
			this._inferParametersButton = new global::System.Windows.Forms.Button();
			this._queryBuilderButton = new global::System.Windows.Forms.Button();
			this._commandLabel = new global::System.Windows.Forms.Label();
			this._commandTextBox = new global::System.Windows.Forms.TextBox();
			this._parameterEditorUserControl = new global::System.Web.UI.Design.WebControls.ParameterEditorUserControl(base.ServiceProvider, (global::System.Web.UI.WebControls.SqlDataSource)this._sqlDataSourceDesigner.Component);
			base.SuspendLayout();
			this._okButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._okButton.Location = new global::System.Drawing.Point(377, 379);
			this._okButton.TabIndex = 150;
			this._okButton.Click += new global::System.EventHandler(this.OnOkButtonClick);
			this._cancelButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this._cancelButton.Location = new global::System.Drawing.Point(457, 379);
			this._cancelButton.TabIndex = 160;
			this._cancelButton.Click += new global::System.EventHandler(this.OnCancelButtonClick);
			this._commandLabel.Location = new global::System.Drawing.Point(12, 12);
			this._commandLabel.Size = new global::System.Drawing.Size(200, 16);
			this._commandLabel.TabIndex = 10;
			this._commandTextBox.AcceptsReturn = true;
			this._commandTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._commandTextBox.Location = new global::System.Drawing.Point(12, 30);
			this._commandTextBox.Multiline = true;
			this._commandTextBox.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this._commandTextBox.Size = new global::System.Drawing.Size(520, 78);
			this._commandTextBox.TabIndex = 20;
			this._inferParametersButton.AutoSize = true;
			this._inferParametersButton.Location = new global::System.Drawing.Point(12, 112);
			this._inferParametersButton.Size = new global::System.Drawing.Size(128, 23);
			this._inferParametersButton.TabIndex = 30;
			this._inferParametersButton.Click += new global::System.EventHandler(this.OnInferParametersButtonClick);
			this._queryBuilderButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this._queryBuilderButton.AutoSize = true;
			this._queryBuilderButton.Location = new global::System.Drawing.Point(404, 112);
			this._queryBuilderButton.Size = new global::System.Drawing.Size(128, 23);
			this._queryBuilderButton.TabIndex = 40;
			this._queryBuilderButton.Click += new global::System.EventHandler(this.OnQueryBuilderButtonClick);
			this._parameterEditorUserControl.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._parameterEditorUserControl.Location = new global::System.Drawing.Point(12, 144);
			this._parameterEditorUserControl.Size = new global::System.Drawing.Size(520, 224);
			this._parameterEditorUserControl.TabIndex = 50;
			base.AcceptButton = this._okButton;
			base.CancelButton = this._cancelButton;
			base.ClientSize = new global::System.Drawing.Size(544, 410);
			base.Controls.Add(this._queryBuilderButton);
			base.Controls.Add(this._inferParametersButton);
			base.Controls.Add(this._commandTextBox);
			base.Controls.Add(this._commandLabel);
			base.Controls.Add(this._cancelButton);
			base.Controls.Add(this._okButton);
			base.Controls.Add(this._parameterEditorUserControl);
			this.MinimumSize = new global::System.Drawing.Size(488, 440);
			base.InitializeForm();
			base.ResumeLayout(false);
		}

		// Token: 0x04001E61 RID: 7777
		private global::System.Windows.Forms.Label _commandLabel;

		// Token: 0x04001E62 RID: 7778
		private global::System.Windows.Forms.TextBox _commandTextBox;

		// Token: 0x04001E63 RID: 7779
		private global::System.Web.UI.Design.WebControls.ParameterEditorUserControl _parameterEditorUserControl;

		// Token: 0x04001E64 RID: 7780
		private global::System.Windows.Forms.Button _okButton;

		// Token: 0x04001E65 RID: 7781
		private global::System.Windows.Forms.Button _cancelButton;

		// Token: 0x04001E66 RID: 7782
		private global::System.Windows.Forms.Button _inferParametersButton;

		// Token: 0x04001E67 RID: 7783
		private global::System.Windows.Forms.Button _queryBuilderButton;

		// Token: 0x04001E6D RID: 7789
		private global::System.Web.UI.Design.WebControls.SqlDataSourceDesigner _sqlDataSourceDesigner;
	}
}
