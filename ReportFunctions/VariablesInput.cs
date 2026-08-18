using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AutoComboBox;
using AutoComboBox.MyControls;
using ClockWorkAPI;
using DevComponents.DotNetBar;
using DynamicScreens;
using EncryptionClassLibrary;
using ReportFunctions.Properties;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000044 RID: 68
	public partial class VariablesInput : Form
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00044C64 File Offset: 0x00043C64
		public VariablesInput(int SearchInfoID, DataTable VariablesTable, UnivDataAdapter Da, DataSet ComboBoxData, DataSet LookupTablesForControls, ref ArrayList Variables, DataTable Sessions, object[] YearStartEnd, string SearchTitle, DataTable DynamicScreenNonDataControlsTable, DataTable SearchCustomTable, ref ArrayList CustomVariables, int OverrideDynamicControlsScreenNum, TripleDESEncryptionClass TripleDES, TechnoProReports technoProReports, int DbLocationCode, string context)
		{
			this.context = context;
			this.InitializeComponent();
			this.technoProReports = technoProReports;
			this.dbLocationCode = DbLocationCode;
			this.searchInfoID = SearchInfoID;
			this.variablesTable = VariablesTable;
			this.comboBoxData = ComboBoxData;
			if (this.da == null)
			{
				this.da = ClientCache.CurrentInstance.da;
			}
			this.lookupTablesForControls = LookupTablesForControls;
			this.variables = Variables;
			this.sessions = Sessions;
			this.yearStartEnd = YearStartEnd;
			this.searchTitle = SearchTitle;
			this.dynamicScreenNonDataControlsTable = DynamicScreenNonDataControlsTable;
			this.searchCustomTable = SearchCustomTable;
			this.customVariables = CustomVariables;
			this.overrideDynamicControlsScreenNum = OverrideDynamicControlsScreenNum;
			this.tripleDES = TripleDES;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00044D6C File Offset: 0x00043D6C
		public VariablesInput(int SearchInfoID, DynamicControlCollection controlCollection, UnivDataAdapter Da, DataSet ComboBoxData, DataSet LookupTablesForControls, ref ArrayList Variables, DataTable Sessions, object[] YearStartEnd, string SearchTitle, DataTable DynamicScreenNonDataControlsTable, DataTable SearchCustomTable, ref ArrayList CustomVariables, int OverrideDynamicControlsScreenNum, TripleDESEncryptionClass TripleDES, TechnoProReports technoProReports, int DbLocationCode, string context)
		{
			this.context = context;
			this.InitializeComponent();
			this.technoProReports = technoProReports;
			this.dbLocationCode = DbLocationCode;
			this.searchInfoID = SearchInfoID;
			this.variablesTable = new DataTable();
			Type typeFromHandle = typeof(int);
			this.variablesTable.Columns.Add("controlid", typeFromHandle);
			this.variablesTable.Columns.Add("screennum", typeFromHandle);
			this.variablesTable.Columns.Add("controlcode", typeFromHandle);
			this.variablesTable.Columns.Add("controlcaption");
			this.variablesTable.Columns.Add("setting1", typeFromHandle);
			this.variablesTable.Columns.Add("setting2", typeFromHandle);
			this.variablesTable.Columns.Add("setting3", typeFromHandle);
			this.variablesTable.Columns.Add("defaultvalue", typeFromHandle);
			this.variablesTable.Columns.Add("defaultvaluestring");
			foreach (object obj in controlCollection)
			{
				DynamicControl dynamicControl = (DynamicControl)obj;
				DataRow dataRow = this.variablesTable.NewRow();
				dataRow["controlid"] = dynamicControl.ControlId;
				dataRow["screennum"] = -1;
				dataRow["controlcode"] = dynamicControl.ControlCode;
				dataRow["controlcaption"] = dynamicControl.ControlCaption;
				dataRow["setting1"] = dynamicControl.Setting1;
				dataRow["setting2"] = dynamicControl.Setting2;
				dataRow["setting3"] = dynamicControl.Setting3;
				dataRow["defaultvalue"] = dynamicControl.DefaultValue;
				dataRow["defaultvaluestring"] = dynamicControl.DefaultValueString;
				this.variablesTable.Rows.Add(dataRow);
			}
			this.comboBoxData = ComboBoxData;
			UnivConnection univConnection = UnivOleDbFactory.CreateConnection(Da.Connection.OriginalConnectionString);
			this.da = univConnection.CreateDataAdapter();
			this.lookupTablesForControls = LookupTablesForControls;
			this.variables = Variables;
			this.sessions = Sessions;
			this.yearStartEnd = YearStartEnd;
			this.searchTitle = SearchTitle;
			this.dynamicScreenNonDataControlsTable = DynamicScreenNonDataControlsTable;
			this.searchCustomTable = SearchCustomTable;
			this.customVariables = CustomVariables;
			this.overrideDynamicControlsScreenNum = OverrideDynamicControlsScreenNum;
			this.tripleDES = TripleDES;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00045F94 File Offset: 0x00044F94
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00045FAC File Offset: 0x00044FAC
		public DataRow ReportDr
		{
			get
			{
				return this.reportDr;
			}
			set
			{
				this.reportDr = value;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00045FB8 File Offset: 0x00044FB8
		private DataRow GetCustomRowOffTheGrid(int searchcustomid, string searchCustomCode, string description, string sql, bool multiselect)
		{
			DataRow dataRow = this.searchCustomTable.NewRow();
			dataRow[0] = searchcustomid;
			dataRow[1] = searchCustomCode;
			dataRow[2] = description;
			dataRow[3] = sql;
			dataRow[4] = multiselect;
			return dataRow;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00046010 File Offset: 0x00045010
		private DataRow GetCustomRow(string searchCustomCode, int screenNum)
		{
			int num = searchCustomCode.IndexOf("!");
			int num2 = searchCustomCode.IndexOf("@");
			DataRow result;
			if (num == 0 || num2 == 0)
			{
				string text = searchCustomCode.Substring(1);
				text = text.Replace("_", " ");
				result = this.GetCustomRowOffTheGrid(-10, searchCustomCode, "", text, num == 0);
			}
			else if (searchCustomCode.IndexOf("_screen") == 0)
			{
				string text2 = "SELECT dsc.controlid,dc.controlcaption,case when dc.controlcode=30 then cast(0 as bit) when dc.controlcode=31 then cast(0 as bit) else cast(1 as bit) end AS includeme FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=@screennum AND dsc.isactive=1 AND NOT dc.controlcode IN (SELECT controlcode FROM dynamicscreennondatacontrols WHERE not controlcode=30 AND NOT controlcode=31) ORDER BY dsc.ordernum";
				text2 = text2.Replace("@screennum", screenNum.ToString());
				result = this.GetCustomRowOffTheGrid(-11, searchCustomCode, "", text2, true);
			}
			else
			{
				result = this.GetSearchCustomRow(searchCustomCode);
			}
			return result;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000460D4 File Offset: 0x000450D4
		private void VariablesInput_Load(object sender, EventArgs e)
		{
			this.da2 = this.da;
			this.Text = "Report Parameters: " + this.searchTitle;
			if (this.customVariables.Count > 0)
			{
				this.p_custom.Visible = true;
				this.expandableSplitter1.Visible = true;
				this.webBrowser1.Visible = false;
				this.ts_custom.Visible = true;
				int y = 0;
				Graphics graphics = this.p_custom.CreateGraphics();
				int num = this.p_custom.Width - this.p_custom.DockPadding.Left - this.p_custom.DockPadding.Right - SystemInformation.VerticalScrollBarWidth;
				foreach (object obj in this.customVariables)
				{
					Variable variable = (Variable)obj;
					DataRow searchFunctionsDataRow = variable.SearchFunctionsDataRow;
					if (searchFunctionsDataRow != null)
					{
						string text = searchFunctionsDataRow[5].ToString().ToLower().Trim();
						bool flag = text.CompareTo("all") == 0;
						string text2 = variable.VariableName.Replace("custom", "");
						DataRow customRow = this.GetCustomRow(text2, -1);
						if (customRow != null)
						{
							this.variablesWithCustomSql.Add(variable);
							this.SetupSearchCustom(variable, y, -1);
						}
						else
						{
							this.ShowError("Can't find searchcustomrow!");
						}
					}
					else
					{
						this.ShowError("Can't find searchfunctionsrow!");
					}
				}
			}
			else
			{
				base.Height = 384;
			}
			int num2;
			if (this.overrideDynamicControlsScreenNum == 0)
			{
				num2 = this.searchInfoID;
			}
			else if (this.overrideDynamicControlsScreenNum < 0)
			{
				if (this.overrideDynamicControlsScreenNum == -1)
				{
					num2 = -1;
				}
				else
				{
					num2 = -this.overrideDynamicControlsScreenNum;
				}
			}
			else
			{
				num2 = this.overrideDynamicControlsScreenNum;
			}
			int num3 = this.p_data.Width - 10;
			foreach (object obj2 in this.variablesTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				int num4 = (dataRow["screennum"] != DBNull.Value) ? ((int)dataRow["screennum"]) : 0;
				if (num4 == 0)
				{
					dataRow["screennum"] = num2;
				}
			}
			bool flag2 = false;
			foreach (object obj3 in this.variablesTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj3;
				int num5 = (int)dataRow2["controlcode"];
				if (num5 == 800)
				{
					DynamicControl dynamicControl = new DynamicControl(dataRow2);
					if (dynamicControl.HasSpecialInstructions)
					{
						Control topLevelControl = this.p_data.TopLevelControl;
						string text3 = dynamicControl.SpecialInstructions("width");
						string text4 = dynamicControl.SpecialInstructions("height");
						string text5 = dynamicControl.SpecialInstructions("colwidth");
						string text6 = dynamicControl.SpecialInstructions("size");
						if (!string.IsNullOrEmpty(text6))
						{
							if (text6.Equals("small"))
							{
								base.Width = 600;
								base.Height = 500;
								num3 = 290;
							}
							else if (text6.Equals("medium"))
							{
								base.Width = 800;
								base.Height = 600;
								num3 = 390;
							}
							else if (text6.Equals("large"))
							{
								base.Width = 1000;
								base.Height = 700;
								num3 = 330;
							}
						}
						else
						{
							if ((!string.IsNullOrEmpty(text3) && text3.Equals("max")) || (!string.IsNullOrEmpty(text4) && text4.Equals("max")))
							{
								base.WindowState = FormWindowState.Maximized;
								num3 = topLevelControl.Width - SystemInformation.VerticalScrollBarWidth - 5;
								if (this.webBrowser1.Visible)
								{
									num3 -= this.webBrowser1.Width;
								}
							}
							else
							{
								if (text3 != null && text3.Trim().Length > 0)
								{
									int num6 = int.Parse(text3);
									if (num6 > 0)
									{
										topLevelControl.Width = num6 + (this.webBrowser1.Visible ? this.webBrowser1.Width : 0);
										if (!flag2)
										{
											num3 = num6 - SystemInformation.VerticalScrollBarWidth - 5;
										}
									}
								}
								if (text4 != null && text4.Trim().Length > 0)
								{
									int num7 = int.Parse(text4);
									if (num7 > 0)
									{
										topLevelControl.Height = num7;
									}
								}
							}
							if (!string.IsNullOrEmpty(text5))
							{
								int num8 = 0;
								if (int.TryParse(text5, out num8))
								{
									if (num8 > 0)
									{
										num3 = topLevelControl.Width - SystemInformation.VerticalScrollBarWidth - 5;
										if (this.webBrowser1.Visible)
										{
											num3 -= this.webBrowser1.Width;
										}
										num3 = Convert.ToInt32(Convert.ToDouble(num3) * (Convert.ToDouble(num8) / 100.0));
									}
								}
							}
						}
					}
					break;
				}
			}
			base.CenterToScreen();
			this.screen = new ScreenInfo(num2, this.p_data, true, 0, num3, 25, new Font("Arial", 9f), -1, "Unknown", false, false, Color.Empty, Color.Empty);
			Panel panel = this.p_data;
			DynamicScreen.TranslateControls(this.da2, this.tripleDES, ref panel, this.screen, this.variablesTable, ref this.comboBoxData, null, this.lookupTablesForControls, new ArrayList(), -1);
			Dictionary<int, Compiler> dictionary = new Dictionary<int, Compiler>();
			PersonBaseDTO student = new PersonBaseDTO
			{
				PersonId = 0,
				CoreGroup = eCoreGroupDTO.Unknown
			};
			this.existingCompiler = Compiler.SetupNewCompiler(ref dictionary, this.screen, this.p_data, this.da, this.tripleDES, student);
			this.ts_custom.Visible = false;
			Variable variable2 = this.GetVariable("schoolyear_startdate");
			if (variable2 != null && variable2.VariableValue is DateTime)
			{
				this.FixSchoolYearChooserToShowLastUserChosenRange(this.p_data, (DateTime)variable2.VariableValue);
			}
			this.AddCustomStuff();
			this.SetDefaultValues2();
			foreach (object obj4 in this.screenCombos)
			{
				AutoComboBox sender2 = (AutoComboBox)obj4;
				this.cmb_SelectedIndexChanged(sender2, new EventArgs());
			}
			if (this.p_data.Controls.Count < 1 && this.p_custom.Controls.Count < 1)
			{
				base.Close();
			}
			if (this.reportDr != null)
			{
				string text7 = this.reportDr["title"].ToString();
				string text8 = this.reportDr["description"].ToString();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<h2>");
				stringBuilder.Append(this.reportDr["title"].ToString());
				stringBuilder.Append("</h2>");
				stringBuilder.Append("<p>");
				stringBuilder.Append(this.reportDr["description"].ToString());
				stringBuilder.Append("</p>");
				if (DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.ReportExecutionLog))
				{
					this.da.SelectCommand.CommandText = "SELECT s.personid,p.firstname,p.lastname,s.reportrundate FROM searchinfolog s LEFT JOIN people p ON p.personid=s.personid WHERE s.searchinfoid=@rid ORDER BY s.reportrundate DESC";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@rid", (int)this.reportDr[0]);
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					dataTable = this.tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"firstname",
						"lastname"
					});
					if (dataTable.Rows.Count > 0)
					{
						DataRow dataRow3 = dataTable.Rows[0];
						stringBuilder.Append("<p>Last run by: ");
						stringBuilder.Append(dataRow3["firstname"].ToString());
						stringBuilder.Append(" ");
						stringBuilder.Append(dataRow3["lastname"].ToString());
						stringBuilder.Append(" [");
						stringBuilder.Append(((DateTime)dataRow3["reportrundate"]).ToString("yyyy-MM-dd h:mm tt"));
						stringBuilder.Append("]");
					}
				}
				this.ShowDetailString(stringBuilder.ToString());
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00046B74 File Offset: 0x00045B74
		private void SetDefaultValues2()
		{
			try
			{
				string valueName = "vars_" + this.context;
				string registryValueStringCurrentUser = ClockWorkCore.GetRegistryValueStringCurrentUser(valueName, true);
				if (registryValueStringCurrentUser != null && registryValueStringCurrentUser.Length > 0)
				{
					string s = CompressionTP.Decompress(registryValueStringCurrentUser, false);
					DataSet dataSet = new DataSet();
					StringReader input = new StringReader(s);
					XmlReader reader = XmlReader.Create(input);
					dataSet.ReadXml(reader, XmlReadMode.ReadSchema);
					if (dataSet.Tables.Count > 0)
					{
						DataTable userSelections = dataSet.Tables[0];
						this.ResetVariableValues(this.p_data, userSelections);
					}
				}
				this.EnsureDatesAreNotBlank(this.p_data);
			}
			catch
			{
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00046C3C File Offset: 0x00045C3C
		private void EnsureDatesAreNotBlank(Control parent)
		{
			if (parent is MyDateTimePicker)
			{
				MyDateTimePicker myDateTimePicker = (MyDateTimePicker)parent;
				if (myDateTimePicker.Value == DateTime.MinValue)
				{
					myDateTimePicker.Value = DateTime.Now;
				}
			}
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				this.EnsureDatesAreNotBlank(parent2);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00046CE0 File Offset: 0x00045CE0
		private void FixSchoolYearChooserToShowLastUserChosenRange(Control p, DateTime schoolYearStartDate)
		{
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel)
				{
					this.FixSchoolYearChooserToShowLastUserChosenRange(control, schoolYearStartDate);
				}
				else if (control is SchoolYearChooserCtrl)
				{
					SchoolYearChooserCtrl schoolYearChooserCtrl = (SchoolYearChooserCtrl)control;
					schoolYearChooserCtrl.DateScopes.SetScope(schoolYearStartDate);
					schoolYearChooserCtrl.UpdateDates();
					break;
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00046D8C File Offset: 0x00045D8C
		private void ClearControls(Panel c0)
		{
			foreach (object obj in c0.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel || control is GroupBox)
				{
					this.ClearControls((Panel)control);
				}
				else if (control != null)
				{
					if (control is Label)
					{
						Label label = (Label)control;
						label.Click -= this.l_Click;
					}
					else if (control is Button)
					{
						Button button = (Button)control;
						button.Click -= this.btn_Click;
					}
					else if (control is CheckBox || control is MyCheckBox)
					{
						CheckBox checkBox = (CheckBox)control;
						checkBox.MouseUp -= this.chk_MouseUp;
						checkBox.CheckedChanged -= this.chk_CheckedChanged;
					}
					c0.Controls.Remove(control);
					control.Dispose();
				}
			}
			c0.Controls.Clear();
			c0.Dispose();
			c0 = null;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00046F24 File Offset: 0x00045F24
		private void SetupSearchCustom(Variable v, int y, int screenNum)
		{
			DataRow searchFunctionsDataRow = v.SearchFunctionsDataRow;
			string text = searchFunctionsDataRow[5].ToString().ToLower().Trim();
			bool flag = text.CompareTo("all") == 0;
			this.searchCustomCode = v.VariableName.Replace("custom", "");
			DataRow customRow = this.GetCustomRow(this.searchCustomCode, screenNum);
			Variable variable = this.GetVariable("@customcode" + this.searchCustomCode);
			if (variable != null && variable.VariableValue.ToString().Trim().Length < 1)
			{
				variable = null;
			}
			Graphics graphics = this.p_custom.CreateGraphics();
			int num = this.p_custom.Width - this.p_custom.DockPadding.Left - this.p_custom.DockPadding.Right - SystemInformation.VerticalScrollBarWidth;
			string commandText = customRow[3].ToString().Trim();
			this.da.SelectCommand.CommandText = commandText;
			DataTable dataTable = new DataTable();
			string text2;
			this.da.Fill(dataTable, out text2);
			if (text2 != null && text2.Length > 0)
			{
				this.ShowError(text2);
			}
			if (this.p != null)
			{
				this.ClearControls(this.p);
				this.p = null;
			}
			this.p = new Panel();
			this.p.Tag = customRow;
			this.p.AutoScroll = true;
			this.p.Dock = DockStyle.Fill;
			int num2 = 0;
			int num3 = -1;
			int num4 = -1;
			if (dataTable.Columns.Count > 2)
			{
				Type type = Type.GetType("System.String");
				Type type2 = Type.GetType("System.Boolean");
				for (int i = 2; i < dataTable.Columns.Count; i++)
				{
					if (num3 < 0 && dataTable.Columns[i].DataType == type)
					{
						num3 = i;
					}
					else if (num4 < 0 && dataTable.Columns[i].DataType == type2)
					{
						num4 = i;
					}
				}
			}
			int count = dataTable.Columns.Count;
			string text3 = customRow[2].ToString().Trim();
			Label label = new Label();
			label.Font = new Font(this.p_custom.Font.FontFamily, 14f);
			label.Text = text3;
			label.Width = this.p_custom.Width - SystemInformation.Border3DSize.Width * 2;
			label.Dock = DockStyle.Top;
			label.Click += this.l_Click;
			this.p.Controls.Add(label);
			num2 += label.Height + 10;
			StringFormat stringFormat = new StringFormat();
			stringFormat.FormatFlags = StringFormatFlags.FitBlackBox;
			stringFormat.Trimming = StringTrimming.Character;
			SizeF layoutArea = new SizeF((float)num, (float)base.Height);
			int num5;
			int num6;
			graphics.MeasureString(label.Text, label.Font, layoutArea, stringFormat, out num5, out num6);
			if (num6 > 1)
			{
				label.Height *= num6;
			}
			int num7 = 0;
			int num8 = 0;
			Panel panel = null;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (num4 >= 0 && (dataRow[num4] == DBNull.Value || !(bool)dataRow[num4]))
				{
					num7++;
				}
				else
				{
					if (num4 >= 0 && num7 != num8)
					{
						if (panel != null)
						{
							Button button = new Button();
							button.TabStop = false;
							button.Text = "s e l e c t";
							button.Click += this.btn_Click;
							button.Dock = DockStyle.Right;
							button.Width = 25;
							panel.Controls.Add(button);
							button.SendToBack();
							this.SetPanelHeight(panel);
						}
						panel = new Panel();
						panel.Dock = DockStyle.Top;
						panel.BorderStyle = BorderStyle.Fixed3D;
						this.p.Controls.Add(panel);
						panel.BringToFront();
					}
					num8 = num7;
					CheckBox checkBox = new CheckBox();
					checkBox.TextAlign = ContentAlignment.TopLeft;
					checkBox.Text = dataRow[1].ToString().Trim();
					if (num3 > 1)
					{
						string text4 = dataRow[num3].ToString().Trim();
						if (text4.Length > 0)
						{
							CheckBox checkBox2 = checkBox;
							checkBox2.Text = checkBox2.Text + ": " + text4;
						}
					}
					checkBox.Tag = dataRow;
					int num9;
					if (panel != null)
					{
						num9 = num - SystemInformation.Border3DSize.Width * 2 - 60;
					}
					else
					{
						num9 = num - SystemInformation.Border3DSize.Width * 2 - 35;
					}
					SizeF layoutArea2 = new SizeF((float)num9, (float)base.Height);
					int num10;
					int num11;
					SizeF sizeF = graphics.MeasureString(checkBox.Text, checkBox.Font, layoutArea2, stringFormat, out num10, out num11);
					if (num11 > 1)
					{
						checkBox.Height = (checkBox.Height - 2) * num11;
					}
					checkBox.Dock = DockStyle.Top;
					string text5 = checkBox.Text.ToLower();
					if (variable != null)
					{
						checkBox.Checked = variable.CommaSeparatedValueContains(text5.Replace(",", ""));
					}
					else
					{
						checkBox.Checked = (flag || text.IndexOf(text5) >= 0);
					}
					checkBox.MouseUp += this.chk_MouseUp;
					checkBox.CheckedChanged += this.chk_CheckedChanged;
					if (panel != null)
					{
						panel.Controls.Add(checkBox);
					}
					else
					{
						this.p.Controls.Add(checkBox);
					}
					checkBox.BringToFront();
					num2 += checkBox.Height + 5;
				}
			}
			if (panel != null)
			{
				Button button = new Button();
				button.TabStop = false;
				button.Text = "s e l e c t";
				button.Click += this.btn_Click;
				button.Dock = DockStyle.Right;
				button.Width = 25;
				panel.Controls.Add(button);
				button.SendToBack();
				this.SetPanelHeight(panel);
				panel = null;
			}
			this.p.Top = y;
			this.p.Left = 0;
			y += this.p.Height;
			this.p.MouseUp += this.chk_MouseUp;
			this.p_custom.Controls.Add(this.p);
			this.contextMenuCustomPanel = this.p;
			this.EnableDisableSelectAll(this.p);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000476E8 File Offset: 0x000466E8
		private void SetPanelHeight(Panel p)
		{
			int num = 5;
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is CheckBox || control is MyCheckBox)
				{
					int num2 = control.Top + control.Height;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			num += SystemInformation.Border3DSize.Height * 2;
			if (num > 0)
			{
				p.Height = num;
			}
			p.AutoScroll = true;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000477BC File Offset: 0x000467BC
		private void AdjustWidthsHeights(ref Panel p)
		{
			Graphics graphics = p.CreateGraphics();
			StringFormat stringFormat = new StringFormat();
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is Label)
				{
					Label label = (Label)control;
					SizeF layoutArea = new SizeF((float)(label.Width - SystemInformation.Border3DSize.Width), (float)label.Height);
					int num;
					int num2;
					Size size = graphics.MeasureString(label.Text, label.Font, layoutArea, stringFormat, out num, out num2).ToSize();
					if (num2 > 1)
					{
						int num3 = label.Height - SystemInformation.Border3DSize.Height;
						label.Height = num3 * num2;
					}
				}
				else if (control is CheckBox || control is MyCheckBox)
				{
					CheckBox checkBox = (CheckBox)control;
					SizeF layoutArea = new SizeF((float)(checkBox.Width - SystemInformation.MenuCheckSize.Width - SystemInformation.Border3DSize.Width - 10), (float)checkBox.Height);
					int num;
					int num2;
					Size size = graphics.MeasureString(checkBox.Text, checkBox.Font, layoutArea, stringFormat, out num, out num2).ToSize();
					if (num2 > 1)
					{
						int num3 = checkBox.Height - SystemInformation.Border3DSize.Height;
						checkBox.Height = num3 * num2;
					}
				}
				else if (control is Panel)
				{
					Panel panel = (Panel)control;
					this.AdjustWidthsHeights(ref panel);
				}
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000479CC File Offset: 0x000469CC
		private void AddCustomStuff()
		{
			this.FixPerStudentScreenNameChooser(this.p_data);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000479DC File Offset: 0x000469DC
		private void FixPerStudentScreenNameChooser(Panel p)
		{
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel)
				{
					this.FixPerStudentScreenNameChooser((Panel)control);
				}
				else if (control is AutoComboBox && control.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)control.Tag;
					string text = dataRow[3].ToString().Trim().ToLower();
					string text2 = null;
					if (text.IndexOf("per student screen name") == 0)
					{
						text2 = "(typecode=0)";
					}
					else if (text.IndexOf("per appointment screen name") == 0)
					{
						text2 = "(typecode=1)";
					}
					if (text2 != null)
					{
						AutoComboBox autoComboBox = (AutoComboBox)control;
						this.da2.SelectCommand.CommandText = "SELECT screenid,screennum,description FROM screens WHERE isactive=@true AND " + text2 + " ORDER BY sign(screennum-2),description";
						this.da2.SelectCommand.Parameters.Clear();
						this.da2.SelectCommand.Parameters.Add("@true", true);
						DataTable dataTable = new DataTable();
						this.da2.Fill(dataTable);
						autoComboBox.DataSource = dataTable;
						autoComboBox.DisplayMember = "description";
						this.ignoreScreenComboSelectedIndexChanged = true;
						autoComboBox.SelectedIndexChanged += this.cmb_SelectedIndexChanged;
						this.screenCombos.Add(autoComboBox);
						this.ignoreScreenComboSelectedIndexChanged = false;
						break;
					}
				}
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00047BD4 File Offset: 0x00046BD4
		private void DisposePanel(Panel panel)
		{
			for (int i = 0; i < panel.Controls.Count; i++)
			{
				Control control = panel.Controls[i];
				if (control is Panel)
				{
					this.DisposePanel((Panel)control);
				}
				else if (control is CheckBox || control is MyCheckBox)
				{
					CheckBox checkBox = (CheckBox)control;
					checkBox.MouseUp -= this.chk_MouseUp;
					checkBox.CheckedChanged -= this.chk_CheckedChanged;
					control.Dispose();
				}
			}
			panel.Controls.Clear();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00047C8F File Offset: 0x00046C8F
		private void ShowError(string message)
		{
			MessageBox.Show(message);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00047C9C File Offset: 0x00046C9C
		private DataRow GetSearchCustomRow(string SearchCustomCode)
		{
			foreach (object obj in this.searchCustomTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[1].ToString().ToLower().Trim();
				if (text.CompareTo(SearchCustomCode) == 0)
				{
					return dataRow;
				}
			}
			return null;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00047D3C File Offset: 0x00046D3C
		private void SetDefaultValues(Panel p)
		{
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel)
				{
					this.SetDefaultValues((Panel)control);
				}
				else if (control.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)control.Tag;
					string text = dataRow[8].ToString();
					string caption = dataRow["controlcaption"].ToString();
					if (text.CompareTo("STARTYEAR") == 0)
					{
						if (this.yearStartEnd != null && this.yearStartEnd.Length > 1)
						{
							DateTime value = (DateTime)this.yearStartEnd[0];
							Variable variable = this.GetVariable("startdate");
							if (variable != null && variable.VariableValue != null && variable.VariableValue is DateTime)
							{
								value = (DateTime)variable.VariableValue;
							}
							if (control is MyDateTimePicker)
							{
								MyDateTimePicker myDateTimePicker = (MyDateTimePicker)control;
								myDateTimePicker.Value = value;
								myDateTimePicker.Format = DateTimePickerFormat.Custom;
								myDateTimePicker.CustomFormat = "MMM dd, yyyy";
							}
						}
					}
					else if (text.CompareTo("ENDYEAR") == 0)
					{
						if (this.yearStartEnd != null && this.yearStartEnd.Length > 1)
						{
							DateTime value2 = (DateTime)this.yearStartEnd[1];
							Variable variable = this.GetVariable("enddate");
							if (variable != null && variable.VariableValue != null && variable.VariableValue is DateTime)
							{
								value2 = (DateTime)variable.VariableValue;
							}
							if (control is MyDateTimePicker)
							{
								MyDateTimePicker myDateTimePicker = (MyDateTimePicker)control;
								myDateTimePicker.Value = value2;
								myDateTimePicker.Format = DateTimePickerFormat.Custom;
								myDateTimePicker.CustomFormat = "MMM dd, yyyy";
							}
						}
					}
					else if (control is TextBox || control is Label)
					{
						control.Text = text;
					}
					else if (control is MyCheckBox)
					{
						MyCheckBox myCheckBox = (MyCheckBox)control;
						Variable variable2 = this.GetVariable(VariablesInput.GetVarName(myCheckBox.Text));
						if (variable2 != null && ((variable2.VariableValue is bool && (bool)variable2.VariableValue) || (variable2.VariableValue is string && variable2.VariableValue.ToString().Trim().Length > 0)))
						{
							myCheckBox.Checked = true;
						}
						else if (text.Length > 0)
						{
							myCheckBox.Checked = true;
						}
					}
					else if (control is AutoComboBox)
					{
						AutoComboBox autoComboBox = (AutoComboBox)control;
						Variable variable2 = this.GetVariable(VariablesInput.GetVarName(caption));
						if (variable2 != null)
						{
							DataTable dataTable;
							if (autoComboBox.DataSource is DataTable)
							{
								dataTable = (DataTable)autoComboBox.DataSource;
							}
							else if (autoComboBox.DataSource is DataView)
							{
								dataTable = ((DataView)autoComboBox.DataSource).Table;
							}
							else
							{
								dataTable = null;
							}
							if (dataTable != null)
							{
								string strB = variable2.VariableValue.ToString().Trim().ToLower();
								for (int i = 0; i < dataTable.Rows.Count; i++)
								{
									DataRow dataRow2 = dataTable.Rows[i];
									string text2 = dataRow2[autoComboBox.DisplayMember].ToString().Trim().ToLower();
									if (text2.CompareTo(strB) == 0)
									{
										autoComboBox.SelectedIndex = i;
										break;
									}
								}
							}
						}
						else
						{
							DataTable dataTable2;
							if (autoComboBox.DataSource is DataTable)
							{
								dataTable2 = (DataTable)autoComboBox.DataSource;
							}
							else if (autoComboBox.DataSource is DataView)
							{
								dataTable2 = ((DataView)autoComboBox.DataSource).Table;
							}
							else
							{
								dataTable2 = null;
							}
							if (dataTable2 != null)
							{
								int num = dataTable2.Columns.IndexOf(autoComboBox.DisplayMember);
								if (num >= 0)
								{
									string strB2 = text.Trim().ToLower();
									bool flag = false;
									foreach (object obj2 in dataTable2.Rows)
									{
										DataRow dataRow3 = (DataRow)obj2;
										string text3 = dataRow3[num].ToString().Trim().ToLower();
										if (text3.CompareTo(strB2) == 0)
										{
											flag = true;
											break;
										}
									}
									if (flag)
									{
										control.Text = text;
									}
									else
									{
										autoComboBox.SelectedIndex = 0;
									}
								}
								else
								{
									control.Text = text;
								}
							}
							else
							{
								control.Text = text;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00048350 File Offset: 0x00047350
		private Variable GetVariable(string varName)
		{
			string strB = varName.Trim().ToLower();
			foreach (object obj in this.variables)
			{
				Variable variable = (Variable)obj;
				string text = variable.VariableName.Trim().ToLower();
				if (text.CompareTo(strB) == 0)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000483F8 File Offset: 0x000473F8
		private void VariablesInput_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000483FB File Offset: 0x000473FB
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00048408 File Offset: 0x00047408
		private void VariablesInput_Closing(object sender, CancelEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				base.ActiveControl = null;
				this.SaveVariableValues(this.p_data);
				try
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("controlid", typeof(int));
					dataTable.Columns.Add("val");
					this.RememberVariableValues(this.p_data, ref dataTable);
					string valueName = "vars_" + this.context;
					DataSet dataSet = new DataSet();
					dataSet.Tables.Add(dataTable);
					StringBuilder stringBuilder = new StringBuilder();
					StringWriter writer = new StringWriter(stringBuilder);
					dataSet.WriteXml(writer, XmlWriteMode.WriteSchema);
					string text = stringBuilder.ToString();
					string valueObject = CompressionTP.Compress(text, false);
					ClockWorkCore.SetRegistryValueCurrentUser(valueName, valueObject, true);
				}
				catch
				{
				}
				if (this.p_custom.Visible && !this.webBrowser1.Visible)
				{
					this.SaveCustomVariableValues(this.p_custom);
				}
			}
			foreach (object obj in this.screenCombos)
			{
				AutoComboBox autoComboBox = (AutoComboBox)obj;
				autoComboBox.SelectedIndexChanged -= this.cmb_SelectedIndexChanged;
			}
			this.screenCombos.Clear();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000485A0 File Offset: 0x000475A0
		private void SaveCustomVariableValues(Panel panel)
		{
			foreach (object obj in panel.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel && control.Tag is DataRow)
				{
					Panel panel2 = (Panel)control;
					DataRow dataRow = (DataRow)panel2.Tag;
					string str = dataRow[1].ToString().Trim();
					string variableName = "custom" + str;
					Variable variable = this.GetVariable(this.customVariables, variableName);
					DataRow searchFunctionsDataRow = variable.SearchFunctionsDataRow;
					string sqlInjection = searchFunctionsDataRow[6].ToString().Trim();
					string text = searchFunctionsDataRow[7].ToString().Trim();
					if (text.Length < 1)
					{
						text = "OR";
					}
					text = " " + text + " ";
					string variableValue = "";
					this.SaveCustomVariableValues2(panel2, ref variableValue, sqlInjection, text);
					variable.VariableValue = variableValue;
				}
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00048704 File Offset: 0x00047704
		private void SaveCustomVariableValues2(Panel p, ref string varValue, string sqlInjection, string sqlOperator)
		{
			string text = "";
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if ((control is CheckBox || control is MyCheckBox) && control.Tag is DataRow)
				{
					CheckBox checkBox = (CheckBox)control;
					if (checkBox.Checked)
					{
						if (text.Length > 0)
						{
							text += ",";
						}
						text += checkBox.Text.Replace(",", "");
						DataRow dataRow = (DataRow)checkBox.Tag;
						string text2 = sqlInjection;
						for (int i = 0; i < dataRow.Table.Columns.Count; i++)
						{
							text2 = text2.Replace("#<" + i.ToString() + ">#", dataRow[i].ToString().Trim());
						}
						if (varValue.Length > 0)
						{
							varValue += sqlOperator;
						}
						varValue += text2;
					}
				}
				else if (control is Panel)
				{
					this.SaveCustomVariableValues2((Panel)control, ref varValue, sqlInjection, sqlOperator);
				}
			}
			if (text.Length > 0)
			{
				this.SaveVariable(this.variables, "@customcode" + this.searchCustomCode, text);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000488F4 File Offset: 0x000478F4
		private void RememberVariableValues(Control parent, ref DataTable userSelections)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is MyDynamicControl && control.Tag != null && control.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)control.Tag;
					int num = (int)dataRow["controlid"];
					MyDynamicControl myDynamicControl = (MyDynamicControl)control;
					string text = myDynamicControl.ToString();
					userSelections.Rows.Add(new object[]
					{
						num,
						text
					});
				}
				if (control.Controls.Count > 0)
				{
					this.RememberVariableValues(control, ref userSelections);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00048A04 File Offset: 0x00047A04
		private void ResetVariableValues(Control parent, DataTable userSelections)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is MyDynamicControl)
				{
					DataRow dataRow = (DataRow)control.Tag;
					int num = (dataRow == null) ? 0 : ((int)dataRow["controlid"]);
					MyDynamicControl myDynamicControl = (MyDynamicControl)control;
					foreach (object obj2 in userSelections.Rows)
					{
						DataRow dataRow2 = (DataRow)obj2;
						int num2 = (int)dataRow2[0];
						if (num2 == num)
						{
							string s = dataRow2[1].ToString();
							myDynamicControl.FromString(s);
							break;
						}
					}
				}
				if (control.Controls.Count > 0)
				{
					this.ResetVariableValues(control, userSelections);
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00048B80 File Offset: 0x00047B80
		private int LookupSNum(string snum)
		{
			byte[] parameterValue = this.tripleDES.Encrypt(snum);
			this.da2.SelectCommand.CommandText = "SELECT personid FROM people WHERE student_no=@student_no";
			this.da2.SelectCommand.Parameters.Clear();
			this.da2.SelectCommand.Parameters.Add("@student_no", parameterValue);
			DataTable dataTable = new DataTable();
			this.da2.Fill(dataTable);
			int result;
			if (dataTable.Rows.Count <= 0)
			{
				result = -1;
			}
			else
			{
				result = (int)dataTable.Rows[0][0];
			}
			return result;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00048C2C File Offset: 0x00047C2C
		private void SaveVariableValues(Panel p)
		{
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel)
				{
					this.SaveVariableValues((Panel)control);
				}
				else if (control.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)control.Tag;
					int num = (int)dataRow[2];
					if (DynamicScreen.IsControlCodeDataHolding(this.dynamicScreenNonDataControlsTable, num))
					{
						string text = VariablesInput.GetVarName(dataRow);
						bool flag = text != null && text.Equals("studentnumber", StringComparison.OrdinalIgnoreCase);
						bool flag2 = text != null && text.EndsWith("~~encrypted", StringComparison.OrdinalIgnoreCase);
						object obj2;
						if (control is MyDynamicControl)
						{
							MyDynamicControl myDynamicControl = (MyDynamicControl)control;
							obj2 = myDynamicControl.ReportObject;
							if (obj2 != null && obj2 is string)
							{
								if (flag)
								{
									string text2 = ((string)obj2).Trim().ToUpper();
									if (!string.IsNullOrEmpty(text2))
									{
										obj2 = this.LookupSNum(text2);
									}
								}
								else if (flag2)
								{
									string plainText = (string)obj2;
									obj2 = this.tripleDES.Encrypt(plainText);
								}
							}
						}
						else if (control is TextBox || control is MyTextBox)
						{
							if (flag)
							{
								obj2 = this.LookupSNum(control.Text.Trim());
							}
							else if (flag2)
							{
								obj2 = this.tripleDES.Encrypt(control.Text.Trim().ToUpper());
							}
							else
							{
								obj2 = control.Text.Trim();
							}
						}
						else if (control is AutoComboBox)
						{
							AutoComboBox autoComboBox = (AutoComboBox)control;
							if (num == 100 && autoComboBox.DataSource != null)
							{
								obj2 = autoComboBox.SelectedValue;
							}
							else
							{
								obj2 = autoComboBox.Text;
							}
						}
						else if (control is DateTimePicker)
						{
							obj2 = ((DateTimePicker)control).Value;
						}
						else if (control is CheckBox || control is MyCheckBox)
						{
							CheckBox checkBox = (CheckBox)control;
							obj2 = checkBox.Checked;
						}
						else if (control is SchoolYearChooserCtrl)
						{
							SchoolYearChooserCtrl schoolYearChooserCtrl = (SchoolYearChooserCtrl)control;
							text = "schoolyear_startdate";
							obj2 = schoolYearChooserCtrl.StartDate;
							this.SaveVariableValue(text, obj2);
							text = "schoolyear_enddate";
							obj2 = schoolYearChooserCtrl.EndDate;
						}
						else
						{
							obj2 = DBNull.Value;
						}
						this.SaveVariableValue(text, obj2);
					}
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00048F84 File Offset: 0x00047F84
		public static string GetVarName(DataRow dr)
		{
			string caption = dr[3].ToString();
			return VariablesInput.GetVarName(caption);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00048FAC File Offset: 0x00047FAC
		internal static string GetVarName(string caption)
		{
			string text = caption.Replace(" ", "");
			string text2 = "";
			foreach (char c in text.ToCharArray())
			{
				if (char.IsLetter(c))
				{
					text2 += c;
				}
			}
			return text2;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0004901C File Offset: 0x0004801C
		private Variable SaveVariableValue(string variableName, object variableValue)
		{
			return this.SaveVariable(this.variables, variableName, variableValue);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0004903C File Offset: 0x0004803C
		private Variable SaveVariable(ArrayList variablesList, string variableName, object variableValue)
		{
			string text = variableName.Trim().ToLower();
			Variable variable = this.GetVariable(variablesList, variableName);
			if (variable == null)
			{
				variable = new Variable(variableName, variableValue);
				variablesList.Add(variable);
			}
			else
			{
				variable.VariableValue = variableValue;
			}
			return variable;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0004908C File Offset: 0x0004808C
		private Variable GetVariable(ArrayList variablesList, string variableName)
		{
			variableName = variableName.ToLower().Trim();
			foreach (object obj in variablesList)
			{
				Variable variable = (Variable)obj;
				string strB = variable.VariableName.ToLower().Trim();
				if (variableName.CompareTo(strB) == 0)
				{
					return variable;
				}
			}
			return null;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0004912C File Offset: 0x0004812C
		private Variable SaveCustomVariableValue(string variableName, object variableValue)
		{
			return this.SaveVariable(this.customVariables, variableName, variableValue);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0004914C File Offset: 0x0004814C
		private object[] GetStartEndSessionDates(DateTime nowAdjusted)
		{
			int month = nowAdjusted.Month;
			int day = nowAdjusted.Day;
			int year = nowAdjusted.Year;
			DateTime dateTime = nowAdjusted;
			DateTime dateTime2 = nowAdjusted;
			DataRow dataRow = null;
			foreach (object obj in this.sessions.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				int num = (int)dataRow2[2];
				int num2 = (int)dataRow2[4];
				int num3 = (int)dataRow2[3];
				int num4 = (int)dataRow2[5];
				if (num2 < num)
				{
					if (month >= num && day >= num3)
					{
						dateTime = new DateTime(year, num, num3, 0, 0, 0);
						dateTime2 = new DateTime(year + 1, num2, num4, 23, 59, 59);
						dataRow = dataRow2;
						break;
					}
					if (month <= num2 && day <= num4)
					{
						dateTime = new DateTime(year - 1, num, num3, 0, 0, 0);
						dateTime2 = new DateTime(year, num2, num4, 23, 59, 59);
						dataRow = dataRow2;
						break;
					}
				}
				else if (month >= num && month <= num2 && day >= num3 && day <= num4)
				{
					dateTime = new DateTime(year, num, num3, 0, 0, 0);
					dateTime2 = new DateTime(year, num2, num4, 23, 59, 59);
					dataRow = dataRow2;
					break;
				}
			}
			return new object[]
			{
				dateTime,
				dateTime2,
				dataRow
			};
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0004932C File Offset: 0x0004832C
		private void chk_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (sender is CheckBox || sender is MyCheckBox)
				{
					CheckBox checkBox = (CheckBox)sender;
					if (checkBox.Parent is Panel)
					{
						this.contextMenuCustomPanel = this.p_custom;
						this.cm_customCheckboxes.Show(checkBox, new Point(e.X, e.Y));
					}
				}
				else if (sender is Panel)
				{
					this.contextMenuCustomPanel = (Panel)sender;
					this.cm_customCheckboxes.Show(this.contextMenuCustomPanel, new Point(e.X, e.Y));
				}
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000493FC File Offset: 0x000483FC
		private void EnableDisableSelectAll(Panel p)
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = false;
			this.CheckAllCustomChecked(p, ref flag, ref flag2, ref flag3);
			this.btn_customSelectAll.Enabled = !flag;
			this.btn_selectNone.Enabled = !flag2;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00049440 File Offset: 0x00048440
		private void CheckAllCustomChecked(Panel p, ref bool allSelected, ref bool noneSelected, ref bool atLeastOneSelected)
		{
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is CheckBox || control is MyCheckBox)
				{
					CheckBox checkBox = (CheckBox)control;
					allSelected = (allSelected && checkBox.Checked);
					noneSelected = (noneSelected && !checkBox.Checked);
					atLeastOneSelected = (atLeastOneSelected || checkBox.Checked);
				}
				else if (control is Panel)
				{
					this.CheckAllCustomChecked((Panel)control, ref allSelected, ref noneSelected, ref atLeastOneSelected);
				}
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00049524 File Offset: 0x00048524
		private void MENU_customCheckSelectAll_Click(object sender, EventArgs e)
		{
			if (this.contextMenuCustomPanel != null)
			{
				this.CheckUncheckAllCustom(ref this.contextMenuCustomPanel, true);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00049550 File Offset: 0x00048550
		private void CheckUncheckAllCustom(ref Panel p, bool newChecked)
		{
			foreach (object obj in p.Controls)
			{
				Control control = (Control)obj;
				if (control is CheckBox || control is MyCheckBox)
				{
					CheckBox checkBox = (CheckBox)control;
					if (checkBox.Checked != newChecked)
					{
						checkBox.Checked = newChecked;
					}
				}
				else if (control is Panel)
				{
					Panel panel = (Panel)control;
					this.CheckUncheckAllCustom(ref panel, newChecked);
				}
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0004961C File Offset: 0x0004861C
		private void MENU_customChecksClearAll_Click(object sender, EventArgs e)
		{
			if (this.contextMenuCustomPanel != null)
			{
				this.CheckUncheckAllCustom(ref this.contextMenuCustomPanel, false);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00049647 File Offset: 0x00048647
		private void btn_fakeOK_Click(object sender, EventArgs e)
		{
			this.btn_runReport_Click(this.btn_runReport, null);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00049658 File Offset: 0x00048658
		private void chk_CheckedChanged(object sender, EventArgs e)
		{
			if (sender is CheckBox || sender is MyCheckBox)
			{
				CheckBox checkBox = (CheckBox)sender;
				Panel panel = this.p_custom;
				this.EnableDisableSelectAll(panel);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0004969C File Offset: 0x0004869C
		private void btn_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (button.Parent is Panel)
			{
				Panel panel = (Panel)button.Parent;
				bool flag = true;
				foreach (object obj in panel.Controls)
				{
					Control control = (Control)obj;
					if (control is CheckBox || control is MyCheckBox)
					{
						CheckBox checkBox = (CheckBox)control;
						if (!checkBox.Checked)
						{
							flag = false;
							break;
						}
					}
				}
				foreach (object obj2 in panel.Controls)
				{
					Control control = (Control)obj2;
					if (control is CheckBox || control is MyCheckBox)
					{
						CheckBox checkBox = (CheckBox)control;
						if (checkBox.Checked != !flag)
						{
							checkBox.Checked = !flag;
						}
					}
				}
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00049810 File Offset: 0x00048810
		private void l_Click(object sender, EventArgs e)
		{
			if (this.p_custom.Controls.Count > 0)
			{
				this.p_custom.Controls[0].Focus();
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00049854 File Offset: 0x00048854
		private void cmb_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.ignoreScreenComboSelectedIndexChanged)
			{
				AutoComboBox autoComboBox = (AutoComboBox)sender;
				string text = autoComboBox.Text;
				if (this.variablesWithCustomSql.Count > 0 && text.Length > 0)
				{
					Variable v = (Variable)this.variablesWithCustomSql[0];
					this.da2.SelectCommand.CommandText = "SELECT screennum FROM screens WHERE description=@description";
					this.da2.SelectCommand.Parameters.Clear();
					this.da2.SelectCommand.Parameters.Add("@description", text);
					DataTable dataTable = new DataTable();
					this.da2.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						this.SetupSearchCustom(v, 0, (int)dataTable.Rows[0][0]);
					}
				}
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00049952 File Offset: 0x00048952
		private void btn_selectNone_Click(object sender, EventArgs e)
		{
			this.MENU_customChecksClearAll_Click(this.MENU_customChecksClearAll, null);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00049963 File Offset: 0x00048963
		private void btn_customSelectAll_Click(object sender, EventArgs e)
		{
			this.MENU_customCheckSelectAll_Click(this.MENU_customCheckSelectAll, null);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00049974 File Offset: 0x00048974
		private void btn_runReport_Click(object sender, EventArgs e)
		{
			if (this.existingCompiler == null || this.existingCompiler.PreSave())
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000499AE File Offset: 0x000489AE
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000499B8 File Offset: 0x000489B8
		private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.ToString() != "about:blank")
			{
				e.Cancel = true;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000499EC File Offset: 0x000489EC
		private void ShowDetailString(string html)
		{
			this.webBrowser1.Navigate("about:blank");
			if (this.webBrowser1.Document != null)
			{
				this.webBrowser1.Document.Write(string.Empty);
			}
			this.webBrowser1.DocumentText = "<html><head><style TYPE=\"text/css\"> <!-- body { font-family: Arial, Palatino, Zapf Calligraphic, Georgia, Times New Roman, Times, Serif; font-size: .9em;  } h2 { border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: orange; font-size: 1.1em; margin-bottom: 2px; } --> </style></head><body>" + html + "</body></html>";
			this.webBrowser1.Document.ExecCommand("SelectAll", false, null);
			this.webBrowser1.Document.ExecCommand("FontName", false, "Arial");
		}

		// Token: 0x040001F8 RID: 504
		private const string html1 = "<html><head><style TYPE=\"text/css\"> <!-- body { font-family: Arial, Palatino, Zapf Calligraphic, Georgia, Times New Roman, Times, Serif; font-size: .9em;  } h2 { border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: orange; font-size: 1.1em; margin-bottom: 2px; } --> </style></head><body>";

		// Token: 0x040001F9 RID: 505
		private const string html2 = "</body></html>";

		// Token: 0x04000210 RID: 528
		private ScreenInfo screen;

		// Token: 0x04000211 RID: 529
		private DataTable variablesTable;

		// Token: 0x04000212 RID: 530
		private DataTable sessions;

		// Token: 0x04000213 RID: 531
		private DataTable dynamicScreenNonDataControlsTable;

		// Token: 0x04000214 RID: 532
		private DataTable searchCustomTable;

		// Token: 0x04000215 RID: 533
		private int searchInfoID;

		// Token: 0x04000216 RID: 534
		private DataSet comboBoxData;

		// Token: 0x04000217 RID: 535
		private UnivDataAdapter da;

		// Token: 0x04000218 RID: 536
		private UnivDataAdapter da2;

		// Token: 0x04000219 RID: 537
		private DataSet lookupTablesForControls;

		// Token: 0x0400021A RID: 538
		private ArrayList variables;

		// Token: 0x0400021B RID: 539
		private ArrayList customVariables;

		// Token: 0x0400021C RID: 540
		private object[] yearStartEnd;

		// Token: 0x0400021D RID: 541
		private string searchTitle;

		// Token: 0x0400021E RID: 542
		private int overrideDynamicControlsScreenNum;

		// Token: 0x0400021F RID: 543
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000220 RID: 544
		private TechnoProReports technoProReports;

		// Token: 0x04000221 RID: 545
		private int dbLocationCode;

		// Token: 0x04000222 RID: 546
		private string context = "";

		// Token: 0x04000223 RID: 547
		private DataRow reportDr;

		// Token: 0x04000224 RID: 548
		private Compiler existingCompiler = null;

		// Token: 0x04000225 RID: 549
		private ArrayList variablesWithCustomSql = new ArrayList();

		// Token: 0x04000226 RID: 550
		private Panel p = null;

		// Token: 0x04000227 RID: 551
		private string searchCustomCode = "";

		// Token: 0x04000228 RID: 552
		private ArrayList screenCombos = new ArrayList();

		// Token: 0x04000229 RID: 553
		private bool ignoreScreenComboSelectedIndexChanged = false;

		// Token: 0x0400022A RID: 554
		private Panel contextMenuCustomPanel = null;
	}
}
