using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using DevComponents.DotNetBar;
using DynamicScreens;
using EncryptionClassLibrary;
using SettingsPermissions;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.OldUserSettings;
using UnivOleDb;

namespace ClockWorkAPI.DynamicDataForms
{
	// Token: 0x02000060 RID: 96
	public class DataPerApp : UserControl
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x00019FBC File Offset: 0x00018FBC
		public DataPerApp()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00019FF8 File Offset: 0x00018FF8
		public void Init(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int perAppScreenNum, PersonBaseDTO student, AppointmentDTO app, int whoAmIId, Settings settings, Permissions permissions, bool prefersFrench)
		{
			this.da = da;
			this.tripleDES = tripleDES;
			this.screenNum = perAppScreenNum;
			this.student = student;
			this.app = app;
			this.prefersFrench = prefersFrench;
			this.whoAmIId = whoAmIId;
			this.settings = settings;
			this.permissions = permissions;
			this.Init();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001A054 File Offset: 0x00019054
		private void TryToSaveCurrentData()
		{
			if (this.data != null && this.data.Tables.Count > 0)
			{
				this.Save();
				if (this.app != null && this.app.AppointmentId < 1)
				{
					if (this.dataToWrite.ContainsKey(this.app.AppointmentId))
					{
						this.dataToWrite.Remove(this.app.AppointmentId);
					}
					this.dataToWrite.Add(this.app.AppointmentId, this.data);
				}
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000542 RID: 1346 RVA: 0x0001A108 File Offset: 0x00019108
		// (remove) Token: 0x06000543 RID: 1347 RVA: 0x0001A144 File Offset: 0x00019144
		public event EventHandler Cancelled;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000544 RID: 1348 RVA: 0x0001A180 File Offset: 0x00019180
		// (remove) Token: 0x06000545 RID: 1349 RVA: 0x0001A1BC File Offset: 0x000191BC
		public event EventHandler Saved;

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001A1F8 File Offset: 0x000191F8
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001A210 File Offset: 0x00019210
		public AppointmentDTO Appointment
		{
			get
			{
				return this.app;
			}
			set
			{
				this.app = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001A21C File Offset: 0x0001921C
		public PersonBaseDTO Student
		{
			get
			{
				return this.student;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001A234 File Offset: 0x00019234
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001A24C File Offset: 0x0001924C
		public DataSet Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001A258 File Offset: 0x00019258
		public Panel P_data
		{
			get
			{
				return this.p_data;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001A270 File Offset: 0x00019270
		private void Init()
		{
			base.SuspendLayout();
			if (DataPerApp.comboBoxData == null)
			{
				DataPerApp.comboBoxData = new DataSet();
			}
			if (DataPerApp.lookupTablesForControls == null)
			{
				DataPerApp.lookupTablesForControls = new DataSet();
			}
			this.allowedToChangeAnything = this.permissions.IsPersonAllowed(3, this.screenNum);
			if (!this.allowedToChangeAnything)
			{
				this.lbl_noPermissionsToChangeMessage.Text = "You are not allowed to modify any information on this form because your permissions do not allow it.";
			}
			this.onlyAllowCounsellorsInTheAppToEnterTheAssessment = OldUserSettingClientManager.CurrentInstance.IntToBool(OldUserSettingClientManager.CurrentInstance.GetSetting(478));
			this.onlyAllowCounsellorsInTheAppToSeeTheAssessment = OldUserSettingClientManager.CurrentInstance.IntToBool(OldUserSettingClientManager.CurrentInstance.GetSetting(99653));
			this.onlyAllowCounsellorsInTheAppToSeeTheAssessmentTextBoxesOnly = OldUserSettingClientManager.CurrentInstance.IntToBool(OldUserSettingClientManager.CurrentInstance.GetSetting(99654));
			if (this.p_data.Controls.Count < 1)
			{
				DataTable controlListTable = DynamicScreen.LoadControls(this.da, this.screenNum);
				this.screen = this.GetScreenInfo(this.screenNum, 1, this.p_data);
				if (this.screen == null)
				{
					int columnWidth = Convert.ToInt32((double)this.p_data.Width * 0.3);
					this.screen = new ScreenInfo(this.screenNum, this.p_data, false, 0, columnWidth, 25, new Font("Arial", 9f), -1, "Unknown", false, this.Forms_OverridePanelColourEnabled, this.Forms_OverridePanelBackgroundColour, this.Forms_OverridePanelForegroundColour);
					this.screen.UseFrench = this.prefersFrench;
				}
				this.controlIdToActivate = this.screen.ControlIdToActivate;
				Panel panel = this.p_data;
				ArrayList eventHandlers = new ArrayList();
				DynamicScreen.TranslateControls(this.da, this.tripleDES, ref panel, this.screen, controlListTable, ref DataPerApp.comboBoxData, null, DataPerApp.lookupTablesForControls, eventHandlers, this.whoAmIId, this.student.GetName(), this.DynamicScreenReadOnlyCids, this.DynamicScreenInvisibleCids);
			}
			else
			{
				this.ClearData();
			}
			this.LoadData();
			if (this.screen.PerStudentScreenNum > 0)
			{
				this.Init_ps(this.screen.PerStudentScreenNum, this.screen.PerStudentScreenNum_Height);
			}
			base.ResumeLayout(true);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001A4D1 File Offset: 0x000194D1
		private void ClearData()
		{
			DynamicScreen.ResetScreenToDefaults(this.p_data, true);
			DynamicScreen.ResetScreenToDefaults(this.p_data_ps, true);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001A4EE File Offset: 0x000194EE
		public void ClearPerAppData()
		{
			DynamicScreen.ResetScreenToDefaults(this.p_data, true);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001A500 File Offset: 0x00019500
		private void LoadData()
		{
			if (this.student.PersonId >= 0 && this.app != null && this.app.AppointmentId > 0)
			{
				this.data = DynamicScreen.LoadData(this.da, this.p_data, this.screenNum, this.student.PersonId, "maininfopa", "otherinfopa", "datetimeinfopa", "imageinfopa", this.tripleDES, true, false, this.app.AppointmentId, UseDefaults.dontUseDefaults, true, this.OverrideDefaultControlValues(this.app));
			}
			else if (this.app != null)
			{
				if (this.dataToWrite.ContainsKey(this.app.AppointmentId))
				{
					this.data = this.dataToWrite[this.app.AppointmentId];
					foreach (object obj in this.data.Tables)
					{
						DataTable t = (DataTable)obj;
						DynamicScreen.SetControlValues(this.p_data, t, this.tripleDES, this.app.AppointmentId, true, this.da, this.OverrideDefaultControlValues(this.app));
					}
				}
				else if (this.student.PersonId <= 0)
				{
					this.data = DynamicScreen.LoadData(this.da, this.p_data, this.screenNum, -1, "maininfopa", "otherinfopa", "datetimeinfopa", "imageinfopa", this.tripleDES, true, false, this.app.AppointmentId, UseDefaults.dontUseDefaults, true, this.OverrideDefaultControlValues(this.app));
				}
				else
				{
					this.data = DynamicScreen.LoadData(this.da, this.p_data, this.screenNum, this.student.PersonId, "maininfopa", "otherinfopa", "datetimeinfopa", "imageinfopa", this.tripleDES, true, false, this.app.AppointmentId, UseDefaults.dontUseDefaults, true, this.OverrideDefaultControlValues(this.app));
				}
			}
			else
			{
				DynamicScreen.ResetScreenToDefaults(this.p_data, true);
				this.data = DynamicScreen.LoadData(this.da, this.p_data, this.screenNum, -1, "maininfopa", "otherinfopa", "datetimeinfopa", "imageinfopa", this.tripleDES, true, false, 0, UseDefaults.dontUseDefaults, false, this.OverrideDefaultControlValues(this.app));
			}
			if (this.data != null && this.app != null)
			{
				foreach (object obj2 in this.data.Tables)
				{
					DataTable dataTable = (DataTable)obj2;
					List<DataRow> list = new List<DataRow>();
					foreach (object obj3 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj3;
						if (dataRow["appointmentid"] != DBNull.Value)
						{
							int num = (int)dataRow["appointmentid"];
							if (num != this.app.AppointmentId)
							{
								list.Add(dataRow);
							}
						}
					}
					foreach (DataRow dataRow in list)
					{
						DataRow dataRow;
						dataTable.Rows.Remove(dataRow);
					}
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001A95C File Offset: 0x0001995C
		public void SetControlValues(DataSet data)
		{
			foreach (object obj in data.Tables)
			{
				DataTable t = (DataTable)obj;
				DynamicScreen.SetControlValues(this.p_data, t, this.tripleDES, this.app.AppointmentId, true, this.da, this.OverrideDefaultControlValues(this.app));
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001A9EC File Offset: 0x000199EC
		public void SetControlValues(DataSet data, AppointmentDTO app, int pid)
		{
			foreach (object obj in data.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						int num = (int)dataRow["personid"];
						if (num != pid)
						{
							dataRow["personid"] = pid;
						}
					}
					else
					{
						dataRow.RejectChanges();
						int num = (int)dataRow["personid"];
						if (num != pid)
						{
							dataRow["personid"] = pid;
						}
						dataRow.Delete();
					}
				}
				DynamicScreen.SetControlValues(this.p_data, dataTable, this.tripleDES, app.AppointmentId, true, this.da, this.OverrideDefaultControlValues(app));
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001AB78 File Offset: 0x00019B78
		public void SetOverrideControlValues(AppointmentDTO app)
		{
			Dictionary<string, string> overrideDefaultControlValues = this.OverrideDefaultControlValues(app);
			DynamicScreen.SetOverrideControlValues(this.p_data, overrideDefaultControlValues);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001AB9B File Offset: 0x00019B9B
		public void RemoveControls()
		{
			this.RemoveControls(this.p_data, this.p_data);
			this.RemoveControls(this.p_data_ps, this.p_data_ps);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001ABC4 File Offset: 0x00019BC4
		private void RemoveControls(Control parent)
		{
			this.RemoveControls(parent, parent);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001ABD0 File Offset: 0x00019BD0
		private void RemoveControls(Control topParent, Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				this.RemoveControls(topParent, parent2);
			}
			if (parent != topParent && parent.Parent != null)
			{
				parent.Parent.Controls.Remove(parent);
				parent.Dispose();
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001AC68 File Offset: 0x00019C68
		private bool Forms_OverridePanelColourEnabled
		{
			get
			{
				return OldUserSettingClientManager.CurrentInstance.IntToBool(OldUserSettingClientManager.CurrentInstance.GetSetting(99612));
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001AC94 File Offset: 0x00019C94
		private Color Forms_OverridePanelBackgroundColour
		{
			get
			{
				return Color.FromArgb(OldUserSettingClientManager.CurrentInstance.GetSetting(99610));
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001ACBC File Offset: 0x00019CBC
		private Color Forms_OverridePanelForegroundColour
		{
			get
			{
				return Color.FromArgb(OldUserSettingClientManager.CurrentInstance.GetSetting(99611));
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001ACE4 File Offset: 0x00019CE4
		private ScreenInfo GetScreenInfo(int screenNum, int typeCode, Panel p_data)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, DatabaseVersionManager.ClockWorkFeature.ScreensExtended);
			bool flag2 = this.da.DoesColumnExist("screens", "studentnumbercaption");
			if (DataPerApp.screens == null)
			{
				DataPerApp.screens = new List<DataRow>();
			}
			foreach (DataRow dataRow in DataPerApp.screens)
			{
				DataRow dataRow;
				int num = (int)dataRow[0];
				int num2 = (int)dataRow[2];
				if (num == screenNum && (typeCode == 0 || num2 == typeCode))
				{
					ScreenInfo screenInfo = this.GetScreenInfo(dataRow, p_data, this.prefersFrench, this.settings);
					this.AdjustScreenAccessible(ref screenInfo, this.settings);
					return screenInfo;
				}
			}
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
						DataPerApp.screens.Add(dataRow);
						ScreenInfo screenInfo2 = this.GetScreenInfo(dataRow, p_data, this.prefersFrench, this.settings);
						this.AdjustScreenAccessible(ref screenInfo2, this.settings);
						return screenInfo2;
					}
				}
			}
			return null;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001AFEC File Offset: 0x00019FEC
		private ScreenInfo GetScreenInfo(DataRow dr, Panel p_data, bool prefersFrench, Settings settings)
		{
			bool applyColWidthToCurrentPanel = true;
			int height = p_data.ClientSize.Height;
			ScreenInfo screenInfo = ScreenInfo.GetScreenInfo(dr, p_data, applyColWidthToCurrentPanel, p_data.Width, p_data.Height, this.Forms_OverridePanelColourEnabled, this.Forms_OverridePanelBackgroundColour, this.Forms_OverridePanelForegroundColour);
			screenInfo.UseFrench = prefersFrench;
			this.AdjustScreenAccessible(ref screenInfo, settings);
			return screenInfo;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001B04C File Offset: 0x0001A04C
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

		// Token: 0x0600055C RID: 1372 RVA: 0x0001B104 File Offset: 0x0001A104
		private void Init_ps(int screenNum, int height)
		{
			this.p_data_ps.Visible = true;
			this.splitter_ps.Visible = true;
			if (height > 0)
			{
				this.p_data_ps.Height = height;
			}
			ScreenInfo screenInfo = this.GetScreenInfo(screenNum, 0, this.p_data_ps);
			if (screenInfo == null)
			{
				int columnWidth = Convert.ToInt32((double)this.p_data.Width * 0.3);
				screenInfo = new ScreenInfo(screenNum, this.p_data_ps, false, 0, columnWidth, 25, new Font("Arial", 9f), -1, "Unknown", false, this.Forms_OverridePanelColourEnabled, this.Forms_OverridePanelBackgroundColour, this.Forms_OverridePanelForegroundColour);
				screenInfo.UseFrench = this.prefersFrench;
			}
			this.p_data_ps.Tooltip = this.toolTip1;
			this.p_data_ps.IsDynamicScreenContainer = true;
			this.psControls = DynamicScreen.LoadControls(this.da, screenNum);
			Panel panel = this.p_data_ps;
			if (panel.Controls.Count < 1)
			{
				ArrayList eventHandlers = new ArrayList();
				DynamicScreen.TranslateControls(this.da, this.tripleDES, ref panel, screenInfo, this.psControls, ref DataPerApp.comboBoxData, null, DataPerApp.lookupTablesForControls, eventHandlers, this.whoAmIId, "", this.DynamicScreenReadOnlyCids, this.DynamicScreenInvisibleCids);
			}
			this.ReloadStudentPsData(this.student);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001B26C File Offset: 0x0001A26C
		private bool AnyPSDataLoaded(int pid)
		{
			bool result;
			if (this.psData == null)
			{
				result = false;
			}
			else
			{
				foreach (object obj in this.psData.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						if (dataRow.RowState != DataRowState.Deleted && dataRow["personid"] != DBNull.Value && (int)dataRow["personid"] == pid)
						{
							return true;
						}
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001B38C File Offset: 0x0001A38C
		public void ReloadStudentPsData(PersonBaseDTO student)
		{
			int perStudentScreenNum = this.screen.PerStudentScreenNum;
			this.student = student;
			if (this.student == null || student == null || !this.AnyPSDataLoaded(student.PersonId))
			{
				if (this.psControls == null)
				{
					this.psControls = DynamicScreen.LoadControls(this.da, perStudentScreenNum);
				}
				Panel panel = this.p_data_ps;
				ArrayList arrayList = new ArrayList();
				this.psScreenInfo = this.screen;
				this.psData = new DataSet();
				try
				{
					if (student != null && student.PersonId >= 0)
					{
						string text = this.screen.Args["usedefaults"];
						if (text != null)
						{
							if (text.CompareTo("1") == 0)
							{
							}
						}
						this.psData = DynamicScreen.LoadData(this.da, panel, perStudentScreenNum, student.PersonId, "maininfops", "otherinfops", "datetimeinfops", "imageinfops", this.tripleDES, false, false, -1, UseDefaults.dontUseDefaults, true);
					}
					else
					{
						this.psData = DynamicScreen.LoadData(this.da, panel, perStudentScreenNum, -1, "maininfops", "otherinfops", "datetimeinfops", "imageinfops", this.tripleDES, false, false, -1, UseDefaults.useDefaults, true);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001B514 File Offset: 0x0001A514
		public int[] DynamicScreenReadOnlyCids
		{
			get
			{
				if (this.dynamicScreenReadOnlyCids == null)
				{
					string settingString = OldUserSettingClientManager.CurrentInstance.GetSettingString(99647, "");
					int[] array = OldUserSettingClientManager.CurrentInstance.GetSettingString_IntArray(99511, "");
					if (settingString != null && settingString.Length > 0)
					{
						this.da.SelectCommand.CommandText = "SELECT controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@sns,','))";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@sns", settingString);
						DataTable dataTable = new DataTable();
						this.da.Fill(dataTable);
						int[] array2;
						int num;
						if (array != null)
						{
							array2 = new int[array.Length + dataTable.Rows.Count];
							array.CopyTo(array2, 0);
							num = array.Length;
						}
						else
						{
							array2 = new int[dataTable.Rows.Count];
							num = 0;
						}
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							int num2 = (int)dataRow[0];
							array2[num++] = num2;
						}
						array = array2;
					}
					int[] array3 = OldUserSettingClientManager.CurrentInstance.GetSettingString_IntArray(99620, "");
					if (array == null)
					{
						array = new int[0];
					}
					if (array3 == null)
					{
						array3 = new int[0];
					}
					List<int> list = new List<int>();
					foreach (int num3 in array)
					{
						if (Array.IndexOf<int>(array3, num3) < 0)
						{
							list.Add(num3);
						}
					}
					this.dynamicScreenReadOnlyCids = list.ToArray();
				}
				return this.dynamicScreenReadOnlyCids;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001B740 File Offset: 0x0001A740
		public int[] DynamicScreenInvisibleCids
		{
			get
			{
				if (this.dynamicScreenInvisibleCids == null)
				{
					string settingString = OldUserSettingClientManager.CurrentInstance.GetSettingString(99648, "");
					int[] array = OldUserSettingClientManager.CurrentInstance.GetSettingString_IntArray(99510, "");
					int[] array2 = OldUserSettingClientManager.CurrentInstance.GetSettingString_IntArray(99621, "");
					if (settingString != null && settingString.Length > 0)
					{
						this.da.SelectCommand.CommandText = "SELECT controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@sns,','))";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@sns", settingString);
						DataTable dataTable = new DataTable();
						this.da.Fill(dataTable);
						int[] array3;
						int num;
						if (array != null)
						{
							array3 = new int[array.Length + dataTable.Rows.Count];
							array.CopyTo(array3, 0);
							num = array.Length;
						}
						else
						{
							array3 = new int[dataTable.Rows.Count];
							num = 0;
						}
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							int num2 = (int)dataRow[0];
							array3[num++] = num2;
						}
						array = array3;
					}
					if (array == null)
					{
						array = new int[0];
					}
					if (array2 == null)
					{
						array2 = new int[0];
					}
					List<int> list = new List<int>();
					foreach (int num3 in array)
					{
						if (Array.IndexOf<int>(array2, num3) < 0)
						{
							list.Add(num3);
						}
					}
					this.dynamicScreenInvisibleCids = list.ToArray();
				}
				return this.dynamicScreenInvisibleCids;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001B970 File Offset: 0x0001A970
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel and discard any changes?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				this.FireCancelled();
				this.LoadData();
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001B9AC File Offset: 0x0001A9AC
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001B9B8 File Offset: 0x0001A9B8
		public void Save()
		{
			this.RememberData();
			if (this.app != null && this.app.AppointmentId > 0)
			{
				this.SaveData();
			}
			this.FireSaved();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001BA00 File Offset: 0x0001AA00
		private void FireSaved()
		{
			if (this.Saved != null)
			{
				this.Saved(this, new EventArgs());
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001BA30 File Offset: 0x0001AA30
		private void FireCancelled()
		{
			if (this.Cancelled != null)
			{
				this.Cancelled(this, new EventArgs());
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001BAA0 File Offset: 0x0001AAA0
		private Dictionary<string, string> OverrideDefaultControlValues(AppointmentDTO app)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("date~~date", "");
			dictionary.Add("time~~time", "");
			dictionary.Add("room~~room", "");
			dictionary.Add("hours-~~hours-", "");
			dictionary.Add("hours+~~hours+", "");
			dictionary.Add("hours~~hours", "");
			dictionary.Add("day_of_week~~day_of_week", "");
			double num = (app == null) ? 0.0 : Convert.ToDouble(app.GetDurationInMinutes());
			double num2 = Math.Round(num / 60.0, 2);
			double num3 = (num % 60.0 == 0.0) ? num2 : (num2 + 1.0);
			if (app != null)
			{
				dictionary["date~~date"] = app.StartDateTime.ToString("MMMM d, yyyy");
				dictionary["day_of_week~~day_of_week"] = app.StartDateTime.DayOfWeek.ToString();
				dictionary["time~~time"] = app.StartDateTime.ToString("h:mm tt") + " to " + app.EndDateTime.ToString("h:mm tt");
				List<AttendeeDTO> list = app.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Rooms);
				string value = string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => att.Person.FirstName).ToArray());
				dictionary["room~~room"] = value;
				dictionary["hours-~~hours-"] = num2.ToString();
				dictionary["hours+~~hours+"] = num3.ToString();
				dictionary["hours~~hours"] = num2.ToString();
			}
			return dictionary;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001BCC0 File Offset: 0x0001ACC0
		private void RememberData()
		{
			DynamicScreen.SaveData(ref this.data, this.p_data, this.screenNum, this.student.PersonId, "maininfopa", "otherinfopa", "datetimeinfopa", this.tripleDES, this.app.AppointmentId, true, this.OverrideDefaultControlValues(this.app));
			if (this.psData != null && this.psScreenInfo != null && this.psScreenInfo.screenNum > 0)
			{
				DynamicScreen.SaveData(ref this.psData, this.p_data_ps, this.psScreenInfo.screenNum, this.student.PersonId, "mainInfoPS", "otherInfoPS", "datetimeinfops", this.tripleDES, -1);
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001BD88 File Offset: 0x0001AD88
		public void RememberPerAppData(AppointmentDTO app, int pid)
		{
			DynamicScreen.SaveData(ref this.data, this.p_data, this.screenNum, pid, "maininfopa", "otherinfopa", "datetimeinfopa", this.tripleDES, app.AppointmentId, true, this.OverrideDefaultControlValues(app));
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001BDD4 File Offset: 0x0001ADD4
		public void RememberPerAppData(int appId, int pid)
		{
			DynamicScreen.SaveData(ref this.data, this.p_data, this.screenNum, pid, "maininfopa", "otherinfopa", "datetimeinfopa", this.tripleDES, appId, true, this.OverrideDefaultControlValues(null));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001BE1C File Offset: 0x0001AE1C
		private void SaveData()
		{
			if (this.psScreenInfo != null && this.psScreenInfo.screenNum > 0)
			{
				DataPerApp.SaveData(this.data, this.psData, this.da, this.tripleDES, this.screenNum, this.student.PersonId, this.psScreenInfo.screenNum);
			}
			else
			{
				DataPerApp.SaveData(this.data, this.da, this.tripleDES, this.screenNum, this.student.PersonId);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001BEB3 File Offset: 0x0001AEB3
		public static void SaveData(DataSet data, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int pid)
		{
			DataPerApp.SaveData(data, null, da, tripleDES, screenNum, pid, 0);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001BEC4 File Offset: 0x0001AEC4
		public static void SaveData(DataSet data, DataSet psData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int pid, int psScreenNum)
		{
			ArrayList arrayList = new ArrayList();
			DataTable t = data.Tables["mainInfoTable"];
			DataPerApp.SaveData(t, "mainInfoPA", ref arrayList, da, tripleDES, screenNum, pid);
			t = data.Tables["otherInfoTable"];
			DataPerApp.SaveData(t, "otherInfoPA", ref arrayList, da, tripleDES, screenNum, pid);
			t = data.Tables["dateTimeInfoTable"];
			DataPerApp.SaveData(t, "dateTimeInfoPA", ref arrayList, da, tripleDES, screenNum, pid);
			t = data.Tables["imageInfoTable"];
			DataPerApp.SaveData(t, "imageInfoPA", ref arrayList, da, tripleDES, screenNum, pid);
			if (psData != null)
			{
				DataPerApp.SavePSData(da, tripleDES, pid, psData, psScreenNum);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001BF84 File Offset: 0x0001AF84
		public static void FixAppIdPid(DataSet data, int appId, int pid)
		{
			foreach (object obj in data.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					bool flag = dataRow.RowState == DataRowState.Deleted;
					if (flag)
					{
						dataRow.RejectChanges();
						flag = true;
					}
					int num = (dataRow["appointmentid"] == DBNull.Value) ? 0 : ((int)dataRow["appointmentid"]);
					int num2 = (dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"]);
					if (num != appId)
					{
						dataRow["appointmentid"] = appId;
					}
					if (num2 != pid)
					{
						dataRow["personid"] = pid;
					}
					if (flag)
					{
						dataRow.Delete();
					}
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001C124 File Offset: 0x0001B124
		private static void SaveData(DataTable t, string tableName, ref ArrayList changedAppIds, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int pid)
		{
			ClockWorkAPI.Student.SaveData(da, tripleDES, screenNum, pid, t, tableName, ref changedAppIds);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001C138 File Offset: 0x0001B138
		private static void SavePSData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int pid, DataSet psData, int psScreenNum)
		{
			DataTable t = psData.Tables["mainInfoTable"];
			DataPerApp.SavePsData(t, "mainInfoPS", da, tripleDES, psScreenNum, pid);
			t = psData.Tables["otherInfoTable"];
			DataPerApp.SavePsData(t, "otherInfoPS", da, tripleDES, psScreenNum, pid);
			t = psData.Tables["dateTimeInfoTable"];
			DataPerApp.SavePsData(t, "dateTimeInfoPS", da, tripleDES, psScreenNum, pid);
			t = psData.Tables["imageInfoTable"];
			DataPerApp.SavePsData(t, "imageInfoPS", da, tripleDES, psScreenNum, pid);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001C1D0 File Offset: 0x0001B1D0
		private static Exception SavePsData(DataTable t, string tableName, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int pid)
		{
			Exception result;
			DynamicData.SaveDataPS(da, t, tableName, screenNum, pid, 0, out result);
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001C1F4 File Offset: 0x0001B1F4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001C22C File Offset: 0x0001B22C
		private void InitializeComponent()
		{
			this.components = new Container();
			this.p_data = new MyPanel();
			this.p_data_ps = new MyPanel();
			this.splitter_ps = new ExpandableSplitter();
			this.lbl_noPermissionsToChangeMessage = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.btn_cancel = new Button();
			this.label1 = new Label();
			this.btn_save = new Button();
			this.p_saveCancel = new Panel();
			this.p_saveCancel.SuspendLayout();
			base.SuspendLayout();
			this.p_data.AutoScroll = true;
			this.p_data.BalloonTip = null;
			this.p_data.BorderStyle = BorderStyle.Fixed3D;
			this.p_data.Caption = "";
			this.p_data.DefaultActiveControl = 0;
			this.p_data.Dock = DockStyle.Fill;
			this.p_data.FirstName = null;
			this.p_data.IsDynamicScreenContainer = false;
			this.p_data.IsTopLevelDynamicControlsContainer = false;
			this.p_data.LastName = null;
			this.p_data.Location = new Point(0, 124);
			this.p_data.Margin = new Padding(3, 4, 3, 4);
			this.p_data.Name = "p_data";
			this.p_data.Pid = 0;
			this.p_data.PrimaryClientDescription = null;
			this.p_data.PrimaryClientPid = 0;
			this.p_data.Screen = null;
			this.p_data.Size = new Size(668, 291);
			this.p_data.Student_no = null;
			this.p_data.TabIndex = 0;
			this.p_data.Tag2 = null;
			this.p_data.Tag3 = null;
			this.p_data.TagInt = -1;
			this.p_data.Tooltip = null;
			this.p_data_ps.AutoScroll = true;
			this.p_data_ps.BackColor = SystemColors.Control;
			this.p_data_ps.BalloonTip = null;
			this.p_data_ps.BorderStyle = BorderStyle.Fixed3D;
			this.p_data_ps.Caption = "";
			this.p_data_ps.DefaultActiveControl = 0;
			this.p_data_ps.Dock = DockStyle.Top;
			this.p_data_ps.FirstName = null;
			this.p_data_ps.Font = new Font("Arial", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.p_data_ps.IsDynamicScreenContainer = false;
			this.p_data_ps.IsTopLevelDynamicControlsContainer = true;
			this.p_data_ps.LastName = null;
			this.p_data_ps.Location = new Point(0, 0);
			this.p_data_ps.Name = "p_data_ps";
			this.p_data_ps.Pid = 0;
			this.p_data_ps.PrimaryClientDescription = null;
			this.p_data_ps.PrimaryClientPid = 0;
			this.p_data_ps.Screen = null;
			this.p_data_ps.Size = new Size(668, 111);
			this.p_data_ps.Student_no = null;
			this.p_data_ps.TabIndex = 16;
			this.p_data_ps.Tag2 = null;
			this.p_data_ps.Tag3 = null;
			this.p_data_ps.TagInt = -1;
			this.p_data_ps.Tooltip = null;
			this.p_data_ps.Visible = false;
			this.splitter_ps.BackColor2 = Color.FromArgb(0, 45, 150);
			this.splitter_ps.BackColor2SchemePart = 53;
			this.splitter_ps.BackColorSchemePart = 51;
			this.splitter_ps.BorderStyle = BorderStyle.Fixed3D;
			this.splitter_ps.Dock = DockStyle.Top;
			this.splitter_ps.ExpandFillColor = Color.FromArgb(0, 45, 150);
			this.splitter_ps.ExpandFillColorSchemePart = 53;
			this.splitter_ps.ExpandLineColor = SystemColors.ControlText;
			this.splitter_ps.ExpandLineColorSchemePart = 40;
			this.splitter_ps.GripDarkColor = SystemColors.ControlText;
			this.splitter_ps.GripDarkColorSchemePart = 40;
			this.splitter_ps.GripLightColor = Color.FromArgb(223, 237, 254);
			this.splitter_ps.GripLightColorSchemePart = 0;
			this.splitter_ps.HotBackColor = Color.FromArgb(254, 142, 75);
			this.splitter_ps.HotBackColor2 = Color.FromArgb(255, 207, 139);
			this.splitter_ps.HotBackColor2SchemePart = 35;
			this.splitter_ps.HotBackColorSchemePart = 34;
			this.splitter_ps.HotExpandFillColor = Color.FromArgb(0, 45, 150);
			this.splitter_ps.HotExpandFillColorSchemePart = 53;
			this.splitter_ps.HotExpandLineColor = SystemColors.ControlText;
			this.splitter_ps.HotExpandLineColorSchemePart = 40;
			this.splitter_ps.HotGripDarkColor = Color.FromArgb(0, 45, 150);
			this.splitter_ps.HotGripDarkColorSchemePart = 53;
			this.splitter_ps.HotGripLightColor = Color.FromArgb(223, 237, 254);
			this.splitter_ps.HotGripLightColorSchemePart = 0;
			this.splitter_ps.Location = new Point(0, 111);
			this.splitter_ps.Name = "splitter_ps";
			this.splitter_ps.Size = new Size(668, 13);
			this.splitter_ps.TabIndex = 18;
			this.splitter_ps.TabStop = false;
			this.splitter_ps.Visible = false;
			this.lbl_noPermissionsToChangeMessage.BackColor = SystemColors.Highlight;
			this.lbl_noPermissionsToChangeMessage.BorderStyle = BorderStyle.Fixed3D;
			this.lbl_noPermissionsToChangeMessage.Dock = DockStyle.Bottom;
			this.lbl_noPermissionsToChangeMessage.Font = new Font("Arial", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbl_noPermissionsToChangeMessage.ForeColor = SystemColors.HighlightText;
			this.lbl_noPermissionsToChangeMessage.Location = new Point(0, 415);
			this.lbl_noPermissionsToChangeMessage.Name = "lbl_noPermissionsToChangeMessage";
			this.lbl_noPermissionsToChangeMessage.Size = new Size(668, 51);
			this.lbl_noPermissionsToChangeMessage.TabIndex = 19;
			this.lbl_noPermissionsToChangeMessage.Text = "Only the person who had the appointment with this student is allowed to enter this assessment";
			this.lbl_noPermissionsToChangeMessage.TextAlign = ContentAlignment.MiddleCenter;
			this.lbl_noPermissionsToChangeMessage.Visible = false;
			this.btn_cancel.Dock = DockStyle.Right;
			this.btn_cancel.Location = new Point(564, 4);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new Size(96, 32);
			this.btn_cancel.TabIndex = 0;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.UseVisualStyleBackColor = true;
			this.btn_cancel.Click += this.btn_cancel_Click;
			this.label1.Dock = DockStyle.Right;
			this.label1.Location = new Point(553, 4);
			this.label1.Name = "label1";
			this.label1.Size = new Size(11, 32);
			this.label1.TabIndex = 2;
			this.btn_save.Dock = DockStyle.Right;
			this.btn_save.Location = new Point(457, 4);
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new Size(96, 32);
			this.btn_save.TabIndex = 1;
			this.btn_save.Text = "&Save";
			this.btn_save.UseVisualStyleBackColor = true;
			this.btn_save.Click += this.btn_save_Click;
			this.p_saveCancel.BorderStyle = BorderStyle.Fixed3D;
			this.p_saveCancel.Controls.Add(this.btn_save);
			this.p_saveCancel.Controls.Add(this.label1);
			this.p_saveCancel.Controls.Add(this.btn_cancel);
			this.p_saveCancel.Dock = DockStyle.Bottom;
			this.p_saveCancel.Font = new Font("Arial", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.p_saveCancel.Location = new Point(0, 466);
			this.p_saveCancel.Name = "p_saveCancel";
			this.p_saveCancel.Padding = new Padding(4);
			this.p_saveCancel.Size = new Size(668, 44);
			this.p_saveCancel.TabIndex = 20;
			this.p_saveCancel.Visible = false;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.p_data);
			base.Controls.Add(this.splitter_ps);
			base.Controls.Add(this.p_data_ps);
			base.Controls.Add(this.lbl_noPermissionsToChangeMessage);
			base.Controls.Add(this.p_saveCancel);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "DataPerApp";
			base.Size = new Size(668, 510);
			this.p_saveCancel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x040001F2 RID: 498
		private UnivDataAdapter da;

		// Token: 0x040001F3 RID: 499
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040001F4 RID: 500
		private int screenNum;

		// Token: 0x040001F5 RID: 501
		private int whoAmIId;

		// Token: 0x040001F6 RID: 502
		private AppointmentDTO app;

		// Token: 0x040001F7 RID: 503
		private PersonBaseDTO student;

		// Token: 0x040001F8 RID: 504
		private Settings settings;

		// Token: 0x040001F9 RID: 505
		private Permissions permissions;

		// Token: 0x040001FA RID: 506
		private bool prefersFrench;

		// Token: 0x040001FB RID: 507
		private bool allowedToChangeAnything;

		// Token: 0x040001FC RID: 508
		private bool onlyAllowCounsellorsInTheAppToEnterTheAssessment;

		// Token: 0x040001FD RID: 509
		private bool onlyAllowCounsellorsInTheAppToSeeTheAssessment;

		// Token: 0x040001FE RID: 510
		private bool onlyAllowCounsellorsInTheAppToSeeTheAssessmentTextBoxesOnly;

		// Token: 0x040001FF RID: 511
		private ScreenInfo screen;

		// Token: 0x04000200 RID: 512
		private int controlIdToActivate;

		// Token: 0x04000201 RID: 513
		private DataSet data;

		// Token: 0x04000202 RID: 514
		private DataSet psData;

		// Token: 0x04000203 RID: 515
		private ScreenInfo psScreenInfo;

		// Token: 0x04000204 RID: 516
		private static DataSet comboBoxData = null;

		// Token: 0x04000205 RID: 517
		private static DataSet lookupTablesForControls = null;

		// Token: 0x04000206 RID: 518
		private Dictionary<int, DataSet> dataToWrite = new Dictionary<int, DataSet>();

		// Token: 0x04000209 RID: 521
		private static List<DataRow> screens;

		// Token: 0x0400020A RID: 522
		private DataTable psControls = null;

		// Token: 0x0400020B RID: 523
		private int[] dynamicScreenReadOnlyCids = null;

		// Token: 0x0400020C RID: 524
		private int[] dynamicScreenInvisibleCids = null;

		// Token: 0x0400020D RID: 525
		private IContainer components = null;

		// Token: 0x0400020E RID: 526
		private MyPanel p_data;

		// Token: 0x0400020F RID: 527
		private MyPanel p_data_ps;

		// Token: 0x04000210 RID: 528
		private ExpandableSplitter splitter_ps;

		// Token: 0x04000211 RID: 529
		private Label lbl_noPermissionsToChangeMessage;

		// Token: 0x04000212 RID: 530
		private ToolTip toolTip1;

		// Token: 0x04000213 RID: 531
		private Button btn_cancel;

		// Token: 0x04000214 RID: 532
		private Label label1;

		// Token: 0x04000215 RID: 533
		private Button btn_save;

		// Token: 0x04000216 RID: 534
		private Panel p_saveCancel;
	}
}
