using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoComboBox;
using DevComponents.DotNetBar;
using ReportFunctions.Properties;
using TechnoPro.ClockWork.ClockWorkMigration;
using TechnoPro.ClockWork.ClockWorkMigration.ctrls;
using UnivOleDb;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x02000045 RID: 69
	public class ReportFunctionParameterControl_DataSyncDataMap : UserControl, iReportFunctionParameter
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00049A8C File Offset: 0x00048A8C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00049AC4 File Offset: 0x00048AC4
		private void InitializeComponent()
		{
			this.components = new Container();
			this.listViewEx1 = new ListViewEx();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.clockWorkFieldChooser1 = new ClockWorkFieldChooser();
			this.toolStrip1 = new ToolStrip();
			this.toolStripButton1 = new ToolStripButton();
			this.expandableSplitter1 = new ExpandableSplitter();
			this.panel2 = new Panel();
			this.lv = new ListViewEx();
			this.columnHeader4 = new ColumnHeader();
			this.label1 = new Label();
			this.toolStrip1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.listViewEx1.AutoSortingEnabled = false;
			this.listViewEx1.BackColourSelected = Color.LightBlue;
			this.listViewEx1.CalcButtonCid = 0;
			this.listViewEx1.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3
			});
			this.listViewEx1.DefaultSortByAsc = true;
			this.listViewEx1.DefaultSortByColInd = -1;
			this.listViewEx1.Dock = DockStyle.Fill;
			this.listViewEx1.DrawMode = DrawMode.Normal;
			this.listViewEx1.EnterTriggersDoubleClickEvent = false;
			this.listViewEx1.FullRowSelect = true;
			this.listViewEx1.IsFileList = false;
			this.listViewEx1.ItemHeight = 16;
			this.listViewEx1.Location = new Point(0, 25);
			this.listViewEx1.Name = "listViewEx1";
			this.listViewEx1.NoDeleting = false;
			this.listViewEx1.NoEditing = false;
			this.listViewEx1.Size = new Size(281, 291);
			this.listViewEx1.TabIndex = 0;
			this.listViewEx1.Tag2 = null;
			this.listViewEx1.UseCompatibleStateImageBehavior = false;
			this.listViewEx1.View = View.Details;
			this.listViewEx1.SelectedIndexChanged += this.listViewEx1_SelectedIndexChanged;
			this.listViewEx1.SubItemEndEditing += this.listViewEx1_SubItemEndEditing;
			this.listViewEx1.SubItemClicked += this.listViewEx1_SubItemClicked;
			this.columnHeader1.Text = "Report column";
			this.columnHeader1.Width = 180;
			this.columnHeader2.Text = "ClockWork field";
			this.columnHeader2.Width = 217;
			this.columnHeader3.Text = "controlid";
			this.clockWorkFieldChooser1.Location = new Point(3, 163);
			this.clockWorkFieldChooser1.Name = "clockWorkFieldChooser1";
			this.clockWorkFieldChooser1.Size = new Size(569, 233);
			this.clockWorkFieldChooser1.TabIndex = 1;
			this.clockWorkFieldChooser1.Visible = false;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripButton1
			});
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(491, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripButton1.Image = Resources.star_yellow_new;
			this.toolStripButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new Size(153, 22);
			this.toolStripButton1.Text = "&Add new report column";
			this.toolStripButton1.Click += this.toolStripButton1_Click;
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
			this.expandableSplitter1.Location = new Point(281, 25);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new Size(10, 291);
			this.expandableSplitter1.TabIndex = 4;
			this.expandableSplitter1.TabStop = false;
			this.panel2.Controls.Add(this.lv);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = DockStyle.Right;
			this.panel2.Location = new Point(291, 25);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(200, 291);
			this.panel2.TabIndex = 3;
			this.lv.AutoSortingEnabled = false;
			this.lv.BackColourSelected = Color.LightBlue;
			this.lv.CalcButtonCid = 0;
			this.lv.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader4
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
			this.lv.Size = new Size(200, 231);
			this.lv.TabIndex = 0;
			this.lv.Tag2 = null;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = View.Details;
			this.lv.DoubleClick += this.lv_DoubleClick;
			this.columnHeader4.Text = "Column name";
			this.columnHeader4.Width = 170;
			this.label1.Dock = DockStyle.Top;
			this.label1.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(200, 60);
			this.label1.TabIndex = 1;
			this.label1.Text = "Double-click on a column name to add to the list on the left.";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.listViewEx1);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.clockWorkFieldChooser1);
			base.Name = "ReportFunctionParameterControl_DataSyncDataMap";
			base.Size = new Size(491, 316);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0004A460 File Offset: 0x00049460
		public ReportFunctionParameterControl_DataSyncDataMap()
		{
			this.InitializeComponent();
			Control[] array = new Control[3];
			array[1] = this.clockWorkFieldChooser1;
			this.ctrls = array;
			this.listViewColumnSortings = new bool[this.listViewEx1.Columns.Count];
			for (int i = 0; i < this.listViewEx1.Columns.Count; i++)
			{
				this.listViewColumnSortings[i] = false;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0004A4E7 File Offset: 0x000494E7
		public UnivDataAdapter Da
		{
			set
			{
				this.da = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0004A4F4 File Offset: 0x000494F4
		public List<DynamicControl> AllClockWorkControls
		{
			get
			{
				if (this.allClockWorkControls == null && this.da != null)
				{
					this.allClockWorkControls = DynamicControl.LoadFromDatabase(this.da);
				}
				return this.allClockWorkControls;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0004A537 File Offset: 0x00049537
		public void Initialize(UnivDataAdapter da)
		{
			this.da = da;
			this.allClockWorkControls = DynamicControl.LoadFromDatabase(da);
			this.clockWorkFieldChooser1.SetDataSource(this.allClockWorkControls);
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0004A560 File Offset: 0x00049560
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0004A660 File Offset: 0x00049660
		public string Parameter
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.listViewEx1.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					DataMap dataMap = (DataMap)listViewItem.Tag;
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(dataMap.ClockWorkControlId.ToString());
					stringBuilder.Append("=");
					stringBuilder.Append(dataMap.ExternalColName);
				}
				return stringBuilder.ToString();
			}
			set
			{
				List<DataMap> list = new List<DataMap>();
				string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(value, true);
				this.listViewEx1.Items.Clear();
				foreach (string text in array)
				{
					int num = text.IndexOf('=');
					if (num >= 0)
					{
						string s = text.Substring(0, num);
						int cid;
						if (!int.TryParse(s, out cid))
						{
							cid = 0;
						}
						DynamicControl dynamicControl = this.AllClockWorkControls.Find((DynamicControl e) => e.ControlId == cid);
						string text2;
						if (dynamicControl != null)
						{
							text2 = dynamicControl.ControlCaption;
						}
						else
						{
							text2 = "";
						}
						ListViewItem listViewItem = new ListViewItem(text.Substring(num + 1));
						listViewItem.SubItems.Add(text2);
						listViewItem.SubItems.Add(cid.ToString());
						DataMap tag = new DataMap(listViewItem.Text, text2, cid, 0);
						listViewItem.Tag = tag;
						this.listViewEx1.Items.Add(listViewItem);
					}
				}
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0004A7A4 File Offset: 0x000497A4
		private void listViewEx1_SubItemClicked(object sender, SubItemClickEventArgs e)
		{
			Control control = this.ctrls[e.SubItem];
			if (control != null)
			{
				DataMap currentDataMap = (DataMap)e.Item.Tag;
				this.clockWorkFieldChooser1.SetCurrentDataMap(currentDataMap);
				this.listViewEx1.StartEditing(control, e.Item, e.SubItem, false);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0004A804 File Offset: 0x00049804
		private void listViewEx1_SubItemEndEditing(object sender, SubItemClickEventArgs e)
		{
			int subItem = e.SubItem;
			if (subItem == 1)
			{
				if (this.clockWorkFieldChooser1.UserCancelled)
				{
					return;
				}
				DataMap currentDataMapFilledIn = this.clockWorkFieldChooser1.GetCurrentDataMapFilledIn();
				e.Item.Tag = currentDataMapFilledIn;
				e.Item.SubItems[1].Text = string.Format("{0} [cid={1},sn={2}]", currentDataMapFilledIn.ControlCaption, currentDataMapFilledIn.ClockWorkControlId.ToString(), currentDataMapFilledIn.ClockWorkScreenNum.ToString());
			}
			this.listViewEx1_SelectedIndexChanged(this.listViewEx1, new EventArgs());
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0004A8A4 File Offset: 0x000498A4
		private void listViewEx1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listViewEx1.SelectedItems.Count > 0)
			{
				DataMap dataMap = (DataMap)this.listViewEx1.SelectedItems[0].Tag;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0004A8EC File Offset: 0x000498EC
		public void SetColumnsToChooseFrom(string sqlCode)
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

		// Token: 0x06000437 RID: 1079 RVA: 0x0004AA40 File Offset: 0x00049A40
		private void lv_DoubleClick(object sender, EventArgs e)
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				string text = this.lv.SelectedItems[0].Text;
				this.AddExternalCol(text);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0004AA8C File Offset: 0x00049A8C
		private void AddExternalCol(string selectedColName)
		{
			ListViewItem listViewItem = new ListViewItem(selectedColName);
			listViewItem.SubItems.Add("");
			listViewItem.SubItems.Add("");
			DataMap tag = new DataMap(selectedColName, "", 0, 0);
			listViewItem.Tag = tag;
			this.listViewEx1.Items.Add(listViewItem);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0004AAEC File Offset: 0x00049AEC
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			string userInput = InputBox.GetUserInput(this, "Add new report column", "Enter a new report column name", "");
			if (!string.IsNullOrEmpty(userInput))
			{
				string[] array = userInput.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					if (!string.IsNullOrEmpty(text))
					{
						this.AddExternalCol(text);
					}
				}
			}
		}

		// Token: 0x0400022B RID: 555
		private IContainer components = null;

		// Token: 0x0400022C RID: 556
		private ListViewEx listViewEx1;

		// Token: 0x0400022D RID: 557
		private ColumnHeader columnHeader1;

		// Token: 0x0400022E RID: 558
		private ColumnHeader columnHeader2;

		// Token: 0x0400022F RID: 559
		private ClockWorkFieldChooser clockWorkFieldChooser1;

		// Token: 0x04000230 RID: 560
		private ToolStrip toolStrip1;

		// Token: 0x04000231 RID: 561
		private ToolStripButton toolStripButton1;

		// Token: 0x04000232 RID: 562
		private ColumnHeader columnHeader3;

		// Token: 0x04000233 RID: 563
		private ExpandableSplitter expandableSplitter1;

		// Token: 0x04000234 RID: 564
		private Panel panel2;

		// Token: 0x04000235 RID: 565
		private ListViewEx lv;

		// Token: 0x04000236 RID: 566
		private ColumnHeader columnHeader4;

		// Token: 0x04000237 RID: 567
		private Label label1;

		// Token: 0x04000238 RID: 568
		private Control[] ctrls;

		// Token: 0x04000239 RID: 569
		private bool[] listViewColumnSortings;

		// Token: 0x0400023A RID: 570
		private List<DynamicControl> allClockWorkControls = null;

		// Token: 0x0400023B RID: 571
		private UnivDataAdapter da;
	}
}
