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
	// Token: 0x02000045 RID: 69
	public class EmailHistory : UserControl
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x00033AA0 File Offset: 0x00032AA0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00033AD8 File Offset: 0x00032AD8
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
			this.grid.Name = "grid";
			this.grid.Size = new Size(444, 242);
			this.grid.TabIndex = 52;
			this.grid.ThemeName = "Office2010Silver";
			this.toolStrip1.Dock = DockStyle.Bottom;
			this.toolStrip1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_email
			});
			this.toolStrip1.Location = new Point(0, 258);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(444, 25);
			this.toolStrip1.TabIndex = 54;
			this.toolStrip1.TabStop = true;
			this.btn_email.Image = Resources.star_yellow_new;
			this.btn_email.ImageTransparentColor = Color.Magenta;
			this.btn_email.Name = "btn_email";
			this.btn_email.Size = new Size(51, 22);
			this.btn_email.Text = "&Email";
			this.btn_email.Click += this.btn_email_Click;
			this.lbl.AutoSize = true;
			this.lbl.Dock = DockStyle.Top;
			this.lbl.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl.Location = new Point(0, 0);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(90, 16);
			this.lbl.TabIndex = 53;
			this.lbl.Text = "Email history";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.grid);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl);
			base.Name = "EmailHistory";
			base.Size = new Size(444, 283);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00033DFA File Offset: 0x00032DFA
		public EmailHistory()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00033E1C File Offset: 0x00032E1C
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00033E34 File Offset: 0x00032E34
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

		// Token: 0x1700011C RID: 284
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00033E5F File Offset: 0x00032E5F
		public UnivDataAdapter Da
		{
			set
			{
				this.da = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00033E69 File Offset: 0x00032E69
		public TripleDESEncryptionClass TripleDES
		{
			set
			{
				this.tripleDES = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00033E74 File Offset: 0x00032E74
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00033E8C File Offset: 0x00032E8C
		public EmailHistoryMode EmailHistoryMode
		{
			get
			{
				return this.emailHistoryMode;
			}
			set
			{
				this.emailHistoryMode = value;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00033E98 File Offset: 0x00032E98
		public void RefreshList()
		{
			if (this.da != null)
			{
				if (this.emailHistoryMode == EmailHistoryMode.Case)
				{
					this.da.SelectCommand.CommandText = "SELECT eh.datesent,p.firstname,p.lastname,et.efrom AS EmailTemplate,eh.etoccbcc FROM emailhistory eh LEFT JOIN people p ON p.personid=eh.sentby LEFT JOIN emailtemplates et ON et.templateid=eh.templateid WHERE eh.infopcid=@id";
				}
				else
				{
					this.da.SelectCommand.CommandText = "SELECT eh.datesent,p.firstname,p.lastname,et.efrom AS EmailTemplate,eh.etoccbcc FROM emailhistory eh LEFT JOIN people p ON p.personid=eh.sentby LEFT JOIN emailtemplates et ON et.templateid=eh.templateid WHERE eh.personid=@id";
				}
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@id", this.pid);
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				BindingSource bindingSource = new BindingSource();
				bindingSource.DataSource = dataTable;
				bindingSource.Sort = "datesent";
				this.grid.DataSource = bindingSource;
				this.grid.BestFitColumns();
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00033F78 File Offset: 0x00032F78
		private void btn_email_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00033F7C File Offset: 0x00032F7C
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00033F99 File Offset: 0x00032F99
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

		// Token: 0x040002BF RID: 703
		private IContainer components = null;

		// Token: 0x040002C0 RID: 704
		private CtrlGrid grid;

		// Token: 0x040002C1 RID: 705
		private ToolStrip toolStrip1;

		// Token: 0x040002C2 RID: 706
		private ToolStripButton btn_email;

		// Token: 0x040002C3 RID: 707
		private Label lbl;

		// Token: 0x040002C4 RID: 708
		private UnivDataAdapter da;

		// Token: 0x040002C5 RID: 709
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040002C6 RID: 710
		private int pid;

		// Token: 0x040002C7 RID: 711
		private EmailHistoryMode emailHistoryMode = EmailHistoryMode.Student;
	}
}
