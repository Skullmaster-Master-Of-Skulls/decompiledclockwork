namespace DynamicScreens.AdminTools
{
	// Token: 0x02000006 RID: 6
	public partial class LookupItemEdit : global::System.Windows.Forms.Form
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003150 File Offset: 0x00002150
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

		// Token: 0x0600006E RID: 110 RVA: 0x0000318C File Offset: 0x0000218C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.AdminTools.LookupItemEdit));
			this.label1 = new global::System.Windows.Forms.Label();
			this.txt_description = new global::System.Windows.Forms.TextBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.txt_descriptionFrench = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.p_children = new global::System.Windows.Forms.Panel();
			this.label3 = new global::System.Windows.Forms.Label();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.btn_save = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.lv_childItems = new global::AutoComboBox.ListViewEx();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			this.p_children.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(576, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Item description:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.txt_description.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.txt_description.Location = new global::System.Drawing.Point(4, 22);
			this.txt_description.Multiline = true;
			this.txt_description.Name = "txt_description";
			this.txt_description.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txt_description.Size = new global::System.Drawing.Size(576, 38);
			this.txt_description.TabIndex = 1;
			this.panel1.Controls.Add(this.txt_descriptionFrench);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.txt_description);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new global::System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new global::System.Windows.Forms.Padding(4);
			this.panel1.Size = new global::System.Drawing.Size(584, 113);
			this.panel1.TabIndex = 2;
			this.txt_descriptionFrench.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.txt_descriptionFrench.Location = new global::System.Drawing.Point(4, 81);
			this.txt_descriptionFrench.Name = "txt_descriptionFrench";
			this.txt_descriptionFrench.Size = new global::System.Drawing.Size(576, 26);
			this.txt_descriptionFrench.TabIndex = 3;
			this.label4.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label4.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(4, 60);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(576, 21);
			this.label4.TabIndex = 2;
			this.label4.Text = "Alternate Item description";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.p_children.Controls.Add(this.lv_childItems);
			this.p_children.Controls.Add(this.label3);
			this.p_children.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_children.Enabled = false;
			this.p_children.Location = new global::System.Drawing.Point(0, 113);
			this.p_children.Name = "p_children";
			this.p_children.Padding = new global::System.Windows.Forms.Padding(4);
			this.p_children.Size = new global::System.Drawing.Size(584, 267);
			this.p_children.TabIndex = 3;
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new global::System.Drawing.Point(4, 4);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(576, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Child items:";
			this.panel3.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.btn_save);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.btn_cancel);
			this.panel3.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new global::System.Drawing.Point(0, 380);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new global::System.Windows.Forms.Padding(6);
			this.panel3.Size = new global::System.Drawing.Size(584, 48);
			this.panel3.TabIndex = 4;
			this.btn_save.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_save.Location = new global::System.Drawing.Point(336, 6);
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(112, 34);
			this.btn_save.TabIndex = 2;
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.label2.Location = new global::System.Drawing.Point(448, 6);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(16, 34);
			this.label2.TabIndex = 1;
			this.btn_cancel.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_cancel.Location = new global::System.Drawing.Point(464, 6);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(112, 34);
			this.btn_cancel.TabIndex = 0;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.lv_childItems.AutoSortingEnabled = false;
			this.lv_childItems.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv_childItems.CheckBoxes = true;
			this.lv_childItems.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv_childItems.DefaultSortByAsc = true;
			this.lv_childItems.DefaultSortByColInd = -1;
			this.lv_childItems.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv_childItems.DrawMode = global::System.Windows.Forms.DrawMode.Normal;
			this.lv_childItems.EnterTriggersDoubleClickEvent = false;
			this.lv_childItems.GridLines = true;
			this.lv_childItems.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.lv_childItems.IsFileList = false;
			this.lv_childItems.ItemHeight = 16;
			this.lv_childItems.Location = new global::System.Drawing.Point(4, 27);
			this.lv_childItems.Name = "lv_childItems";
			this.lv_childItems.Size = new global::System.Drawing.Size(576, 236);
			this.lv_childItems.TabIndex = 0;
			this.lv_childItems.Tag2 = null;
			this.lv_childItems.UseCompatibleStateImageBehavior = false;
			this.lv_childItems.View = global::System.Windows.Forms.View.Details;
			this.columnHeader1.Width = 529;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(8, 19);
			base.ClientSize = new global::System.Drawing.Size(584, 428);
			base.Controls.Add(this.p_children);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "LookupItemEdit";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit lookup list item";
			base.Load += new global::System.EventHandler(this.LookupItemEdit_Load);
			base.Closing += new global::System.ComponentModel.CancelEventHandler(this.LookupItemEdit_Closing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.p_children.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000004 RID: 4
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000005 RID: 5
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000006 RID: 6
		private global::System.Windows.Forms.TextBox txt_description;

		// Token: 0x04000007 RID: 7
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000008 RID: 8
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.Panel p_children;

		// Token: 0x0400000B RID: 11
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400000C RID: 12
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x0400000D RID: 13
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x0400000E RID: 14
		private global::System.Windows.Forms.Button btn_save;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000010 RID: 16
		private global::System.Windows.Forms.TextBox txt_descriptionFrench;

		// Token: 0x04000011 RID: 17
		private global::AutoComboBox.ListViewEx lv_childItems;
	}
}
