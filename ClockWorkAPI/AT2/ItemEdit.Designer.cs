namespace ClockWorkAPI.AT2
{
	// Token: 0x02000078 RID: 120
	public partial class ItemEdit : global::System.Windows.Forms.Form
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x00020514 File Offset: 0x0001F514
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002054C File Offset: 0x0001F54C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ClockWorkAPI.AT2.ItemEdit));
			this.txt_item = new global::DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX1 = new global::DevComponents.DotNetBar.LabelX();
			this.tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.txt_cost = new global::System.Windows.Forms.MaskedTextBox();
			this.labelX3 = new global::DevComponents.DotNetBar.LabelX();
			this.txt_category = new global::DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX2 = new global::DevComponents.DotNetBar.LabelX();
			this.txt_vendor = new global::DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX4 = new global::DevComponents.DotNetBar.LabelX();
			this.labelX5 = new global::DevComponents.DotNetBar.LabelX();
			this.txt_description = new global::DevComponents.DotNetBar.Controls.TextBoxX();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new global::System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			base.SuspendLayout();
			this.txt_item.Border.Class = "TextBoxBorder";
			this.txt_item.Location = new global::System.Drawing.Point(119, 4);
			this.txt_item.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_item.Name = "txt_item";
			this.txt_item.Size = new global::System.Drawing.Size(272, 22);
			this.txt_item.TabIndex = 2;
			this.labelX1.Location = new global::System.Drawing.Point(3, 4);
			this.labelX1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new global::System.Drawing.Size(87, 22);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "Item";
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 24.84076f));
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 75.15924f));
			this.tableLayoutPanel1.Controls.Add(this.txt_cost, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.labelX3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txt_category, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelX2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txt_item, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelX1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_vendor, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.labelX4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.labelX5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.txt_description, 1, 4);
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(24, 29);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(471, 180);
			this.tableLayoutPanel1.TabIndex = 0;
			this.txt_cost.Location = new global::System.Drawing.Point(119, 93);
			this.txt_cost.Name = "txt_cost";
			this.txt_cost.Size = new global::System.Drawing.Size(116, 22);
			this.txt_cost.TabIndex = 8;
			this.txt_cost.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.labelX3.Location = new global::System.Drawing.Point(3, 64);
			this.labelX3.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new global::System.Drawing.Size(87, 22);
			this.labelX3.TabIndex = 5;
			this.labelX3.Text = "Vendor";
			this.txt_category.Border.Class = "TextBoxBorder";
			this.txt_category.Location = new global::System.Drawing.Point(119, 34);
			this.txt_category.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_category.Name = "txt_category";
			this.txt_category.Size = new global::System.Drawing.Size(272, 22);
			this.txt_category.TabIndex = 4;
			this.labelX2.Location = new global::System.Drawing.Point(3, 34);
			this.labelX2.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new global::System.Drawing.Size(87, 22);
			this.labelX2.TabIndex = 3;
			this.labelX2.Text = "Category";
			this.txt_vendor.Border.Class = "TextBoxBorder";
			this.txt_vendor.Location = new global::System.Drawing.Point(119, 64);
			this.txt_vendor.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_vendor.Name = "txt_vendor";
			this.txt_vendor.Size = new global::System.Drawing.Size(272, 22);
			this.txt_vendor.TabIndex = 6;
			this.labelX4.Location = new global::System.Drawing.Point(3, 94);
			this.labelX4.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new global::System.Drawing.Size(87, 22);
			this.labelX4.TabIndex = 7;
			this.labelX4.Text = "Cost";
			this.labelX5.Location = new global::System.Drawing.Point(3, 124);
			this.labelX5.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new global::System.Drawing.Size(87, 22);
			this.labelX5.TabIndex = 9;
			this.labelX5.Text = "Description";
			this.txt_description.Border.Class = "TextBoxBorder";
			this.txt_description.Location = new global::System.Drawing.Point(119, 124);
			this.txt_description.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_description.Multiline = true;
			this.txt_description.Name = "txt_description";
			this.txt_description.Size = new global::System.Drawing.Size(272, 43);
			this.txt_description.TabIndex = 10;
			this.toolStrip2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip2.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripButton1,
				this.toolStripButton2
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 250);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Padding = new global::System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStrip2.Size = new global::System.Drawing.Size(525, 39);
			this.toolStrip2.TabIndex = 11;
			this.toolStrip2.TabStop = true;
			this.toolStrip2.Text = "toolStrip2";
			this.toolStripButton1.Image = global::ClockWorkAPI.Properties.Resources.check2;
			this.toolStripButton1.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new global::System.Drawing.Size(64, 36);
			this.toolStripButton1.Text = "&Ok";
			this.toolStripButton1.Click += new global::System.EventHandler(this.toolStripButton1_Click);
			this.toolStripButton2.Image = global::ClockWorkAPI.Properties.Resources.delete2;
			this.toolStripButton2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new global::System.Drawing.Size(93, 36);
			this.toolStripButton2.Text = "&Cancel";
			this.toolStripButton2.Click += new global::System.EventHandler(this.toolStripButton2_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(525, 289);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.tableLayoutPanel1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "ItemEdit";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Item Edit";
			base.Load += new global::System.EventHandler(this.ItemEdit_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000312 RID: 786
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000313 RID: 787
		private global::DevComponents.DotNetBar.Controls.TextBoxX txt_item;

		// Token: 0x04000314 RID: 788
		private global::DevComponents.DotNetBar.LabelX labelX1;

		// Token: 0x04000315 RID: 789
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000316 RID: 790
		private global::DevComponents.DotNetBar.LabelX labelX3;

		// Token: 0x04000317 RID: 791
		private global::DevComponents.DotNetBar.Controls.TextBoxX txt_category;

		// Token: 0x04000318 RID: 792
		private global::DevComponents.DotNetBar.LabelX labelX2;

		// Token: 0x04000319 RID: 793
		private global::DevComponents.DotNetBar.Controls.TextBoxX txt_vendor;

		// Token: 0x0400031A RID: 794
		private global::DevComponents.DotNetBar.LabelX labelX4;

		// Token: 0x0400031B RID: 795
		private global::DevComponents.DotNetBar.LabelX labelX5;

		// Token: 0x0400031C RID: 796
		private global::DevComponents.DotNetBar.Controls.TextBoxX txt_description;

		// Token: 0x0400031D RID: 797
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x0400031E RID: 798
		private global::System.Windows.Forms.ToolStripButton toolStripButton1;

		// Token: 0x0400031F RID: 799
		private global::System.Windows.Forms.ToolStripButton toolStripButton2;

		// Token: 0x04000320 RID: 800
		private global::System.Windows.Forms.MaskedTextBox txt_cost;
	}
}
