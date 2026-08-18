namespace AutoComboBox.MyControls
{
	// Token: 0x0200005E RID: 94
	public partial class AccommodationControlPopup : global::System.Windows.Forms.Form
	{
		// Token: 0x06000355 RID: 853 RVA: 0x0001A5A8 File Offset: 0x000195A8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001A5E0 File Offset: 0x000195E0
		private void InitializeComponent()
		{
			this.btn_clearExpiry = new global::System.Windows.Forms.Button();
			this.txt_rationale = new global::System.Windows.Forms.TextBox();
			this.chk_caption = new global::System.Windows.Forms.CheckBox();
			this.txt_letterText = new global::System.Windows.Forms.TextBox();
			this.chk_offline = new global::System.Windows.Forms.CheckBox();
			this.chk_letter = new global::System.Windows.Forms.CheckBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btn_requestChange = new global::System.Windows.Forms.Button();
			this.btn_ok = new global::System.Windows.Forms.Button();
			this.label4 = new global::System.Windows.Forms.Label();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.dtp_expiry = new global::AutoComboBox.MyDateTimePicker();
			this.chk_approved = new global::System.Windows.Forms.CheckBox();
			this.chk_recommendedToStudentButDeclined = new global::System.Windows.Forms.CheckBox();
			this.txt_recommendedToStudentButDeclinedDetail = new global::System.Windows.Forms.TextBox();
			this.p_top = new global::System.Windows.Forms.Panel();
			this.lbl_msg = new global::System.Windows.Forms.Label();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.p_top.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.btn_clearExpiry.Font = new global::System.Drawing.Font("Arial", 6f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_clearExpiry.Location = new global::System.Drawing.Point(271, 36);
			this.btn_clearExpiry.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btn_clearExpiry.Name = "btn_clearExpiry";
			this.btn_clearExpiry.Size = new global::System.Drawing.Size(38, 23);
			this.btn_clearExpiry.TabIndex = 7;
			this.btn_clearExpiry.Text = "Clear";
			this.btn_clearExpiry.UseVisualStyleBackColor = true;
			this.btn_clearExpiry.Click += new global::System.EventHandler(this.btn_clearExpiry_Click);
			this.txt_rationale.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txt_rationale.Location = new global::System.Drawing.Point(110, 67);
			this.txt_rationale.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_rationale.Multiline = true;
			this.txt_rationale.Name = "txt_rationale";
			this.txt_rationale.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txt_rationale.Size = new global::System.Drawing.Size(355, 42);
			this.txt_rationale.TabIndex = 9;
			this.chk_caption.AutoSize = true;
			this.chk_caption.Location = new global::System.Drawing.Point(347, 141);
			this.chk_caption.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chk_caption.Name = "chk_caption";
			this.chk_caption.Size = new global::System.Drawing.Size(92, 20);
			this.chk_caption.TabIndex = 19;
			this.chk_caption.Text = "checkBox1";
			this.chk_caption.UseVisualStyleBackColor = true;
			this.chk_caption.Visible = false;
			this.chk_caption.CheckedChanged += new global::System.EventHandler(this.chk_caption_CheckedChanged);
			this.txt_letterText.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txt_letterText.Location = new global::System.Drawing.Point(110, 117);
			this.txt_letterText.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_letterText.Multiline = true;
			this.txt_letterText.Name = "txt_letterText";
			this.txt_letterText.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txt_letterText.Size = new global::System.Drawing.Size(355, 93);
			this.txt_letterText.TabIndex = 11;
			this.chk_offline.AutoSize = true;
			this.chk_offline.Location = new global::System.Drawing.Point(12, 7);
			this.chk_offline.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chk_offline.Name = "chk_offline";
			this.chk_offline.Size = new global::System.Drawing.Size(63, 20);
			this.chk_offline.TabIndex = 3;
			this.chk_offline.Text = "&Offline";
			this.chk_offline.UseVisualStyleBackColor = true;
			this.chk_letter.AutoSize = true;
			this.chk_letter.Location = new global::System.Drawing.Point(12, 218);
			this.chk_letter.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chk_letter.Name = "chk_letter";
			this.chk_letter.Size = new global::System.Drawing.Size(110, 20);
			this.chk_letter.TabIndex = 12;
			this.chk_letter.Text = "Show on letter";
			this.chk_letter.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(9, 70);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(62, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Rationale";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(9, 40);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(74, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Expiry date";
			this.label3.Location = new global::System.Drawing.Point(9, 120);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(97, 41);
			this.label3.TabIndex = 10;
			this.label3.Text = "Additional &text for letter";
			this.panel1.Controls.Add(this.btn_requestChange);
			this.panel1.Controls.Add(this.btn_ok);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.btn_cancel);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new global::System.Drawing.Point(0, 371);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new global::System.Windows.Forms.Padding(4);
			this.panel1.Size = new global::System.Drawing.Size(477, 39);
			this.panel1.TabIndex = 15;
			this.btn_requestChange.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.btn_requestChange.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_requestChange.Location = new global::System.Drawing.Point(4, 4);
			this.btn_requestChange.Name = "btn_requestChange";
			this.btn_requestChange.Size = new global::System.Drawing.Size(155, 31);
			this.btn_requestChange.TabIndex = 16;
			this.btn_requestChange.Text = "&Request change";
			this.btn_requestChange.UseVisualStyleBackColor = true;
			this.btn_requestChange.Visible = false;
			this.btn_ok.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_ok.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_ok.Location = new global::System.Drawing.Point(306, 4);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(75, 31);
			this.btn_ok.TabIndex = 17;
			this.btn_ok.Text = "&Ok";
			this.btn_ok.UseVisualStyleBackColor = true;
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.label4.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.label4.Location = new global::System.Drawing.Point(381, 4);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(17, 31);
			this.label4.TabIndex = 2;
			this.btn_cancel.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_cancel.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_cancel.Location = new global::System.Drawing.Point(398, 4);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(75, 31);
			this.btn_cancel.TabIndex = 18;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.UseVisualStyleBackColor = true;
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.dtp_expiry.BaseValue = new global::System.DateTime(2008, 12, 6, 14, 32, 19, 691);
			this.dtp_expiry.CustomFormat = "MMMM dd, yyyy";
			this.dtp_expiry.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.dtp_expiry.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_expiry.GreyedOut = false;
			this.dtp_expiry.Location = new global::System.Drawing.Point(110, 37);
			this.dtp_expiry.Name = "dtp_expiry";
			this.dtp_expiry.Size = new global::System.Drawing.Size(155, 22);
			this.dtp_expiry.TabIndex = 6;
			this.dtp_expiry.Value = new global::System.DateTime(2008, 12, 6, 14, 32, 19, 691);
			this.chk_approved.AutoSize = true;
			this.chk_approved.Enabled = false;
			this.chk_approved.Location = new global::System.Drawing.Point(110, 7);
			this.chk_approved.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chk_approved.Name = "chk_approved";
			this.chk_approved.Size = new global::System.Drawing.Size(80, 20);
			this.chk_approved.TabIndex = 4;
			this.chk_approved.Text = "&Approved";
			this.chk_approved.UseVisualStyleBackColor = true;
			this.chk_approved.Visible = false;
			this.chk_recommendedToStudentButDeclined.AutoSize = true;
			this.chk_recommendedToStudentButDeclined.Location = new global::System.Drawing.Point(12, 246);
			this.chk_recommendedToStudentButDeclined.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chk_recommendedToStudentButDeclined.Name = "chk_recommendedToStudentButDeclined";
			this.chk_recommendedToStudentButDeclined.Size = new global::System.Drawing.Size(250, 20);
			this.chk_recommendedToStudentButDeclined.TabIndex = 13;
			this.chk_recommendedToStudentButDeclined.Text = "&Recommended to student but declined";
			this.chk_recommendedToStudentButDeclined.UseVisualStyleBackColor = true;
			this.txt_recommendedToStudentButDeclinedDetail.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txt_recommendedToStudentButDeclinedDetail.Location = new global::System.Drawing.Point(110, 268);
			this.txt_recommendedToStudentButDeclinedDetail.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_recommendedToStudentButDeclinedDetail.Multiline = true;
			this.txt_recommendedToStudentButDeclinedDetail.Name = "txt_recommendedToStudentButDeclinedDetail";
			this.txt_recommendedToStudentButDeclinedDetail.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txt_recommendedToStudentButDeclinedDetail.Size = new global::System.Drawing.Size(355, 44);
			this.txt_recommendedToStudentButDeclinedDetail.TabIndex = 14;
			this.p_top.BackColor = global::System.Drawing.SystemColors.Highlight;
			this.p_top.Controls.Add(this.lbl_msg);
			this.p_top.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_top.ForeColor = global::System.Drawing.SystemColors.HighlightText;
			this.p_top.Location = new global::System.Drawing.Point(0, 0);
			this.p_top.Name = "p_top";
			this.p_top.Padding = new global::System.Windows.Forms.Padding(3);
			this.p_top.Size = new global::System.Drawing.Size(477, 55);
			this.p_top.TabIndex = 0;
			this.p_top.Visible = false;
			this.lbl_msg.AutoSize = true;
			this.lbl_msg.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lbl_msg.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_msg.Location = new global::System.Drawing.Point(3, 3);
			this.lbl_msg.Name = "lbl_msg";
			this.lbl_msg.Size = new global::System.Drawing.Size(73, 18);
			this.lbl_msg.TabIndex = 1;
			this.lbl_msg.Text = "Message";
			this.panel2.Controls.Add(this.txt_recommendedToStudentButDeclinedDetail);
			this.panel2.Controls.Add(this.chk_caption);
			this.panel2.Controls.Add(this.chk_approved);
			this.panel2.Controls.Add(this.chk_letter);
			this.panel2.Controls.Add(this.chk_offline);
			this.panel2.Controls.Add(this.chk_recommendedToStudentButDeclined);
			this.panel2.Controls.Add(this.txt_letterText);
			this.panel2.Controls.Add(this.dtp_expiry);
			this.panel2.Controls.Add(this.txt_rationale);
			this.panel2.Controls.Add(this.btn_clearExpiry);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new global::System.Drawing.Point(0, 55);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(477, 316);
			this.panel2.TabIndex = 2;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(477, 410);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.p_top);
			base.Controls.Add(this.panel1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "AccommodationControlPopup";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Accommodation";
			base.Load += new global::System.EventHandler(this.AccommodationControlPopup_Load);
			this.panel1.ResumeLayout(false);
			this.p_top.ResumeLayout(false);
			this.p_top.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000330 RID: 816
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000331 RID: 817
		private global::System.Windows.Forms.Button btn_clearExpiry;

		// Token: 0x04000332 RID: 818
		private global::System.Windows.Forms.TextBox txt_rationale;

		// Token: 0x04000333 RID: 819
		private global::System.Windows.Forms.CheckBox chk_caption;

		// Token: 0x04000334 RID: 820
		private global::System.Windows.Forms.TextBox txt_letterText;

		// Token: 0x04000335 RID: 821
		private global::System.Windows.Forms.CheckBox chk_offline;

		// Token: 0x04000336 RID: 822
		private global::System.Windows.Forms.CheckBox chk_letter;

		// Token: 0x04000337 RID: 823
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000338 RID: 824
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000339 RID: 825
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400033A RID: 826
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400033B RID: 827
		private global::System.Windows.Forms.Button btn_ok;

		// Token: 0x0400033C RID: 828
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400033D RID: 829
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x0400033E RID: 830
		private global::AutoComboBox.MyDateTimePicker dtp_expiry;

		// Token: 0x0400033F RID: 831
		private global::System.Windows.Forms.CheckBox chk_approved;

		// Token: 0x04000340 RID: 832
		private global::System.Windows.Forms.CheckBox chk_recommendedToStudentButDeclined;

		// Token: 0x04000341 RID: 833
		private global::System.Windows.Forms.TextBox txt_recommendedToStudentButDeclinedDetail;

		// Token: 0x04000342 RID: 834
		private global::System.Windows.Forms.Button btn_requestChange;

		// Token: 0x04000343 RID: 835
		private global::System.Windows.Forms.Panel p_top;

		// Token: 0x04000344 RID: 836
		private global::System.Windows.Forms.Label lbl_msg;

		// Token: 0x04000345 RID: 837
		private global::System.Windows.Forms.Panel panel2;
	}
}
