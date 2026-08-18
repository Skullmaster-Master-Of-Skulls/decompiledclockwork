using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using AutoComboBox;
using AutoComboBox.MyControls;
using AutoComboBox.MyControls.CustomTableControls;
using AutoComboBox.MyControls.MultiLineTextBox;
using ClockWorkLogger;
using DynamicScreens.Controls;
using DynamicScreens.CustomControls;
using DynamicScreens.CustomControls.DynamicControls;
using DynamicScreens.DynamicControlWrappers;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using Microsoft.Data.Odbc;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls;
using TechnoPro.Common.UI.WinForms.Entity.DynamicForms;
using TechnoPro.Common.UI.WinForms.SigPlus.Controls;
using UnivOleDb;

namespace DynamicScreens
{
	// Token: 0x0200003A RID: 58
	public class DynamicScreen
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0001FF08 File Offset: 0x0001EF08
		public static string GetControlNameByControlCode(int controlCode)
		{
			string result;
			if (Enum.IsDefined(typeof(eControlCode), controlCode))
			{
				result = ((eControlCode)controlCode).GetTitle();
			}
			else
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001FF48 File Offset: 0x0001EF48
		public static bool CheckForMissingFields(Control parentControl, ref ArrayList warnings, ref ArrayList errors)
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < parentControl.Controls.Count; i++)
			{
				Control control = parentControl.Controls[i];
				if (control is MyRadioGroupPrimary || control is MyRadioGroupPrimaryCheckboxMultiple)
				{
					if (!flag2)
					{
						flag2 = true;
						if (control is MyRadioGroupPrimary)
						{
							bool flag3 = false;
							for (int j = i + 1; j < parentControl.Controls.Count; j++)
							{
								Control control2 = parentControl.Controls[j];
								if (control2 is MyRadioGroupPrimaryCheckboxMultiple)
								{
									MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control2;
									flag3 = myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked;
									if (flag3)
									{
										break;
									}
								}
							}
							if (!flag3)
							{
								DataRow dataRow = (DataRow)control.Tag;
								DynamicControl dynamicControl = new DynamicControl(dataRow);
								if (dynamicControl.Enforce == 1)
								{
									warnings.Add(dynamicControl);
								}
								else if (dynamicControl.Enforce == 2)
								{
									errors.Add(dynamicControl);
								}
							}
						}
					}
				}
				else if (control.Controls.Count > 0)
				{
					bool flag4 = DynamicScreen.CheckForMissingFields(control, ref warnings, ref errors);
					flag = (flag || flag4);
				}
				else if (control.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)control.Tag;
					if (dataRow.Table.Columns.Contains("controlcode"))
					{
						if (DynamicScreen.IsControlCodeDataHolding((int)dataRow["controlcode"]))
						{
							if (control.Enabled)
							{
								if (!DynamicScreen.IsControlFilledIn(control))
								{
									DynamicControl dynamicControl = new DynamicControl(dataRow);
									if (dynamicControl.Enforce == 1)
									{
										warnings.Add(dynamicControl);
									}
									else if (dynamicControl.Enforce == 2)
									{
										errors.Add(dynamicControl);
									}
								}
								else
								{
									flag = true;
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000201B4 File Offset: 0x0001F1B4
		private static bool IsControlFilledIn(Control c)
		{
			bool result;
			if (c is MyDynamicControl)
			{
				MyDynamicControl myDynamicControl = (MyDynamicControl)c;
				result = myDynamicControl.FilledIn;
			}
			else if (c is TextBox)
			{
				result = (((TextBox)c).Text.Trim().Length > 0);
			}
			else if (c is MaskedTextBox)
			{
				result = (((MaskedTextBox)c).Text.Trim().Length > 0);
			}
			else if (c is MyMaskedTextBox)
			{
				result = (((MyMaskedTextBox)c).Text.Trim().Length > 0);
			}
			else if (c is CheckBox)
			{
				result = ((CheckBox)c).Checked;
			}
			else if (c is RadioButton)
			{
				result = ((RadioButton)c).Checked;
			}
			else if (c is MyRadioGroup)
			{
				MyRadioGroup myRadioGroup = (MyRadioGroup)c;
				result = (myRadioGroup.SelectedId > -1);
			}
			else if (c is AutoComboBox)
			{
				AutoComboBox autoComboBox = (AutoComboBox)c;
				DataRow dataRow = autoComboBox.SelectedDataRow();
				if (dataRow == null)
				{
					result = (autoComboBox.SelectedText.Trim().Length > 0);
				}
				else
				{
					string text = dataRow[autoComboBox.DisplayMember].ToString().Trim();
					result = (text.Length > 0);
				}
			}
			else if (c is ListView)
			{
				result = (((ListView)c).Items.Count > 0);
			}
			else if (c is MyDateTimePicker)
			{
				MyDateTimePicker myDateTimePicker = (MyDateTimePicker)c;
				result = (myDateTimePicker.Value != DateTime.MinValue);
			}
			else if (c is CtrlDateTimePicker)
			{
				CtrlDateTimePicker ctrlDateTimePicker = (CtrlDateTimePicker)c;
				result = (ctrlDateTimePicker.Value != null);
			}
			else if (c is MyDateTimePickerForAccommodationsExpiry)
			{
				MyDateTimePickerForAccommodationsExpiry myDateTimePickerForAccommodationsExpiry = (MyDateTimePickerForAccommodationsExpiry)c;
				result = myDateTimePickerForAccommodationsExpiry.FilledIn;
			}
			else if (c is Label)
			{
				result = true;
			}
			else
			{
				MessageBox.Show("Unknown control: " + c.GetType().ToString() + "; " + c.Text);
				result = false;
			}
			return result;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0002045C File Offset: 0x0001F45C
		public static DataTable LoadDynamicControlsTable(UnivDataAdapter da, int screenNum)
		{
			DataTable dataTable = new DataTable();
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.DynamicScreenControlExtendedDescriptionFields_Mar_07);
			da.SelectCommand.CommandText = "SELECT    dsc.controlid,@screennum AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,\r\n            dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,\r\n            dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline,s.description,acc.longdescription,acc.showonletter,acc.showonemail,\r\n            acc.accommodationid,dc.uniqueid,dc.specialcontroltype\r\nFROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid LEFT JOIN screens s ON s.screennum=dsc.screennum LEFT JOIN accommodations acc ON acc.controlid=dc.controlid";
			if (screenNum > 0)
			{
				UnivCommand selectCommand = da.SelectCommand;
				selectCommand.CommandText += " WHERE dsc.screennum=@screennum AND dsc.isactive=@true ";
			}
			else
			{
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText += " WHERE dsc.isactive=@true ";
			}
			UnivCommand selectCommand3 = da.SelectCommand;
			selectCommand3.CommandText += " ORDER BY s.description,dsc.ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			da.SelectCommand.Parameters.Add("@true", true);
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00020540 File Offset: 0x0001F540
		public static DataTable LoadDynamicControlsTableWithExtendedAccommInfo(UnivDataAdapter da, int screenNum)
		{
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = "SELECT dsc.controlid,@screennum AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,\r\n        dc.ControlName,dc.ControlGroup,dsc.controlgroup AS controlgroupoverride,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,\r\n        dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,\r\n        dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline,\r\n        s.description,acc.longdescription,acc.showonletter,acc.showonemail,acc.accommodationid,acc.shortcode,acc.showonletter,acc.showonemail,\r\n        acc.extratime,acc.isalone,acc.needscomputer,acc.needsreaderscribe,acc.isgroup,acc.tapedexams,acc.other,acc.enlarged,acc.showonreport,dc.uniqueid,dc.specialcontroltype\r\nFROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid \r\nLEFT JOIN screens s ON s.screennum=dsc.screennum \r\nLEFT JOIN accommodations acc ON acc.controlid=dc.controlid";
			if (screenNum > 0)
			{
				UnivCommand selectCommand = da.SelectCommand;
				selectCommand.CommandText += " WHERE dsc.screennum=@screennum AND dsc.isactive=@true ";
			}
			else
			{
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText += " WHERE dsc.isactive=@true ";
			}
			UnivCommand selectCommand3 = da.SelectCommand;
			selectCommand3.CommandText += " ORDER BY s.description,dsc.ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			da.SelectCommand.Parameters.Add("@true", true);
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0002061C File Offset: 0x0001F61C
		public static DataTable LoadDynamicControlsTable2(UnivDataAdapter da, int screenNum)
		{
			return DynamicScreen.LoadDynamicControlsTable2(da, screenNum, false);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00020638 File Offset: 0x0001F638
		public static DataTable LoadDynamicControlsTable2(UnivDataAdapter da, int screenNum, bool excludeNonDataHoldingControls)
		{
			DataTable dataTable = new DataTable();
			string str = "SELECT    dsc.controlid,dsc.screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,\r\n            dc.defaultvalue,dc.ControlName,dc.ControlGroup,dsc.controlgroup AS controlgroupoverride,\r\n            dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,\r\n            dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,dc.enabled,dc.readonly,\r\n            dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline,s.description,acc.longdescription,\r\n            acc.showonletter,acc.showonemail,acc.accommodationid,acc.shortcode,acc.showonletter,\r\n            acc.showonemail,acc.extratime,acc.isalone,acc.needscomputer,acc.needsreaderscribe,acc.isgroup,\r\n            acc.tapedexams,acc.other,acc.enlarged,acc.showonreport,dc.uniqueid,dc.specialcontroltype\r\nFROM        dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid \r\n            LEFT JOIN screens s ON s.screennum=dsc.screennum \r\n            LEFT JOIN accommodations acc ON acc.controlid=dc.controlid";
			string commandText;
			if (screenNum > 0)
			{
				if (excludeNonDataHoldingControls)
				{
					commandText = str + "\r\nWHERE       dsc.screennum=@screennum \r\n            AND NOT dsc.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode IN \r\n                                        (SELECT controlcode FROM dynamicscreennondatacontrols) )\r\nORDER BY s.description,dsc.ordernum";
				}
				else
				{
					commandText = str + "\r\nWHERE       dsc.screennum=@screennum\r\nORDER BY s.description,dsc.ordernum";
				}
			}
			else if (excludeNonDataHoldingControls)
			{
				commandText = str + "\r\nWHERE       NOT dsc.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode IN \r\n                                        (SELECT controlcode FROM dynamicscreennondatacontrols) )\r\nORDER BY s.description,dsc.ordernum";
			}
			else
			{
				commandText = str + "\r\nORDER BY s.description,dsc.ordernum";
			}
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			da.SelectCommand.Parameters.Add("@true", true);
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00020718 File Offset: 0x0001F718
		public static void LoadDynamicScreenControls(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, ref MyPanel p_data, int overrideFontSize, ScreenInfo screen, DataSet comboBoxData, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmI_pid, string whoAmIName, int[] readOnlyCids, int[] invisibleCids)
		{
			DataTable controlListTable = DynamicScreen.LoadDynamicControlsTable(da, screenNum);
			if (overrideFontSize > 0)
			{
				screen.font = new Font(screen.font.FontFamily, (float)overrideFontSize);
			}
			p_data.Screen = screen;
			Panel panel = p_data;
			DynamicScreen.TranslateControls(da, tripleDES, ref panel, screen, controlListTable, ref comboBoxData, null, lookupTablesForControls, eventHandlers, whoAmI_pid, whoAmIName, readOnlyCids, invisibleCids);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00020780 File Offset: 0x0001F780
		public static DataTable TranslateControls(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Panel panel, ScreenInfo screen, string XMLstring, ref DataSet comboBoxData, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID)
		{
			DataSet dataSet;
			try
			{
				dataSet = new DataSet();
				byte[] bytes = Encoding.ASCII.GetBytes(XMLstring);
				MemoryStream memoryStream = new MemoryStream(bytes);
				dataSet.ReadXml(memoryStream, XmlReadMode.InferSchema);
				memoryStream.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				dataSet = null;
				return null;
			}
			DataTable result;
			if (dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				DataTable dataTable2 = new DataTable();
				Type type = Type.GetType("System.String");
				Type type2 = Type.GetType("System.Int32");
				dataTable2.Columns.Add("ID", type);
				dataTable2.Columns.Add("screenNum", type2);
				dataTable2.Columns.Add("controlID", type2);
				dataTable2.Columns.Add("controlCaption", type);
				dataTable2.Columns.Add("setting1", type2);
				dataTable2.Columns.Add("setting2", type2);
				dataTable2.Columns.Add("setting3", type2);
				dataTable2.Columns.Add("defaultItem", type2);
				dataTable2.Columns[0].DefaultValue = -1;
				dataTable2.Columns[1].DefaultValue = 1;
				dataTable2.Columns[3].DefaultValue = "Control";
				dataTable2.Columns[4].DefaultValue = 0;
				dataTable2.Columns[5].DefaultValue = 0;
				dataTable2.Columns[6].DefaultValue = 0;
				dataTable2.Columns[7].DefaultValue = 0;
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					object[] array = new object[dataTable.Columns.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = dataRow[i];
					}
					dataTable2.Rows.Add(array);
				}
				int num = DynamicScreen.TranslateControls(da, tripleDES, ref panel, screen, dataTable2, ref comboBoxData, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID);
				result = dataTable2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00020A48 File Offset: 0x0001FA48
		private static string GetControlCaption(DataRow dr, bool useFrench)
		{
			string result;
			if (16 < dr.Table.Columns.Count)
			{
				string text = useFrench ? dr[16].ToString() : "";
				result = ((text.Length > 0) ? text : ((string)dr[3]));
			}
			else
			{
				result = (string)dr[3];
			}
			return result;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00020AB8 File Offset: 0x0001FAB8
		private static string GetControlCaption(DynamicControl dc, bool useFrench)
		{
			string text = useFrench ? dc.Setting4String : "";
			return (text.Length > 0) ? text : dc.ControlCaption;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00020AF0 File Offset: 0x0001FAF0
		private static string GetControlCaptionForDisplay(DynamicControl dc, bool useFrench)
		{
			string text = useFrench ? dc.FrenchControlCaptionForDisplay : "";
			return (text.Length > 0) ? text : dc.ControlCaptionForDisplay;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00020B28 File Offset: 0x0001FB28
		public static int TranslateControls(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Panel panel, ScreenInfo screen, DataTable controlListTable, ref DataSet comboBoxData, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID)
		{
			return DynamicScreen.TranslateControls(da, tripleDES, ref panel, screen, controlListTable, ref comboBoxData, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, "");
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00020B54 File Offset: 0x0001FB54
		public static int TranslateControls(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Panel panel, ScreenInfo screen, DataTable controlListTable, ref DataSet comboBoxData, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, string whoAmIName)
		{
			return DynamicScreen.TranslateControls(da, tripleDES, ref panel, screen, controlListTable, ref comboBoxData, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, whoAmIName, new int[0], new int[0]);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00020B8C File Offset: 0x0001FB8C
		public static DataTable LoadControls(UnivDataAdapter da, int screenNum)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.DynamicScreenControlExtendedDescriptionFields_Mar_07);
			da.SelectCommand.CommandText = "SELECT    dsc.controlid,dsc.screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,\r\n            dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline,\r\n            dc.uniqueid,dc.specialcontroltype\r\nFROM        dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid \r\nWHERE       dsc.screennum=@screennum AND dsc.isactive=@true ";
			UnivCommand selectCommand = da.SelectCommand;
			selectCommand.CommandText += " ORDER BY dsc.ordernum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			da.SelectCommand.Parameters.Add("@true", true);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00020C2C File Offset: 0x0001FC2C
		public static DataTable LoadControls(int screenNum)
		{
			string commandText = "SELECT    dsc.controlid,dsc.screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,\r\n            dc.defaultvalue,dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,\r\n            dsc.controlgroup AS controlgroupoverride,\r\n            dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,\r\n            dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nFROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=@screennum AND dsc.isactive=@true \r\nORDER BY dsc.ordernum";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			da.SelectCommand.Parameters.Add("@true", true);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00020CB4 File Offset: 0x0001FCB4
		public static DataRow LoadControl(int cid)
		{
			string commandText = "SELECT    dc.controlid,0 AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,\r\n            dc.defaultvalue,dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,\r\n            dc.ActionHandlers,dc.DefaultValueString,dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,\r\n            dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nFROM dynamiccontrols dc\r\nWHERE dc.controlid=@cid";
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@cid", cid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return (dataTable.Rows.Count > 0) ? dataTable.Rows[0] : null;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00020D3C File Offset: 0x0001FD3C
		public static int TranslateControls(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref Panel panel, ScreenInfo screen, DataTable controlListTable, ref DataSet comboBoxData, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, string whoAmIName, int[] readOnlyCids, int[] invisibleCids)
		{
			ScreenInfo screenInfo = screen;
			int screenNum = screenInfo.screenNum;
			PanelInfo panelInfo = new PanelInfo(screenNum, panel, controlListTable);
			panel.Tag = panelInfo;
			Stack stack = new Stack();
			stack.Push(screenInfo);
			bool flag = false;
			DateScopes dateScopes = null;
			ArrayList arrayList = new ArrayList();
			int num = -1;
			MyTextBox myTextBox = null;
			ToolTip toolTip = new ToolTip();
			panel.SuspendLayout();
			for (;;)
			{
				num++;
				if (num >= controlListTable.Rows.Count)
				{
					break;
				}
				DataRow dataRow = controlListTable.Rows[num];
				int num2 = (int)dataRow[1];
				if (num2 == screenNum || screenNum == 0)
				{
					if (!flag)
					{
						flag = true;
					}
					int num3 = (int)dataRow[2];
					string controlCaption = DynamicScreen.GetControlCaption(dataRow, screen.UseFrench);
					int num4 = (int)dataRow[0];
					bool flag2;
					if (invisibleCids != null)
					{
						if (Array.IndexOf<int>(invisibleCids, num4) >= 0)
						{
							flag2 = true;
						}
						else if (num3 == 31)
						{
							int i = num - 1;
							flag2 = false;
							while (i >= 0)
							{
								int num5 = (int)controlListTable.Rows[i][2];
								if (num5 == 30)
								{
									if (Array.IndexOf<int>(invisibleCids, (int)controlListTable.Rows[i][0]) >= 0)
									{
										flag2 = true;
										break;
									}
								}
								else if (num5 == 31)
								{
									break;
								}
								i--;
							}
						}
						else
						{
							flag2 = false;
						}
					}
					else
					{
						flag2 = false;
					}
					if (myTextBox != null && num3 == 1 && controlCaption.IndexOf("__") > 0)
					{
						myTextBox.MaxLength += 3000;
						myTextBox.AddMultipleCid(dataRow);
					}
					else if (!flag2)
					{
						Control control = DynamicScreen.AddControl(ref screenInfo, stack, num3, dataRow, ref comboBoxData, da, tripleDES, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, ref dateScopes, whoAmIName, readOnlyCids != null && Array.IndexOf<int>(readOnlyCids, num4) >= 0, toolTip, panelInfo);
						if (control is TextBox && ((TextBox)control).Multiline)
						{
							myTextBox = (MyTextBox)control;
						}
						else
						{
							myTextBox = null;
						}
						if (dataRow.Table.Columns.Count > 10 && control != null && (control is Label || control is CheckBox || control is RadioButton || control is TextBox || control is ComboBox || control is MaskedTextBox || control is MyTextBox))
						{
							string text = (string)dataRow[10];
							if (text.Length > 0)
							{
								int num6 = text.IndexOf(Environment.NewLine);
								string title;
								if (num6 > 0)
								{
									title = text.Substring(0, num6);
									text = text.Substring(num6 + 1);
								}
								else
								{
									title = DynamicScreen.GetControlCaption(dataRow, screen.UseFrench);
								}
								int num7 = (int)dataRow[11];
								ArrayList arrayList2 = new ArrayList(1);
								arrayList2.Add(control);
								if (num7 == 3 || num7 == 4)
								{
									num6 = control.Parent.Controls.IndexOf(control) - 1;
									Control control2 = (num6 >= 0) ? control.Parent.Controls[num6] : null;
									if (control2 != null && !(control2.Tag is DataRow))
									{
										if (num7 == 3)
										{
											arrayList2.Clear();
										}
										arrayList2.Add(control2);
									}
								}
								if (panel is MyPanel && ((MyPanel)panel).IsDynamicScreenContainer)
								{
									foreach (object obj in arrayList2)
									{
										Control c = (Control)obj;
										((MyPanel)panel).RegisterHelpText(num7, c, title, text);
									}
								}
							}
						}
						if (control is MyTextBox)
						{
							MyTextBox myTextBox2 = (MyTextBox)control;
							if (myTextBox2.MaskCid > 0)
							{
								arrayList.Add(control);
							}
						}
						else if (control is MyCheckBox)
						{
							MyCheckBox myCheckBox = (MyCheckBox)control;
							if (myCheckBox.SetEnabledControlId > 0 || myCheckBox.AutoCheckThisBoxWhenOtherControlModified_cid > 0)
							{
								arrayList.Add(control);
							}
						}
						else if (control is AutoComboBox)
						{
							AutoComboBox autoComboBox = (AutoComboBox)control;
							if (autoComboBox.ChildLookupGroupId > 0)
							{
								arrayList.Add(control);
							}
						}
						else if (control is MyInfoBox)
						{
							MyInfoBox myInfoBox = (MyInfoBox)control;
							arrayList.Add(myInfoBox);
						}
						else if (control is CalculationButton)
						{
							arrayList.Add(control);
						}
					}
				}
				else if (flag)
				{
					break;
				}
			}
			panel.ResumeLayout(false);
			screenInfo.CurrentListSelect = null;
			foreach (object obj2 in arrayList)
			{
				Control control = (Control)obj2;
				if (control is MyTextBox)
				{
					MyTextBox myTextBox2 = (MyTextBox)control;
					if (myTextBox2.MaskCid > 0)
					{
						Control control3 = DynamicScreen.FindControl(panel, myTextBox2.MaskCid);
						if (control3 == null)
						{
							control3 = DynamicScreen.FindControl(control.TopLevelControl, myTextBox2.MaskCid);
						}
						if (control3 != null && control3 is AutoComboBox)
						{
							AutoComboBox autoComboBox2 = (AutoComboBox)control3;
							autoComboBox2.MaskedTextBox = myTextBox2;
						}
					}
				}
				else if (control is MyCheckBox)
				{
					MyCheckBox myCheckBox = (MyCheckBox)control;
					int num4 = myCheckBox.SetEnabledControlId;
					if (num4 > 0)
					{
						Control control4 = DynamicScreen.FindControl(panel, num4);
						if (control4 == null)
						{
							control4 = DynamicScreen.FindControl(control.TopLevelControl, num4);
						}
						if (control4 != null)
						{
							myCheckBox.SetEnabledControl = control4;
							myCheckBox.SetEnabledControlEnabled(myCheckBox.Checked);
						}
					}
					int autoCheckThisBoxWhenOtherControlModified_cid = myCheckBox.AutoCheckThisBoxWhenOtherControlModified_cid;
					if (autoCheckThisBoxWhenOtherControlModified_cid > 0)
					{
						Control control2 = DynamicScreen.FindControl(panel, autoCheckThisBoxWhenOtherControlModified_cid);
						if (control2 != null)
						{
							if (control2 is MyTextBox)
							{
								MyTextBox myTextBox3 = (MyTextBox)control2;
								myTextBox3.SyncedCheckbox = (MyCheckBox)control;
							}
							else if (control2 is MyDateTimePicker)
							{
								MyDateTimePicker myDateTimePicker = (MyDateTimePicker)control2;
								myDateTimePicker.SyncedCheckbox = (MyCheckBox)control;
							}
							else if (control2 is AutoComboBox)
							{
								AutoComboBox autoComboBox3 = (AutoComboBox)control2;
								autoComboBox3.SyncedCheckbox = (MyCheckBox)control;
							}
							else if (control2 is MyCheckBox)
							{
								MyCheckBox myCheckBox2 = (MyCheckBox)control2;
								myCheckBox2.SyncedCheckbox = (MyCheckBox)control;
							}
						}
					}
				}
				else if (control is AutoComboBox)
				{
					try
					{
						AutoComboBox autoComboBox4 = (AutoComboBox)control;
						if (autoComboBox4.ChildLookupGroupId > 0)
						{
							Control parent = autoComboBox4.Parent;
							if (parent != null)
							{
								foreach (object obj3 in parent.Controls)
								{
									Control control5 = (Control)obj3;
									if (control5 is AutoComboBox)
									{
										AutoComboBox autoComboBox5 = (AutoComboBox)control5;
										if (autoComboBox5.LookupGroupId == autoComboBox4.ChildLookupGroupId)
										{
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
				else if (control is MyInfoBox)
				{
					MyInfoBox myInfoBox = (MyInfoBox)control;
					myInfoBox.SetupHandlerToDetectWhenUserIsChanged(panel);
				}
				else if (control is CalculationButton)
				{
					CalculationButton calculationButton = (CalculationButton)control;
					calculationButton.SetupAutoRecalc();
				}
			}
			return 1;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00021734 File Offset: 0x00020734
		public static Control FindControl(Control cTop, int controlId)
		{
			if (cTop != null)
			{
				if (cTop.Tag != null && cTop.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)cTop.Tag;
					int num = (dataRow[0] != DBNull.Value) ? ((int)dataRow[0]) : 0;
					if (num == controlId)
					{
						return cTop;
					}
				}
				foreach (object obj in cTop.Controls)
				{
					Control cTop2 = (Control)obj;
					Control control = DynamicScreen.FindControl(cTop2, controlId);
					if (control != null)
					{
						return control;
					}
				}
			}
			return null;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0002182C File Offset: 0x0002082C
		private static void AddControl(ref ScreenInfo currentScreenInfo, Stack screenInfos, int controlID, DataRow controlListRow, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID)
		{
			DynamicScreen.AddControl(ref currentScreenInfo, screenInfos, controlID, controlListRow, ref comboBoxData, da, tripleDES, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, "");
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00021858 File Offset: 0x00020858
		private static void AddControl(ref ScreenInfo currentScreenInfo, Stack screenInfos, int controlID, DataRow controlListRow, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, string whoAmIName)
		{
			DateScopes dateScopes = null;
			DynamicScreen.AddControl(ref currentScreenInfo, screenInfos, controlID, controlListRow, ref comboBoxData, da, tripleDES, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, ref dateScopes);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00021884 File Offset: 0x00020884
		private static Control AddControl(ref ScreenInfo currentScreenInfo, Stack screenInfos, int controlID, DataRow controlListRow, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, ref DateScopes dateScopes)
		{
			return DynamicScreen.AddControl(ref currentScreenInfo, screenInfos, controlID, controlListRow, ref comboBoxData, da, tripleDES, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, ref dateScopes, "");
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000218B4 File Offset: 0x000208B4
		private static Control AddControl(ref ScreenInfo currentScreenInfo, Stack screenInfos, int controlID, DataRow controlListRow, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, ref DateScopes dateScopes, string whoAmIName)
		{
			return DynamicScreen.AddControl(ref currentScreenInfo, screenInfos, controlID, controlListRow, ref comboBoxData, da, tripleDES, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, ref dateScopes, whoAmIName, false);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000218E4 File Offset: 0x000208E4
		private static Control AddControl(ref ScreenInfo currentScreenInfo, Stack screenInfos, int controlID, DataRow controlListRow, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, ref DateScopes dateScopes, string whoAmIName, bool forceReadOnly)
		{
			return DynamicScreen.AddControl(ref currentScreenInfo, screenInfos, controlID, controlListRow, ref comboBoxData, da, tripleDES, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, ref dateScopes, whoAmIName, forceReadOnly, null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00021914 File Offset: 0x00020914
		private static Control AddControl(ref ScreenInfo currentScreenInfo, Stack screenInfos, int controlID, DataRow controlListRow, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, ref DateScopes dateScopes, string whoAmIName, bool forceReadOnly, ToolTip toolTip)
		{
			return DynamicScreen.AddControl(ref currentScreenInfo, screenInfos, controlID, controlListRow, ref comboBoxData, da, tripleDES, lockImage, lookupTablesForControls, eventHandlers, whoAmIPersonID, ref dateScopes, whoAmIName, forceReadOnly, toolTip, null);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00021948 File Offset: 0x00020948
		private static Control AddControl(ref ScreenInfo currentScreenInfo, Stack screenInfos, int controlID, DataRow controlListRow, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, Image lockImage, DataSet lookupTablesForControls, ArrayList eventHandlers, int whoAmIPersonID, ref DateScopes dateScopes, string whoAmIName, bool forceReadOnly, ToolTip toolTip, PanelInfo panelInfo)
		{
			DynamicControl dynamicControl = new DynamicControl(controlListRow);
			if (forceReadOnly)
			{
				dynamicControl.ReadOnly = true;
			}
			ScreenInfo screenInfo = (screenInfos.Count > 0) ? ((ScreenInfo)screenInfos.ToArray()[screenInfos.Count - 1]) : currentScreenInfo;
			Control result;
			if (controlID == 31 || controlID == 34 || controlID == 35)
			{
				ScreenInfo screenInfo2 = currentScreenInfo;
				screenInfos.Pop();
				Control parentControl = screenInfo2.parentControl;
				currentScreenInfo = (ScreenInfo)screenInfos.Peek();
				Control parentControl2 = currentScreenInfo.parentControl;
				int num = currentScreenInfo.parentControlHeight;
				if (parentControl2 is Panel || parentControl2 is MyTabControl || parentControl2 is MyTabPage)
				{
					if (parentControl2.Controls.Count > 0 && parentControl2.Controls[0] is Label)
					{
						parentControl2.Text = parentControl2.Controls[0].Text;
						parentControl2.AccessibleName = parentControl2.Text;
						parentControl2.AccessibleDescription = parentControl2.Text;
					}
					if (screenInfo2.bottomLess)
					{
						if (parentControl is MyLayoutPanel)
						{
							MyLayoutPanel myLayoutPanel = (MyLayoutPanel)parentControl;
							if (myLayoutPanel.IsLayoutPanel)
							{
								parentControl.Height = myLayoutPanel.RealHeight;
							}
							else
							{
								parentControl.Height = screenInfo2.currentY + 4;
							}
						}
						else
						{
							parentControl.Height = screenInfo2.currentY + 4;
						}
					}
					int y = num - currentScreenInfo.currentY;
					if (!currentScreenInfo.WillYFitInCurrentColumn(y))
					{
						if (currentScreenInfo.currentY > currentScreenInfo.BORDERPADY)
						{
							currentScreenInfo.GotoNextColumn();
						}
						else if (currentScreenInfo.parentControl.Height < parentControl.Height + currentScreenInfo.BORDERPADY)
						{
							num = parentControl.Height + currentScreenInfo.BORDERPADY;
							currentScreenInfo.parentControlHeight = num;
							currentScreenInfo.parentControl.Height = num;
						}
					}
					if (!screenInfo2.bottomLess && screenInfo2.numColumns > 0)
					{
						int currentMaxY = screenInfo2.GetCurrentMaxY();
						if (currentMaxY > parentControl.Height)
						{
							parentControl.Height = currentMaxY + screenInfo2.verticalControlPad;
						}
					}
					parentControl.Top = currentScreenInfo.currentY;
					parentControl.Left = currentScreenInfo.currentX;
					parentControl.ResumeLayout(false);
					parentControl2.Controls.Add(parentControl);
					if (parentControl is MyExpandableGroupBox)
					{
						MyExpandableGroupBox myExpandableGroupBox = (MyExpandableGroupBox)parentControl;
						myExpandableGroupBox.Expanded = false;
						myExpandableGroupBox.Height = 30;
					}
					if (screenInfo.OverridePanelBackgroundColourEnabled && parentControl is Panel)
					{
						foreach (object obj in parentControl.Controls)
						{
							Control control = (Control)obj;
							if (control is Label || control is CheckBox || control is RadioButton || control is MyRadioButton)
							{
								control.ForeColor = screenInfo.OverridePanelForegroundColour;
							}
							else if (control is MyRadioGroupPrimaryCheckboxMultiple)
							{
								((MyRadioGroupPrimaryCheckboxMultiple)control).SetForeColour(screenInfo.OverridePanelForegroundColour);
							}
							else if (control is MyRadioGroup)
							{
								((MyRadioGroup)control).SetForeColour(screenInfo.OverridePanelForegroundColour);
								((MyRadioGroup)control).SetBackColour(screenInfo.OverridePanelBackgroundColour);
							}
							else if (control is AccommodationControl2)
							{
								((AccommodationControl2)control).SetForeColour(screenInfo.OverridePanelForegroundColour);
							}
						}
					}
					currentScreenInfo.currentY += parentControl.Height + 6 + currentScreenInfo.verticalControlPad;
					currentScreenInfo.NotifyAddedControl();
				}
				else if (parentControl2 is MyTabControl && parentControl is MyTabPage)
				{
					currentScreenInfo.NotifyAddedControl();
				}
				result = parentControl2;
			}
			else if (controlID == 32)
			{
				int num2 = dynamicControl.Setting1;
				int num3 = dynamicControl.Setting2;
				int setting = dynamicControl.Setting4;
				int num4 = dynamicControl.DefaultValue;
				bool flag;
				if (num4 < 0)
				{
					num4 = -num4;
					flag = true;
				}
				else
				{
					flag = false;
				}
				int num5 = (setting <= 0) ? (currentScreenInfo.columnWidth - 25) : setting;
				int num6 = (int)controlListRow[6];
				MyTabControl myTabControl = new MyTabControl();
				myTabControl.Dock = DockStyle.Fill;
				myTabControl.BringToFront();
				currentScreenInfo.parentControl.Controls.Add(myTabControl);
				ScreenInfo screenInfo3 = new ScreenInfo(currentScreenInfo.screenNum, myTabControl, num4 == 0, currentScreenInfo.verticalControlPad, num5, currentScreenInfo.columnPad, currentScreenInfo.font, currentScreenInfo.iconID, currentScreenInfo.description, currentScreenInfo.studentNameNumEditable, currentScreenInfo.OverridePanelBackgroundColourEnabled, currentScreenInfo.OverridePanelBackgroundColour, currentScreenInfo.OverridePanelForegroundColour);
				screenInfo3.UseFrench = currentScreenInfo.UseFrench;
				screenInfo3.NumLinesVerticalLimit = num4;
				screenInfo3.NewNumLinesVerticalLimit = flag;
				screenInfo3.numColumns = 1;
				screenInfo3.parentControlHeight = myTabControl.Height - 2;
				screenInfos.Push(screenInfo3);
				currentScreenInfo = screenInfo3;
				result = myTabControl;
			}
			else if (controlID == 33 && (currentScreenInfo.parentControl is MyTabControl || currentScreenInfo.parentControl is MyTabPage))
			{
				int num2 = dynamicControl.Setting1;
				int num3 = dynamicControl.Setting2;
				int num4 = dynamicControl.DefaultValue;
				bool flag;
				if (num4 < 0)
				{
					num4 = -num4;
					flag = true;
				}
				else
				{
					flag = false;
				}
				int num5 = currentScreenInfo.columnWidth;
				int setting2 = dynamicControl.Setting4;
				if (setting2 > 0)
				{
					double num7 = (double)setting2 / 100.0;
					if (screenInfos.Count > 0)
					{
						object[] array = screenInfos.ToArray();
						ScreenInfo screenInfo4 = (ScreenInfo)array[array.Length - 1];
						double num8 = (double)screenInfo4.ColumnWidth / screenInfo4.WidthPercent;
						num5 = Convert.ToInt32(num8 * num7);
					}
				}
				MyTabControl myTabControl2;
				if (currentScreenInfo.parentControl is MyTabControl)
				{
					myTabControl2 = (MyTabControl)currentScreenInfo.parentControl;
				}
				else
				{
					myTabControl2 = (MyTabControl)((MyTabPage)currentScreenInfo.parentControl).Parent;
					screenInfos.Pop();
				}
				string text = DynamicScreen.GetControlCaption(dynamicControl, currentScreenInfo.UseFrench);
				MyTabPage myTabPage = new MyTabPage(text);
				myTabControl2.AddTabPage(myTabPage);
				ScreenInfo screenInfo3 = new ScreenInfo(currentScreenInfo.screenNum, myTabPage, num4 == 0, currentScreenInfo.verticalControlPad, num5, currentScreenInfo.columnPad, currentScreenInfo.font, currentScreenInfo.iconID, currentScreenInfo.description, currentScreenInfo.studentNameNumEditable, currentScreenInfo.OverridePanelBackgroundColourEnabled, currentScreenInfo.OverridePanelBackgroundColour, currentScreenInfo.OverridePanelForegroundColour);
				screenInfo3.UseFrench = currentScreenInfo.UseFrench;
				screenInfo3.NumLinesVerticalLimit = num4;
				screenInfo3.NewNumLinesVerticalLimit = flag;
				screenInfo3.numColumns = 1;
				screenInfo3.parentControlHeight = myTabPage.Height - 2;
				if (setting2 > 0)
				{
					screenInfo3.WidthPercent = (double)setting2;
				}
				screenInfos.Push(screenInfo3);
				currentScreenInfo = screenInfo3;
				result = myTabPage;
			}
			else if (controlID == 30)
			{
				int num2 = (int)controlListRow[4];
				int num3 = (int)controlListRow[5];
				int num4 = (int)controlListRow[7];
				int num6 = (int)controlListRow[6];
				string text2 = dynamicControl.SpecialInstructions("screennum");
				int num9 = (text2 == null || text2.Trim().Length < 1) ? 0 : int.Parse(text2);
				bool flag2 = 22 < controlListRow.Table.Columns.Count && controlListRow[22] != DBNull.Value && controlListRow[22].GetType() == typeof(bool) && (bool)controlListRow[22];
				int num10 = dynamicControl.Setting4;
				bool flag;
				if (num4 < 0)
				{
					num4 = -num4;
					flag = true;
				}
				else
				{
					flag = false;
				}
				Control control2;
				if (flag2)
				{
					MyExpandableGroupBox myExpandableGroupBox2 = new MyExpandableGroupBox();
					control2 = myExpandableGroupBox2;
				}
				else
				{
					control2 = new MyLayoutPanel();
				}
				control2.AccessibleName = dynamicControl.ControlCaptionForDisplay;
				control2.AccessibleDefaultActionDescription = dynamicControl.ControlCaptionForDisplay;
				control2.AccessibleDescription = dynamicControl.ControlCaptionForDisplay;
				control2.TabStop = true;
				control2.Text = dynamicControl.ControlCaptionForDisplay;
				string text3 = controlListRow[3].ToString();
				int num11 = text3.IndexOf("~~");
				string colWidthsRowHeightsDef;
				if (num11 >= 0)
				{
					colWidthsRowHeightsDef = text3.Substring(num11 + 2);
				}
				else
				{
					colWidthsRowHeightsDef = "";
				}
				if (num10 > 0)
				{
					num10--;
					if (control2 is MyLayoutPanel)
					{
						MyLayoutPanel myLayoutPanel = (MyLayoutPanel)control2;
						if (num10 > 0)
						{
							myLayoutPanel.ConvertToLayoutPanel(num10, num6, colWidthsRowHeightsDef);
						}
						else
						{
							myLayoutPanel.ConvertToLayoutPanel(num6, colWidthsRowHeightsDef);
						}
					}
				}
				control2.Tag = controlListRow;
				if (control2 is Panel)
				{
					((Panel)control2).BorderStyle = (BorderStyle)num2;
				}
				if (screenInfo.OverridePanelBackgroundColourEnabled)
				{
					num3 = screenInfo.OverridePanelBackgroundColour.ToArgb();
					control2.ForeColor = screenInfo.OverridePanelForegroundColour;
				}
				if (num3 != 0)
				{
					Color color = Color.FromArgb(num3);
					if (control2 is MyExpandableGroupBox)
					{
						MyExpandableGroupBox myExpandableGroupBox3 = (MyExpandableGroupBox)control2;
						myExpandableGroupBox3.Style.BackColor1.Color = color;
						myExpandableGroupBox3.Style.BackColor2.Color = ControlPaint.Dark(color);
						myExpandableGroupBox3.Style.ForeColor.Color = Color.Black;
					}
					else
					{
						control2.BackColor = color;
						control2.ForeColor = Color.Black;
					}
				}
				control2.Width = currentScreenInfo.columnWidth;
				int num5 = currentScreenInfo.columnWidth - 25;
				if (num4 > 0)
				{
					int num12 = currentScreenInfo.font.Height + 4 + 6;
					control2.Height = num12 * (flag ? 1 : num4);
					control2.Height += 6;
					if (num6 > 0)
					{
						int num13 = num5 - currentScreenInfo.columnPad;
						num13 -= num6;
						num5 = num13 / num6;
					}
				}
				else if (control2 is Panel)
				{
					((Panel)control2).AutoScroll = true;
				}
				if (control2 is MyExpandableGroupBox)
				{
					MyExpandableGroupBox myExpandableGroupBox4 = (MyExpandableGroupBox)control2;
					myExpandableGroupBox4.TitleText = text3;
				}
				control2.SuspendLayout();
				ScreenInfo screenInfo3 = new ScreenInfo(currentScreenInfo.screenNum, control2, num4 == 0, currentScreenInfo.verticalControlPad, num5, currentScreenInfo.columnPad, currentScreenInfo.font, currentScreenInfo.iconID, currentScreenInfo.description, currentScreenInfo.OverridePanelBackgroundColourEnabled, currentScreenInfo.studentNameNumEditable, currentScreenInfo.OverridePanelBackgroundColour, currentScreenInfo.OverridePanelForegroundColour);
				screenInfo3.UseFrench = currentScreenInfo.UseFrench;
				screenInfo3.NumLinesVerticalLimit = num4;
				screenInfo3.NewNumLinesVerticalLimit = flag;
				screenInfo3.numColumns = num6;
				screenInfo3.parentControlHeight = control2.Height - 2;
				screenInfo3.border = num2;
				if (control2 is MyExpandableGroupBox)
				{
					screenInfo3.currentY += ((MyExpandableGroupBox)control2).TitleHeight;
				}
				screenInfos.Push(screenInfo3);
				currentScreenInfo = screenInfo3;
				result = control2;
			}
			else
			{
				string text = DynamicScreen.GetControlCaption(controlListRow, currentScreenInfo.UseFrench).ToLower().Trim();
				if (text.CompareTo("perfectionism") == 0)
				{
					text += "a";
				}
				Control control;
				if (controlID <= 500)
				{
					if (controlID <= 100)
					{
						switch (controlID)
						{
						case 1:
							control = DynamicScreen.AddTextBox(dynamicControl, controlListRow, ref currentScreenInfo, lockImage, true, whoAmIPersonID, whoAmIName);
							goto IL_13B4;
						case 2:
							control = DynamicScreen.AddCheckbox(dynamicControl, controlListRow, ref currentScreenInfo, true);
							goto IL_13B4;
						case 3:
							control = DynamicScreen.AddComboBox(dynamicControl, controlListRow, ref currentScreenInfo, ref comboBoxData, da, lockImage, tripleDES);
							goto IL_13B4;
						case 4:
							control = DynamicScreen.AddRadioButton(dynamicControl, controlListRow, ref currentScreenInfo);
							goto IL_13B4;
						case 5:
							control = DynamicScreen.AddLabel(controlListRow, ref currentScreenInfo, panelInfo);
							goto IL_13B4;
						case 6:
							control = DynamicScreen.AddDate(dynamicControl, controlListRow, ref currentScreenInfo);
							goto IL_13B4;
						case 7:
						case 15:
						case 16:
						case 17:
						case 18:
						case 19:
						case 22:
						case 23:
						case 24:
							break;
						case 8:
							control = DynamicScreen.AddHorizontalRule(controlListRow, ref currentScreenInfo);
							goto IL_13B4;
						case 9:
							control = DynamicScreen.AddBlankSpace(dynamicControl, controlListRow, ref currentScreenInfo);
							goto IL_13B4;
						case 10:
							control = DynamicScreen.AddListView(dynamicControl, controlListRow, ref currentScreenInfo, eventHandlers, ref comboBoxData, da);
							goto IL_13B4;
						case 11:
						{
							MyTextBox myTextBox = (MyTextBox)DynamicScreen.AddMyTextBox(dynamicControl, controlListRow, ref currentScreenInfo, lockImage, whoAmIPersonID, whoAmIName);
							control = myTextBox;
							goto IL_13B4;
						}
						case 12:
						{
							MyCheckBox myCheckBox = (MyCheckBox)DynamicScreen.AddMyCheckbox(dynamicControl, controlListRow, ref currentScreenInfo);
							control = myCheckBox;
							goto IL_13B4;
						}
						case 13:
							DynamicScreen.AddIndent(controlListRow, ref currentScreenInfo);
							return null;
						case 14:
							control = DynamicScreen.AddRadioGroup(dynamicControl, controlListRow, ref currentScreenInfo, ref comboBoxData, da, lockImage);
							goto IL_13B4;
						case 20:
							control = DynamicScreen.AddFileList2(dynamicControl, controlListRow, ref currentScreenInfo, eventHandlers, ref comboBoxData, da, whoAmIPersonID, whoAmIName, tripleDES);
							goto IL_13B4;
						case 21:
							control = DynamicScreen.AddPicture(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da, tripleDES);
							goto IL_13B4;
						case 25:
							control = DynamicScreen.AddDynamicTable(dynamicControl, ref currentScreenInfo, da, tripleDES);
							goto IL_13B4;
						default:
							if (controlID == 50)
							{
								control = DynamicScreen.AddColumnBreak(controlListRow, ref currentScreenInfo);
								goto IL_13B4;
							}
							if (controlID == 100)
							{
								control = DynamicScreen.AddStaffComboBox(dynamicControl, controlListRow, ref currentScreenInfo, ref comboBoxData, da, lockImage, lookupTablesForControls, whoAmIPersonID, tripleDES);
								goto IL_13B4;
							}
							break;
						}
					}
					else if (controlID <= 301)
					{
						if (controlID == 200)
						{
							control = DynamicScreen.AddSchoolYearChooser(dynamicControl, controlListRow, ref currentScreenInfo, da, ref dateScopes);
							goto IL_13B4;
						}
						switch (controlID)
						{
						case 300:
							control = DynamicScreen.AddMaskedTextBox(dynamicControl, controlListRow, ref currentScreenInfo, lockImage, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
							goto IL_13B4;
						case 301:
							control = DynamicScreen.AddListSelect(dynamicControl, ref currentScreenInfo);
							goto IL_13B4;
						}
					}
					else
					{
						if (controlID == 400)
						{
							control = DynamicScreen.AddFile(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, da, tripleDES);
							goto IL_13B4;
						}
						if (controlID == 500)
						{
							control = DynamicScreen.AddMultiCheckBox(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
							goto IL_13B4;
						}
					}
				}
				else if (controlID <= 530)
				{
					if (controlID == 510)
					{
						control = DynamicScreen.AddMultiCheckBoxTextBox(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
						goto IL_13B4;
					}
					if (controlID == 520)
					{
						control = DynamicScreen.AddMultiCheckBoxComboBox(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
						goto IL_13B4;
					}
					if (controlID == 530)
					{
						control = DynamicScreen.AddMultiCheckBoxHeader(controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
						goto IL_13B4;
					}
				}
				else if (controlID <= 620)
				{
					if (controlID == 600)
					{
						control = DynamicScreen.AddRichTextBox(dynamicControl, controlListRow, ref currentScreenInfo, lockImage, whoAmIPersonID, whoAmIName);
						goto IL_13B4;
					}
					if (controlID == 620)
					{
						control = DynamicScreen.AddMultilineTextBox(dynamicControl, controlListRow, ref currentScreenInfo, lockImage, whoAmIPersonID, whoAmIName);
						goto IL_13B4;
					}
				}
				else
				{
					switch (controlID)
					{
					case 700:
						control = DynamicScreen.AddAccommodationCheckbox(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
						goto IL_13B4;
					case 701:
						control = DynamicScreen.AddAccommodationTextbox(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
						goto IL_13B4;
					case 702:
						control = DynamicScreen.AddAccommodationDatePicker(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da);
						goto IL_13B4;
					case 703:
						control = DynamicScreen.AddAccommodationDroplist(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da, tripleDES);
						goto IL_13B4;
					default:
						switch (controlID)
						{
						case 800:
							if (screenInfos.Count > 0)
							{
								ScreenInfo screenInfo5 = (ScreenInfo)screenInfos.ToArray()[0];
								StringDictionary stringDictionary = DynamicScreen.ParseArgs(dynamicControl.ControlGroup, Environment.NewLine.ToCharArray());
								string[] array2 = new string[stringDictionary.Keys.Count];
								stringDictionary.Keys.CopyTo(array2, 0);
								foreach (string text4 in array2)
								{
									string text5 = text4.ToLower();
									try
									{
										string text6 = text5;
										if (text6 != null)
										{
											if (text6 == "psscreennum")
											{
												screenInfo5.PerStudentScreenNum = int.Parse(stringDictionary[text4]);
												goto IL_10EC;
											}
											if (text6 == "psscreennum_height")
											{
												screenInfo5.PerStudentScreenNum_Height = int.Parse(stringDictionary[text4]);
												goto IL_10EC;
											}
											if (text6 == "activecontrol")
											{
												MyPanel myPanel = null;
												for (Control control3 = screenInfo5.parentControl; control3 != null; control3 = control3.Parent)
												{
													if (control3 is MyPanel)
													{
														myPanel = (MyPanel)control3;
														break;
													}
												}
												if (myPanel != null)
												{
													int defaultActiveControl;
													if (int.TryParse(stringDictionary[text4], out defaultActiveControl))
													{
														myPanel.DefaultActiveControl = defaultActiveControl;
														int defaultActiveControl2 = myPanel.DefaultActiveControl;
													}
												}
												goto IL_10EC;
											}
										}
										screenInfo5.AddArg(text4, stringDictionary[text4]);
										IL_10EC:;
									}
									catch
									{
									}
								}
								string helpText = dynamicControl.HelpText;
								string defaultValueString = dynamicControl.DefaultValueString;
								string actionHandlers = dynamicControl.ActionHandlers;
								if (!string.IsNullOrEmpty(helpText))
								{
									screenInfo5.AddArg("code_formLoaded", helpText);
								}
								if (!string.IsNullOrEmpty(defaultValueString))
								{
									screenInfo5.AddArg("code_preSave", defaultValueString);
								}
								if (!string.IsNullOrEmpty(actionHandlers))
								{
									screenInfo5.AddArg("code_misc", actionHandlers);
								}
							}
							control = null;
							goto IL_13B4;
						case 801:
							control = DynamicScreen.AddDynamicControlsChooser(dynamicControl, controlListRow, ref currentScreenInfo, whoAmIPersonID, whoAmIName, ref comboBoxData, da, tripleDES);
							goto IL_13B4;
						case 802:
							control = DynamicScreen.AddMultiDatabaseItemChooser(dynamicControl, controlListRow, ref currentScreenInfo, da, tripleDES, whoAmIPersonID);
							goto IL_13B4;
						case 803:
							control = DynamicScreen.AddInfoDisplayBox(dynamicControl, controlListRow, ref currentScreenInfo, lookupTablesForControls, whoAmIPersonID, da, tripleDES);
							goto IL_13B4;
						case 804:
							control = DynamicScreen.AddCalcButton(dynamicControl, controlListRow, ref currentScreenInfo, lockImage, true, whoAmIPersonID, whoAmIName);
							goto IL_13B4;
						case 805:
							control = DynamicScreen.AddPMTable(dynamicControl, controlListRow, ref currentScreenInfo, da, tripleDES, eventHandlers, whoAmIPersonID);
							goto IL_13B4;
						case 806:
							control = DynamicScreen.AddCaseComboBox(dynamicControl, controlListRow, ref currentScreenInfo, ref comboBoxData, da, lockImage, lookupTablesForControls, whoAmIPersonID, tripleDES);
							goto IL_13B4;
						case 807:
							control = DynamicScreen.AddEmailHistory(dynamicControl, controlListRow, ref currentScreenInfo, da, tripleDES, whoAmIPersonID);
							goto IL_13B4;
						case 808:
							control = DynamicScreen.AddAppointmentHistory(dynamicControl, controlListRow, ref currentScreenInfo, da, tripleDES, whoAmIPersonID);
							goto IL_13B4;
						}
						break;
					}
				}
				return null;
				IL_13B4:
				if (toolTip != null && control != null && !string.IsNullOrEmpty(dynamicControl.HelpText) && !dynamicControl.IsLabel)
				{
					toolTip.SetToolTip(control, dynamicControl.HelpText);
				}
				result = control;
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00022D7C File Offset: 0x00021D7C
		private static Control AddListSelect(DynamicControl dc, ref ScreenInfo currentScreenInfo)
		{
			int setting = dc.Setting2;
			string setting4String = dc.Setting4String;
			bool flag = dc.Setting3 != 0;
			ListSelect currentListSelect = currentScreenInfo.CurrentListSelect;
			Control result;
			if (currentListSelect == null)
			{
				currentScreenInfo.CurrentListSelect = new ListSelect();
				currentListSelect = currentScreenInfo.CurrentListSelect;
				if (flag)
				{
					currentListSelect.ConvertToDropList();
					currentListSelect.Height = currentListSelect.Font.Height + 8;
				}
				currentListSelect.addItem(new ListSelectItem(dc.ControlCaption, dc.ControlId));
				Control[] controls = new Control[]
				{
					currentListSelect
				};
				int[] verticalPad = new int[1];
				DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, verticalPad);
				if (setting > 0 && !flag)
				{
					currentListSelect.Height = DynamicScreen.RowCountToPixelHeight(setting, currentListSelect.Font);
				}
				int effectiveWidthForControl = currentScreenInfo.GetEffectiveWidthForControl();
				currentListSelect.Width = ((effectiveWidthForControl > 0) ? effectiveWidthForControl : 2);
				if (setting4String.Length > 0)
				{
					string[] array = setting4String.Split(new char[]
					{
						'`'
					});
					currentListSelect.List1Label = ((array.Length > 0) ? array[0] : "");
					currentListSelect.List2Label = ((array.Length > 1) ? array[1] : "");
				}
				result = currentListSelect;
			}
			else
			{
				currentListSelect.addItem(new ListSelectItem(dc.ControlCaption, dc.ControlId));
				if (setting > 0 && !flag)
				{
					currentListSelect.Height = DynamicScreen.RowCountToPixelHeight(setting, currentListSelect.Font);
				}
				if (setting4String.Length > 0)
				{
					string[] array = setting4String.Split(new char[]
					{
						'`'
					});
					currentListSelect.List1Label = ((array.Length > 0) ? array[0] : "");
					currentListSelect.List2Label = ((array.Length > 1) ? array[1] : "");
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00022F78 File Offset: 0x00021F78
		private static Control AddIndent(DataRow controlListRow, ref ScreenInfo currentScreenInfo)
		{
			int num = (int)controlListRow[4];
			currentScreenInfo.currentIndent += num;
			return null;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00022FA8 File Offset: 0x00021FA8
		private static Control AddMultiCheckBoxHeader(DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			DynamicControl dynamicControl = new DynamicControl(controlListRow);
			string[] colCaptions = dynamicControl.ControlCaptionForDisplay.Split(new char[]
			{
				'.'
			});
			MyMultiCheckbox myMultiCheckbox = new MyMultiCheckbox(currentScreenInfo.ColumnWidth, colCaptions);
			DynamicScreen.AddControl(myMultiCheckbox, ref currentScreenInfo, 0);
			return myMultiCheckbox;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00022FF8 File Offset: 0x00021FF8
		private static Control AddPicture(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			CtrlPicture ctrlPicture = new CtrlPicture();
			ctrlPicture.Tag = controlListRow;
			if (dc.Setting1 > 0)
			{
				ctrlPicture.Height = dc.Setting1;
			}
			BorderStyle setting = (BorderStyle)dc.Setting2;
			ctrlPicture.BorderStyle = setting;
			ctrlPicture.Width = currentScreenInfo.GetEffectiveWidthForControl();
			DynamicScreen.AddControl(ctrlPicture, ref currentScreenInfo, 0);
			return ctrlPicture;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00023060 File Offset: 0x00022060
		private static Control AddDynamicControlsChooser(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			bool showDisabledForms = dc.Setting3 == 1;
			DynamicControlChooser dynamicControlChooser = new DynamicControlChooser();
			if (setting > 0)
			{
				dynamicControlChooser.Height = setting;
			}
			dynamicControlChooser.Initialize(da, tripleDES, showDisabledForms, dc.DefaultValueString, new int[]
			{
				setting2
			});
			dynamicControlChooser.Tag = controlListRow;
			DynamicScreen.AddControl(dynamicControlChooser, ref currentScreenInfo, 0);
			return dynamicControlChooser;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000230DC File Offset: 0x000220DC
		private static Control AddFormSummary(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, PanelInfo panelInfo)
		{
			int setting = dc.Setting4;
			MyWebBrowser myWebBrowser = new MyWebBrowser();
			myWebBrowser.AccessibleDescription = dc.ControlCaptionForDisplay;
			myWebBrowser.AccessibleName = dc.ControlCaptionForDisplay;
			if (setting > 0)
			{
				myWebBrowser.Height = setting;
			}
			myWebBrowser.Width = currentScreenInfo.columnWidth;
			if (panelInfo != null)
			{
				if (panelInfo.panel is MyPanel)
				{
					myWebBrowser.MyPanel = (MyPanel)panelInfo.panel;
				}
			}
			myWebBrowser.AccessibleName = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			myWebBrowser.AccessibleDescription = myWebBrowser.AccessibleName;
			myWebBrowser.Title = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			myWebBrowser.Tag = controlListRow;
			DynamicScreen.AddControl(myWebBrowser, ref currentScreenInfo, 2, dc.DontWrapToNextLine);
			return myWebBrowser;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000231B8 File Offset: 0x000221B8
		private static Control AddLink(int templateId, DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, PanelInfo panelInfo)
		{
			bool flag = dc.Setting2 != 0;
			CtrlLinkLabel ctrlLinkLabel = new CtrlLinkLabel();
			ctrlLinkLabel.TemplateId = templateId;
			ctrlLinkLabel.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			int num = currentScreenInfo.GetEffectiveWidthForControl();
			if (dc.DontWrapToNextLine)
			{
				num = Convert.ToInt32(num / 2) - 4;
			}
			if (!flag)
			{
				ctrlLinkLabel.Width = num;
			}
			using (Graphics graphics = ctrlLinkLabel.CreateGraphics())
			{
				ctrlLinkLabel.Height = Convert.ToInt32(graphics.MeasureString(ctrlLinkLabel.Text, ctrlLinkLabel.Font, ctrlLinkLabel.Width).Height);
				if (dc.DontWrapToNextLine && flag)
				{
					if (ctrlLinkLabel.Text.Length > 0 && ctrlLinkLabel.Text[ctrlLinkLabel.Text.Length - 1] != ':')
					{
						CtrlLinkLabel ctrlLinkLabel2 = ctrlLinkLabel;
						ctrlLinkLabel2.Text += ':';
					}
					ctrlLinkLabel.Width = Convert.ToInt32(graphics.MeasureString(ctrlLinkLabel.Text, ctrlLinkLabel.Font).Width) + 5;
				}
			}
			if (flag && !dc.DontWrapToNextLine)
			{
				int height = ctrlLinkLabel.Height;
				ctrlLinkLabel.AutoSize = true;
				int width = ctrlLinkLabel.Width;
				ctrlLinkLabel.AutoSize = false;
				ctrlLinkLabel.Width = width;
				ctrlLinkLabel.Height = height;
			}
			if (!dc.Enabled)
			{
				ctrlLinkLabel.Enabled = false;
			}
			DynamicScreen.AddControl(ctrlLinkLabel, ref currentScreenInfo, 2, dc.DontWrapToNextLine);
			return ctrlLinkLabel;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00023380 File Offset: 0x00022380
		private static Control AddButtonSignatureButton(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, PanelInfo panelInfo)
		{
			CtrlSignedDocumentButton ctrlSignedDocumentButton = new CtrlSignedDocumentButton();
			ctrlSignedDocumentButton.ControlId = dc.ControlId;
			ctrlSignedDocumentButton.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			ctrlSignedDocumentButton.Tag = controlListRow;
			if (dc.HasSpecialInstructions)
			{
				string text = dc.SpecialInstructionsNoNull("templateid").ToLower();
				int templateId;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out templateId))
				{
					ctrlSignedDocumentButton.TemplateId = templateId;
				}
			}
			int defaultValue = dc.DefaultValue;
			if (defaultValue > 0)
			{
				if (defaultValue == 0)
				{
					Font font = currentScreenInfo.font;
				}
				else
				{
					double num = Convert.ToDouble(defaultValue) / 100.0;
					int num2 = Convert.ToInt32((double)currentScreenInfo.font.Size * num);
					if (num2 > 0)
					{
						Font font = new Font(currentScreenInfo.font.FontFamily, (float)num2);
					}
					else
					{
						Font font = currentScreenInfo.font;
					}
				}
			}
			else
			{
				ctrlSignedDocumentButton.AutoSize = true;
			}
			if (!dc.Enabled)
			{
				ctrlSignedDocumentButton.Visible = false;
			}
			DynamicScreen.AddControl(ctrlSignedDocumentButton, ref currentScreenInfo, 2, dc.DontWrapToNextLine);
			return ctrlSignedDocumentButton;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000234C4 File Offset: 0x000224C4
		private static Control AddButton(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, PanelInfo panelInfo)
		{
			Button button = new Button();
			button.AutoSize = true;
			button.Text = dc.ControlCaptionForDisplay;
			button.Tag = controlListRow;
			if (!dc.Enabled)
			{
				button.Visible = false;
			}
			DynamicScreen.AddControl(button, ref currentScreenInfo, 2, dc.DontWrapToNextLine);
			return button;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0002351C File Offset: 0x0002251C
		private static Control AddLabel(DataRow controlListRow, ref ScreenInfo currentScreenInfo, PanelInfo panelInfo)
		{
			DynamicControl dynamicControl = new DynamicControl(controlListRow);
			if (dynamicControl.HasSpecialInstructions)
			{
				string text = dynamicControl.SpecialInstructionsNoNull("isButton").ToLower();
				bool flag = text.Length > 0 && "1yestrue".IndexOf(text) >= 0;
				if (flag)
				{
					return DynamicScreen.AddButton(dynamicControl, controlListRow, ref currentScreenInfo, panelInfo);
				}
			}
			if (dynamicControl.HasSpecialInstructions)
			{
				string text = dynamicControl.SpecialInstructionsNoNull("isButtonSig").ToLower();
				bool flag = text.Length > 0 && "1yestrue".IndexOf(text) >= 0;
				if (flag)
				{
					return DynamicScreen.AddButtonSignatureButton(dynamicControl, controlListRow, ref currentScreenInfo, panelInfo);
				}
				string text2 = dynamicControl.SpecialInstructionsNoNull("isLink").ToLower();
				bool flag2 = text2.Length > 0 && "1yestrue".IndexOf(text2) >= 0;
				if (flag2)
				{
					string s = dynamicControl.SpecialInstructionsNoNull("templateId");
					int templateId;
					int.TryParse(s, out templateId);
					return DynamicScreen.AddLink(templateId, dynamicControl, controlListRow, ref currentScreenInfo, panelInfo);
				}
			}
			bool flag3 = dynamicControl.Setting3 == 1;
			Control result;
			if (flag3)
			{
				result = DynamicScreen.AddFormSummary(dynamicControl, controlListRow, ref currentScreenInfo, panelInfo);
			}
			else
			{
				int setting = dynamicControl.Setting1;
				int defaultValue = dynamicControl.DefaultValue;
				int setting2 = dynamicControl.Setting4;
				bool flag4 = dynamicControl.Setting2 != 0;
				MyLabel myLabel = new MyLabel();
				myLabel.Tag = controlListRow;
				FontStyle newStyle = (FontStyle)setting;
				Font prototype;
				if (defaultValue == 0)
				{
					prototype = currentScreenInfo.font;
				}
				else
				{
					double num = Convert.ToDouble(defaultValue) / 100.0;
					int num2 = Convert.ToInt32((double)currentScreenInfo.font.Size * num);
					if (num2 > 0)
					{
						prototype = new Font(currentScreenInfo.font.FontFamily, (float)num2);
					}
					else
					{
						prototype = currentScreenInfo.font;
					}
				}
				myLabel.Font = new Font(prototype, newStyle);
				myLabel.Text = DynamicScreen.GetControlCaptionForDisplay(dynamicControl, currentScreenInfo.UseFrench);
				int num3 = currentScreenInfo.GetEffectiveWidthForControl();
				if (dynamicControl.HasSpecialInstructions)
				{
					string text3 = dynamicControl.SpecialInstructionsNoNull("align").ToLower();
					if (text3.Equals("right"))
					{
						myLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					}
					else if (text3.Equals("center"))
					{
						myLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
					}
				}
				if (dynamicControl.HasSpecialInstructions)
				{
					string text4 = dynamicControl.SpecialInstructionsNoNull("forecolour").Trim();
					if (text4.Length > 0)
					{
						try
						{
							int argb;
							if (char.IsDigit(text4[0]) && int.TryParse(text4, out argb))
							{
								myLabel.ForeColor = Color.FromArgb(argb);
							}
							else
							{
								myLabel.ForeColor = Color.FromName(text4);
							}
						}
						catch
						{
						}
					}
				}
				if (dynamicControl.DontWrapToNextLine)
				{
					num3 = Convert.ToInt32(num3 / 2) - 4;
				}
				if (!flag4)
				{
					myLabel.Width = num3;
				}
				using (Graphics graphics = myLabel.CreateGraphics())
				{
					myLabel.Height = Convert.ToInt32(graphics.MeasureString(myLabel.Text, myLabel.Font, myLabel.Width).Height);
					if (dynamicControl.DontWrapToNextLine && flag4)
					{
						if (myLabel.Text.Length > 0 && myLabel.Text[myLabel.Text.Length - 1] != ':')
						{
							MyLabel myLabel2 = myLabel;
							myLabel2.Text += ':';
						}
						myLabel.Width = Convert.ToInt32(graphics.MeasureString(myLabel.Text, myLabel.Font).Width) + 5;
					}
				}
				if (flag4 && !dynamicControl.DontWrapToNextLine)
				{
					int height = myLabel.Height;
					myLabel.AutoSize = true;
					int width = myLabel.Width;
					myLabel.AutoSize = false;
					myLabel.Width = width;
					myLabel.Height = height;
				}
				if (setting2 > 0)
				{
					myLabel.Width -= setting2;
				}
				if (dynamicControl.HelpText.Length > 0)
				{
					myLabel.HelpText = dynamicControl.HelpText;
					myLabel.Height += 5;
				}
				if (!dynamicControl.Enabled)
				{
					myLabel.Visible = false;
				}
				DynamicScreen.AddControl(myLabel, ref currentScreenInfo, 2, dynamicControl.DontWrapToNextLine);
				if (setting2 > 0)
				{
					myLabel.Left += setting2;
				}
				result = myLabel;
			}
			return result;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00023A68 File Offset: 0x00022A68
		private static Control AddMyCheckbox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo)
		{
			return DynamicScreen.AddCheckbox(dc, controlListRow, ref currentScreenInfo, false);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00023A84 File Offset: 0x00022A84
		private static Control AddAccommodationCheckbox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting3;
			int width = Convert.ToInt32(currentScreenInfo.columnWidth);
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			AccommodationControl2 accommodationControl = new AccommodationControl2();
			accommodationControl.AccommodationControlType = AccommodationControlType.CheckBox;
			accommodationControl.Caption = (dc.HideCaption ? "" : dc.ControlCaptionForDisplay);
			accommodationControl.Width = width;
			int defaultValue = dc.DefaultValue;
			if (defaultValue > 0)
			{
				accommodationControl.SetIndent(defaultValue);
			}
			accommodationControl.DefaultShowOnLetter = dc.ShowOnLetter;
			if (dc.ReadOnly)
			{
				accommodationControl.SetReadOnly();
			}
			accommodationControl.Tag = controlListRow;
			DynamicScreen.AddControlsHorizontal(new Control[]
			{
				accommodationControl
			}, ref currentScreenInfo, 3, dc.DontWrapToNextLine, dc);
			return accommodationControl;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00023B68 File Offset: 0x00022B68
		private static Control AddAccommodationTextbox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting3;
			int setting2 = dc.Setting1;
			int width = Convert.ToInt32(currentScreenInfo.columnWidth);
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			AccommodationControl2 accommodationControl = new AccommodationControl2();
			accommodationControl.AccommodationControlType = AccommodationControlType.TextBox;
			accommodationControl.Caption = (dc.HideCaption ? "" : dc.ControlCaptionForDisplay);
			accommodationControl.Width = width;
			int defaultValue = dc.DefaultValue;
			if (defaultValue > 0)
			{
				accommodationControl.SetIndent(defaultValue);
			}
			if (setting2 > 1)
			{
				accommodationControl.Txt.Multiline = true;
				accommodationControl.Txt.ScrollBars = ScrollBars.Vertical;
				accommodationControl.Height = (accommodationControl.Txt.Font.Height + 3) * setting2;
			}
			accommodationControl.DefaultShowOnLetter = dc.ShowOnLetter;
			if (dc.ReadOnly)
			{
				accommodationControl.SetReadOnly();
			}
			accommodationControl.Tag = controlListRow;
			DynamicScreen.AddControl(accommodationControl, ref currentScreenInfo, 0, dc.DontWrapToNextLine);
			return accommodationControl;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00023C8C File Offset: 0x00022C8C
		private static Control AddAccommodationDroplist(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			int setting = dc.Setting3;
			if (dc == null)
			{
				dc = new DynamicControl(controlListRow);
			}
			int setting2 = dc.Setting1;
			int setting3 = dc.Setting2;
			int setting4 = dc.Setting3;
			int setting5 = dc.Setting4;
			AccommodationControlType accommodationControlType;
			if (setting4 == 0)
			{
				accommodationControlType = AccommodationControlType.ComboBoxSimple;
			}
			else
			{
				accommodationControlType = AccommodationControlType.ComboText;
			}
			int width = Convert.ToInt32(currentScreenInfo.columnWidth);
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			AccommodationControl2 accommodationControl = new AccommodationControl2();
			accommodationControl.AccommodationControlType = accommodationControlType;
			accommodationControl.Caption = (dc.HideCaption ? "" : dc.ControlCaptionForDisplay);
			accommodationControl.Width = width;
			int defaultValue = dc.DefaultValue;
			if (defaultValue > 0)
			{
				accommodationControl.SetIndent(defaultValue);
			}
			accommodationControl.DefaultShowOnLetter = dc.ShowOnLetter;
			if (dc.ReadOnly)
			{
				accommodationControl.SetReadOnly();
			}
			accommodationControl.Tag = controlListRow;
			Control[] array;
			AutoComboBox autoComboBox = DynamicScreen.CreateComboBox(dc, setting2, (int)controlListRow[7], setting4, true, setting5, currentScreenInfo, comboBoxData, false, da, out array, tripleDES);
			accommodationControl.Cmb.DataSource = autoComboBox.DataSource;
			accommodationControl.Cmb.DisplayMember = autoComboBox.DisplayMember;
			accommodationControl.Cmb.ValueMember = autoComboBox.ValueMember;
			accommodationControl.Cmb.defaultIndex = autoComboBox.defaultIndex;
			accommodationControl.Cmb.DropDownStyle = autoComboBox.DropDownStyle;
			autoComboBox.Dispose();
			array = null;
			DynamicScreen.AddControl(accommodationControl, ref currentScreenInfo, 0, dc.DontWrapToNextLine);
			return accommodationControl;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00023E50 File Offset: 0x00022E50
		private static Control AddAccommodationDatePicker(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting3;
			int width = Convert.ToInt32(currentScreenInfo.columnWidth);
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			AccommodationControl2 accommodationControl = new AccommodationControl2();
			accommodationControl.AccommodationControlType = AccommodationControlType.Date;
			accommodationControl.Caption = (dc.HideCaption ? "" : dc.ControlCaptionForDisplay);
			accommodationControl.Width = width;
			int defaultValue = dc.DefaultValue;
			if (defaultValue > 0)
			{
				accommodationControl.SetIndent(defaultValue);
			}
			accommodationControl.DefaultShowOnLetter = dc.ShowOnLetter;
			if (dc.ReadOnly)
			{
				accommodationControl.SetReadOnly();
			}
			accommodationControl.Tag = controlListRow;
			DynamicScreen.AddControl(accommodationControl, ref currentScreenInfo, 0, dc.DontWrapToNextLine);
			return accommodationControl;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00023F28 File Offset: 0x00022F28
		private static Control AddMultiCheckBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting3;
			string[] colCaptions = dc.ControlCaption.Split(new char[]
			{
				'.'
			});
			MyMultiCheckbox myMultiCheckbox = new MyMultiCheckbox(colCaptions, dc.HideCaption);
			Font font;
			if (setting == 0)
			{
				font = currentScreenInfo.font;
			}
			else
			{
				double num = Convert.ToDouble(setting) / 100.0;
				int num2 = Convert.ToInt32((double)currentScreenInfo.font.Size * num);
				if (num2 > 0)
				{
					font = new Font(currentScreenInfo.font.FontFamily, (float)num2);
				}
				else
				{
					font = currentScreenInfo.font;
				}
			}
			myMultiCheckbox.SetFont(font);
			int width = currentScreenInfo.GetEffectiveWidthForControl();
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			myMultiCheckbox.Width = width;
			int width2 = CheckBoxRenderer.GetGlyphSize(currentScreenInfo.graphics, CheckBoxState.UncheckedNormal).Width;
			myMultiCheckbox.Height = Convert.ToInt32(currentScreenInfo.graphics.MeasureString(myMultiCheckbox.Text, myMultiCheckbox.GetFont(), myMultiCheckbox.GetLastCheckbox().Width - width2 - 2).Height + (float)(myMultiCheckbox.GetLastCheckbox().Top * 2));
			myMultiCheckbox.Tag = controlListRow;
			DynamicScreen.AddControl(myMultiCheckbox, ref currentScreenInfo, 0, dc.DontWrapToNextLine);
			return myMultiCheckbox;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00024098 File Offset: 0x00023098
		private static Control AddMultiDatabaseItemChooser(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmIPersonId)
		{
			int setting = dc.Setting1;
			string defaultValueString = dc.DefaultValueString;
			int defaultValue = dc.DefaultValue;
			bool flag = dc.Setting2 == 1;
			AutoComboBox.MyControls.MultiDatabaseItemSelect multiDatabaseItemSelect = new AutoComboBox.MyControls.MultiDatabaseItemSelect();
			if (flag)
			{
				multiDatabaseItemSelect.MultipleChecksAllowed = false;
			}
			multiDatabaseItemSelect.Initialize(dc.ControlCaptionForDisplay, defaultValueString, da, tripleDES);
			if (defaultValue != 0)
			{
				multiDatabaseItemSelect.SelectAll();
			}
			if (setting > 0)
			{
				multiDatabaseItemSelect.Height = setting;
			}
			int width = currentScreenInfo.GetEffectiveWidthForControl();
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			multiDatabaseItemSelect.Width = width;
			if (dc.HideCaption)
			{
				multiDatabaseItemSelect.HideCaption();
			}
			multiDatabaseItemSelect.Tag = controlListRow;
			DynamicScreen.AddControl(multiDatabaseItemSelect, ref currentScreenInfo, 0, dc.DontWrapToNextLine);
			return multiDatabaseItemSelect;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00024188 File Offset: 0x00023188
		private static Control AddCheckbox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, bool regularCheckbox)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int defaultValue = dc.DefaultValue;
			int setting3 = dc.Setting3;
			int setting4 = dc.Setting4;
			bool flag = (defaultValue & 1) == 1;
			int num = defaultValue >> 1;
			MyCheckBox myCheckBox = new MyCheckBox();
			if (num > 0)
			{
				currentScreenInfo.currentIndent += num;
			}
			if (setting > 0)
			{
				myCheckBox.SetEnabledControlId = setting;
			}
			if (setting2 > 0)
			{
				myCheckBox.AutoCheckThisBoxWhenOtherControlModified_cid = setting2;
			}
			myCheckBox.Tag = controlListRow;
			if (flag)
			{
				myCheckBox.Checked = true;
			}
			myCheckBox.FlatStyle = FlatStyle.System;
			if (dc.ReadOnly)
			{
				myCheckBox.Enabled = false;
			}
			Font font;
			if (setting3 == 0)
			{
				font = currentScreenInfo.font;
			}
			else
			{
				double num2 = Convert.ToDouble(setting3) / 100.0;
				int num3 = Convert.ToInt32((double)currentScreenInfo.font.Size * num2);
				if (num3 > 0)
				{
					font = new Font(currentScreenInfo.font.FontFamily, (float)num3);
				}
				else
				{
					font = currentScreenInfo.font;
				}
			}
			myCheckBox.Font = font;
			if (setting4 != 0)
			{
				myCheckBox.BackColor = Color.FromArgb(setting4);
				myCheckBox.ForeColor = Color.Black;
			}
			myCheckBox.Text = (dc.HideCaption ? " " : DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench));
			int num4 = currentScreenInfo.GetEffectiveWidthForControl();
			num4 += num;
			if (dc.DontWrapToNextLine)
			{
				num4 = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			myCheckBox.Width = num4;
			int width = CheckBoxRenderer.GetGlyphSize(currentScreenInfo.graphics, CheckBoxState.UncheckedNormal).Width;
			myCheckBox.Height = Convert.ToInt32(currentScreenInfo.graphics.MeasureString(myCheckBox.Text, myCheckBox.Font, myCheckBox.Width - width - 2).Height);
			if (currentScreenInfo.radioGroupBehindMultipleCheckboxes != null)
			{
				MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = new MyRadioGroupPrimaryCheckboxMultiple();
				if (currentScreenInfo.radioGroupBehindMultipleCheckboxes.ReadOnly)
				{
					myRadioGroupPrimaryCheckboxMultiple.ReadOnlyPrimary = true;
				}
				if (dc.ReadOnly)
				{
					myRadioGroupPrimaryCheckboxMultiple.ReadOnlyPrimary = true;
					myRadioGroupPrimaryCheckboxMultiple.ReadOnlySecondary = true;
				}
				if (dc.HasSpecialInstructions)
				{
					string text = dc.SpecialInstructions("hideprimary");
					string text2 = dc.SpecialInstructions("hidesecondary");
					if (text != null && text.CompareTo("1") == 0)
					{
						myRadioGroupPrimaryCheckboxMultiple.HidePrimary();
					}
					if (text2 != null && text2.CompareTo("1") == 0)
					{
						myRadioGroupPrimaryCheckboxMultiple.HideSecondary();
					}
					string text3 = dc.SpecialInstructions("disableprimary");
					string text4 = dc.SpecialInstructions("disablesecondary");
					if (text3 != null && text3.CompareTo("1") == 0)
					{
						myRadioGroupPrimaryCheckboxMultiple.ReadOnlyPrimary = true;
					}
					if (text4 != null && text4.CompareTo("1") == 0)
					{
						myRadioGroupPrimaryCheckboxMultiple.ReadOnlySecondary = true;
					}
					string text5 = dc.SpecialInstructions("allowboth");
					if (text5 != null && text5.CompareTo("1") == 0)
					{
						myRadioGroupPrimaryCheckboxMultiple.AllowBoth = true;
					}
				}
				myRadioGroupPrimaryCheckboxMultiple.Tag = controlListRow;
				myRadioGroupPrimaryCheckboxMultiple.Width = myCheckBox.Width;
				myRadioGroupPrimaryCheckboxMultiple.Text = myCheckBox.Text;
				if (setting > 0)
				{
					myRadioGroupPrimaryCheckboxMultiple.MyCheckbox.SetEnabledControlId = setting;
				}
				if (setting2 > 0)
				{
					myRadioGroupPrimaryCheckboxMultiple.AutoCheckThisBoxWhenOtherControlModified_cid = setting2;
				}
				if (!dc.Enabled)
				{
					myRadioGroupPrimaryCheckboxMultiple.Visible = false;
				}
				DynamicScreen.AddControl(myRadioGroupPrimaryCheckboxMultiple, ref currentScreenInfo, 2, dc.DontWrapToNextLine);
			}
			else
			{
				if (!dc.Enabled)
				{
					myCheckBox.Visible = false;
				}
				DynamicScreen.AddControl(myCheckBox, ref currentScreenInfo, 2, dc.DontWrapToNextLine);
			}
			if (num > 0)
			{
				currentScreenInfo.currentIndent -= num;
			}
			return myCheckBox;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000245F4 File Offset: 0x000235F4
		private static Control AddRadioGroup(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, ref DataSet comboBoxData, UnivDataAdapter da, Image lockImage)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			bool flag = dc.Setting4 == 1;
			bool flag2 = dc.Setting4 == 2;
			bool flag3 = flag || flag2;
			int num = currentScreenInfo.columnWidth - currentScreenInfo.currentX;
			num -= currentScreenInfo.tempOffsetX;
			Control result;
			if (flag3)
			{
				currentScreenInfo.radioGroupBehindMultipleCheckboxes = dc;
				MyRadioGroupPrimary myRadioGroupPrimary = new MyRadioGroupPrimary();
				if (dc.ReadOnly)
				{
					myRadioGroupPrimary.ReadOnlyPrimary = true;
				}
				myRadioGroupPrimary.Tag = controlListRow;
				myRadioGroupPrimary.Width = num;
				if (!dc.Enabled)
				{
					myRadioGroupPrimary.Visible = false;
				}
				DynamicScreen.AddControl(myRadioGroupPrimary, ref currentScreenInfo, 6, dc.DontWrapToNextLine);
				result = myRadioGroupPrimary;
			}
			else
			{
				MyRadioGroup myRadioGroup = new MyRadioGroup();
				if (currentScreenInfo != null && currentScreenInfo.parentControl != null)
				{
					myRadioGroup.BackColor = currentScreenInfo.parentControl.BackColor;
				}
				myRadioGroup.Tag = controlListRow;
				myRadioGroup.Font = currentScreenInfo.font;
				if (dc.ReadOnly)
				{
					myRadioGroup.Enabled = false;
				}
				MyRadioGroup.DisplayFormat displayFormat;
				try
				{
					displayFormat = (MyRadioGroup.DisplayFormat)setting3;
				}
				catch
				{
					displayFormat = MyRadioGroup.DisplayFormat.NoLabel;
				}
				if (displayFormat != MyRadioGroup.DisplayFormat.NoLabel)
				{
					myRadioGroup.DisplayType = displayFormat;
				}
				myRadioGroup.AccessibleName = dc.ControlCaptionForDisplay;
				myRadioGroup.TabStop = true;
				myRadioGroup.Title = dc.ControlCaptionForDisplay;
				myRadioGroup.Width = num;
				myRadioGroup.NumHorizontal = setting2;
				int num2 = -1;
				if (setting > 0)
				{
					num2 = (int)controlListRow[7];
					DataTable dataTable = DynamicScreen.GetLookupList(setting, false, num2, ref comboBoxData, da, currentScreenInfo.UseFrench);
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						object obj2 = dataRow["LookupText"];
						if (obj2 != DBNull.Value)
						{
							string text = (string)obj2;
							int num3 = text.IndexOf('`');
							if (num3 > 0)
							{
								dataRow["LookupText"] = text.Substring(0, num3);
							}
							else if (num3 == 0)
							{
								dataRow["LookupText"] = "";
							}
						}
					}
					myRadioGroup.DataSource = new DataView(dataTable);
					myRadioGroup.DisplayMember = "LookupText";
					myRadioGroup.ValueMember = "LookupListID";
				}
				else
				{
					DataTable dataTable = null;
				}
				Control[] controls = new Control[]
				{
					myRadioGroup
				};
				if (!dc.Enabled)
				{
					myRadioGroup.Visible = false;
				}
				DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc);
				myRadioGroup.DefaultId = num2;
				result = myRadioGroup;
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00024924 File Offset: 0x00023924
		private static int GetChildLookupGroupId(DataSet comboBoxData, int lookupGroupId)
		{
			if (comboBoxData.Tables.Contains("child"))
			{
				DataTable dataTable = comboBoxData.Tables["child"];
				string strB = "d" + lookupGroupId.ToString();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = (string)dataRow[0];
					if (text.CompareTo(strB) == 0)
					{
						return (int)dataRow[1];
					}
				}
			}
			return 0;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00024A08 File Offset: 0x00023A08
		private static AutoComboBox CreateComboBox(DynamicControl dc, int lookupGroupID, int defaultValue, int comboType, bool hideCaption, int charWidth, ScreenInfo currentScreenInfo, DataSet comboBoxData, bool addControlsToScreen, UnivDataAdapter da, out Control[] controls, TripleDESEncryptionClass tripleDES)
		{
			return DynamicScreen.CreateComboBox(dc, lookupGroupID, defaultValue, comboType, hideCaption, charWidth, currentScreenInfo, comboBoxData, addControlsToScreen, da, out controls, "", "", tripleDES, eLabelOrientation.LabelLeft);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00024A40 File Offset: 0x00023A40
		private static AutoComboBox CreateComboBox(DynamicControl dc, int lookupGroupID, int defaultValue, int comboType, bool hideCaption, int charWidth, ScreenInfo currentScreenInfo, DataSet comboBoxData, bool addControlsToScreen, UnivDataAdapter da, out Control[] controls, string name, string sql, TripleDESEncryptionClass tripleDES)
		{
			return DynamicScreen.CreateComboBox(dc, lookupGroupID, defaultValue, comboType, hideCaption, charWidth, currentScreenInfo, comboBoxData, addControlsToScreen, da, out controls, name, sql, tripleDES, eLabelOrientation.LabelLeft);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00024A70 File Offset: 0x00023A70
		private static AutoComboBox CreateComboBox(DynamicControl dc, int lookupGroupID, int defaultValue, int comboType, bool hideCaption, int charWidth, ScreenInfo currentScreenInfo, DataSet comboBoxData, bool addControlsToScreen, UnivDataAdapter da, out Control[] controls, string name, string sql, TripleDESEncryptionClass tripleDES, eLabelOrientation labelOrientation)
		{
			Label label;
			if (dc.HideCaption || labelOrientation == eLabelOrientation.NoLabel)
			{
				label = null;
			}
			else
			{
				label = new Label();
				label.FlatStyle = FlatStyle.System;
				label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				label.Font = currentScreenInfo.labelFont;
				label.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
				if (labelOrientation == eLabelOrientation.LabelAbove)
				{
					label.Width = currentScreenInfo.columnWidth;
				}
				else
				{
					label.Width = currentScreenInfo.labelWidth;
				}
			}
			AutoComboBox autoComboBox = new AutoComboBox();
			autoComboBox.Font = currentScreenInfo.font;
			autoComboBox.Height = autoComboBox.Font.Height + 8;
			int num = currentScreenInfo.GetEffectiveWidthForControl();
			if (label != null && labelOrientation != eLabelOrientation.LabelAbove)
			{
				num -= label.Width;
			}
			if (charWidth > 0)
			{
				int num2 = Convert.ToInt32(autoComboBox.Font.Size * (float)charWidth + (float)(SystemInformation.VerticalScrollBarWidth * 2)) + 4;
				if (num > num2 && num2 > 30)
				{
					num = num2;
				}
			}
			autoComboBox.Width = num;
			if (dc.ReadOnly)
			{
				autoComboBox.Enabled = false;
			}
			int num3 = -1;
			bool flag = true;
			DataTable dataTable;
			if (lookupGroupID > 0)
			{
				num3 = defaultValue;
				dataTable = DynamicScreen.GetLookupList(lookupGroupID, flag, num3, ref comboBoxData, da, currentScreenInfo.UseFrench);
				int childLookupGroupId = DynamicScreen.GetChildLookupGroupId(comboBoxData, lookupGroupID);
				if (childLookupGroupId > 0)
				{
					autoComboBox.ChildLookupGroupId = childLookupGroupId;
				}
				autoComboBox.DataSource = dataTable;
				autoComboBox.DisplayMember = "LookupText";
				autoComboBox.ValueMember = "LookupListID";
				autoComboBox.LookupGroupId = lookupGroupID;
				if (comboType == 0)
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
					autoComboBox.AutoCompleteEnabled = false;
				}
				else if (comboType == 1 || comboType == -1)
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDown;
				}
				else if (comboType == 2)
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDown;
					autoComboBox.AllowUserToEnterAnyText = false;
				}
				else
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDown;
					autoComboBox.AllowUserToEnterAnyText = false;
				}
			}
			else
			{
				dataTable = null;
			}
			if (!string.IsNullOrEmpty(sql) && comboType != 0)
			{
				autoComboBox.Da = da;
				autoComboBox.TripleDES = tripleDES;
				autoComboBox.Sql = sql;
				dataTable = new DataTable();
				if (sql.IndexOf("@pid") < 0)
				{
					dataTable = autoComboBox.RunSql();
				}
			}
			if (dc.HideCaption || labelOrientation == eLabelOrientation.NoLabel)
			{
				controls = new Control[]
				{
					autoComboBox
				};
				if (!dc.Enabled)
				{
					autoComboBox.Visible = false;
				}
				if (addControlsToScreen)
				{
					DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc.DontWrapToNextLine, dc);
				}
			}
			else
			{
				int labelHeight = DynamicScreen.GetLabelHeight(label, autoComboBox, currentScreenInfo);
				label.Height = labelHeight;
				controls = new Control[]
				{
					label,
					autoComboBox
				};
				if (!dc.Enabled)
				{
					label.Visible = false;
					autoComboBox.Visible = false;
				}
				if (labelOrientation == eLabelOrientation.LabelAbove)
				{
					if (label != null)
					{
						label.Width = currentScreenInfo.columnWidth;
						int labelHeight2 = DynamicScreen.GetLabelHeight(label, null, currentScreenInfo);
						label.Height = labelHeight2;
					}
					autoComboBox.Width = currentScreenInfo.columnWidth;
				}
				if (addControlsToScreen)
				{
					if (labelOrientation == eLabelOrientation.LabelAbove)
					{
						DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, new int[]
						{
							2,
							6
						});
					}
					else
					{
						DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc);
					}
				}
			}
			if (num3 >= 0 && dataTable != null)
			{
				int num4 = -1;
				int num5;
				if (flag)
				{
					num5 = 1;
				}
				else
				{
					num5 = 0;
				}
				for (int i = num5; i < dataTable.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.Rows[i];
					int num6 = (int)dataRow[0];
					if (num6 == num3)
					{
						num4 = i;
					}
				}
				if (num4 < dataTable.Rows.Count)
				{
					autoComboBox.defaultIndex = num4;
				}
			}
			return autoComboBox;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00024F00 File Offset: 0x00023F00
		private static Control AddComboBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, ref DataSet comboBoxData, UnivDataAdapter da, Image lockImage, TripleDESEncryptionClass tripleDES)
		{
			if (dc == null)
			{
				dc = new DynamicControl(controlListRow);
			}
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			int setting4 = dc.Setting4;
			bool hideCaption = dc.HideCaption;
			bool flag = false;
			eLabelOrientation labelOrientation = eLabelOrientation.LabelLeft;
			string text2;
			string name;
			int num2;
			if (dc.HasSpecialInstructions)
			{
				string text = dc.SpecialInstructions("labelorientation");
				int num;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num))
				{
					if (Enum.IsDefined(typeof(eLabelOrientation), num))
					{
						labelOrientation = (eLabelOrientation)num;
					}
				}
				text2 = dc.SpecialInstructions("sql");
				if (!string.IsNullOrEmpty(text2))
				{
					name = dc.SpecialInstructions("name");
				}
				else
				{
					name = "_";
				}
				string text3 = dc.SpecialInstructions("valuecid");
				if (!string.IsNullOrEmpty(text3))
				{
					if (!int.TryParse(text3, out num2))
					{
						num2 = 0;
					}
				}
				else
				{
					num2 = 0;
				}
				string value = dc.SpecialInstructions("usemousewheel");
				if (!string.IsNullOrEmpty(value))
				{
					if (bool.TryParse(value, out flag) && flag)
					{
						flag = true;
					}
				}
			}
			else
			{
				text2 = "";
				name = "";
				num2 = 0;
			}
			Control[] array;
			AutoComboBox autoComboBox = DynamicScreen.CreateComboBox(dc, setting, (int)controlListRow[7], setting3, hideCaption, setting4, currentScreenInfo, comboBoxData, true, da, out array, name, text2, tripleDES, labelOrientation);
			autoComboBox.Tag = controlListRow;
			if (flag)
			{
				autoComboBox.IgnoreScrollWheel = false;
			}
			if (num2 > 0)
			{
				autoComboBox.CidToNotifyWithValueMember = num2;
			}
			return autoComboBox;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000250D0 File Offset: 0x000240D0
		private static Control AddMultiCheckBoxComboBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			int setting4 = dc.Setting4;
			AutoComboBox autoComboBox = new AutoComboBox();
			autoComboBox.Font = currentScreenInfo.font;
			autoComboBox.Height = autoComboBox.Font.Height + 8;
			int num = currentScreenInfo.GetEffectiveWidthForControl();
			if (setting4 > 0)
			{
				int num2 = Convert.ToInt32(autoComboBox.Font.Size * (float)setting4 + (float)(SystemInformation.VerticalScrollBarWidth * 2)) + 4;
				if (num > num2 && num2 > 30)
				{
					num = num2;
				}
			}
			autoComboBox.Width = num;
			if (dc.ReadOnly)
			{
				autoComboBox.Enabled = false;
			}
			int num3 = -1;
			bool flag = true;
			DataTable dataTable;
			if (setting > 0)
			{
				num3 = (int)controlListRow[7];
				dataTable = DynamicScreen.GetLookupList(setting, flag, num3, ref comboBoxData, da, currentScreenInfo.UseFrench);
				int childLookupGroupId = DynamicScreen.GetChildLookupGroupId(comboBoxData, setting);
				if (childLookupGroupId > 0)
				{
					autoComboBox.ChildLookupGroupId = childLookupGroupId;
				}
				autoComboBox.DataSource = dataTable;
				autoComboBox.DisplayMember = "LookupText";
				autoComboBox.ValueMember = "LookupListID";
				autoComboBox.LookupGroupId = setting;
				if (setting3 == 0)
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
					autoComboBox.AutoCompleteEnabled = false;
				}
				else if (setting3 == 1 || setting3 == -1)
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDown;
				}
				else if (setting3 == 2)
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDown;
					autoComboBox.AllowUserToEnterAnyText = false;
				}
				else
				{
					autoComboBox.DropDownStyle = ComboBoxStyle.DropDown;
					autoComboBox.AllowUserToEnterAnyText = false;
				}
			}
			else
			{
				dataTable = null;
			}
			Control[] controls = new Control[]
			{
				autoComboBox
			};
			if (num3 >= 0 && dataTable != null)
			{
				int num4 = -1;
				int num5;
				if (flag)
				{
					num5 = 1;
				}
				else
				{
					num5 = 0;
				}
				for (int i = num5; i < dataTable.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.Rows[i];
					int num6 = (int)dataRow[0];
					if (num6 == num3)
					{
						num4 = i;
					}
				}
				if (num4 < dataTable.Rows.Count)
				{
					autoComboBox.defaultIndex = num4;
				}
			}
			string[] colCaptions = dc.ControlCaptionForDisplay.Split(new char[]
			{
				'.'
			});
			MyMultiCheckbox myMultiCheckbox = new MyMultiCheckbox(currentScreenInfo.ColumnWidth, colCaptions, dc.HideCaption, controls);
			DynamicScreen.AddControl(myMultiCheckbox, ref currentScreenInfo, 0, dc.DontWrapToNextLine);
			myMultiCheckbox.Tag = controlListRow;
			return myMultiCheckbox;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000253BC File Offset: 0x000243BC
		private static Control AddInfoDisplayBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, DataSet lookupTablesForControls, int whoAmIPersonID, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			MyInfoBox myInfoBox = new MyInfoBox(da, tripleDES, dc.Setting1, dc.Setting2);
			if (dc.Setting3 > 0)
			{
				int num = myInfoBox.Font.Height + 2;
				myInfoBox.Height = num * dc.Setting3;
			}
			int effectiveWidthForControl = currentScreenInfo.GetEffectiveWidthForControl();
			myInfoBox.Width = effectiveWidthForControl;
			DynamicScreen.AddControl(myInfoBox, ref currentScreenInfo, 0);
			return myInfoBox;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00025430 File Offset: 0x00024430
		private static Control AddStaffComboBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, ref DataSet comboBoxData, UnivDataAdapter da, Image lockImage, DataSet lookupTablesForControls, int whoAmIPersonID, TripleDESEncryptionClass tripleDES)
		{
			int num = (int)controlListRow[4];
			if (num < 1)
			{
				num = 2;
			}
			eLabelOrientation eLabelOrientation = eLabelOrientation.LabelLeft;
			if (Enum.IsDefined(typeof(eLabelOrientation), dc.Setting4))
			{
				eLabelOrientation = (eLabelOrientation)dc.Setting4;
			}
			Label label;
			if (eLabelOrientation != eLabelOrientation.NoLabel && !dc.HideCaption)
			{
				label = new Label();
				label.FlatStyle = FlatStyle.System;
				label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				label.Font = currentScreenInfo.labelFont;
				label.Text = DynamicScreen.GetControlCaption(controlListRow, currentScreenInfo.UseFrench);
				label.Width = currentScreenInfo.labelWidth;
			}
			else
			{
				label = null;
			}
			AutoComboBox autoComboBox = new AutoComboBox();
			autoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			autoComboBox.Tag = controlListRow;
			autoComboBox.Font = currentScreenInfo.font;
			autoComboBox.Height = autoComboBox.Font.Height + 8;
			autoComboBox.Width = currentScreenInfo.columnWidth - ((label != null) ? label.Width : 0);
			if (dc.ReadOnly)
			{
				autoComboBox.Enabled = false;
			}
			int num2 = -1;
			DataTable dataTable = null;
			num2 = dc.DefaultValue;
			if (num2 == -2)
			{
				num2 = whoAmIPersonID;
				autoComboBox.defaultIndex = num2;
			}
			DataSet dataSet = ClientCache.CurrentInstance.lookupTablesForControls;
			if (dataSet == null)
			{
				dataSet = new DataSet();
				ClientCache.CurrentInstance.lookupTablesForControls = dataSet;
			}
			string text = "namestable2" + num.ToString();
			DataTable dataTable2;
			if (lookupTablesForControls.Tables.Contains(text))
			{
				dataTable2 = lookupTablesForControls.Tables[text];
			}
			else
			{
				da.SelectCommand.CommandText = "SELECT DISTINCT pg.personid,p.firstname,p.lastname,p.student_no FROM peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid WHERE pg.groupid=@gid AND NOT pg.personid IN (SELECT personid FROM people WHERE isactive=0)";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@gid", num);
				DataTable dataTable3 = new DataTable();
				da.Fill(dataTable3);
				DataTable dataTable4 = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable3, new string[]
				{
					"firstname",
					"lastname",
					"student_no"
				});
				dataTable4.TableName = text;
				dataTable4.Columns.Add("lastfirstname");
				DataRow dataRow = dataTable4.NewRow();
				dataRow[0] = -1;
				dataRow[1] = "";
				dataRow[2] = "";
				dataRow[3] = "";
				dataRow[4] = "";
				dataTable4.Rows.InsertAt(dataRow, 0);
				DataTable dataTable5 = dataTable4.Clone();
				dataTable5.TableName = dataTable4.TableName;
				List<int> list = new List<int>();
				foreach (object obj in dataTable4.Rows)
				{
					DataRow dataRow2 = (DataRow)obj;
					int item = (dataRow2["personid"] == DBNull.Value) ? 0 : ((int)dataRow2["personid"]);
					if (!list.Contains(item))
					{
						dataRow2["lastfirstname"] = string.Format("{0}, {1}", dataRow2["lastname"].ToString(), dataRow2["firstname"].ToString());
						dataTable5.ImportRow(dataRow2);
					}
				}
				lookupTablesForControls.Tables.Add(dataTable5);
				dataTable2 = dataTable5;
			}
			if (dataTable2 != null)
			{
				DataView dataView = new DataView(dataTable2);
				dataView.Sort = "lastfirstname";
				dataTable = dataTable2.Copy();
				autoComboBox.DataSource = dataView;
				autoComboBox.DisplayMember = "lastfirstname";
				autoComboBox.ValueMember = "personid";
			}
			if (label != null)
			{
				int labelHeight = DynamicScreen.GetLabelHeight(label, autoComboBox, currentScreenInfo);
				label.Height = labelHeight;
			}
			Control[] controls;
			if (label != null)
			{
				if (!dc.Enabled)
				{
					label.Visible = false;
					autoComboBox.Visible = false;
				}
				controls = new Control[]
				{
					label,
					autoComboBox
				};
			}
			else
			{
				if (!dc.Enabled)
				{
					autoComboBox.Visible = false;
				}
				controls = new Control[]
				{
					autoComboBox
				};
			}
			if (eLabelOrientation == eLabelOrientation.LabelAbove)
			{
				if (label != null)
				{
					label.Width = currentScreenInfo.columnWidth;
					int labelHeight2 = DynamicScreen.GetLabelHeight(label, null, currentScreenInfo);
					label.Height = labelHeight2;
				}
				autoComboBox.Width = currentScreenInfo.columnWidth;
				DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, new int[]
				{
					2,
					6
				});
			}
			else
			{
				DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc);
			}
			if (num2 >= 0 && dataTable != null)
			{
			}
			return autoComboBox;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00025990 File Offset: 0x00024990
		private static Control AddCaseComboBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, ref DataSet comboBoxData, UnivDataAdapter da, Image lockImage, DataSet lookupTablesForControls, int whoAmIPersonID, TripleDESEncryptionClass tripleDES)
		{
			int num = (int)controlListRow[4];
			if (num < 1)
			{
				num = 0;
			}
			Label label;
			if (!dc.HideCaption)
			{
				label = new Label();
				label.FlatStyle = FlatStyle.System;
				label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				label.Font = currentScreenInfo.labelFont;
				label.Text = DynamicScreen.GetControlCaption(controlListRow, currentScreenInfo.UseFrench);
				label.Width = currentScreenInfo.labelWidth;
			}
			else
			{
				label = null;
			}
			AutoComboBox autoComboBox = new AutoComboBox();
			autoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			autoComboBox.Tag = controlListRow;
			autoComboBox.Font = currentScreenInfo.font;
			autoComboBox.Height = autoComboBox.Font.Height + 8;
			autoComboBox.Width = currentScreenInfo.columnWidth - ((label != null) ? label.Width : 0);
			if (dc.ReadOnly)
			{
				autoComboBox.Enabled = false;
			}
			DataTable dataTable = null;
			int defaultValue = dc.DefaultValue;
			if (defaultValue == -2)
			{
			}
			string text = "casestable" + num.ToString();
			DataTable dataTable2;
			if (lookupTablesForControls != null && lookupTablesForControls.Tables.Contains(text))
			{
				dataTable2 = lookupTablesForControls.Tables[text];
			}
			else
			{
				text = "casestable" + num.ToString();
				if (lookupTablesForControls != null && lookupTablesForControls.Tables.Contains(text))
				{
					dataTable2 = lookupTablesForControls.Tables[text];
				}
				else
				{
					bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.InfoPc);
					if (lookupTablesForControls == null)
					{
						lookupTablesForControls = new DataSet();
					}
					DataTable dataTable4;
					if (flag)
					{
						da.SelectCommand.CommandText = "SELECT ipc.personid,CONVERT(VARCHAR(12), ipc.dateentered, 107) AS lastfirstname,'' AS firstname,'' AS lastname,ipc.student_no FROM infopc ipc ";
						da.SelectCommand.Parameters.Clear();
						DataTable dataTable3 = new DataTable();
						da.Fill(dataTable3);
						dataTable4 = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable3, new string[]
						{
							"student_no"
						});
					}
					else
					{
						dataTable4 = new DataTable();
					}
					dataTable4.TableName = text;
					DataRow dataRow = dataTable4.NewRow();
					dataRow[0] = -1;
					dataRow[1] = "";
					dataRow[2] = "";
					dataRow[3] = "";
					dataRow[4] = "";
					dataTable4.Rows.InsertAt(dataRow, 0);
					if (lookupTablesForControls == null)
					{
						lookupTablesForControls = new DataSet();
					}
					lookupTablesForControls.Tables.Add(dataTable4);
					dataTable2 = dataTable4;
				}
			}
			if (dataTable2 != null)
			{
				DataView dataView = new DataView(dataTable2);
				dataView.Sort = "lastfirstname";
				dataTable = dataTable2.Copy();
				autoComboBox.DataSource = dataView;
				autoComboBox.DisplayMember = "lastfirstname";
				autoComboBox.ValueMember = "personid";
			}
			if (label != null)
			{
				int labelHeight = DynamicScreen.GetLabelHeight(label, autoComboBox, currentScreenInfo);
				label.Height = labelHeight;
			}
			Control[] controls;
			if (label != null)
			{
				controls = new Control[]
				{
					label,
					autoComboBox
				};
			}
			else
			{
				controls = new Control[]
				{
					autoComboBox
				};
			}
			DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc);
			if (defaultValue >= 0 && dataTable != null)
			{
				autoComboBox.defaultIndex = defaultValue;
			}
			return autoComboBox;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00025D1C File Offset: 0x00024D1C
		private static Control AddRadioButton(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo)
		{
			int defaultValue = dc.DefaultValue;
			RadioButton radioButton = new RadioButton();
			radioButton.Tag = controlListRow;
			radioButton.TabStop = true;
			if (defaultValue > 0)
			{
				radioButton.Checked = true;
			}
			radioButton.FlatStyle = FlatStyle.System;
			if (dc.HideCaption)
			{
				radioButton.Text = "";
			}
			else
			{
				radioButton.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			}
			radioButton.Width = currentScreenInfo.columnWidth;
			if (dc.ReadOnly)
			{
				radioButton.Enabled = false;
			}
			DynamicScreen.AddControl(radioButton, ref currentScreenInfo, 2);
			return radioButton;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00025DC4 File Offset: 0x00024DC4
		private static object GetEventHandler(int eventHandlerID, ArrayList eventHandlers)
		{
			if (eventHandlers != null)
			{
				foreach (object obj in eventHandlers)
				{
					object[] array = (object[])obj;
					int num = (int)array[0];
					if (num == eventHandlerID)
					{
						return array[1];
					}
				}
			}
			return null;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00025E50 File Offset: 0x00024E50
		private static int RowCountToPixelHeight(int rowCount, Font font)
		{
			return Convert.ToInt32(rowCount * font.Height);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00025E70 File Offset: 0x00024E70
		private static Control AddDynamicTable(DynamicControl dc, ref ScreenInfo currentScreenInfo, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			int num = dc.Setting2;
			if (num <= 0)
			{
				num = 3;
			}
			TableProperty tableProperty = new TableProperty();
			NotesDef ctype = new NotesDef();
			CheckBoxDef ctype2 = new CheckBoxDef();
			FileNameDef ctype3 = new FileNameDef();
			tableProperty.Add(new ColumnDefinition("bob", ctype));
			tableProperty.Add(new ColumnDefinition("alphabet", ctype2));
			tableProperty.Add(new ColumnDefinition("soupy soup", ctype3));
			CustomTable customTable = new CustomTable(tableProperty);
			customTable.Height = Convert.ToInt32(num * customTable.Font.Height);
			Label label;
			if (dc.HideCaption)
			{
				label = null;
			}
			else
			{
				label = new Label();
				label.FlatStyle = FlatStyle.System;
				label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				label.Font = currentScreenInfo.labelFont;
				label.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
				label.Width = currentScreenInfo.columnWidth;
				int labelHeight = DynamicScreen.GetLabelHeight(label, null, currentScreenInfo);
				label.Height = labelHeight;
			}
			Control[] array = (label != null) ? new Control[]
			{
				label,
				customTable
			} : new Control[]
			{
				customTable
			};
			Control[] controls = array;
			int[] verticalPad = new int[2];
			DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, verticalPad);
			return customTable;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00025FCC File Offset: 0x00024FCC
		private static Control AddListView(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, ArrayList eventHandlers, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting3;
			int num = dc.Setting2;
			if (num <= 0)
			{
				num = 3;
			}
			DataTable lookupList = DynamicScreen.GetLookupList(setting, false, -1, ref comboBoxData, da, currentScreenInfo.UseFrench);
			bool noDeleting;
			if (dc.HasSpecialInstructions)
			{
				string text = dc.SpecialInstructions("nodeleting");
				noDeleting = (!string.IsNullOrEmpty(text) && text.Equals("1"));
			}
			else
			{
				noDeleting = false;
			}
			Label label;
			if (!dc.HideCaption)
			{
				label = new Label();
				label.FlatStyle = FlatStyle.System;
				label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				label.Font = currentScreenInfo.labelFont;
				label.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
				label.Width = currentScreenInfo.columnWidth;
				int labelHeight = DynamicScreen.GetLabelHeight(label, null, currentScreenInfo);
				label.Height = labelHeight;
			}
			else
			{
				label = null;
			}
			int num2;
			if (dc.Setting4 > 0)
			{
				num2 = Convert.ToInt32(0.08 * Convert.ToDouble(dc.Setting4));
			}
			else
			{
				num2 = 8;
			}
			ListViewEx listViewEx = new ListViewEx();
			listViewEx.View = View.Details;
			listViewEx.FullRowSelect = true;
			listViewEx.GridLines = (setting2 == 1);
			listViewEx.EnterTriggersDoubleClickEvent = true;
			listViewEx.Font = new Font(currentScreenInfo.labelFont.FontFamily, (float)num2);
			listViewEx.NoDeleting = noDeleting;
			listViewEx.DefaultSortByColInd = 0;
			listViewEx.Width = currentScreenInfo.columnWidth;
			listViewEx.Height = Convert.ToInt32(num * listViewEx.Font.Height);
			int num3 = listViewEx.Width - 24;
			if (num3 < 10)
			{
				num3 = 10;
			}
			int num4 = 0;
			int num5 = lookupList.Rows.Count + 1;
			foreach (object obj in lookupList.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num6 = Convert.ToInt32(num3 / num5);
				string text2 = dataRow[2].ToString();
				string[] array = text2.Split(new char[]
				{
					'`'
				});
				listViewEx.Columns.Add(array[0], num6, HorizontalAlignment.Left);
				num4 += num6;
			}
			listViewEx.Columns.Add("Date_", num3 - num4, HorizontalAlignment.Left);
			listViewEx.Tag = controlListRow;
			Button button;
			if (dc.ReadOnly)
			{
				button = null;
			}
			else
			{
				object eventHandler = DynamicScreen.GetEventHandler(1, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.SubItemClicked += (SubItemClickEventHandler)eventHandler;
				}
				eventHandler = DynamicScreen.GetEventHandler(4, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.KeyUp += (KeyEventHandler)eventHandler;
				}
				eventHandler = DynamicScreen.GetEventHandler(2, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.DoubleClick += (EventHandler)eventHandler;
				}
				eventHandler = DynamicScreen.GetEventHandler(5, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.ColumnClick += (ColumnClickEventHandler)eventHandler;
				}
				button = new Button();
				button.Font = currentScreenInfo.labelFont;
				button.Text = "Add Item";
				button.TextImageRelation = TextImageRelation.ImageBeforeText;
				button.Image = Resources.add;
				button.Width = currentScreenInfo.columnWidth;
				button.Tag = listViewEx;
				eventHandler = DynamicScreen.GetEventHandler(3, eventHandlers);
				if (eventHandler != null)
				{
					button.Click += (EventHandler)eventHandler;
				}
			}
			if (label != null)
			{
				Control[] array2 = (button == null) ? new Control[]
				{
					label,
					listViewEx
				} : new Control[]
				{
					label,
					listViewEx,
					button
				};
				Control[] controls = array2;
				int[] verticalPad = new int[3];
				DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, verticalPad);
			}
			else
			{
				Control[] array2 = (button == null) ? new Control[]
				{
					listViewEx
				} : new Control[]
				{
					listViewEx,
					button
				};
				Control[] controls2 = array2;
				int[] verticalPad = new int[3];
				DynamicScreen.AddControlsVertical(controls2, ref currentScreenInfo, verticalPad);
			}
			listViewEx.AutoSortingEnabled = true;
			return listViewEx;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00026450 File Offset: 0x00025450
		private static Control AddFileList2(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, ArrayList eventHandlers, ref DataSet comboBoxData, UnivDataAdapter da, int whoAmIPersonID, string whoAmIName, TripleDESEncryptionClass tripleDES)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting3;
			int num = dc.Setting2;
			if (num <= 0)
			{
				num = 3;
			}
			int emailTemplateId = 0;
			bool noEditing;
			bool noDeleting;
			if (dc.HasSpecialInstructions)
			{
				string text = dc.SpecialInstructions("noediting");
				noEditing = (!string.IsNullOrEmpty(text) && text.Equals("1"));
				string text2 = dc.SpecialInstructions("nodeleting");
				noDeleting = (!string.IsNullOrEmpty(text2) && text2.Equals("1"));
				string text3 = dc.SpecialInstructions("templateid");
				if (string.IsNullOrEmpty(text3) || !int.TryParse(text3, out emailTemplateId))
				{
					emailTemplateId = 0;
				}
			}
			else
			{
				noEditing = false;
				noDeleting = false;
			}
			DataTable lookupList = DynamicScreen.GetLookupList(setting, false, -1, ref comboBoxData, da, currentScreenInfo.UseFrench);
			Label label;
			if (!dc.HideCaption)
			{
				label = new Label();
				label.FlatStyle = FlatStyle.System;
				label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				label.Font = currentScreenInfo.labelFont;
				label.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
				label.Width = currentScreenInfo.columnWidth;
				int labelHeight = DynamicScreen.GetLabelHeight(label, null, currentScreenInfo);
				label.Height = labelHeight;
			}
			else
			{
				label = null;
			}
			CtrlFileList ctrlFileList = new CtrlFileList();
			ctrlFileList.EmailTemplateId = emailTemplateId;
			ctrlFileList.NoEditing = noEditing;
			ctrlFileList.NoDeleting = noDeleting;
			ctrlFileList.Width = currentScreenInfo.columnWidth;
			ctrlFileList.Height = Convert.ToInt32(num * ctrlFileList.Font.Height);
			int num2 = ctrlFileList.Width - 24;
			if (num2 < 10)
			{
				num2 = 10;
			}
			int num3 = 0;
			int num4 = lookupList.Rows.Count + 2;
			List<FileColumn> list = new List<FileColumn>();
			foreach (object obj in lookupList.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num5 = Convert.ToInt32(num2 / num4);
				string text4 = dataRow[2].ToString();
				string[] array = text4.Split(new char[]
				{
					'`'
				});
				eFileColumnControlType controlType = 1;
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (array.Length > 1)
				{
					if (array[1].Length > 0 && array[1][0] == '.')
					{
						if (array[1].Equals(".tx2", StringComparison.OrdinalIgnoreCase))
						{
							controlType = 5;
							if (array.Length > 2)
							{
								dictionary.Add("rowcount", array[2]);
							}
						}
						else if (array[1].Equals(".chk", StringComparison.OrdinalIgnoreCase))
						{
							controlType = 4;
						}
						else if (array[1].Equals(".dat", StringComparison.OrdinalIgnoreCase))
						{
							controlType = 6;
						}
						else if (array[1].Equals(".da2", StringComparison.OrdinalIgnoreCase))
						{
							controlType = 6;
							dictionary.Add("defaultdate", DateTime.Now.ToString("yyyy-MM-dd"));
						}
					}
					else if (array[1].Length > 0)
					{
						dictionary.Add("droplistitems", text4);
						controlType = 3;
					}
				}
				FileColumn item = new FileColumn
				{
					ColumnName = array[0],
					ColumnWidth = num5,
					ControlType = controlType,
					Args = dictionary
				};
				list.Add(item);
				num3 += num5;
			}
			int num6 = num2 - num3;
			int num7;
			if (num6 > 10)
			{
				num7 = Convert.ToInt32(Convert.ToDouble(num6) / 2.0);
			}
			else
			{
				num7 = 0;
			}
			if (num7 < 1)
			{
				num7 = 10;
			}
			list.Add(new FileColumn
			{
				ColumnName = "Date_",
				ColumnWidth = num7,
				ControlType = 6,
				Args = new Dictionary<string, string>()
			});
			list.Add(new FileColumn
			{
				ColumnName = "Filename_",
				ColumnWidth = num7,
				ControlType = 2,
				Args = new Dictionary<string, string>()
			});
			ctrlFileList.SetupColumns(list);
			ctrlFileList.Tag = controlListRow;
			if (label != null)
			{
				Control[] array2 = new Control[]
				{
					label,
					ctrlFileList
				};
				Control[] controls = array2;
				int[] verticalPad = new int[3];
				DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, verticalPad);
			}
			else
			{
				Control[] controls2 = new Control[]
				{
					ctrlFileList
				};
				int[] verticalPad = new int[1];
				DynamicScreen.AddControlsVertical(controls2, ref currentScreenInfo, verticalPad);
			}
			ctrlFileList.Init();
			return ctrlFileList;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0002698C File Offset: 0x0002598C
		private static Control AddFileList(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, ArrayList eventHandlers, ref DataSet comboBoxData, UnivDataAdapter da, int whoAmIPersonID, string whoAmIName, TripleDESEncryptionClass tripleDES)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting3;
			int num = dc.Setting2;
			if (num <= 0)
			{
				num = 3;
			}
			bool noEditing;
			bool noDeleting;
			if (dc.HasSpecialInstructions)
			{
				string text = dc.SpecialInstructions("noediting");
				noEditing = (!string.IsNullOrEmpty(text) && text.Equals("1"));
				string text2 = dc.SpecialInstructions("nodeleting");
				noDeleting = (!string.IsNullOrEmpty(text2) && text2.Equals("1"));
			}
			else
			{
				noEditing = false;
				noDeleting = false;
			}
			DataTable lookupList = DynamicScreen.GetLookupList(setting, false, -1, ref comboBoxData, da, currentScreenInfo.UseFrench);
			Label label = new Label();
			label.FlatStyle = FlatStyle.System;
			label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			label.Font = currentScreenInfo.labelFont;
			label.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			label.Width = currentScreenInfo.columnWidth;
			int labelHeight = DynamicScreen.GetLabelHeight(label, null, currentScreenInfo);
			label.Height = labelHeight;
			ListViewEx listViewEx = new ListViewEx();
			listViewEx.IsFileList = true;
			listViewEx.View = View.Details;
			listViewEx.FullRowSelect = true;
			listViewEx.GridLines = (setting2 == 1);
			listViewEx.EnterTriggersDoubleClickEvent = true;
			listViewEx.Font = new Font(currentScreenInfo.labelFont.FontFamily, 8f);
			listViewEx.NoEditing = noEditing;
			listViewEx.NoDeleting = noDeleting;
			listViewEx.Width = currentScreenInfo.columnWidth;
			listViewEx.Height = Convert.ToInt32(num * listViewEx.Font.Height);
			int num2 = listViewEx.Width - 24;
			if (num2 < 10)
			{
				num2 = 10;
			}
			int num3 = 0;
			int num4 = lookupList.Rows.Count + 2;
			foreach (object obj in lookupList.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num5 = Convert.ToInt32(num2 / num4);
				string text3 = dataRow[2].ToString();
				string[] array = text3.Split(new char[]
				{
					'`'
				});
				listViewEx.Columns.Add(array[0], num5, HorizontalAlignment.Left);
				num3 += num5;
			}
			listViewEx.Columns.Add("Date_", num2 - num3, HorizontalAlignment.Left);
			listViewEx.Columns.Add("Filename_", num2 - num3, HorizontalAlignment.Left);
			listViewEx.Tag = controlListRow;
			Button button;
			if (dc.ReadOnly)
			{
				button = null;
			}
			else
			{
				object eventHandler = DynamicScreen.GetEventHandler(1, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.SubItemClicked += (SubItemClickEventHandler)eventHandler;
				}
				eventHandler = DynamicScreen.GetEventHandler(4, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.KeyUp += (KeyEventHandler)eventHandler;
				}
				eventHandler = DynamicScreen.GetEventHandler(2, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.DoubleClick += (EventHandler)eventHandler;
				}
				eventHandler = DynamicScreen.GetEventHandler(5, eventHandlers);
				if (eventHandler != null)
				{
					listViewEx.ColumnClick += (ColumnClickEventHandler)eventHandler;
				}
				button = new Button();
				button.Font = currentScreenInfo.labelFont;
				button.Text = "Add Item";
				button.Image = Resources.add;
				button.TextImageRelation = TextImageRelation.ImageBeforeText;
				button.Width = currentScreenInfo.columnWidth;
				button.Tag = listViewEx;
				eventHandler = DynamicScreen.GetEventHandler(3, eventHandlers);
				if (eventHandler != null)
				{
					button.Click += (EventHandler)eventHandler;
					listViewEx.AllowUserToDragAFile_WillFireAddNewItem((EventHandler)eventHandler);
				}
			}
			Control[] array2 = (button == null) ? new Control[]
			{
				label,
				listViewEx
			} : new Control[]
			{
				label,
				listViewEx,
				button
			};
			Control[] controls = array2;
			int[] verticalPad = new int[3];
			DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, verticalPad);
			listViewEx.AutoSortingEnabled = true;
			return listViewEx;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00026DD4 File Offset: 0x00025DD4
		private static Control AddSchoolYearChooser(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, UnivDataAdapter da, ref DateScopes dateScopes)
		{
			Label label = new Label();
			label.FlatStyle = FlatStyle.System;
			label.Font = currentScreenInfo.labelFont;
			label.Text = DynamicScreen.GetControlCaption(controlListRow, currentScreenInfo.UseFrench);
			label.Width = currentScreenInfo.labelWidth;
			if (dateScopes == null)
			{
				da.SelectCommand.CommandText = "SELECT daterangeid,description,startmonth,endmonth,numyearsbetween,usecode,startday,endday FROM dateranges WHERE usecode=0";
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				dateScopes = new DateScopes(dataTable);
			}
			SchoolYearChooserCtrl schoolYearChooserCtrl = new SchoolYearChooserCtrl(dateScopes);
			if (dc.ReadOnly)
			{
				schoolYearChooserCtrl.Enabled = false;
			}
			schoolYearChooserCtrl.Tag = controlListRow;
			Control[] controls = new Control[]
			{
				label,
				schoolYearChooserCtrl
			};
			label.Height = schoolYearChooserCtrl.Height;
			DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc);
			return schoolYearChooserCtrl;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00026EB4 File Offset: 0x00025EB4
		private static Control AddMyTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage)
		{
			return DynamicScreen.AddMyTextBox(dc, controlListRow, ref currentScreenInfo, lockImage, -1, "");
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00026ED8 File Offset: 0x00025ED8
		private static Control AddMyTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage, int whoAmIPersonID, string whoAmIName)
		{
			return DynamicScreen.AddTextBox(dc, controlListRow, ref currentScreenInfo, lockImage, false);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00026EF4 File Offset: 0x00025EF4
		private static Control AddTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage, bool regularTextBox)
		{
			return DynamicScreen.AddTextBox(dc, controlListRow, ref currentScreenInfo, lockImage, regularTextBox, -1, "");
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00026F18 File Offset: 0x00025F18
		private static Control AddMaskedTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			int setting4 = dc.Setting4;
			bool flag = setting3 != 0;
			bool flag2 = dc.DefaultValue == 1;
			bool flag3 = false;
			MyLabel myLabel;
			if (dc.HideCaption)
			{
				myLabel = null;
			}
			else
			{
				myLabel = new MyLabel();
				myLabel.FlatStyle = FlatStyle.System;
				myLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				myLabel.Font = currentScreenInfo.labelFont;
				myLabel.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
				myLabel.Width = currentScreenInfo.labelWidth;
			}
			MyMaskedTextBox myMaskedTextBox = new MyMaskedTextBox();
			myMaskedTextBox.BackColor = Color.Green;
			myMaskedTextBox.Tag = controlListRow;
			myMaskedTextBox.Font = currentScreenInfo.font;
			myMaskedTextBox.Width = currentScreenInfo.columnWidth - ((myLabel == null) ? 0 : myLabel.Width);
			if (dc.Mask.Length > 0)
			{
				myMaskedTextBox.Mask = dc.Mask;
			}
			if (setting4 == 1)
			{
				bool shouldAddBlankFirstItem = true;
				DataTable dataTable;
				if (setting > 0)
				{
					dataTable = DynamicScreen.GetLookupList(setting, shouldAddBlankFirstItem, -1, ref comboBoxData, da, currentScreenInfo.UseFrench);
				}
				else
				{
					dataTable = new DataTable();
				}
				myMaskedTextBox.ConvertToMultiSelectList(dataTable.DefaultView);
			}
			if (dc.ReadOnly)
			{
				myMaskedTextBox.ReadOnly = true;
			}
			if (flag2)
			{
			}
			myMaskedTextBox.AccessibleName = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			if (setting2 > 0)
			{
				if (currentScreenInfo.oneCharWidth < 0)
				{
					currentScreenInfo.oneCharWidth = (int)currentScreenInfo.graphics.MeasureString("ZZ", currentScreenInfo.font).Width;
					currentScreenInfo.oneCharWidth /= 2;
				}
				int oneCharWidth = currentScreenInfo.oneCharWidth;
				int num = oneCharWidth * setting2;
				if (num < myMaskedTextBox.Width)
				{
					myMaskedTextBox.Width = num;
				}
			}
			Control[] controls;
			if (myLabel != null)
			{
				controls = new Control[]
				{
					myLabel,
					myMaskedTextBox
				};
			}
			else
			{
				controls = new Control[]
				{
					myMaskedTextBox
				};
			}
			if (myLabel != null)
			{
				int labelHeight = DynamicScreen.GetLabelHeight(myLabel, myMaskedTextBox, currentScreenInfo);
				myLabel.Height = labelHeight;
			}
			DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc.DontWrapToNextLine, dc);
			if (flag3)
			{
			}
			return myMaskedTextBox;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000271C8 File Offset: 0x000261C8
		private static Control AddRichTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage, int whoAmIPersonID, string whoAmIName)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			bool flag = setting3 != 0;
			bool flag2 = dc.DefaultValue == 1;
			bool flag3 = setting > 1;
			if (dc.Setting4 > 0)
			{
				flag3 = false;
			}
			MyRichText myRichText = new MyRichText();
			myRichText.Caption = (dc.HideCaption ? "" : DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench));
			myRichText.Tag = controlListRow;
			myRichText.Font = currentScreenInfo.font;
			myRichText.Width = currentScreenInfo.columnWidth;
			AnchorStyles anchor;
			myRichText.SetHeight(currentScreenInfo.parentControl, setting, out anchor);
			if (dc.ReadOnly)
			{
				myRichText.ReadOnly = true;
			}
			if (flag2)
			{
				myRichText.OnlyAllowAdding = true;
				myRichText.WhoAmIName = whoAmIName;
			}
			myRichText.AccessibleName = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			myRichText.AccessibleDescription = myRichText.AccessibleName;
			if (setting2 > 0)
			{
				if (currentScreenInfo.oneCharWidth < 0)
				{
					currentScreenInfo.oneCharWidth = (int)currentScreenInfo.graphics.MeasureString("ZZ", currentScreenInfo.font).Width;
					currentScreenInfo.oneCharWidth /= 2;
				}
				int oneCharWidth = currentScreenInfo.oneCharWidth;
				int num = oneCharWidth * setting2;
				if (num < myRichText.Width)
				{
					myRichText.Width = num;
				}
			}
			Control[] array = new Control[]
			{
				myRichText
			};
			myRichText.ScrollBars = RichTextBoxScrollBars.Vertical;
			myRichText.Width = currentScreenInfo.columnWidth;
			DynamicScreen.AddControl(myRichText, ref currentScreenInfo, 6);
			if (flag3)
			{
				myRichText.EnableSpellCheck();
			}
			myRichText.Anchor = anchor;
			return myRichText;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000273C0 File Offset: 0x000263C0
		private static Control AddMultilineTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage, int whoAmIPersonID, string whoAmIName)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			bool flag = setting3 != 0;
			bool flag2 = dc.DefaultValue == 1;
			MyMultilineTextBoxWithEditingControls myMultilineTextBoxWithEditingControls = new MyMultilineTextBoxWithEditingControls();
			myMultilineTextBoxWithEditingControls.WhoAmIPid = whoAmIPersonID;
			myMultilineTextBoxWithEditingControls.WhoAmIName = whoAmIName;
			myMultilineTextBoxWithEditingControls.Caption = (dc.HideCaption ? "" : DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench));
			myMultilineTextBoxWithEditingControls.Tag = controlListRow;
			myMultilineTextBoxWithEditingControls.Font = currentScreenInfo.font;
			myMultilineTextBoxWithEditingControls.Width = currentScreenInfo.columnWidth;
			if (dc.ReadOnly)
			{
				myMultilineTextBoxWithEditingControls.SetReadOnly();
			}
			if (flag2)
			{
			}
			myMultilineTextBoxWithEditingControls.AccessibleName = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			myMultilineTextBoxWithEditingControls.AccessibleDescription = myMultilineTextBoxWithEditingControls.AccessibleName;
			Control[] array = new Control[]
			{
				myMultilineTextBoxWithEditingControls
			};
			myMultilineTextBoxWithEditingControls.Width = currentScreenInfo.columnWidth;
			myMultilineTextBoxWithEditingControls.SetHeight(setting);
			DynamicScreen.AddControl(myMultilineTextBoxWithEditingControls, ref currentScreenInfo, 6);
			return myMultilineTextBoxWithEditingControls;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000274E8 File Offset: 0x000264E8
		private static Control AddFile(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			MyFile myFile = new MyFile();
			MyLabel myLabel;
			if (dc.HideCaption)
			{
				myLabel = null;
			}
			else
			{
				myLabel = new MyLabel();
				myLabel.FlatStyle = FlatStyle.System;
				myLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				myLabel.Font = currentScreenInfo.labelFont;
				myLabel.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
				myLabel.Width = currentScreenInfo.labelWidth;
			}
			myFile.AccessibleName = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			myFile.AccessibleDescription = myFile.AccessibleName;
			Control[] controls;
			if (myLabel != null)
			{
				controls = new Control[]
				{
					myLabel,
					myFile
				};
			}
			else
			{
				controls = new Control[]
				{
					myFile
				};
			}
			if (myLabel != null)
			{
				int labelHeight = DynamicScreen.GetLabelHeight(myLabel, myFile, currentScreenInfo);
				myLabel.Height = labelHeight;
			}
			myFile.Tag = controlListRow;
			DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc.DontWrapToNextLine, dc);
			return myFile;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000275EC File Offset: 0x000265EC
		private static Control AddPMTable(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ArrayList eventHandlers, int whoAmIPersonID)
		{
			PMTable pmtable = new PMTable();
			pmtable.Title = dc.ControlCaption;
			pmtable.Da = da;
			pmtable.TripleDES = tripleDES;
			pmtable.EventHandlers = eventHandlers;
			pmtable.WhoAmIPersonID = whoAmIPersonID;
			pmtable.Cids = dc.DefaultValueString;
			int width = currentScreenInfo.GetEffectiveWidthForControl();
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			pmtable.Width = width;
			if (dc.Setting2 > 0)
			{
				pmtable.Height = dc.Setting2;
			}
			pmtable.FormNumber = dc.Setting1;
			DynamicScreen.AddControlsVertical(new Control[]
			{
				pmtable
			}, ref currentScreenInfo, new int[]
			{
				2
			});
			return pmtable;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000276C4 File Offset: 0x000266C4
		private static Control AddEmailHistory(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmIPersonID)
		{
			EmailHistory emailHistory = new EmailHistory();
			emailHistory.Title = dc.ControlCaption;
			emailHistory.Da = da;
			emailHistory.TripleDES = tripleDES;
			int width = currentScreenInfo.GetEffectiveWidthForControl();
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			emailHistory.Width = width;
			if (dc.Setting2 > 0)
			{
				emailHistory.Height = dc.Setting2;
			}
			DynamicScreen.AddControlsVertical(new Control[]
			{
				emailHistory
			}, ref currentScreenInfo, new int[]
			{
				2
			});
			return emailHistory;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00027770 File Offset: 0x00026770
		private static Control AddAppointmentHistory(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int whoAmIPersonID)
		{
			AppointmentHistory appointmentHistory = new AppointmentHistory();
			appointmentHistory.Title = dc.ControlCaption;
			appointmentHistory.Da = da;
			appointmentHistory.TripleDES = tripleDES;
			int width = currentScreenInfo.GetEffectiveWidthForControl();
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			appointmentHistory.Width = width;
			if (dc.Setting2 > 0)
			{
				appointmentHistory.Height = dc.Setting2;
			}
			DynamicScreen.AddControlsVertical(new Control[]
			{
				appointmentHistory
			}, ref currentScreenInfo, new int[]
			{
				2
			});
			appointmentHistory.AppTypeIds = dc.DefaultValueString;
			return appointmentHistory;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00027828 File Offset: 0x00026828
		private static Control AddCalcButton(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage, bool regularTextBox, int whoAmIPersonID, string whoAmIName)
		{
			string defaultValueString = dc.DefaultValueString;
			CalculationButton calculationButton = new CalculationButton();
			calculationButton.MyCid = dc.ControlId;
			calculationButton.Calculation = defaultValueString;
			calculationButton.Text = dc.ControlCaption;
			calculationButton.Tag = controlListRow;
			calculationButton.LookupTable = dc.Mask;
			int width = currentScreenInfo.GetEffectiveWidthForControl();
			if (dc.DontWrapToNextLine)
			{
				width = Convert.ToInt32(currentScreenInfo.columnWidth / 2) - 4;
			}
			calculationButton.Width = width;
			if (!dc.Enabled)
			{
				calculationButton.Visible = false;
			}
			DynamicScreen.AddControlsVertical(new Control[]
			{
				calculationButton
			}, ref currentScreenInfo, new int[]
			{
				2
			});
			return calculationButton;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0002790C File Offset: 0x0002690C
		private static Control AddTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, Image lockImage, bool regularTextBox, int whoAmIPersonID, string whoAmIName)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			bool flag = setting3 != 0;
			bool flag2 = dc.DefaultValue == 1;
			eLabelOrientation eLabelOrientation = eLabelOrientation.LabelLeft;
			if (Enum.IsDefined(typeof(eLabelOrientation), dc.Setting4))
			{
				eLabelOrientation = (eLabelOrientation)dc.Setting4;
			}
			bool flag3 = setting > 1;
			string text = dc.Mask.Trim();
			bool flag4 = text.Equals("$");
			if (flag4)
			{
				text = "";
			}
			bool flag5 = eLabelOrientation == eLabelOrientation.NoLabel || dc.HideCaption;
			MyLabel myLabel;
			if (flag5)
			{
				myLabel = null;
			}
			else
			{
				myLabel = new MyLabel();
				myLabel.FlatStyle = FlatStyle.System;
				myLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
				myLabel.Font = currentScreenInfo.labelFont;
				myLabel.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
				myLabel.Width = currentScreenInfo.labelWidth;
				if (dc.HasSpecialInstructions)
				{
					string text2 = dc.SpecialInstructionsNoNull("align").ToLower();
					if (text2.Equals("right"))
					{
						myLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					}
					else if (text2.Equals("center"))
					{
						myLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
					}
				}
			}
			Control control;
			if (text.Length < 1)
			{
				MyTextBox myTextBox = new MyTextBox(dc);
				if (dc.HasSpecialInstructions)
				{
					string text3 = dc.SpecialInstructions("masktype");
					int maskCid;
					if (!string.IsNullOrEmpty(text3) && int.TryParse(text3, out maskCid))
					{
						string text4 = dc.SpecialInstructions("maskrules");
						if (text4 == null)
						{
							text4 = "";
						}
						myTextBox.SetMaskRules(maskCid, text4);
					}
					string text5 = dc.SpecialInstructions("selectfolder");
					if (!string.IsNullOrEmpty(text5))
					{
						if ("1yestrue".IndexOf(text5.ToLower()) >= 0)
						{
							myTextBox.ActAsFolderBrowser = true;
						}
					}
					else
					{
						string text6 = dc.SpecialInstructions("selectfile");
						if (!string.IsNullOrEmpty(text6))
						{
							if ("1yestrue".IndexOf(text6.ToLower()) >= 0)
							{
								myTextBox.ActAsFileBrowser = true;
							}
						}
					}
					string value = dc.SpecialInstructions("casing");
					if (!string.IsNullOrEmpty(value) && Enum.IsDefined(typeof(CharacterCasing), value))
					{
						myTextBox.CharacterCasing = (CharacterCasing)Enum.Parse(typeof(CharacterCasing), value);
					}
				}
				myTextBox.SuppressEnter = false;
				myTextBox.IsCurrency = flag4;
				myTextBox.MaxLength = 3000;
				myTextBox.Tag = controlListRow;
				myTextBox.Font = currentScreenInfo.font;
				myTextBox.Width = currentScreenInfo.columnWidth - ((myLabel == null) ? 0 : myLabel.Width);
				if (dc.ReadOnly)
				{
					myTextBox.ReadOnly = true;
					myTextBox.IsReadOnly = true;
				}
				if (flag2)
				{
					myTextBox.OnlyAllowAdding = true;
					myTextBox.WhoAmIName = whoAmIName;
				}
				control = myTextBox;
			}
			else
			{
				MaskedTextBox maskedTextBox = new MaskedTextBox(text);
				maskedTextBox.PromptChar = ' ';
				maskedTextBox.HidePromptOnLeave = true;
				maskedTextBox.Tag = controlListRow;
				maskedTextBox.Font = currentScreenInfo.font;
				maskedTextBox.Width = currentScreenInfo.columnWidth - ((myLabel == null) ? 0 : myLabel.Width);
				if (dc.ReadOnly)
				{
					maskedTextBox.ReadOnly = true;
				}
				if (flag2)
				{
				}
				control = maskedTextBox;
			}
			control.AccessibleName = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			control.AccessibleDescription = control.AccessibleName;
			if (setting2 > 0)
			{
				if (currentScreenInfo.oneCharWidth < 0)
				{
					currentScreenInfo.oneCharWidth = (int)currentScreenInfo.graphics.MeasureString("ZZ", currentScreenInfo.font).Width;
					currentScreenInfo.oneCharWidth /= 2;
				}
				int oneCharWidth = currentScreenInfo.oneCharWidth;
				int num = oneCharWidth * setting2;
				if (num < control.Width)
				{
					control.Width = num;
				}
			}
			Control[] controls;
			if (myLabel != null)
			{
				controls = new Control[]
				{
					myLabel,
					control
				};
			}
			else
			{
				controls = new Control[]
				{
					control
				};
			}
			if (eLabelOrientation == eLabelOrientation.LabelAbove && setting < 2)
			{
				Control control2 = control;
				if (myLabel != null)
				{
					myLabel.Width = currentScreenInfo.columnWidth;
					int labelHeight = DynamicScreen.GetLabelHeight(myLabel, null, currentScreenInfo);
					myLabel.Height = labelHeight;
				}
				control2.Width = currentScreenInfo.columnWidth;
				DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, new int[]
				{
					2,
					6,
					6
				});
			}
			else if (setting > 1 && control is TextBox)
			{
				TextBox textBox = (TextBox)control;
				textBox.Multiline = true;
				control.Height *= setting;
				textBox.ScrollBars = ScrollBars.Vertical;
				if (myLabel != null)
				{
					myLabel.Width = currentScreenInfo.columnWidth;
					int labelHeight = DynamicScreen.GetLabelHeight(myLabel, null, currentScreenInfo);
					myLabel.Height = labelHeight;
				}
				control.Width = currentScreenInfo.columnWidth;
				DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, new int[]
				{
					2,
					6,
					6
				});
			}
			else
			{
				if (myLabel != null)
				{
					int labelHeight = DynamicScreen.GetLabelHeight(myLabel, control, currentScreenInfo);
					myLabel.Height = labelHeight;
				}
				if (eLabelOrientation == eLabelOrientation.LabelAbove)
				{
					DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, new int[]
					{
						6,
						6
					});
				}
				else
				{
					DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, 6, dc.DontWrapToNextLine, dc);
				}
			}
			if (flag3 && control is MyTextBox)
			{
				((MyTextBox)control).EnableSpellCheck();
			}
			return control;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00027FB0 File Offset: 0x00026FB0
		private static Control AddMultiCheckBoxTextBox(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo, int whoAmIPersonID, string whoAmIName, ref DataSet comboBoxData, UnivDataAdapter da)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			bool flag = setting3 != 0;
			bool flag2 = dc.DefaultValue == 1;
			bool flag3 = setting > 1;
			string text = dc.Mask.Trim();
			string controlCaptionForDisplay = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			string[] array = controlCaptionForDisplay.Split(new char[]
			{
				'.'
			});
			int num = array.Length;
			string[] colCaptions = array;
			Control control;
			if (text.Length < 1)
			{
				MyTextBox myTextBox = new MyTextBox();
				myTextBox.SuppressEnter = false;
				myTextBox.MaxLength = 3000;
				myTextBox.Font = currentScreenInfo.font;
				myTextBox.Width = currentScreenInfo.columnWidth;
				if (dc.ReadOnly)
				{
					myTextBox.ReadOnly = true;
					myTextBox.IsReadOnly = true;
				}
				if (flag2)
				{
					myTextBox.OnlyAllowAdding = true;
					myTextBox.WhoAmIName = whoAmIName;
				}
				control = myTextBox;
			}
			else
			{
				MaskedTextBox maskedTextBox = new MaskedTextBox(text);
				maskedTextBox.Font = currentScreenInfo.font;
				if (dc.ReadOnly)
				{
					maskedTextBox.ReadOnly = true;
				}
				control = maskedTextBox;
			}
			control.AccessibleName = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
			control.AccessibleDescription = control.AccessibleName;
			if (setting2 > 0)
			{
				if (currentScreenInfo.oneCharWidth < 0)
				{
					currentScreenInfo.oneCharWidth = (int)currentScreenInfo.graphics.MeasureString("ZZ", currentScreenInfo.font).Width;
					currentScreenInfo.oneCharWidth /= 2;
				}
				int oneCharWidth = currentScreenInfo.oneCharWidth;
				int num2 = oneCharWidth * setting2;
				if (num2 < control.Width)
				{
					control.Width = num2;
				}
			}
			Control[] controls = new Control[]
			{
				control
			};
			if (setting > 1 && control is TextBox)
			{
				TextBox textBox = (TextBox)control;
				textBox.Multiline = true;
				int num3 = control.Height * setting;
				textBox.ScrollBars = ScrollBars.Vertical;
			}
			else
			{
				int num3 = control.Height;
			}
			if (flag3 && control is MyTextBox)
			{
				((MyTextBox)control).EnableSpellCheck();
			}
			MyMultiCheckbox myMultiCheckbox = new MyMultiCheckbox(currentScreenInfo.ColumnWidth, colCaptions, dc.HideCaption, controls);
			myMultiCheckbox.Width = currentScreenInfo.ColumnWidth;
			if (setting > 1)
			{
				myMultiCheckbox.Height += (setting - 1) * control.Height;
			}
			myMultiCheckbox.Tag = controlListRow;
			DynamicScreen.AddControl(myMultiCheckbox, ref currentScreenInfo, 0, dc.DontWrapToNextLine);
			return myMultiCheckbox;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000282A8 File Offset: 0x000272A8
		private static int GetLabelHeight(Label l, Control accompanyingControl, ScreenInfo currentScreenInfo)
		{
			int num = (int)currentScreenInfo.graphics.MeasureString(l.Text, l.Font, l.Width + 4).Height + 2;
			if (accompanyingControl != null && num < accompanyingControl.Height)
			{
				num = accompanyingControl.Height;
			}
			return num;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00028308 File Offset: 0x00027308
		private static int GetLabelHeight(MyLabel l, Control accompanyingControl, ScreenInfo currentScreenInfo)
		{
			int num = (int)currentScreenInfo.graphics.MeasureString(l.Text, l.Font, l.Width + 4).Height + 2;
			if (accompanyingControl != null && num < accompanyingControl.Height)
			{
				num = accompanyingControl.Height;
			}
			return num;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00028368 File Offset: 0x00027368
		private static Control AddDate(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo)
		{
			int defaultValue = dc.DefaultValue;
			string text = "MMMM dd, yyyy";
			int setting = dc.Setting1;
			bool flag = dc.Setting2 == 1;
			eLabelOrientation eLabelOrientation = eLabelOrientation.LabelLeft;
			if (Enum.IsDefined(typeof(eLabelOrientation), dc.Setting4))
			{
				eLabelOrientation = (eLabelOrientation)dc.Setting4;
			}
			bool flag2 = false;
			if (dc.HasSpecialInstructions)
			{
				string text2 = dc.SpecialInstructions("dateformat");
				if (!string.IsNullOrEmpty(text2))
				{
					text = text2;
					flag2 = true;
				}
			}
			int num = currentScreenInfo.GetEffectiveWidthForControl();
			Control result;
			if (flag)
			{
				MyDateTimePickerForAccommodationsExpiry myDateTimePickerForAccommodationsExpiry = new MyDateTimePickerForAccommodationsExpiry();
				myDateTimePickerForAccommodationsExpiry.BorderStyle = (BorderStyle)setting;
				myDateTimePickerForAccommodationsExpiry.Title = dc.ControlCaptionForDisplay;
				myDateTimePickerForAccommodationsExpiry.Format = DateTimePickerFormat.Custom;
				myDateTimePickerForAccommodationsExpiry.CustomFormat = text;
				myDateTimePickerForAccommodationsExpiry.Width = num;
				using (Graphics graphics = myDateTimePickerForAccommodationsExpiry.CreateGraphics())
				{
					DateTime dateTime = new DateTime(2007, 12, 25);
					int num2 = Convert.ToInt32(graphics.MeasureString(dateTime.ToString(text), myDateTimePickerForAccommodationsExpiry.Font).Width) + SystemInformation.VerticalScrollBarWidth * 2;
					if (num2 < num && num2 > 30)
					{
						num = num2;
					}
				}
				myDateTimePickerForAccommodationsExpiry.Dtp.Width = num + 20;
				if (dc.ReadOnly)
				{
					myDateTimePickerForAccommodationsExpiry.Enabled = false;
				}
				myDateTimePickerForAccommodationsExpiry.Tag = controlListRow;
				myDateTimePickerForAccommodationsExpiry.AccessibleName = dc.ControlCaptionForDisplay;
				myDateTimePickerForAccommodationsExpiry.AccessibleDescription = myDateTimePickerForAccommodationsExpiry.AccessibleName;
				myDateTimePickerForAccommodationsExpiry.DefaultValue = (DateDefaultValue)defaultValue;
				Control[] controls = new Control[]
				{
					myDateTimePickerForAccommodationsExpiry
				};
				int[] verticalPad = new int[1];
				DynamicScreen.AddControlsVertical(controls, ref currentScreenInfo, verticalPad);
				result = myDateTimePickerForAccommodationsExpiry;
			}
			else
			{
				Label label;
				if (dc.HideCaption || eLabelOrientation == eLabelOrientation.NoLabel)
				{
					label = null;
				}
				else
				{
					label = new Label();
					label.FlatStyle = FlatStyle.System;
					label.Font = currentScreenInfo.labelFont;
					label.Text = DynamicScreen.GetControlCaptionForDisplay(dc, currentScreenInfo.UseFrench);
					label.Width = currentScreenInfo.labelWidth;
				}
				Control control;
				if (flag2)
				{
					CtrlDateTimePicker ctrlDateTimePicker = new CtrlDateTimePicker();
					DynamicFieldView dynamicFieldView = new DynamicFieldView
					{
						ControlId = dc.ControlId,
						ControlCode = (eControlCode)dc.ControlCode,
						DefaultValue = dc.DefaultValue,
						ControlCaption = dc.ControlCaption
					};
					DynamicDataView dynamicData = new DynamicDataView
					{
						Field = dynamicFieldView,
						Value = new DateTime?(DateTime.Now.Date)
					};
					Control.ControlCollection controls2 = currentScreenInfo.parentControl.Controls;
					ctrlDateTimePicker.SetupControl(ref controls2, dynamicFieldView, new DynamicFormContext());
					if (defaultValue == 1)
					{
						ctrlDateTimePicker.DynamicData = dynamicData;
					}
					ctrlDateTimePicker.AccessibleName = dc.ControlCaptionForDisplay;
					ctrlDateTimePicker.AccessibleDescription = ctrlDateTimePicker.AccessibleName;
					ctrlDateTimePicker.DateFormat = text;
					ctrlDateTimePicker.Tag = controlListRow;
					control = ctrlDateTimePicker;
				}
				else if (dc.ReadOnly)
				{
					dc.ReadOnly = false;
					TextBox textBox = new TextBox();
					textBox.ReadOnly = true;
					textBox.Tag = controlListRow;
					textBox.AccessibleName = dc.ControlCaptionForDisplay;
					textBox.AccessibleDescription = textBox.AccessibleName;
					control = textBox;
				}
				else
				{
					MyDateTimePicker myDateTimePicker = new MyDateTimePicker();
					myDateTimePicker.Value = DateTime.MinValue;
					myDateTimePicker.Tag = controlListRow;
					myDateTimePicker.AccessibleName = dc.ControlCaptionForDisplay;
					myDateTimePicker.AccessibleDescription = myDateTimePicker.AccessibleName;
					myDateTimePicker.Format = DateTimePickerFormat.Custom;
					myDateTimePicker.CustomFormat = text;
					control = myDateTimePicker;
				}
				if (label != null)
				{
					num -= label.Width;
				}
				using (Graphics graphics = control.CreateGraphics())
				{
					DateTime dateTime = new DateTime(2007, 12, 25);
					int num2 = Convert.ToInt32(graphics.MeasureString(dateTime.ToString(text), control.Font).Width) + SystemInformation.VerticalScrollBarWidth * 2;
					if (num2 < num && num2 > 30)
					{
						num = num2;
					}
				}
				control.Width = num;
				if (dc.ReadOnly)
				{
					control.Enabled = false;
				}
				if (eLabelOrientation == eLabelOrientation.LabelAbove)
				{
					if (label != null)
					{
						label.Width = currentScreenInfo.columnWidth;
					}
					control.Width = currentScreenInfo.columnWidth;
				}
				if (label != null)
				{
					Control[] controls3 = new Control[]
					{
						label,
						control
					};
					if (eLabelOrientation == eLabelOrientation.LabelAbove)
					{
						label.Height = DynamicScreen.GetLabelHeight(label, null, currentScreenInfo);
						DynamicScreen.AddControlsVertical(controls3, ref currentScreenInfo, new int[]
						{
							2,
							4
						});
					}
					else
					{
						label.Height = DynamicScreen.GetLabelHeight(label, control, currentScreenInfo);
						DynamicScreen.AddControlsHorizontal(controls3, ref currentScreenInfo, 4, dc);
					}
				}
				else
				{
					DynamicScreen.AddControlsHorizontal(new Control[]
					{
						control
					}, ref currentScreenInfo, 4, dc);
				}
				result = control;
			}
			return result;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00028910 File Offset: 0x00027910
		private static Control AddHorizontalRule(DataRow controlListRow, ref ScreenInfo currentScreenInfo)
		{
			int num = (int)controlListRow[4];
			int num2 = (int)controlListRow[5];
			Label label = new Label();
			label.BorderStyle = BorderStyle.Fixed3D;
			if (num == -2)
			{
				label.Height = -5;
				label.BorderStyle = BorderStyle.None;
			}
			else if (num <= 0)
			{
				label.Height = 2 + currentScreenInfo.verticalControlPad;
			}
			else
			{
				label.Height = num;
			}
			if (num2 == 0)
			{
				label.BackColor = SystemColors.InactiveCaptionText;
			}
			else
			{
				label.BackColor = Color.FromArgb(num2);
			}
			label.Height += 6;
			label.Width = currentScreenInfo.columnWidth;
			label.Text = "";
			label.Tag = controlListRow;
			Control control = DynamicScreen.AddControl(label, ref currentScreenInfo, 6, 8);
			Control result;
			if (control != null)
			{
				label.Height -= 6;
				label.Top += 6;
				result = label;
			}
			else
			{
				label.Dispose();
				result = null;
			}
			return result;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00028A34 File Offset: 0x00027A34
		private static Control AddBlankSpace(DynamicControl dc, DataRow controlListRow, ref ScreenInfo currentScreenInfo)
		{
			int setting = dc.Setting1;
			int setting2 = dc.Setting2;
			int setting3 = dc.Setting3;
			bool dontWrapToNextLine = dc.DontWrapToNextLine;
			Label label = new Label();
			if (setting > 0)
			{
				label.Height = (int)((double)(label.Height * setting) / 100.0);
			}
			if (setting3 > 0)
			{
				label.Width = setting3;
			}
			else if (setting2 > 0)
			{
				int num = (int)((double)(currentScreenInfo.columnWidth * setting2) / 100.0);
				label.Width = currentScreenInfo.columnWidth;
			}
			else
			{
				label.Width = currentScreenInfo.columnWidth;
			}
			label.Text = "";
			label.Tag = controlListRow;
			DynamicScreen.AddControl(label, ref currentScreenInfo, 2, dontWrapToNextLine);
			return label;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00028B1C File Offset: 0x00027B1C
		private static Control AddColumnBreak(DataRow controlListRow, ref ScreenInfo currentScreenInfo)
		{
			int value = (int)controlListRow[4];
			double num = Convert.ToDouble(value) / 100.0;
			Label label = new Label();
			label.Font = new Font("Arial", 7f, FontStyle.Bold);
			label.Text = "";
			label.Height = 5;
			label.Tag = controlListRow;
			label.Top = currentScreenInfo.currentY;
			label.Left = currentScreenInfo.currentX;
			label.PerformLayout();
			currentScreenInfo.parentControl.Controls.Add(label);
			currentScreenInfo.NotifyAddedControl();
			currentScreenInfo.GotoNextColumn();
			if (num > 0.0)
			{
				currentScreenInfo.WidthPercent += (double)Convert.ToInt32(num * currentScreenInfo.WidthPercent);
				currentScreenInfo.columnWidth += Convert.ToInt32(num * (double)currentScreenInfo.columnWidth);
			}
			return null;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00028C20 File Offset: 0x00027C20
		private static Control AddControl(Control control, ref ScreenInfo currentScreenInfo, int verticalPad)
		{
			return DynamicScreen.AddControl(control, ref currentScreenInfo, verticalPad, 999999);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00028C40 File Offset: 0x00027C40
		private static Control AddControl(Control control, ref ScreenInfo currentScreenInfo, int verticalPad, bool dontWrapToNextLine)
		{
			return DynamicScreen.AddControl(control, ref currentScreenInfo, verticalPad, 999999, dontWrapToNextLine);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00028C60 File Offset: 0x00027C60
		private static Control AddControl(Control control, ref ScreenInfo currentScreenInfo, int verticalPad, int controlCode)
		{
			return DynamicScreen.AddControl(control, ref currentScreenInfo, verticalPad, controlCode, false);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00028C7C File Offset: 0x00027C7C
		private static Control AddControl(Control control, ref ScreenInfo currentScreenInfo, int verticalPad, int controlCode, bool dontWrapToNextLine)
		{
			int num = control.Visible ? (control.Height + verticalPad + currentScreenInfo.verticalControlPad) : 0;
			if (control is Label)
			{
				Label label = (Label)control;
				using (Graphics graphics = label.CreateGraphics())
				{
					SizeF sizeF = graphics.MeasureString(label.Text, label.Font, label.Width);
					if (sizeF.Height > (float)num)
					{
						num = Convert.ToInt32(sizeF.Height);
					}
				}
			}
			int y = currentScreenInfo.currentY + num;
			bool flag;
			if (!currentScreenInfo.WillYFitInCurrentColumn(y))
			{
				flag = true;
				if (currentScreenInfo.currentY > currentScreenInfo.BORDERPADY)
				{
					currentScreenInfo.GotoNextColumn();
				}
				else if (currentScreenInfo.parentControl.Height < num + currentScreenInfo.BORDERPADY)
				{
					currentScreenInfo.parentControl.Height = num + currentScreenInfo.BORDERPADY;
				}
			}
			else
			{
				flag = false;
			}
			control.Top = currentScreenInfo.currentY;
			control.Left = currentScreenInfo.currentX + currentScreenInfo.tempOffsetX;
			bool flag2 = true;
			if (flag)
			{
				if (controlCode == 8)
				{
					flag2 = false;
				}
			}
			Control result;
			if (flag2)
			{
				if (dontWrapToNextLine)
				{
					currentScreenInfo.tempOffsetX = control.Left + control.Width;
					currentScreenInfo.BiggestCurrentRowHeight = num;
				}
				else
				{
					if (currentScreenInfo.BiggestCurrentRowHeight > num)
					{
						num = currentScreenInfo.BiggestCurrentRowHeight;
					}
					currentScreenInfo.currentY += num;
					currentScreenInfo.tempOffsetX = 0;
					currentScreenInfo.BiggestCurrentRowHeight = 0;
				}
				control.PerformLayout();
				currentScreenInfo.parentControl.Controls.Add(control);
				currentScreenInfo.NotifyAddedControl();
				result = control;
			}
			else
			{
				control.Dispose();
				result = null;
			}
			return result;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00028EA0 File Offset: 0x00027EA0
		private static void AddControlsHorizontal(Control[] controls, ref ScreenInfo currentScreenInfo, int verticalPad, DynamicControl dc)
		{
			DynamicScreen.AddControlsHorizontal(controls, ref currentScreenInfo, verticalPad, false, dc);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00028EB0 File Offset: 0x00027EB0
		private static void AddControlsHorizontal(Control[] controls, ref ScreenInfo currentScreenInfo, int verticalPad, bool dontWrapToNextLine, DynamicControl dc)
		{
			int num = currentScreenInfo.currentY;
			int num2 = 0;
			bool flag = true;
			foreach (Control control in controls)
			{
				if (control.Height > num2)
				{
					num2 = control.Height;
				}
				if (control.Visible)
				{
					flag = false;
				}
			}
			string text;
			if (controls.Length > 0 && controls[0] is Label)
			{
				Label label = (Label)controls[0];
				using (Graphics graphics = label.CreateGraphics())
				{
					SizeF sizeF = graphics.MeasureString(label.Text, label.Font, label.Width);
					if (sizeF.Height > (float)num2)
					{
						num2 = Convert.ToInt32(sizeF.Height);
					}
					label.Height = num2 + verticalPad + currentScreenInfo.verticalControlPad;
					int num3 = Convert.ToInt32(sizeF.Width);
					if (label.Width < num3)
					{
						label.Width = num3;
					}
					label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				}
				text = label.Text;
			}
			else
			{
				text = "";
			}
			if (!flag)
			{
				num += num2 + verticalPad + currentScreenInfo.verticalControlPad;
				if (!currentScreenInfo.WillYFitInCurrentColumn(num))
				{
					if (currentScreenInfo.currentY > currentScreenInfo.BORDERPADY)
					{
						currentScreenInfo.GotoNextColumn();
					}
					else if (currentScreenInfo.parentControl.Height < num2 + currentScreenInfo.BORDERPADY)
					{
						currentScreenInfo.parentControl.Height = num2 + currentScreenInfo.BORDERPADY;
					}
				}
			}
			int num4 = 0;
			int tempOffsetX = 0;
			foreach (Control control in controls)
			{
				control.Top = currentScreenInfo.currentY;
				control.Left = currentScreenInfo.currentX + num4 + currentScreenInfo.tempOffsetX;
				num4 += control.Width;
				if (text.Length > 0)
				{
					control.AccessibleDescription = text;
					control.AccessibleName = text;
				}
				control.PerformLayout();
				currentScreenInfo.parentControl.Controls.Add(control);
				tempOffsetX = control.Left + control.Width + 4;
			}
			if (!flag)
			{
				int num5 = num2 + verticalPad + currentScreenInfo.verticalControlPad;
				if (dontWrapToNextLine)
				{
					currentScreenInfo.tempOffsetX = tempOffsetX;
					currentScreenInfo.BiggestCurrentRowHeight = num5;
				}
				else
				{
					if (currentScreenInfo.BiggestCurrentRowHeight > num5)
					{
						num5 = currentScreenInfo.BiggestCurrentRowHeight;
					}
					currentScreenInfo.currentY += num5;
					currentScreenInfo.tempOffsetX = 0;
					currentScreenInfo.BiggestCurrentRowHeight = 0;
				}
			}
			currentScreenInfo.NotifyAddedControl();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000291D4 File Offset: 0x000281D4
		private static void AddControlsVertical(Control[] controls, ref ScreenInfo currentScreenInfo, int[] verticalPad)
		{
			currentScreenInfo.BiggestCurrentRowHeight = 0;
			int num = currentScreenInfo.currentY;
			int num2 = 0;
			int num3 = 0;
			foreach (Control control in controls)
			{
				if (control.Visible)
				{
					num2 += control.Height + verticalPad[num3++];
				}
			}
			if (num2 > 0)
			{
				num2 += currentScreenInfo.verticalControlPad;
			}
			num += num2;
			if (!currentScreenInfo.WillYFitInCurrentColumn(num) && num2 > 0)
			{
				currentScreenInfo.GotoNextColumn();
			}
			int num4 = 0;
			foreach (Control control in controls)
			{
				bool visible = control.Visible;
				control.Top = currentScreenInfo.currentY;
				control.Left = currentScreenInfo.currentX;
				control.PerformLayout();
				currentScreenInfo.parentControl.Controls.Add(control);
				int num5 = visible ? (control.Height + verticalPad[num4++] + currentScreenInfo.verticalControlPad) : 0;
				currentScreenInfo.currentY += num5;
			}
			currentScreenInfo.tempOffsetX = 0;
			currentScreenInfo.NotifyAddedControl();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00029328 File Offset: 0x00028328
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, UnivDataAdapter da, bool useFrench = false)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.ChildrenInLookupLists);
			string text = useFrench ? "coalesce(nullif(lookupvalue,''),lookuptext) AS lookuptext" : "lookuptext";
			string text2 = "d" + lookupGroupID.ToString();
			DataTable dataTable = comboBoxData.Tables[text2];
			DataTable result;
			if (dataTable != null)
			{
				DataTable dataTable2 = dataTable.Copy();
				result = dataTable2;
			}
			else
			{
				if (shouldAddBlankFirstItem)
				{
					if (flag)
					{
						da.SelectCommand.CommandText = string.Concat(new string[]
						{
							"SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children,'' AS lookupvalue UNION SELECT lookuplistid,lookupgroupid,",
							text,
							",ordernum,children,lookupvalue FROM lookuplists WHERE lookupgroupid=",
							lookupGroupID.ToString(),
							" ORDER BY ordernum,lookuptext"
						});
					}
					else
					{
						da.SelectCommand.CommandText = string.Concat(new string[]
						{
							"SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children,'' AS lookupvalue UNION SELECT lookuplistid,lookupgroupid,",
							text,
							",ordernum,'' AS children,lookupvalue FROM lookuplists WHERE lookupgroupid=",
							lookupGroupID.ToString(),
							" ORDER BY ordernum,lookuptext"
						});
					}
				}
				else if (flag)
				{
					da.SelectCommand.CommandText = "SELECT lookuplistid,lookupgroupid," + text + ",children,lookupvalue FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";
				}
				else
				{
					da.SelectCommand.CommandText = "SELECT lookuplistid,lookupgroupid," + text + ",'' AS children,lookupvalue FROM lookuplists WHERE lookupgroupid=@lookupgroupid ORDER BY ordernum,lookuptext";
				}
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@lookupgroupid", lookupGroupID);
				dataTable = new DataTable(text2);
				string text3;
				da.Fill(dataTable, out text3);
				if (text3 != null && text3.Length > 0)
				{
					MessageBox.Show(text3);
				}
				comboBoxData.Tables.Add(dataTable);
				DataTable dataTable3;
				if (!comboBoxData.Tables.Contains("child"))
				{
					dataTable3 = new DataTable("child");
					dataTable3.Columns.Add("tablename");
					dataTable3.Columns.Add("childlookupgroupid", typeof(int));
					comboBoxData.Tables.Add(dataTable3);
				}
				else
				{
					dataTable3 = comboBoxData.Tables["child"];
				}
				da.SelectCommand.CommandText = "SELECT childlist FROM lookupgroups WHERE lookupgroupid=" + lookupGroupID.ToString();
				DataTable dataTable4 = new DataTable();
				da.Fill(dataTable4);
				if (dataTable4.Rows.Count > 0 && dataTable4.Rows[0][0] != DBNull.Value)
				{
					DataRow dataRow = dataTable3.NewRow();
					dataRow[0] = dataTable.TableName;
					dataRow[1] = (int)dataTable4.Rows[0][0];
					dataTable3.Rows.Add(dataRow);
				}
				result = dataTable.Copy();
			}
			return result;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00029628 File Offset: 0x00028628
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, OdbcDataAdapter da)
		{
			return DynamicScreen.GetLookupList(lookupGroupID, shouldAddBlankFirstItem, defaultIndex, ref comboBoxData, da, false);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00029648 File Offset: 0x00028648
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, OdbcDataAdapter da, bool useFrench)
		{
			string text = useFrench ? "lookupvalue" : "lookuptext";
			string text2 = "d" + lookupGroupID.ToString();
			DataTable dataTable = comboBoxData.Tables[text2];
			DataTable result;
			if (dataTable != null)
			{
				result = dataTable.Copy();
			}
			else
			{
				if (shouldAddBlankFirstItem)
				{
					da.SelectCommand.CommandText = string.Concat(new string[]
					{
						"SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children UNION SELECT lookuplistid,lookupgroupid,",
						text,
						" AS lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=",
						lookupGroupID.ToString(),
						" ORDER BY ordernum,lookuptext"
					});
				}
				else
				{
					da.SelectCommand.CommandText = string.Concat(new string[]
					{
						"SELECT lookuplistid,lookupgroupid,",
						text,
						" AS lookuptext,children FROM lookuplists WHERE lookupgroupid=",
						lookupGroupID.ToString(),
						" ORDER BY ordernum,lookuptext"
					});
				}
				da.SelectCommand.CommandText = string.Concat(new string[]
				{
					"SELECT lookuplistid,lookupgroupid,",
					text,
					" AS lookuptext,children FROM lookuplists WHERE lookupgroupid=",
					lookupGroupID.ToString(),
					" ORDER BY ordernum,lookuptext"
				});
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@lookupgroupid", lookupGroupID);
				dataTable = new DataTable(text2);
				try
				{
					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
					dataTable = new DataTable();
				}
				if (shouldAddBlankFirstItem)
				{
					comboBoxData.Tables.Add(dataTable);
				}
				DataTable dataTable2;
				if (!comboBoxData.Tables.Contains("child"))
				{
					dataTable2 = new DataTable("child");
					dataTable2.Columns.Add("tablename");
					dataTable2.Columns.Add("childlookupgroupid", typeof(int));
					comboBoxData.Tables.Add(dataTable2);
				}
				else
				{
					dataTable2 = comboBoxData.Tables["child"];
				}
				da.SelectCommand.CommandText = "SELECT childlist FROM lookupgroups WHERE lookupgroupid=" + lookupGroupID.ToString();
				DataTable dataTable3 = new DataTable();
				da.Fill(dataTable3);
				if (dataTable3.Rows.Count > 0 && dataTable3.Rows[0][0] != DBNull.Value)
				{
					DataRow dataRow = dataTable2.NewRow();
					dataRow[0] = dataTable.TableName;
					dataRow[1] = (int)dataTable3.Rows[0][0];
					dataTable2.Rows.Add(dataRow);
				}
				result = dataTable.Copy();
			}
			return result;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0002993C File Offset: 0x0002893C
		public static DataTable GetLookupList(int lookupGroupID, bool shouldAddBlankFirstItem, int defaultIndex, ref DataSet comboBoxData, bool useFrench)
		{
			string text = useFrench ? "lookupvalue" : "lookuptext";
			string text2 = "d" + lookupGroupID.ToString();
			DataTable dataTable = comboBoxData.Tables[text2];
			DataTable result;
			if (dataTable != null)
			{
				result = dataTable.Copy();
			}
			else
			{
				string commandText;
				if (shouldAddBlankFirstItem)
				{
					commandText = string.Concat(new string[]
					{
						"SELECT NULL AS lookuplistid,NULL AS lookupgroupid,NULL AS lookuptext,-999 AS ordernum,'' AS children UNION SELECT lookuplistid,lookupgroupid,",
						text,
						" AS lookuptext,ordernum,children FROM lookuplists WHERE lookupgroupid=",
						lookupGroupID.ToString(),
						" ORDER BY ordernum,lookuptext"
					});
				}
				else
				{
					commandText = string.Concat(new string[]
					{
						"SELECT lookuplistid,lookupgroupid,",
						text,
						" AS lookuptext,children FROM lookuplists WHERE lookupgroupid=",
						lookupGroupID.ToString(),
						" ORDER BY ordernum,lookuptext"
					});
				}
				commandText = string.Concat(new string[]
				{
					"SELECT lookuplistid,lookupgroupid,",
					text,
					" AS lookuptext,children FROM lookuplists WHERE lookupgroupid=",
					lookupGroupID.ToString(),
					" ORDER BY ordernum,lookuptext"
				});
				dataTable = new DataTable(text2);
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				try
				{
					da.SelectCommand.CommandText = commandText;
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@lookupgroupid", lookupGroupID);
					da.Fill(dataTable);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
					dataTable = new DataTable();
				}
				dataTable.TableName = text2;
				if (shouldAddBlankFirstItem)
				{
					comboBoxData.Tables.Add(dataTable);
				}
				DataTable dataTable2;
				if (!comboBoxData.Tables.Contains("child"))
				{
					dataTable2 = new DataTable("child");
					dataTable2.Columns.Add("tablename");
					dataTable2.Columns.Add("childlookupgroupid", typeof(int));
					comboBoxData.Tables.Add(dataTable2);
				}
				else
				{
					dataTable2 = comboBoxData.Tables["child"];
				}
				commandText = "SELECT childlist FROM lookupgroups WHERE lookupgroupid=" + lookupGroupID.ToString();
				DataTable dataTable3 = new DataTable();
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.Fill(dataTable3);
				if (dataTable3.Rows.Count > 0 && dataTable3.Rows[0][0] != DBNull.Value)
				{
					DataRow dataRow = dataTable2.NewRow();
					dataRow[0] = dataTable.TableName;
					dataRow[1] = (int)dataTable3.Rows[0][0];
					dataTable2.Rows.Add(dataRow);
				}
				result = dataTable.Copy();
			}
			return result;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00029C44 File Offset: 0x00028C44
		public static void ResetScreenToDefaults(Control parentControl, bool useDefaults)
		{
			DynamicScreen.ResetScreenToDefaults(parentControl, useDefaults, new Dictionary<string, string>());
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00029C54 File Offset: 0x00028C54
		public static void ResetScreenToDefaults(Control parentControl, bool useDefaults, Dictionary<string, string> overrideValues)
		{
			foreach (object obj in parentControl.Controls)
			{
				Control control = (Control)obj;
				if (control is MyTabControl)
				{
					((MyTabControl)control).ClearDisplayIfFieldsAreFilledIn();
				}
				if (control.Controls.Count > 0)
				{
					if (!(control is AccommodationControl2))
					{
						DynamicScreen.ResetScreenToDefaults(control, useDefaults, overrideValues);
					}
				}
				if (control is MyRadioGroupPrimary)
				{
					MyRadioGroupPrimary myRadioGroupPrimary = (MyRadioGroupPrimary)control;
					myRadioGroupPrimary.Clear();
				}
				else if (control.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)control.Tag;
					if (dataRow.Table.Columns.Contains("controlcode"))
					{
						int num = (int)dataRow[2];
						string key = dataRow["controlcaption"].ToString().ToLower();
						int num2 = (int)dataRow[7];
						string text = dataRow.Table.Columns.Contains("defaultvaluestring") ? dataRow["defaultvaluestring"].ToString().Trim() : "";
						if (!(control is Label))
						{
							if (control is MyCheckBox)
							{
								MyCheckBox myCheckBox = (MyCheckBox)control;
								bool @checked = useDefaults && (num2 & 1) == 1;
								if (overrideValues.ContainsKey(key))
								{
									@checked = (overrideValues[key].Length > 0);
								}
								myCheckBox.Checked = @checked;
							}
							else if (control is MyTextBox)
							{
								string text2 = (useDefaults && text.Length > 0) ? text : "";
								if (overrideValues.ContainsKey(key))
								{
									text2 = overrideValues[key];
								}
								control.Text = text2;
								MyTextBox myTextBox = (MyTextBox)control;
								if (myTextBox.OnlyAllowAdding && !myTextBox.IsReadOnly)
								{
									myTextBox.ReadOnly = false;
								}
								myTextBox.ClearAddedText();
							}
							else if (control is MaskedTextBox)
							{
								control.Text = ((useDefaults && text.Length > 0) ? text : "");
							}
							else if (control is TextBox)
							{
								if (overrideValues.ContainsKey(key))
								{
									control.Text = overrideValues[key];
								}
								if (useDefaults)
								{
									control.Text = text;
								}
								else
								{
									control.Text = "";
								}
							}
							else if (control is CheckBox)
							{
								CheckBox checkBox = (CheckBox)control;
								if (!useDefaults)
								{
									num2 = 0;
								}
								checkBox.Checked = DynamicScreen.IntToBool(num2);
							}
							else if (control is RadioButton)
							{
								RadioButton radioButton = (RadioButton)control;
								if (!useDefaults)
								{
									num2 = 0;
								}
								radioButton.Checked = DynamicScreen.IntToBool(num2);
							}
							else if (control is MyRadioGroup)
							{
								MyRadioGroup myRadioGroup = (MyRadioGroup)control;
								myRadioGroup.ClearCheckedRadioButtons();
							}
							else if (control is CtrlDateTimePicker)
							{
								CtrlDateTimePicker ctrlDateTimePicker = (CtrlDateTimePicker)control;
								if (!useDefaults || num2 <= 0)
								{
									ctrlDateTimePicker.Value = null;
								}
								else
								{
									ctrlDateTimePicker.Value = new DateTime?(DateTime.Now.Date);
								}
							}
							else if (control is MyDateTimePicker)
							{
								MyDateTimePicker myDateTimePicker = (MyDateTimePicker)control;
								if (!useDefaults || num2 <= 0)
								{
									myDateTimePicker.Value = DateTime.MinValue;
								}
								else
								{
									myDateTimePicker.Value = DateTime.Now.Date;
								}
							}
							else if (control is MyDateTimePickerForAccommodationsExpiry)
							{
								MyDateTimePickerForAccommodationsExpiry myDateTimePickerForAccommodationsExpiry = (MyDateTimePickerForAccommodationsExpiry)control;
								myDateTimePickerForAccommodationsExpiry.ClearDate();
							}
							else if (control is AutoComboBox)
							{
								AutoComboBox autoComboBox = (AutoComboBox)control;
								int num3 = autoComboBox.defaultIndex;
								if (!useDefaults)
								{
									num3 = -1;
								}
								if (num3 >= 0 && autoComboBox.DataSource is DataTable && autoComboBox.defaultIndex < ((DataTable)autoComboBox.DataSource).Rows.Count)
								{
									autoComboBox.SelectedIndex = num3;
								}
								else if (num3 >= 0 && autoComboBox.DataSource is DataView)
								{
									autoComboBox.SelectIndexByValueMember(num3);
								}
								else
								{
									autoComboBox.SelectedIndex = -1;
								}
								try
								{
									autoComboBox.Text = "";
								}
								catch
								{
								}
							}
							else if (control is ListViewEx)
							{
								ListViewEx listViewEx = (ListViewEx)control;
								listViewEx.Items.Clear();
							}
							else if (control is MyRichText)
							{
								((MyRichText)control).SetTextFromDatabase("", null);
							}
							else if (control is MyMultilineTextBoxWithEditingControls)
							{
								((MyMultilineTextBoxWithEditingControls)control).Clear();
							}
							else if (control is AccommodationControl2)
							{
								AccommodationControl2 accommodationControl = (AccommodationControl2)control;
								accommodationControl.Reset();
							}
							else if (control is MyMultiCheckbox)
							{
								((MyMultiCheckbox)control).Reset();
							}
							else if (control is CtrlFileList)
							{
								CtrlFileList ctrlFileList = (CtrlFileList)control;
								ctrlFileList.ClearData();
							}
							else if (!(control is MyRadioGroupPrimaryCheckboxMultiple) && !(control is Label) && !(control is Button))
							{
								control.Text = "";
							}
						}
					}
				}
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0002A318 File Offset: 0x00029318
		private static void TellControlsAboutNewContextPid(Control parent, int pid)
		{
			if (parent is PMTable)
			{
				PMTable pmtable = (PMTable)parent;
				pmtable.Pid = pid;
			}
			else if (parent is CtrlSignedDocumentButton)
			{
				CtrlSignedDocumentButton ctrlSignedDocumentButton = (CtrlSignedDocumentButton)parent;
				ctrlSignedDocumentButton.Pid = pid;
			}
			else if (parent is AppointmentHistory)
			{
				AppointmentHistory appointmentHistory = (AppointmentHistory)parent;
				appointmentHistory.Pid = pid;
			}
			else if (parent is EmailHistory)
			{
				EmailHistory emailHistory = (EmailHistory)parent;
				emailHistory.Pid = pid;
			}
			else if (parent is AutoComboBox)
			{
				AutoComboBox autoComboBox = (AutoComboBox)parent;
				autoComboBox.Pid = pid;
			}
			else if (parent is CtrlSignedDocumentButton)
			{
				CtrlSignedDocumentButton ctrlSignedDocumentButton2 = (CtrlSignedDocumentButton)parent;
				ctrlSignedDocumentButton2.Pid = pid;
			}
			else if (parent is CtrlFileList)
			{
				CtrlFileList ctrlFileList = (CtrlFileList)parent;
				ctrlFileList.Pid = pid;
			}
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				DynamicScreen.TellControlsAboutNewContextPid(parent2, pid);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0002A49C File Offset: 0x0002949C
		private static DataTable LoadData(UnivDataAdapter da, bool isAppointment, bool isAccommodation, int screennum, int personid, string returnDataTableName, string databaseTableName)
		{
			bool flag = isAccommodation && DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.NewAccommodations_Dec2008);
			bool flag2 = isAccommodation && DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.NewAccommodations_July2009);
			DataTable result;
			if (da.DoesTableExist(databaseTableName))
			{
				da.SelectCommand.CommandText = "SELECT dataID,@screennum AS screenNum,personID,controlID,controlValue";
				if (isAppointment)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += ",appointmentid";
				}
				if (isAccommodation)
				{
					UnivCommand selectCommand2 = da.SelectCommand;
					selectCommand2.CommandText += ",courseid,flavour";
					if (flag)
					{
						UnivCommand selectCommand3 = da.SelectCommand;
						selectCommand3.CommandText += ",offline,datemodified,whomodified,expirydate,altlongdescription,note,showonletter,sessiondateentered";
						if (flag2)
						{
							UnivCommand selectCommand4 = da.SelectCommand;
							selectCommand4.CommandText += ",approved,recommendedbutdeclined,recommendedbutdeclineddetail";
						}
					}
				}
				UnivCommand selectCommand5 = da.SelectCommand;
				selectCommand5.CommandText = selectCommand5.CommandText + " FROM " + databaseTableName + " WHERE controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum) AND ";
				if (personid < 0)
				{
					UnivCommand selectCommand6 = da.SelectCommand;
					selectCommand6.CommandText += "1=0";
				}
				else
				{
					UnivCommand selectCommand7 = da.SelectCommand;
					selectCommand7.CommandText += "personid=@personid";
				}
				UnivCommand selectCommand8 = da.SelectCommand;
				selectCommand8.CommandText += " ORDER BY controlid";
				da.SelectCommand.Parameters.Clear();
				if (personid >= 0)
				{
					da.SelectCommand.Parameters.Add("@personid", personid);
				}
				da.SelectCommand.Parameters.Add("@screennum", screennum);
				DataTable dataTable = new DataTable(returnDataTableName);
				string text;
				da.Fill(dataTable, out text);
				if (text != null && text.Length > 0)
				{
					throw new Exception("Can't loaddata!: " + text);
				}
				result = dataTable;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0002A6B0 File Offset: 0x000296B0
		public static DataSet LoadData(UnivDataAdapter da, Panel panel, int screenNum, int personID, string mainInfoTableName, string otherInfoTableName, string dateTimeInfoTableName, TripleDESEncryptionClass tripleDES, bool isAppointment, bool isAccommodation, int appointmentIDToDisplay, UseDefaults _UseDefaults, bool DataToScreen)
		{
			return DynamicScreen.LoadData(da, panel, screenNum, personID, mainInfoTableName, otherInfoTableName, dateTimeInfoTableName, null, tripleDES, isAppointment, isAccommodation, appointmentIDToDisplay, _UseDefaults, DataToScreen);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0002A6E0 File Offset: 0x000296E0
		public static DataSet LoadData(UnivDataAdapter da, Panel panel, int screenNum, int personID, string mainInfoTableName, string otherInfoTableName, string dateTimeInfoTableName, string imageInfoTableName, TripleDESEncryptionClass tripleDES, bool isAppointment, bool isAccommodation, int appointmentIDToDisplay, UseDefaults _UseDefaults, bool DataToScreen)
		{
			return DynamicScreen.LoadData(da, panel, screenNum, personID, mainInfoTableName, otherInfoTableName, dateTimeInfoTableName, imageInfoTableName, tripleDES, isAppointment, isAccommodation, appointmentIDToDisplay, _UseDefaults, DataToScreen, new Dictionary<string, string>());
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0002A714 File Offset: 0x00029714
		public static DataSet LoadData(UnivDataAdapter da, Panel panel, int screenNum, int personID, string mainInfoTableName, string otherInfoTableName, string dateTimeInfoTableName, string imageInfoTableName, TripleDESEncryptionClass tripleDES, bool isAppointment, bool isAccommodation, int appointmentIDToDisplay, UseDefaults _UseDefaults, bool DataToScreen, Dictionary<string, string> overrideDefaultValues)
		{
			DynamicScreen.TellControlsAboutNewContextPid(panel, personID);
			DataSet dataSet = new DataSet();
			DataTable dataTable;
			if (mainInfoTableName != null)
			{
				dataTable = dataSet.Tables["mainInfoTable"];
				if (dataTable == null)
				{
					dataTable = DynamicScreen.LoadData(da, isAppointment, isAccommodation, screenNum, personID, "mainInfoTable", mainInfoTableName);
					dataSet.Tables.Add(dataTable);
				}
			}
			else
			{
				dataTable = null;
			}
			DataTable dataTable2;
			if (otherInfoTableName != null)
			{
				dataTable2 = dataSet.Tables["otherInfoTable"];
				if (dataTable2 == null)
				{
					dataTable2 = DynamicScreen.LoadData(da, isAppointment, isAccommodation, screenNum, personID, "otherInfoTable", otherInfoTableName);
					dataSet.Tables.Add(dataTable2);
				}
			}
			else
			{
				dataTable2 = null;
			}
			DataTable dataTable3;
			if (dateTimeInfoTableName != null)
			{
				dataTable3 = dataSet.Tables["dateTimeInfoTable"];
				if (dataTable3 == null)
				{
					dataTable3 = DynamicScreen.LoadData(da, isAppointment, isAccommodation, screenNum, personID, "dateTimeInfoTable", dateTimeInfoTableName);
					dataSet.Tables.Add(dataTable3);
				}
			}
			else
			{
				dataTable3 = null;
			}
			DataTable dataTable4;
			if (imageInfoTableName != null && imageInfoTableName.Length > 0 && DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.DynamicImageData))
			{
				dataTable4 = dataSet.Tables["imageInfoTable"];
				if (dataTable4 == null)
				{
					dataTable4 = DynamicScreen.LoadData(da, isAppointment, isAccommodation, screenNum, personID, "imageInfoTable", imageInfoTableName);
					if (dataTable4 != null)
					{
						dataSet.Tables.Add(dataTable4);
					}
				}
			}
			else
			{
				dataTable4 = null;
			}
			bool useDefaults;
			if (_UseDefaults == UseDefaults.useDefaultsIfNoDataPresent)
			{
				int num = 0;
				if (dataTable != null)
				{
					num += DynamicScreen.GetDataRowCount(dataTable, isAppointment, appointmentIDToDisplay);
				}
				if (dataTable2 != null)
				{
					num += DynamicScreen.GetDataRowCount(dataTable2, isAppointment, appointmentIDToDisplay);
				}
				if (dataTable3 != null)
				{
					num += DynamicScreen.GetDataRowCount(dataTable3, isAppointment, appointmentIDToDisplay);
				}
				if (dataTable4 != null)
				{
					num += DynamicScreen.GetDataRowCount(dataTable4, isAppointment, appointmentIDToDisplay);
				}
				useDefaults = (num < 1);
			}
			else
			{
				useDefaults = (_UseDefaults == UseDefaults.useDefaults);
			}
			if (DataToScreen)
			{
				DynamicScreen.ResetScreenToDefaults(panel, useDefaults, overrideDefaultValues);
			}
			if (DataToScreen)
			{
				if (dataTable != null)
				{
					if (!isAppointment || (isAppointment && appointmentIDToDisplay >= 0))
					{
						DynamicScreen.SetControlValues(panel, dataTable, tripleDES, appointmentIDToDisplay, false, da, overrideDefaultValues);
					}
				}
				if (dataTable2 != null)
				{
					if (!isAppointment || (isAppointment && appointmentIDToDisplay >= 0))
					{
						DynamicScreen.SetControlValues(panel, dataTable2, tripleDES, appointmentIDToDisplay, false, da, overrideDefaultValues);
					}
				}
				if (dataTable3 != null)
				{
					if (!isAppointment || (isAppointment && appointmentIDToDisplay >= 0))
					{
						DynamicScreen.SetControlValues(panel, dataTable3, tripleDES, appointmentIDToDisplay, false, da, overrideDefaultValues);
					}
				}
				if (dataTable4 != null)
				{
					if (!isAppointment || (isAppointment && appointmentIDToDisplay >= 0))
					{
						DynamicScreen.SetControlValues(panel, dataTable4, tripleDES, appointmentIDToDisplay, false, da, overrideDefaultValues);
					}
				}
				if (panel is MyPanel)
				{
					MyPanel myPanel = (MyPanel)panel;
					myPanel.FireDataRenderCompleted(personID);
				}
			}
			return dataSet;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0002AA50 File Offset: 0x00029A50
		public static int GetDataRowCount(DataTable dynamicDataTable, bool isAppointment, int appointmentId)
		{
			int num = 0;
			foreach (object obj in dynamicDataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					if (isAppointment)
					{
						int num2 = (int)dataRow["appointmentid"];
						if (num2 == appointmentId)
						{
							num++;
						}
					}
					else
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0002AB08 File Offset: 0x00029B08
		public static string[] StringToStrings(string s)
		{
			return s.Split(DynamicScreen.ByteStringDelimiter.ToCharArray());
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0002AB2C File Offset: 0x00029B2C
		public static string StringsToString(string[] ss)
		{
			string text = "";
			for (int i = 0; i < ss.Length; i++)
			{
				if (i > 0)
				{
					text += DynamicScreen.ByteStringDelimiter;
				}
				text += ss[i];
			}
			return text;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0002AB7C File Offset: 0x00029B7C
		public static StringDictionary ParseArgs(string args, char delimiter)
		{
			return DynamicScreen.ParseArgs(args, new char[]
			{
				delimiter
			});
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0002ABA0 File Offset: 0x00029BA0
		public static StringDictionary ParseArgs(string args, char[] delimiter)
		{
			string[] array = args.Split(delimiter);
			StringDictionary stringDictionary = new StringDictionary();
			foreach (string text in array)
			{
				if (text.Trim().Length > 0)
				{
					int num = text.IndexOf('=');
					if (num > 0)
					{
						stringDictionary.Add(text.Substring(0, num), text.Substring(num + 1));
					}
					else
					{
						stringDictionary.Add(text, "");
					}
				}
			}
			return stringDictionary;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0002AC40 File Offset: 0x00029C40
		public static string GetTempFilename(string fnExtension)
		{
			string tempFileName = Path.GetTempFileName();
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = Path.GetFileNameWithoutExtension(tempFileName) + fnExtension;
			return Path.Combine(text, path);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0002AC9D File Offset: 0x00029C9D
		public static void SetControlValues(Control panel, DataTable t, TripleDESEncryptionClass tripleDES, int appointmentID, UnivDataAdapter da)
		{
			DynamicScreen.SetControlValues(panel, t, tripleDES, appointmentID, false, da);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0002ACAD File Offset: 0x00029CAD
		public static void SetControlValues(Control panel, DataTable t, TripleDESEncryptionClass tripleDES, int appointmentID, bool forceUseAppointmentId, UnivDataAdapter da)
		{
			DynamicScreen.SetControlValues(panel, t, tripleDES, appointmentID, forceUseAppointmentId, da, new Dictionary<string, string>());
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0002ACC4 File Offset: 0x00029CC4
		public static void SetControlValues(Control panel, DataTable t, TripleDESEncryptionClass tripleDES, int appointmentID, bool forceUseAppointmentId, UnivDataAdapter da, Dictionary<string, string> overrideDefaultValues)
		{
			bool flag = t.Columns.Contains("courseid");
			MyTabControl[] array = null;
			foreach (object obj in panel.Controls)
			{
				Control control = (Control)obj;
				if (control is MyTabControl)
				{
					array = new MyTabControl[]
					{
						(MyTabControl)control
					};
					break;
				}
			}
			foreach (object obj2 in t.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				try
				{
					if (dataRow.RowState != DataRowState.Deleted && ((appointmentID < 0 && !forceUseAppointmentId) || ((appointmentID >= 0 || forceUseAppointmentId) && (int)dataRow[5] == appointmentID)))
					{
						int controlID = (int)dataRow[3];
						Control control2 = DynamicScreen.GetControl(panel, controlID);
						DataRow dataRow2;
						bool isMultipleTextBox;
						if (control2 == null)
						{
							control2 = DynamicScreen.GetControlMultipleTextBox(panel, controlID, out dataRow2);
							isMultipleTextBox = true;
						}
						else if (control2 is ListSelect)
						{
							dataRow2 = null;
							isMultipleTextBox = false;
						}
						else
						{
							isMultipleTextBox = false;
							dataRow2 = (DataRow)control2.Tag;
						}
						if (control2 != null)
						{
							int controlCode;
							if (control2 is ListSelect)
							{
								controlCode = 301;
							}
							else
							{
								controlCode = (int)dataRow2[2];
							}
							try
							{
								DynamicScreen.SetControlValues(controlCode, controlID, control2, dataRow, dataRow2, tripleDES, isMultipleTextBox, da, overrideDefaultValues);
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.ToString());
							}
							if (flag)
							{
							}
						}
					}
				}
				catch (Exception ex2)
				{
					MessageBox.Show(ex2.ToString());
				}
			}
			if (array != null)
			{
				foreach (MyTabControl myTabControl in array)
				{
					myTabControl.ShowDisplayIfFieldsAreFilledIn();
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0002AF84 File Offset: 0x00029F84
		private static void SetControlValues(int controlCode, int controlID, Control foundControl, DataRow dr, DataRow controlDR, TripleDESEncryptionClass tripleDES, bool isMultipleTextBox, UnivDataAdapter da, bool encryptedDataIsInCopiedDataAsPlainText)
		{
			DynamicScreen.SetControlValues(controlCode, controlID, foundControl, dr, controlDR, tripleDES, isMultipleTextBox, da, new Dictionary<string, string>(), encryptedDataIsInCopiedDataAsPlainText);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0002AFAC File Offset: 0x00029FAC
		private static void SetControlValues(int controlCode, int controlID, Control foundControl, DataRow dr, DataRow controlDR, TripleDESEncryptionClass tripleDES, bool isMultipleTextBox, UnivDataAdapter da)
		{
			DynamicScreen.SetControlValues(controlCode, controlID, foundControl, dr, controlDR, tripleDES, isMultipleTextBox, da, new Dictionary<string, string>(), false);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0002AFD4 File Offset: 0x00029FD4
		private static void SetControlValues(int controlCode, int controlID, Control foundControl, DataRow dr, DataRow controlDR, TripleDESEncryptionClass tripleDES, bool isMultipleTextBox, UnivDataAdapter da, Dictionary<string, string> overrideDefaultValues)
		{
			DynamicScreen.SetControlValues(controlCode, controlID, foundControl, dr, controlDR, tripleDES, isMultipleTextBox, da, overrideDefaultValues, false);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0002AFF8 File Offset: 0x00029FF8
		private static void SetControlValues(int controlCode, int controlID, Control foundControl, DataRow dr, DataRow controlDR, TripleDESEncryptionClass tripleDES, bool isMultipleTextBox, UnivDataAdapter da, Dictionary<string, string> overrideDefaultValues, bool encryptedDataIsInCopiedDataAsPlainText)
		{
			try
			{
				bool flag;
				if (foundControl != null && foundControl.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)foundControl.Tag;
					string key = dataRow["controlcaption"].ToString().ToLower();
					flag = overrideDefaultValues.ContainsKey(key);
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					int num = controlCode;
					if (num <= 500)
					{
						int num3;
						if (num <= 21)
						{
							switch (num)
							{
							case 1:
								goto IL_F5A;
							case 2:
								break;
							case 3:
							{
								AutoComboBox autoComboBox = (AutoComboBox)foundControl;
								int num2 = (int)controlDR[6];
								if (num2 == 0 || num2 == 2)
								{
									num3 = (int)dr[4];
									int lookupListCMBIndex = DynamicScreen.GetLookupListCMBIndex(autoComboBox, num3);
									int itemCount = autoComboBox.GetItemCount();
									if (lookupListCMBIndex >= 0 && lookupListCMBIndex < itemCount)
									{
										try
										{
											autoComboBox.SelectedIndex = lookupListCMBIndex;
										}
										catch (Exception ex)
										{
											MessageBox.Show(ex.ToString());
										}
									}
									else if (num3 > -1)
									{
										MessageBox.Show(string.Concat(new string[]
										{
											"The combobox item with lookuplistid ",
											num3.ToString(),
											" doesn't exist [controlID=",
											controlID.ToString(),
											"]!"
										}));
									}
								}
								else if (num2 < 0)
								{
									byte[] inputInBytes = (byte[])dr[4];
									string text = tripleDES.Decrypt(inputInBytes);
									DynamicScreen.SetComboText(autoComboBox, text.Trim());
								}
								else if (dr[4] != DBNull.Value && dr[4] is byte[])
								{
									byte[] bytes = (byte[])dr[4];
									UTF8Encoding utf8Encoding = new UTF8Encoding();
									string text = utf8Encoding.GetString(bytes);
									DynamicScreen.SetComboText(autoComboBox, text.Trim());
								}
								goto IL_13EE;
							}
							case 4:
								num3 = (int)dr[4];
								if (num3 > 0)
								{
									RadioButton radioButton = (RadioButton)foundControl;
									radioButton.Checked = true;
								}
								goto IL_13EE;
							case 5:
							case 7:
							case 8:
							case 9:
								goto IL_13EE;
							case 6:
							{
								DateTime dateTime;
								if (dr[4] == DBNull.Value)
								{
									dateTime = DateTime.MinValue;
								}
								else
								{
									dateTime = (DateTime)dr[4];
								}
								if (foundControl is MyDateTimePicker)
								{
									MyDateTimePicker myDateTimePicker = (MyDateTimePicker)foundControl;
									myDateTimePicker.Value = dateTime;
								}
								else if (foundControl is CtrlDateTimePicker)
								{
									CtrlDateTimePicker ctrlDateTimePicker = (CtrlDateTimePicker)foundControl;
									ctrlDateTimePicker.Value = ((dateTime == DateTime.MinValue) ? null : new DateTime?(dateTime));
								}
								else if (foundControl is TextBox)
								{
									TextBox textBox = (TextBox)foundControl;
									textBox.Text = ((dateTime == DateTime.MinValue) ? "" : dateTime.ToString("MMM d, yyyy"));
								}
								else
								{
									MyDateTimePickerForAccommodationsExpiry myDateTimePickerForAccommodationsExpiry = (MyDateTimePickerForAccommodationsExpiry)foundControl;
									myDateTimePickerForAccommodationsExpiry.Value = dateTime;
								}
								goto IL_13EE;
							}
							case 10:
							{
								byte[] bytes2 = (byte[])dr[4];
								UTF8Encoding utf8Encoding2 = new UTF8Encoding();
								string @string = utf8Encoding2.GetString(bytes2);
								ListViewEx listViewEx = (ListViewEx)foundControl;
								string[] array = @string.Split(new char[]
								{
									'\t'
								});
								listViewEx.Items.Clear();
								foreach (string text2 in array)
								{
									if (text2.Trim().Length > 0)
									{
										string text3 = text2;
										char[] separator = new char[1];
										string[] array3 = text3.Split(separator);
										string text4;
										if (array3.Length > 0)
										{
											text4 = array3[0];
										}
										else
										{
											text4 = "";
										}
										ListViewItem listViewItem = new ListViewItem(text4);
										int num4 = listViewEx.Columns.Count - array3.Length;
										if (num4 > 0)
										{
											string[] array4 = new string[listViewEx.Columns.Count];
											for (int j = 0; j < num4; j++)
											{
												array4[j] = "";
											}
											for (int j = 0; j < array3.Length; j++)
											{
												array4[j + num4] = array3[j];
											}
											array3 = array4;
										}
										for (int k = 1; k < array3.Length; k++)
										{
											string text5 = array3[k];
											listViewItem.SubItems.Add(text5);
										}
										listViewEx.Items.Add(listViewItem);
									}
								}
								goto IL_13EE;
							}
							default:
								if (num == 14)
								{
									num3 = (int)dr[4];
									if (foundControl is MyRadioGroupPrimary)
									{
										MyRadioGroupPrimary myRadioGroupPrimary = (MyRadioGroupPrimary)foundControl;
										if (num3 > 0)
										{
											Control parent = myRadioGroupPrimary.Parent;
											foreach (object obj in parent.Controls)
											{
												Control control = (Control)obj;
												if (control is MyRadioGroupPrimaryCheckboxMultiple && control.Tag != null && control.Tag is DataRow)
												{
													DataRow dataRow2 = (DataRow)control.Tag;
													int num5 = (int)dataRow2[0];
													if (num5 == num3)
													{
														MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
														myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked = true;
														break;
													}
												}
											}
										}
									}
									else
									{
										MyRadioGroup myRadioGroup = (MyRadioGroup)foundControl;
										myRadioGroup.SelectedId = num3;
									}
									goto IL_13EE;
								}
								switch (num)
								{
								case 20:
								{
									byte[] bytes3 = (byte[])dr[4];
									UTF8Encoding utf8Encoding3 = new UTF8Encoding();
									string string2 = utf8Encoding3.GetString(bytes3);
									CtrlFileList ctrlFileList = (CtrlFileList)foundControl;
									ctrlFileList.DynamicValue = string2;
									goto IL_13EE;
								}
								case 21:
								{
									byte[] array5 = (byte[])dr[4];
									if (array5 != null)
									{
										CtrlPicture ctrlPicture = (CtrlPicture)foundControl;
										ctrlPicture.DynamicData = new DynamicDataView
										{
											Field = null,
											DataId = 0,
											Value = array5
										};
									}
									goto IL_13EE;
								}
								default:
									goto IL_13EE;
								}
								break;
							}
						}
						else if (num <= 301)
						{
							if (num == 100)
							{
								AutoComboBox autoComboBox2 = (AutoComboBox)foundControl;
								num3 = (int)dr[4];
								int lookupListCMBIndex2 = DynamicScreen.GetLookupListCMBIndex(autoComboBox2, num3);
								int itemCount = autoComboBox2.GetItemCount();
								if (lookupListCMBIndex2 >= 0 && lookupListCMBIndex2 < itemCount)
								{
									autoComboBox2.SelectedIndex = lookupListCMBIndex2;
								}
								else if (num3 > -1)
								{
									da.SelectCommand.CommandText = "SELECT personid,firstname,lastname,student_no FROM people WHERE personid=" + num3.ToString();
									da.SelectCommand.Parameters.Clear();
									DataTable dataTable = new DataTable();
									da.Fill(dataTable);
									if (dataTable.Rows.Count > 0)
									{
										dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
										{
											"firstname",
											"lastname",
											"student_no"
										});
										DataTable dataTable2;
										if (autoComboBox2.DataSource is DataView)
										{
											dataTable2 = ((DataView)autoComboBox2.DataSource).Table;
										}
										else if (autoComboBox2.DataSource is DataTable)
										{
											dataTable2 = (DataTable)autoComboBox2.DataSource;
										}
										else
										{
											dataTable2 = null;
										}
										if (dataTable2 != null)
										{
											DataRow dataRow3 = dataTable.Rows[0];
											DataRow dataRow4 = dataTable2.NewRow();
											dataRow4[0] = num3;
											dataRow4[1] = dataRow3["firstname"];
											dataRow4[2] = dataRow3["lastname"];
											dataRow4[3] = dataRow3["student_no"];
											dataRow4[4] = dataRow3["lastname"].ToString() + ", " + dataRow3["firstname"].ToString();
											dataTable2.Rows.Add(dataRow4);
											lookupListCMBIndex2 = DynamicScreen.GetLookupListCMBIndex(autoComboBox2, num3);
											itemCount = autoComboBox2.GetItemCount();
											if (lookupListCMBIndex2 >= 0 && lookupListCMBIndex2 < itemCount)
											{
												autoComboBox2.SelectedIndex = lookupListCMBIndex2;
											}
											else
											{
												MessageBox.Show(string.Concat(new string[]
												{
													"The combobox item with lookuplistid ",
													num3.ToString(),
													" doesn't exist [controlID=",
													controlID.ToString(),
													"]!"
												}));
											}
										}
									}
									else
									{
										MessageBox.Show(string.Concat(new string[]
										{
											"The combobox item with lookuplistid ",
											num3.ToString(),
											" doesn't exist [controlID=",
											controlID.ToString(),
											"]!"
										}));
									}
								}
								goto IL_13EE;
							}
							switch (num)
							{
							case 300:
								goto IL_F5A;
							case 301:
							{
								num3 = (int)dr[4];
								ListSelect listSelect = (ListSelect)foundControl;
								if (num3 > 0)
								{
									listSelect.SetChecked(controlID);
								}
								goto IL_13EE;
							}
							default:
								goto IL_13EE;
							}
						}
						else
						{
							if (num == 400)
							{
								byte[] array6 = (byte[])dr[4];
								try
								{
									int num6 = 6;
									byte[] array7 = new byte[num6];
									for (int k = 0; k < num6; k++)
									{
										array7[k] = array6[k];
									}
									string s = DynamicScreen.BytesToString(array7, false, null);
									int num7 = int.Parse(s);
									byte[] array8 = new byte[num7];
									for (int k = 0; k < num7; k++)
									{
										array8[k] = array6[k + num6];
									}
									string args = DynamicScreen.BytesToString(array8, false, null);
									StringDictionary stringDictionary = DynamicScreen.ParseArgs(args, ';');
									string text6 = stringDictionary["filename"];
									int num8 = array6.Length - num6 - num7;
									byte[] array9 = new byte[num8];
									for (int k = 0; k < array9.Length; k++)
									{
										array9[k] = array6[k + num7 + num6];
									}
									if (text6 != null)
									{
										string tempFilename = DynamicScreen.GetTempFilename(Path.GetExtension(text6));
										FileStream fileStream = File.Create(tempFilename);
										BinaryWriter binaryWriter = new BinaryWriter(fileStream);
										binaryWriter.Write(array9);
										binaryWriter.Close();
										fileStream.Close();
										MyFile myFile = (MyFile)foundControl;
										myFile.Filename = tempFilename;
									}
								}
								catch (Exception ex2)
								{
									MessageBox.Show(ex2.Message);
								}
								goto IL_13EE;
							}
							if (num != 500)
							{
								goto IL_13EE;
							}
						}
						num3 = (int)dr[4];
						if (num3 > 0)
						{
							if (foundControl is CheckBox)
							{
								CheckBox checkBox = (CheckBox)foundControl;
								checkBox.Checked = true;
							}
							else if (foundControl is MyMultiCheckbox)
							{
								MyMultiCheckbox myMultiCheckbox = (MyMultiCheckbox)foundControl;
								myMultiCheckbox.CheckedIntVal = num3;
							}
							else
							{
								MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple2 = (MyRadioGroupPrimaryCheckboxMultiple)foundControl;
								myRadioGroupPrimaryCheckboxMultiple2.Checked = true;
							}
						}
						goto IL_13EE;
						IL_F5A:
						bool decrypt = !encryptedDataIsInCopiedDataAsPlainText && (int)controlDR[6] != 0;
						byte[] bytes4 = (byte[])dr[4];
						string text7 = DynamicScreen.BytesToString(bytes4, decrypt, tripleDES);
						if (isMultipleTextBox)
						{
							text7 = foundControl.Text + text7;
						}
						if (foundControl is MyTextBox)
						{
							MyTextBox myTextBox = (MyTextBox)foundControl;
							myTextBox.SetTextFromDatabase(text7, dr);
						}
						else
						{
							foundControl.Text = text7;
						}
						if (foundControl is MaskedTextBox)
						{
							if (foundControl.Text.CompareTo(text7) != 0)
							{
								MaskedTextBox maskedTextBox = (MaskedTextBox)foundControl;
								maskedTextBox.Mask = "";
								maskedTextBox.Text = text7;
							}
						}
					}
					else if (num <= 600)
					{
						if (num != 510)
						{
							if (num != 520)
							{
								if (num == 600)
								{
									byte[] bytes5 = (byte[])dr[4];
									bool decrypt2 = !encryptedDataIsInCopiedDataAsPlainText && (int)controlDR[6] != 0;
									MyRichText myRichText = (MyRichText)foundControl;
									myRichText.SetTextFromDatabase(DynamicScreen.BytesToString(bytes5, decrypt2, tripleDES), dr);
								}
							}
							else if (foundControl is MyMultiCheckbox)
							{
								MyMultiCheckbox myMultiCheckbox2 = (MyMultiCheckbox)foundControl;
								AutoComboBox autoComboBox = myMultiCheckbox2.GetComboBox();
								if (autoComboBox != null)
								{
									int num9 = (int)controlDR[6];
									if (num9 == 0 || num9 == 2)
									{
										int num10 = (int)dr[4];
										int numCheckboxes = myMultiCheckbox2.NumCheckboxes;
										int num3 = num10 >> numCheckboxes;
										num3--;
										myMultiCheckbox2.CheckedIntVal = num10;
										int lookupListCMBIndex = DynamicScreen.GetLookupListCMBIndex(autoComboBox, num3);
										int itemCount = autoComboBox.GetItemCount();
										if (lookupListCMBIndex >= 0 && lookupListCMBIndex < itemCount)
										{
											try
											{
												autoComboBox.SelectedIndex = lookupListCMBIndex;
											}
											catch (Exception ex)
											{
												MessageBox.Show(ex.ToString());
											}
										}
									}
									else if (dr.Table.Columns["controlvalue"].DataType == typeof(int))
									{
										int num3 = (int)dr[4];
										myMultiCheckbox2.CheckedIntVal = num3;
									}
									else
									{
										bool decrypt3 = num9 < 0;
										byte[] bytes6 = (byte[])dr[4];
										string text8 = DynamicScreen.BytesToString(bytes6, decrypt3, tripleDES);
										DynamicScreen.SetComboText(autoComboBox, text8.Trim());
									}
								}
							}
						}
						else if (foundControl is MyMultiCheckbox)
						{
							MyMultiCheckbox myMultiCheckbox2 = (MyMultiCheckbox)foundControl;
							if (dr.Table.Columns["controlvalue"].DataType == typeof(int))
							{
								myMultiCheckbox2.CheckedIntVal = (int)dr["controlvalue"];
							}
							else
							{
								bool decrypt4 = !encryptedDataIsInCopiedDataAsPlainText && (int)controlDR[6] != 0;
								byte[] bytes7 = (byte[])dr[4];
								string text7 = DynamicScreen.BytesToString(bytes7, decrypt4, tripleDES);
								myMultiCheckbox2.SetTextBoxText(text7);
							}
						}
					}
					else if (num <= 703)
					{
						if (num != 620)
						{
							switch (num)
							{
							case 700:
							{
								AccommodationControl2 accommodationControl = (AccommodationControl2)foundControl;
								CheckBox chk_caption = accommodationControl.Chk_caption;
								DynamicScreen.SetControlValues(2, controlID, chk_caption, dr, controlDR, tripleDES, false, da, encryptedDataIsInCopiedDataAsPlainText);
								chk_caption.Tag = null;
								DynamicScreen.SetExtraAccommodationData(accommodationControl, dr, tripleDES);
								break;
							}
							case 701:
							{
								AccommodationControl2 accommodationControl2 = (AccommodationControl2)foundControl;
								TextBox txt = accommodationControl2.Txt;
								DynamicScreen.SetControlValues(1, controlID, txt, dr, controlDR, tripleDES, false, da, encryptedDataIsInCopiedDataAsPlainText);
								txt.Tag = null;
								DynamicScreen.SetExtraAccommodationData(accommodationControl2, dr, tripleDES);
								break;
							}
							case 702:
							{
								AccommodationControl2 accommodationControl3 = (AccommodationControl2)foundControl;
								DateTimePicker dtp = accommodationControl3.Dtp;
								DynamicScreen.SetControlValues(6, controlID, dtp, dr, controlDR, tripleDES, false, da, encryptedDataIsInCopiedDataAsPlainText);
								dtp.Tag = null;
								DynamicScreen.SetExtraAccommodationData(accommodationControl3, dr, tripleDES);
								break;
							}
							case 703:
							{
								AccommodationControl2 accommodationControl4 = (AccommodationControl2)foundControl;
								ComboBox cmb = accommodationControl4.Cmb;
								DynamicScreen.SetControlValues(3, controlID, cmb, dr, controlDR, tripleDES, false, da, encryptedDataIsInCopiedDataAsPlainText);
								cmb.Tag = null;
								DynamicScreen.SetExtraAccommodationData(accommodationControl4, dr, tripleDES);
								break;
							}
							}
						}
						else
						{
							byte[] bytes8 = (byte[])dr[4];
							bool decrypt5 = !encryptedDataIsInCopiedDataAsPlainText && (int)controlDR[6] != 0;
							string items = DynamicScreen.BytesToString(bytes8, decrypt5, tripleDES);
							MyMultilineTextBoxWithEditingControls myMultilineTextBoxWithEditingControls = (MyMultilineTextBoxWithEditingControls)foundControl;
							myMultilineTextBoxWithEditingControls.SetItems(items);
						}
					}
					else if (num != 802)
					{
						if (num == 806)
						{
							AutoComboBox autoComboBox3 = (AutoComboBox)foundControl;
							int num3 = (int)dr[4];
							int lookupListCMBIndex3 = DynamicScreen.GetLookupListCMBIndex(autoComboBox3, num3);
							int itemCount = autoComboBox3.GetItemCount();
							if (lookupListCMBIndex3 >= 0 && lookupListCMBIndex3 < itemCount)
							{
								autoComboBox3.SelectedIndex = lookupListCMBIndex3;
							}
							else if (num3 > -1)
							{
								da.SelectCommand.CommandText = "SELECT personid,firstname,lastname,student_no FROM people WHERE personid=" + num3.ToString();
								da.SelectCommand.Parameters.Clear();
								DataTable dataTable = new DataTable();
								da.Fill(dataTable);
								if (dataTable.Rows.Count > 0)
								{
									dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
									{
										"firstname",
										"lastname",
										"student_no"
									});
									DataTable dataTable2;
									if (autoComboBox3.DataSource is DataView)
									{
										dataTable2 = ((DataView)autoComboBox3.DataSource).Table;
									}
									else if (autoComboBox3.DataSource is DataTable)
									{
										dataTable2 = (DataTable)autoComboBox3.DataSource;
									}
									else
									{
										dataTable2 = null;
									}
									if (dataTable2 != null)
									{
										DataRow dataRow3 = dataTable.Rows[0];
										DataRow dataRow4 = dataTable2.NewRow();
										dataRow4[0] = num3;
										dataRow4[1] = dataRow3["firstname"];
										dataRow4[2] = dataRow3["lastname"];
										dataRow4[3] = dataRow3["student_no"];
										dataRow4[4] = dataRow3["lastname"].ToString() + ", " + dataRow3["firstname"].ToString();
										dataTable2.Rows.Add(dataRow4);
										lookupListCMBIndex3 = DynamicScreen.GetLookupListCMBIndex(autoComboBox3, num3);
										itemCount = autoComboBox3.GetItemCount();
										if (lookupListCMBIndex3 >= 0 && lookupListCMBIndex3 < itemCount)
										{
											autoComboBox3.SelectedIndex = lookupListCMBIndex3;
										}
										else
										{
											MessageBox.Show(string.Concat(new string[]
											{
												"The case combobox item with lookuplistid ",
												num3.ToString(),
												" doesn't exist [controlID=",
												controlID.ToString(),
												"]!"
											}));
										}
									}
								}
								else
								{
									MessageBox.Show(string.Concat(new string[]
									{
										"The case combobox item with lookuplistid ",
										num3.ToString(),
										" doesn't exist [controlID=",
										controlID.ToString(),
										"]!"
									}));
								}
							}
						}
					}
					else
					{
						string text7 = DynamicScreen.BytesToString((byte[])dr[4], false, tripleDES);
						if (!string.IsNullOrEmpty(text7))
						{
							if (foundControl is AutoComboBox.MyControls.MultiDatabaseItemSelect)
							{
								AutoComboBox.MyControls.MultiDatabaseItemSelect multiDatabaseItemSelect = (AutoComboBox.MyControls.MultiDatabaseItemSelect)foundControl;
								multiDatabaseItemSelect.FromString(text7);
							}
						}
					}
					IL_13EE:;
				}
			}
			catch (Exception ex3)
			{
				MessageBox.Show(string.Concat(new string[]
				{
					"controlcode=",
					controlCode.ToString(),
					"; controlid=",
					controlID.ToString(),
					": ",
					ex3.ToString()
				}));
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0002C4CC File Offset: 0x0002B4CC
		public static byte[] ExtractImageBytes(byte[] dbBytes, out string fileName)
		{
			byte[] result;
			try
			{
				int num = 6;
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = dbBytes[i];
				}
				string s = DynamicScreen.BytesToString(array, false, null);
				int num2 = int.Parse(s);
				byte[] array2 = new byte[num2];
				for (int i = 0; i < num2; i++)
				{
					array2[i] = dbBytes[i + num];
				}
				string args = DynamicScreen.BytesToString(array2, false, null);
				StringDictionary stringDictionary = DynamicScreen.ParseArgs(args, ';');
				fileName = stringDictionary["filename"];
				string text = fileName;
				int num3 = dbBytes.Length - num - num2;
				byte[] array3 = new byte[num3];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = dbBytes[i + num2 + num];
				}
				result = array3;
			}
			catch (Exception ex)
			{
				fileName = "";
				result = new byte[0];
			}
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0002C5C0 File Offset: 0x0002B5C0
		private static void SetExtraAccommodationData(AccommodationControl2 ctrl, DataRow dr, TripleDESEncryptionClass tripleDES)
		{
			ctrl.Chk_caption.Checked = true;
			if (dr.Table.Columns.Contains("offline"))
			{
				ctrl.Offline = (dr["offline"] != DBNull.Value && Convert.ToBoolean(dr["offline"]));
				ctrl.ExpiryDate = ((dr["expirydate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["expirydate"]));
				ctrl.TextForLetter = ((dr["altlongdescription"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dr["altlongdescription"]));
				ctrl.PrivateNote = ((dr["note"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dr["note"]));
				ctrl.ShowOnLetter = (dr["showonletter"] == DBNull.Value || (int)dr["showonletter"] == 1);
				if (dr.Table.Columns.Contains("recommendedbutdeclined"))
				{
					ctrl.RecommendedToStudentButDeclined = (dr["recommendedbutdeclined"] != DBNull.Value && Convert.ToBoolean(dr["recommendedbutdeclined"]));
				}
				if (dr.Table.Columns.Contains("recommendedbutdeclineddetail"))
				{
					if (dr["recommendedbutdeclineddetail"] != DBNull.Value)
					{
						ctrl.RecommendedToStudentButDeclinedDetail = tripleDES.Decrypt((byte[])dr["recommendedbutdeclineddetail"]);
					}
				}
				if (dr.Table.Columns.Contains("approved"))
				{
					ctrl.Approved = (dr["approved"] != DBNull.Value && Convert.ToBoolean(dr["approved"]));
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0002C7DC File Offset: 0x0002B7DC
		public static void SetComboText(AutoComboBox cmb, string plainText)
		{
			if (cmb.DataSource != null)
			{
				DataTable dataTable = (DataTable)cmb.DataSource;
				DataTable dataTable2 = dataTable.Copy();
				int num = dataTable2.Columns.IndexOf(cmb.DisplayMember);
				object[] array = new object[dataTable2.Columns.Count];
				array[num] = plainText;
				dataTable2.Rows.Add(array);
				cmb.DataSource = null;
				cmb.DisplayMember = "";
				cmb.ValueMember = "";
				cmb.DataSource = dataTable2;
				cmb.DisplayMember = dataTable2.Columns[num].ColumnName;
				try
				{
					cmb.SelectedIndex = dataTable2.Rows.Count - 1;
				}
				catch
				{
					cmb.SelectedText = plainText;
				}
			}
			else
			{
				cmb.SelectedText = plainText;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0002C8C8 File Offset: 0x0002B8C8
		private static int GetLookupListCMBIndex(DataTable t, int lookupListID)
		{
			int num = 0;
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted && dataRow[0] != DBNull.Value)
				{
					int num2 = (int)dataRow[0];
					if (num2 == lookupListID)
					{
						return num;
					}
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0002C980 File Offset: 0x0002B980
		private static int GetLookupListCMBIndex(DataView dv, int lookupListID)
		{
			int num = 0;
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				if (row.RowState != DataRowState.Deleted && row[0] != DBNull.Value)
				{
					int num2 = (int)row[0];
					if (num2 == lookupListID)
					{
						return num;
					}
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0002CA3C File Offset: 0x0002BA3C
		private static int GetLookupListCMBIndex(AutoComboBox cmb, int lookupListID)
		{
			int result;
			if (cmb.DataSource is DataTable)
			{
				result = DynamicScreen.GetLookupListCMBIndex((DataTable)cmb.DataSource, lookupListID);
			}
			else if (cmb.DataSource is DataView)
			{
				result = DynamicScreen.GetLookupListCMBIndex((DataView)cmb.DataSource, lookupListID);
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0002CAA4 File Offset: 0x0002BAA4
		public static int GetLookupListID(DataTable lookupListTable, string listValue)
		{
			string strB = listValue.ToLower().Trim();
			foreach (object obj in lookupListTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted && dataRow[0] != DBNull.Value)
				{
					string text = dataRow[2].ToString().Trim().ToLower();
					if (text.CompareTo(strB) == 0)
					{
						return (int)dataRow[0];
					}
				}
			}
			return -1;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0002CB7C File Offset: 0x0002BB7C
		public static string GetLookupListValue(DataTable t, int lookupListID)
		{
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted && dataRow[0] != DBNull.Value)
				{
					int num = (int)dataRow[0];
					if (num == lookupListID)
					{
						return dataRow[2].ToString();
					}
				}
			}
			return "";
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0002CC38 File Offset: 0x0002BC38
		private static Control GetControlMultipleTextBox(Control panel, int controlID, out DataRow dr)
		{
			foreach (object obj in panel.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel || control is TabControl || control is TabPage || control is GroupBox || control is MyTabControl || control is MyTabPage)
				{
					DataRow dataRow;
					Control controlMultipleTextBox = DynamicScreen.GetControlMultipleTextBox(control, controlID, out dataRow);
					if (controlMultipleTextBox != null)
					{
						dr = dataRow;
						return controlMultipleTextBox;
					}
				}
				else if (control.Tag is DataRow && control is MyTextBox)
				{
					MyTextBox myTextBox = (MyTextBox)control;
					if (myTextBox.MultipleCids != null)
					{
						for (int i = 0; i < myTextBox.MultipleCids.Length; i++)
						{
							DataRow dataRow2 = myTextBox.MultipleCids[i];
							int num = (int)dataRow2[0];
							if (num == controlID)
							{
								dr = dataRow2;
								return control;
							}
						}
					}
				}
			}
			dr = null;
			return null;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0002CDB0 File Offset: 0x0002BDB0
		private static Control GetControl(Control panel, int controlID)
		{
			if (panel.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)panel.Tag;
				if (dataRow.Table.Columns.Contains("controlid"))
				{
					int num = (int)dataRow["controlid"];
					if (num == controlID)
					{
						return panel;
					}
				}
			}
			foreach (object obj in panel.Controls)
			{
				Control control = (Control)obj;
				if (control.Controls.Count > 0 && !(control is ListSelect))
				{
					Control control2 = DynamicScreen.GetControl(control, controlID);
					if (control2 != null)
					{
						return control2;
					}
				}
				else if (control.Tag is DataRow && ((DataRow)control.Tag).Table.Columns.Contains("controlid"))
				{
					DataRow dataRow2 = (DataRow)control.Tag;
					int num = (int)dataRow2["controlid"];
					if (num == controlID)
					{
						return control;
					}
				}
				else if (control is ListSelect)
				{
					ListSelect listSelect = (ListSelect)control;
					List<int> cids = listSelect.GetCids();
					if (cids.Contains(controlID))
					{
						return listSelect;
					}
				}
			}
			return null;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0002CF8C File Offset: 0x0002BF8C
		private static DataRow[] GetDataDrs(DataSet data, int controlID, int appointmentID)
		{
			return DynamicScreen.GetDataDrs(data, controlID, appointmentID, false);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0002CFA8 File Offset: 0x0002BFA8
		private static DataRow[] GetDataDrs(DataSet data, int controlID, int appointmentID, bool forceUseAppointmentId)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in data.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (dataRow.RowState != DataRowState.Deleted && dataRow[3] != DBNull.Value)
					{
						int num = (int)dataRow[3];
						bool flag;
						if ((appointmentID >= 0 || forceUseAppointmentId) && dataRow[5] != DBNull.Value)
						{
							int num2 = (int)dataRow[5];
							flag = (num2 == appointmentID);
						}
						else
						{
							flag = true;
						}
						if (num == controlID && flag)
						{
							arrayList.Add(dataRow);
						}
					}
				}
			}
			DataRow[] array = new DataRow[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				array[i] = (DataRow)arrayList[i];
			}
			return array;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0002D144 File Offset: 0x0002C144
		private static DataRow GetDataDR(DataSet data, int controlID, int appointmentID)
		{
			return DynamicScreen.GetDataDR(data, controlID, appointmentID, false);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0002D160 File Offset: 0x0002C160
		private static DataRow GetDataDR(DataSet data, int controlID, int appointmentID, bool forceUseAppointmentId)
		{
			foreach (object obj in data.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (dataRow.RowState != DataRowState.Deleted && dataRow[3] != DBNull.Value)
					{
						int num = (int)dataRow[3];
						bool flag;
						if ((appointmentID >= 0 || forceUseAppointmentId) && dataRow[5] != DBNull.Value)
						{
							int num2 = (int)dataRow[5];
							flag = (num2 == appointmentID);
						}
						else
						{
							flag = true;
						}
						if (num == controlID && flag)
						{
							return dataRow;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0002D2B4 File Offset: 0x0002C2B4
		public static void SaveData(ref DataSet data, Control panel, int screenNum, int personID, string mainInfoTableName, string otherInfoTableName, string dateTimeInfoTableName, TripleDESEncryptionClass tripleDES, int appointmentID)
		{
			DynamicScreen.SaveData(ref data, panel, screenNum, personID, mainInfoTableName, otherInfoTableName, dateTimeInfoTableName, tripleDES, appointmentID, false);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0002D2D8 File Offset: 0x0002C2D8
		public static void SaveData(ref DataSet data, Control panel, int screenNum, int personID, string mainInfoTableName, string otherInfoTableName, string dateTimeInfoTableName, TripleDESEncryptionClass tripleDES, int appointmentID, bool forceUseAppointmentId)
		{
			DynamicScreen.SaveData(ref data, panel, screenNum, personID, mainInfoTableName, otherInfoTableName, dateTimeInfoTableName, tripleDES, appointmentID, forceUseAppointmentId, new Dictionary<string, string>());
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0002D480 File Offset: 0x0002C480
		public static void Paste(Control panel, string copiedData, bool encryptedDataIsInCopiedDataAsPlainText)
		{
			if (!string.IsNullOrEmpty(copiedData))
			{
				try
				{
					XDocument xdocument = XDocument.Parse(copiedData);
					IEnumerable<DynamicDataItemValue> enumerable = Enumerable.Select<XElement, DynamicDataItemValue>(xdocument.Root.Elements("dynamiccontroldataitem"), (XElement r) => new DynamicDataItemValue
					{
						ControlId = ((r.Attribute("cid") == null) ? 0 : ((int)r.Attribute("cid"))),
						ControlCaption = (string)r.Attribute("controlcaption"),
						ValBytes = ((r.Attribute("valbytes") == null) ? null : Convert.FromBase64String((string)r.Attribute("valbytes"))),
						ValInt = ((r.Attribute("valint") == null) ? 0 : ((int)r.Attribute("valint"))),
						ValDateTime = (string.IsNullOrEmpty((string)r.Attribute("valdatetime")) ? null : new DateTime?(DateTime.Parse((string)r.Attribute("valdatetime")))),
						ValType = (eDynamicDataItemValueType)((r.Attribute("valtype") == null) ? 0 : ((int)r.Attribute("valtype"))),
						IsEncryptedData = (r.Attribute("isencrypteddata") != null && (bool)r.Attribute("isencrypteddata"))
					});
					DynamicScreen.ResetScreenToDefaults(panel, false);
					foreach (DynamicDataItemValue dynamicDataItemValue in enumerable)
					{
						try
						{
							Control control = DynamicScreen.FindControl(panel, dynamicDataItemValue.ControlId);
							if (control != null)
							{
								if (control.Tag is DataRow)
								{
									DataRow dataRow = (DataRow)control.Tag;
									DataRow dataRow2;
									switch (dynamicDataItemValue.ValType)
									{
									case eDynamicDataItemValueType.Int:
										dataRow2 = DynamicScreen.templateMainInfoTable.NewRow();
										dataRow2["controlid"] = dynamicDataItemValue.ControlId;
										dataRow2["controlvalue"] = dynamicDataItemValue.ValInt;
										DynamicScreen.templateMainInfoTable.Rows.Clear();
										DynamicScreen.templateMainInfoTable.Rows.Add(dataRow2);
										break;
									case eDynamicDataItemValueType.ByteArray:
										if (dynamicDataItemValue.ValBytes != null)
										{
											dataRow2 = DynamicScreen.templateOtherInfoTable.NewRow();
											dataRow2["controlid"] = dynamicDataItemValue.ControlId;
											dataRow2["controlvalue"] = dynamicDataItemValue.ValBytes;
											DynamicScreen.templateOtherInfoTable.Rows.Clear();
											DynamicScreen.templateOtherInfoTable.Rows.Add(dataRow2);
										}
										else
										{
											dataRow2 = null;
										}
										break;
									case eDynamicDataItemValueType.DateTime:
										if (dynamicDataItemValue.ValDateTime != null)
										{
											dataRow2 = DynamicScreen.templateDateTimeInfoTable.NewRow();
											dataRow2["controlid"] = dynamicDataItemValue.ControlId;
											dataRow2["controlvalue"] = dynamicDataItemValue.ValDateTime.Value;
											DynamicScreen.templateDateTimeInfoTable.Rows.Clear();
											DynamicScreen.templateDateTimeInfoTable.Rows.Add(dataRow2);
										}
										else
										{
											dataRow2 = null;
										}
										break;
									default:
										dataRow2 = null;
										break;
									}
									if (dataRow2 != null)
									{
										DynamicScreen.SetControlValues((int)dataRow[2], dynamicDataItemValue.ControlId, control, dataRow2, dataRow, ClientCache.CurrentInstance.tripleDES, false, ClientCache.CurrentInstance.da, new Dictionary<string, string>(), encryptedDataIsInCopiedDataAsPlainText);
									}
								}
							}
						}
						catch (Exception ex)
						{
						}
					}
				}
				catch (Exception ex2)
				{
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0002D7A0 File Offset: 0x0002C7A0
		public static IList<DynamicDataItemValue> CopyDataList(Control panel, bool returnEncryptedDataAsPlainText, bool returnEmptyFieldsToo = false)
		{
			List<DynamicDataItemValue> result = new List<DynamicDataItemValue>();
			DynamicScreen.CopyData(panel.Controls, ref result, returnEmptyFieldsToo, returnEncryptedDataAsPlainText);
			return result;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0002D7CC File Offset: 0x0002C7CC
		public static string CopyData(Control panel, bool returnEmptyFieldsToo = false)
		{
			return DynamicScreen.CopyData(panel, false, returnEmptyFieldsToo);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0002D90C File Offset: 0x0002C90C
		public static string CopyData(Control panel, bool returnEncryptedDataAsPlainText, bool returnEmptyFieldsToo)
		{
			try
			{
				IList<DynamicDataItemValue> list = DynamicScreen.CopyDataList(panel, returnEncryptedDataAsPlainText, false);
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement("dynamiccontroldata", Enumerable.Select<DynamicDataItemValue, XElement>(list, (DynamicDataItemValue cv) => new XElement("dynamiccontroldataitem", new object[]
				{
					new XAttribute("cid", cv.ControlId),
					new XAttribute("controlcaption", cv.ControlCaption),
					new XAttribute("valint", cv.ValInt),
					new XAttribute("valdatetime", (cv.ValDateTime != null) ? cv.ValDateTime.Value.ToString("yyyy-MM-dd H:mm") : ""),
					new XAttribute("valbytes", (cv.ValBytes == null) ? "" : Convert.ToBase64String(cv.ValBytes)),
					new XAttribute("valtype", (int)cv.ValType),
					new XAttribute("isencrypteddata", cv.IsEncryptedData)
				})));
				XDocument xdocument = new XDocument(declaration, array);
				return xdocument.Declaration.ToString() + xdocument.ToString();
			}
			catch (Exception ex)
			{
			}
			return "";
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0002D9F4 File Offset: 0x0002C9F4
		private static void CopyData(Control.ControlCollection controls, ref List<DynamicDataItemValue> collectedValues, bool returnEmptyFieldsToo, bool returnEncryptedDataAsPlainText = false)
		{
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				try
				{
					if (control is Panel || control is TabControl || control is TabPage || control is GroupBox || control is MyTabPage || control is MyTabControl)
					{
						DynamicScreen.CopyData(control.Controls, ref collectedValues, returnEmptyFieldsToo, returnEncryptedDataAsPlainText);
					}
					else if (control is ListSelect)
					{
						DynamicScreen.CopyData(control.Controls, ref collectedValues, returnEmptyFieldsToo, returnEncryptedDataAsPlainText);
					}
					else if (control.Tag is DataRow)
					{
						DataRow dataRow = (DataRow)control.Tag;
						DynamicControl dynamicControl = new DynamicControl(dataRow);
						bool readOnly = dynamicControl.ReadOnly;
						bool flag = 1 == 0;
						int num;
						int controlId;
						try
						{
							num = (int)dataRow[2];
							controlId = (int)dataRow[0];
						}
						catch (Exception ex)
						{
							throw new Exception("controlid/controlcode: " + ex.Message, ex.InnerException);
						}
						bool flag2 = true;
						try
						{
							if (num > 0 && Enum.IsDefined(typeof(eControlCode), num))
							{
								eControlCode controlCode = (eControlCode)num;
								DynamicControlAttribute controlCodeAttribute = controlCode.GetControlCodeAttribute();
								if (controlCodeAttribute != null && !controlCodeAttribute.IsDataHolding)
								{
									flag2 = false;
								}
							}
						}
						catch
						{
						}
						if (flag2)
						{
							DynamicDataItemValue item2 = DynamicScreen.GetControlValue(control, controlId, num, dataRow[3].ToString().Trim(), dataRow, returnEmptyFieldsToo, returnEncryptedDataAsPlainText);
							if (item2 != null)
							{
								DynamicDataItemValue dynamicDataItemValue = Enumerable.FirstOrDefault<DynamicDataItemValue>(collectedValues, (DynamicDataItemValue g) => g != null && g.ControlCaption == item2.ControlCaption);
								if (dynamicDataItemValue != null)
								{
									collectedValues.Remove(dynamicDataItemValue);
								}
								collectedValues.Add(item2);
							}
						}
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("DynamicScreens.DynamicScreen.CopyData:Error={0}", ex.ToString());
				}
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0002DCC0 File Offset: 0x0002CCC0
		public static void SaveData(ref DataSet data, Control panel, int screenNum, int personID, string mainInfoTableName, string otherInfoTableName, string dateTimeInfoTableName, TripleDESEncryptionClass tripleDES, int appointmentID, bool forceUseAppointmentId, Dictionary<string, string> overrideDefaultControlValues)
		{
			if (data != null)
			{
				int numColumns;
				if (appointmentID >= 0 || forceUseAppointmentId)
				{
					numColumns = 6;
				}
				else
				{
					numColumns = 5;
				}
				DataTable dataTable = data.Tables["mainInfoTable"];
				DataTable otherInfoTable = data.Tables["otherInfoTable"];
				DataTable dateTimeInfoTable = data.Tables["dateTimeInfoTable"];
				DataTable imageInfoTable = data.Tables.Contains("imageInfoTable") ? data.Tables["imageInfoTable"] : null;
				bool flag = dataTable.Columns.Contains("courseid");
				if (flag)
				{
					numColumns = 7;
				}
				foreach (object obj in panel.Controls)
				{
					Control control = (Control)obj;
					if (control is Panel || control is TabControl || control is TabPage || control is GroupBox || control is MyTabPage || control is MyTabControl)
					{
						DynamicScreen.SaveData(ref data, control, screenNum, personID, mainInfoTableName, otherInfoTableName, dateTimeInfoTableName, tripleDES, appointmentID, forceUseAppointmentId, overrideDefaultControlValues);
					}
					else if (control is ListSelect)
					{
						ListSelect listSelect = (ListSelect)control;
						List<int> cids = listSelect.GetCids();
						foreach (int controlID in cids)
						{
							DataRow dataDR = DynamicScreen.GetDataDR(data, controlID, appointmentID, forceUseAppointmentId);
							DynamicScreen.SaveData(listSelect, controlID, 301, data, numColumns, screenNum, personID, appointmentID, forceUseAppointmentId, null, dataDR, dataTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, tripleDES);
						}
					}
					else if (control.Tag is DataRow)
					{
						DataRow dataRow = (DataRow)control.Tag;
						DynamicControl dynamicControl = new DynamicControl(dataRow);
						if (!overrideDefaultControlValues.ContainsKey(dynamicControl.ControlCaption.ToLower()))
						{
							bool readOnly = dynamicControl.ReadOnly;
							bool flag2 = 1 == 0;
							int controlCode;
							int controlID2;
							try
							{
								controlCode = (int)dataRow[2];
								controlID2 = (int)dataRow[0];
							}
							catch (Exception ex)
							{
								throw new Exception("controlid/controlcode: " + ex.Message, ex.InnerException);
							}
							DataRow dataDR = DynamicScreen.GetDataDR(data, controlID2, appointmentID, forceUseAppointmentId);
							DynamicScreen.SaveData(control, controlID2, controlCode, data, numColumns, screenNum, personID, appointmentID, forceUseAppointmentId, dataRow, dataDR, dataTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, tripleDES);
						}
					}
				}
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0002DFE8 File Offset: 0x0002CFE8
		public static void SetOverrideControlValues(Control parent, Dictionary<string, string> overrideDefaultControlValues)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is Panel || control is TabControl || control is TabPage || control is GroupBox || control is MyTabPage || control is MyTabControl)
				{
					DynamicScreen.SetOverrideControlValues(control, overrideDefaultControlValues);
				}
				else if (control.Tag is DataRow)
				{
					DataRow dr = (DataRow)control.Tag;
					DynamicControl dynamicControl = new DynamicControl(dr);
					if (overrideDefaultControlValues.ContainsKey(dynamicControl.ControlCaption.ToLower()))
					{
						string text = overrideDefaultControlValues[dynamicControl.ControlCaption.ToLower()];
						if (control is TextBox)
						{
							((TextBox)control).Text = text;
						}
						else if (control is AutoComboBox)
						{
							((AutoComboBox)control).Text = text;
						}
					}
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0002E154 File Offset: 0x0002D154
		private static DataTable templateMainInfoTable
		{
			get
			{
				if (DynamicScreen._templateMainInfoTable == null)
				{
					DynamicScreen._templateMainInfoTable = new DataTable("maininfotable");
					DynamicScreen._templateMainInfoTable.Columns.Add("dataid", typeof(int));
					DynamicScreen._templateMainInfoTable.Columns.Add("screennum", typeof(int));
					DynamicScreen._templateMainInfoTable.Columns.Add("personid", typeof(int));
					DynamicScreen._templateMainInfoTable.Columns.Add("controlid", typeof(int));
					DynamicScreen._templateMainInfoTable.Columns.Add("controlvalue", typeof(int));
				}
				return DynamicScreen._templateMainInfoTable;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0002E22C File Offset: 0x0002D22C
		private static DataTable templateOtherInfoTable
		{
			get
			{
				if (DynamicScreen._templateOtherInfoTable == null)
				{
					DynamicScreen._templateOtherInfoTable = new DataTable("maininfotable");
					DynamicScreen._templateOtherInfoTable.Columns.Add("dataid", typeof(int));
					DynamicScreen._templateOtherInfoTable.Columns.Add("screennum", typeof(int));
					DynamicScreen._templateOtherInfoTable.Columns.Add("personid", typeof(int));
					DynamicScreen._templateOtherInfoTable.Columns.Add("controlid", typeof(int));
					DynamicScreen._templateOtherInfoTable.Columns.Add("controlvalue", typeof(byte[]));
				}
				return DynamicScreen._templateOtherInfoTable;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0002E304 File Offset: 0x0002D304
		private static DataTable templateDateTimeInfoTable
		{
			get
			{
				if (DynamicScreen._templateDateTimeInfoTable == null)
				{
					DynamicScreen._templateDateTimeInfoTable = new DataTable("maininfotable");
					DynamicScreen._templateDateTimeInfoTable.Columns.Add("dataid", typeof(int));
					DynamicScreen._templateDateTimeInfoTable.Columns.Add("screennum", typeof(int));
					DynamicScreen._templateDateTimeInfoTable.Columns.Add("personid", typeof(int));
					DynamicScreen._templateDateTimeInfoTable.Columns.Add("controlid", typeof(int));
					DynamicScreen._templateDateTimeInfoTable.Columns.Add("controlvalue", typeof(DateTime));
				}
				return DynamicScreen._templateDateTimeInfoTable;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0002E3DC File Offset: 0x0002D3DC
		private static DataTable templateImageInfoTable
		{
			get
			{
				if (DynamicScreen._templateImageInfoTable == null)
				{
					DynamicScreen._templateImageInfoTable = new DataTable("maininfotable");
					DynamicScreen._templateImageInfoTable.Columns.Add("dataid", typeof(int));
					DynamicScreen._templateImageInfoTable.Columns.Add("screennum", typeof(int));
					DynamicScreen._templateImageInfoTable.Columns.Add("personid", typeof(int));
					DynamicScreen._templateImageInfoTable.Columns.Add("controlid", typeof(int));
					DynamicScreen._templateImageInfoTable.Columns.Add("controlvalue", typeof(byte[]));
				}
				return DynamicScreen._templateImageInfoTable;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0002E4B4 File Offset: 0x0002D4B4
		private static DynamicDataItemValue GetControlValue(Control c, int controlId, int controlCode, string controlCaption, DataRow controlDr, bool returnEmptyFieldsToo, bool returnEncryptedDataAsPlainText = false)
		{
			DynamicScreen.templateMainInfoTable.Rows.Clear();
			DynamicScreen.templateOtherInfoTable.Rows.Clear();
			DynamicScreen.templateDateTimeInfoTable.Rows.Clear();
			DynamicScreen.templateImageInfoTable.Rows.Clear();
			DataSet data = new DataSet();
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			bool isEncryptedData;
			DynamicScreen.SaveData(c, controlId, controlCode, data, 0, 1, 1, -1, false, controlDr, null, DynamicScreen.templateMainInfoTable, DynamicScreen.templateOtherInfoTable, DynamicScreen.templateDateTimeInfoTable, DynamicScreen.templateImageInfoTable, tripleDES, out isEncryptedData, returnEncryptedDataAsPlainText);
			DynamicDataItemValue dynamicDataItemValue = new DynamicDataItemValue
			{
				ControlId = controlId,
				ControlCaption = controlCaption,
				IsEncryptedData = isEncryptedData
			};
			if (DynamicScreen.templateMainInfoTable.Rows.Count > 0)
			{
				dynamicDataItemValue.ValInt = ((DynamicScreen.templateMainInfoTable.Rows[0]["controlvalue"] is DBNull) ? 0 : ((int)DynamicScreen.templateMainInfoTable.Rows[0]["controlvalue"]));
				dynamicDataItemValue.ValType = eDynamicDataItemValueType.Int;
			}
			else if (DynamicScreen.templateOtherInfoTable.Rows.Count > 0)
			{
				dynamicDataItemValue.ValBytes = ((DynamicScreen.templateOtherInfoTable.Rows[0]["controlvalue"] is DBNull) ? null : ((byte[])DynamicScreen.templateOtherInfoTable.Rows[0]["controlvalue"]));
				dynamicDataItemValue.ValType = eDynamicDataItemValueType.ByteArray;
			}
			else if (DynamicScreen.templateDateTimeInfoTable.Rows.Count > 0)
			{
				dynamicDataItemValue.ValDateTime = ((DynamicScreen.templateDateTimeInfoTable.Rows[0]["controlvalue"] is DBNull) ? null : new DateTime?((DateTime)DynamicScreen.templateDateTimeInfoTable.Rows[0]["controlvalue"]));
				dynamicDataItemValue.ValType = eDynamicDataItemValueType.DateTime;
			}
			else if (DynamicScreen.templateImageInfoTable.Rows.Count > 0)
			{
				dynamicDataItemValue.ValBytes = ((DynamicScreen.templateImageInfoTable.Rows[0]["controlvalue"] is DBNull) ? null : ((byte[])DynamicScreen.templateImageInfoTable.Rows[0]["controlvalue"]));
				dynamicDataItemValue.ValType = eDynamicDataItemValueType.ByteArray;
			}
			else
			{
				if (!returnEmptyFieldsToo)
				{
					return null;
				}
				dynamicDataItemValue.ValType = eDynamicDataItemValueType.EmptyField;
			}
			return dynamicDataItemValue;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0002E764 File Offset: 0x0002D764
		private static void SaveData(Control c, int controlID, int controlCode, DataSet data, int numColumns, int screenNum, int personID, int appointmentID, bool forceUseAppointmentId, DataRow dr, DataRow dataDR, DataTable mainInfoTable, DataTable otherInfoTable, DataTable dateTimeInfoTable, DataTable imageInfoTable, TripleDESEncryptionClass tripleDES)
		{
			bool flag;
			DynamicScreen.SaveData(c, controlID, controlCode, data, numColumns, screenNum, personID, appointmentID, forceUseAppointmentId, dr, dataDR, mainInfoTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, tripleDES, out flag);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0002E798 File Offset: 0x0002D798
		private static void SaveData(Control c, int controlID, int controlCode, DataSet data, int numColumns, int screenNum, int personID, int appointmentID, bool forceUseAppointmentId, DataRow dr, DataRow dataDR, DataTable mainInfoTable, DataTable otherInfoTable, DataTable dateTimeInfoTable, DataTable imageInfoTable, TripleDESEncryptionClass tripleDES, out bool isEncryptedData)
		{
			DynamicScreen.SaveData(c, controlID, controlCode, data, numColumns, screenNum, personID, appointmentID, forceUseAppointmentId, dr, dataDR, mainInfoTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, tripleDES, out isEncryptedData, false);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0002E7CC File Offset: 0x0002D7CC
		private static void SaveData(Control c, int controlID, int controlCode, DataSet data, int numColumns, int screenNum, int personID, int appointmentID, bool forceUseAppointmentId, DataRow dr, DataRow dataDR, DataTable mainInfoTable, DataTable otherInfoTable, DataTable dateTimeInfoTable, DataTable imageInfoTable, TripleDESEncryptionClass tripleDES, out bool isEncryptedData, bool saveEncryptedDataAsPlainText)
		{
			object[] array = new object[mainInfoTable.Columns.Count];
			if (array.Length > 6)
			{
				array[6] = -1;
			}
			isEncryptedData = false;
			int num = mainInfoTable.Columns.IndexOf("showonletter");
			if (num >= 0)
			{
				array[num] = true;
			}
			AutoComboBox autoComboBox;
			int num9;
			if (controlCode <= 500)
			{
				if (controlCode <= 21)
				{
					switch (controlCode)
					{
					case 1:
					{
						int num2 = (int)dr[6];
						bool isEncrypted = num2 != 0;
						if (saveEncryptedDataAsPlainText)
						{
							isEncrypted = false;
						}
						if (c is MyTextBox || c is MaskedTextBox)
						{
							MyTextBox myTextBox = (c is MyTextBox) ? ((MyTextBox)c) : null;
							MaskedTextBox maskedTextBox = (myTextBox == null) ? ((MaskedTextBox)c) : null;
							bool flag = myTextBox != null && myTextBox.MultipleCids != null && myTextBox.MultipleCids.Length > 0;
							string text = (myTextBox != null) ? myTextBox.Text.Trim() : maskedTextBox.Text.Trim();
							if (flag)
							{
								string[] array2 = DynamicScreen.SplitUpString(text, myTextBox.MultipleCids.Length + 1);
								DynamicScreen.SaveTextBox(array2[0], isEncrypted, tripleDES, ref dataDR, screenNum, personID, controlID, otherInfoTable, appointmentID, ref array, forceUseAppointmentId);
								for (int i = 0; i < myTextBox.MultipleCids.Length; i++)
								{
									DataRow dataRow = myTextBox.MultipleCids[i];
									int controlID2 = (int)dataRow[0];
									dataDR = DynamicScreen.GetDataDR(data, controlID2, appointmentID, forceUseAppointmentId);
									DynamicScreen.SaveTextBox(array2[i + 1], isEncrypted, tripleDES, ref dataDR, screenNum, personID, controlID2, otherInfoTable, appointmentID, ref array, forceUseAppointmentId);
								}
							}
							else
							{
								DynamicScreen.SaveTextBox(text, isEncrypted, tripleDES, ref dataDR, screenNum, personID, controlID, otherInfoTable, appointmentID, ref array, forceUseAppointmentId);
							}
						}
						else
						{
							TextBox textBox = (TextBox)c;
							DynamicScreen.SaveTextBox(textBox.Text.Trim(), isEncrypted, tripleDES, ref dataDR, screenNum, personID, controlID, otherInfoTable, appointmentID, ref array, forceUseAppointmentId);
						}
						return;
					}
					case 2:
						break;
					case 3:
					{
						autoComboBox = (AutoComboBox)c;
						int num3 = (int)dr[6];
						bool isEncrypted2 = num3 < 0;
						if (saveEncryptedDataAsPlainText)
						{
							isEncrypted2 = false;
						}
						int num4 = -1;
						string text2 = autoComboBox.Text.Trim();
						if (num3 == 0 || num3 == 2)
						{
							if (text2.Length > 0)
							{
								int num5 = autoComboBox.SelectedIndexByText();
								if (num5 < 0)
								{
									num4 = -1;
								}
								else
								{
									DataTable dataTable = (DataTable)autoComboBox.DataSource;
									if (dataTable.Rows.Count > num5 && dataTable.Rows[num5][0] != DBNull.Value)
									{
										num4 = (int)dataTable.Rows[num5][0];
									}
								}
							}
							if (num4 < 0 && dataDR != null)
							{
								dataDR.Delete();
							}
							else if (num4 >= 0)
							{
								if (dataDR == null)
								{
									array[1] = screenNum;
									array[2] = personID;
									array[3] = controlID;
									array[4] = num4;
									if (appointmentID >= 0 || forceUseAppointmentId)
									{
										array[5] = appointmentID;
									}
									dataDR = mainInfoTable.Rows.Add(array);
								}
								else
								{
									int num6 = (dataDR[4] == DBNull.Value) ? (num4 + 1) : ((int)dataDR[4]);
									if (num6 != num4)
									{
										dataDR[4] = num4;
									}
								}
							}
						}
						else
						{
							DynamicScreen.SaveTextBox(text2, isEncrypted2, tripleDES, ref dataDR, screenNum, personID, controlID, otherInfoTable, appointmentID, ref array, forceUseAppointmentId);
						}
						return;
					}
					case 4:
					{
						RadioButton radioButton = (RadioButton)c;
						if (radioButton.Checked)
						{
							if (dataDR == null)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = DynamicScreen.BoolToInt(true);
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								mainInfoTable.Rows.Add(array);
							}
						}
						else if (dataDR != null)
						{
							dataDR.Delete();
						}
						return;
					}
					case 5:
					case 7:
					case 8:
					case 9:
						return;
					case 6:
						if (!(c is TextBox))
						{
							DateTime dateTime;
							if (c is MyDateTimePicker)
							{
								MyDateTimePicker myDateTimePicker = (MyDateTimePicker)c;
								dateTime = myDateTimePicker.Value;
							}
							else if (c is CtrlDateTimePicker)
							{
								CtrlDateTimePicker ctrlDateTimePicker = (CtrlDateTimePicker)c;
								DateTime? value = ctrlDateTimePicker.Value;
								dateTime = ((value != null) ? value.Value : DateTime.MinValue);
							}
							else
							{
								MyDateTimePickerForAccommodationsExpiry myDateTimePickerForAccommodationsExpiry = (MyDateTimePickerForAccommodationsExpiry)c;
								dateTime = myDateTimePickerForAccommodationsExpiry.Value;
							}
							if (dateTime == DateTime.MinValue && dataDR != null)
							{
								dataDR.Delete();
							}
							else if (dateTime != DateTime.MinValue)
							{
								if (dataDR == null)
								{
									array[1] = screenNum;
									array[2] = personID;
									array[3] = controlID;
									array[4] = dateTime;
									if (appointmentID >= 0 || forceUseAppointmentId)
									{
										array[5] = appointmentID;
									}
									dataDR = dateTimeInfoTable.Rows.Add(array);
								}
								else
								{
									DateTime value2 = (DateTime)dataDR[4];
									if (dateTime.CompareTo(value2) != 0)
									{
										dataDR[4] = dateTime;
									}
								}
							}
						}
						return;
					case 10:
					{
						ListViewEx listViewEx = (ListViewEx)c;
						bool flag2 = true;
						string text = "";
						foreach (object obj in listViewEx.Items)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							int num7 = 0;
							bool flag3 = true;
							string text3 = "";
							for (int i = 0; i < listViewEx.Columns.Count; i++)
							{
								string text4 = listViewItem.SubItems[i].Text.Trim();
								if (num7++ > 0)
								{
									text3 += '\0';
								}
								text3 += text4;
								if (text4.Length > 0)
								{
									flag3 = false;
								}
							}
							if (!flag3)
							{
								if (flag2)
								{
									flag2 = false;
								}
								else
								{
									text += '\t';
								}
								text += text3;
							}
						}
						if (text.Length > 0)
						{
							UTF8Encoding utf8Encoding = new UTF8Encoding();
							byte[] bytes = utf8Encoding.GetBytes(text);
							if (dataDR == null)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = bytes;
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = otherInfoTable.Rows.Add(array);
							}
							else
							{
								byte[] bytes2 = (byte[])dataDR[4];
								UTF8Encoding utf8Encoding2 = new UTF8Encoding();
								string text5 = utf8Encoding2.GetString(bytes2).Trim();
								if (text5.CompareTo(text) != 0)
								{
									dataDR[4] = bytes;
								}
							}
						}
						else if (dataDR != null)
						{
							dataDR.Delete();
						}
						return;
					}
					default:
						if (controlCode == 14)
						{
							if (c is MyRadioGroupPrimary)
							{
								Control parent = c.Parent;
								int num8 = 0;
								foreach (object obj2 in parent.Controls)
								{
									Control control = (Control)obj2;
									if (control is MyRadioGroupPrimaryCheckboxMultiple)
									{
										MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
										if (myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked)
										{
											DataRow dataRow2 = (DataRow)myRadioGroupPrimaryCheckboxMultiple.Tag;
											num8 = (int)dataRow2[0];
											break;
										}
									}
								}
								num9 = num8;
							}
							else
							{
								num9 = ((MyRadioGroup)c).SelectedId;
							}
							if (num9 <= 0)
							{
								if (dataDR != null)
								{
									dataDR.Delete();
								}
							}
							else if (dataDR == null)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = num9;
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = mainInfoTable.Rows.Add(array);
							}
							else
							{
								int num10 = (dataDR[4] == DBNull.Value) ? (num9 + 1) : ((int)dataDR[4]);
								if (num9 != num10)
								{
									dataDR[4] = num9;
								}
							}
							return;
						}
						switch (controlCode)
						{
						case 20:
						{
							CtrlFileList ctrlFileList = (CtrlFileList)c;
							string text = ctrlFileList.DynamicValue;
							if (text.Length > 0)
							{
								UTF8Encoding utf8Encoding = new UTF8Encoding();
								byte[] bytes = utf8Encoding.GetBytes(text);
								if (dataDR == null)
								{
									array[1] = screenNum;
									array[2] = personID;
									array[3] = controlID;
									array[4] = bytes;
									if (appointmentID >= 0 || forceUseAppointmentId)
									{
										array[5] = appointmentID;
									}
									dataDR = otherInfoTable.Rows.Add(array);
								}
								else
								{
									byte[] bytes2 = (byte[])dataDR[4];
									UTF8Encoding utf8Encoding2 = new UTF8Encoding();
									string text5 = utf8Encoding2.GetString(bytes2).Trim();
									if (text5.CompareTo(text) != 0)
									{
										dataDR[4] = bytes;
									}
								}
							}
							else if (dataDR != null)
							{
								dataDR.Delete();
							}
							return;
						}
						case 21:
							if (imageInfoTable != null)
							{
								if (c is CtrlPicture)
								{
									CtrlPicture ctrlPicture = (CtrlPicture)c;
									if (ctrlPicture.userChangedPicture)
									{
										string text6 = (ctrlPicture.DynamicData == null) ? null : ((string)ctrlPicture.DynamicData.Value);
										byte[] array3 = string.IsNullOrEmpty(text6) ? null : Convert.FromBase64String(text6);
										if (dataDR == null)
										{
											if (array3 != null && array3.Length > 0)
											{
												array[1] = screenNum;
												array[2] = personID;
												array[3] = controlID;
												array[4] = array3;
												if (appointmentID >= 0 || forceUseAppointmentId)
												{
													array[5] = appointmentID;
												}
												dataDR = imageInfoTable.Rows.Add(array);
											}
										}
										else if (array3 == null || array3.Length < 1)
										{
											dataDR.Delete();
										}
										else
										{
											byte[] array4 = (byte[])dataDR["controlvalue"];
											bool flag4 = array4.Length == array3.Length;
											if (flag4)
											{
												int i = 0;
												if (i < array3.Length)
												{
													if (array3[i] != array4[i])
													{
														flag4 = false;
													}
												}
											}
											if (!flag4)
											{
												dataDR[4] = array3;
											}
										}
									}
								}
							}
							else
							{
								MessageBox.Show("You need to patch your database to support the new rich text box control - the text was not saved.");
							}
							return;
						default:
							return;
						}
						break;
					}
				}
				else if (controlCode <= 301)
				{
					if (controlCode == 100)
					{
						goto IL_C11;
					}
					switch (controlCode)
					{
					case 300:
					{
						MyMaskedTextBox myMaskedTextBox = (MyMaskedTextBox)c;
						int num11 = (int)dr[6];
						bool isEncrypted3 = num11 != 0;
						if (saveEncryptedDataAsPlainText)
						{
							isEncrypted3 = false;
						}
						string controlValue = myMaskedTextBox.Text.Trim();
						DynamicScreen.SaveTextBox(controlValue, isEncrypted3, tripleDES, ref dataDR, screenNum, personID, controlID, otherInfoTable, appointmentID, ref array, forceUseAppointmentId);
						return;
					}
					case 301:
						break;
					default:
						return;
					}
				}
				else
				{
					if (controlCode == 400)
					{
						if (imageInfoTable != null)
						{
							if (c is MyFile)
							{
								MyFile myFile = (MyFile)c;
								byte[] array3;
								if (myFile.Filename.Trim().Length > 0)
								{
									array3 = DynamicScreen.GetFileBytes(myFile.Filename);
								}
								else
								{
									array3 = null;
								}
								if (dataDR == null)
								{
									if (array3 != null && array3.Length > 0)
									{
										array[1] = screenNum;
										array[2] = personID;
										array[3] = controlID;
										array[4] = array3;
										if (appointmentID >= 0 || forceUseAppointmentId)
										{
											array[5] = appointmentID;
										}
										dataDR = imageInfoTable.Rows.Add(array);
									}
								}
								else if (array3 == null || array3.Length < 1)
								{
									dataDR.Delete();
								}
								else
								{
									byte[] array4 = (byte[])dataDR["controlvalue"];
									bool flag4 = array4.Length == array3.Length;
									if (flag4)
									{
										int i = 0;
										if (i < array3.Length)
										{
											if (array3[i] != array4[i])
											{
												flag4 = false;
											}
										}
									}
									if (!flag4)
									{
										dataDR[4] = array3;
									}
								}
							}
						}
						else
						{
							MessageBox.Show("You need to patch your database to support the new rich text box control - the text was not saved.");
						}
						return;
					}
					if (controlCode != 500)
					{
						return;
					}
					if (c is MyMultiCheckbox)
					{
						MyMultiCheckbox myMultiCheckbox = (MyMultiCheckbox)c;
						int checkedIntVal = myMultiCheckbox.CheckedIntVal;
						if (dataDR == null)
						{
							if (checkedIntVal > 0)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = checkedIntVal;
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = mainInfoTable.Rows.Add(array);
							}
						}
						else if (checkedIntVal == 0)
						{
							dataDR.Delete();
						}
						else
						{
							int num12 = (int)dataDR[4];
							if (num12 != checkedIntVal)
							{
								dataDR[4] = checkedIntVal;
							}
						}
					}
					return;
				}
				bool flag5;
				if (c is CheckBox)
				{
					CheckBox checkBox = (CheckBox)c;
					flag5 = checkBox.Checked;
				}
				else if (c is ListSelect)
				{
					ListSelect listSelect = (ListSelect)c;
					flag5 = listSelect.IsChecked(controlID);
				}
				else
				{
					MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple2 = (MyRadioGroupPrimaryCheckboxMultiple)c;
					flag5 = myRadioGroupPrimaryCheckboxMultiple2.Checked;
				}
				if (flag5)
				{
					if (dataDR == null)
					{
						array[1] = screenNum;
						array[2] = personID;
						array[3] = controlID;
						array[4] = DynamicScreen.BoolToInt(true);
						if (appointmentID >= 0 || forceUseAppointmentId)
						{
							array[5] = appointmentID;
						}
						dataDR = mainInfoTable.Rows.Add(array);
					}
				}
				else if (dataDR != null)
				{
					dataDR.Delete();
				}
				return;
			}
			if (controlCode <= 600)
			{
				if (controlCode == 510)
				{
					if (c is MyMultiCheckbox)
					{
						MyMultiCheckbox myMultiCheckbox2 = (MyMultiCheckbox)c;
						DataRow[] dataDrs = DynamicScreen.GetDataDrs(data, controlID, appointmentID, forceUseAppointmentId);
						DataRow dataRow3 = null;
						DataRow dataRow4 = null;
						foreach (DataRow dataRow5 in dataDrs)
						{
							if (dataRow5.Table.Columns["controlvalue"].DataType == typeof(int))
							{
								dataRow3 = dataRow5;
							}
							else
							{
								dataRow4 = dataRow5;
							}
						}
						int checkedIntVal2 = myMultiCheckbox2.CheckedIntVal;
						if (dataRow3 == null)
						{
							if (checkedIntVal2 > 0)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = checkedIntVal2;
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = mainInfoTable.Rows.Add(array);
							}
						}
						else
						{
							int num13 = (int)dataRow3[4];
							if (num13 == 0)
							{
								dataRow3.Delete();
							}
							else if (num13 != checkedIntVal2)
							{
								dataRow3[4] = checkedIntVal2;
							}
						}
						string text7 = myMultiCheckbox2.GetText().Trim();
						int num14 = (int)dr[6];
						bool flag6 = num14 != 0;
						if (saveEncryptedDataAsPlainText)
						{
							flag6 = false;
						}
						if (dataRow4 == null)
						{
							string text8 = text7;
							if (text8.Length > 0)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = DynamicScreen.StringToBytes(text7, flag6, tripleDES);
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = otherInfoTable.Rows.Add(array);
								isEncryptedData = flag6;
							}
						}
						else if (text7.Length < 1)
						{
							dataRow4.Delete();
						}
						else
						{
							byte[] bytes3 = (byte[])dataRow4[4];
							string text9 = DynamicScreen.BytesToString(bytes3, flag6, tripleDES);
							if (text9.CompareTo(text7) != 0)
							{
								dataRow4[4] = DynamicScreen.StringToBytes(text7, flag6, tripleDES);
								isEncryptedData = flag6;
							}
						}
					}
					return;
				}
				if (controlCode == 520)
				{
					if (c is MyMultiCheckbox)
					{
						MyMultiCheckbox myMultiCheckbox2 = (MyMultiCheckbox)c;
						DataRow[] dataDrs = DynamicScreen.GetDataDrs(data, controlID, appointmentID, forceUseAppointmentId);
						DataRow dataRow3 = null;
						DataRow dataRow4 = null;
						foreach (DataRow dataRow5 in dataDrs)
						{
							if (dataRow5.Table.Columns["controlvalue"].DataType == typeof(int))
							{
								dataRow3 = dataRow5;
							}
							else
							{
								dataRow4 = dataRow5;
							}
						}
						int num15 = (int)dr[6];
						bool flag7 = num15 < 0;
						if (saveEncryptedDataAsPlainText)
						{
							isEncryptedData = false;
						}
						autoComboBox = myMultiCheckbox2.GetComboBox();
						if (num15 == 0 || num15 == 2)
						{
							int checkedIntVal2 = myMultiCheckbox2.CheckedIntVal;
							int num16 = -1;
							string text10 = autoComboBox.Text.Trim();
							if (text10.Length > 0)
							{
								int num5 = autoComboBox.SelectedIndexByText();
								if (num5 < 0)
								{
									num16 = -1;
								}
								else
								{
									DataTable dataTable = (DataTable)autoComboBox.DataSource;
									if (dataTable.Rows.Count > num5 && dataTable.Rows[num5][0] != DBNull.Value)
									{
										num16 = (int)dataTable.Rows[num5][0];
									}
								}
							}
							num16 = ((num16 >= 0) ? (num16 + 1 << myMultiCheckbox2.NumCheckboxes) : 0);
							num16 += myMultiCheckbox2.CheckedIntVal;
							if (num16 <= 0 && dataRow3 != null)
							{
								dataRow3.Delete();
							}
							else if (num16 > 0)
							{
								if (dataRow3 == null)
								{
									array[1] = screenNum;
									array[2] = personID;
									array[3] = controlID;
									array[4] = num16;
									if (appointmentID >= 0 || forceUseAppointmentId)
									{
										array[5] = appointmentID;
									}
									dataRow3 = mainInfoTable.Rows.Add(array);
								}
								else
								{
									int num6 = (dataRow3[4] == DBNull.Value) ? (num16 + 1) : ((int)dataRow3[4]);
									if (num6 != num16)
									{
										dataRow3[4] = num16;
									}
								}
							}
						}
						else
						{
							string text7 = autoComboBox.Text.Trim();
							int num14 = (int)dr[6];
							bool flag6 = num14 != 0;
							if (saveEncryptedDataAsPlainText)
							{
								flag6 = false;
							}
							if (dataRow4 == null)
							{
								string text8 = text7;
								if (text8.Length > 0)
								{
									array[1] = screenNum;
									array[2] = personID;
									array[3] = controlID;
									array[4] = DynamicScreen.StringToBytes(text7, flag6, tripleDES);
									if (appointmentID >= 0 || forceUseAppointmentId)
									{
										array[5] = appointmentID;
									}
									otherInfoTable.Rows.Add(array);
									isEncryptedData = flag6;
								}
							}
							else if (text7.Length < 1)
							{
								dataRow4.Delete();
							}
							else
							{
								byte[] bytes3 = (byte[])dataRow4[4];
								string text9 = DynamicScreen.BytesToString(bytes3, flag6, tripleDES);
								if (text9.CompareTo(text7) != 0)
								{
									dataRow4[4] = DynamicScreen.StringToBytes(text7, flag6, tripleDES);
									isEncryptedData = flag6;
								}
							}
							int checkedIntVal3 = myMultiCheckbox2.CheckedIntVal;
							if (dataRow3 == null)
							{
								if (checkedIntVal3 > 0)
								{
									array[1] = screenNum;
									array[2] = personID;
									array[3] = controlID;
									array[4] = checkedIntVal3;
									if (appointmentID >= 0 || forceUseAppointmentId)
									{
										array[5] = appointmentID;
									}
									mainInfoTable.Rows.Add(array);
								}
							}
							else if (checkedIntVal3 <= 0)
							{
								dataRow3.Delete();
							}
							else
							{
								int num17 = (int)dataRow3[4];
								if (num17 != checkedIntVal3)
								{
									dataRow3[4] = checkedIntVal3;
								}
							}
						}
					}
					return;
				}
				if (controlCode != 600)
				{
					return;
				}
				if (imageInfoTable != null)
				{
					if (c is MyRichText)
					{
						int num18 = (int)dr[6];
						bool flag8 = num18 != 0;
						if (saveEncryptedDataAsPlainText)
						{
							flag8 = false;
						}
						MyRichText myRichText = (MyRichText)c;
						string text11 = myRichText.Text.Trim();
						string text12 = myRichText.PlainText.Trim();
						if (dataDR == null)
						{
							if (!myRichText.Empty)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = DynamicScreen.StringToBytes(text11, flag8, tripleDES);
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = imageInfoTable.Rows.Add(array);
								isEncryptedData = flag8;
							}
						}
						else if (text12.Length < 1)
						{
							dataDR.Delete();
						}
						else
						{
							string text13 = DynamicScreen.BytesToString((byte[])dataDR["controlvalue"], flag8, tripleDES);
							if (text13.CompareTo(text11) != 0)
							{
								dataDR[4] = DynamicScreen.StringToBytes(text11, flag8, tripleDES);
								isEncryptedData = flag8;
							}
						}
					}
				}
				else
				{
					MessageBox.Show("You need to patch your database to support the new rich text box control - the text was not saved.");
				}
				return;
			}
			else if (controlCode <= 703)
			{
				if (controlCode == 620)
				{
					if (imageInfoTable != null)
					{
						if (c is MyMultilineTextBoxWithEditingControls)
						{
							int num19 = (int)dr[6];
							bool flag9 = num19 != 0;
							if (saveEncryptedDataAsPlainText)
							{
								flag9 = false;
							}
							MyMultilineTextBoxWithEditingControls myMultilineTextBoxWithEditingControls = (MyMultilineTextBoxWithEditingControls)c;
							string itemsAsXml = myMultilineTextBoxWithEditingControls.GetItemsAsXml();
							if (dataDR == null)
							{
								if (itemsAsXml.Length > 0)
								{
									array[1] = screenNum;
									array[2] = personID;
									array[3] = controlID;
									array[4] = DynamicScreen.StringToBytes(itemsAsXml, flag9, tripleDES);
									if (appointmentID >= 0 || forceUseAppointmentId)
									{
										array[5] = appointmentID;
									}
									dataDR = imageInfoTable.Rows.Add(array);
									isEncryptedData = flag9;
								}
							}
							else if (itemsAsXml.Length < 1)
							{
								dataDR.Delete();
							}
							else
							{
								string text13 = DynamicScreen.BytesToString((byte[])dataDR["controlvalue"], flag9, tripleDES);
								if (text13.CompareTo(itemsAsXml) != 0)
								{
									dataDR[4] = DynamicScreen.StringToBytes(itemsAsXml, flag9, tripleDES);
									isEncryptedData = flag9;
								}
							}
						}
					}
					else
					{
						MessageBox.Show("You need to patch your database to support the new multi line text box control - the text was not saved.");
					}
					return;
				}
				switch (controlCode)
				{
				case 700:
				{
					AccommodationControl2 accommodationControl = (AccommodationControl2)c;
					if (!accommodationControl.Checked)
					{
						if (dataDR != null)
						{
							dataDR.Delete();
						}
					}
					else
					{
						bool @checked = accommodationControl.Checked;
						if (dataDR == null)
						{
							array[1] = screenNum;
							array[2] = personID;
							array[3] = controlID;
							array[4] = DynamicScreen.BoolToInt(true);
							if (appointmentID >= 0 || forceUseAppointmentId)
							{
								array[5] = appointmentID;
							}
							dataDR = mainInfoTable.Rows.Add(array);
						}
						DynamicScreen.SaveExtraAccommodationInfo(accommodationControl, dr, ref dataDR, mainInfoTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, personID, controlID, appointmentID, tripleDES, forceUseAppointmentId);
					}
					return;
				}
				case 701:
				{
					AccommodationControl2 accommodationControl2 = (AccommodationControl2)c;
					if (!accommodationControl2.Checked)
					{
						if (dataDR != null)
						{
							dataDR.Delete();
						}
					}
					else
					{
						int num18 = (int)dr[6];
						bool flag8 = num18 != 0;
						if (saveEncryptedDataAsPlainText)
						{
							flag8 = false;
						}
						TextBox txt = accommodationControl2.Txt;
						string text14 = txt.Text.Trim();
						if (text14.Length < 1)
						{
							text14 = " ";
						}
						byte[] array6 = DynamicScreen.StringToBytes(text14, flag8, tripleDES);
						isEncryptedData = flag8;
						if (dataDR == null)
						{
							array[1] = screenNum;
							array[2] = personID;
							array[3] = controlID;
							array[4] = array6;
							if (appointmentID >= 0 || forceUseAppointmentId)
							{
								array[5] = appointmentID;
							}
							dataDR = otherInfoTable.Rows.Add(array);
						}
						else
						{
							string text15 = DynamicScreen.BytesToString((byte[])dataDR[4], flag8, tripleDES);
							if (text15.CompareTo(text14) != 0)
							{
								dataDR[4] = array6;
							}
						}
						DynamicScreen.SaveExtraAccommodationInfo(accommodationControl2, dr, ref dataDR, mainInfoTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, personID, controlID, appointmentID, tripleDES, forceUseAppointmentId);
					}
					return;
				}
				case 702:
				{
					AccommodationControl2 accommodationControl3 = (AccommodationControl2)c;
					if (!accommodationControl3.Checked)
					{
						if (dataDR != null)
						{
							dataDR.Delete();
						}
					}
					else
					{
						DateTime dtpValue = accommodationControl3.DtpValue;
						if (dataDR == null)
						{
							array[1] = screenNum;
							array[2] = personID;
							array[3] = controlID;
							if (dtpValue == DateTime.MinValue)
							{
								array[4] = null;
							}
							else
							{
								array[4] = dtpValue;
							}
							if (appointmentID >= 0 || forceUseAppointmentId)
							{
								array[5] = appointmentID;
							}
							dataDR = dateTimeInfoTable.Rows.Add(array);
						}
						else
						{
							bool flag10 = dataDR[4] == DBNull.Value && dtpValue != DateTime.MinValue;
							if (!flag10)
							{
								DateTime value2 = (DateTime)dataDR[4];
								if (dtpValue.CompareTo(value2) != 0)
								{
									flag10 = true;
								}
							}
							if (flag10)
							{
								dataDR[4] = dtpValue;
							}
						}
						DynamicScreen.SaveExtraAccommodationInfo(accommodationControl3, dr, ref dataDR, mainInfoTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, personID, controlID, appointmentID, tripleDES, forceUseAppointmentId);
					}
					return;
				}
				case 703:
				{
					AccommodationControl2 accommodationControl4 = (AccommodationControl2)c;
					if (!accommodationControl4.Checked)
					{
						if (dataDR != null)
						{
							dataDR.Delete();
						}
					}
					else
					{
						autoComboBox = accommodationControl4.Cmb;
						int num20 = (int)dr[6];
						bool flag11 = num20 < 0;
						if (saveEncryptedDataAsPlainText)
						{
							flag11 = false;
						}
						int num21 = -1;
						string text16 = autoComboBox.Text.Trim();
						if (num20 == 0 || num20 == 2)
						{
							if (text16.Length > 0)
							{
								int num5 = autoComboBox.SelectedIndexByText();
								if (num5 < 0)
								{
									num21 = -1;
								}
								else
								{
									DataTable dataTable = (DataTable)autoComboBox.DataSource;
									if (dataTable.Rows.Count > num5 && dataTable.Rows[num5][0] != DBNull.Value)
									{
										num21 = (int)dataTable.Rows[num5][0];
									}
								}
							}
							if (dataDR == null)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = num21;
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = mainInfoTable.Rows.Add(array);
							}
							else
							{
								int num6 = (dataDR[4] == DBNull.Value) ? (num21 + 1) : ((int)dataDR[4]);
								if (num6 != num21)
								{
									dataDR[4] = num21;
								}
							}
						}
						else
						{
							string text17 = text16.Trim();
							if (text17.Length < 1)
							{
								text17 = " ";
							}
							byte[] array7 = DynamicScreen.StringToBytes(text17, flag11, tripleDES);
							isEncryptedData = flag11;
							if (saveEncryptedDataAsPlainText)
							{
								isEncryptedData = false;
							}
							if (dataDR == null)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = array7;
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = otherInfoTable.Rows.Add(array);
							}
							else
							{
								string text18 = DynamicScreen.BytesToString((byte[])dataDR[4], flag11, tripleDES);
								if (text18.CompareTo(text17) != 0)
								{
									dataDR[4] = array7;
								}
							}
						}
						DynamicScreen.SaveExtraAccommodationInfo(accommodationControl4, dr, ref dataDR, mainInfoTable, otherInfoTable, dateTimeInfoTable, imageInfoTable, personID, controlID, appointmentID, tripleDES, forceUseAppointmentId);
					}
					return;
				}
				default:
					return;
				}
			}
			else
			{
				switch (controlCode)
				{
				case 801:
					if (c is DynamicControlChooser)
					{
						DynamicControlChooser dynamicControlChooser = (DynamicControlChooser)c;
						string selectedControlIdsStringCommaSeparated = dynamicControlChooser.GetSelectedControlIdsStringCommaSeparated();
						if (selectedControlIdsStringCommaSeparated.Length > 0)
						{
							if (dataDR == null)
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = DynamicScreen.StringToBytes(selectedControlIdsStringCommaSeparated, false, tripleDES);
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = mainInfoTable.Rows.Add(array);
							}
						}
						else
						{
							dataDR.Delete();
						}
					}
					return;
				case 802:
					if (c is AutoComboBox.MyControls.MultiDatabaseItemSelect)
					{
						AutoComboBox.MyControls.MultiDatabaseItemSelect multiDatabaseItemSelect = (AutoComboBox.MyControls.MultiDatabaseItemSelect)c;
						string text4 = multiDatabaseItemSelect.ToString();
						if (dataDR == null)
						{
							if (!string.IsNullOrEmpty(text4))
							{
								array[1] = screenNum;
								array[2] = personID;
								array[3] = controlID;
								array[4] = DynamicScreen.StringToBytes(text4, false, tripleDES);
								if (appointmentID >= 0 || forceUseAppointmentId)
								{
									array[5] = appointmentID;
								}
								dataDR = otherInfoTable.Rows.Add(array);
							}
						}
						else if (string.IsNullOrEmpty(text4))
						{
							dataDR.Delete();
						}
						else
						{
							byte[] array8 = (byte[])dataDR[4];
							if (array8 == null || DynamicScreen.BytesToString(array8, false, tripleDES) != text4)
							{
								dataDR[4] = DynamicScreen.StringToBytes(text4, false, tripleDES);
							}
						}
					}
					return;
				default:
					if (controlCode != 806)
					{
						return;
					}
					break;
				}
			}
			IL_C11:
			autoComboBox = (AutoComboBox)c;
			DataRow dataRow6 = autoComboBox.SelectedDataRow();
			if (dataRow6 != null)
			{
				num9 = (int)dataRow6[autoComboBox.ValueMember];
			}
			else
			{
				num9 = -1;
			}
			if ((dataRow6 == null || num9 < 0) && dataDR != null)
			{
				dataDR.Delete();
			}
			else if (dataRow6 != null && num9 > -1)
			{
				if (dataDR == null)
				{
					array[1] = screenNum;
					array[2] = personID;
					array[3] = controlID;
					array[4] = (int)dataRow6[autoComboBox.ValueMember];
					if (appointmentID >= 0 || forceUseAppointmentId)
					{
						array[5] = appointmentID;
					}
					dataDR = mainInfoTable.Rows.Add(array);
				}
				else
				{
					int num10 = (dataDR[4] == DBNull.Value) ? (num9 + 1) : ((int)dataDR[4]);
					if (num9 != num10)
					{
						dataDR[4] = num9;
					}
				}
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00030BDC File Offset: 0x0002FBDC
		private static void SaveExtraAccommodationInfo(AccommodationControl2 ctrl, DataRow dr, ref DataRow dataDR, DataTable mainInfoTable, DataTable otherInfoTable, DataTable dateTimeInfoTable, DataTable imageInfoTable, int personID, int controlID, int appointmentID, TripleDESEncryptionClass tripleDES, bool forceUseAppointmentId)
		{
			if (mainInfoTable.Columns.Contains("offline"))
			{
				if (!ctrl.Checked && dataDR != null)
				{
					dataDR.Delete();
				}
				else if (ctrl.Checked)
				{
					bool offline = ctrl.Offline;
					bool showOnLetter = ctrl.ShowOnLetter;
					DateTime expiryDate = ctrl.ExpiryDate;
					string text = ctrl.TextForLetter.Trim();
					string text2 = ctrl.PrivateNote.Trim();
					string recommendedToStudentButDeclinedDetail = ctrl.RecommendedToStudentButDeclinedDetail;
					bool recommendedToStudentButDeclined = ctrl.RecommendedToStudentButDeclined;
					bool approved = ctrl.Approved;
					if (dataDR != null && dataDR.RowState == DataRowState.Detached)
					{
						dataDR = null;
					}
					if (dataDR == null)
					{
						switch (ctrl.AccommodationControlType)
						{
						case AccommodationControlType.CheckBox:
							dataDR = mainInfoTable.NewRow();
							dataDR["controlvalue"] = 1;
							break;
						case AccommodationControlType.TextBox:
						{
							dataDR = otherInfoTable.NewRow();
							int num = (int)dr[6];
							dataDR["controlvalue"] = DynamicScreen.StringToBytes(ctrl.Txt.Text, num != 0, tripleDES);
							break;
						}
						case AccommodationControlType.ComboBoxSimple:
							dataDR = mainInfoTable.NewRow();
							dataDR["controlvalue"] = ctrl.CmbValue;
							break;
						case AccommodationControlType.ComboText:
						{
							dataDR = otherInfoTable.NewRow();
							int num2 = (int)dr[6];
							dataDR["controlvalue"] = DynamicScreen.StringToBytes(ctrl.CmbText, num2 != 0, tripleDES);
							break;
						}
						case AccommodationControlType.Date:
							dataDR = dateTimeInfoTable.NewRow();
							dataDR["controlvalue"] = ctrl.DtpValue;
							break;
						}
						dataDR["screennum"] = 0;
						dataDR["personid"] = personID;
						dataDR["controlid"] = controlID;
						if (appointmentID >= 0 || forceUseAppointmentId)
						{
							dr[5] = appointmentID;
						}
						dataDR["offline"] = offline;
						if (expiryDate == DateTime.MinValue)
						{
							dataDR["expirydate"] = DBNull.Value;
						}
						else
						{
							dataDR["expirydate"] = expiryDate;
						}
						if (text.Length < 1)
						{
							dataDR["altlongdescription"] = DBNull.Value;
						}
						else
						{
							dataDR["altlongdescription"] = tripleDES.Encrypt(text);
						}
						if (text2.Length < 1)
						{
							dataDR["note"] = DBNull.Value;
						}
						else
						{
							dataDR["note"] = tripleDES.Encrypt(text2);
						}
						dataDR["showonletter"] = showOnLetter;
						if (dataDR.Table.Columns.Contains("recommendedbutdeclineddetail"))
						{
							dataDR["recommendedbutdeclined"] = recommendedToStudentButDeclined;
							dataDR["recommendedbutdeclineddetail"] = recommendedToStudentButDeclinedDetail;
							dataDR["approved"] = approved;
						}
						switch (ctrl.AccommodationControlType)
						{
						case AccommodationControlType.CheckBox:
						case AccommodationControlType.ComboBoxSimple:
							mainInfoTable.Rows.Add(dataDR);
							break;
						case AccommodationControlType.TextBox:
						case AccommodationControlType.ComboText:
							otherInfoTable.Rows.Add(dataDR);
							break;
						case AccommodationControlType.Date:
							dateTimeInfoTable.Rows.Add(dataDR);
							break;
						}
					}
					else
					{
						if (dataDR.RowState == DataRowState.Deleted)
						{
							dataDR.RejectChanges();
						}
						bool flag = dataDR["offline"] != DBNull.Value && (bool)dataDR["offline"];
						bool flag2 = dataDR["showonletter"] != DBNull.Value && (int)dataDR["showonletter"] == 1;
						DateTime d = (dataDR["expirydate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataDR["expirydate"]);
						string text3 = (dataDR["altlongdescription"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataDR["altlongdescription"]);
						string text4 = (dataDR["note"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataDR["note"]);
						bool flag3 = false;
						if (dataDR.Table.Columns.Contains("recommendedbutdeclineddetail"))
						{
							string text5 = (dataDR["recommendedbutdeclineddetail"] == DBNull.Value) ? "" : tripleDES.Decrypt((byte[])dataDR["recommendedbutdeclineddetail"]);
							bool flag4 = dataDR["recommendedbutdeclined"] != DBNull.Value && Convert.ToBoolean(dataDR["recommendedbutdeclined"]);
							bool flag5 = dataDR["approved"] != DBNull.Value && Convert.ToBoolean(dataDR["approved"]);
							if (flag5 != approved || flag4 != recommendedToStudentButDeclined || !text5.Equals(recommendedToStudentButDeclinedDetail))
							{
								flag3 = true;
								dataDR["approved"] = approved;
								dataDR["recommendedbutdeclined"] = recommendedToStudentButDeclined;
								if (recommendedToStudentButDeclinedDetail == null || recommendedToStudentButDeclinedDetail.Trim().Length < 1)
								{
									dataDR["recommendedbutdeclineddetail"] = DBNull.Value;
								}
								else
								{
									dataDR["recommendedbutdeclineddetail"] = tripleDES.Encrypt(recommendedToStudentButDeclinedDetail);
								}
							}
						}
						if (flag != offline || flag2 != showOnLetter || d != expiryDate || text3.CompareTo(text) != 0 || text4.CompareTo(text2) != 0 || flag3)
						{
							dataDR["offline"] = offline;
							if (expiryDate == DateTime.MinValue)
							{
								dataDR["expirydate"] = DBNull.Value;
							}
							else
							{
								dataDR["expirydate"] = expiryDate;
							}
							if (text.Length < 1)
							{
								dataDR["altlongdescription"] = DBNull.Value;
							}
							else
							{
								dataDR["altlongdescription"] = tripleDES.Encrypt(text);
							}
							if (text2.Length < 1)
							{
								dataDR["note"] = DBNull.Value;
							}
							else
							{
								dataDR["note"] = tripleDES.Encrypt(text2);
							}
							dataDR["showonletter"] = showOnLetter;
						}
					}
				}
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00031310 File Offset: 0x00030310
		public static byte[] GetFileBytes(string filename)
		{
			long num;
			byte[] fileBytes = DynamicScreen.ReadByteArrayFromFile(filename, out num);
			FileInfo fileInfo = new FileInfo(filename);
			return DynamicScreen.PackageUpFile(fileBytes, filename, "", "", "");
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00031348 File Offset: 0x00030348
		public static byte[] PackageUpFile(byte[] FileBytes, string filename, string contentType, string encryptionMethodBlankForNoEncryption, string compressionMethodBlankForNoCompression)
		{
			if (compressionMethodBlankForNoCompression.Length > 0)
			{
			}
			int num = FileBytes.Length;
			if (encryptionMethodBlankForNoEncryption.Length > 0)
			{
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("filename=");
			stringBuilder.Append(Path.GetFileName(filename));
			stringBuilder.Append(";");
			stringBuilder.Append("filetype=");
			stringBuilder.Append(contentType);
			stringBuilder.Append(";");
			stringBuilder.Append("filesize=");
			stringBuilder.Append(num.ToString());
			stringBuilder.Append(";");
			stringBuilder.Append("emethod=");
			stringBuilder.Append(encryptionMethodBlankForNoEncryption);
			stringBuilder.Append(";");
			stringBuilder.Append("cmethod=");
			stringBuilder.Append(compressionMethodBlankForNoCompression);
			stringBuilder.Append(";");
			string txt = stringBuilder.ToString();
			byte[] array = DynamicScreen.StringToBytes(txt, false, null);
			string text = array.Length.ToString();
			int num2 = 6 - text.Length;
			if (num2 > 0 && num2 < 7)
			{
				text = new string('0', num2) + text;
			}
			byte[] array2 = DynamicScreen.StringToBytes(text, false, null);
			int num3 = array.Length + array2.Length + FileBytes.Length;
			byte[] array3 = new byte[num3];
			array2.CopyTo(array3, 0);
			array.CopyTo(array3, array2.Length);
			FileBytes.CopyTo(array3, array2.Length + array.Length);
			return array3;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000314E0 File Offset: 0x000304E0
		private static byte[] ReadByteArrayFromFile(string fileName, out long length)
		{
			byte[] result = null;
			try
			{
				FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				BinaryReader binaryReader = new BinaryReader(input);
				long length2 = new FileInfo(fileName).Length;
				length = length2;
				result = binaryReader.ReadBytes((int)length2);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				length = 0L;
			}
			return result;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0003154C File Offset: 0x0003054C
		private static string[] SplitUpString(string controlValue, int count)
		{
			string[] array = new string[count];
			int num = 0;
			int length = controlValue.Length;
			for (int i = 0; i < count; i++)
			{
				if (num < length)
				{
					int num2 = length - num;
					int length2 = (num2 < 3000) ? num2 : 3000;
					array[i] = controlValue.Substring(num, length2);
					num += 3000;
				}
				else
				{
					array[i] = "";
				}
			}
			return array;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000315CC File Offset: 0x000305CC
		public static bool CheckWarningsErrors(Control p_data, out string errmsg)
		{
			errmsg = "";
			bool flag = true;
			bool result;
			if (p_data.Enabled)
			{
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				bool flag2 = DynamicScreen.CheckForMissingFields(p_data, ref arrayList, ref arrayList2);
				if (flag2)
				{
					if (arrayList2.Count > 0)
					{
						flag = false;
						string text = "";
						foreach (object obj in arrayList2)
						{
							DynamicControl dynamicControl = (DynamicControl)obj;
							text = text + dynamicControl.ControlCaption + Environment.NewLine;
						}
						errmsg = "One or more required fields are missing (see list below).  You must fill in the missing field(s) in order to continue." + Environment.NewLine + Environment.NewLine + text;
					}
					else if (arrayList.Count > 0)
					{
						string text2 = "";
						foreach (object obj2 in arrayList)
						{
							DynamicControl dynamicControl = (DynamicControl)obj2;
							text2 = text2 + dynamicControl.ControlCaption + Environment.NewLine;
						}
						errmsg = "The following required field(s) are missing:" + Environment.NewLine + text2;
					}
				}
				result = flag;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0003176C File Offset: 0x0003076C
		private static void SaveTextBox(string controlValue, bool isEncrypted, TripleDESEncryptionClass tripleDES, ref DataRow dataDR, int screenNum, int personID, int controlID, DataTable otherInfoTable, int appointmentID, ref object[] o, bool forceUseAppointmentId)
		{
			if (appointmentID >= 0 || forceUseAppointmentId)
			{
			}
			bool flag = controlValue.Length < 1;
			byte[] array;
			if (isEncrypted && !flag)
			{
				array = tripleDES.Encrypt(controlValue);
			}
			else if (!flag)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				array = utf8Encoding.GetBytes(controlValue);
			}
			else
			{
				array = null;
			}
			if (flag && dataDR != null)
			{
				dataDR.Delete();
			}
			else if (!flag && dataDR == null)
			{
				o[1] = screenNum;
				o[2] = personID;
				o[3] = controlID;
				o[4] = array;
				if (appointmentID >= 0 || forceUseAppointmentId)
				{
					o[5] = appointmentID;
				}
				dataDR = otherInfoTable.Rows.Add(o);
			}
			else if (!flag)
			{
				object obj = dataDR[4];
				if (obj != null && obj != DBNull.Value && !(obj is byte[]))
				{
					throw new Exception("Data cannot be saved.  It looks like there is data in a drop list that was previously saved as a lookup list value, but is now set to a text value.  This will happen if you change the type of a drop list (from 'user can only pick from a list' to 'user can enter text') in the forms builder.  Please remove this field and add a new one to resolve this issue, or contact support at https://support.tpro.ca.  The control id is " + controlID.ToString());
				}
				byte[] array2 = (byte[])obj;
				string text;
				if (isEncrypted)
				{
					text = tripleDES.Decrypt(array2).Trim();
				}
				else
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					text = utf8Encoding.GetString(array2).Trim();
				}
				if (text.CompareTo(controlValue) != 0)
				{
					dataDR[4] = array;
				}
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0003190C File Offset: 0x0003090C
		public static bool IsControlCodeDataHolding(DataTable dynamicScreenNonDataControlsTable, int ControlCode)
		{
			bool result;
			if (ControlCode == 804)
			{
				result = false;
			}
			else
			{
				if (dynamicScreenNonDataControlsTable == null)
				{
					dynamicScreenNonDataControlsTable = ClientCache.CurrentInstance.dynamicScreenNonDataControlsTable;
				}
				foreach (object obj in dynamicScreenNonDataControlsTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (int)dataRow[1];
					if (num == ControlCode)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000319F0 File Offset: 0x000309F0
		public static bool IsControlCodeDataHolding(int controlCode)
		{
			int[] array = new int[]
			{
				8,
				9,
				13,
				30,
				31,
				32,
				33,
				34,
				35,
				50
			};
			return Array.IndexOf<int>(array, controlCode) < 0;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00031A20 File Offset: 0x00030A20
		public static string GetDataString(DataTable accommodationsTable, Panel p_data, int controlID, DataRow dataDR, TripleDESEncryptionClass tripleDES, int AccommodationLetterGrouping, int AccommodationEmailGrouping, out double extraTimePercent, int extraTimeType)
		{
			return DynamicScreen.GetDataString(accommodationsTable, p_data, controlID, dataDR, tripleDES, AccommodationLetterGrouping, AccommodationEmailGrouping, out extraTimePercent, extraTimeType, "", true);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00031A4C File Offset: 0x00030A4C
		public static string GetDataString(DataTable accommodationsTable, Panel p_data, int controlID, DataRow dataDR, TripleDESEncryptionClass tripleDES, int AccommodationLetterGrouping, int AccommodationEmailGrouping, out double extraTimePercent, int extraTimeType, string languageCode)
		{
			return DynamicScreen.GetDataString(accommodationsTable, p_data, controlID, dataDR, tripleDES, AccommodationLetterGrouping, AccommodationEmailGrouping, out extraTimePercent, extraTimeType, languageCode, true);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00031A74 File Offset: 0x00030A74
		public static string GetDataString(DataTable accommodationsTable, Panel p_data, int controlID, DataRow dataDR, TripleDESEncryptionClass tripleDES, int AccommodationLetterGrouping, int AccommodationEmailGrouping, out double extraTimePercent, int extraTimeType, string languageCode, bool ignoreNonShowOnLetterShowOnEmailDateTimePicker)
		{
			return DynamicScreen.GetDataString(accommodationsTable, p_data, controlID, dataDR, tripleDES, AccommodationLetterGrouping, AccommodationEmailGrouping, out extraTimePercent, extraTimeType, languageCode, ignoreNonShowOnLetterShowOnEmailDateTimePicker, null);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00031AA0 File Offset: 0x00030AA0
		public static string GetDataString(DataTable accommodationsTable, Panel p_data, int controlID, DataRow dataDR, TripleDESEncryptionClass tripleDES, int AccommodationLetterGrouping, int AccommodationEmailGrouping, out double extraTimePercent, int extraTimeType, string languageCode, bool ignoreNonShowOnLetterShowOnEmailDateTimePicker, string[] onlyIncludeShortCodes)
		{
			Control control = DynamicScreen.GetControl(p_data, controlID);
			if (control != null && control.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)control.Tag;
				string text = dataRow.Table.Columns.Contains("setting4string") ? dataRow["setting4string"].ToString().Trim() : "";
				string text2;
				if (languageCode.CompareTo("fr") == 0 && text.Length > 0)
				{
					text2 = text;
				}
				else
				{
					text2 = (string)dataRow[3];
				}
				bool flag = false;
				bool flag2 = false;
				if (accommodationsTable != null)
				{
					foreach (object obj in accommodationsTable.Rows)
					{
						DataRow dataRow2 = (DataRow)obj;
						int num = (int)dataRow2[1];
						if (num == controlID)
						{
							flag2 = true;
							int num2 = Convert.ToInt32(dataRow2[4]);
							int num3 = Convert.ToInt32(dataRow2[5]);
							int num4 = Convert.ToInt32(dataRow2["showOnReport"]);
							flag = (flag || Convert.ToBoolean(dataRow2[6]));
							bool flag3 = AccommodationLetterGrouping < 0 || (AccommodationLetterGrouping & num2) > 0;
							bool flag4 = AccommodationEmailGrouping < 0 || (AccommodationEmailGrouping & num3) > 0;
							string text3 = dataRow2["shortcode"].ToString().ToLower().Trim();
							bool flag5 = true;
							if (onlyIncludeShortCodes != null)
							{
								if (text3.IndexOf(',') > 0)
								{
									string[] array = text3.Split(new char[]
									{
										','
									});
									bool flag6 = false;
									foreach (string value in array)
									{
										if (Array.IndexOf<string>(onlyIncludeShortCodes, value) >= 0)
										{
											flag6 = true;
											break;
										}
									}
									flag5 = flag6;
								}
								else if (Array.IndexOf<string>(onlyIncludeShortCodes, text3) < 0)
								{
									flag5 = false;
								}
							}
							if (!flag5)
							{
								text2 = null;
							}
							else if (ignoreNonShowOnLetterShowOnEmailDateTimePicker && (!flag3 || !flag4 || control is MyDateTimePicker))
							{
								text2 = null;
							}
							else if (languageCode.CompareTo("fr") != 0)
							{
								string text4 = ((string)dataRow2[2]).Trim();
								if (text4.Length > 0)
								{
									text2 = text4;
								}
							}
						}
					}
				}
				if (!flag2 && AccommodationLetterGrouping > -1)
				{
					text2 = null;
				}
				if (text2 != null)
				{
					int num5 = text2.IndexOf("~~");
					if (num5 > 0)
					{
						text2 = text2.Substring(0, num5);
					}
					if (text2 == "." || text2 == ".:")
					{
						text2 = "";
					}
					if (control is CheckBox || control is MyCheckBox || (control is AccommodationControl2 && ((AccommodationControl2)control).AccommodationControlType == AccommodationControlType.CheckBox))
					{
						if (flag)
						{
							extraTimePercent = DynamicScreen.GetExtraTimePercent(text2, extraTimeType);
						}
						else
						{
							extraTimePercent = 0.0;
						}
						return text2;
					}
					if (control is ListSelect)
					{
						if (flag)
						{
							extraTimePercent = DynamicScreen.GetExtraTimePercent(text2, extraTimeType);
						}
						else
						{
							extraTimePercent = 0.0;
						}
						return text2;
					}
					if (control is RadioButton)
					{
						if (flag)
						{
							extraTimePercent = DynamicScreen.GetExtraTimePercent(text2, extraTimeType);
						}
						else
						{
							extraTimePercent = 0.0;
						}
						return text2;
					}
					if (control is TextBox || control is MyTextBox || control is MyMaskedTextBox || control is MaskedTextBox || (control is AccommodationControl2 && ((AccommodationControl2)control).AccommodationControlType == AccommodationControlType.TextBox))
					{
						int num6 = (int)dataRow[6];
						bool flag7 = num6 != 0;
						byte[] array3 = (byte[])dataDR[4];
						string text5;
						if (!flag7)
						{
							UTF8Encoding utf8Encoding = new UTF8Encoding();
							text5 = utf8Encoding.GetString(array3);
						}
						else
						{
							text5 = tripleDES.Decrypt(array3);
						}
						if (flag)
						{
							extraTimePercent = DynamicScreen.GetExtraTimePercent(text5, extraTimeType);
						}
						else
						{
							extraTimePercent = 0.0;
						}
						return text2 + ": " + text5;
					}
					if (control is MyRadioGroup)
					{
						extraTimePercent = 0.0;
						MyRadioGroup myRadioGroup = (MyRadioGroup)control;
						int id;
						if (dataDR[4] != DBNull.Value && dataDR[4] is int)
						{
							id = (int)dataDR[4];
						}
						else
						{
							id = 0;
						}
						return text2 + ": " + myRadioGroup.GetText(id);
					}
					if (control is MyMultiCheckbox)
					{
						MyMultiCheckbox myMultiCheckbox = (MyMultiCheckbox)control;
						extraTimePercent = 0.0;
						return myMultiCheckbox.ToStringMailMerge();
					}
					if (control is AutoComboBox || (control is AccommodationControl2 && (((AccommodationControl2)control).AccommodationControlType == AccommodationControlType.ComboBoxSimple || ((AccommodationControl2)control).AccommodationControlType == AccommodationControlType.ComboText)))
					{
						AutoComboBox autoComboBox;
						if (control is AccommodationControl2)
						{
							autoComboBox = ((AccommodationControl2)control).Cmb;
						}
						else
						{
							autoComboBox = (AutoComboBox)control;
						}
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
						if (dataDR[4] == DBNull.Value)
						{
							extraTimePercent = 0.0;
							return text2;
						}
						if (!(dataDR[4] is int))
						{
							int num6 = (int)dataRow[6];
							bool flag7 = num6 == -1;
							byte[] bytes = (byte[])dataDR[4];
							string text6 = DynamicScreen.BytesToString(bytes, flag7, tripleDES);
							if (flag)
							{
								extraTimePercent = DynamicScreen.GetExtraTimePercent(text6, extraTimeType);
							}
							else
							{
								extraTimePercent = 0.0;
							}
							return text2 + ": " + text6;
						}
						int num7 = (dataDR[4] == DBNull.Value) ? 0 : ((int)dataDR[4]);
						foreach (object obj2 in dataTable.Rows)
						{
							DataRow dataRow3 = (DataRow)obj2;
							if (dataRow3[0] != DBNull.Value)
							{
								int num8 = (int)dataRow3[0];
								if (num8 == num7)
								{
									string text7;
									if (languageCode.CompareTo("fr") == 0)
									{
										text7 = dataRow3["lookupvalue"].ToString();
									}
									else
									{
										text7 = (string)dataRow3[2];
									}
									if (flag)
									{
										extraTimePercent = DynamicScreen.GetExtraTimePercent(text7, extraTimeType);
									}
									else
									{
										extraTimePercent = 0.0;
									}
									return text2 + ": " + text7;
								}
							}
						}
					}
					else if (control is MyDateTimePicker || (control is AccommodationControl2 && ((AccommodationControl2)control).AccommodationControlType == AccommodationControlType.Date))
					{
						if (control is AccommodationControl2)
						{
							MyDateTimePicker myDateTimePicker = ((AccommodationControl2)control).Dtp;
						}
						else
						{
							MyDateTimePicker myDateTimePicker = (MyDateTimePicker)control;
						}
						DateTime d = (dataDR[4] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataDR[4]);
						extraTimePercent = 0.0;
						if (d == DateTime.MinValue)
						{
							return text2 + ": not set";
						}
						return text2 + ": " + d.ToString("MMMM d, yyyy");
					}
					else if (control is CtrlDateTimePicker)
					{
						CtrlDateTimePicker ctrlDateTimePicker = (CtrlDateTimePicker)control;
						extraTimePercent = 0.0;
						DateTime? value2 = ctrlDateTimePicker.Value;
						if (value2 == null)
						{
							return text2 + ": not set";
						}
						return text2 + ": " + value2.Value.ToString("MMMM d, yyyy");
					}
					else if (control is MyDateTimePickerForAccommodationsExpiry)
					{
						MyDateTimePickerForAccommodationsExpiry myDateTimePickerForAccommodationsExpiry = (MyDateTimePickerForAccommodationsExpiry)control;
						DateTime d = (dataDR[4] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataDR[4]);
						extraTimePercent = 0.0;
						if (d == DateTime.MinValue)
						{
							return text2 + ": not set";
						}
						return text2 + ": " + d.ToString("MMMM d, yyyy");
					}
					else if (control is MyPicture)
					{
						extraTimePercent = 0.0;
						MyPicture myPicture = (MyPicture)control;
						return myPicture.GetBase64String();
					}
				}
			}
			extraTimePercent = 0.0;
			return null;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00032550 File Offset: 0x00031550
		public static object GetDataFromControl(Control c, DynamicControl dc, DynamicScreen.DynamicDataFormatType formatType, TripleDESEncryptionClass tripleDES)
		{
			object result;
			if (c is TextBox)
			{
				string text = ((TextBox)c).Text.Trim();
				if (formatType == DynamicScreen.DynamicDataFormatType.Runtime)
				{
					result = text;
				}
				else if (text.Length < 1)
				{
					result = DBNull.Value;
				}
				else
				{
					result = DynamicScreen.StringToBytes(text, dc.Setting1 != 0, tripleDES);
				}
			}
			else if (c is CheckBox)
			{
				bool @checked = ((CheckBox)c).Checked;
				if (formatType == DynamicScreen.DynamicDataFormatType.Runtime)
				{
					result = @checked;
				}
				else if (!@checked)
				{
					result = DBNull.Value;
				}
				else
				{
					result = 1;
				}
			}
			else if (c is MyRadioGroup)
			{
				MyRadioGroup myRadioGroup = (MyRadioGroup)c;
				if (formatType == DynamicScreen.DynamicDataFormatType.Runtime)
				{
					result = myRadioGroup.SelectedId;
				}
				else if (myRadioGroup.SelectedId < 0)
				{
					result = DBNull.Value;
				}
				else
				{
					result = myRadioGroup.SelectedId;
				}
			}
			else if (c is MyFile)
			{
				MyFile myFile = (MyFile)c;
				result = null;
			}
			else if (c is AutoComboBox)
			{
				AutoComboBox autoComboBox = (AutoComboBox)c;
				if (dc.Setting1 == 0)
				{
					DataRow dataRow = autoComboBox.SelectedDataRow();
					if (dataRow == null)
					{
						if (formatType == DynamicScreen.DynamicDataFormatType.Runtime)
						{
							result = -1;
						}
						else
						{
							result = DBNull.Value;
						}
					}
					else
					{
						result = (int)dataRow[autoComboBox.ValueMember];
					}
				}
				else
				{
					string text2 = autoComboBox.SelectedText.Trim();
					if (formatType == DynamicScreen.DynamicDataFormatType.Runtime)
					{
						result = text2;
					}
					else if (text2.Length < 1)
					{
						result = DBNull.Value;
					}
					else
					{
						result = DynamicScreen.StringToBytes(text2, dc.Setting1 == -1, tripleDES);
					}
				}
			}
			else if (c is MyRichText)
			{
				result = null;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000327B0 File Offset: 0x000317B0
		public static string GetDataString(Control c, string controlCaption)
		{
			string result;
			if (c is CheckBox || c is MyCheckBox)
			{
				result = controlCaption;
			}
			else if (c is RadioButton)
			{
				result = controlCaption;
			}
			else if (c is TextBox || c is MyTextBox || c is MyMaskedTextBox || c is MaskedTextBox)
			{
				result = controlCaption + ": " + c.Text;
			}
			else if (c is MyRadioGroup)
			{
				MyRadioGroup myRadioGroup = (MyRadioGroup)c;
				result = myRadioGroup.SelectedText;
			}
			else if (c is MyMultiCheckbox)
			{
				MyMultiCheckbox myMultiCheckbox = (MyMultiCheckbox)c;
				result = myMultiCheckbox.ToStringMailMerge();
			}
			else if (c is AutoComboBox)
			{
				AutoComboBox autoComboBox = (AutoComboBox)c;
				DataRow dataRow = autoComboBox.SelectedDataRow();
				if (dataRow != null)
				{
					result = dataRow[autoComboBox.DisplayMember].ToString();
				}
				else
				{
					result = "";
				}
			}
			else if (c is MyDateTimePicker)
			{
				DateTimePicker dateTimePicker = (DateTimePicker)c;
				result = dateTimePicker.Value.ToShortDateString();
			}
			else if (c is CtrlDateTimePicker)
			{
				CtrlDateTimePicker ctrlDateTimePicker = (CtrlDateTimePicker)c;
				DateTime? value = ctrlDateTimePicker.Value;
				result = ((value != null) ? value.Value.ToShortDateString() : "");
			}
			else if (c is MyDateTimePickerForAccommodationsExpiry)
			{
				MyDateTimePickerForAccommodationsExpiry myDateTimePickerForAccommodationsExpiry = (MyDateTimePickerForAccommodationsExpiry)c;
				result = myDateTimePickerForAccommodationsExpiry.Value.ToShortDateString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00032998 File Offset: 0x00031998
		public static double GetExtraTimePercent(string extraTimeText, int extraTimeType)
		{
			double num = DynamicScreen.ExtractDouble(extraTimeText);
			double result;
			if (extraTimeText.Length > 2 && extraTimeText[0] == '[' && extraTimeText[extraTimeText.Length - 1] == ']')
			{
				result = -num;
			}
			else
			{
				switch (extraTimeType)
				{
				case 0:
				{
					string text = extraTimeText.ToLower();
					int num2 = text.LastIndexOf("min");
					if (num2 > 0)
					{
						string text2 = "";
						for (int i = num2 + 3; i < text.Length; i++)
						{
							char c = text[i];
							if (c == '/')
							{
								text2 += "per";
							}
							else if (char.IsLetter(c))
							{
								text2 += c;
							}
						}
						if (text2.IndexOf("perhour") == 0 || text2.IndexOf("utesperhour") == 0)
						{
							return num / 60.0;
						}
					}
					return num / 100.0;
				}
				case 1:
					return num / 60.0;
				case 2:
					return num - 1.0;
				case 3:
					return num;
				case 5:
					return (num - 1.0) / 100.0;
				}
				result = num / 100.0;
			}
			return result;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00032B34 File Offset: 0x00031B34
		public static double ExtractDouble(string s)
		{
			string text = "";
			bool flag = false;
			foreach (char c in s)
			{
				if (char.IsDigit(c))
				{
					text += c;
				}
				else if (!flag && c == '.')
				{
					text += c;
					flag = true;
				}
			}
			if (text.Length > 0)
			{
				try
				{
					return double.Parse(text);
				}
				catch
				{
					return 0.0;
				}
			}
			return 0.0;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00032C04 File Offset: 0x00031C04
		private static int ExtractNumber(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				if (char.IsNumber(c))
				{
					text += c;
				}
			}
			if (text.Length < 1)
			{
				text = "0";
			}
			return int.Parse(text);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00032C80 File Offset: 0x00031C80
		public static bool IntToBool(int i)
		{
			return i != 0;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00032C9C File Offset: 0x00031C9C
		public static int BoolToInt(bool b)
		{
			int result;
			if (b)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00032CBC File Offset: 0x00031CBC
		public static byte[] StringToBytes(string txt, bool encrypt, TripleDESEncryptionClass tripleDES)
		{
			if (txt == null)
			{
				txt = "";
			}
			byte[] result;
			if (encrypt)
			{
				result = tripleDES.Encrypt(txt);
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetBytes(txt);
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00032D00 File Offset: 0x00031D00
		public static string BytesToString(byte[] bytes, bool decrypt, TripleDESEncryptionClass tripleDES)
		{
			string result;
			if (bytes == null)
			{
				result = "";
			}
			else if (decrypt)
			{
				result = tripleDES.Decrypt(bytes);
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetString(bytes);
			}
			return result;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00032D48 File Offset: 0x00031D48
		public static ScreenInfo GetScreenInfo(UnivDataAdapter da, int screenNum, Panel p_data, bool applyColWidthToCurrentPanel)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.ScreensExtended);
			bool flag2 = da.DoesColumnExist("screens", "studentnumbercaption");
			da.SelectCommand.CommandText = "SELECT screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid";
			if (flag)
			{
				UnivCommand selectCommand = da.SelectCommand;
				selectCommand.CommandText += ",fontname,fontsize,groupids,iswebscreen,longdescription,controlIdToActivate";
			}
			else
			{
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText += ",'' AS fontname,0 AS fontsize,'' AS groupids,0 AS iswebscreen,'' AS longdescription,0 AS controlIdToActivate";
			}
			if (flag2)
			{
				UnivCommand selectCommand3 = da.SelectCommand;
				selectCommand3.CommandText += ",studentnumbercaption,studentnumberautogeneraterule,studentnamehidden";
			}
			else
			{
				UnivCommand selectCommand4 = da.SelectCommand;
				selectCommand4.CommandText += ",'' AS studentnumbercaption,'' AS studentnumberautogeneraterule,0 AS studentnamehidden";
			}
			UnivCommand selectCommand5 = da.SelectCommand;
			selectCommand5.CommandText += " FROM screens WHERE screennum=@screennum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			int num = 0;
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num2 = (int)dataRow[0];
					int num3 = (int)dataRow[2];
					if (num2 == screenNum && (num == 0 || num3 == num))
					{
						return DynamicScreen.GetScreenInfo(p_data.Width, dataRow, p_data, applyColWidthToCurrentPanel);
					}
				}
			}
			return null;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00032F34 File Offset: 0x00031F34
		private static ScreenInfo GetScreenInfo(int width, DataRow dr, Panel p_data, bool applyColWidthToCurrentPanel)
		{
			int height = SystemInformation.PrimaryMonitorMaximizedWindowSize.Height;
			return ScreenInfo.GetScreenInfo(dr, p_data, applyColWidthToCurrentPanel, width, height, false, Color.Transparent, Color.Transparent);
		}

		// Token: 0x04000269 RID: 617
		public const int SCREENS_StudentInfo = 1;

		// Token: 0x0400026A RID: 618
		public const int EVENTHANDLERS_ListViewSubItemClicked = 1;

		// Token: 0x0400026B RID: 619
		public const int EVENTHANDLERS_ListViewDoubleClicked = 2;

		// Token: 0x0400026C RID: 620
		public const int EVENTHANDLERS_ListViewAddButtonClicked = 3;

		// Token: 0x0400026D RID: 621
		public const int EVENTHANDLERS_ListViewKeyUp = 4;

		// Token: 0x0400026E RID: 622
		public const int EVENTHANDLERS_ListViewHeaderClicked = 5;

		// Token: 0x0400026F RID: 623
		public const int slice = 3000;

		// Token: 0x04000270 RID: 624
		public const char LIST_COL_SEPARATOR = '\0';

		// Token: 0x04000271 RID: 625
		public const char LIST_ROW_SEPARATOR = '\t';

		// Token: 0x04000272 RID: 626
		public static int[] ControlCodes_DataHolding = new int[]
		{
			1,
			2,
			3,
			4,
			6,
			7,
			10,
			12,
			11,
			14,
			100,
			200
		};

		// Token: 0x04000273 RID: 627
		public static string ByteStringDelimiter = "\n-\n";

		// Token: 0x04000274 RID: 628
		private static DataTable _templateMainInfoTable;

		// Token: 0x04000275 RID: 629
		private static DataTable _templateOtherInfoTable;

		// Token: 0x04000276 RID: 630
		private static DataTable _templateDateTimeInfoTable;

		// Token: 0x04000277 RID: 631
		private static DataTable _templateImageInfoTable;

		// Token: 0x0200003B RID: 59
		public enum DynamicDataFormatType
		{
			// Token: 0x0400027B RID: 635
			Runtime,
			// Token: 0x0400027C RID: 636
			Database
		}
	}
}
