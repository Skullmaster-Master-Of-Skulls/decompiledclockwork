namespace AutoComboBox.HelperForms
{
	// Token: 0x02000085 RID: 133
	public partial class FrmAccommodationExpiryPresetDatesEdit : global::System.Windows.Forms.Form
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x0002B90C File Offset: 0x0002A90C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002B944 File Offset: 0x0002A944
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.HelperForms.FrmAccommodationExpiryPresetDatesEdit));
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.btn_addNewDate = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_removeSelectedDate = new global::System.Windows.Forms.ToolStripButton();
			this.cms_list = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.removeItemToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.addDateToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.cms_list.SuspendLayout();
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
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 414);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(346, 39);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "Options toolstrip";
			this.btn_cancel.AccessibleName = "Cancel";
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.btn_save.AccessibleName = "Save";
			this.btn_save.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(80, 36);
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.listView1.AccessibleName = "Preset dates listing";
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listView1.ContextMenuStrip = this.cms_list;
			this.listView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new global::System.Drawing.Point(0, 25);
			this.listView1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(346, 389);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.toolStrip2.AccessibleName = "List options toolstrip";
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_addNewDate,
				this.toolStripSeparator1,
				this.btn_removeSelectedDate
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new global::System.Drawing.Size(346, 25);
			this.toolStrip2.TabIndex = 2;
			this.toolStrip2.TabStop = true;
			this.toolStrip2.Text = "List options toolstrip";
			this.btn_addNewDate.AccessibleName = "Add a new date to the list";
			this.btn_addNewDate.Image = global::AutoComboBox.Properties.Resources.add;
			this.btn_addNewDate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_addNewDate.Name = "btn_addNewDate";
			this.btn_addNewDate.Size = new global::System.Drawing.Size(100, 22);
			this.btn_addNewDate.Text = "&Add new date";
			this.btn_addNewDate.Click += new global::System.EventHandler(this.btn_addNewDate_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 25);
			this.btn_removeSelectedDate.AccessibleName = "Remove the selected date from the list";
			this.btn_removeSelectedDate.Image = global::AutoComboBox.Properties.Resources.delete;
			this.btn_removeSelectedDate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_removeSelectedDate.Name = "btn_removeSelectedDate";
			this.btn_removeSelectedDate.Size = new global::System.Drawing.Size(142, 22);
			this.btn_removeSelectedDate.Text = "&Remove selected date";
			this.btn_removeSelectedDate.Click += new global::System.EventHandler(this.btn_removeSelectedDate_Click);
			this.cms_list.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.removeItemToolStripMenuItem,
				this.toolStripMenuItem1,
				this.addDateToolStripMenuItem
			});
			this.cms_list.Name = "cms_list";
			this.cms_list.Size = new global::System.Drawing.Size(190, 54);
			this.columnHeader1.Text = "Date";
			this.columnHeader1.Width = 306;
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(276, 171);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 4;
			this.btn_fakeCancel.TabStop = false;
			this.btn_fakeCancel.UseVisualStyleBackColor = true;
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.removeItemToolStripMenuItem.Name = "removeItemToolStripMenuItem";
			this.removeItemToolStripMenuItem.Size = new global::System.Drawing.Size(189, 22);
			this.removeItemToolStripMenuItem.Text = "Remove selected date";
			this.removeItemToolStripMenuItem.Click += new global::System.EventHandler(this.removeItemToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(186, 6);
			this.addDateToolStripMenuItem.Name = "addDateToolStripMenuItem";
			this.addDateToolStripMenuItem.Size = new global::System.Drawing.Size(189, 22);
			this.addDateToolStripMenuItem.Text = "Add date";
			this.addDateToolStripMenuItem.Click += new global::System.EventHandler(this.addDateToolStripMenuItem_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(346, 453);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "FrmAccommodationExpiryPresetDatesEdit";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit accommodation expiry preset dates";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.cms_list.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000462 RID: 1122
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000463 RID: 1123
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000464 RID: 1124
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x04000465 RID: 1125
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000466 RID: 1126
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x04000467 RID: 1127
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x04000468 RID: 1128
		private global::System.Windows.Forms.ToolStripButton btn_addNewDate;

		// Token: 0x04000469 RID: 1129
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400046A RID: 1130
		private global::System.Windows.Forms.ToolStripButton btn_removeSelectedDate;

		// Token: 0x0400046B RID: 1131
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x0400046C RID: 1132
		private global::System.Windows.Forms.ContextMenuStrip cms_list;

		// Token: 0x0400046D RID: 1133
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x0400046E RID: 1134
		private global::System.Windows.Forms.ToolStripMenuItem removeItemToolStripMenuItem;

		// Token: 0x0400046F RID: 1135
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

		// Token: 0x04000470 RID: 1136
		private global::System.Windows.Forms.ToolStripMenuItem addDateToolStripMenuItem;
	}
}
