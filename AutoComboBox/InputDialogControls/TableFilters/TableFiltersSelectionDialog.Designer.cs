namespace AutoComboBox.InputDialogControls.TableFilters
{
	// Token: 0x02000071 RID: 113
	public partial class TableFiltersSelectionDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x000236A0 File Offset: 0x000226A0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000236D8 File Offset: 0x000226D8
		private void InitializeComponent()
		{
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.radioButton3 = new global::System.Windows.Forms.RadioButton();
			this.radioButton4 = new global::System.Windows.Forms.RadioButton();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.radioButton2 = new global::System.Windows.Forms.RadioButton();
			this.radioButton1 = new global::System.Windows.Forms.RadioButton();
			this.tableFilterList1 = new global::AutoComboBox.InputDialogControls.TableFilters.TableFilterList();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel2.Controls.Add(this.radioButton3);
			this.panel2.Controls.Add(this.radioButton4);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new global::System.Drawing.Point(0, 31);
			this.panel2.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new global::System.Windows.Forms.Padding(2);
			this.panel2.Size = new global::System.Drawing.Size(679, 31);
			this.panel2.TabIndex = 3;
			this.radioButton3.AutoSize = true;
			this.radioButton3.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.radioButton3.Location = new global::System.Drawing.Point(226, 2);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new global::System.Drawing.Size(152, 27);
			this.radioButton3.TabIndex = 1;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "Match any of the following:";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton4.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.radioButton4.Location = new global::System.Drawing.Point(2, 2);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new global::System.Drawing.Size(224, 27);
			this.radioButton4.TabIndex = 0;
			this.radioButton4.TabStop = true;
			this.radioButton4.Text = "Match all of the following:";
			this.radioButton4.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.radioButton2);
			this.panel1.Controls.Add(this.radioButton1);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new global::System.Drawing.Point(0, 0);
			this.panel1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new global::System.Windows.Forms.Padding(2);
			this.panel1.Size = new global::System.Drawing.Size(679, 31);
			this.panel1.TabIndex = 2;
			this.radioButton2.AutoSize = true;
			this.radioButton2.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.radioButton2.Location = new global::System.Drawing.Point(122, 2);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new global::System.Drawing.Size(77, 27);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "&Show rows";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton1.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.radioButton1.Location = new global::System.Drawing.Point(2, 2);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new global::System.Drawing.Size(120, 27);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "&Hide rows";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.tableFilterList1.DataSource = null;
			this.tableFilterList1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableFilterList1.Location = new global::System.Drawing.Point(0, 62);
			this.tableFilterList1.Name = "tableFilterList1";
			this.tableFilterList1.Size = new global::System.Drawing.Size(679, 296);
			this.tableFilterList1.TabIndex = 4;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(679, 358);
			base.Controls.Add(this.tableFilterList1);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Name = "TableFiltersSelectionDialog";
			this.Text = "TableFiltersSelectionDialog";
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040003D7 RID: 983
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040003D8 RID: 984
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040003D9 RID: 985
		private global::System.Windows.Forms.RadioButton radioButton3;

		// Token: 0x040003DA RID: 986
		private global::System.Windows.Forms.RadioButton radioButton4;

		// Token: 0x040003DB RID: 987
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040003DC RID: 988
		private global::System.Windows.Forms.RadioButton radioButton2;

		// Token: 0x040003DD RID: 989
		private global::System.Windows.Forms.RadioButton radioButton1;

		// Token: 0x040003DE RID: 990
		private global::AutoComboBox.InputDialogControls.TableFilters.TableFilterList tableFilterList1;
	}
}
