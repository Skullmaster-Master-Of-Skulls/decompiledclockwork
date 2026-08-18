using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using DevComponents.DotNetBar;
using EncryptionClassLibrary;
using UnivOleDb;

namespace DynamicScreens.CustomControls
{
	// Token: 0x02000019 RID: 25
	public class DynamicControlChooser : UserControl, MyDynamicControl
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000162F8 File Offset: 0x000152F8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00016330 File Offset: 0x00015330
		private void InitializeComponent()
		{
			this.tv = new TreeView();
			this.expandableSplitter1 = new ExpandableSplitter();
			this.panel1 = new Panel();
			this.txt_selected = new TextBox();
			this.label1 = new Label();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.tv.CheckBoxes = true;
			this.tv.Dock = DockStyle.Fill;
			this.tv.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.tv.Location = new Point(0, 0);
			this.tv.Name = "tv";
			this.tv.Size = new Size(214, 193);
			this.tv.TabIndex = 0;
			this.tv.AfterCheck += this.tv_AfterCheck;
			this.expandableSplitter1.BackColor2 = Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.Dock = DockStyle.Right;
			this.expandableSplitter1.ExpandableControl = this.panel1;
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
			this.expandableSplitter1.Location = new Point(214, 0);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new Size(10, 193);
			this.expandableSplitter1.TabIndex = 1;
			this.expandableSplitter1.TabStop = false;
			this.panel1.Controls.Add(this.txt_selected);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = DockStyle.Right;
			this.panel1.Location = new Point(224, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(89, 193);
			this.panel1.TabIndex = 2;
			this.txt_selected.Dock = DockStyle.Fill;
			this.txt_selected.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txt_selected.Location = new Point(0, 14);
			this.txt_selected.Multiline = true;
			this.txt_selected.Name = "txt_selected";
			this.txt_selected.ReadOnly = true;
			this.txt_selected.ScrollBars = ScrollBars.Both;
			this.txt_selected.Size = new Size(89, 179);
			this.txt_selected.TabIndex = 2;
			this.txt_selected.WordWrap = false;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Top;
			this.label1.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(55, 14);
			this.label1.TabIndex = 3;
			this.label1.Text = "Selected";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tv);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.panel1);
			base.Name = "DynamicControlChooser";
			base.Size = new Size(313, 193);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000168AE File Offset: 0x000158AE
		public DynamicControlChooser()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000168D0 File Offset: 0x000158D0
		public object ReportObject
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000168E8 File Offset: 0x000158E8
		public void Initialize(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool showDisabledForms, string defaultSelectedCids, params int[] formTypesToShow)
		{
			this.Initialize(da, tripleDES, showDisabledForms, defaultSelectedCids, false, formTypesToShow);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000168FC File Offset: 0x000158FC
		public void Initialize(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool showDisabledForms, string defaultSelectedCids, bool showAccommodations, params int[] formTypesToShow)
		{
			showAccommodations = (formTypesToShow.Length == 1 && formTypesToShow[0] == -1);
			this.da = da;
			this.tripleDES = tripleDES;
			string[] array = defaultSelectedCids.Split(new char[]
			{
				','
			});
			this.defaultCids = new List<int>();
			foreach (string text in array)
			{
				string text2 = text.Trim();
				if (text2.Length > 0)
				{
					int num;
					try
					{
						num = int.Parse(text2);
					}
					catch
					{
						num = 0;
					}
					if (num > 0)
					{
						this.defaultCids.Add(num);
					}
				}
			}
			string text3 = "";
			foreach (int num2 in formTypesToShow)
			{
				if (text3.Length > 0)
				{
					text3 += ",";
				}
				text3 += num2.ToString();
			}
			da.SelectCommand.CommandText = "SELECT s.screennum,s.description,dsc.controlid,dc.controlcaption,dc.controlcode,dc.setting4string FROM screens s LEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=s.screennum LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE ((s.screennum=4 AND @showaccommodationsform=1) OR (NOT s.screennum=4 AND s.typecode IN (SELECT orderid AS typecode FROM splitorderids(@stypes,',')))) AND (@includedisabled=1 OR s.isactive=1) AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols) ORDER BY s.description,dsc.ordernum,dc.controlcaption";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@includedisabled", showDisabledForms);
			da.SelectCommand.Parameters.Add("@stypes", text3);
			da.SelectCommand.Parameters.Add("@showaccommodationsform", showAccommodations);
			this.controlsTable = new DataTable();
			string text4;
			da.Fill(this.controlsTable, out text4);
			if (text4 != null && text4.Length > 0)
			{
				MessageBox.Show(text4);
			}
			this.ToScreen();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00016AE4 File Offset: 0x00015AE4
		private void ToScreen()
		{
			this.tv.BeginUpdate();
			this.tv.Nodes.Clear();
			int num = -1;
			TreeNode treeNode = null;
			foreach (object obj in this.controlsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = (int)dataRow["screennum"];
				if (num2 != num)
				{
					treeNode = new TreeNode(dataRow["description"].ToString());
					treeNode.Tag = num2;
					this.tv.Nodes.Add(treeNode);
					num = num2;
				}
				int num3 = (dataRow["controlid"] != DBNull.Value) ? ((int)dataRow["controlid"]) : 0;
				if (num3 > 0)
				{
					TreeNode treeNode2 = new TreeNode(dataRow["controlcaption"].ToString());
					treeNode2.Tag = num3;
					if (this.defaultCids.Contains(num3))
					{
						treeNode2.Checked = true;
					}
					treeNode.Nodes.Add(treeNode2);
				}
			}
			this.tv.EndUpdate();
			if (this.tv.Nodes.Count == 1)
			{
				this.tv.Nodes[0].ExpandAll();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00016CA8 File Offset: 0x00015CA8
		public void SetSelectedControlIdsStringCommaSeparated(string controlIds)
		{
			this.ignoreCheckedChanged = true;
			List<int> list = new List<int>();
			string[] array = controlIds.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				try
				{
					if (text.Trim().Length > 0)
					{
						list.Add(int.Parse(text));
					}
				}
				catch
				{
				}
			}
			List<TreeNode> list2 = new List<TreeNode>();
			foreach (object obj in this.tv.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				foreach (object obj2 in treeNode.Nodes)
				{
					TreeNode treeNode2 = (TreeNode)obj2;
					int item = (int)treeNode2.Tag;
					treeNode2.Checked = list.Contains(item);
					if (!list2.Contains(treeNode))
					{
						list2.Add(treeNode);
					}
				}
			}
			foreach (TreeNode treeNode in list2)
			{
				TreeNode treeNode;
				treeNode.Expand();
			}
			list2.Clear();
			this.ignoreCheckedChanged = false;
			this.RefreshSelectedSummary();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00016E94 File Offset: 0x00015E94
		public bool FilledIn
		{
			get
			{
				string text = this.ToString();
				return text.Length > 0;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00016EB8 File Offset: 0x00015EB8
		public new string ToString()
		{
			return this.GetSelectedControlIdsStringCommaSeparated();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00016ED0 File Offset: 0x00015ED0
		public void FromString(string s)
		{
			this.SetSelectedControlIdsStringCommaSeparated(s);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00016EDC File Offset: 0x00015EDC
		public List<string> GetSelectedCidCommaDescriptions()
		{
			List<string> list = new List<string>();
			foreach (object obj in this.tv.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				foreach (object obj2 in treeNode.Nodes)
				{
					TreeNode treeNode2 = (TreeNode)obj2;
					if (treeNode2.Checked)
					{
						int num = (int)treeNode2.Tag;
						string text = treeNode2.Text;
						list.Add(num.ToString() + "," + text);
					}
				}
			}
			return list;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00016FF8 File Offset: 0x00015FF8
		private void RefreshSelectedSummary()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.tv.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				foreach (object obj2 in treeNode.Nodes)
				{
					TreeNode treeNode2 = (TreeNode)obj2;
					if (treeNode2.Checked)
					{
						string text = treeNode2.Text;
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(Environment.NewLine);
						}
						stringBuilder.AppendFormat("• {0}", text);
					}
				}
			}
			this.txt_selected.Text = stringBuilder.ToString();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00017120 File Offset: 0x00016120
		public List<int> GetSelectedControlIds()
		{
			List<int> list = new List<int>();
			foreach (object obj in this.tv.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				foreach (object obj2 in treeNode.Nodes)
				{
					TreeNode treeNode2 = (TreeNode)obj2;
					if (treeNode2.Checked)
					{
						int item = (int)treeNode2.Tag;
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00017220 File Offset: 0x00016220
		public string GetSelectedControlIdsStringCommaSeparated()
		{
			List<int> selectedControlIds = this.GetSelectedControlIds();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int num in selectedControlIds)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(num.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000172B8 File Offset: 0x000162B8
		private void tv_AfterCheck(object sender, TreeViewEventArgs e)
		{
			bool @checked = e.Node.Checked;
			foreach (object obj in e.Node.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				treeNode.Checked = @checked;
			}
			e.Node.Expand();
			if (!this.ignoreCheckedChanged)
			{
				this.RefreshSelectedSummary();
			}
		}

		// Token: 0x0400012C RID: 300
		private IContainer components = null;

		// Token: 0x0400012D RID: 301
		private TreeView tv;

		// Token: 0x0400012E RID: 302
		private ExpandableSplitter expandableSplitter1;

		// Token: 0x0400012F RID: 303
		private Panel panel1;

		// Token: 0x04000130 RID: 304
		private TextBox txt_selected;

		// Token: 0x04000131 RID: 305
		private Label label1;

		// Token: 0x04000132 RID: 306
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000133 RID: 307
		private UnivDataAdapter da;

		// Token: 0x04000134 RID: 308
		private DataTable controlsTable;

		// Token: 0x04000135 RID: 309
		private List<int> defaultCids;

		// Token: 0x04000136 RID: 310
		private bool ignoreCheckedChanged = false;
	}
}
