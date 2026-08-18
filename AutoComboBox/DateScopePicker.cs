using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000AD RID: 173
	public class DateScopePicker : UserControl
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00033C44 File Offset: 0x00032C44
		public DateScopePicker(DateScopes _DateScopes)
		{
			this.InitializeComponent();
			this.dateScopes = _DateScopes;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00033C64 File Offset: 0x00032C64
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

		// Token: 0x06000676 RID: 1654 RVA: 0x00033CA0 File Offset: 0x00032CA0
		private void InitializeComponent()
		{
			this.lbl_yearRange = new Label();
			this.btn_now = new Button();
			this.btn_yearUp = new Button();
			this.btn_yearDown = new Button();
			this.lbl_availableDateRange = new Label();
			this.cmb_availableDateRanges = new AutoComboBox();
			base.SuspendLayout();
			this.lbl_yearRange.BackColor = SystemColors.Info;
			this.lbl_yearRange.BorderStyle = BorderStyle.FixedSingle;
			this.lbl_yearRange.Dock = DockStyle.Right;
			this.lbl_yearRange.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl_yearRange.ForeColor = SystemColors.InfoText;
			this.lbl_yearRange.Location = new Point(300, 2);
			this.lbl_yearRange.Name = "lbl_yearRange";
			this.lbl_yearRange.Size = new Size(88, 28);
			this.lbl_yearRange.TabIndex = 5;
			this.lbl_yearRange.TextAlign = ContentAlignment.MiddleCenter;
			this.btn_now.Dock = DockStyle.Right;
			this.btn_now.FlatStyle = FlatStyle.System;
			this.btn_now.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btn_now.Location = new Point(388, 2);
			this.btn_now.Name = "btn_now";
			this.btn_now.Size = new Size(56, 28);
			this.btn_now.TabIndex = 8;
			this.btn_now.Text = "NOW";
			this.btn_now.Click += this.btn_now_Click;
			this.btn_yearUp.Dock = DockStyle.Right;
			this.btn_yearUp.FlatStyle = FlatStyle.System;
			this.btn_yearUp.Font = new Font("Marlett", 12f, FontStyle.Bold, GraphicsUnit.Point, 2);
			this.btn_yearUp.Location = new Point(444, 2);
			this.btn_yearUp.Name = "btn_yearUp";
			this.btn_yearUp.Size = new Size(32, 28);
			this.btn_yearUp.TabIndex = 7;
			this.btn_yearUp.Text = "5";
			this.btn_yearUp.Click += this.btn_yearUp_Click;
			this.btn_yearDown.Dock = DockStyle.Right;
			this.btn_yearDown.FlatStyle = FlatStyle.System;
			this.btn_yearDown.Font = new Font("Marlett", 12f, FontStyle.Bold, GraphicsUnit.Point, 2);
			this.btn_yearDown.Location = new Point(476, 2);
			this.btn_yearDown.Name = "btn_yearDown";
			this.btn_yearDown.Size = new Size(32, 28);
			this.btn_yearDown.TabIndex = 6;
			this.btn_yearDown.Text = "6";
			this.btn_yearDown.Click += this.btn_yearDown_Click;
			this.lbl_availableDateRange.BorderStyle = BorderStyle.Fixed3D;
			this.lbl_availableDateRange.Dock = DockStyle.Fill;
			this.lbl_availableDateRange.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl_availableDateRange.Location = new Point(2, 2);
			this.lbl_availableDateRange.Name = "lbl_availableDateRange";
			this.lbl_availableDateRange.Size = new Size(298, 28);
			this.lbl_availableDateRange.TabIndex = 10;
			this.lbl_availableDateRange.Text = "label1";
			this.lbl_availableDateRange.TextAlign = ContentAlignment.MiddleLeft;
			this.cmb_availableDateRanges.AutoCompleteEnabled = true;
			this.cmb_availableDateRanges.Dock = DockStyle.Fill;
			this.cmb_availableDateRanges.Location = new Point(2, 2);
			this.cmb_availableDateRanges.Name = "cmb_availableDateRanges";
			this.cmb_availableDateRanges.Size = new Size(298, 26);
			this.cmb_availableDateRanges.TabIndex = 11;
			this.cmb_availableDateRanges.Visible = false;
			base.Controls.Add(this.cmb_availableDateRanges);
			base.Controls.Add(this.lbl_availableDateRange);
			base.Controls.Add(this.lbl_yearRange);
			base.Controls.Add(this.btn_now);
			base.Controls.Add(this.btn_yearUp);
			base.Controls.Add(this.btn_yearDown);
			base.DockPadding.All = 2;
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Name = "DateScopePicker";
			base.Size = new Size(510, 32);
			base.Load += this.DateScopePicker_Load;
			base.ResumeLayout(false);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00034188 File Offset: 0x00033188
		private void DateScopePicker_Load(object sender, EventArgs e)
		{
			if (this.dateScopes != null)
			{
				if (this.dateScopes.Count() > 1)
				{
					this.cmb_availableDateRanges.Visible = true;
					this.lbl_availableDateRange.Visible = false;
					foreach (DateScope item in this.dateScopes.dateScopes)
					{
						this.cmb_availableDateRanges.Items.Add(item);
					}
					this.cmb_availableDateRanges.SelectedItem = this.dateScopes.dateScope;
				}
				else if (this.dateScopes.Count() > 1)
				{
					this.lbl_availableDateRange.Text = this.dateScopes.dateScopes[0].description;
				}
			}
			this.DateScopeChanged();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0003426C File Offset: 0x0003326C
		private void DateScopeChanged()
		{
			if (this.dateScopes.dateScope != null)
			{
				if (this.dateScopes.startDate.Year == this.dateScopes.endDate.Year)
				{
					this.lbl_yearRange.Text = this.dateScopes.startDate.ToString("yyyy");
				}
				else
				{
					this.lbl_yearRange.Text = this.dateScopes.startDate.ToString("yyyy") + " - " + this.dateScopes.endDate.ToString("yyyy");
				}
			}
			else
			{
				this.lbl_yearRange.Text = "";
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00034334 File Offset: 0x00033334
		private void btn_now_Click(object sender, EventArgs e)
		{
			this.dateScopes.SetScope(DateTime.Now);
			this.DateScopeChanged();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0003434F File Offset: 0x0003334F
		private void btn_yearUp_Click(object sender, EventArgs e)
		{
			this.dateScopes.MoveScope(1);
			this.DateScopeChanged();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00034366 File Offset: 0x00033366
		private void btn_yearDown_Click(object sender, EventArgs e)
		{
			this.dateScopes.MoveScope(-1);
			this.DateScopeChanged();
		}

		// Token: 0x0400050E RID: 1294
		public const int COLS_dateRangeID = 0;

		// Token: 0x0400050F RID: 1295
		public const int COLS_description = 1;

		// Token: 0x04000510 RID: 1296
		public const int COLS_startMonth = 2;

		// Token: 0x04000511 RID: 1297
		public const int COLS_endMonth = 3;

		// Token: 0x04000512 RID: 1298
		public const int COLS_numYearsBetween = 4;

		// Token: 0x04000513 RID: 1299
		public const int COLS_useCode = 5;

		// Token: 0x04000514 RID: 1300
		public const int COLS_startDay = 6;

		// Token: 0x04000515 RID: 1301
		public const int COLS_endDay = 7;

		// Token: 0x04000516 RID: 1302
		private Label lbl_yearRange;

		// Token: 0x04000517 RID: 1303
		private Button btn_now;

		// Token: 0x04000518 RID: 1304
		private Button btn_yearUp;

		// Token: 0x04000519 RID: 1305
		private Button btn_yearDown;

		// Token: 0x0400051A RID: 1306
		private Label lbl_availableDateRange;

		// Token: 0x0400051B RID: 1307
		private AutoComboBox cmb_availableDateRanges;

		// Token: 0x0400051C RID: 1308
		private Container components = null;

		// Token: 0x0400051D RID: 1309
		private DateScopes dateScopes;
	}
}
