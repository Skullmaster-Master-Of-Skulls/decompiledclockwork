namespace System.Web.UI.Design.Util
{
	// Token: 0x02000166 RID: 358
	internal abstract partial class TaskFormBase : global::System.Web.UI.Design.Util.DesignerForm
	{
		// Token: 0x06000CB5 RID: 3253 RVA: 0x00051AEC File Offset: 0x0004FCEC
		private void InitializeComponent()
		{
			this._taskPanel = new global::System.Windows.Forms.Panel();
			this._bottomDividerLabel = new global::System.Windows.Forms.Label();
			this._captionLabel = new global::System.Windows.Forms.Label();
			this._headerPanel = new global::System.Windows.Forms.Panel();
			this._glyphPictureBox = new global::System.Windows.Forms.PictureBox();
			this._headerPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this._glyphPictureBox).BeginInit();
			base.SuspendLayout();
			this._taskPanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._taskPanel.Location = new global::System.Drawing.Point(14, 78);
			this._taskPanel.Name = "_taskPanel";
			this._taskPanel.Size = new global::System.Drawing.Size(544, 274);
			this._taskPanel.TabIndex = 30;
			this._bottomDividerLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._bottomDividerLabel.BackColor = global::System.Drawing.SystemColors.Window;
			this._bottomDividerLabel.Location = new global::System.Drawing.Point(0, 366);
			this._bottomDividerLabel.Name = "_bottomDividerLabel";
			this._bottomDividerLabel.Size = new global::System.Drawing.Size(572, 1);
			this._bottomDividerLabel.TabIndex = 40;
			this._headerPanel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._headerPanel.BackColor = global::System.Drawing.SystemColors.Window;
			this._headerPanel.Controls.Add(this._glyphPictureBox);
			this._headerPanel.Controls.Add(this._captionLabel);
			this._headerPanel.Location = new global::System.Drawing.Point(0, 0);
			this._headerPanel.Name = "_headerPanel";
			this._headerPanel.Size = new global::System.Drawing.Size(572, 64);
			this._headerPanel.TabIndex = 10;
			this._glyphPictureBox.Location = new global::System.Drawing.Point(0, 0);
			this._glyphPictureBox.Name = "_glyphPictureBox";
			this._glyphPictureBox.Size = new global::System.Drawing.Size(65, 64);
			this._glyphPictureBox.TabIndex = 20;
			this._glyphPictureBox.TabStop = false;
			this._captionLabel.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this._captionLabel.Location = new global::System.Drawing.Point(71, 17);
			this._captionLabel.Name = "_captionLabel";
			this._captionLabel.Size = new global::System.Drawing.Size(487, 47);
			this._captionLabel.TabIndex = 10;
			base.ClientSize = new global::System.Drawing.Size(572, 416);
			base.Controls.Add(this._headerPanel);
			base.Controls.Add(this._bottomDividerLabel);
			base.Controls.Add(this._taskPanel);
			this.MinimumSize = new global::System.Drawing.Size(580, 450);
			base.Name = "TaskForm";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			this._headerPanel.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this._glyphPictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040007B1 RID: 1969
		private global::System.Windows.Forms.Panel _taskPanel;

		// Token: 0x040007B2 RID: 1970
		private global::System.Windows.Forms.Label _bottomDividerLabel;

		// Token: 0x040007B3 RID: 1971
		private global::System.Windows.Forms.Panel _headerPanel;

		// Token: 0x040007B4 RID: 1972
		private global::System.Windows.Forms.Label _captionLabel;

		// Token: 0x040007B5 RID: 1973
		private global::System.Windows.Forms.PictureBox _glyphPictureBox;
	}
}
