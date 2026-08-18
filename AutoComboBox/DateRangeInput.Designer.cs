namespace AutoComboBox
{
	// Token: 0x020000EC RID: 236
	public partial class DateRangeInput : global::System.Windows.Forms.Form
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x000489F4 File Offset: 0x000479F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00048A30 File Offset: 0x00047A30
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.DateRangeInput));
			this.p_endTime = new global::System.Windows.Forms.Panel();
			this.dtp_endDate = new global::System.Windows.Forms.DateTimePicker();
			this.lbl_endTime = new global::System.Windows.Forms.Label();
			this.p_startTime = new global::System.Windows.Forms.Panel();
			this.dtp_startDate = new global::System.Windows.Forms.DateTimePicker();
			this.lbl_startTime = new global::System.Windows.Forms.Label();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_fakeOK = new global::System.Windows.Forms.Button();
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_goBackward = new global::System.Windows.Forms.ToolStripButton();
			this.btn_goForward = new global::System.Windows.Forms.ToolStripButton();
			this.btn_goToToday = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.btn_set1week = new global::System.Windows.Forms.Button();
			this.btn_set1month = new global::System.Windows.Forms.Button();
			this.btn_setToCurrentTerm = new global::System.Windows.Forms.Button();
			this.p_endTime.SuspendLayout();
			this.p_startTime.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.p_endTime.Controls.Add(this.dtp_endDate);
			this.p_endTime.Controls.Add(this.lbl_endTime);
			this.p_endTime.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_endTime.Location = new global::System.Drawing.Point(0, 66);
			this.p_endTime.Name = "p_endTime";
			this.p_endTime.Padding = new global::System.Windows.Forms.Padding(0, 1, 30, 1);
			this.p_endTime.Size = new global::System.Drawing.Size(402, 36);
			this.p_endTime.TabIndex = 3;
			this.dtp_endDate.CustomFormat = "MMMM dd, yyyy";
			this.dtp_endDate.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dtp_endDate.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.dtp_endDate.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_endDate.Location = new global::System.Drawing.Point(106, 1);
			this.dtp_endDate.Name = "dtp_endDate";
			this.dtp_endDate.Size = new global::System.Drawing.Size(266, 29);
			this.dtp_endDate.TabIndex = 6;
			this.dtp_endDate.Value = new global::System.DateTime(2004, 6, 25, 18, 52, 2, 945);
			this.dtp_endDate.Visible = false;
			this.dtp_endDate.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.dtp_startDate_KeyPress);
			this.lbl_endTime.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.lbl_endTime.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_endTime.Location = new global::System.Drawing.Point(0, 1);
			this.lbl_endTime.Name = "lbl_endTime";
			this.lbl_endTime.Size = new global::System.Drawing.Size(106, 34);
			this.lbl_endTime.TabIndex = 4;
			this.lbl_endTime.Text = "End Date:";
			this.lbl_endTime.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl_endTime.Visible = false;
			this.p_startTime.Controls.Add(this.dtp_startDate);
			this.p_startTime.Controls.Add(this.lbl_startTime);
			this.p_startTime.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_startTime.Location = new global::System.Drawing.Point(0, 32);
			this.p_startTime.Name = "p_startTime";
			this.p_startTime.Padding = new global::System.Windows.Forms.Padding(0, 1, 30, 1);
			this.p_startTime.Size = new global::System.Drawing.Size(402, 34);
			this.p_startTime.TabIndex = 2;
			this.dtp_startDate.CustomFormat = "MMMM dd, yyyy";
			this.dtp_startDate.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dtp_startDate.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.dtp_startDate.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_startDate.Location = new global::System.Drawing.Point(106, 1);
			this.dtp_startDate.Name = "dtp_startDate";
			this.dtp_startDate.Size = new global::System.Drawing.Size(266, 29);
			this.dtp_startDate.TabIndex = 3;
			this.dtp_startDate.Value = new global::System.DateTime(2004, 6, 25, 18, 52, 3, 15);
			this.dtp_startDate.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.dtp_startDate_KeyDown);
			this.lbl_startTime.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.lbl_startTime.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_startTime.Location = new global::System.Drawing.Point(0, 1);
			this.lbl_startTime.Name = "lbl_startTime";
			this.lbl_startTime.Size = new global::System.Drawing.Size(106, 32);
			this.lbl_startTime.TabIndex = 0;
			this.lbl_startTime.Text = "Start Date:";
			this.lbl_startTime.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(402, 32);
			this.label1.TabIndex = 5;
			this.btn_fakeOK.Location = new global::System.Drawing.Point(66, 0);
			this.btn_fakeOK.Name = "btn_fakeOK";
			this.btn_fakeOK.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeOK.TabIndex = 6;
			this.btn_fakeOK.TabStop = false;
			this.btn_fakeOK.Text = "button1";
			this.btn_fakeOK.Click += new global::System.EventHandler(this.btn_fakeOK_Click);
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(201, 76);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 7;
			this.btn_fakeCancel.Text = "button1";
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_goBackward,
				this.btn_goForward,
				this.btn_goToToday,
				this.toolStripSeparator1,
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 140);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(402, 39);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.TabStop = true;
			this.btn_goBackward.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btn_goBackward.Image = global::AutoComboBox.Properties.Resources.nav_left_blue;
			this.btn_goBackward.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_goBackward.Name = "btn_goBackward";
			this.btn_goBackward.Size = new global::System.Drawing.Size(36, 36);
			this.btn_goBackward.Text = "Go backward";
			this.btn_goBackward.Click += new global::System.EventHandler(this.btn_goBackward_Click);
			this.btn_goForward.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btn_goForward.Image = global::AutoComboBox.Properties.Resources.nav_right_blue;
			this.btn_goForward.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_goForward.Name = "btn_goForward";
			this.btn_goForward.Size = new global::System.Drawing.Size(36, 36);
			this.btn_goForward.Text = "Go forward";
			this.btn_goForward.Click += new global::System.EventHandler(this.btn_goForward_Click);
			this.btn_goToToday.Image = global::AutoComboBox.Properties.Resources.home;
			this.btn_goToToday.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_goToToday.Name = "btn_goToToday";
			this.btn_goToToday.Size = new global::System.Drawing.Size(85, 36);
			this.btn_goToToday.Text = "Today";
			this.btn_goToToday.Click += new global::System.EventHandler(this.btn_goToToday_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_ok.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.btn_set1week.Font = new global::System.Drawing.Font("Arial", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_set1week.Location = new global::System.Drawing.Point(106, 108);
			this.btn_set1week.Name = "btn_set1week";
			this.btn_set1week.Size = new global::System.Drawing.Size(80, 23);
			this.btn_set1week.TabIndex = 9;
			this.btn_set1week.Text = "Set 1 week";
			this.btn_set1week.UseVisualStyleBackColor = true;
			this.btn_set1week.Visible = false;
			this.btn_set1week.Click += new global::System.EventHandler(this.btn_set1week_Click);
			this.btn_set1month.Font = new global::System.Drawing.Font("Arial", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_set1month.Location = new global::System.Drawing.Point(192, 108);
			this.btn_set1month.Name = "btn_set1month";
			this.btn_set1month.Size = new global::System.Drawing.Size(80, 23);
			this.btn_set1month.TabIndex = 10;
			this.btn_set1month.Text = "Set 1 month";
			this.btn_set1month.UseVisualStyleBackColor = true;
			this.btn_set1month.Visible = false;
			this.btn_set1month.Click += new global::System.EventHandler(this.btn_set1month_Click);
			this.btn_setToCurrentTerm.Font = new global::System.Drawing.Font("Arial", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_setToCurrentTerm.Location = new global::System.Drawing.Point(278, 108);
			this.btn_setToCurrentTerm.Name = "btn_setToCurrentTerm";
			this.btn_setToCurrentTerm.Size = new global::System.Drawing.Size(117, 23);
			this.btn_setToCurrentTerm.TabIndex = 11;
			this.btn_setToCurrentTerm.Text = "Set to current term";
			this.btn_setToCurrentTerm.UseVisualStyleBackColor = true;
			this.btn_setToCurrentTerm.Visible = false;
			this.btn_setToCurrentTerm.Click += new global::System.EventHandler(this.btn_setToCurrentTerm_Click);
			base.AcceptButton = this.btn_fakeOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(402, 179);
			base.Controls.Add(this.btn_setToCurrentTerm);
			base.Controls.Add(this.btn_set1month);
			base.Controls.Add(this.btn_set1week);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.btn_fakeOK);
			base.Controls.Add(this.p_endTime);
			base.Controls.Add(this.p_startTime);
			base.Controls.Add(this.label1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.KeyPreview = true;
			base.Name = "DateRangeInput";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Please enter the appropriate dates:";
			base.Load += new global::System.EventHandler(this.DateRangeInput_Load);
			this.p_endTime.ResumeLayout(false);
			this.p_startTime.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040006C0 RID: 1728
		private global::System.Windows.Forms.Panel p_endTime;

		// Token: 0x040006C1 RID: 1729
		private global::System.Windows.Forms.Label lbl_endTime;

		// Token: 0x040006C2 RID: 1730
		private global::System.Windows.Forms.Panel p_startTime;

		// Token: 0x040006C3 RID: 1731
		private global::System.Windows.Forms.Label lbl_startTime;

		// Token: 0x040006C4 RID: 1732
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x040006C5 RID: 1733
		private global::System.Windows.Forms.DateTimePicker dtp_endDate;

		// Token: 0x040006C6 RID: 1734
		private global::System.Windows.Forms.DateTimePicker dtp_startDate;

		// Token: 0x040006C7 RID: 1735
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040006C8 RID: 1736
		private global::System.Windows.Forms.Button btn_fakeOK;

		// Token: 0x040006C9 RID: 1737
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x040006CA RID: 1738
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040006CB RID: 1739
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x040006CC RID: 1740
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x040006CD RID: 1741
		private global::System.Windows.Forms.Button btn_set1week;

		// Token: 0x040006CE RID: 1742
		private global::System.Windows.Forms.Button btn_set1month;

		// Token: 0x040006CF RID: 1743
		private global::System.Windows.Forms.Button btn_setToCurrentTerm;

		// Token: 0x040006D0 RID: 1744
		private global::System.Windows.Forms.ToolStripButton btn_goBackward;

		// Token: 0x040006D1 RID: 1745
		private global::System.Windows.Forms.ToolStripButton btn_goForward;

		// Token: 0x040006D2 RID: 1746
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040006D3 RID: 1747
		private global::System.Windows.Forms.ToolStripButton btn_goToToday;

		// Token: 0x040006D4 RID: 1748
		private global::System.ComponentModel.IContainer components;
	}
}
