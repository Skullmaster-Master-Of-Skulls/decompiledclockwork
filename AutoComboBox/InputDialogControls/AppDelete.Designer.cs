namespace AutoComboBox.InputDialogControls
{
	// Token: 0x02000084 RID: 132
	public partial class AppDelete : global::System.Windows.Forms.Form
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0002AD44 File Offset: 0x00029D44
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

		// Token: 0x06000527 RID: 1319 RVA: 0x0002AD80 File Offset: 0x00029D80
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputDialogControls.AppDelete));
			this.lbl_title = new global::System.Windows.Forms.Label();
			this.btn_removeMeFromThisApp = new global::AutoComboBox.MyButton();
			this.btn_deleteThisApp = new global::AutoComboBox.MyButton();
			this.btn_cancel = new global::AutoComboBox.MyButton();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lbl_splitCancel = new global::System.Windows.Forms.Label();
			this.chk_iUnderstand = new global::System.Windows.Forms.CheckBox();
			this.label3 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.lbl_title.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_title.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_title.ForeColor = global::System.Drawing.SystemColors.HotTrack;
			this.lbl_title.Location = new global::System.Drawing.Point(12, 0);
			this.lbl_title.Name = "lbl_title";
			this.lbl_title.Size = new global::System.Drawing.Size(440, 40);
			this.lbl_title.TabIndex = 6;
			this.lbl_title.Text = "Are you sure you want to delete this appointment?";
			this.lbl_title.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_removeMeFromThisApp.BackColor = global::System.Drawing.SystemColors.Control;
			this.btn_removeMeFromThisApp.BackColorHighlight = global::System.Drawing.SystemColors.ControlLightLight;
			this.btn_removeMeFromThisApp.BackGradientIncrement = -25;
			this.btn_removeMeFromThisApp.BackGradientOn = false;
			this.btn_removeMeFromThisApp.BackHighlightGradientOn = true;
			this.btn_removeMeFromThisApp.BorderStyle = global::AutoComboBox.MyBorderStyle.roundedBox;
			this.btn_removeMeFromThisApp.BorderStyleHighlight = global::AutoComboBox.MyBorderStyle.roundedBox;
			this.btn_removeMeFromThisApp.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.btn_removeMeFromThisApp.Enabled = false;
			this.btn_removeMeFromThisApp.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_removeMeFromThisApp.ForeColorHighlight = global::System.Drawing.SystemColors.HotTrack;
			this.btn_removeMeFromThisApp.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_removeMeFromThisApp.Image");
			this.btn_removeMeFromThisApp.Location = new global::System.Drawing.Point(12, 90);
			this.btn_removeMeFromThisApp.Name = "btn_removeMeFromThisApp";
			this.btn_removeMeFromThisApp.PadBetweenImageAndText = 20;
			this.btn_removeMeFromThisApp.PadBottom = 4;
			this.btn_removeMeFromThisApp.PadLeft = 4;
			this.btn_removeMeFromThisApp.PadRight = 4;
			this.btn_removeMeFromThisApp.PadTop = 4;
			this.btn_removeMeFromThisApp.Size = new global::System.Drawing.Size(440, 104);
			this.btn_removeMeFromThisApp.TabIndex = 8;
			this.btn_removeMeFromThisApp.Text = "&Remove me from this appointment\\nDon't delete this appointment for everyone; just remove my name from the attendees list.";
			this.btn_removeMeFromThisApp.TitleFontSize = 14;
			this.btn_removeMeFromThisApp.UseVisualStyleBackColor = false;
			this.btn_removeMeFromThisApp.Click += new global::System.EventHandler(this.btn_removeMeFromThisApp_Click);
			this.btn_deleteThisApp.BackColor = global::System.Drawing.SystemColors.Control;
			this.btn_deleteThisApp.BackColorHighlight = global::System.Drawing.SystemColors.ControlLightLight;
			this.btn_deleteThisApp.BackGradientIncrement = -25;
			this.btn_deleteThisApp.BackGradientOn = false;
			this.btn_deleteThisApp.BackHighlightGradientOn = true;
			this.btn_deleteThisApp.BorderStyle = global::AutoComboBox.MyBorderStyle.roundedBox;
			this.btn_deleteThisApp.BorderStyleHighlight = global::AutoComboBox.MyBorderStyle.roundedBox;
			this.btn_deleteThisApp.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.btn_deleteThisApp.Enabled = false;
			this.btn_deleteThisApp.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_deleteThisApp.ForeColorHighlight = global::System.Drawing.SystemColors.HotTrack;
			this.btn_deleteThisApp.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_deleteThisApp.Image");
			this.btn_deleteThisApp.Location = new global::System.Drawing.Point(12, 202);
			this.btn_deleteThisApp.Name = "btn_deleteThisApp";
			this.btn_deleteThisApp.PadBetweenImageAndText = 20;
			this.btn_deleteThisApp.PadBottom = 4;
			this.btn_deleteThisApp.PadLeft = 4;
			this.btn_deleteThisApp.PadRight = 4;
			this.btn_deleteThisApp.PadTop = 4;
			this.btn_deleteThisApp.Size = new global::System.Drawing.Size(440, 104);
			this.btn_deleteThisApp.TabIndex = 9;
			this.btn_deleteThisApp.Text = "&Delete this appointment\\nThis appointment will be permanently deleted for all attendees.";
			this.btn_deleteThisApp.TitleFontSize = 14;
			this.btn_deleteThisApp.UseVisualStyleBackColor = false;
			this.btn_deleteThisApp.Click += new global::System.EventHandler(this.btn_deleteThisApp_Click);
			this.btn_cancel.BackColor = global::System.Drawing.SystemColors.Control;
			this.btn_cancel.BackColorHighlight = global::System.Drawing.SystemColors.ControlLightLight;
			this.btn_cancel.BackGradientIncrement = -25;
			this.btn_cancel.BackGradientOn = false;
			this.btn_cancel.BackHighlightGradientOn = true;
			this.btn_cancel.BorderStyle = global::AutoComboBox.MyBorderStyle.roundedBox;
			this.btn_cancel.BorderStyleHighlight = global::AutoComboBox.MyBorderStyle.roundedBox;
			this.btn_cancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.btn_cancel.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_cancel.ForeColorHighlight = global::System.Drawing.SystemColors.HotTrack;
			this.btn_cancel.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_cancel.Image");
			this.btn_cancel.Location = new global::System.Drawing.Point(12, 323);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.PadBetweenImageAndText = 20;
			this.btn_cancel.PadBottom = 4;
			this.btn_cancel.PadLeft = 4;
			this.btn_cancel.PadRight = 4;
			this.btn_cancel.PadTop = 4;
			this.btn_cancel.Size = new global::System.Drawing.Size(440, 104);
			this.btn_cancel.TabIndex = 10;
			this.btn_cancel.Text = "&Cancel\\nDo nothing.";
			this.btn_cancel.TitleFontSize = 14;
			this.btn_cancel.UseVisualStyleBackColor = false;
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.label2.BackColor = global::System.Drawing.SystemColors.ControlText;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new global::System.Drawing.Point(12, 314);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(440, 1);
			this.label2.TabIndex = 11;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(12, 194);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(440, 8);
			this.label1.TabIndex = 12;
			this.lbl_splitCancel.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_splitCancel.Location = new global::System.Drawing.Point(12, 306);
			this.lbl_splitCancel.Name = "lbl_splitCancel";
			this.lbl_splitCancel.Size = new global::System.Drawing.Size(440, 8);
			this.lbl_splitCancel.TabIndex = 11;
			this.chk_iUnderstand.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.chk_iUnderstand.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.chk_iUnderstand.Location = new global::System.Drawing.Point(12, 40);
			this.chk_iUnderstand.Name = "chk_iUnderstand";
			this.chk_iUnderstand.Size = new global::System.Drawing.Size(440, 50);
			this.chk_iUnderstand.TabIndex = 13;
			this.chk_iUnderstand.Text = "I &understand that I will lose the information contained in this appointment (check this box in order to proceed)";
			this.chk_iUnderstand.UseVisualStyleBackColor = true;
			this.chk_iUnderstand.CheckedChanged += new global::System.EventHandler(this.chk_iUnderstand_CheckedChanged);
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new global::System.Drawing.Point(12, 315);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(440, 8);
			this.label3.TabIndex = 14;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(8, 19);
			base.CancelButton = this.btn_cancel;
			base.ClientSize = new global::System.Drawing.Size(464, 434);
			base.Controls.Add(this.btn_cancel);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.lbl_splitCancel);
			base.Controls.Add(this.btn_deleteThisApp);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btn_removeMeFromThisApp);
			base.Controls.Add(this.chk_iUnderstand);
			base.Controls.Add(this.lbl_title);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.ForeColor = global::System.Drawing.SystemColors.ControlLight;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "AppDelete";
			base.Padding = new global::System.Windows.Forms.Padding(12, 0, 12, 12);
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Delete appointment(s)";
			base.Load += new global::System.EventHandler(this.AppDelete_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x04000456 RID: 1110
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000457 RID: 1111
		private global::AutoComboBox.MyButton btn_removeMeFromThisApp;

		// Token: 0x04000458 RID: 1112
		private global::AutoComboBox.MyButton btn_deleteThisApp;

		// Token: 0x04000459 RID: 1113
		private global::AutoComboBox.MyButton btn_cancel;

		// Token: 0x0400045A RID: 1114
		private global::System.Windows.Forms.Label lbl_title;

		// Token: 0x0400045B RID: 1115
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400045C RID: 1116
		private global::System.Windows.Forms.Label lbl_splitCancel;

		// Token: 0x0400045D RID: 1117
		private global::System.Windows.Forms.CheckBox chk_iUnderstand;

		// Token: 0x0400045E RID: 1118
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400045F RID: 1119
		private global::System.ComponentModel.IContainer components;
	}
}
