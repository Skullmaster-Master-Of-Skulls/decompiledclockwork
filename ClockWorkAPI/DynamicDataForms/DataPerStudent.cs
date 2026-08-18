using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using AutoComboBox.MyControls;
using DynamicScreens;
using EncryptionClassLibrary;
using SettingsPermissions;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.OldUserSettings;
using UnivOleDb;

namespace ClockWorkAPI.DynamicDataForms
{
	// Token: 0x02000036 RID: 54
	public class DataPerStudent : UserControl
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0001041C File Offset: 0x0000F41C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00010454 File Offset: 0x0000F454
		private void InitializeComponent()
		{
			this.p_data = new MyPanel();
			base.SuspendLayout();
			this.p_data.AutoScroll = true;
			this.p_data.BalloonTip = null;
			this.p_data.Caption = "";
			this.p_data.DefaultActiveControl = 0;
			this.p_data.Dock = DockStyle.Fill;
			this.p_data.FirstName = null;
			this.p_data.Font = new Font("Arial Narrow", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.p_data.IsDynamicScreenContainer = false;
			this.p_data.IsTopLevelDynamicControlsContainer = false;
			this.p_data.LastName = null;
			this.p_data.Location = new Point(0, 0);
			this.p_data.Name = "p_data";
			this.p_data.Pid = 0;
			this.p_data.PrimaryClientDescription = null;
			this.p_data.PrimaryClientPid = 0;
			this.p_data.Screen = null;
			this.p_data.Size = new Size(502, 435);
			this.p_data.Student_no = null;
			this.p_data.TabIndex = 1;
			this.p_data.Tag2 = null;
			this.p_data.Tag3 = null;
			this.p_data.TagInt = -1;
			this.p_data.Tooltip = null;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.p_data);
			base.Name = "DataPerStudent";
			base.Size = new Size(502, 435);
			base.ResumeLayout(false);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00010628 File Offset: 0x0000F628
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00010640 File Offset: 0x0000F640
		public DataTable ControlsTable
		{
			get
			{
				return this.controlsTable;
			}
			set
			{
				this.controlsTable = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001064C File Offset: 0x0000F64C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00010664 File Offset: 0x0000F664
		public int[] DynamicScreenReadOnlyCids
		{
			get
			{
				return this.dynamicScreenReadOnlyCids;
			}
			set
			{
				this.dynamicScreenReadOnlyCids = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00010670 File Offset: 0x0000F670
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00010688 File Offset: 0x0000F688
		public int[] DynamicScreenInvisibleCids
		{
			get
			{
				return this.dynamicScreenInvisibleCids;
			}
			set
			{
				this.dynamicScreenInvisibleCids = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00010694 File Offset: 0x0000F694
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000106AC File Offset: 0x0000F6AC
		public bool OverridePanelColourEnabled
		{
			get
			{
				return this.overridePanelColourEnabled;
			}
			set
			{
				this.overridePanelColourEnabled = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000106B8 File Offset: 0x0000F6B8
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000106D0 File Offset: 0x0000F6D0
		public Color OverridePanelBackgroundColour
		{
			get
			{
				return this.overridePanelBackgroundColour;
			}
			set
			{
				this.overridePanelBackgroundColour = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000106DC File Offset: 0x0000F6DC
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000106F4 File Offset: 0x0000F6F4
		public Color OverridePanelForegroundColour
		{
			get
			{
				return this.overridePanelForegroundColour;
			}
			set
			{
				this.overridePanelForegroundColour = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000106FE File Offset: 0x0000F6FE
		public UnivDataAdapter Da
		{
			set
			{
				this.da = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00010708 File Offset: 0x0000F708
		public TripleDESEncryptionClass TripleDES
		{
			set
			{
				this.tripleDES = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00010712 File Offset: 0x0000F712
		public PersonBaseDTO WhoAmI
		{
			set
			{
				this.whoAmI = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0001071C File Offset: 0x0000F71C
		public ArrayList EventHandlers
		{
			set
			{
				this.eventHandlers = value;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00010728 File Offset: 0x0000F728
		public DataPerStudent()
		{
			this.dataTableNamesPostfix = "";
			this.whoAmI = null;
			this.eventHandlers = new ArrayList();
			this.lookupTablesForControls = new DataSet();
			this.comboBoxData = new DataSet();
			this.prefersFrench = false;
			this.settings = null;
			this.screen = null;
			this.controlsTable = null;
			this.formIsRendered = false;
			this.da = null;
			this.tripleDES = null;
			this.pid = 0;
			this.screenNum = 0;
			this.InitializeComponent();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000107DC File Offset: 0x0000F7DC
		public void Init(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int personId, int screenNum, bool prefersFrench, ref DataSet comboBoxData, ref DataSet lookupTablesForControls, ArrayList eventHandlers, string dataTableNamesPostfix, PersonBaseDTO whoAmI, Settings settings, Permissions permissions)
		{
			this.permissions = permissions;
			this.dataTableNamesPostfix = dataTableNamesPostfix;
			this.lookupTablesForControls = lookupTablesForControls;
			this.whoAmI = whoAmI;
			this.eventHandlers = eventHandlers;
			this.comboBoxData = comboBoxData;
			this.prefersFrench = prefersFrench;
			this.settings = settings;
			this.screen = null;
			this.controlsTable = null;
			this.formIsRendered = false;
			this.da = da;
			this.tripleDES = tripleDES;
			this.pid = personId;
			this.screenNum = screenNum;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00010860 File Offset: 0x0000F860
		public void RenderForm()
		{
			Panel panel = this.p_data;
			if (this.controlsTable == null)
			{
				this.controlsTable = DynamicScreen.LoadControls(this.da, this.screenNum);
			}
			if (this.screen == null)
			{
				this.screen = this.GetScreenInfo(this.screenNum, 0, this.p_data);
			}
			if (this.screen == null)
			{
				int columnWidth = Convert.ToInt32((double)this.p_data.Width * 0.3);
				this.screen = new ScreenInfo(this.screenNum, this.p_data, false, 0, columnWidth, 25, new Font("Arial", 9f), -1, "Unknown", false, this.overridePanelColourEnabled, this.overridePanelBackgroundColour, this.overridePanelForegroundColour);
				this.screen.UseFrench = this.prefersFrench;
			}
			DynamicScreen.TranslateControls(this.da, this.tripleDES, ref panel, this.screen, this.controlsTable, ref this.comboBoxData, null, this.lookupTablesForControls, this.eventHandlers, this.whoAmI.PersonId, this.whoAmI.GetName(), this.dynamicScreenReadOnlyCids, this.dynamicScreenInvisibleCids);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000109A0 File Offset: 0x0000F9A0
		public ScreenInfo Screen
		{
			get
			{
				return this.screen;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000109B8 File Offset: 0x0000F9B8
		public void LoadDataAndDisplay()
		{
			this.LoadDataAndDisplay(UseDefaults.useDefaults);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000109C4 File Offset: 0x0000F9C4
		public void LoadDataAndDisplay(UseDefaults useDefaults)
		{
			this.data = DynamicScreen.LoadData(this.da, this.p_data, this.screenNum, this.pid, "maininfo" + this.dataTableNamesPostfix, "otherinfo" + this.dataTableNamesPostfix, "datetimeinfo" + this.dataTableNamesPostfix, "imageinfo" + this.dataTableNamesPostfix, this.tripleDES, false, false, -1, useDefaults, true);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010A40 File Offset: 0x0000FA40
		public bool AnyChanges()
		{
			this.SaveChanges(false);
			foreach (object obj in this.data.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (dataRow.RowState != DataRowState.Unchanged)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00010B24 File Offset: 0x0000FB24
		public DataSet Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010B3C File Offset: 0x0000FB3C
		private DataTable CreateDataTable(string tableName, Type controlValueType)
		{
			return new DataTable(tableName)
			{
				Columns = 
				{
					{
						"dataid",
						typeof(int)
					},
					{
						"screennum",
						typeof(int)
					},
					{
						"personid",
						typeof(int)
					},
					{
						"controlid",
						typeof(int)
					},
					{
						"controlvalue",
						controlValueType
					}
				}
			};
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00010BD4 File Offset: 0x0000FBD4
		public Exception SaveChanges(bool writeToDatabase)
		{
			if (this.data == null)
			{
				this.data = new DataSet();
				this.data.Tables.Add(this.CreateDataTable("mainInfoTable", typeof(int)));
				this.data.Tables.Add(this.CreateDataTable("otherInfoTable", typeof(byte[])));
				this.data.Tables.Add(this.CreateDataTable("dateTimeInfoTable", typeof(DateTime)));
				this.data.Tables.Add(this.CreateDataTable("imageInfoTable", typeof(byte[])));
			}
			DynamicScreen.SaveData(ref this.data, this.p_data, this.screenNum, this.pid, "maininfo" + this.dataTableNamesPostfix, "otherinfo" + this.dataTableNamesPostfix, "datetimeinfo" + this.dataTableNamesPostfix, this.tripleDES, -1, false);
			if (writeToDatabase)
			{
				int num = 0;
				DataTable t = this.data.Tables["mainInfoTable"];
				Exception ex;
				num += DynamicData.SaveDataPS(this.da, t, "maininfo" + this.dataTableNamesPostfix, this.screenNum, this.pid, this.whoAmI.PersonId, out ex);
				if (ex != null)
				{
					return ex;
				}
				t = this.data.Tables["otherInfoTable"];
				num += DynamicData.SaveDataPS(this.da, t, "otherInfo" + this.dataTableNamesPostfix, this.screenNum, this.pid, this.whoAmI.PersonId, out ex);
				if (ex != null)
				{
					return ex;
				}
				t = this.data.Tables["dateTimeInfoTable"];
				num += DynamicData.SaveDataPS(this.da, t, "dateTimeInfo" + this.dataTableNamesPostfix, this.screenNum, this.pid, this.whoAmI.PersonId, out ex);
				if (ex != null)
				{
					return ex;
				}
				t = this.data.Tables["imageInfoTable"];
				num += DynamicData.SaveDataPS(this.da, t, "imageInfo" + this.dataTableNamesPostfix, this.screenNum, this.pid, this.whoAmI.PersonId, out ex);
				if (ex != null)
				{
					return ex;
				}
				if (num > 0)
				{
					DynamicData.LogDataChange(this.da, false, this.screenNum, this.pid, this.whoAmI.PersonId);
				}
			}
			return null;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00010EB4 File Offset: 0x0000FEB4
		public void FillInData(Dictionary<string, string> codes)
		{
			this.FillInData(this.p_data, codes);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00010EC8 File Offset: 0x0000FEC8
		private void FillInData(Control parent, Dictionary<string, string> codes)
		{
			string text = null;
			if (parent.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)parent.Tag;
				if (dataRow.Table.Columns.Contains("controlcaption"))
				{
					text = dataRow["controlcaption"].ToString().ToLower();
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (codes.ContainsKey(text))
				{
					string text2 = codes[text];
					if (!string.IsNullOrEmpty(text2))
					{
						if (parent is TextBox)
						{
							((TextBox)parent).Text = text2;
						}
						else if (parent is CheckBox)
						{
							((CheckBox)parent).Checked = true;
						}
						else if (parent is MyRichText)
						{
							MyRichText myRichText = (MyRichText)parent;
							myRichText.PlainText = text2;
						}
						else if (parent is AutoComboBox)
						{
							AutoComboBox autoComboBox = (AutoComboBox)parent;
							autoComboBox.Text = text2;
						}
					}
				}
			}
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				this.FillInData(parent2, codes);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001106C File Offset: 0x0001006C
		private ScreenInfo GetScreenInfo(int screenNum, int typeCode, Panel p_data)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.ScreensExtended);
			bool flag2 = this.da.DoesColumnExist("screens", "studentnumbercaption");
			this.da.SelectCommand.CommandText = "SELECT screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid";
			if (flag)
			{
				UnivCommand selectCommand = this.da.SelectCommand;
				selectCommand.CommandText += ",fontname,fontsize,groupids,iswebscreen,longdescription,controlIdToActivate";
			}
			else
			{
				UnivCommand selectCommand2 = this.da.SelectCommand;
				selectCommand2.CommandText += ",'' AS fontname,0 AS fontsize,'' AS groupids,0 AS iswebscreen,'' AS longdescription,0 AS controlIdToActivate";
			}
			if (flag2)
			{
				UnivCommand selectCommand3 = this.da.SelectCommand;
				selectCommand3.CommandText += ",studentnumbercaption,studentnumberautogeneraterule,studentnamehidden";
			}
			else
			{
				UnivCommand selectCommand4 = this.da.SelectCommand;
				selectCommand4.CommandText += ",'' AS studentnumbercaption,'' AS studentnumberautogeneraterule,0 AS studentnamehidden";
			}
			UnivCommand selectCommand5 = this.da.SelectCommand;
			selectCommand5.CommandText += " FROM screens WHERE screennum=@screennum";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@screennum", screenNum);
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (int)dataRow[0];
					int num2 = (int)dataRow[2];
					if (num == screenNum && (typeCode == 0 || num2 == typeCode))
					{
						ScreenInfo screenInfo = this.GetScreenInfo(dataRow, p_data, this.prefersFrench, this.settings);
						this.AdjustScreenAccessible(ref screenInfo, this.settings);
						return screenInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000112A0 File Offset: 0x000102A0
		private ScreenInfo GetScreenInfo(DataRow dr, Panel p_data, bool prefersFrench, Settings settings)
		{
			bool applyColWidthToCurrentPanel = true;
			int height = p_data.ClientSize.Height;
			ScreenInfo screenInfo = ScreenInfo.GetScreenInfo(dr, p_data, applyColWidthToCurrentPanel, p_data.Width, p_data.Height, this.overridePanelColourEnabled, this.overridePanelBackgroundColour, this.overridePanelForegroundColour);
			screenInfo.UseFrench = prefersFrench;
			this.AdjustScreenAccessible(ref screenInfo, settings);
			return screenInfo;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00011300 File Offset: 0x00010300
		public void AdjustScreenAccessible(ref ScreenInfo screen, Settings settings)
		{
			int setting = OldUserSettingClientManager.CurrentInstance.GetSetting(280, -1);
			if (setting >= 0 && setting != Convert.ToInt32(screen.font.Size))
			{
				int num = screen.columnWidth;
				if (setting != 10)
				{
					int num2 = setting - 10;
					double num3 = (double)(num2 / 10);
					int num4 = Convert.ToInt32(Convert.ToDouble(num) * num3) + num;
					if (num4 > 0 && num4 < 2147483647)
					{
						num = num4;
					}
				}
				screen.font = new Font(screen.font.FontFamily, (float)setting);
				screen.columnWidth = num;
			}
		}

		// Token: 0x04000165 RID: 357
		private IContainer components = null;

		// Token: 0x04000166 RID: 358
		private MyPanel p_data;

		// Token: 0x04000167 RID: 359
		private DataSet data;

		// Token: 0x04000168 RID: 360
		private UnivDataAdapter da;

		// Token: 0x04000169 RID: 361
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400016A RID: 362
		private int pid;

		// Token: 0x0400016B RID: 363
		private int screenNum;

		// Token: 0x0400016C RID: 364
		private bool formIsRendered;

		// Token: 0x0400016D RID: 365
		private DataTable controlsTable;

		// Token: 0x0400016E RID: 366
		private int[] dynamicScreenReadOnlyCids;

		// Token: 0x0400016F RID: 367
		private int[] dynamicScreenInvisibleCids;

		// Token: 0x04000170 RID: 368
		private ScreenInfo screen;

		// Token: 0x04000171 RID: 369
		private Settings settings;

		// Token: 0x04000172 RID: 370
		private Permissions permissions;

		// Token: 0x04000173 RID: 371
		private bool prefersFrench;

		// Token: 0x04000174 RID: 372
		private bool overridePanelColourEnabled = false;

		// Token: 0x04000175 RID: 373
		private Color overridePanelBackgroundColour = Color.Transparent;

		// Token: 0x04000176 RID: 374
		private Color overridePanelForegroundColour = Color.Transparent;

		// Token: 0x04000177 RID: 375
		private DataSet comboBoxData;

		// Token: 0x04000178 RID: 376
		private DataSet lookupTablesForControls;

		// Token: 0x04000179 RID: 377
		private ArrayList eventHandlers;

		// Token: 0x0400017A RID: 378
		private PersonBaseDTO whoAmI;

		// Token: 0x0400017B RID: 379
		private string dataTableNamesPostfix;
	}
}
