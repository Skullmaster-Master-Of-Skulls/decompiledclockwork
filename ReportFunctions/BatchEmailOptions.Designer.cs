namespace ReportFunctions
{
	// Token: 0x0200000B RID: 11
	public partial class BatchEmailOptions : global::System.Windows.Forms.Form
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00005598 File Offset: 0x00004598
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000055D0 File Offset: 0x000045D0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ReportFunctions.BatchEmailOptions));
			this.btn_previewEmails = new global::AutoComboBox.MyButton();
			this.btn_sendFirstEmail = new global::AutoComboBox.MyButton();
			this.btn_dontSendAnyEmails = new global::AutoComboBox.MyButton();
			this.btn_sendAllEmails = new global::AutoComboBox.MyButton();
			base.SuspendLayout();
			this.btn_previewEmails.BackColor = global::System.Drawing.SystemColors.Control;
			this.btn_previewEmails.BackColorHighlight = global::System.Drawing.SystemColors.ControlLightLight;
			this.btn_previewEmails.BackGradientIncrement = -25;
			this.btn_previewEmails.BackGradientOn = false;
			this.btn_previewEmails.BackHighlightGradientOn = true;
			this.btn_previewEmails.BorderStyle = global::AutoComboBox.MyBorderStyle.none;
			this.btn_previewEmails.BorderStyleHighlight = global::AutoComboBox.MyBorderStyle.none;
			this.btn_previewEmails.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_previewEmails.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_previewEmails.ForeColorHighlight = global::System.Drawing.SystemColors.HotTrack;
			this.btn_previewEmails.Image = global::ReportFunctions.Properties.Resources.mail_view;
			this.btn_previewEmails.Location = new global::System.Drawing.Point(16, 332);
			this.btn_previewEmails.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_previewEmails.Name = "btn_previewEmails";
			this.btn_previewEmails.PadBetweenImageAndText = 20;
			this.btn_previewEmails.PadBottom = 4;
			this.btn_previewEmails.PadLeft = 4;
			this.btn_previewEmails.PadRight = 4;
			this.btn_previewEmails.PadTop = 4;
			this.btn_previewEmails.Size = new global::System.Drawing.Size(427, 95);
			this.btn_previewEmails.TabIndex = 3;
			this.btn_previewEmails.Text = "&Preview the emails\r\nNo emails will be sent";
			this.btn_previewEmails.TitleFontSize = 18;
			this.btn_previewEmails.UseVisualStyleBackColor = true;
			this.btn_previewEmails.Click += new global::System.EventHandler(this.btn_previewEmails_Click);
			this.btn_sendFirstEmail.BackColor = global::System.Drawing.SystemColors.Control;
			this.btn_sendFirstEmail.BackColorHighlight = global::System.Drawing.SystemColors.ControlLightLight;
			this.btn_sendFirstEmail.BackGradientIncrement = -25;
			this.btn_sendFirstEmail.BackGradientOn = false;
			this.btn_sendFirstEmail.BackHighlightGradientOn = true;
			this.btn_sendFirstEmail.BorderStyle = global::AutoComboBox.MyBorderStyle.none;
			this.btn_sendFirstEmail.BorderStyleHighlight = global::AutoComboBox.MyBorderStyle.none;
			this.btn_sendFirstEmail.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_sendFirstEmail.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_sendFirstEmail.ForeColorHighlight = global::System.Drawing.SystemColors.HotTrack;
			this.btn_sendFirstEmail.Image = global::ReportFunctions.Properties.Resources.user1_into;
			this.btn_sendFirstEmail.Location = new global::System.Drawing.Point(16, 228);
			this.btn_sendFirstEmail.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_sendFirstEmail.Name = "btn_sendFirstEmail";
			this.btn_sendFirstEmail.PadBetweenImageAndText = 20;
			this.btn_sendFirstEmail.PadBottom = 4;
			this.btn_sendFirstEmail.PadLeft = 4;
			this.btn_sendFirstEmail.PadRight = 4;
			this.btn_sendFirstEmail.PadTop = 4;
			this.btn_sendFirstEmail.Size = new global::System.Drawing.Size(427, 95);
			this.btn_sendFirstEmail.TabIndex = 2;
			this.btn_sendFirstEmail.Text = "Send the &first email only\r\nOne email will be sent";
			this.btn_sendFirstEmail.TitleFontSize = 18;
			this.btn_sendFirstEmail.UseVisualStyleBackColor = true;
			this.btn_sendFirstEmail.Click += new global::System.EventHandler(this.btn_sendFirstEmail_Click);
			this.btn_dontSendAnyEmails.BackColor = global::System.Drawing.SystemColors.Control;
			this.btn_dontSendAnyEmails.BackColorHighlight = global::System.Drawing.SystemColors.ControlLightLight;
			this.btn_dontSendAnyEmails.BackGradientIncrement = -25;
			this.btn_dontSendAnyEmails.BackGradientOn = false;
			this.btn_dontSendAnyEmails.BackHighlightGradientOn = true;
			this.btn_dontSendAnyEmails.BorderStyle = global::AutoComboBox.MyBorderStyle.none;
			this.btn_dontSendAnyEmails.BorderStyleHighlight = global::AutoComboBox.MyBorderStyle.none;
			this.btn_dontSendAnyEmails.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_dontSendAnyEmails.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_dontSendAnyEmails.ForeColorHighlight = global::System.Drawing.SystemColors.HotTrack;
			this.btn_dontSendAnyEmails.Image = global::ReportFunctions.Properties.Resources.delete1;
			this.btn_dontSendAnyEmails.Location = new global::System.Drawing.Point(16, 123);
			this.btn_dontSendAnyEmails.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_dontSendAnyEmails.Name = "btn_dontSendAnyEmails";
			this.btn_dontSendAnyEmails.PadBetweenImageAndText = 20;
			this.btn_dontSendAnyEmails.PadBottom = 4;
			this.btn_dontSendAnyEmails.PadLeft = 4;
			this.btn_dontSendAnyEmails.PadRight = 4;
			this.btn_dontSendAnyEmails.PadTop = 4;
			this.btn_dontSendAnyEmails.Size = new global::System.Drawing.Size(427, 84);
			this.btn_dontSendAnyEmails.TabIndex = 1;
			this.btn_dontSendAnyEmails.Text = "&Don't send any emails\r\nNo emails will be sent";
			this.btn_dontSendAnyEmails.TitleFontSize = 18;
			this.btn_dontSendAnyEmails.UseVisualStyleBackColor = true;
			this.btn_dontSendAnyEmails.Click += new global::System.EventHandler(this.btn_dontSendAnyEmails_Click);
			this.btn_sendAllEmails.BackColor = global::System.Drawing.SystemColors.Control;
			this.btn_sendAllEmails.BackColorHighlight = global::System.Drawing.SystemColors.ControlLightLight;
			this.btn_sendAllEmails.BackGradientIncrement = -25;
			this.btn_sendAllEmails.BackGradientOn = false;
			this.btn_sendAllEmails.BackHighlightGradientOn = true;
			this.btn_sendAllEmails.BorderStyle = global::AutoComboBox.MyBorderStyle.none;
			this.btn_sendAllEmails.BorderStyleHighlight = global::AutoComboBox.MyBorderStyle.none;
			this.btn_sendAllEmails.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_sendAllEmails.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_sendAllEmails.ForeColorHighlight = global::System.Drawing.SystemColors.HotTrack;
			this.btn_sendAllEmails.Image = global::ReportFunctions.Properties.Resources.mail;
			this.btn_sendAllEmails.Location = new global::System.Drawing.Point(16, 18);
			this.btn_sendAllEmails.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_sendAllEmails.Name = "btn_sendAllEmails";
			this.btn_sendAllEmails.PadBetweenImageAndText = 20;
			this.btn_sendAllEmails.PadBottom = 4;
			this.btn_sendAllEmails.PadLeft = 4;
			this.btn_sendAllEmails.PadRight = 4;
			this.btn_sendAllEmails.PadTop = 4;
			this.btn_sendAllEmails.Size = new global::System.Drawing.Size(427, 95);
			this.btn_sendAllEmails.TabIndex = 0;
			this.btn_sendAllEmails.Text = "&Send all emails\r\nAll emails will be sent";
			this.btn_sendAllEmails.TitleFontSize = 18;
			this.btn_sendAllEmails.UseVisualStyleBackColor = true;
			this.btn_sendAllEmails.Click += new global::System.EventHandler(this.btn_sendAllEmails_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(8f, 20f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(469, 460);
			base.Controls.Add(this.btn_previewEmails);
			base.Controls.Add(this.btn_sendFirstEmail);
			base.Controls.Add(this.btn_dontSendAnyEmails);
			base.Controls.Add(this.btn_sendAllEmails);
			this.Font = new global::System.Drawing.Font("Arial Narrow", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(4, 5, 4, 5);
			base.Name = "BatchEmailOptions";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Batch Email Options";
			base.Load += new global::System.EventHandler(this.BatchEmailOptions_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x040000D5 RID: 213
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000D6 RID: 214
		private global::AutoComboBox.MyButton btn_sendAllEmails;

		// Token: 0x040000D7 RID: 215
		private global::AutoComboBox.MyButton btn_dontSendAnyEmails;

		// Token: 0x040000D8 RID: 216
		private global::AutoComboBox.MyButton btn_sendFirstEmail;

		// Token: 0x040000D9 RID: 217
		private global::AutoComboBox.MyButton btn_previewEmails;
	}
}
