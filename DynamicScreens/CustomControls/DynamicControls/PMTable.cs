using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.WinForms.CoreComponents.Controls.Grid;
using UnivOleDb;

namespace DynamicScreens.CustomControls.DynamicControls
{
	// Token: 0x02000058 RID: 88
	public class PMTable : UserControl, MyDynamicControl
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0003F151 File Offset: 0x0003E151
		public PMTable()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0003F178 File Offset: 0x0003E178
		public bool FilledIn
		{
			get
			{
				return this.ctrlGrid1.RowCount > 0;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0003F198 File Offset: 0x0003E198
		public void FromString(string s)
		{
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0003F19C File Offset: 0x0003E19C
		public object ReportObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700014E RID: 334
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0003F1AF File Offset: 0x0003E1AF
		public int WhoAmIPersonID
		{
			set
			{
				this.whoAmIPersonID = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0003F1BC File Offset: 0x0003E1BC
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0003F1D4 File Offset: 0x0003E1D4
		public string Cids
		{
			get
			{
				return this.cids;
			}
			set
			{
				this.cids = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0003F1DE File Offset: 0x0003E1DE
		public UnivDataAdapter Da
		{
			set
			{
				this.da = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0003F1E8 File Offset: 0x0003E1E8
		public TripleDESEncryptionClass TripleDES
		{
			set
			{
				this.tripleDES = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0003F1F4 File Offset: 0x0003E1F4
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0003F20C File Offset: 0x0003E20C
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0003F238 File Offset: 0x0003E238
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0003F255 File Offset: 0x0003E255
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0003F268 File Offset: 0x0003E268
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0003F280 File Offset: 0x0003E280
		public int FormNumber
		{
			get
			{
				return this.formNumber;
			}
			set
			{
				this.formNumber = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0003F28C File Offset: 0x0003E28C
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0003F2A4 File Offset: 0x0003E2A4
		public ArrayList EventHandlers
		{
			get
			{
				return this.eventHandlers;
			}
			set
			{
				this.eventHandlers = value;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0003F2AE File Offset: 0x0003E2AE
		private void btn_removeSelected_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0003F2B4 File Offset: 0x0003E2B4
		public void RefreshList()
		{
			if (this.da != null)
			{
				string str = "SELECT 0 AS usertype,'Primary Client' AS title UNION SELECT 1 AS usertype,'Client' AS title UNION SELECT 2 AS usertype,'Respondent' AS title";
				string text = "SELECT DISTINCT ipc.personid AS infopcid,ipc.student_no AS CaseNumber,ipc.dateentered\r\n                            ,ipc.whoentered,p.firstname,p.lastname,p.student_no,ll.lookuptext AS status\r\n                            ,x.title AS ClientRespondentType\r\n                            ,ipc.title\r\n                            @selects\r\n                FROM    infopcpeople ipcp LEFT JOIN infopc ipc ON ipc.personid=ipcp.infopcid\r\n                        LEFT JOIN people p ON p.personid=ipc.whoentered \r\n                                LEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=@screennum \r\n                                    AND dsc.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcaption LIKE '%status%' AND controlcode=3) \r\n                                LEFT JOIN pcdata2 pcd ON pcd.infopcid=ipc.personid AND pcd.controlid=dsc.controlid \r\n                                LEFT JOIN lookuplists ll ON ll.lookuplistid=pcd.valint \r\n                                LEFT JOIN (" + str + ") x ON x.usertype=ipcp.usertype \r\n                                @joins\r\n                WHERE ipcp.personid=@pid AND ipc.isactive=1 \r\n                        AND (ipc.personid IN (SELECT orderid AS personid FROM splitorderids(@infopcids,',')) \r\n                                OR ipc.personid IN (SELECT infopcid AS personid FROM infopcpeople WHERE personid=@pid))\r\n                        AND p.isactive=1 \r\n                ORDER BY ipc.dateentered DESC";
				string text2 = "";
				string text3 = "";
				string[] array = this.cids.Split(new char[]
				{
					','
				});
				int num = 0;
				foreach (string s in array)
				{
					int num2;
					if (int.TryParse(s, out num2))
					{
						string text4 = "custom_" + num.ToString() + "_" + num2.ToString();
						string text5 = "pc" + num.ToString();
						string text6 = text3;
						text3 = string.Concat(new string[]
						{
							text6,
							",",
							text5,
							".controlcaption AS ",
							text4
						});
						text6 = text2;
						text2 = string.Concat(new string[]
						{
							text6,
							" LEFT JOIN pcdata2 ",
							text5,
							" ON ",
							text5,
							".infopcid=ipc.personid AND ",
							text5,
							".controlid=",
							num2.ToString()
						});
						num++;
					}
				}
				text = text.Replace("@joins", text2);
				text = text.Replace("@selects", text3);
				this.da.SelectCommand.CommandText = text;
				this.da.SelectCommand.Parameters.Clear();
				this.da.SelectCommand.Parameters.Add("@pid", this.pid);
				this.da.SelectCommand.Parameters.Add("@infopcids", this.GetInfoPcIdsFromCases());
				this.da.SelectCommand.Parameters.Add("@screennum", this.formNumber);
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				dataTable = this.tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no",
					"casenumber"
				});
				if (dataTable.Columns.Count > 10)
				{
					dataTable.Columns.Add("Information");
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string text7 = "";
						for (int j = 10; j < dataTable.Columns.Count - 1; j++)
						{
							string text8 = dataRow[j].ToString().Trim();
							if (text8.Length > 0)
							{
								if (text7.Length > 0)
								{
									text7 += ", ";
								}
								text7 += text8;
							}
						}
						dataRow["Information"] = text7;
					}
					DataColumn[] array3 = new DataColumn[dataTable.Columns.Count - 11];
					for (int j = 10; j < dataTable.Columns.Count - 1; j++)
					{
						array3[j - 10] = dataTable.Columns[j];
					}
					for (int j = 0; j < array3.Length; j++)
					{
						dataTable.Columns.Remove(array3[j]);
					}
				}
				BindingSource bindingSource = new BindingSource();
				bindingSource.DataSource = dataTable;
				bindingSource.Sort = "dateentered";
				this.ctrlGrid1.DataSource = bindingSource;
				string[] array4 = new string[]
				{
					"infopcid",
					"whoentered",
					"student_no"
				};
				foreach (string text9 in array4)
				{
					this.ctrlGrid1.Columns.SetVisible(text9, false);
				}
				this.ctrlGrid1.BestFitColumns();
			}
			this.btn_addNew.Enabled = (this.pid > 0);
			if (this.pid > 0)
			{
				this.btn_addNew.Text = "&Add new";
			}
			else
			{
				this.btn_addNew.Text = "&Add new (disabled - save the new client info first)";
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0003F7C8 File Offset: 0x0003E7C8
		private string GetInfoPcIdsFromCases()
		{
			string result;
			if (this.cases == null || this.cases.Count < 1)
			{
				result = "";
			}
			else
			{
				string text = "";
				foreach (CaseSingle caseSingle in this.cases)
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += caseSingle.InfoPcPid.ToString();
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0003F87C File Offset: 0x0003E87C
		private void grid_DoubleClick(object sender, EventArgs e)
		{
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0003F880 File Offset: 0x0003E880
		public List<CaseSingle> Cases
		{
			get
			{
				return this.cases;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0003F898 File Offset: 0x0003E898
		private void btn_addNew_Click(object sender, EventArgs e)
		{
			CaseDetail caseDetail = new CaseDetail(this.da, this.tripleDES, this.formNumber, this.pid, this.eventHandlers, 0, this.whoAmIPersonID, null);
			DialogResult dialogResult = caseDetail.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				CaseSingle item = new CaseSingle(caseDetail.InfoPcPid, caseDetail.CasePeople);
				this.cases.Add(item);
				this.RefreshList();
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0003F910 File Offset: 0x0003E910
		private void ctrlGrid1_DoubleClick(object sender, EventArgs e)
		{
			if (this.ctrlGrid1.SelectedRows.Count == 1)
			{
				DataRowView dataRowView = this.ctrlGrid1.SelectedRows.DataBoundItem<DataRowView>(0);
				int infoPcPid = (int)dataRowView["infopcid"];
				CaseSingle caseSingle = CaseSingle.FindCaseSingle(this.cases, infoPcPid);
				CaseDetail caseDetail = new CaseDetail(this.da, this.tripleDES, this.formNumber, this.pid, this.eventHandlers, infoPcPid, this.whoAmIPersonID, caseSingle);
				DialogResult dialogResult = caseDetail.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					caseSingle = caseDetail.CaseSingle;
					if (caseSingle == null)
					{
						caseSingle = new CaseSingle(caseDetail.InfoPcPid, caseDetail.CasePeople);
						this.cases.Add(caseSingle);
					}
					this.RefreshList();
				}
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0003F9F0 File Offset: 0x0003E9F0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0003FA28 File Offset: 0x0003EA28
		private void InitializeComponent()
		{
			this.lbl = new Label();
			this.toolStrip1 = new ToolStrip();
			this.btn_addNew = new ToolStripButton();
			this.ctrlGrid1 = new CtrlGrid();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.lbl.AutoSize = true;
			this.lbl.Dock = DockStyle.Top;
			this.lbl.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl.Location = new Point(0, 0);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(45, 16);
			this.lbl.TabIndex = 49;
			this.lbl.Text = "Cases";
			this.toolStrip1.Dock = DockStyle.Bottom;
			this.toolStrip1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_addNew
			});
			this.toolStrip1.Location = new Point(0, 148);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(360, 25);
			this.toolStrip1.TabIndex = 51;
			this.toolStrip1.TabStop = true;
			this.btn_addNew.Image = Resources.star_yellow_new;
			this.btn_addNew.ImageTransparentColor = Color.Magenta;
			this.btn_addNew.Name = "btn_addNew";
			this.btn_addNew.Size = new Size(72, 22);
			this.btn_addNew.Text = "&Add new";
			this.btn_addNew.Click += this.btn_addNew_Click;
			this.ctrlGrid1.AutoGenerateColumns = true;
			this.ctrlGrid1.DataSource = null;
			this.ctrlGrid1.Dock = DockStyle.Fill;
			this.ctrlGrid1.DontShowFilteringRow = false;
			this.ctrlGrid1.EnableAlternatingRowColor = true;
			this.ctrlGrid1.EnableFiltering = true;
			this.ctrlGrid1.EnableGrouping = true;
			this.ctrlGrid1.Location = new Point(0, 16);
			this.ctrlGrid1.Margin = new Padding(3, 4, 3, 4);
			this.ctrlGrid1.MultiSelect = false;
			this.ctrlGrid1.Name = "ctrlGrid1";
			this.ctrlGrid1.Size = new Size(360, 132);
			this.ctrlGrid1.TabIndex = 52;
			this.ctrlGrid1.ThemeName = "Office2010Silver";
			this.ctrlGrid1.DoubleClick += this.ctrlGrid1_DoubleClick;
			base.AutoScaleDimensions = new SizeF(6f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.ctrlGrid1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl);
			this.Font = new Font("Arial Narrow", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "PMTable";
			base.Size = new Size(360, 173);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000357 RID: 855
		private string cids;

		// Token: 0x04000358 RID: 856
		private UnivDataAdapter da;

		// Token: 0x04000359 RID: 857
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400035A RID: 858
		private int pid;

		// Token: 0x0400035B RID: 859
		private int formNumber;

		// Token: 0x0400035C RID: 860
		private int whoAmIPersonID;

		// Token: 0x0400035D RID: 861
		private ArrayList eventHandlers;

		// Token: 0x0400035E RID: 862
		private List<CaseSingle> cases = new List<CaseSingle>();

		// Token: 0x0400035F RID: 863
		private IContainer components = null;

		// Token: 0x04000360 RID: 864
		private Label lbl;

		// Token: 0x04000361 RID: 865
		private ToolStrip toolStrip1;

		// Token: 0x04000362 RID: 866
		private ToolStripButton btn_addNew;

		// Token: 0x04000363 RID: 867
		private CtrlGrid ctrlGrid1;
	}
}
