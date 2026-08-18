using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000088 RID: 136
	public class SchoolYearChooserCtrl : UserControl
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0002D4E0 File Offset: 0x0002C4E0
		public SchoolYearChooserCtrl(DateScopes dateScopes)
		{
			this.InitializeComponent();
			this.dateScopes = dateScopes;
			if (dateScopes != null)
			{
				this.originalStartDate = dateScopes.startDate;
				this.originalEndDate = dateScopes.endDate;
			}
			else
			{
				this.originalStartDate = DateTime.Now;
				this.originalEndDate = DateTime.Now.AddYears(1);
			}
			this.UpdateDates();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002D560 File Offset: 0x0002C560
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

		// Token: 0x0600056E RID: 1390 RVA: 0x0002D59C File Offset: 0x0002C59C
		private void InitializeComponent()
		{
			this.btn_moveRight = new Button();
			this.btn_moveLeft = new Button();
			this.lbl_year = new Label();
			this.lbl_dates = new Label();
			this.btn_now = new Button();
			this.p_yearText = new Panel();
			this.p_yearText.SuspendLayout();
			base.SuspendLayout();
			this.btn_moveRight.BackColor = SystemColors.Control;
			this.btn_moveRight.Dock = DockStyle.Right;
			this.btn_moveRight.Font = new Font("Marlett", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 2);
			this.btn_moveRight.Location = new Point(188, 0);
			this.btn_moveRight.Name = "btn_moveRight";
			this.btn_moveRight.Size = new Size(36, 35);
			this.btn_moveRight.TabIndex = 0;
			this.btn_moveRight.Text = "4";
			this.btn_moveRight.Click += this.btn_moveRight_Click;
			this.btn_moveLeft.BackColor = SystemColors.Control;
			this.btn_moveLeft.Dock = DockStyle.Right;
			this.btn_moveLeft.Font = new Font("Marlett", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 2);
			this.btn_moveLeft.Location = new Point(112, 0);
			this.btn_moveLeft.Name = "btn_moveLeft";
			this.btn_moveLeft.Size = new Size(36, 35);
			this.btn_moveLeft.TabIndex = 1;
			this.btn_moveLeft.Text = "3";
			this.btn_moveLeft.Click += this.btn_moveLeft_Click;
			this.lbl_year.Dock = DockStyle.Fill;
			this.lbl_year.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbl_year.Location = new Point(2, 2);
			this.lbl_year.Name = "lbl_year";
			this.lbl_year.Size = new Size(106, 21);
			this.lbl_year.TabIndex = 2;
			this.lbl_year.Text = "2005 - 2006";
			this.lbl_year.TextAlign = ContentAlignment.BottomCenter;
			this.lbl_dates.Dock = DockStyle.Bottom;
			this.lbl_dates.Font = new Font("Arial", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbl_dates.Location = new Point(2, 23);
			this.lbl_dates.Name = "lbl_dates";
			this.lbl_dates.Size = new Size(106, 8);
			this.lbl_dates.TabIndex = 3;
			this.lbl_dates.Text = "May 1 - April 30";
			this.lbl_dates.TextAlign = ContentAlignment.BottomCenter;
			this.btn_now.BackColor = SystemColors.Control;
			this.btn_now.Dock = DockStyle.Right;
			this.btn_now.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btn_now.Location = new Point(148, 0);
			this.btn_now.Name = "btn_now";
			this.btn_now.Size = new Size(40, 35);
			this.btn_now.TabIndex = 4;
			this.btn_now.Text = "Now";
			this.btn_now.Click += this.btn_now_Click;
			this.p_yearText.BorderStyle = BorderStyle.FixedSingle;
			this.p_yearText.Controls.Add(this.lbl_year);
			this.p_yearText.Controls.Add(this.lbl_dates);
			this.p_yearText.Dock = DockStyle.Fill;
			this.p_yearText.DockPadding.All = 2;
			this.p_yearText.Location = new Point(0, 0);
			this.p_yearText.Name = "p_yearText";
			this.p_yearText.Size = new Size(112, 35);
			this.p_yearText.TabIndex = 5;
			this.BackColor = SystemColors.ControlLightLight;
			base.Controls.Add(this.p_yearText);
			base.Controls.Add(this.btn_moveLeft);
			base.Controls.Add(this.btn_now);
			base.Controls.Add(this.btn_moveRight);
			this.ForeColor = SystemColors.ControlText;
			base.Name = "SchoolYearChooserCtrl";
			base.Size = new Size(224, 35);
			this.p_yearText.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002DA64 File Offset: 0x0002CA64
		public void UpdateDates()
		{
			if (this.dateScopes == null)
			{
				this.lbl_year.Text = "-";
			}
			else
			{
				this.lbl_year.Text = this.dateScopes.startDate.Year.ToString() + " - " + this.dateScopes.endDate.Year.ToString();
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002DADC File Offset: 0x0002CADC
		private void btn_moveRight_Click(object sender, EventArgs e)
		{
			if (this.dateScopes != null)
			{
				this.dateScopes.startDate = this.dateScopes.startDate.AddYears(1);
				this.dateScopes.endDate = this.dateScopes.endDate.AddYears(1);
				this.UpdateDates();
			}
			else
			{
				this.lbl_year.Text = "-";
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002DB50 File Offset: 0x0002CB50
		private void btn_moveLeft_Click(object sender, EventArgs e)
		{
			if (this.dateScopes != null)
			{
				this.dateScopes.startDate = this.dateScopes.startDate.AddYears(-1);
				this.dateScopes.endDate = this.dateScopes.endDate.AddYears(-1);
				this.UpdateDates();
			}
			else
			{
				this.lbl_year.Text = "-";
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002DBC1 File Offset: 0x0002CBC1
		private void btn_now_Click(object sender, EventArgs e)
		{
			this.dateScopes.startDate = this.originalStartDate;
			this.dateScopes.endDate = this.originalEndDate;
			this.UpdateDates();
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0002DBF0 File Offset: 0x0002CBF0
		public DateScopes DateScopes
		{
			get
			{
				return this.dateScopes;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0002DC08 File Offset: 0x0002CC08
		public DateTime StartDate
		{
			get
			{
				DateTime result;
				if (this.dateScopes == null)
				{
					result = DateTime.MinValue;
				}
				else
				{
					result = this.dateScopes.startDate;
				}
				return result;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0002DC40 File Offset: 0x0002CC40
		public DateTime EndDate
		{
			get
			{
				DateTime result;
				if (this.dateScopes == null)
				{
					result = DateTime.MinValue;
				}
				else
				{
					result = this.dateScopes.endDate;
				}
				return result;
			}
		}

		// Token: 0x04000482 RID: 1154
		private Button btn_moveRight;

		// Token: 0x04000483 RID: 1155
		private Button btn_moveLeft;

		// Token: 0x04000484 RID: 1156
		private Label lbl_year;

		// Token: 0x04000485 RID: 1157
		private Label lbl_dates;

		// Token: 0x04000486 RID: 1158
		private Panel p_yearText;

		// Token: 0x04000487 RID: 1159
		private Button btn_now;

		// Token: 0x04000488 RID: 1160
		private Container components = null;

		// Token: 0x04000489 RID: 1161
		private DateScopes dateScopes = null;

		// Token: 0x0400048A RID: 1162
		private DateTime originalStartDate;

		// Token: 0x0400048B RID: 1163
		private DateTime originalEndDate;
	}
}
