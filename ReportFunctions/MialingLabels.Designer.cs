namespace ReportFunctions
{
	// Token: 0x02000037 RID: 55
	public partial class MialingLabels : global::System.Windows.Forms.Form
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0003F320 File Offset: 0x0003E320
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0003F358 File Offset: 0x0003E358
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ReportFunctions.MialingLabels));
			this.label3 = new global::System.Windows.Forms.Label();
			this.txt_mailingLabelType = new global::System.Windows.Forms.TextBox();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.cmb_chk1 = new global::AutoComboBox.AutoComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.cmb_chk2 = new global::AutoComboBox.AutoComboBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.lv = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.label4 = new global::System.Windows.Forms.Label();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.btn_add = new global::System.Windows.Forms.ToolStripButton();
			this.btn_remove = new global::System.Windows.Forms.ToolStripButton();
			this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
			this.toolStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			base.SuspendLayout();
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(3, 7);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(171, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Mailing label type (ex. 5160)";
			this.txt_mailingLabelType.Location = new global::System.Drawing.Point(194, 4);
			this.txt_mailingLabelType.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_mailingLabelType.Name = "txt_mailingLabelType";
			this.txt_mailingLabelType.Size = new global::System.Drawing.Size(251, 22);
			this.txt_mailingLabelType.TabIndex = 8;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 431);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(742, 39);
			this.toolStrip1.TabIndex = 10;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_ok.Image = global::ReportFunctions.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::ReportFunctions.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.cmb_chk1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new global::System.Drawing.Point(6, 33);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(367, 185);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Address 1";
			this.textBox1.AllowDrop = true;
			this.textBox1.Location = new global::System.Drawing.Point(7, 50);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(339, 127);
			this.textBox1.TabIndex = 15;
			this.textBox1.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
			this.textBox1.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
			this.cmb_chk1.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_chk1.AllowUserToEnterAnyText = true;
			this.cmb_chk1.AltValueMember = null;
			this.cmb_chk1.AutoCompleteEnabled = true;
			this.cmb_chk1.CalcButtonCid = 0;
			this.cmb_chk1.ChildLookupGroupId = 0;
			this.cmb_chk1.CidToNotifyWithValueMember = 0;
			this.cmb_chk1.FormattingEnabled = true;
			this.cmb_chk1.GotoNextItemOnDoubleClick = false;
			this.cmb_chk1.IgnoreScrollWheel = true;
			this.cmb_chk1.Location = new global::System.Drawing.Point(145, 20);
			this.cmb_chk1.LookupGroupId = 0;
			this.cmb_chk1.Name = "cmb_chk1";
			this.cmb_chk1.Size = new global::System.Drawing.Size(201, 24);
			this.cmb_chk1.TabIndex = 14;
			this.cmb_chk1.TryToSelectOnFocusLeave = true;
			this.cmb_chk1.OnTooltipPopup += new global::AutoComboBox.AutoComboBox.ToolTipPopupHandler(this.cmb_chk1_OnTooltipPopup);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(6, 23);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(133, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Checkbox to activate:";
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Controls.Add(this.cmb_chk2);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new global::System.Drawing.Point(6, 233);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(367, 182);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Address 2";
			this.textBox2.AllowDrop = true;
			this.textBox2.Location = new global::System.Drawing.Point(9, 50);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new global::System.Drawing.Size(339, 126);
			this.textBox2.TabIndex = 17;
			this.textBox2.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.textBox2_DragDrop);
			this.textBox2.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.textBox2_DragEnter);
			this.cmb_chk2.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_chk2.AllowUserToEnterAnyText = true;
			this.cmb_chk2.AltValueMember = null;
			this.cmb_chk2.AutoCompleteEnabled = true;
			this.cmb_chk2.CalcButtonCid = 0;
			this.cmb_chk2.ChildLookupGroupId = 0;
			this.cmb_chk2.CidToNotifyWithValueMember = 0;
			this.cmb_chk2.FormattingEnabled = true;
			this.cmb_chk2.GotoNextItemOnDoubleClick = false;
			this.cmb_chk2.IgnoreScrollWheel = true;
			this.cmb_chk2.Location = new global::System.Drawing.Point(148, 20);
			this.cmb_chk2.LookupGroupId = 0;
			this.cmb_chk2.Name = "cmb_chk2";
			this.cmb_chk2.Size = new global::System.Drawing.Size(201, 24);
			this.cmb_chk2.TabIndex = 16;
			this.cmb_chk2.TryToSelectOnFocusLeave = true;
			this.cmb_chk2.OnTooltipPopup += new global::AutoComboBox.AutoComboBox.ToolTipPopupHandler(this.cmb_chk2_OnTooltipPopup);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(9, 23);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(133, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "Checkbox to activate:";
			this.panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.lv);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.toolStrip2);
			this.panel1.Location = new global::System.Drawing.Point(379, 33);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(351, 382);
			this.panel1.TabIndex = 13;
			this.lv.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.lv.Location = new global::System.Drawing.Point(0, 41);
			this.lv.Name = "lv";
			this.lv.Size = new global::System.Drawing.Size(347, 337);
			this.lv.TabIndex = 3;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = global::System.Windows.Forms.View.Details;
			this.lv.ItemDrag += new global::System.Windows.Forms.ItemDragEventHandler(this.lv_ItemDrag);
			this.columnHeader1.Width = 310;
			this.label4.AutoSize = true;
			this.label4.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new global::System.Drawing.Point(0, 25);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(222, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Available fields (drag to labels on left)";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_add,
				this.btn_remove
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new global::System.Drawing.Size(347, 25);
			this.toolStrip2.TabIndex = 2;
			this.toolStrip2.TabStop = true;
			this.btn_add.Image = global::ReportFunctions.Properties.Resources.check;
			this.btn_add.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_add.Name = "btn_add";
			this.btn_add.Size = new global::System.Drawing.Size(49, 22);
			this.btn_add.Text = "Add";
			this.btn_add.Click += new global::System.EventHandler(this.btn_add_Click);
			this.btn_remove.Image = global::ReportFunctions.Properties.Resources.delete;
			this.btn_remove.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_remove.Name = "btn_remove";
			this.btn_remove.Size = new global::System.Drawing.Size(70, 22);
			this.btn_remove.Text = "Remove";
			this.btn_remove.Click += new global::System.EventHandler(this.btn_remove_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(742, 470);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.txt_mailingLabelType);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "MialingLabels";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Mailing Labels";
			base.Load += new global::System.EventHandler(this.MialingLabels_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MialingLabels_FormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000190 RID: 400
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000191 RID: 401
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000192 RID: 402
		private global::System.Windows.Forms.TextBox txt_mailingLabelType;

		// Token: 0x04000193 RID: 403
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000194 RID: 404
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x04000195 RID: 405
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000196 RID: 406
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000197 RID: 407
		private global::AutoComboBox.AutoComboBox cmb_chk1;

		// Token: 0x04000198 RID: 408
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000199 RID: 409
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x0400019A RID: 410
		private global::AutoComboBox.AutoComboBox cmb_chk2;

		// Token: 0x0400019B RID: 411
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400019C RID: 412
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x0400019D RID: 413
		private global::System.Windows.Forms.TextBox textBox2;

		// Token: 0x0400019E RID: 414
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400019F RID: 415
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040001A0 RID: 416
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x040001A1 RID: 417
		private global::System.Windows.Forms.ToolStripButton btn_add;

		// Token: 0x040001A2 RID: 418
		private global::System.Windows.Forms.ToolStripButton btn_remove;

		// Token: 0x040001A3 RID: 419
		private global::System.Windows.Forms.ListView lv;

		// Token: 0x040001A4 RID: 420
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x040001A5 RID: 421
		private global::System.Windows.Forms.ToolTip toolTip1;
	}
}
