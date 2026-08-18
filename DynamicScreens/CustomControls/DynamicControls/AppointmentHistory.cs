using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid;
using UnivOleDb;

namespace DynamicScreens.CustomControls.DynamicControls
{
	// Token: 0x0200000F RID: 15
	public class AppointmentHistory : UserControl
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00007FC1 File Offset: 0x00006FC1
		public AppointmentHistory()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007FDC File Offset: 0x00006FDC
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00007FF4 File Offset: 0x00006FF4
		public int Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				if (this.pid != value)
				{
					this.pid = value;
					this.RefreshList();
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000801F File Offset: 0x0000701F
		public UnivDataAdapter Da
		{
			set
			{
				this.da = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00008029 File Offset: 0x00007029
		public TripleDESEncryptionClass TripleDES
		{
			set
			{
				this.tripleDES = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00008034 File Offset: 0x00007034
		// (set) Token: 0x060000ED RID: 237 RVA: 0x0000804C File Offset: 0x0000704C
		public string AppTypeIds
		{
			get
			{
				return this.appTypeIds;
			}
			set
			{
				this.appTypeIds = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00008058 File Offset: 0x00007058
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00008075 File Offset: 0x00007075
		public string Title
		{
			get
			{
				return this.lbl.Text;
			}
			set
			{
				this.lbl.Text = value;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008088 File Offset: 0x00007088
		public void RefreshList()
		{
			this.da.SelectCommand.CommandText = "SELECT a.* FROM apps a WHERE a.personid=@pid AND a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')) ORDER BY a.startdate";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@apptypeids", this.appTypeIds);
			this.da.SelectCommand.Parameters.Add("@pid", this.pid);
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			dataTable = new DataTable();
			dataTable.Columns.Add("Date", typeof(DateTime));
			dataTable.Columns.Add("Who");
			dataTable.Columns.Add("Type");
			dataTable.Columns.Add("Status");
			dataTable.Rows.Add(new object[]
			{
				new DateTime(2009, 9, 15),
				"Mike Dinunzio",
				"Client interview"
			});
			dataTable.Rows.Add(new object[]
			{
				new DateTime(2009, 10, 31),
				"Mike Dinunzio",
				"Client interview"
			});
			BindingSource bindingSource = new BindingSource();
			bindingSource.DataSource = dataTable;
			this.grid.DataSource = bindingSource;
			this.grid.BestFitColumns();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000820C File Offset: 0x0000720C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008244 File Offset: 0x00007244
		private void InitializeComponent()
		{
			this.grid = new CtrlGrid();
			this.toolStrip1 = new ToolStrip();
			this.btn_email = new ToolStripButton();
			this.lbl = new Label();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.grid.Dock = DockStyle.Fill;
			this.grid.Location = new Point(0, 16);
			this.grid.Margin = new Padding(3, 4, 3, 4);
			this.grid.MultiSelect = true;
			this.grid.Name = "grid";
			this.grid.Padding = new Padding(0, 0, 0, 1);
			this.grid.EnableGrouping = false;
			this.grid.Size = new Size(150, 109);
			this.grid.TabIndex = 55;
			this.grid.ThemeName = "Office2010Silver";
			this.toolStrip1.Dock = DockStyle.Bottom;
			this.toolStrip1.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_email
			});
			this.toolStrip1.Location = new Point(0, 125);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(150, 25);
			this.toolStrip1.TabIndex = 57;
			this.toolStrip1.TabStop = true;
			this.btn_email.Image = Resources.star_yellow_new;
			this.btn_email.ImageTransparentColor = Color.Magenta;
			this.btn_email.Name = "btn_email";
			this.btn_email.Size = new Size(128, 22);
			this.btn_email.Text = "&New appointment";
			this.lbl.AutoSize = true;
			this.lbl.Dock = DockStyle.Top;
			this.lbl.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl.Location = new Point(0, 0);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(135, 16);
			this.lbl.TabIndex = 56;
			this.lbl.Text = "Appointment history";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.grid);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl);
			base.Name = "AppointmentHistory";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000072 RID: 114
		private UnivDataAdapter da;

		// Token: 0x04000073 RID: 115
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000074 RID: 116
		private int pid;

		// Token: 0x04000075 RID: 117
		private string appTypeIds;

		// Token: 0x04000076 RID: 118
		private IContainer components = null;

		// Token: 0x04000077 RID: 119
		private ToolStripButton btn_email;

		// Token: 0x04000078 RID: 120
		private CtrlGrid grid;

		// Token: 0x04000079 RID: 121
		private ToolStrip toolStrip1;

		// Token: 0x0400007A RID: 122
		private Label lbl;
	}
}
