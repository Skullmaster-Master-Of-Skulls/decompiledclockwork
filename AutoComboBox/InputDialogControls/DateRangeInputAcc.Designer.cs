namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000CE RID: 206
	public partial class DateRangeInputAcc : global::System.Windows.Forms.Form
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x0003E4A0 File Offset: 0x0003D4A0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0003E4D8 File Offset: 0x0003D4D8
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputDialogControls.DateRangeInputAcc));
			this.dtp_date = new global::AutoComboBox.MyControls.MyDateTimePickerAcc();
			this.btn_select = new global::System.Windows.Forms.Button();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.dtp_date.AccessibleDescription = "Date";
			this.dtp_date.AccessibleName = "Date";
			this.dtp_date.Date = new global::System.DateTime(2008, 10, 23, 0, 0, 0, 0);
			this.dtp_date.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.dtp_date.Location = new global::System.Drawing.Point(15, 13);
			this.dtp_date.Margin = new global::System.Windows.Forms.Padding(6, 6, 6, 6);
			this.dtp_date.Name = "dtp_date";
			this.dtp_date.Size = new global::System.Drawing.Size(402, 68);
			this.dtp_date.TabIndex = 0;
			this.btn_select.AccessibleDescription = "Select";
			this.btn_select.AccessibleName = "Select";
			this.btn_select.Location = new global::System.Drawing.Point(160, 82);
			this.btn_select.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btn_select.Name = "btn_select";
			this.btn_select.Size = new global::System.Drawing.Size(112, 32);
			this.btn_select.TabIndex = 1;
			this.btn_select.Text = "&Select";
			this.btn_select.UseVisualStyleBackColor = true;
			this.btn_select.Click += new global::System.EventHandler(this.btn_select_Click);
			this.btn_cancel.AccessibleDescription = "Cancel";
			this.btn_cancel.AccessibleName = "Cancel";
			this.btn_cancel.Location = new global::System.Drawing.Point(298, 82);
			this.btn_cancel.Margin = new global::System.Windows.Forms.Padding(4);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(112, 32);
			this.btn_cancel.TabIndex = 2;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.UseVisualStyleBackColor = true;
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(423, 123);
			base.Controls.Add(this.btn_cancel);
			base.Controls.Add(this.btn_select);
			base.Controls.Add(this.dtp_date);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			base.Name = "DateRangeInputAcc";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select a date";
			base.Load += new global::System.EventHandler(this.DateRangeInputAcc_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x040005F5 RID: 1525
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040005F6 RID: 1526
		private global::AutoComboBox.MyControls.MyDateTimePickerAcc dtp_date;

		// Token: 0x040005F7 RID: 1527
		private global::System.Windows.Forms.Button btn_select;

		// Token: 0x040005F8 RID: 1528
		private global::System.Windows.Forms.Button btn_cancel;
	}
}
