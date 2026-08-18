using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using DevComponents.DotNetBar;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x0200002E RID: 46
	public class ReportFunctionParameterControl_CommaSeparatedWithColumnChooser : UserControl, iReportFunctionParameter
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0003C5D8 File Offset: 0x0003B5D8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0003C610 File Offset: 0x0003B610
		private void InitializeComponent()
		{
			this.components = new Container();
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.txt = new TextBox();
			this.expandableSplitter1 = new ExpandableSplitter();
			this.lv = new ListViewEx();
			this.label1 = new Label();
			this.columnHeader1 = new ColumnHeader();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.BorderStyle = BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.txt);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new Padding(2);
			this.panel1.Size = new Size(281, 316);
			this.panel1.TabIndex = 0;
			this.panel2.Controls.Add(this.lv);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = DockStyle.Right;
			this.panel2.Location = new Point(291, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(200, 316);
			this.panel2.TabIndex = 1;
			this.txt.Dock = DockStyle.Fill;
			this.txt.Location = new Point(2, 2);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.ScrollBars = ScrollBars.Vertical;
			this.txt.Size = new Size(273, 308);
			this.txt.TabIndex = 0;
			this.expandableSplitter1.BackColor2 = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.Dock = DockStyle.Right;
			this.expandableSplitter1.ExpandFillColor = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.ExpandFillColorSchemePart = 53;
			this.expandableSplitter1.ExpandLineColor = SystemColors.ControlText;
			this.expandableSplitter1.ExpandLineColorSchemePart = 40;
			this.expandableSplitter1.GripDarkColor = SystemColors.ControlText;
			this.expandableSplitter1.GripDarkColorSchemePart = 40;
			this.expandableSplitter1.GripLightColor = Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.GripLightColorSchemePart = 0;
			this.expandableSplitter1.HotBackColor = Color.FromArgb(254, 142, 75);
			this.expandableSplitter1.HotBackColor2 = Color.FromArgb(255, 207, 139);
			this.expandableSplitter1.HotBackColor2SchemePart = 35;
			this.expandableSplitter1.HotBackColorSchemePart = 34;
			this.expandableSplitter1.HotExpandFillColor = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotExpandFillColorSchemePart = 53;
			this.expandableSplitter1.HotExpandLineColor = SystemColors.ControlText;
			this.expandableSplitter1.HotExpandLineColorSchemePart = 40;
			this.expandableSplitter1.HotGripDarkColor = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotGripDarkColorSchemePart = 53;
			this.expandableSplitter1.HotGripLightColor = Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.HotGripLightColorSchemePart = 0;
			this.expandableSplitter1.Location = new Point(281, 0);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new Size(10, 316);
			this.expandableSplitter1.TabIndex = 2;
			this.expandableSplitter1.TabStop = false;
			this.lv.AutoSortingEnabled = false;
			this.lv.BackColourSelected = Color.LightBlue;
			this.lv.CalcButtonCid = 0;
			this.lv.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv.DefaultSortByAsc = true;
			this.lv.DefaultSortByColInd = -1;
			this.lv.Dock = DockStyle.Fill;
			this.lv.DrawMode = DrawMode.Normal;
			this.lv.EnterTriggersDoubleClickEvent = false;
			this.lv.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lv.IsFileList = false;
			this.lv.ItemHeight = 16;
			this.lv.Location = new Point(0, 60);
			this.lv.Name = "lv";
			this.lv.NoDeleting = false;
			this.lv.NoEditing = false;
			this.lv.Size = new Size(200, 256);
			this.lv.TabIndex = 0;
			this.lv.Tag2 = null;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = View.Details;
			this.lv.DoubleClick += this.lv_DoubleClick;
			this.label1.Dock = DockStyle.Top;
			this.label1.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(200, 60);
			this.label1.TabIndex = 1;
			this.label1.Text = "Double-click on a column name to add to the list on the left.";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.columnHeader1.Text = "Column name";
			this.columnHeader1.Width = 170;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.panel2);
			base.Name = "ReportFunctionParameterControl_CommaSeparatedWithColumnChooser";
			base.Size = new Size(491, 316);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0003CD09 File Offset: 0x0003BD09
		public ReportFunctionParameterControl_CommaSeparatedWithColumnChooser()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0003CD2C File Offset: 0x0003BD2C
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0003CD44 File Offset: 0x0003BD44
		public TextParameterType TextParameterType
		{
			get
			{
				return this.textParameterType;
			}
			set
			{
				this.textParameterType = value;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0003CD50 File Offset: 0x0003BD50
		public void SetColumnsToChooseFrom(string sqlCode)
		{
			try
			{
				string text = sqlCode.ToLower();
				int num = text.IndexOf("select");
				if (num >= 0)
				{
					string text2 = text.Substring(num + 6);
					int num2 = text2.IndexOf("from", num + 1);
					if (num2 > 0)
					{
						text2 = text2.Substring(0, num2);
						string[] array = text2.Split(new char[]
						{
							','
						});
						this.lv.Items.Clear();
						foreach (string text3 in array)
						{
							string text4 = text3.Trim();
							if (text4.Length > 0)
							{
								int num3 = text4.IndexOf('.');
								if (num3 > 0)
								{
									text4 = text4.Substring(num3 + 1);
								}
								int num4 = text4.IndexOf(" as ");
								if (num4 > 0)
								{
									text4 = text4.Substring(num4 + 4);
								}
								if (text4.IndexOf('@') < 0)
								{
									this.lv.Items.Add(text4);
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0003CECC File Offset: 0x0003BECC
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0003CEE9 File Offset: 0x0003BEE9
		public string Parameter
		{
			get
			{
				return this.txt.Text;
			}
			set
			{
				this.txt.Text = value;
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0003CEFC File Offset: 0x0003BEFC
		private void lv_DoubleClick(object sender, EventArgs e)
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				string text = this.lv.SelectedItems[0].Text;
				TextParameterType textParameterType = this.textParameterType;
				if (textParameterType == TextParameterType.CommaSeparatedList)
				{
					if (!this.txt.Text.Contains(text))
					{
						string text2 = this.txt.Text.Trim();
						if (text2.Length > 0)
						{
							text2 += ",";
						}
						this.txt.Text = text2 + text;
					}
				}
			}
		}

		// Token: 0x04000167 RID: 359
		private IContainer components = null;

		// Token: 0x04000168 RID: 360
		private Panel panel1;

		// Token: 0x04000169 RID: 361
		private TextBox txt;

		// Token: 0x0400016A RID: 362
		private Panel panel2;

		// Token: 0x0400016B RID: 363
		private ExpandableSplitter expandableSplitter1;

		// Token: 0x0400016C RID: 364
		private ListViewEx lv;

		// Token: 0x0400016D RID: 365
		private Label label1;

		// Token: 0x0400016E RID: 366
		private ColumnHeader columnHeader1;

		// Token: 0x0400016F RID: 367
		private TextParameterType textParameterType = TextParameterType.CommaSeparatedList;
	}
}
