namespace AutoComboBox
{
	// Token: 0x020000A9 RID: 169
	public partial class InputCheckedList : global::System.Windows.Forms.Form
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x000328CC File Offset: 0x000318CC
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

		// Token: 0x06000658 RID: 1624 RVA: 0x00032908 File Offset: 0x00031908
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputCheckedList));
			this.lb = new global::System.Windows.Forms.CheckedListBox();
			this.btn_selectAll = new global::System.Windows.Forms.Button();
			this.btn_selectNone = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.btn_ok = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.lbl_caption = new global::System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.lb.CheckOnClick = true;
			this.lb.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lb.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lb.Location = new global::System.Drawing.Point(6, 40);
			this.lb.Name = "lb";
			this.lb.Size = new global::System.Drawing.Size(556, 340);
			this.lb.TabIndex = 0;
			this.btn_selectAll.Location = new global::System.Drawing.Point(8, 8);
			this.btn_selectAll.Name = "btn_selectAll";
			this.btn_selectAll.Size = new global::System.Drawing.Size(96, 40);
			this.btn_selectAll.TabIndex = 1;
			this.btn_selectAll.Text = "Select &all";
			this.btn_selectAll.Click += new global::System.EventHandler(this.btn_selectAll_Click);
			this.btn_selectNone.Location = new global::System.Drawing.Point(8, 56);
			this.btn_selectNone.Name = "btn_selectNone";
			this.btn_selectNone.Size = new global::System.Drawing.Size(96, 40);
			this.btn_selectNone.TabIndex = 2;
			this.btn_selectNone.Text = "Select &none";
			this.btn_selectNone.Click += new global::System.EventHandler(this.btn_selectNone_Click);
			this.panel1.Controls.Add(this.btn_selectAll);
			this.panel1.Controls.Add(this.btn_selectNone);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new global::System.Drawing.Point(562, 40);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(112, 348);
			this.panel1.TabIndex = 3;
			this.panel2.Controls.Add(this.btn_ok);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.btn_cancel);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new global::System.Drawing.Point(6, 388);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new global::System.Windows.Forms.Padding(4);
			this.panel2.Size = new global::System.Drawing.Size(668, 40);
			this.panel2.TabIndex = 4;
			this.btn_ok.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_ok.Location = new global::System.Drawing.Point(456, 4);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(96, 32);
			this.btn_ok.TabIndex = 2;
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.label1.Location = new global::System.Drawing.Point(552, 4);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(16, 32);
			this.label1.TabIndex = 4;
			this.btn_cancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_cancel.Location = new global::System.Drawing.Point(568, 4);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(96, 32);
			this.btn_cancel.TabIndex = 3;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.lbl_caption.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_caption.Location = new global::System.Drawing.Point(6, 6);
			this.lbl_caption.Name = "lbl_caption";
			this.lbl_caption.Size = new global::System.Drawing.Size(668, 34);
			this.lbl_caption.TabIndex = 5;
			this.lbl_caption.Text = "Caption";
			base.AcceptButton = this.btn_ok;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_cancel;
			base.ClientSize = new global::System.Drawing.Size(680, 430);
			base.Controls.Add(this.lb);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.lbl_caption);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "InputCheckedList";
			base.Padding = new global::System.Windows.Forms.Padding(6, 6, 6, 2);
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "InputCheckedList";
			base.Load += new global::System.EventHandler(this.InputCheckedList_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x040004EF RID: 1263
		private global::System.Windows.Forms.CheckedListBox lb;

		// Token: 0x040004F0 RID: 1264
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040004F1 RID: 1265
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040004F2 RID: 1266
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040004F3 RID: 1267
		private global::System.Windows.Forms.Button btn_selectAll;

		// Token: 0x040004F4 RID: 1268
		private global::System.Windows.Forms.Button btn_selectNone;

		// Token: 0x040004F5 RID: 1269
		private global::System.Windows.Forms.Button btn_ok;

		// Token: 0x040004F6 RID: 1270
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x040004F7 RID: 1271
		private global::System.Windows.Forms.Label lbl_caption;

		// Token: 0x040004F8 RID: 1272
		private global::System.ComponentModel.Container components = null;
	}
}
