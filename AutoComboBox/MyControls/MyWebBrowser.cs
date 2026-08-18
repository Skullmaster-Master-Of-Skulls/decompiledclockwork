using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoComboBox.MyControls.MultiLineTextBox;
using AutoComboBox.Properties;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000D6 RID: 214
	public class MyWebBrowser : UserControl, MyDynamicControl
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00040374 File Offset: 0x0003F374
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0004038C File Offset: 0x0003F38C
		public MyPanel MyPanel
		{
			get
			{
				return this.myPanel;
			}
			set
			{
				if (this.myPanel != null)
				{
					this.myPanel.OnDataRenderCompleted -= this.myPanel_OnDataRenderCompleted;
				}
				this.myPanel = value;
				if (this.myPanel != null)
				{
					this.myPanel.OnDataRenderCompleted += this.myPanel_OnDataRenderCompleted;
				}
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000403EF File Offset: 0x0003F3EF
		public void Print()
		{
			this.webBrowser1.ShowPrintDialog();
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00040400 File Offset: 0x0003F400
		public MyWebBrowser()
		{
			this.AllowNavigateExternalLink = false;
			this.InitializeComponent();
			this.webBrowser1.GotFocus += this.webBrowser1_GotFocus;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00040460 File Offset: 0x0003F460
		private void webBrowser1_GotFocus(object sender, EventArgs e)
		{
			if (this.webBrowser1.Document != null && this.webBrowser1.Document.Body != null)
			{
				this.webBrowser1.Document.Body.Focus();
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000404BC File Offset: 0x0003F4BC
		bool MyDynamicControl.FilledIn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000404D0 File Offset: 0x0003F4D0
		string MyDynamicControl.ToString()
		{
			return "";
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000404E7 File Offset: 0x0003F4E7
		void MyDynamicControl.FromString(string s)
		{
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x000404EC File Offset: 0x0003F4EC
		object MyDynamicControl.ReportObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000404FF File Offset: 0x0003F4FF
		void MyDynamicControl.Refresh()
		{
			this.RefreshSummary();
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000840 RID: 2112 RVA: 0x0004050C File Offset: 0x0003F50C
		// (remove) Token: 0x06000841 RID: 2113 RVA: 0x00040548 File Offset: 0x0003F548
		public event WebBrowserDocumentCompletedEventHandler WebBrowser_DocumentCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000842 RID: 2114 RVA: 0x00040584 File Offset: 0x0003F584
		// (remove) Token: 0x06000843 RID: 2115 RVA: 0x000405C0 File Offset: 0x0003F5C0
		public event CancelEventHandler WebBrowser_Validating;

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000405FC File Offset: 0x0003F5FC
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x00040619 File Offset: 0x0003F619
		public string Title
		{
			get
			{
				return this.lbl_formSummary.Text;
			}
			set
			{
				this.lbl_formSummary.Text = value;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00040629 File Offset: 0x0003F629
		public void RemoveNavigatingHandler()
		{
			this.webBrowser1.Navigating -= this.webBrowser1_Navigating;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00040644 File Offset: 0x0003F644
		public string NavigateTo(string url)
		{
			string result;
			try
			{
				this.webBrowser1.Navigate(url);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00040684 File Offset: 0x0003F684
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0004069B File Offset: 0x0003F69B
		public virtual bool AllowNavigateExternalLink { get; set; }

		// Token: 0x0600084A RID: 2122 RVA: 0x000406A4 File Offset: 0x0003F6A4
		private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			this.FireNavigating(sender, e);
			if (!this.AllowNavigateExternalLink)
			{
				if (e.Url.ToString() != "about:blank")
				{
					e.Cancel = true;
				}
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000406F0 File Offset: 0x0003F6F0
		private void FireNavigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (this.Navigating != null)
			{
				this.Navigating(sender, e);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0004071B File Offset: 0x0003F71B
		public void HideRefreshButton()
		{
			this.btn_refresh2.Visible = false;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0004072B File Offset: 0x0003F72B
		public void HideTitle()
		{
			this.lbl_formSummary.Visible = false;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0004073B File Offset: 0x0003F73B
		public void HideEverythingButBrowser()
		{
			this.toolStrip1.Visible = false;
			this.lbl_formSummary.Visible = false;
			this.btn_refresh2.Visible = false;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00040768 File Offset: 0x0003F768
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00040780 File Offset: 0x0003F780
		public string Css
		{
			get
			{
				return this.css;
			}
			set
			{
				this.css = value;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000851 RID: 2129 RVA: 0x0004078C File Offset: 0x0003F78C
		// (remove) Token: 0x06000852 RID: 2130 RVA: 0x000407C8 File Offset: 0x0003F7C8
		public event WebBrowserNavigatingEventHandler Navigating;

		// Token: 0x06000853 RID: 2131 RVA: 0x00040804 File Offset: 0x0003F804
		public void ShowHtml(string html)
		{
			this.webBrowser1.Navigate("about:blank");
			if (this.webBrowser1.Document != null)
			{
				this.webBrowser1.Document.Write(string.Empty);
			}
			this.webBrowser1.DocumentText = string.Concat(new string[]
			{
				"<html><head><style TYPE=\"text/css\"> <!-- ",
				this.css,
				" --> </style></head><body>",
				html,
				"</body></html>"
			});
			this.webBrowser1.Document.ExecCommand("SelectAll", false, null);
			this.webBrowser1.Document.ExecCommand("FontName", false, "Arial");
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000408C5 File Offset: 0x0003F8C5
		private void btn_refresh_Click(object sender, EventArgs e)
		{
			this.RefreshSummary();
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000408D0 File Offset: 0x0003F8D0
		public string RefreshSummary()
		{
			string result;
			if (this.myPanel != null)
			{
				List<HtmlGroup> list = new List<HtmlGroup>();
				int num = this.AddItemsToSummary(ref list, this.myPanel);
				StringBuilder stringBuilder = new StringBuilder();
				if (this.myPanel.FirstName != null && this.myPanel.LastName != null && this.myPanel.Student_no != null)
				{
					stringBuilder.AppendFormat("<h2>{0} {1} ({2})</h2>", this.myPanel.FirstName, this.myPanel.LastName, this.myPanel.Student_no);
				}
				foreach (HtmlGroup htmlGroup in list)
				{
					StringBuilder items = htmlGroup.Items;
					if (items.Length > 0)
					{
						if (!htmlGroup.Title.Equals("-"))
						{
							stringBuilder.Append(string.Format("<h2>{0}</h2>", htmlGroup.Title));
						}
						stringBuilder.Append(items);
					}
				}
				string text = stringBuilder.ToString();
				this.ShowHtml(text);
				if (!this.Focused)
				{
					base.Focus();
				}
				result = text;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00040A3C File Offset: 0x0003FA3C
		private int AddItemsToSummary(ref List<HtmlGroup> htmlGroups, Control parent)
		{
			int num = 0;
			int num2 = 0;
			try
			{
				if (parent == this)
				{
					return num;
				}
				if (parent is Panel)
				{
					Panel panel = (Panel)parent;
					if (panel.Controls.Count > 0 && panel.Controls[0] is Label)
					{
						Label label = (Label)panel.Controls[0];
						htmlGroups.Add(new HtmlGroup(label.Text));
					}
				}
				if (htmlGroups.Count < 1)
				{
					htmlGroups.Add(new HtmlGroup("-"));
				}
				HtmlGroup htmlGroup = htmlGroups[htmlGroups.Count - 1];
				string value;
				if (parent.Tag != null && parent.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)parent.Tag;
					if (dataRow.Table.Columns.Contains("controlcaption") && dataRow.Table.Columns.Contains("controlcode") && dataRow["controlcode"] != DBNull.Value)
					{
						string text = dataRow["controlcaption"].ToString();
						int num3 = text.IndexOf("~~");
						if (num3 == 0)
						{
							text = "";
						}
						else if (num3 > 0)
						{
							text = text.Substring(0, num3);
						}
						int num4 = (int)dataRow["controlcode"];
						string text2 = "";
						num2 = num4;
						int num5 = num4;
						if (num5 <= 500)
						{
							if (num5 <= 20)
							{
								switch (num5)
								{
								case 1:
								case 11:
									goto IL_2AA;
								case 2:
								case 12:
									goto IL_2DD;
								case 3:
									goto IL_308;
								case 4:
									if (parent is RadioButton && ((RadioButton)parent).Checked)
									{
										text2 = "Selected";
									}
									goto IL_683;
								case 5:
								case 7:
								case 8:
								case 9:
								case 13:
									goto IL_67F;
								case 6:
									if (parent is MyDateTimePicker)
									{
										MyDateTimePicker myDateTimePicker = (MyDateTimePicker)parent;
										if (myDateTimePicker.Value != DateTime.MinValue)
										{
											text2 = myDateTimePicker.Value.ToString("MMMM d, yyyy");
										}
									}
									goto IL_683;
								case 10:
									break;
								case 14:
									if (parent is MyRadioGroupPrimary)
									{
										MyRadioGroupPrimary myRadioGroupPrimary = (MyRadioGroupPrimary)parent;
										text2 = myRadioGroupPrimary.SelectedText;
									}
									else if (parent is MyRadioGroup)
									{
										MyRadioGroup myRadioGroup = (MyRadioGroup)parent;
										text2 = myRadioGroup.SelectedText;
									}
									goto IL_683;
								default:
									if (num5 != 20)
									{
										goto IL_67F;
									}
									break;
								}
								if (parent is ListViewEx)
								{
									ListViewEx listViewEx = (ListViewEx)parent;
									if (listViewEx.Items.Count > 0)
									{
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append("<table cellpadding='2' cellspacing='2'><tr>");
										foreach (object obj in listViewEx.Columns)
										{
											ColumnHeader columnHeader = (ColumnHeader)obj;
											stringBuilder.Append(string.Format("<td><b>{0}</b></td>", columnHeader.Text));
										}
										stringBuilder.Append("</tr>");
										foreach (object obj2 in listViewEx.Items)
										{
											ListViewItem listViewItem = (ListViewItem)obj2;
											stringBuilder.Append("<tr>");
											for (int i = 0; i < listViewEx.Columns.Count - 1; i++)
											{
												stringBuilder.Append(string.Format("<td>{0}</td>", listViewItem.SubItems[i].Text));
											}
											stringBuilder.Append("</tr>");
										}
										stringBuilder.Append("</table>");
										text2 = stringBuilder.ToString();
									}
								}
								goto IL_683;
							}
							if (num5 == 100)
							{
								goto IL_308;
							}
							switch (num5)
							{
							case 300:
								break;
							case 301:
								goto IL_2DD;
							default:
								if (num5 != 500)
								{
									goto IL_67F;
								}
								goto IL_5F2;
							}
							IL_2AA:
							if (parent is TextBox)
							{
								text2 = ((TextBox)parent).Text.Replace(Environment.NewLine, "<br />");
							}
							goto IL_683;
							IL_2DD:
							if (parent is CheckBox && ((CheckBox)parent).Checked)
							{
								text2 = "Checked";
							}
							goto IL_683;
							IL_308:
							if (parent is AutoComboBox)
							{
								AutoComboBox autoComboBox = (AutoComboBox)parent;
								DataRow dataRow2 = autoComboBox.SelectedDataRow();
								if (dataRow2 != null)
								{
									text2 = ((dataRow2[autoComboBox.DisplayMember] == DBNull.Value) ? "" : ((string)dataRow2[autoComboBox.DisplayMember]));
								}
								if (string.IsNullOrEmpty(text2.Trim()))
								{
									text2 = autoComboBox.Text;
								}
							}
							goto IL_683;
						}
						if (num5 <= 520)
						{
							if (num5 != 510 && num5 != 520)
							{
								goto IL_67F;
							}
						}
						else
						{
							if (num5 == 600)
							{
								if (parent is MyRichText)
								{
									MyRichText myRichText = (MyRichText)parent;
									text2 = myRichText.PlainText;
								}
								goto IL_683;
							}
							if (num5 == 620)
							{
								if (parent is MyMultilineTextBoxWithEditingControls)
								{
									MyMultilineTextBoxWithEditingControls myMultilineTextBoxWithEditingControls = (MyMultilineTextBoxWithEditingControls)parent;
									text2 = myMultilineTextBoxWithEditingControls.TextBox.Text;
								}
								goto IL_683;
							}
							switch (num5)
							{
							case 700:
							case 701:
							case 702:
							case 703:
							{
								AccommodationControl2 accommodationControl = (AccommodationControl2)parent;
								text2 = accommodationControl.GetDataWithValueTextAndSummaryHtml();
								goto IL_683;
							}
							default:
								goto IL_67F;
							}
						}
						IL_5F2:
						if (parent is MyMultiCheckbox)
						{
							MyMultiCheckbox myMultiCheckbox = (MyMultiCheckbox)parent;
							text2 = myMultiCheckbox.ToStringMailMerge();
						}
						IL_67F:
						IL_683:
						text2 = text2.Trim();
						if (!string.IsNullOrEmpty(text2))
						{
							num++;
							value = string.Format("<b>{0}</b>: {1}", text, text2);
						}
						else
						{
							value = null;
						}
					}
					else
					{
						value = null;
					}
				}
				else if (parent is MyDynamicControl)
				{
					MyDynamicControl myDynamicControl = (MyDynamicControl)parent;
					if (parent is CheckBox)
					{
						CheckBox checkBox = (CheckBox)parent;
						if (checkBox.Checked)
						{
							if (parent.Parent != null && parent.Parent is MyRadioGroupPrimaryCheckboxMultiple)
							{
								value = string.Format("<b>{0}</b>: {1}", parent.Text, "Secondary");
							}
							else
							{
								value = string.Format("<b>{0}</b>: {1}", parent.Text, "Checked");
							}
						}
						else
						{
							value = null;
						}
					}
					else if (parent is MyRadioButton)
					{
						value = null;
					}
					else
					{
						value = string.Format("<b>{0}</b>: {1}", parent.Text, myDynamicControl.ToString());
					}
				}
				else
				{
					value = null;
				}
				if (!string.IsNullOrEmpty(value))
				{
					htmlGroup.Items.Append(value);
					htmlGroup.Items.Append("<br />");
				}
				foreach (object obj3 in parent.Controls)
				{
					Control parent2 = (Control)obj3;
					num += this.AddItemsToSummary(ref htmlGroups, parent2);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(num2.ToString() + ": " + ex.ToString());
			}
			return num;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000412F8 File Offset: 0x000402F8
		private void myPanel_OnDataRenderCompleted(object sender, EventArgs e, int personId)
		{
			this.RefreshSummary();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00041304 File Offset: 0x00040304
		private void MyWebBrowser_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				if (e.KeyCode == Keys.P)
				{
					this.webBrowser1.ShowPrintDialog();
				}
				else if (e.KeyCode == Keys.Add)
				{
					this.ZoomIn();
				}
				else if (e.KeyCode == Keys.Subtract)
				{
					this.ZoomOut();
				}
			}
			else if (e.KeyCode == Keys.F5 && this.btn_refresh2.Visible)
			{
				this.RefreshSummary();
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000413A4 File Offset: 0x000403A4
		public WebBrowser Browser
		{
			get
			{
				return this.webBrowser1;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000413BC File Offset: 0x000403BC
		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (this.fontSizeEm != 1.0)
			{
				this.webBrowser1.Document.Body.Style = string.Format("font-size:{0}em;", this.fontSizeEm.ToString());
			}
			if (this.WebBrowser_DocumentCompleted != null)
			{
				this.WebBrowser_DocumentCompleted(sender, e);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0004142C File Offset: 0x0004042C
		public HtmlDocument Document
		{
			get
			{
				return this.webBrowser1.Document;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00041449 File Offset: 0x00040449
		private void btn_fontSizeDown_Click(object sender, EventArgs e)
		{
			this.ZoomOut();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00041454 File Offset: 0x00040454
		private void ZoomOut()
		{
			this.fontSizeEm -= 0.1;
			if (this.fontSizeEm < 0.1)
			{
				this.fontSizeEm = 0.1;
			}
			this.webBrowser1.Refresh();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000414AB File Offset: 0x000404AB
		private void btn_fontSizeUp_Click(object sender, EventArgs e)
		{
			this.ZoomIn();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000414B5 File Offset: 0x000404B5
		private void ZoomIn()
		{
			this.fontSizeEm += 0.1;
			this.webBrowser1.Refresh();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000414DA File Offset: 0x000404DA
		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowPrintDialog();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000414E9 File Offset: 0x000404E9
		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowPrintPreviewDialog();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000414F8 File Offset: 0x000404F8
		private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowPageSetupDialog();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00041507 File Offset: 0x00040507
		private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0004150C File Offset: 0x0004050C
		private void webBrowser1_Validating(object sender, CancelEventArgs e)
		{
			if (this.WebBrowser_Validating != null)
			{
				this.WebBrowser_Validating(sender, e);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00041538 File Offset: 0x00040538
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				if (this.myPanel != null)
				{
					this.myPanel.OnDataRenderCompleted -= this.myPanel_OnDataRenderCompleted;
					this.myPanel.Dispose();
					this.myPanel = null;
				}
				if (this.webBrowser1 != null)
				{
					this.webBrowser1.GotFocus -= this.webBrowser1_GotFocus;
				}
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000415D0 File Offset: 0x000405D0
		private void InitializeComponent()
		{
			this.webBrowser1 = new WebBrowser();
			this.lbl_formSummary = new Label();
			this.toolStrip1 = new ToolStrip();
			this.btn_fontSizeUp = new ToolStripButton();
			this.btn_fontSizeDown = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripDropDownButton1 = new ToolStripDropDownButton();
			this.printToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.printPreviewToolStripMenuItem = new ToolStripMenuItem();
			this.pageSetupToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.btn_refresh2 = new ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.webBrowser1.Dock = DockStyle.Fill;
			this.webBrowser1.Location = new Point(0, 41);
			this.webBrowser1.MinimumSize = new Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new Size(390, 245);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.Validating += this.webBrowser1_Validating;
			this.webBrowser1.Navigating += this.webBrowser1_Navigating;
			this.webBrowser1.PreviewKeyDown += this.webBrowser1_PreviewKeyDown;
			this.webBrowser1.DocumentCompleted += this.webBrowser1_DocumentCompleted;
			this.lbl_formSummary.AutoSize = true;
			this.lbl_formSummary.Dock = DockStyle.Top;
			this.lbl_formSummary.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl_formSummary.Location = new Point(0, 0);
			this.lbl_formSummary.Name = "lbl_formSummary";
			this.lbl_formSummary.Size = new Size(103, 16);
			this.lbl_formSummary.TabIndex = 2;
			this.lbl_formSummary.Text = "Form summary";
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_fontSizeUp,
				this.btn_fontSizeDown,
				this.toolStripSeparator1,
				this.toolStripDropDownButton1,
				this.toolStripSeparator2,
				this.btn_refresh2
			});
			this.toolStrip1.Location = new Point(0, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(390, 25);
			this.toolStrip1.TabIndex = 3;
			this.btn_fontSizeUp.AccessibleDescription = "Increase font size";
			this.btn_fontSizeUp.AccessibleName = "Increase font size";
			this.btn_fontSizeUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_fontSizeUp.Image = Resources.nav_up_blue;
			this.btn_fontSizeUp.ImageTransparentColor = Color.Magenta;
			this.btn_fontSizeUp.Name = "btn_fontSizeUp";
			this.btn_fontSizeUp.Size = new Size(23, 22);
			this.btn_fontSizeUp.Text = "Increase font size";
			this.btn_fontSizeUp.Click += this.btn_fontSizeUp_Click;
			this.btn_fontSizeDown.AccessibleDescription = "Decrease font size";
			this.btn_fontSizeDown.AccessibleName = "Decrease font size";
			this.btn_fontSizeDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_fontSizeDown.Image = Resources.nav_down_blue;
			this.btn_fontSizeDown.ImageTransparentColor = Color.Magenta;
			this.btn_fontSizeDown.Name = "btn_fontSizeDown";
			this.btn_fontSizeDown.Size = new Size(23, 22);
			this.btn_fontSizeDown.Text = "Decrease font size";
			this.btn_fontSizeDown.Click += this.btn_fontSizeDown_Click;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.printToolStripMenuItem,
				this.toolStripMenuItem1,
				this.printPreviewToolStripMenuItem,
				this.pageSetupToolStripMenuItem
			});
			this.toolStripDropDownButton1.Image = Resources.printer;
			this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new Size(61, 22);
			this.toolStripDropDownButton1.Text = "&Print";
			this.printToolStripMenuItem.Image = Resources.printer;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.Size = new Size(143, 22);
			this.printToolStripMenuItem.Text = "&Print (ctrl+p)";
			this.printToolStripMenuItem.Click += this.printToolStripMenuItem_Click;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(140, 6);
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new Size(143, 22);
			this.printPreviewToolStripMenuItem.Text = "Print pre&view";
			this.printPreviewToolStripMenuItem.Click += this.printPreviewToolStripMenuItem_Click;
			this.pageSetupToolStripMenuItem.Image = Resources.printer_view;
			this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
			this.pageSetupToolStripMenuItem.Size = new Size(143, 22);
			this.pageSetupToolStripMenuItem.Text = "Page setu&p";
			this.pageSetupToolStripMenuItem.Click += this.pageSetupToolStripMenuItem_Click;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.btn_refresh2.Image = Resources.refresh;
			this.btn_refresh2.ImageTransparentColor = Color.Magenta;
			this.btn_refresh2.Name = "btn_refresh2";
			this.btn_refresh2.Size = new Size(89, 22);
			this.btn_refresh2.Text = "Refresh (F5)";
			this.btn_refresh2.Click += this.btn_refresh_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.webBrowser1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl_formSummary);
			base.Name = "MyWebBrowser";
			base.Size = new Size(390, 286);
			base.KeyDown += this.MyWebBrowser_KeyDown;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400061B RID: 1563
		private const string html1 = "<html><head><style TYPE=\"text/css\"> <!-- ";

		// Token: 0x0400061C RID: 1564
		private const string html2 = " --> </style></head><body>";

		// Token: 0x0400061D RID: 1565
		private const string html3 = "</body></html>";

		// Token: 0x0400061E RID: 1566
		private MyPanel myPanel;

		// Token: 0x04000621 RID: 1569
		private string css = "body { font-family: Arial, Palatino, Zapf Calligraphic, Georgia, Times New Roman, Times, Serif; font-size: .9em;  } h2 { border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: orange; font-size: 1.1em; margin-bottom: 2px; } h1 { border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: orange; font-size: 1.4em; margin-bottom: 2px; }";

		// Token: 0x04000623 RID: 1571
		private double fontSizeEm = 1.0;

		// Token: 0x04000624 RID: 1572
		private IContainer components = null;

		// Token: 0x04000625 RID: 1573
		private WebBrowser webBrowser1;

		// Token: 0x04000626 RID: 1574
		private Label lbl_formSummary;

		// Token: 0x04000627 RID: 1575
		private ToolStrip toolStrip1;

		// Token: 0x04000628 RID: 1576
		private ToolStripButton btn_fontSizeUp;

		// Token: 0x04000629 RID: 1577
		private ToolStripButton btn_fontSizeDown;

		// Token: 0x0400062A RID: 1578
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400062B RID: 1579
		private ToolStripSeparator toolStripSeparator2;

		// Token: 0x0400062C RID: 1580
		private ToolStripButton btn_refresh2;

		// Token: 0x0400062D RID: 1581
		private ToolStripDropDownButton toolStripDropDownButton1;

		// Token: 0x0400062E RID: 1582
		private ToolStripMenuItem printToolStripMenuItem;

		// Token: 0x0400062F RID: 1583
		private ToolStripSeparator toolStripMenuItem1;

		// Token: 0x04000630 RID: 1584
		private ToolStripMenuItem printPreviewToolStripMenuItem;

		// Token: 0x04000631 RID: 1585
		private ToolStripMenuItem pageSetupToolStripMenuItem;
	}
}
