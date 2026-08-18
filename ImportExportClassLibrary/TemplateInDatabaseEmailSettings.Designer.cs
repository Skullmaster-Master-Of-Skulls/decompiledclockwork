namespace ImportExportClassLibrary
{
	// Token: 0x02000022 RID: 34
	public partial class TemplateInDatabaseEmailSettings : global::System.Windows.Forms.Form
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00005570 File Offset: 0x00004570
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005590 File Offset: 0x00004590
		private void InitializeComponent()
		{
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.txt_body = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.txt_attach = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.txt_subject = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txt_bcc = new global::System.Windows.Forms.TextBox();
			this.txt_cc = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.txt_to = new global::System.Windows.Forms.TextBox();
			this.txt_from = new global::System.Windows.Forms.TextBox();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 378);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(497, 39);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_save.Image = global::ImportExportClassLibrary.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(80, 36);
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Image = global::ImportExportClassLibrary.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 17.50503f));
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 82.49497f));
			this.tableLayoutPanel1.Controls.Add(this.txt_body, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.txt_attach, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.txt_subject, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txt_bcc, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.txt_cc, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_to, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txt_from, 1, 0);
			this.tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(497, 378);
			this.tableLayoutPanel1.TabIndex = 6;
			this.txt_body.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_body.Location = new global::System.Drawing.Point(89, 171);
			this.txt_body.Multiline = true;
			this.txt_body.Name = "txt_body";
			this.txt_body.Size = new global::System.Drawing.Size(405, 204);
			this.txt_body.TabIndex = 9;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(3, 168);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(38, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "Body";
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(3, 140);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(50, 16);
			this.label7.TabIndex = 12;
			this.label7.Text = "Attach:";
			this.txt_attach.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_attach.Location = new global::System.Drawing.Point(89, 143);
			this.txt_attach.Name = "txt_attach";
			this.txt_attach.Size = new global::System.Drawing.Size(405, 22);
			this.txt_attach.TabIndex = 13;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(3, 112);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(52, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Subject";
			this.txt_subject.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_subject.Location = new global::System.Drawing.Point(89, 115);
			this.txt_subject.Name = "txt_subject";
			this.txt_subject.Size = new global::System.Drawing.Size(405, 22);
			this.txt_subject.TabIndex = 7;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(3, 84);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(35, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Bcc:";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(3, 56);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(28, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Cc:";
			this.txt_bcc.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_bcc.Location = new global::System.Drawing.Point(89, 87);
			this.txt_bcc.Name = "txt_bcc";
			this.txt_bcc.Size = new global::System.Drawing.Size(405, 22);
			this.txt_bcc.TabIndex = 6;
			this.txt_cc.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_cc.Location = new global::System.Drawing.Point(89, 59);
			this.txt_cc.Name = "txt_cc";
			this.txt_cc.Size = new global::System.Drawing.Size(405, 22);
			this.txt_cc.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(3, 28);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(25, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "To:";
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(3, 0);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(42, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = "From:";
			this.txt_to.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_to.Location = new global::System.Drawing.Point(89, 31);
			this.txt_to.Name = "txt_to";
			this.txt_to.Size = new global::System.Drawing.Size(405, 22);
			this.txt_to.TabIndex = 1;
			this.txt_from.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_from.Location = new global::System.Drawing.Point(89, 3);
			this.txt_from.Name = "txt_from";
			this.txt_from.Size = new global::System.Drawing.Size(405, 22);
			this.txt_from.TabIndex = 11;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(497, 417);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "TemplateInDatabaseEmailSettings";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Embedded Email Template";
			base.Load += new global::System.EventHandler(this.TemplateInDatabaseEmailSettings_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400003C RID: 60
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.TextBox txt_cc;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.TextBox txt_to;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.TextBox txt_bcc;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.TextBox txt_subject;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.TextBox txt_body;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.TextBox txt_from;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.TextBox txt_attach;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.Label label7;
	}
}
